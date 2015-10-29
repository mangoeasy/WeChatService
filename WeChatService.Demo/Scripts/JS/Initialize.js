var page = {
    viewModel: {
        isInitialize: ko.observable(false)
    }
};
$(function () {
    ko.applyBindings(page);
    $.get('/api/Initialize', function(result) {
        ko.mapping.fromJS(result, {}, page.viewModel.isInitialize);
    });
});