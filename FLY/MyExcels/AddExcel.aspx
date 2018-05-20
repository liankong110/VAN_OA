<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddExcel.aspx.cs" Inherits="VAN_OA.MyExcels.AddExcel"
    MasterPageFile="~/DefaultMaster.Master" Title="Excel上传" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                Excel上传
            </td>
        </tr>
        <tr>
            <td>
                Excel名称：
            </td>
            <td>
                <asp:TextBox ID="txtAttName" runat="server"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
            <td>
                Excel文件：
            </td>
            <td>
                <input type="file" name="fileUpload" id="fileUpload" />
                <input type="hidden" id="lastFileName" name="lastFileName" value="" />
                    <input type="hidden" id="FileName" name="FileName" value="" />
            </td>
        </tr>
        <tr>
            <td>
                描述：
            </td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" OnClientClick="return check();"  Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <script src="../Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="../uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../uploadify/jquery.uploadify.js" type="text/javascript"></script>
    <link href="../uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

       function check()
       { 
            if($("#<%=txtAttName.ClientID %>").val()=="")
            {
                alert("请填写Excel名称");
                return false;
            }
            if($("#lastFileName").val()=="")
            {
                alert("请选择文件");
                return false;
            }
           
            return true;
       }
 
        $(function () {
            $("#fileUpload").uploadify({
                height: 30,
                swf: '/uploadify/uploadify.swf',
                uploader: '/WFUpload.aspx',
                width: 120,
                fileTypeExts: '*.xlsx;*.xls,*.xlsm',
                buttonText: '选择Excel',
                auto: true,
                multi: false,
                removeCompleted: false,
               
                fileSizeLimit: '500MB', 
                successTimeout:'1800',
                'onUploadSuccess':function(file,data,response){ 
                         $("#lastFileName").val(data);
                            $("#FileName").val(file.name);
                    },
                    //加上此句会重写onSelectError方法【需要重写的事件】
                    'overrideEvents': ['onSelectError', 'onDialogClose'],
                    //返回一个错误，选择文件的时候触发
                    'onSelectError':function(file, errorCode, errorMsg){
                        switch(errorCode) {
                            case -110:
                                alert("文件 ["+file.name+"] 大小超出系统限制的" + jQuery('#upload_org_code').uploadify('settings', 'fileSizeLimit') + "大小！");
                                break;
                            case -120:
                                alert("文件 ["+file.name+"] 大小异常！");
                                break;
                            case -130:
                                alert("文件 ["+file.name+"] 类型不正确！");
                                break;
                        }
                    },
            });
        });
    </script>
</asp:Content>
