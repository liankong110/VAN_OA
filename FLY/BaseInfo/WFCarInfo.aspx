<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCarInfo.aspx.cs" Inherits="VAN_OA.BaseInfo.WFCarInfo"
    MasterPageFile="~/DefaultMaster.Master" Title="商品档案" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                车辆信息
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                车牌号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCarNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                品牌型号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCarModel" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                发动机号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCarEngine" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td height="25" width="30%" align="right">
                车辆识别代号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCarShiBieNO" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td height="25" width="30%" align="right">
                车架号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCarJiaNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td height="25" width="30%" align="right">
                行驶证号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCaiXingShiNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td height="25" width="30%" align="right">
                年检时间 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtNianJian" runat="server" Width="200px"></asp:TextBox>
                 <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                   <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtNianJian">
                </cc1:CalendarExtender>
            </td>
        </tr>
         <tr>
            <td height="25" width="30%" align="right">
                保险时间 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBaoxian" runat="server" Width="200px"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                   <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtBaoxian">
                </cc1:CalendarExtender>
            </td>
        </tr>
            <tr>
            <td height="25" width="30%" align="right">
                油耗系数 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtOilNumber" runat="server" Width="200px" Text="0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
