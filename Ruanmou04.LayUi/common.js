	
/*弹出层*/
/*
    参数解释：
    title   标题
    url     请求的url
    w       弹出层宽度（缺省调默认值）
    h       弹出层高度（缺省调默认值）
*/
function pageShow(title, url, w, h,data) {
    // debugger;
    if (title == null || title == '') {
        title = false;
    };
    if (url == null || url == '') {
        url = "404.html";
    };
    if (w == null || w == '') {
        w = ($(window).width() * 0.9);
    };
    if (h == null || h == '') {
        h = ($(window).height() - 50);
    };
    layer.open({
        type: 2,
        data: data,
        area: [w + 'px', h + 'px'],
        fix: false, //不固定
        maxmin: true,
        shadeClose: false,
        shade: 0,
        title: title,
        content:url,  // 'http://sentsin.com' //type:2为Iframe，这里content是一个URL，如果你不想让iframe出现滚动条，你还可以content: ['http://sentsin.com', 'no'],
        scrollbar: false,
        zIndex:9999
    });
} 

function initData($,url,config,form){
    $.ajax({
        type: "Get",
        // data: {
        //   userId: parent.layer.data //取的是layer.open的data的值
        // },
        beforeSend: function (XHR) {
          XHR.setRequestHeader("Authorization", "Bearer " + sessionStorage.getItem("apiTicket"));
        },
        url: config.apiUrl + url,
        success: function (result) { 
          if (result.success) {
            form.val("first", result.data); //默认都放在first下面
          }
          else {
            layer.msg(result.msg);
          }
        }
      })
}

function saveDataWay($,data,config,url){  
    //var LoadIndex = layer.msg('正在处理中', { icon: 16, shade: 0.2, time: 0 }); //显示Loading层
    var index= parent.layer.load(5, { shade: [0.5, "#5588AA"] }); 
    $.ajax({
        type: "POST",
        data: JSON.stringify(data),
        contentType: 'application/json',
        headers: { Authorization: "Bearer " + sessionStorage.getItem("apiTicket") },
        url:config.apiUrl +url,
        success: function (result) {
        var jsonData=result;//JSON.parse(result);
            layer.closeAll("loadiing");
            if (jsonData.success) {	
             parent.layer.close(index);
                layer.msg(jsonData.msg, { icon: 1, time: 2000 },//默认是3s
                function(){ //关闭之后弹出的框
                            parent.$('#search').click(); //得到父窗体的控件 
                            parent.layer.closeAll();
                });						
            }
            else {
                layer.msg(jsonData.msg, { icon: 5 });
            }
        },
        error: function (XMLHttpResponse) {
            layer.close(index);
            console.log("error: api request failed");
            console.log(XMLHttpResponse);
          }
    })
}
// var LoadIndex = layer.msg('正在处理中', { icon: 16, shade: 0.2, time: 0 }); //显示Loading层
// $.ajax({
//     type: 'Post',
//     dataType: "json",
//     data: JSON.stringify(user),
//     contentType: 'application/json',
//     headers: { Authorization: "Bearer " + sessionStorage.getItem("apiTicket") },
//     url: config.apiUrl + '/Users/SaveUser',
//     success: function (result) {

//       layer.close(LoadIndex);
//       console.log(result);
//       if (result.success) {
//         layer.msg("保存成功", { icon: 1 });
//       }
//       else {
//         layer.msg(result.msg, { icon: 2 });
//       }
//     },
//     error: function (XMLHttpResponse) {
//       layer.close(LoadIndex);
//       console.log("error: api request failed");
//       console.log(XMLHttpResponse);
//     }
//   })