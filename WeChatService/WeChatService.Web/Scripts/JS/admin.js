var Articles = {
    viewModel: {
        CurrentPageIndex: ko.observable(),
        AllPage: ko.observable(),
        PageSize: ko.observable(),
        Models: ko.observableArray(),
        SearchParm: {
            pageSize: 10,
            pageIndex: 1,
            title: ko.observable(''),
            description: ko.observable(''),
            body: ko.observable(''),
            isPublish: ko.observable(''),
            articleTypeId: ko.observable(''),
            IsRecommend: ko.observable(''),
            IsPush: ko.observable('')
        },
        SelectedArticleModel: {
            Id: ko.observable(),
            Title: ko.observable(),
            Description: ko.observable(),
            Body: ko.observable(),
            ArticleTypeId: ko.observable(),
            IsRecommend: ko.observable(false),
            IsPush: ko.observable(false),
            Thumbnail: ko.observable('')
        },
        ArticleTypes: ko.observableArray(),
    }
};

Articles.viewModel.Previous = function () {
    if (Articles.viewModel.CurrentPageIndex() > 1) {
        var pageIndex = Articles.viewModel.CurrentPageIndex() - 1;
        Articles.viewModel.SearchParm.pageIndex = pageIndex;
        Articles.viewModel.SearchParm.articleTypeId = $('#articleTypeId').val();
        $.get("/api/Article/", Articles.viewModel.SearchParm, function (result) {
            ko.mapping.fromJS(result, {}, Articles.viewModel);
        });
    }
};
Articles.viewModel.Next = function () {
    if (Articles.viewModel.CurrentPageIndex() < Articles.viewModel.AllPage()) {
        var pageIndex = Articles.viewModel.CurrentPageIndex() + 1;
        Articles.viewModel.SearchParm.pageIndex = pageIndex;
        Articles.viewModel.SearchParm.articleTypeId = $('#articleTypeId').val();
        $.get("/api/Article/", Articles.viewModel.SearchParm, function (result) {
            ko.mapping.fromJS(result, {}, Articles.viewModel);
        });
    }
};
Articles.viewModel.PagesArr = ko.computed({
    read: function () {
        var arr = [];
        var startIndex = Articles.viewModel.CurrentPageIndex();
        if (startIndex == 1) {
            for (var j = 1; j <= 5 && j <= Articles.viewModel.AllPage() ; j++) {
                arr.push(j);
            }
        } else {
            if (startIndex % 5 != 0) {
                startIndex = startIndex - startIndex % 5;
            }

            for (var i = startIndex == 0 ? (startIndex + 1) : startIndex; i <= (startIndex + 5) && i <= Articles.viewModel.AllPage() ; i++) {

                arr.push(i);
            }
        }
        return arr;
    }
});
Articles.viewModel.GotoPage = function () {
    Articles.viewModel.SearchParm.pageIndex = this;
    Articles.viewModel.SearchParm.articleTypeId = $('#articleTypeId').val();
    $.get("/api/Article/", Articles.viewModel.SearchParm, function (result) {
        ko.mapping.fromJS(result, {}, Articles.viewModel);
    });
};
//新增文章(弹出modal)
Articles.viewModel.Create = function () {
    $('#createmodal').modal({
        show: true,
        backdrop: 'static'
    });
};
//保存新增的文章
Articles.viewModel.SaveNewArticle = function () {
    var model = {
        ArticleTypeId: $('#type').val(),
        Body: CKEDITOR.instances.newbody.getData(),
        Title: $('#title').val(),
        Description: $('#description').val(),
        IsPush: Articles.viewModel.SelectedArticleModel.IsPush(),
        IsRecommend: Articles.viewModel.SelectedArticleModel.IsRecommend(),
        Thumbnail:Articles.viewModel.SelectedArticleModel.Thumbnail()
    };
    $.post("/api/Article/", model, function (result) {
        if (result.Error) {
            Helper.ShowErrorDialog(result.Message);
        } else {
            Articles.viewModel.Refresh();
            $('#createmodal').modal('hide');
        }
    });
};
//预览对话框
Articles.viewModel.ShowPreviewModal = function () {
    $.get("/api/Article/" + this.Id(), function (result) {
        ko.mapping.fromJS(result, {}, Articles.viewModel.SelectedArticleModel);
        CKEDITOR.instances.body.setData(Articles.viewModel.SelectedArticleModel.Body());
        $('#previewmodal').modal({
            show: true,
            backdrop: 'static'
        });
    });
};
//保存文章
Articles.viewModel.SaveArticle = function () {
    Articles.viewModel.SelectedArticleModel.Body(CKEDITOR.instances.body.getData());
    $.ajax({
        type: 'put',
        url: '/api/Article/' + Articles.viewModel.SelectedArticleModel.Id(),
        contentType: 'application/json',
        dataType: "json",
        data: ko.toJSON(Articles.viewModel.SelectedArticleModel),
        success: function (result) {
            if (result.Error) {
                Helper.ShowErrorDialog(result.Message);
            } else {
                Articles.viewModel.Refresh();
                $('#previewmodal').modal('hide');
            }
        }
    });

};
//刷新
Articles.viewModel.Refresh = function () {
    Articles.viewModel.SearchParm.articleTypeId = $('#articleTypeId').val();
    $.get("/api/Article/", Articles.viewModel.SearchParm, function (result) {
        ko.mapping.fromJS(result, {}, Articles.viewModel);
    });
};
//搜索
Articles.viewModel.Search = function () {
    Articles.viewModel.SearchParm.pageIndex = 1;
    Articles.viewModel.SearchParm.articleTypeId = $('#articleTypeId').val();
    $.get("/api/Article/", Articles.viewModel.SearchParm, function (result) {
        ko.mapping.fromJS(result, {}, Articles.viewModel);
    });
};
//删除
Articles.viewModel.Delete = function () {
    $.ajax({
        type: 'delete',
        url: "/api/Article/" + this.Id(),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            if (!result.Error) {
                Articles.viewModel.Refresh();
            } else {
                Helper.ShowErrorDialog(result.Message);
            }
        }
    });
};
ko.bindingHandlers.date = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Date formats: http://momentjs.com/docs/#/displaying/format/
        var pattern = allBindings.format || 'DD/MM/YYYY';

        var output = "-";
        if (valueUnwrapped !== null && valueUnwrapped !== undefined && valueUnwrapped.length > 0) {
            output = moment(valueUnwrapped).format(pattern);
        }

        if ($(element).is("input") === true) {
            $(element).val(output);
        } else {
            $(element).text(output);
        }
    }
};
$(function () {
    ko.applyBindings(Articles);
    $.get("/api/ArticleType/", function (result) {
        ko.mapping.fromJS(result, {}, Articles.viewModel.ArticleTypes);
        $.get("/api/Article/?pagesize=10", function (data) {
            ko.mapping.fromJS(data, {}, Articles.viewModel);
        });
    });

    CKEDITOR.replace('body', {
        // Load the German interface.
        language: 'zh-cn',
    });
    CKEDITOR.replace('newbody', {
        // Load the German interface.
        language: 'zh-cn',
    });
});