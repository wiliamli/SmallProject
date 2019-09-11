/***
 * 配置文件 
 * ***/

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


