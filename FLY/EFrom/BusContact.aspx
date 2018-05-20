<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusContact.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.BusContact" MasterPageFile="~/DefaultMaster.Master"
    Title="外出业务联系单" %>

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
                外出业务联系单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
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
                申请日期：<font style="color: Red">*</font>
            </td>
            <td >
                <asp:TextBox ID="txtAppDate" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                日期：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtDateTime" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtDateTime">
                </cc1:CalendarExtender>
            </td>
            <td>
                外出时间：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtGotime" runat="server"></asp:TextBox>
            </td>
            <td>
                返回时间：
            </td>
            <td>
                <asp:TextBox ID="txtBackTime" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                外出联系单位：<font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtContactUnit" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                联系人姓名： <font style="color: Red">*</font>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtContacer" runat="server" Width="95%"></asp:TextBox>
            </td>
            <td>
                电话号码： <font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtTel" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <div align="left" style="width: 100%;">
                    <asp:LinkButton ID="lblAttName" runat="server" OnClick="lblAttName_Click" ForeColor="Red"></asp:LinkButton>
                    <asp:Label ID="lblAttName_Vis" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <br />
                <asp:FileUpload ID="fuAttach" runat="server" Visible="false" Width="400px" />
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
                <asp:Button ID="btnFinSub" runat="server" Text="提交 完成申请" BackColor="Yellow" OnClick="btnFinSub_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
