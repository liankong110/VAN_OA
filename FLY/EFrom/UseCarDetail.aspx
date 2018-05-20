<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UseCarDetail.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.UseCarDetail" MasterPageFile="~/DefaultMaster.Master"
    Title="用车明细表" %>

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
                用车明细表-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                申请人：
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox><font style="color: Red">*</font>
                    
                
            </td>
            <td align="right">
             申请日期：
            </td>
             <td>
             <asp:TextBox ID="txtDateTime" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                使用时间：
            </td>
            <td>
               <asp:TextBox ID="txtUseDate" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1"
                    runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <font style="color: Red">*</font>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd" TargetControlID="txtUseDate"
                    PopupButtonID="Image1">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                项目编号:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPONo" runat="server" Width="200px" ReadOnly="true" 
                    AutoPostBack="True" ontextchanged="txtPONo_TextChanged"></asp:TextBox>
                <asp:RadioButton ID="rdoSelect" runat="server" Text="选择" GroupName="N"  Checked="true" AutoPostBack="true"
                    oncheckedchanged="rdoSelect_CheckedChanged"  />
                <asp:RadioButton ID="rdoPhone" runat="server" Text="手机"  GroupName="N" AutoPostBack="true"
                    oncheckedchanged="rdoPhone_CheckedChanged" />

                <asp:LinkButton ID="lbtnSelectPONo" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOList.aspx?CG_Order=true',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
        选择</asp:LinkButton>
            </td>
            <td>
                项目名称:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                客户名称：
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtGuestName" runat="server" Width="95%" ReadOnly="true"></asp:TextBox><font
                    style="color: Red">*</font>
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                所属区域：
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtdeArea" runat="server" Width="95%"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
            <td>
                乘车人：
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtpers_car" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td rowspan="2">
                起始地点
            </td>
            <td>
                出发地：
            </td>
            <td>
                <asp:TextBox ID="txtgoAddress" runat="server" Width="250px"></asp:TextBox>
                <font style="color: Red">*</font>
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
                到达地：
            </td>
            <td>
                <asp:TextBox ID="txttoAddress" runat="server" Width="250px"></asp:TextBox>
                <font style="color: Red">*</font>
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
                启程里程数：
            </td>
            <td>
                <asp:TextBox ID="txtFromRoadLong" runat="server"></asp:TextBox>
            </td>
            <td colspan="2">
                回来里程数： &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtToRoadLong" runat="server"></asp:TextBox>
            </td>
            <td>
                实际里程数：
            </td>
            <td>
                <asp:TextBox ID="txtroadLong" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                车牌号：
            </td>
            <td colspan="2">
                <%--  <asp:TextBox ID="txtCarNo" runat="server" Width="95%"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlCarNo" runat="server" DataTextField="CarNO"
                                DataValueField="CarNO">
                </asp:DropDownList>
            </td>
            <td>
                司机：
            </td>
            <td colspan="2">
             <%--   <asp:TextBox ID="txtDriver" runat="server" Width="95%"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="LoginName" Width="200PX">
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>
                备注:
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="95%" Height="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Label ID="lblDoMess" runat="server" Text="钥匙发放人尚未确认!!!" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblDoPerDesc" runat="server" Text="钥匙发放确认人："></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDoPer" runat="server" Text="lblDoPer"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDoTimeDesc" runat="server" Text="确认时间："></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDoTime" runat="server" Text="lblDoTime"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="5">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
            </td>
            <td colspan="5">
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
            <td colspan="5">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnOtherSub" runat="server" Text="钥匙发放确认提交" BackColor="Yellow" Width="120px"
                    OnClick="btnOtherSub_Click" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnOtherClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
            
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnEdit" runat="server" Text="修改->保存" BackColor="Yellow" OnClick="btnEdit_Click" />&nbsp;
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
