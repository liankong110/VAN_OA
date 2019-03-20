<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSumAllSellTotal.aspx.cs"
    Inherits="VAN_OA.JXC.WFSumAllSellTotal" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="销售统计报表" %>

<%@ Import Namespace="System.Linq" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">销售统计报表
            </td>
        </tr>
        <tr>
            <td colspan="2">项目时间:
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
            <td>公司名称：
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
            <td colspan="4">项目关闭：
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

                客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType">
                </asp:DropDownList>
                客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Style="left: 0px;">
                </asp:DropDownList>
                项目归类： 
                <asp:DropDownList ID="ddlIsSpecial" runat="server">
                    <asp:ListItem Value="0">不含特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                </asp:DropDownList>
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Both">
        <table cellpadding="0" cellspacing="0" width="150%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                    <asp:Label ID="lblSimpName" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="6" style="height: 20px; background-color: #336699; color: White;">企业总计
                </td>
                <td colspan="6" style="height: 20px; background-color: #336699; color: White;">政府总计
                </td>
                <td colspan="4" style="height: 20px; background-color: #336699; color: White;">企业选中
                </td>
                <td colspan="4" style="height: 20px; background-color: #336699; color: White;">政府选中
                </td>
            </tr>
            <tr>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;"></td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额占比
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售总额
                </td>
                   <td colspan="1" style="height: 20px; background-color: #336699; color: White;">PV
                </td>
                   <td colspan="1" style="height: 20px; background-color: #336699; color: White;">SPI
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润总额
                </td>

                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润占比
                </td>

               <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润占比
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">实际利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">发票额
                </td>
              
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润占比
                </td>

                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">实际利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">发票额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">实际利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">发票额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">实际利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">发票额
                </td>
            </tr>
            <% 
                var allSumPototal = allList.Sum(t => t.SumPOTotal);
                var allMaoLi_QZ = allList.Sum(t => t.MaoLi_QZ);
                var allMaoLi_ZZ = allList.Sum(t => t.MaoLi_ZZ);
                var allPoLiRunTotal = allList.Sum(t => t.PoLiRunTotal);
                foreach (var model in allList)
                {%>
            <tr>
                <%-- 1 --%>
                <td>
                    <%=model.AE %>
                </td>
                <td align="right">
                    <%=model.SumPOTotal.ToString("n2") %>
                </td>
                 <td align="right">
                    <%=(allSumPototal!=0?(model.SumPOTotal/allSumPototal):0).ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.PoTotal.ToString("n2") %>
                </td>
                 <td align="right">
                    <%=model.PV.ToString("n2") %>
                </td>
                 <td align="right">
                    <%=model.SPI.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.PoLiRunTotal.ToString("n2") %>
                </td>
                <td align="right">
                    <%=(allPoLiRunTotal!=0?(model.PoLiRunTotal/allPoLiRunTotal):0).ToString("n2") %>
                </td>
                <%-- 2 --%>
                <td align="right">
                    <%=model.SumPOTotal_QZ.ToString("n2") %>
                </td>

                <td align="right">
                    <%=model.MaoLi_QZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=(allMaoLi_QZ!=0?(model.MaoLi_QZ/allMaoLi_QZ):0).ToString("n2") %>                   
                </td>
                <td align="right">
                    <%=model.TureliRun_QZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.SellTotal_QZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.sellFPTotal_QZ.ToString("n2") %>
                </td>
                <%-- 3 --%>

                <td align="right">
                    <%=model.SumPOTotal_ZZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.MaoLi_ZZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=(allMaoLi_ZZ!=0?(model.MaoLi_ZZ/allMaoLi_ZZ):0).ToString("n2") %>
                   
                </td>
                <td align="right">
                    <%=model.TureliRun_ZZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.SellTotal_ZZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.sellFPTotal_ZZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.MaoLi_QXZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.TureliRun_QXZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.SellTotal_QXZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.sellFPTotal_QXZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.MaoLi_ZXZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.TureliRun_ZXZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.SellTotal_ZXZ.ToString("n2") %>
                </td>
                <td align="right">
                    <%=model.sellFPTotal_ZXZ.ToString("n2") %>
                </td>
            </tr>
            <%} %>
            <tr>
                <td>合计
                </td>
                <td align="right">
                    <%= allList.Sum(t => t.SumPOTotal).ToString("n2")%>
                </td>
                <td></td>
                <td align="right">
                    <%= allList.Sum(t => t.PoTotal).ToString("n2")%>
                </td>
                <td></td>
                   <td></td>   
                <td align="right">
                    <%=allList.Sum(t => t.PoLiRunTotal).ToString("n2")%>
                </td>
                <td></td>
                <td align="right">
                    <%=allList.Sum(t => t.SumPOTotal_QZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.MaoLi_QZ).ToString("n2")%>
                </td>
                <td></td>
                <td align="right">
                    <%=allList.Sum(t => t.TureliRun_QZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.SellTotal_QZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.sellFPTotal_QZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.SumPOTotal_ZZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.MaoLi_ZZ).ToString("n2")%>
                </td>
                <td></td>
                <td align="right">
                    <%=allList.Sum(t => t.TureliRun_ZZ).ToString("n2")%>
                </td>
                
                <td align="right">
                    <%=allList.Sum(t => t.SellTotal_ZZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.sellFPTotal_ZZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.MaoLi_QXZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.TureliRun_QXZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.SellTotal_QXZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.sellFPTotal_QXZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.MaoLi_ZXZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.TureliRun_ZXZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.SellTotal_ZXZ).ToString("n2")%>
                </td>
                <td align="right">
                    <%=allList.Sum(t => t.sellFPTotal_ZXZ).ToString("n2")%>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
