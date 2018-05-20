<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UseCar.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.UseCar" MasterPageFile="~/DefaultMaster.Master"
    Title="私车公用申请单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

    <script type="text/javascript">
function show()
{   alert("1");
    document.getElementById("btnSub").disabled=false;

}

function GetTotal() {


    var sl = document.getElementById('<%= txtroadLong.ClientID %>').value;
    
    var dj = document.getElementById('<%= txtOiLXiShu.ClientID %>').value;
    if ((sl != "") && (dj != "")) {

        var total = sl * dj;       
        document.getElementById('<%= lblTotal.ClientID %>').innerHTML = total.toFixed(3).toString();
    }
    else {
        document.getElementById('<%= lblTotal.ClientID %>').innerHTML = "0.00";


    }
}

    </script>

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                私车公用申请单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                申请人：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                申请日期：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtDateTime" runat="server" ReadOnly="true"></asp:TextBox>
              <%--  <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="Image1"
                    Format="yyyy-MM-dd" TargetControlID="txtDateTime">
                </cc1:CalendarExtender>--%>
            </td>
            <td>
                乘车人：
            </td>
            <td>
                <asp:TextBox ID="txtpers_car" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                项目编号:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="lbtnSelectPONo" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOList.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
        选择</asp:LinkButton>
            </td>
            <td>
                项目名称:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                客户名称：
            </td>
            <td>
                <asp:TextBox ID="txtPOGuestName" runat="server" ReadOnly="true" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="rdoDan" runat="server" Text="单程" GroupName="a" />
                <asp:RadioButton ID="rdoWang" runat="server" Text="往返" GroupName="a" />
            </td>
            <td>
                实际里程数：
            </td>
            <td>
                <asp:TextBox ID="txtroadLong" runat="server" onKeyUp="GetTotal();"></asp:TextBox>
            </td>
            <td colspan="3">
                油价系数：
                <asp:TextBox ID="txtOiLXiShu" runat="server" onKeyUp="GetTotal();"></asp:TextBox>
                小计：
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                使用事由： <font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtuseReason" runat="server" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                详细地址： <font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtdeAddress" runat="server" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td rowspan="2">
                起始地点
            </td>
            <td>
                出发地： <font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtgoAddress" runat="server"></asp:TextBox>
            </td>
            <td rowspan="2">
                起始时间
            </td>
            <td>
                出发时间：
            </td>
            <td>
                <asp:TextBox ID="txtgoTime" runat="server"></asp:TextBox>
                <asp:ImageButton ID="imggoTime" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    TargetControlID="txtgoTime" PopupButtonID="imggoTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                到达地： <font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txttoAddress" runat="server"></asp:TextBox>
            </td>
            <td>
                结束时间：
            </td>
            <td>
                <asp:TextBox ID="txtendTime" runat="server"></asp:TextBox>
                <asp:ImageButton ID="imgendTime" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    TargetControlID="txtendTime" PopupButtonID="imgendTime">
                </cc1:CalendarExtender>
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
                <asp:Button ID="btnFinSub" runat="server" Text="修改保存 回来时间" BackColor="Yellow" OnClick="btnFinSub_Click" />
                &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnEdit" runat="server" Text="修改->保存" BackColor="Yellow" OnClick="btnEdit_Click" />
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
