
var global = {
  form: null,
  layer: null,
  $: null,
  config: null
};

function initJs(layui) {
  debugger;
  global.form = layui.form;
  global.layer = layui.layer;
  global.$ = layui.jquery;
  global.config = layui.config;
}
// global.form = layui.form;
// global.layer = layui.layer;
// global.$ = layui.jquery;
// global.config = layui.config;


/*弹出层*/
/*
    参数解释：
    title   标题
    url     请求的url
    w       弹出层宽度（缺省调默认值）
    h       弹出层高度（缺省调默认值）
*/
function pageShow(title, url, w, h, data) {
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
    content: url,  // 'http://sentsin.com' //type:2为Iframe，这里content是一个URL，如果你不想让iframe出现滚动条，你还可以content: ['http://sentsin.com', 'no'],
    scrollbar: false,
    zIndex: 9999
  });
}

/**
 * 初始化编辑或者新增页面数据
 */
function initData(url) {
  debugger;
  var $ = global.$, config = global.config, form = global.form;

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
      debugger;
      if (result.Success) {
        form.val("first", result.Data); //默认都放在first下面
      }
      else {
        layer.msg(result.Message);
      }
    }
  })
}

/**
 * 保存数据
 * @param {*} $ 
 * @param {*} data:要保存的数据 
 * @param {*} config:配置文件 
 * @param {*} url:保存的地址
 */
function saveDataWay(data, url, type) {
  var $ = global.$, config = global.config;
  debugger;
  //var LoadIndex = layer.msg('正在处理中', { icon: 16, shade: 0.2, time: 0 }); //显示Loading层
  var index = parent.layer.load(5, { shade: [0.5, "#5588AA"] });
  if (!type) {
    type = "POST";
  }
  debugger;
  $.ajax({
    type: type,
    // data: JSON.stringify(data),
    data: data,
    contentType: 'application/json',
    headers: {
      "Authorization": "Bearer " + sessionStorage.getItem("apiTicket"),
      "X-Requested-With": "XMLHttpRequest"
    },
    url: config.apiUrl + url,
    success: function (result) {
      var jsonData = result;//JSON.parse(result);
      layer.closeAll("loadiing");
      parent.layer.close(index);
      debugger;
      if (jsonData != null && jsonData.Success) {
        // parent.layer.close(index);
        layer.msg(jsonData.Message, { icon: 1, time: 2000 },//默认是3s
          function () { //关闭之后弹出的框
            parent.$('#search').click(); //得到父窗体的控件 
            parent.layer.closeAll();
          });
      }
      else {
        layer.msg(jsonData.Message, { icon: 2, time: 2000 });
      }
    },
    error: function (XMLHttpResponse) {
      layer.closeAll("loadiing");
      parent.layer.close(index);
      console.log("error: api request failed");
      console.log(XMLHttpResponse);
    }
  })
}

/**
 * 
 * @param {* jQuery} $ 
 * @param {* layer} layer 
 * @param {* 要删除的ID} id 
 * @param {* 要请求的地址} url 
 */
function deleteOne($, layer, id, url) {
  layer.confirm('确定要删除吗???', function (index) {
    $.ajax({
      type: "get",
      data: {
        id: id
      },
      beforeSend: function (XHR) {
        XHR.setRequestHeader("Authorization", "Bearer " + sessionStorage.getItem("apiTicket"));
      },
      url: config.apiUrl + url,
      success: function (jsonData) {
        // var jsonData = JSON.parse(result);
        if (jsonData.Success) {
          layer.msg(jsonData.Message, { icon: 1 }, function () { $("#search").click(); });
          //  $("#search").click();
        }
        else {
          layer.msg(jsonData.msg, { icon: 2 });
        }
      }
    })
  });
}

function deleteMulity($, layer, table, obj, url) {
  // 实现批量删除功能了 
  layer.confirm('确定要删除选中的数据吗???', function (index) {
    debugger
    var checkStatus = table.checkStatus(obj.config.id);

    if (checkStatus.data.length == 0) {
      layer.msg("没有选中数据", { icon: 2 });
      return
    }
    var dataIds = checkStatus.data.map(function (item) {
      return item['Id'];
    });
    $.ajax({
      type: "get",
      data: {
        ids: dataIds.join(',')
      },
      beforeSend: function (XHR) {
        XHR.setRequestHeader("Authorization", "Bearer " + sessionStorage.getItem("apiTicket"));
      },
      url: config.apiUrl + url,
      success: function (jsonData) {
        //var jsonData =result; //JSON.parse(result);
        if (jsonData.Success) {
          layer.msg(jsonData.Message, { icon: 1 }, function () { $("#search").click(); });
          // $("#search").click();
        }
        else {
          layer.msg(jsonData.Message, { icon: 2 });
        }
      }
    })
  });
}

/**
 * 单条启用禁用
 * @param {* jQuery} $ 
 * @param {* layer} layer 
 * @param {* 要删除的ID} id 
 * @param {* 要请求的地址} url 
 */
function updateStatusOne($, layer, id, url) {
  $.ajax({
    type: "get",
    data: {
      id: id
    },
    beforeSend: function (XHR) {
      XHR.setRequestHeader("Authorization", "Bearer " + sessionStorage.getItem("apiTicket"));
    },
    url: config.apiUrl + url,
    success: function (jsonData) {
      if (jsonData.Success) {
        layer.msg(jsonData.Message, { icon: 1 }, function () { $("#search").click(); });
        // $("#search").click();
      }
      else {
        layer.msg(jsonData.Message, { icon: 2 });
      }
    }
  })
}



/**
 * 批量启用禁用
 * @param {*} $ 
 * @param {*} layer 
 * @param {*} table 
 * @param {*} obj 
 * @param {*} url 
 */
function updateStatusMulity($, layer, table, obj, url, status) {
  var checkStatus = table.checkStatus(obj.config.id);
  debugger;
  if (checkStatus.data.length == 0) {
    layer.msg("没有选中数据", { icon: 2 });
    return
  }
  var dataIds = checkStatus.data.map(function (item) {
    return item['Id'];
  });
  $.ajax({
    type: "get",
    data: {
      ids: dataIds.join(','),
      status: status
    },
    beforeSend: function (XHR) {
      XHR.setRequestHeader("Authorization", "Bearer " + sessionStorage.getItem("apiTicket"));
    },
    url: config.apiUrl + url,
    success: function (jsonData) {
      if (jsonData.Success) {
        layer.msg(jsonData.Message, { icon: 1 }, function () { $("#search").click(); });

      }
      else {
        layer.msg(jsonData.Message, { icon: 2 });
      }
    }
  })
}

function LoadData(url, data, fun_callback) {
  var $ = global.$;
  $.ajax({
    url: global.config.apiUrl + url,
    type: 'get',
    data: data,
    beforeSend: function (XHR) {
      XHR.setRequestHeader("Authorization", "Bearer " + sessionStorage.getItem("apiTicket"));
    },
    dataType: 'json',
    contentType: 'application/json',
    success: function (obj) {
      if (fun_callback) {
        fun_callback(obj.Data);
      }
    },
    error: function (data) {
      layer.alert(JSON.stringify(data.Message), {
        title: 'ajax请求失败！'
      });
    }
  });
}