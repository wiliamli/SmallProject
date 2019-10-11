//全局js 
var common = { 
    Init: function ($, layer) {  
        this.CheckLogin($, layer); //检查是否登录
        //初始化绑定事件
        this.Login($, layer);
        this.Register($, layer);
        this.Logout($, layer);
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
            sessionStorage.removeItem("token");
            common.CheckLogin($, layer);
        });
    },
    CheckLogin: function ($, layer) {
        if (typeof (Storage) === "undefined") {
            layer.alert("对不起，本站不支持该浏览器，请切换别的浏览器访问", { title: "系统提示" });
            return;
        } 
        var apiTicket = sessionStorage.getItem("token");
        var $HeaderInfo = $(".rm-header-right");
        var IsMemberPage = $HeaderInfo.attr("member"); 
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
            } else { 
                $HeaderInfo.html(this.WebLoginHtml);
                //var sHtml = '<div class="reg"><a href="mbInfo.html" class="a-mbcenter">个人中心</a></div>';
                //sHtml += '<div class="login"><a href="javascript:void(0);" class="a-logout">退出</a></div>'; 
                //$HeaderInfo.html(sHtml);
            }
        } 
    }
};
