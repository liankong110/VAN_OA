<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFUpdateGuestName.aspx.cs"
    MasterPageFile="~/DefaultMaster.Master" Title="客户名称更新" Inherits="VAN_OA.JXC.WFUpdateGuestName" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td style="height: 20px; background-color: #336699; color: White;" colspan="4">
                客户名称更新<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
         <tr>
            <td>
                原有客户名称：
            </td>
            <td>
                <asp:TextBox ID="txtOldName" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
            <td>
                更新为客户名称:
            </td>
               <td>
                <asp:TextBox ID="txtNewName" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
            </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server"  OnClientClick="return confirm('确定要提交吗？')" Text=" 更新  " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
