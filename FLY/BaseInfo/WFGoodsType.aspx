﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFGoodsType.aspx.cs" Inherits="VAN_OA.BaseInfo.WFGoodsType"
    MasterPageFile="~/DefaultMaster.Master" Title="商品类别" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                商品类别
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                类别名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtInvName" runat="server" Width="200px"></asp:TextBox>
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
