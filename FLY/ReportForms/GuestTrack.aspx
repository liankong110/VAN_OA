<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="GuestTrack.aspx.cs"
    Inherits="VAN_OA.ReportForms.GuestTrack" MasterPageFile="~/DefaultMaster.Master"
    Title="�ͻ���ϵ����" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
   <script type="text/javascript">
       function setColor(_parent) {
           //�ͻ����� ����������û������ð׵׺��֣��������޸ģ��������ҵ�û�,���ú�׺��֣��������޸�
           if (_parent.value == "��ҵ�û�") {
               _parent.style.backgroundColor = 'red';

           }
           else {
               _parent.style.backgroundColor = '';
           }
          


       }
   </script>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                �ͻ���ϵ���ٱ�-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ������ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblCreateUser" runat="server" Text=""></asp:Label>
            </td>
            <td height="25" width="30%" align="right">
                ����ʱ�� ��
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
                <asp:TextBox ID="txtGuestHttp" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:CheckBox ID="cbIsSpecial" runat="server" Text="����" ForeColor="Red" />&nbsp;
                <font style="color: Red">*</font>�ͻ����� ��
            </td >
            <td height="25" width="*" align="left" colspan="3">
                <asp:TextBox ID="txtGuestName" runat="server" Width="300px"></asp:TextBox>
            </td>

             </tr>
              <tr>
               <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>�ͻ���� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSimpGuestName" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>�ͻ�˰��ǼǺ� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestShui" runat="server" Width="300px"></asp:TextBox>
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
                <font style="color: Red">*</font>�ͻ�����ע��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestGong" runat="server" Width="300px"></asp:TextBox>
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
                <asp:TextBox ID="txtGuestBrandNo" runat="server" Width="300px"></asp:TextBox>
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
                <asp:TextBox ID="txtGuestBrandName" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFoxOrEmail" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                AE ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlAE" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAEPre" runat="server" Width="75PX">
                </asp:DropDownList>
                %
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �Ƿ������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:CheckBox ID="chkIfSave" Text="�Ƿ�������" runat="server" Checked="False" />
            </td>
            <td height="25" width="30%" align="right">
                INSIDE ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlINSIDE" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlINSIDEPre" runat="server" Width="75PX">
                </asp:DropDownList>
                %
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                QQ/MSN��ϵ ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtQQMsn" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                �ϼ������۶� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestTotal" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �ͻ�ID ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestId" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                �ϼ������� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestLiRun" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
               <font style="color: Red">*</font> ��ϵ�˵�ַ �� 
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestAddress" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                �ϼ����տ���/�� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestDays" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>�ͻ����� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType" onchange="setColor(this);"
                    DataTextField="GuestType">
                </asp:DropDownList>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>�ͻ����� ��
            </td>
            <td colspan="1" align="right">
                <asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro" DataTextField="GuestProString" style="left:0px;">
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                ��Ӧ����<asp:TextBox ID="txtYear" runat="server" Width="50px" ReadOnly="true"></asp:TextBox>��<asp:TextBox
                    ID="txtMouth" runat="server" Width="50px" ReadOnly="true"></asp:TextBox>
                ����
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ע ��
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="800px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                INSIDE��ע ��
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:TextBox ID="txtINSIDERemark" runat="server" Width="800px"></asp:TextBox>
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
