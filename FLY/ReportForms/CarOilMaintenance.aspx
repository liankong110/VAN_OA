<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="CarOilMaintenance.aspx.cs"
    Inherits="VAN_OA.ReportForms.CarOilMaintenance" MasterPageFile="~/DefaultMaster.Master"
    Title="����������¼��" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                �������ͼ�¼��
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px; background-color: #D7E8FF;">
                ���ƺ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlCarNo" runat="server" Width="300px" DataTextField="CarNO"
                    DataValueField="CarNO" AutoPostBack="True" OnSelectedIndexChanged="ddlCarNo_SelectedIndexChanged">
                </asp:DropDownList>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px; background-color: #D7E8FF;">
                ����ʱ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtMaintenanceTime" runat="server" Width="300px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <font style="color: Red">*</font>
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtMaintenanceTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 20px; background-color: #D7E8FF;">
                ������
            </td>
            <td>
                <asp:TextBox ID="txtUpTotal" runat="server" Width="300px" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 20px; background-color: #D7E8FF;">
                ���ͣ�Ԫ����
            </td>
            <td>
                <asp:TextBox ID="txtOilTotal" runat="server" Width="300px" AutoPostBack="True" OnTextChanged="txtOilTotal_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px; background-color: #D7E8FF;">
                ��ֵʱ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtChongZhiTime" runat="server" Width="300px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtChongZhiTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 20px; background-color: #D7E8FF;">
                ��ֵ��Ԫ����
            </td>
            <td>
                <asp:TextBox ID="txtAddTotal" runat="server" Width="300px" AutoPostBack="True" OnTextChanged="txtOilTotal_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 20px; background-color: #D7E8FF;">
                ������
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" Width="300px" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px; background-color: #D7E8FF;">
                ��ע ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtRemark" runat="server" Width="800px" TextMode="MultiLine" Height="70px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" ��� " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" �޸� " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
