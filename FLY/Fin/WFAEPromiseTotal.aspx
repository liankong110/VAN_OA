<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFAEPromiseTotal.aspx.cs"
    Inherits="VAN_OA.Fin.WFAEPromiseTotal" MasterPageFile="~/DefaultMaster.Master"
    Title="销售指标" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">销售指标
            </td>
        </tr>

        <tr>
            <td height="25" width="30%" align="right">年份 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">AE ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="LoginName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td height="25" width="30%" align="right">承诺销售额指标 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPromiseSellTotal" runat="server"></asp:TextBox>
            </td>
        </tr>
          <tr>
            <td height="25" width="30%" align="right">承诺利润 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPromiseProfit" runat="server"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td height="25" width="30%" align="right">新客户承诺销售额 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtAddGuetSellTotal" runat="server"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td height="25" width="30%" align="right">新客户承诺利润 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtAddGuestProfit" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
