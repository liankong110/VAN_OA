<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="PaymentInfo.aspx.cs"
    Inherits="VAN_OA.HR.PaymentInfo" MasterPageFile="~/DefaultMaster.Master" Title="人员档案" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                工资计算——
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                年月
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:DropDownList ID="ddlYear" runat="server" Height="16px">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem Value="">--选择--</asp:ListItem>
                    <asp:ListItem Value="01">01</asp:ListItem>
                    <asp:ListItem Value="02">02</asp:ListItem>
                    <asp:ListItem Value="03">03</asp:ListItem>
                    <asp:ListItem Value="04">04</asp:ListItem>
                    <asp:ListItem Value="05">05</asp:ListItem>
                    <asp:ListItem Value="06">06</asp:ListItem>
                    <asp:ListItem Value="07">07</asp:ListItem>
                    <asp:ListItem Value="08">08</asp:ListItem>
                    <asp:ListItem Value="09">09</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnRead" runat="server" Text="读取信息" BackColor="Yellow" OnClick="btnRead_Click" />
                &nbsp;<asp:Button ID="btnNew" runat="server" Text="新建" BackColor="Yellow" 
                    OnClick="btnNew_Click" />
                <asp:CheckBox ID="ChkRetailed" runat="server" Visible="False" />
                <asp:CheckBox ID="ChkQuit" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                入职日期</td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblOnBoardTime" runat="server"></asp:Label>
            </td>
            <td height="25" width="20%" align="right">
                离职日期</td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblQuitTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                基本工资 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBasicSalary" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                全勤奖 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFullAttendence" runat="server" Width="100px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                通讯费 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtMobileFee" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                特殊奖励 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSpecialAward" runat="server" Width="100px">0</asp:TextBox>
                <asp:TextBox ID="txtSpecialAwardNote" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                工龄工资 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGonglin" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                岗位考核 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPositionPerformance" runat="server" Width="100px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                职级津贴 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPositionFee" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                工作绩效 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtWorkPerformance" runat="server" Width="100px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                工资合计 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblFullPayment" runat="server" Text="0"></asp:Label>
            </td>
            <td height="25" width="20%" align="right">
                出勤天数 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtWorkDays" runat="server" Width="50px">0</asp:TextBox>
                默认天数 ：<asp:Label ID="lblDefaultWorkDays" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                应得工资 ：</td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblShouldPayment" runat="server" Text="0"></asp:Label>
            </td>
            <td height="25" width="20%" align="right">
                &nbsp;</td>
            <td height="25" width="*" align="left">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                工会费 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUnionFee" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                扣款 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtDeduction" runat="server" Width="100px">0</asp:TextBox>
                <asp:TextBox ID="txtDeductionNote" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                养老金 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtYangLaoJin" runat="server" Width="100px">0</asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                实发工资 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblActualPayment" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                修改人 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUpdatePerson" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                修改时间 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUpdateTime" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                &nbsp;<asp:Button ID="btnCalc" runat="server" Text="计算" BackColor="Yellow" 
                    OnClick="btnCalc_Click" />&nbsp;&nbsp;<asp:Button
                    ID="btnSave" runat="server" Text="保存" BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;

            

            </td>
        </tr>
    </table>
       
                <div> 备注：1.工资合计=基本工资+岗位工资+职级津贴+全勤奖+通讯费+工作绩效</div>
   <div>2. 应得工资=工资合计/默认天数*出勤天数+特殊奖励+工龄工资</div>
   <div> 3.实发工资= 应得工资-扣款-工会费-养老金</div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>
