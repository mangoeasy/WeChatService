﻿var Record = {
    viewModel: {
        PageReady: ko.observable(false),
        LocalId: ko.observable(),
        StartState: ko.observable(false),
        PlayState: ko.observable(false)
    }
};

Record.viewModel.StartRecord = function () {
    wx.startRecord();
    ko.mapping.fromJS(true, {}, Record.viewModel.StartState);
};
Record.viewModel.StopRecord = function () {
    wx.stopRecord({
        success: function (res) {
            ko.mapping.fromJS(res.localId, {}, Record.viewModel.LocalId);
            $('#resultmodal').modal({
                show: true
            });
        }
    });
    ko.mapping.fromJS(false, {}, Record.viewModel.StartState);
};
Record.viewModel.PlayVoice = function () {
    var id = ko.toJS(Record.viewModel.LocalId);
    wx.playVoice({
        localId: id // 需要播放的音频的本地ID，由stopRecord接口获得
    });
    ko.mapping.fromJS(true, {}, Record.viewModel.PlayState);
};
Record.viewModel.PauseVoice = function () {
    var id = ko.toJS(Record.viewModel.LocalId);
    wx.pauseVoice({
        localId: id // 需要播放的音频的本地ID，由stopRecord接口获得
    });
    ko.mapping.fromJS(false, {}, Record.viewModel.PlayState);
};

$(function () {
    ko.applyBindings(Record);
    var model = {
        url: location.href,
        jsApiList: 'startRecord,stopRecord,playVoice,pauseVoice,onMenuShareTimeline,onMenuShareAppMessage,onMenuShareQQ,onMenuShareWeibo,onMenuShareQZone'
    };
    $.get('/api/Initialize', function (result) {
        ko.mapping.fromJS(result, {}, Record.viewModel.PageReady);
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
                                title: '微信录音-Mangoeasy', // 分享标题
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/Record', // 分享链接
                                imgUrl: 'http://wechatservice.demo.mangoeasy.com//Content/Images/logoSZ.png', // 分享图标
                                success: function () {

                                },
                                cancel: function () {
                                    // 用户取消分享后执行的回调函数
                                }
                            });
                            wx.onMenuShareAppMessage({
                                title: '微信录音-Mangoeasy', // 分享标题
                                desc: '请关注 Mangoeasy 获取更多资讯', // 分享描述
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/Record', // 分享链接
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
                                title: '微信录音-Mangoeasy', // 分享标题
                                desc: '请关注 Mangoeasy 获取更多资讯', // 分享描述
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/Record', // 分享链接
                                imgUrl: 'http://wechatservice.demo.mangoeasy.com//Content/Images/logoSZ.png', // 分享图标
                                success: function () {
                                    // 用户确认分享后执行的回调函数
                                },
                                cancel: function () {
                                    // 用户取消分享后执行的回调函数
                                }
                            });
                            wx.onMenuShareWeibo({
                                title: '微信录音-Mangoeasy', // 分享标题
                                desc: '请关注 Mangoeasy 获取更多资讯', // 分享描述
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/Record', // 分享链接
                                imgUrl: 'http://wechatservice.demo.mangoeasy.com//Content/Images/logoSZ.png', // 分享图标
                                success: function () {
                                    // 用户确认分享后执行的回调函数
                                },
                                cancel: function () {
                                    // 用户取消分享后执行的回调函数
                                }
                            });
                            wx.onMenuShareQZone({
                                title: '微信录音-Mangoeasy', // 分享标题
                                desc: '请关注 Mangoeasy 获取更多资讯', // 分享描述
                                link: 'http://wechatservice.demo.mangoeasy.com/demo/Record', // 分享链接
                                imgUrl: 'http://wechatservice.demo.mangoeasy.com//Content/Images/logoSZ.png', // 分享图标
                                success: function () {
                                    // 用户确认分享后执行的回调函数
                                },
                                cancel: function () {
                                    // 用户取消分享后执行的回调函数
                                }
                            });
                            wx.onVoicePlayEnd({
                                success: function (res) {
                                    //var localId = res.localId; // 返回音频的本地ID
                                    ko.mapping.fromJS(false, {}, Record.viewModel.PlayState);
                                }
                            });
                        });
                    }
                }
            });
        });
    });

});