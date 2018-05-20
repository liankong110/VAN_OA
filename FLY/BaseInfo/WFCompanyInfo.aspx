<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCompanyInfo.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFCompanyInfo" MasterPageFile="~/DefaultMaster.Master"
    Title="公司信息" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                公司信息
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                流水号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComId" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                公司代码 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComCode" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                公司名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                公司简称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComSimpName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                排序 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtOrderByIndex" runat="server" Width="200px" Text="0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                住所 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtZhuSuo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                类型 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtLeiXing" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                电话 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtDianHua" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                传真 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtChuanZhen" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                信用代码 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtXinYongCode" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                法人 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFaRen" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                注册资本 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtZhuCeZiBen" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                成立日期 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCreateTime" runat="server" Width="200px"></asp:TextBox>
                  <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtCreateTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                经营期限起始 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtStartTime" runat="server" Width="200px" ></asp:TextBox>
                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtStartTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                经营期限结束 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtEndTime" runat="server" Width="200px" ></asp:TextBox>
                   <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtEndTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                经营范围 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFanWei" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                开户行 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtKaiHuHang" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                帐号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtKaHao" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                对外邮箱 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                公司网址 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtComUrl" runat="server" Width="200px"></asp:TextBox>
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
