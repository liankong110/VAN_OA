<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="SalaryInfo.aspx.cs"
    Inherits="VAN_OA.HR.SalaryInfo" MasterPageFile="~/DefaultMaster.Master" Title="人员档案" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                人员档案——
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                基本工资 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBasicSalary" runat="server" Width="200px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                工龄工资 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGonglin" runat="server" Width="200px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                通讯费 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtMobileFee" runat="server" Width="200px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                职务津贴 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPositionFee" runat="server" Width="200px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                工会费 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUnionFee" runat="server" Width="200px">0</asp:TextBox>&nbsp;</td>
            <td height="25" width="20%" align="right">
                养老金 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtYangLaoJin" runat="server" Width="200px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                默认工作天数 ：</td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtDefaultWorkDays" runat="server" Width="200px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                &nbsp;</td>
            <td height="25" width="*" align="left">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                退休返聘：</td>
            <td height="25" width="*" align="left">
                <asp:CheckBox ID="ChkRetail" runat="server" />
            </td>
            <td height="25" width="20%" align="right">
                离职人员：</td>
            <td height="25" width="*" align="left">
                <asp:CheckBox ID="ChkQuit" runat="server" />
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                修改人 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUpdatePerson" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                修改时间 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUpdateTime" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                &nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="保存" BackColor="Yellow" 
                    OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
    备注：1.工资合计=基本工资+岗位工资+职级津贴+全勤奖+通讯费
   2. 应得工资=工资合计/默认天数*出勤天数+特殊奖励+工龄工资
    3.实发工资= 应得工资-扣款-工会费-养老金

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>

