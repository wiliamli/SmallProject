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

/**

 @Title: layui.config
 @Author: magic
 @License：MIT

 */

layui.define('layer', function (exports) {
 
    var MOD_NAME = 'config'
     , config = {
         apiUrl: 'https://localhost:44367/api',
         fileUrl: "https://localhost:44367/fileUpload/"
     }

 //操作当前实例 
 exports(MOD_NAME, config);
});


