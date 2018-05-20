<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCompanyInfo.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFCompanyInfo" MasterPageFile="~/DefaultMaster.Master"
    Title="��˾��Ϣ" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                ��˾��Ϣ
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ˮ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComId" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��˾���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComCode" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��˾���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��˾��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComSimpName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtOrderByIndex" runat="server" Width="200px" Text="0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ס�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtZhuSuo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtLeiXing" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �绰 ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtDianHua" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtChuanZhen" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���ô��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtXinYongCode" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFaRen" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ע���ʱ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtZhuCeZiBen" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCreateTime" runat="server" Width="200px"></asp:TextBox>
                  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtCreateTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��Ӫ������ʼ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtStartTime" runat="server" Width="200px" ></asp:TextBox>
                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtStartTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��Ӫ���޽��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtEndTime" runat="server" Width="200px" ></asp:TextBox>
                   <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtEndTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��Ӫ��Χ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFanWei" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ������ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtKaiHuHang" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �ʺ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtKaHao" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��˾��ַ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComUrl" runat="server" Width="200px"></asp:TextBox>
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
