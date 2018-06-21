<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="PaymentInfo.aspx.cs"
    Inherits="VAN_OA.HR.PaymentInfo" MasterPageFile="~/DefaultMaster.Master" Title="��Ա����" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                ���ʼ��㡪��
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                ����
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:DropDownList ID="ddlYear" runat="server" Height="16px">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem Value="">--ѡ��--</asp:ListItem>
                    <asp:ListItem Value="01">01</asp:ListItem>
                    <asp:ListItem Value="02">02</asp:ListItem>
                    <asp:ListItem Value="03">03</asp:ListItem>
                    <asp:ListItem Value="04">04</asp:ListItem>
                    <asp:ListItem Value="05">05</asp:ListItem>
                    <asp:ListItem Value="06">06</asp:ListItem>
                    <asp:ListItem Value="07">07</asp:ListItem>
                    <asp:ListItem Value="08">08</asp:ListItem>
                    <asp:ListItem Value="09">09</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnRead" runat="server" Text="��ȡ��Ϣ" BackColor="Yellow" OnClick="btnRead_Click" />
                &nbsp;<asp:Button ID="btnNew" runat="server" Text="�½�" BackColor="Yellow" 
                    OnClick="btnNew_Click" />
                <asp:CheckBox ID="ChkRetailed" runat="server" Visible="False" />
                <asp:CheckBox ID="ChkQuit" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                ��ְ����</td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblOnBoardTime" runat="server"></asp:Label>
            </td>
            <td height="25" width="20%" align="right">
                ��ְ����</td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblQuitTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                �������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBasicSalary" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                ȫ�ڽ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFullAttendence" runat="server" Width="100px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                ͨѶ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtMobileFee" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                ���⽱�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSpecialAward" runat="server" Width="100px">0</asp:TextBox>
                <asp:TextBox ID="txtSpecialAwardNote" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                ���乤�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGonglin" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                ��λ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPositionPerformance" runat="server" Width="100px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                ְ������ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPositionFee" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                ������Ч ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtWorkPerformance" runat="server" Width="100px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                ���ʺϼ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblFullPayment" runat="server" Text="0"></asp:Label>
            </td>
            <td height="25" width="20%" align="right">
                �������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtWorkDays" runat="server" Width="50px">0</asp:TextBox>
                Ĭ������ ��<asp:Label ID="lblDefaultWorkDays" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                Ӧ�ù��� ��</td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblShouldPayment" runat="server" Text="0"></asp:Label>
            </td>
            <td height="25" width="20%" align="right">
                &nbsp;</td>
            <td height="25" width="*" align="left">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                ����� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUnionFee" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                �ۿ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtDeduction" runat="server" Width="100px">0</asp:TextBox>
                <asp:TextBox ID="txtDeductionNote" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                ���Ͻ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtYangLaoJin" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                ʵ������ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblActualPayment" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                �޸��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUpdatePerson" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                �޸�ʱ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUpdateTime" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                &nbsp;<asp:Button ID="btnCalc" runat="server" Text="����" BackColor="Yellow" 
                    OnClick="btnCalc_Click" />&nbsp;&nbsp;<asp:Button
                    ID="btnSave" runat="server" Text="����" BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;

            

            </td>
        </tr>
    </table>
       
                <div> ��ע��1.���ʺϼ�=��������+��λ����+ְ������+ȫ�ڽ�+ͨѶ��+������Ч</div>
   <div>2. Ӧ�ù���=���ʺϼ�/Ĭ������*��������+���⽱��+���乤��</div>
   <div> 3.ʵ������= Ӧ�ù���-�ۿ�-�����-���Ͻ�</div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
