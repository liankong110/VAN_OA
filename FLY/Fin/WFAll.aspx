<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="WFAll.aspx.cs" Inherits="VAN_OA.Fin.WFAll"
    MasterPageFile="~/DefaultMaster.Master" Title="项目结算" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td style="height: 20px; background-color: #336699; color: White;">
                项目结算
            </td>
        </tr>
        <tr>
            <td>
                <div style="vertical-align: middle" align="center">
                    <asp:Button ID="btnCommon" runat="server" Text=" 公共费用 " BackColor="Yellow" OnClick="btnCommon_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSpec" runat="server" Text=" 个性费用 " BackColor="Yellow" 
                        onclick="btnSpec_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                     <%--    <asp:Button ID="btnJieSuan" runat="server" Text="  结算类别定义 " 
                        BackColor="Yellow" onclick="btnJieSuan_Click" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                   <%-- <asp:Button ID="btnSellInfo" runat="server" Text=" 销售结算明细表 " BackColor="Yellow" 
                        onclick="btnSellInfo_Click" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                          <asp:Button ID="Button1" runat="server" Text=" 销售结算明细表 " BackColor="Yellow" 
                        onclick="btnSellInfo1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="btnSelected" runat="server" Text=" 自动选中  " 
                        BackColor="Yellow" onclick="btnSelected_Click" 
                        />&nbsp;&nbsp;&nbsp;&nbsp;</div>
            </td>
        </tr>
    </table>
</asp:Content>
