<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="WFSupplierInfo.aspx.cs"
    Inherits="VAN_OA.ReportForms.WFSupplierInfo" MasterPageFile="~/DefaultMaster.Master"
    Title="供应商联系跟踪" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">供应商申请表-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">申请人 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblCreateUser" runat="server" Text=""></asp:Label>
            </td>
            <td height="25" width="30%" align="right">申请时间 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblCreateTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>登记日期 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtTime" runat="server" Width="270px" onfocus="setday(this)"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    PopupButtonID="Image1" TargetControlID="txtTime">
                </cc1:CalendarExtender>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>联系人网址 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierHttp" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:CheckBox ID="cbIsSpecial" runat="server" Text="特殊" ForeColor="Red" />&nbsp;<font
                    style="color: Red">*</font>供应商名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierName" runat="server" Width="95%"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>助记词 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtZhuJi" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">
                    <asp:CheckBox ID="cbIsUse" runat="server" Text="显示" Checked="true" />
                    *</font>供应商简称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierSimpleName" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>供应商税务登记号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierShui" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>电话/手机 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPhone" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>供应商工商注册号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierGong" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>联系人 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtLikeMan" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>银行账号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierBrandNo" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>职务 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtJob" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>开户行 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierBrandName" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">传真或邮箱 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFoxOrEmail" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td height="25" width="30%" align="right">
                <font style="color: Red">*</font>省份城市 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlCity" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">是否留资料 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:CheckBox ID="chkIfSave" Text="是否留资料" runat="server" Checked="False" />
            </td>
            <td height="25" width="30%" align="right" rowspan="4">主营范围 ：
            </td>
            <td height="25" width="*" align="left" rowspan="4">
                <asp:TextBox ID="txtMainRange" runat="server" TextMode="MultiLine" Width="95%" Height="120PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">QQ/MSN联系 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtQQMsn" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">供应商ID ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierId" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">联系人地址 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierAddress" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">备注 ：
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="800px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right"><font style="color: Red">*</font>供应商特性 ：
            </td>
            <td height="25" width="*" align="left" colspan="3">
                <asp:DropDownList ID="ddlPeculiarity" runat="server">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                     <asp:ListItem Text="厂家" Value="厂家"></asp:ListItem>
                     <asp:ListItem Text="代理商" Value="代理商"></asp:ListItem>
                      <asp:ListItem Text="总代理" Value="总代理"></asp:ListItem>
                       <asp:ListItem Text="个人" Value="个人"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人："></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果："></asp:Label>
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
            <td height="25" width="30%" align="right">
                <asp:Label ID="lblYiJian" runat="server" Text="本次审批意见："></asp:Label>
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click"
                    Visible="false" />&nbsp;
                <asp:Button ID="btnCopy" runat="server" Text=" 复制 " Visible="false" BackColor="Yellow"
                    OnClick="btnCopy_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " Visible="false" BackColor="Yellow"
                    OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " Visible="false" BackColor="Yellow"
                    OnClick="btnClose_Click" />&nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="Button1" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
