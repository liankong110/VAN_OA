<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OverTime.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.OverTime" MasterPageFile="~/DefaultMaster.Master"
    Title="�� �� �� �� ��" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

    <script type="text/javascript">
function show()
{   alert("1");
    document.getElementById("btnSub").disabled=false;
    
}
    </script>

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                �� �� �� �� ��-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                ������<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                ���ţ�<font style="color: Red">*</font>
            </td>
            <td >
                <asp:TextBox ID="txtDepartName" runat="server"></asp:TextBox>
                <asp:DropDownList ID="ddlOverTimeType" runat="server" Width="150px" Visible="false">
                    <asp:ListItem>�Ӱ�</asp:ListItem>
                </asp:DropDownList>
            </td>
             <td>
                 �������ڣ�<font style="color: Red">*</font>
            </td>
            <td >
                <asp:TextBox ID="txtAppDate" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ���:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="lbtnSelectPONo" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOList.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
                ѡ��</asp:LinkButton>
            </td>
            <td>
                ��Ŀ����:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                �ͻ����ƣ�
            </td>
            <td>
                <asp:TextBox ID="txtPOGuestName" runat="server" ReadOnly="true" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �Ӱ����ɣ� <font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtreason" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ��ַ��
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtAddress" runat="server" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �Ӱ����ڣ� <font style="color: Red">*</font>
            </td>
            <td colspan="5">
                ��
                <asp:TextBox ID="txtForm" runat="server" AutoPostBack="True" OnTextChanged="txtForm_TextChanged"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    TargetControlID="txtForm" PopupButtonID="Image1">
                </cc1:CalendarExtender>
                -<asp:TextBox ID="txtTo" runat="server" AutoPostBack="True" OnTextChanged="txtTo_TextChanged"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    TargetControlID="txtTo" PopupButtonID="ImageButton1">
                </cc1:CalendarExtender>
                ��
                <asp:Label ID="lblTiemSpan" runat="server" Text="0Сʱ"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblGuestName" runat="server" Text="�ͻ�����:"></asp:Label>
            </td>
            <td colspan="5">
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="LoginName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="���:"></asp:Label>
                <font style="color: Red">*</font></td>
            <td colspan="5">
                <asp:TextBox ID="txtTotal" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
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
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnSub" runat="server" Text="�ύ" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnClose_Click" />
                &nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
