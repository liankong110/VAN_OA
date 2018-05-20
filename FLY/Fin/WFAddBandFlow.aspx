<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFAddBandFlow.aspx.cs" Inherits="VAN_OA.Fin.WFAddBandFlow"
    MasterPageFile="~/DefaultMaster.Master" Title="银行流水" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">


    <link href="../Styles/bootstrap.min.css" rel="stylesheet" />
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">银行流水管理
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">文件上传 ：
            </td>
            <td height="25" width="*" align="left">
                <div id="uploader" class="wu-example">
                    <!--用来存放文件信息-->
                    <div id="thelist" class="uploader-list"></div>
                    <div class="btns">
                        <div id="picker">选择文件</div>
                        <br />
                        <button id="ctlBtn" class="btn btn-default" type="button">开始上传</button>
                    </div>
                </div>

                
            </td>
        </tr>

        
    </table>


    <script src="../Scripts/jquery-1.10.2/jquery.min.js"></script>
   
    <!--引入CSS-->
    <link href="../Scripts/webuploader-0.1.5/webuploader.css" rel="stylesheet" type="text/css" />
    <!--引入JS-->
    <script src="../Scripts/webuploader-0.1.5/webuploader.js" type="text/javascript"></script>
     <script src="../Scripts/webuploader-0.1.5/getting-started.js"></script>
 
</asp:Content>
