<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="GuestTrack.aspx.cs"
    Inherits="VAN_OA.ReportForms.GuestTrack" MasterPageFile="~/DefaultMaster.Master"
    Title="客户联系跟踪" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
   <script type="text/javascript">
       function setColor(_parent) {
           //客户类型 如果是政府用户，采用白底黑字，可下拉修改；如果是企业用户,采用红底黑字，可下拉修改
           if (_parent.value == "企业用户") {
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
                客户联系跟踪表-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                申请人 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblCreateUser" runat="server" Text=""></asp:Label>
            </td>
            <td height="25" width="30%" align="right">
                申请时间 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblCreateTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>登记日期 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtTime" runat="server" Width="270px" onfocus="setday(this)"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    PopupButtonID="Image1" TargetControlID="txtTime">
                </cc1:CalendarExtender>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>联系人网址 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestHttp" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:CheckBox ID="cbIsSpecial" runat="server" Text="特殊" ForeColor="Red" />&nbsp;
                <font style="color: Red">*</font>客户名称 ：
            </td >
            <td height="25" width="*" align="left" colspan="3">
                <asp:TextBox ID="txtGuestName" runat="server" Width="300px"></asp:TextBox>
            </td>

             </tr>
              <tr>
               <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>客户简称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSimpGuestName" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>客户税务登记号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestShui" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>电话/手机 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPhone" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>客户工商注册号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestGong" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>联系人 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtLikeMan" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>银行账号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestBrandNo" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>职务 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtJob" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>开户行 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestBrandName" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                传真或邮箱 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFoxOrEmail" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                AE ：
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
                是否留资料 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:CheckBox ID="chkIfSave" Text="是否留资料" runat="server" Checked="False" />
            </td>
            <td height="25" width="30%" align="right">
                INSIDE ：
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
                QQ/MSN联系 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtQQMsn" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                上季度销售额 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestTotal" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                客户ID ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestId" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                上季度利润 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestLiRun" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
               <font style="color: Red">*</font> 联系人地址 ： 
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestAddress" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                上季度收款期/天 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestDays" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>客户类型 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType" onchange="setColor(this);"
                    DataTextField="GuestType">
                </asp:DropDownList>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>客户属性 ：
            </td>
            <td colspan="1" align="right">
                <asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro" DataTextField="GuestProString" style="left:0px;">
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                对应日期<asp:TextBox ID="txtYear" runat="server" Width="50px" ReadOnly="true"></asp:TextBox>年<asp:TextBox
                    ID="txtMouth" runat="server" Width="50px" ReadOnly="true"></asp:TextBox>
                季度
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                备注 ：
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="800px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                INSIDE备注 ：
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:TextBox ID="txtINSIDERemark" runat="server" Width="800px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人："></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果："></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserName">
                    <asp:ListItem Selected="True">通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:Label ID="lblYiJian" runat="server" Text="本次审批意见："></asp:Label>
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click"
                    Visible="false" />&nbsp;
                <asp:Button ID="btnCopy" runat="server" Text=" 复制 " Visible="false" BackColor="Yellow"
                    OnClick="btnCopy_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " Visible="false" BackColor="Yellow"
                    OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " Visible="false" BackColor="Yellow"
                    OnClick="btnClose_Click" />&nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="Button1" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
