//全局js
var common = {
    Login: function ($, layer) {
        $(".a-login").click(function () {
            layer.open({
                title: '在线调试'
                , content: '可以填写任意的layer代码'
            });  
        })
        console.log('这是common');
    }
};