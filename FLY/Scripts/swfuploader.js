var iMaxUpdateFileSize = 1024 * 1024 * 500;
//初始化上传控件
//ConId:控件Id
//folderName：保存的文件夹名称
//type：是否是单文件上传（logo图），true：是，false：不是（多图，比如是：资源图片集合）



function StartUploadify(ConId, folderName, type) {
    $("#" + ConId).uploadify({
        'uploader': '/Scripts/uploadify-v2.1.4/uploadify.swf?s' + Math.random(),
        'cancelImg': '/Scripts/uploadify-v2.1.4/cancel.png',
        'buttonText': "Select File",
        'buttonImg': '/Scripts/uploadify-v2.1.4/selectfiles.png',
        'width': 185,
        'height': 30,
        'script': '/WFUpload.aspx?folderName=' + folderName + '&s=' + Math.random(),
        'folder': '/Attachment',
        'fileDesc': 'Files',
        'fileExt': '*.xlsx;*.xls',
        'sizeLimit': iMaxUpdateFileSize,
        'simUploadLimit': 1,
        'multi': false,
        'auto': true,
        'onSelect': function (a, b, c) { /*选择文件上传时可以禁用某些按钮*/ },
        'onComplete': function (a, b, c, d, e) {
            if (type) {//单文件上传 调用SetLogoImg
                alert(d);
                SetLogoImg(d);
            }
            if (!type) {//多文件上传 SetFlashImages
                alert(d);
                SetLogoImg(d);
            }
        },
        'onAllComplete': function (a, b) {
            alert(a);
            enabledSaveButton(true);
        },
        'onCancel': function (a, b, c, d, e) { },
        'onError': function (a, b, c, d, e) {
            if (c.size > iMaxUpdateFileSize) {
                alert("Excel大小应500M以内，请修改后再上传");
                setTimeout("$('#" + ConId + "').uploadifyCancel('" + b + "')", 2000);
            }
        }
    });
}

 
  