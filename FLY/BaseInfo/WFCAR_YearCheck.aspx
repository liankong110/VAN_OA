<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCAR_YearCheck.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFCAR_YearCheck" MasterPageFile="~/DefaultMaster.Master"
    Title="��������¼" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                ��������¼
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���ƺ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlCarNo" runat="server">
                    <asp:ListItem Value="��ESO72V" Text="��ESO72V"></asp:ListItem>
                    <asp:ListItem Value="��E5NR36" Text="��E5NR36"></asp:ListItem>
                    <asp:ListItem Value="��E9KP16" Text="��E9KP16"></asp:ListItem>
                    <asp:ListItem Value="��E5BY68" Text="��E5BY68"></asp:ListItem>
                    <asp:ListItem Value="��E2N756" Text="��E2N756"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �����Ч���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtYearDate" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtYearDate">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ע ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtRemark" runat="server" Width="200px" TextMode="MultiLine" Height="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" ��� " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" �޸� " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
