<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFAllSellTotal.aspx.cs"
    Inherits="VAN_OA.JXC.WFAllSellTotal" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="销售报表" %>

<%@ Import Namespace="System.Linq" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                销售报表
            </td>
        </tr>
        <tr>
            <td colspan="2">
                项目时间:
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
              
            </td>
            <td>
                公司名称：
            </td>
            <td>
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComCode"
                    Width="200PX" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                </asp:DropDownList>
                AE：
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
        <td colspan="4">  项目关闭：
                <asp:DropDownList ID="ddlIsClose" runat="server">
                    <asp:ListItem Text="全部" Value="-1"> </asp:ListItem>
                    <asp:ListItem Text="关闭" Value="1"> </asp:ListItem>
                    <asp:ListItem Text="未关闭" Value="0" Selected="True"> </asp:ListItem>
                </asp:DropDownList>
                结算选中：
                <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="选中" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                </asp:DropDownList> 
             
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Both">
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td colspan="3" style="height: 20px; background-color: #336699; color: White;">
                    <asp:Label ID="lblSimpName" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                    企业总计
                </td>
                <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                    政府总计
                </td>
                <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                    企业选中
                </td>
                <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                    政府选中
                </td>
            </tr>
            <tr>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    销售总额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    利润总额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    实际利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    发票额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    实际利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    发票额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    实际利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    发票额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    实际利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">
                    发票额
                </td>
            </tr>
            <% 
            
                foreach (var model in allList)
                {%>
            <tr>
                <td>
                    <%=model.AE %>
                </td>
                <td>
                    <%=model.PoTotal %>
                </td>
                <td>
                    <%=model.PoLiRunTotal %>
                </td>
                <td>
                    <%=model.MaoLi_QZ %>
                </td>
                <td>
                    <%=model.TureliRun_QZ %>
                </td>
                <td>
                    <%=model.SellTotal_QZ %>
                </td>
                <td>
                    <%=model.sellFPTotal_QZ %>
                </td>
                <td>
                    <%=model.MaoLi_ZZ %>
                </td>
                <td>
                    <%=model.TureliRun_ZZ %>
                </td>
                <td>
                    <%=model.SellTotal_ZZ %>
                </td>
                <td>
                    <%=model.sellFPTotal_ZZ %>
                </td>
                <td>
                    <%=model.MaoLi_QXZ %>
                </td>
                <td>
                    <%=model.TureliRun_QXZ %>
                </td>
                <td>
                    <%=model.SellTotal_QXZ %>
                </td>
                <td>
                    <%=model.sellFPTotal_QXZ %>
                </td>
                <td>
                    <%=model.MaoLi_ZXZ %>
                </td>
                <td>
                    <%=model.TureliRun_ZXZ %>
                </td>
                <td>
                    <%=model.SellTotal_ZXZ %>
                </td>
                <td>
                    <%=model.sellFPTotal_ZXZ %>
                </td>
            </tr>
            <%} %>
            <tr>
                <td>
                    合计
                </td>
                <td>
                    <%= allList.Sum(t => t.PoTotal)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.PoLiRunTotal)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.MaoLi_QZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.TureliRun_QZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.SellTotal_QZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.sellFPTotal_QZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.MaoLi_ZZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.TureliRun_ZZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.SellTotal_ZZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.sellFPTotal_ZZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.MaoLi_QXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.TureliRun_QXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.SellTotal_QXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.sellFPTotal_QXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.MaoLi_ZXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.TureliRun_ZXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.SellTotal_ZXZ)%>
                </td>
                <td>
                    <%=allList.Sum(t => t.sellFPTotal_ZXZ)%>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
