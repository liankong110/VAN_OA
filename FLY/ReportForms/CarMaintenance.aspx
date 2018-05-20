<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="CarMaintenance.aspx.cs"
    Inherits="VAN_OA.ReportForms.CarMaintenance" MasterPageFile="~/DefaultMaster.Master"
    Title="����������¼��" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                ����������¼��-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
          <td  height="25" align="right" style="height: 20px;">
                �������ڣ�
            </td>
            <td  height="25" width="*" align="left">
                <asp:TextBox ID="txtAppDate" runat="server" ReadOnly="true" Width="300px" ></asp:TextBox><font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px;">
                ���ƺ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlCarNo" runat="server" Width="300px" DataTextField="CarNO"
                    DataValueField="CarNO">
                </asp:DropDownList>
                <font style="color: Red">*</font>
                <asp:Label ID="lblName" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px;">
                ����ʱ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtMaintenanceTime" runat="server" Width="300px"></asp:TextBox><asp:ImageButton
                    ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <font style="color: Red">*</font>
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtMaintenanceTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px;">
                ������ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtDistance" runat="server" Width="300px"></asp:TextBox><font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px;">
                ��� ��
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px;">
                �������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtReplaceRemark" runat="server" Width="300px" TextMode="MultiLine"
                    Height="70px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right"style="height: 20px;">
                ά����� ��
            </td>
            <td height="25" width="*" align="left" >
                <asp:TextBox ID="txtReplaceStatus" runat="server" Width="300px" TextMode="MultiLine"
                    Height="70px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="height: 20px;">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="25" align="right" style="height: 20px;">
                ��ע ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtRemark" runat="server" Width="800px" TextMode="MultiLine" Height="70px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserName">
                    <asp:ListItem Selected="True">ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" ��� " BackColor="Yellow" Visible="false"
                    OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" �޸� " BackColor="Yellow" Visible="false"
                    OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" Visible="false"
                    OnClick="btnClose_Click" />&nbsp;
                <asp:Button ID="btnSub" runat="server" Text="�ύ" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="Button1" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
