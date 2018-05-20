<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFTeamInfo.aspx.cs" Inherits="VAN_OA.BaseInfo.WFTeamInfo"
    MasterPageFile="~/DefaultMaster.Master" Title="��ͬ��������" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <style>
        table
        {
            border-collapse: collapse;
            border-spacing: 0;
            border-left: 1px solid #888;
            border-top: 1px solid #888;
        }
        th, td
        {
            border-right: 1px solid #888;
            border-bottom: 1px solid #888;
            padding: 1px 1px;
        }
        th
        {
            font-weight: bold;
        }
         span{ color:Red;}
    </style>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                ���̶ӳ�ά��
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ����<span>*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtTeamLever" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �Ա�<span>*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlSex" runat="server">
                    <asp:ListItem Text="��" Value="��"></asp:ListItem>
                    <asp:ListItem Value="Ů" Text="Ů"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���֤�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCardNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��������<span>*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlBrithdayYear" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlContract_Month" runat="server">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ������ʼʱ��<span>*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContractStartTime" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtContractStartTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��Ա��ģ<span>*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtTeamPersonCount" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td height="25" width="30%" align="right">
                �绰 ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox>
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
