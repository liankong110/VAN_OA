/* 
* @name: 		tinybox.Common.js
* @overview: 	专题模板后台,公共弹框js调用库
* @author: 	    sxh3173@17u.com
* @require: 	tinybox.Common.js
* @version: 	1.0
* @date: 		2012-9-3
******************************/

//弹框iframe
function tinybox_Showiframe(myURL, myWidth, myHeight, isscrolling) {
    isscrolling = isscrolling || "no"; //scrolling='" + isscrolling + "'
    var myCont = "<iframe src='" + myURL + "' width='" + myWidth + "' height='" + myHeight + "' frameborder='no' border='0'  allowtransparency='yes'></iframe>";
    TINY.box.show(myCont, 0, 0, 0, 1);
}
//直接弹出页面
function tinybox_ShowUrl(myURL, myWidth, myHeight) {
    myWidth = myWidth || 0;
    myHeight = myHeight || 0;
    TINY.box.show(myURL, 1, myWidth, myHeight, 1);
}

//弹框内部使用
function tinybox_ShowUrlfill(myURL) {
    TINY.box.fill(myURL, 1, 0, 0, 1);
}

//加载html
function tinybox_ShowHtml(strHtml) {
    TINY.box.show(strHtml, 0, 0, 0, 1);
}
//内部使用
function tinybox_ShowHtmlfill(strHtml) {
    TINY.box.fill(strHtml, 0, 0, 0, 1);
}

//设置框的大小
function tinybox_ShowSetSize(myWidth, myHeight) {
    TINY.box.size('tinybox', myWidth, myHeight, 4);
}

//关闭
function tinybox_Close() {
    TINY.box.hide();
}

//关闭并刷新本页面
function tinybox_CloseRefresh() {
    location.reload();
    TINY.box.hide();
}

//父级关闭
function tinybox_CloseParent() {
    parent.TINY.box.hide();
}