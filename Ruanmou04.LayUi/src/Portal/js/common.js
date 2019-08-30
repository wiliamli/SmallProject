//全局js
var common = { 
    Init: function ($, layer) {
        this.Login($, layer);
        this.Register($, layer);
    },
    Login: function ($, layer) {
        $(".a-login").click(function () {
            layer.open({
                title: '登录'
                , type: 2
                , content: ['poplogin.html', 'no']
                , area: ['350px', '320px']
            });  
        })
    },
    Register: function ($, layer) {
        $(".a-reg").click(function () {
            layer.open({
                title: '注册'
                , type: 2
                , content: 'popregister.html'
                , area: ['420px', '500px']
            });
        })
    }

};
