<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseCmd.aspx.cs" Inherits="VAN_OA.DatabaseCmd"  MasterPageFile="~/DefaultMaster.Master" Title="资料库设置"%>
<asp:Content ContentPlaceHolderID="SampleContent"  runat="server">

   <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">资料库设置</td>
        </tr>
        <tr>
            <td>文件名称：</td>
            <td><asp:TextBox ID="txtFileName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>文件路径：</td>
            <td><asp:TextBox ID="txtUrl" runat="server" Width="582px" ></asp:TextBox></td>
        </tr>
         
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" 
                    onclick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 "  BackColor="Yellow" 
                    onclick="btnUpdate_Click"/>&nbsp;
                <asp:Button ID="btnSet" runat="server" Text=" 重置 "  BackColor="Yellow" 
                    onclick="btnSet_Click"/>&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
 