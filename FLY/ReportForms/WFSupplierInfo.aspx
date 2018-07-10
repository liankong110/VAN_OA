<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="WFSupplierInfo.aspx.cs"
    Inherits="VAN_OA.ReportForms.WFSupplierInfo" MasterPageFile="~/DefaultMaster.Master"
    Title="��Ӧ����ϵ����" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">��Ӧ�������-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">������ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblCreateUser" runat="server" Text=""></asp:Label>
            </td>
            <td height="25" width="30%" align="right">����ʱ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblCreateTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>�Ǽ����� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtTime" runat="server" Width="270px" onfocus="setday(this)"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    PopupButtonID="Image1" TargetControlID="txtTime">
                </cc1:CalendarExtender>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>��ϵ����ַ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierHttp" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:CheckBox ID="cbIsSpecial" runat="server" Text="����" ForeColor="Red" />&nbsp;<font
                    style="color: Red">*</font>��Ӧ������ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierName" runat="server" Width="95%"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>���Ǵ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtZhuJi" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">
                    <asp:CheckBox ID="cbIsUse" runat="server" Text="��ʾ" Checked="true" />
                    *</font>��Ӧ�̼�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierSimpleName" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>��Ӧ��˰��ǼǺ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierShui" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>�绰/�ֻ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPhone" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>��Ӧ�̹���ע��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierGong" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>��ϵ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtLikeMan" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>�����˺� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierBrandNo" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>ְ�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtJob" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>������ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierBrandName" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">��������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFoxOrEmail" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>ʡ�ݳ��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlCity" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">�Ƿ������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:CheckBox ID="chkIfSave" Text="�Ƿ�������" runat="server" Checked="False" />
            </td>
            <td height="25" width="30%" align="right" rowspan="4">��Ӫ��Χ ��
            </td>
            <td height="25" width="*" align="left" rowspan="4">
                <asp:TextBox ID="txtMainRange" runat="server" TextMode="MultiLine" Width="95%" Height="120PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">QQ/MSN��ϵ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtQQMsn" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">��Ӧ��ID ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierId" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">��ϵ�˵�ַ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierAddress" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">��ע ��
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="800px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right"><font style="color: Red">*</font>��Ӧ������ ��
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:DropDownList ID="ddlPeculiarity" runat="server">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                     <asp:ListItem Text="����" Value="����"></asp:ListItem>
                     <asp:ListItem Text="������" Value="������"></asp:ListItem>
                      <asp:ListItem Text="�ܴ���" Value="�ܴ���"></asp:ListItem>
                       <asp:ListItem Text="����" Value="����"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:Label ID="lblPer" runat="server" Text="��һ�������ˣ�"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:Label ID="lblResult" runat="server" Text="�������������"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserName">
                    <asp:ListItem Selected="True">ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:Label ID="lblYiJian" runat="server" Text="�������������"></asp:Label>
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" ��� " BackColor="Yellow" OnClick="btnAdd_Click"
                    Visible="false" />&nbsp;
                <asp:Button ID="btnCopy" runat="server" Text=" ���� " Visible="false" BackColor="Yellow"
                    OnClick="btnCopy_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" �޸� " Visible="false" BackColor="Yellow"
                    OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " Visible="false" BackColor="Yellow"
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
