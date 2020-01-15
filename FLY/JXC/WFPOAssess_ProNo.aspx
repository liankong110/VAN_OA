<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFPOAssess_ProNo.aspx.cs"
    Inherits="VAN_OA.JXC.WFPOAssess_ProNo" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="结算预审" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                结算预审
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
            <td colspan="4">
                <div style="float: left; display: inline;">
                    项目关闭：
                    <asp:DropDownList ID="ddlIsClose" runat="server">
                        <asp:ListItem Text="全部" Value="-1"> </asp:ListItem>
                        <asp:ListItem Text="关闭" Value="1"> </asp:ListItem>
                        <asp:ListItem Text="未关闭" Value="0"> </asp:ListItem>
                    </asp:DropDownList>
                       结算选中：
                   <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                <asp:ListItem Value="1" Text="选中"></asp:ListItem>
                <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                </asp:DropDownList>
             <%--   不含特殊:
                <asp:CheckBox ID="cbIsSpecial" runat="server" Checked="true" />--%>

                  特殊：
                 <asp:DropDownList ID="ddlSpecial" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>                                        
                </asp:DropDownList>   

                </div>
                <div style="float: right; display: inline;">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td style="height: 20px; background-color: #336699; color: White;">
                    出库签回单列表
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    发票签回单列表
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    项目未出清单
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    采库未出清单
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    出库发票清单
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    采购未检验清单
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    应收款
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    催帐提醒
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    发票有误
                </td>
            </tr>
            <% if (ds != null)
               {%>
            <%

               //--出库单签回单-------需要显示 每个AE的未签回单  项目编号
               var dataTable1 = ds.Tables[0];
               var dataTable2 = ds.Tables[1];
               //var dataTable3 = ds.Tables[2];
               var dataTable4 = ds.Tables[3];
               var dataTable5 = ds.Tables[4];
               var dataTable6 = ds.Tables[5];
               var dataTable7 = ds.Tables[6];
               var dataTable8 = ds.Tables[7];
               var dataTable9 = ds.Tables[8];
            %>
            <tr>
                <td style="vertical-align: top;">
                    <%
               foreach (DataRow dr in dataTable1.Rows)
               {
                    %>
                    <a href="/JXC/Sell_OrderOutHouseBackList.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>
                    </a>
                    <br />
                    <%
               }
                    %>
                    &nbsp;
                </td>
                <td style="vertical-align: top;">
                    <%
               foreach (DataRow dr in dataTable2.Rows)
               {
                    %>
                    <a href="/JXC/Sell_OrderPFBackList.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>
                        —<%=dr[1]%>
                    </a>
                    <br />
                    <%
               }
                    %>
                    &nbsp;
                </td>
                <td style="vertical-align: top;">
                    <%
               foreach (DataRow dr in dataTable9.Rows)
               {
                    %>
                    <a href="/JXC/WFPONotCaiReport.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>
                    </a>
                    <br />
                    <%
               }
                    %>
                    &nbsp;
                </td>
                <td style="vertical-align: top;">
                    <%
               foreach (VAN_OA.Model.HashTableModel model in resut_SellGoodsList)
               {
                    %>
                    <a href="/JXC/NoSellAndCaiGoods.aspx?PONo1=<%=model.Key%>" target="_blank">
                        <%=model.Key%>—<%=model.Value%>
                    </a>
                    <br />
                    <%
               }
                    %>
                    &nbsp;
                </td>
                <td style="vertical-align: top;">
                    <%
               foreach (DataRow dr in dataTable4.Rows)
               {
                    %>
                    <a href="/JXC/WFSellFPReport.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>
                    </a>
                    <br />
                    <%
               }
                    %>
                    &nbsp;
                </td>
                <td style="vertical-align: top;">
                    <%
               foreach (DataRow dr in dataTable5.Rows)
               {
                    %>
                    <a href="/JXC/WFCaiNotRuReport.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>
                    </a>
                    <br />
                    <%
               }
                    %>
                    &nbsp;
                </td>
                <td style="vertical-align: top;">
                    <%
               foreach (DataRow dr in dataTable6.Rows)
               {
                    %>
                    <a href="/ReportForms/WFToInvoiceList.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>
                    </a>
                    <br />
                    <%
               }
                    %>
                    &nbsp;
                </td>
                <td style="vertical-align: top;">
                    <%
               foreach (DataRow dr in dataTable7.Rows)
               {
                   object hadFpTotal = dr[2];
                   object POTotal = dr[3];
                   string myColor = "black";
                   if (hadFpTotal == null || hadFpTotal == DBNull.Value)
                   {
                       hadFpTotal = "0";
                   }
                   if (POTotal == null || POTotal == DBNull.Value)
                   {
                       POTotal = "0";
                   }

                   if (Convert.ToDecimal(hadFpTotal) != 0 && Convert.ToDecimal(hadFpTotal) == Convert.ToDecimal(POTotal))
                   {
                       myColor = "Red";
                   }
                   else if (Convert.ToDecimal(hadFpTotal) != 0 && Convert.ToDecimal(hadFpTotal) < Convert.ToDecimal(POTotal))
                   {
                       myColor = "#E36C0A";
                   }
                    
                   
                    %>
                    <a href="/ReportForms/WFToInvoiceList.aspx?ishebing=1&PONo=<%=dr[0]%>" style="color: <%= myColor %>"
                        target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>-<%=dr[4]%>天 </a>
                    <br />
                    <%
               }
                    %>
                    &nbsp;
                </td>
                <td style="vertical-align: top;">
                    <%
               foreach (DataRow dr in dataTable8.Rows)
               {
                    %>
                    <%
                   if (dr[2].ToString() == "1")
                   { 
                    %>
                    <a href="/JXC/Sell_OrderPFList.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>-<%=dr[2]%></a>
                    <%
                   }

                   if (dr[2].ToString() == "2")
                   { 
                    %>
                    <a href="/JXC/JXC_REPORTTotalList.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>-<%=dr[2]%></a>
                    <%
                        }

                        if (dr[2].ToString() == "3")
                        { 
                    %>
                    <a href="/KingdeeInvoice/WFInvoiceCompare.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>-<%=dr[2]%></a>
                    <%
                        }
                        if (dr[2].ToString() == "4")
                        { 
                    %>
                    <a href="/ReportForms/WFToInvoiceList.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>-<%=dr[2]%></a>
                    <%
                         }
                         if (dr[2].ToString() == "5")
                         { 
                    %>
                    <a href="/ReportForms/WFToInvoiceList.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>-<%=dr[2]%></a>
                    <%
                         }
                         if (dr[2].ToString() == "6")
                         { 
                    %>
                    <a href="/ReportForms/WFToInvoiceList.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>-<%=dr[2]%></a>
                    <%
                         }
                         if (dr[2].ToString() == "7")
                         { 
                    %>
                    <a href="/ReportForms/WFToInvoiceList.aspx?PONo=<%=dr[0]%>" target="_blank">
                        <%=dr[0]%>—<%=dr[1]%>-<%=dr[2]%></a>
                    <%
                         }
                    %>
                    <br />
                    <%
               }
                    %>
                    &nbsp;
                </td>
            </tr>
            <%
           }%>
        </table>
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td style="height: 20px; background-color: #336699; color: White;">
                    申请请款单 和预期报销单
                </td>
                <td style="height: 20px; background-color: #336699; color: White;">
                    项目金额<>销售金额
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <%
                        if (dt_Fund != null)
                        {
                            foreach (DataRow dr in dt_Fund.Rows)
                            {

                                if (dr[0].ToString() == "9")
                                { 
                    %>
                     <a href="/EFrom/FundsUse.aspx?ProId=<%=dr[0]%>&allE_id=<%=dr[3]%>&EForm_Id=<%=dr[4]%>" target="_blank">
                    （请）<%=dr[2]%>-<%=dr[1]%></a><br />
                    <%
                            }

                            if (dr[0].ToString() == "12")
                            { 
                    %>
                 <a href="/EFrom/DispatchList.aspx?ProId=<%=dr[0]%>&allE_id=<%=dr[3]%>&EForm_Id=<%=dr[4]%>" target="_blank">
                        （预）<%=dr[2]%>-<%=dr[1]%></a><br />
                    <%
                        }

                        }
                    }
                    %>
                </td>
                <td style="vertical-align: top;">
                    <%
                        if (pOOrderList != null)
                        {
                            foreach (VAN_OA.Model.JXC.JXC_REPORTTotal mm in pOOrderList)
                            {                            
                    %>
                    <a href="/JXC/JXC_REPORTTotalList.aspx?PONo=<%=mm.PONo%>" target="_blank">
                        <%=mm.PONo%>-<%=mm.AE%></a><br />
                    <%
                        }
                    }
                    %>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
