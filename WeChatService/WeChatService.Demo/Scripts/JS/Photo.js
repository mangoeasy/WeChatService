var Photo = {
    viewModel: {
        PageReady: ko.observable(false),
        ImgUrls: ko.observableArray(),
    }
};
var i = 0;
Photo.viewModel.Action = function () {
    i = 0;
    wx.chooseImage({
        count: 2, // 默认9
        sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
        sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
        success: function (res) {
            //var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片
            ko.mapping.fromJS(res.localIds, {}, Photo.viewModel.ImgUrls);
            $('#resultmodal').modal({
                show: true
            });
        }
    });
};

Photo.viewModel.Upload = function () {
    var ids = ko.toJS(Photo.viewModel.ImgUrls);
    var length = ids.length;
    function upload() {
        wx.uploadImage({
            localId: ids[i],
            success: function (res) {
                i++;
                alert(i + "张图片上传成功，服务器Id为：" + res.serverId);
                if (i < length) {
                    upload();
                }
            },
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
    }
    upload();
};
$(function () {
    ko.applyBindings(Photo);
    var model = {
        url: location.href,
        jsApiList: 'chooseImage,uploadImage,onMenuShareTimeline,onMenuShareAppMessage,onMenuShareQQ,onMenuShareWeibo,onMenuShareQZone'
    };
    $.get('/api/Initialize', function (result) {
        ko.mapping.fromJS(result, {}, Photo.viewModel.PageReady);
        $.get('/api/HeaderSetting/', function (base64) {
            $.ajax({
                type: "get",
                url: "http://WeChatService.mangoeasy.com/api/JsApiConfig/",
                data: model,
                beforeSend: function (xhr) { //beforeSend定义全局变量
                    xhr.setRequestHeader("Authorization", base64); //Authorization 需要授权,即身体验证
                },
                success: function (xmlDoc, textStatus, xhr) {
                    if (xhr.status == 200) {
                        wx.config({
                            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
                            appId: xhr.responseJSON.AppId, // 必填，公众号的唯一标识
                            timestamp: xhr.responseJSON.Timestamp, // 必填，生成签名的时间戳
                            nonceStr: xhr.responseJSON.NonceStr, // 必填，生成签名的随机串
                            signature: xhr.responseJSON.Signature,// 必填，签名，见附录1
                            jsApiList: xhr.responseJSON.JsApiList // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
                        });
                        wx.ready(function () {
                            wx.onMenuShareTimeline({
                                title: '微信调用相机 - Mangoeasy', // 分享标题
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/photo', // 分享链接
                                imgUrl: 'http://wechatservice.demo.mangoeasy.com//Content/Images/logoSZ.png', // 分享图标
                                success: function () {

                                },
                                cancel: function () {
                                    // 用户取消分享后执行的回调函数
                                }
                            });
                            wx.onMenuShareAppMessage({
                                title: '微信调用相机 - Mangoeasy', // 分享标题
                                desc: '公众号 - Mangoeasy', // 分享描述
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/photo', // 分享链接
                                imgUrl: 'http://wechatservice.demo.mangoeasy.com//Content/Images/logoSZ.png', // 分享图标
                                type: 'link', // 分享类型,music、video或link，不填默认为link
                                dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                                success: function () {
                                    // 用户确认分享后执行的回调函数
                                },
                                cancel: function () {
                                    // 用户取消分享后执行的回调函数
                                }
                            });
                            wx.onMenuShareQQ({
                                title: '微信调用相机 - Mangoeasy', // 分享标题
                                desc: '公众号 - Mangoeasy', // 分享描述
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/photo', // 分享链接
                                imgUrl: 'http://wechatservice.demo.mangoeasy.com//Content/Images/logoSZ.png', // 分享图标
                                success: function () {
                                    // 用户确认分享后执行的回调函数
                                },
                                cancel: function () {
                                    // 用户取消分享后执行的回调函数
                                }
                            });
                            wx.onMenuShareWeibo({
                                title: '微信调用相机 - Mangoeasy', // 分享标题
                                desc: '公众号 - Mangoeasy', // 分享描述
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/photo', // 分享链接
                                imgUrl: 'http://wechatservice.demo.mangoeasy.com//Content/Images/logoSZ.png', // 分享图标
                                success: function () {
                                    // 用户确认分享后执行的回调函数
                                },
                                cancel: function () {
                                    // 用户取消分享后执行的回调函数
                                }
                            });
                            wx.onMenuShareQZone({
                                title: '微信调用相机 - Mangoeasy', // 分享标题
                                desc: '公众号 - Mangoeasy', // 分享描述
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/photo', // 分享链接
                                imgUrl: 'http://wechatservice.demo.mangoeasy.com//Content/Images/logoSZ.png', // 分享图标
                                success: function () {
                                    // 用户确认分享后执行的回调函数
                                },
                                cancel: function () {
                                    // 用户取消分享后执行的回调函数
                                }
                            });
                        });
                    }
                }
            });
        });
    });

});