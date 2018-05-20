<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="Person.aspx.cs"
    Inherits="VAN_OA.HR.Person" MasterPageFile="~/DefaultMaster.Master" Title="人员档案" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                人员档案
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                用户匹配</td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
                <asp:Button ID="btnSelect" runat="server" Text="匹配" BackColor="Yellow" OnClick="btnSelect_Click" />
                <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                编号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCode" runat="server" Width="200px"></asp:TextBox><font style="color: Red">*</font>
            </td>
            <td height="25" width="20%" align="right">
                约定转正 日期 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBeNormalTime" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                部门 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtDepartment" runat="server" Width="200px"></asp:TextBox><font
                    style="color: Red">*</font>
            </td>
            <td height="25" width="20%" align="right">
                签订合同 日期 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContractTime" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                岗位 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPosition" runat="server" Width="200px"></asp:TextBox><font style="color: Red">*</font>
            </td>
            <td height="25" width="20%" align="right">
                户口所在地 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtHuKou" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                姓名 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox><font style="color: Red">*</font>
            </td>
            <td height="25" width="20%" align="right">
                婚姻状况 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlMarriage" runat="server" Width="208PX">
                    <asp:ListItem>已婚</asp:ListItem>
                    <asp:ListItem>未婚</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                出生年月日 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBirthday" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
            <td height="25" width="20%" align="right">
                身份证号码 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtIDCard" runat="server" Width="200px"></asp:TextBox><font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                性别 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlSex" runat="server" Width="208PX">
                    <asp:ListItem>男</asp:ListItem>
                    <asp:ListItem>女</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td height="25" width="20%" align="right">
                私人联系方式 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtMobilePhone" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                学历 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtEducationLevel" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                家庭电话 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtHomePhone" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                毕业学校 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtEducationSchool" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                家庭住址：</td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtHomeAddress" runat="server" Width="200px" Height="22px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                专业 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtMajor" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                电子邮件地址：</td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtEmailAddress" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                毕业时间 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtGraduationTime" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                入司日期 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtOnBoardTime" runat="server" Width="200px" 
                    style="height: 22px"></asp:TextBox>
                <font style="color: Red">*</font></td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                离职：
            </td>
            <td height="25" width="*" align="left">
                <asp:CheckBox ID="chkQuit" runat="server" />
            </td>
            <td height="25" width="20%" align="right">
                离职时间：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtQuitTime" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                离职原因：</td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtQuitReason" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                                合同到期日期 ： </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContractCloseTime" runat="server" Width="200px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td height="25" width="20%" align="right">
                创建人 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCreatePerson" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
            <td height="25" width="20%" align="right">
                创建时间 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtCreateTime" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
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
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>

