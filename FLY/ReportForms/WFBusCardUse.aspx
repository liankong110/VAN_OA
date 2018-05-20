<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFBusCardUse.aspx.cs" Inherits="VAN_OA.ReportForms.WFBusCardUse" MasterPageFile="~/DefaultMaster.Master" Title="公交卡使用" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">公交卡使用-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>

        <tr>
            <td height="25" width="30%" align="right">公交卡号
	：</td>
            <td height="25" width="*" align="left">

                <asp:DropDownList ID="ddlCardNo" runat="server" Width="300px" DataTextField="CardNo" DataValueField="CardNo">
                </asp:DropDownList>
            </td>
        </tr>
          <tr>
            <td height="25" width="30%" align="right">使用人
	：</td>
            <td height="25" width="*" align="left">

                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="LoginName">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">使用日期
	：</td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBusCardDate" runat="server" Width="300px"></asp:TextBox><asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" /><cc1:CalendarExtender
                    ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtBusCardDate" PopupButtonID="Image1">
                </cc1:CalendarExtender><label style="color:red;">*</label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">地点
	：</td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtAddress" runat="server" Width="300px"></asp:TextBox><label style="color:red;">*</label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">客户人员
	：</td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGuestName" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">金额
	：</td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUseTotal" runat="server" Width="300px"></asp:TextBox><label style="color:red;">*</label>
            </td>
        </tr>

        <tr>
            <td width="30%" align="right">项目编号:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPONo" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="lbtnSelectPONo" runat="server"
                    OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOList.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
       选择</asp:LinkButton><label style="color:red;">*</label>
            </td>
        </tr>

        <tr>
            <td width="30%" align="right">项目名称:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" Width="300px" ReadOnly="true"></asp:TextBox><label style="color:red;">*</label>
            </td>
        </tr>

        <tr>
            <td width="30%" align="right">客户名称：</td>
            <td>
                <asp:TextBox ID="txtPOGuestName" runat="server" Width="300px" ReadOnly="true"></asp:TextBox><label style="color:red;">*</label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">备注
	：</td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBusUseRemark" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
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
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="本次审批意见:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="Button1" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />
                &nbsp;

                <asp:Button ID="Button2" runat="server" Text=" 返回 "  BackColor="Yellow" OnClientClick="go();return false;" />
                <br />  
            </td>
        </tr>
        <%--  <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow"
                    OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " BackColor="Yellow"
                    OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow"
                    OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>--%>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <SCRIPT language="javascript">
function go()
{
window.history.go(-1);
}
 
</SCRIPT>

</asp:Content>
