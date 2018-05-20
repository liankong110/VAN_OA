<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveTask.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.LeaveTask" MasterPageFile="~/DefaultMaster.Master"
    Title="加 班 审 批 单" %>

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
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                工作事务型请假单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                姓名：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                部门：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtDepartName" runat="server"></asp:TextBox>
            </td>
            <td>
                职务：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtZhiWu" runat="server"></asp:TextBox>
            </td>
              <td>
                申请日期：<font style="color: Red">*</font>
            </td>
            <td >
                <asp:TextBox ID="txtAppDate" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                请假类别：
            </td>
            <td colspan="7">
                <asp:Panel ID="Panel1" runat="server">
                    <asp:RadioButton ID="rdo1" runat="server" Text="开会请假" GroupName="a" />
                    <asp:RadioButton ID="rdo2" runat="server" Text="培训请假" GroupName="a" />
                    <asp:RadioButton ID="rdo3" runat="server" Text="公共卫生请假" GroupName="a" />
                    <asp:RadioButton ID="rdo4" runat="server" Text="旅游请假" GroupName="a" />
                    <asp:RadioButton ID="rdo5" runat="server" Text="其它" GroupName="a" />
                </asp:Panel>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                请假日期： <font style="color: Red">*</font>
            </td>
            <td colspan="7">
                从
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
                共
                <asp:Label ID="lblTiemSpan" runat="server" Text="0小时"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                备注：
            </td>
            <td colspan="7">
                <asp:TextBox ID="txtRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="8">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
            </td>
            <td colspan="8">
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
            <td colspan="8">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="8" align="center">
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
