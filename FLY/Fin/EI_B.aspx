<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EI_B.aspx.cs" Inherits="VAN_OA.Fin.EI_B" MasterPageFile="~/DefaultMaster.Master"
    Title="电子票据B" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">电子票据B
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">供应商全称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtSupplierName" runat="server" Width="200px" AutoPostBack="True" OnTextChanged="txtSupplierName_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetFullSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtSupplierName">
                </cc1:AutoCompleteExtender>
                <asp:HiddenField ID="hfSimpName" runat="server" />
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">开户行 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBandName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">银行账户 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtBrandNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">省份城市 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtProvice" runat="server" Width="200px"></asp:TextBox>
                <asp:TextBox ID="txtCity" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">金额 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtTotal" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">票据类型 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="dllBillType" runat="server" DataValueField="Id" DataTextField="BillName">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">财务票据 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="dllPerson" runat="server" DataTextField="Name"
                    DataValueField="Id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">用途 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="dllUse" runat="server">
                     <asp:ListItem></asp:ListItem>
                    <asp:ListItem>货款</asp:ListItem>
                    <asp:ListItem>备用金</asp:ListItem>
                    <asp:ListItem>差旅费</asp:ListItem>
                    <asp:ListItem>还款</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">公司名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="dllCompany" runat="server">
                    <asp:ListItem>苏州万邦电脑系统有限公司</asp:ListItem>
                    <asp:ListItem>苏州工业园区万邦科技有限公司</asp:ListItem>
                    <asp:ListItem>苏州源达万维智能科技有限公司</asp:ListItem>
                    <asp:ListItem>苏州易佳通网络科技有限公司</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnYuLan" runat="server" Text=" 预览 " BackColor="Yellow" OnClick="btnYuLan_Click" />&nbsp;
                    <asp:Button ID="btnPrint" runat="server" Text=" 打印进账单 " BackColor="Yellow" OnClick="btnPrint_Click" />&nbsp;&nbsp;                 
            </td>
        </tr>
    </table>
</asp:Content>
