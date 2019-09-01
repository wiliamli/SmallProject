/***
 * 配置文件 
 * ***/

layui.define(function (exports) { 
    var config = {
        apiUrl: 'https://localhost:44367/', 
        Verson: '1.0.0.1',
    } 
    //操作当前实例 
    exports('config', config);
});