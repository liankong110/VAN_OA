<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDeleteCmd.aspx.cs" Inherits="VAN_OA.AdminDeleteCmd"  MasterPageFile="~/DefaultMaster.Master" Title="删除全选管理"%>
<asp:Content ContentPlaceHolderID="SampleContent" runat="server">
<table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1" >
         <tr>
            <td colspan="2" style=" height:20px; background-color:#336699; color:White;">角色信息</td>
        </tr>
        <tr>
            <td>用户名称：</td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id" Width="200PX">
                </asp:DropDownList>                
            </td>
        </tr>
        
         
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" 
                    onclick="btnAdd_Click" />&nbsp;
               
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 "  BackColor="Yellow" 
                    onclick="btnClose_Click"/>&nbsp;
            </td>
        </tr>
    </table>

</asp:Content>

 
 