
//全局js 
var portalCommon = {
    form: null,
    layer: null,
    $: null,
    config: null,
    loginUrl: 'login.html',
    Authenticate_Failed: 'Authenticate_Failed',

    Init: function ($, layer) {
        this.CheckLogin($, layer); //检查是否登录
        //初始化绑定事件
        this.Login($, layer);
        this.Register($, layer);
        this.Logout($, layer);

        this.form = layui.form;
        this.layer = layui.layer;
        this.$ = layui.jquery;
        this.config = layui.config;
    },
    WebLoginHtml: '<div class="reg"><a href="mbInfo.html" class="a-mbcenter">个人中心</a></div><div class="login"><a href="javascript:void(0);" class="a-logout">退出</a></div>',
    Login: function ($, layer) {
        $(".rm-header").delegate(".a-login", "click", function () {
            layer.open({
                title: '登录'
                , type: 2
                , content: ['poplogin.html', 'no']
                , area: ['350px', '320px']
            });
        });
    },
    Register: function ($, layer) {
        $(".rm-header").delegate(".a-reg", "click", function () {
            layer.open({
                title: '注册'
                , type: 2
                , content: 'popregister.html'
                , area: ['420px', '500px']
            });
        });
    },
    Logout: function ($, layer) {
        $(".rm-header").delegate(".a-logout", "click", function () {
            localStorage.removeItem("token");
            portalCommon.CheckLogin($, layer);
        });
    },
    CheckLogin: function ($, layer) {
        if (typeof (Storage) === "undefined") {
            layer.alert("对不起，本站不支持该浏览器，请切换别的浏览器访问", { title: "系统提示" });
            return;
        }
        var account = localStorage.getItem("account");
        var apiTicket = localStorage.getItem("token");
        var $HeaderInfo = $(".rm-header-right");
        var IsMemberPage = $HeaderInfo.attr("member");
        var $mbNickName = $(".mb-nickname");
        if (apiTicket == null) {
            //未登录
            if (IsMemberPage == "1") {
                //跳转去首页
                window.location.href = "index.html";
            } else {
                //登录页
                var sHtml = '<div class="login"><a href="javascript:void(0);" class="a-login">登录</a></div>';
                sHtml += '<div class="reg"><a href="javascript:void(0);" class="a-reg">注册</a></div>';
                $HeaderInfo.html(sHtml);
            }
        } else {
            //已登录 
            if (IsMemberPage == "1") {
                var sHtml = '<div class="login"><a href="javascript:void(0);" class="a-logout">退出</a></div>';
                $HeaderInfo.html(sHtml);
                $mbNickName.html(account);
            } else {
                $HeaderInfo.html(this.WebLoginHtml);
                //var sHtml = '<div class="reg"><a href="mbInfo.html" class="a-mbcenter">个人中心</a></div>';
                //sHtml += '<div class="login"><a href="javascript:void(0);" class="a-logout">退出</a></div>'; 
                //$HeaderInfo.html(sHtml);
            }
        }
    },
    SaveDataWay: function (data, url, type) {
        var $ = portalCommon.$, config = portalCommon.config;
        //var LoadIndex = layer.msg('正在处理中', { icon: 16, shade: 0.2, time: 0 }); //显示Loading层
        var index = parent.layer.load(5, { shade: [0.5, "#5588AA"] });
        if (!type) {
            type = "POST";
        }
        $.ajax({
            type: type,
            // data: JSON.stringify(data),
            data: data,
            contentType: 'application/json',
            headers: {
                "token": "Bearer " + localStorage.getItem("token"),
                "X-Requested-With": "XMLHttpRequest"
            },
            url: config.apiUrl + url,
            success: function (result) {
                var jsonData = result;//JSON.parse(result);
                layer.closeAll("loadiing");
                parent.layer.close(index);
              
                if (jsonData != null && jsonData.Success) {
                    layer.msg(jsonData.Message, { icon: 1, time: 2000 },//默认是3s
                        function () { });
                }
                else {
                    layer.msg(jsonData.Message, { icon: 2, time: 2000 });
                    if (jsonData.StatusCode && portalCommon.Authenticate_Failed == jsonData.StatusCode) {
                        location.href = portalCommon.loginUrl;
                    }
                }
            },
            error: function (XMLHttpResponse) {
                layer.closeAll("loadiing");
                parent.layer.close(index);
                console.log("error: api request failed");
                console.log(XMLHttpResponse);
            }
        })
    },

    InitData: function (url) {
        debugger;
        var $ = portalCommon.$, config = portalCommon.config, form = portalCommon.form;
        $.ajax({
            type: "Get",
            beforeSend: function (XHR) {
                XHR.setRequestHeader("token", "Bearer " + localStorage.getItem("token"));
            },
            url: config.apiUrl + url,
            success: function (result) {
                if (result.Success) {
                    form.val("first", result.Data); //默认都放在first下面
                    $(".mb-nickname").html(result.Name);
                }
                else {
                    if (result.StatusCode && portalCommon.Authenticate_Failed == result.StatusCode) {
                        location.href = portalCommon.loginUrl;
                    }

                    localStorage.removeItem("token");
                    portalCommon.Init($, layer);
                }
            }
        })
    },
    LoadData: function (url, data, fun_callback, type) {
        var $ = portalCommon.$;
        if (!type) {
            type = 'get';
        }
        $.ajax({
            url: portalCommon.config.apiUrl + url,
            type: type,
            data: data,
            beforeSend: function (XHR) {
                XHR.setRequestHeader("token", "Bearer " + localStorage.getItem("token"));
            },
            dataType: 'json',
            contentType: 'application/json',
            success: function (obj) {
                if (obj.Success) {
                    if (fun_callback) {
                        fun_callback(obj.Data);
                    }
                } else {
                    layer.msg(obj.Message, { icon: 2, time: 2000 });
                    if (obj.StatusCode && portalCommon.Authenticate_Failed == obj.StatusCode) {
                        location.href = portalCommon.loginUrl;
                    }
                }
            },
            error: function (data) {
                layer.alert(JSON.stringify(data.Message), {
                    title: 'ajax请求失败！'
                });
            }
        });
    }
};


