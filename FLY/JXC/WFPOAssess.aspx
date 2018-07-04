<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFPOAssess.aspx.cs" Inherits="VAN_OA.JXC.WFPOAssess"
    Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master" Title="销售月考核" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">销售月考核
            </td>
        </tr>
        <tr>
            <td>AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>项目编号：
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
                特殊：
                 <asp:DropDownList ID="ddlSpecial" runat="server">
                     <asp:ListItem Value="-1">全部</asp:ListItem>
                     <asp:ListItem Value="0">非特殊</asp:ListItem>
                     <asp:ListItem Value="1">特殊</asp:ListItem>
                 </asp:DropDownList>

                公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                    DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>


            </td>
        </tr>
        <tr>
            <td colspan="4" >出库发票缺票日:
                <asp:DropDownList ID="ddlCKFPDays" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem>&gt;</asp:ListItem>
                    <asp:ListItem>&lt;</asp:ListItem>
                    <asp:ListItem>&gt;=</asp:ListItem>
                    <asp:ListItem>&lt;=</asp:ListItem>
                    <asp:ListItem>=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtCKFPDays" runat="server" Width="57px"></asp:TextBox>

                应收款未到日:
                <asp:DropDownList ID="ddlYSDKDays" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem>&gt;</asp:ListItem>
                    <asp:ListItem>&lt;</asp:ListItem>
                    <asp:ListItem>&gt;=</asp:ListItem>
                    <asp:ListItem>&lt;=</asp:ListItem>
                    <asp:ListItem>=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtYSDKDays" runat="server" Width="57px"></asp:TextBox>

                 催账提醒未到日:
                <asp:DropDownList ID="ddlCZTXDays" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem>&gt;</asp:ListItem>
                    <asp:ListItem>&lt;</asp:ListItem>
                    <asp:ListItem>&gt;=</asp:ListItem>
                    <asp:ListItem>&lt;=</asp:ListItem>
                    <asp:ListItem>=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtCZTXDays" runat="server" Width="57px"></asp:TextBox>

                 发票有误:
                <asp:DropDownList ID="ddlFPYW" runat="server" Width="200">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">发票金额>项目金额--1</asp:ListItem>
                    <asp:ListItem Value="2">项目的金额>销售汇总的销售金额--2</asp:ListItem>
                    <asp:ListItem Value="3">发票比对不匹配中OA系统的发票号关联的项目编号--3</asp:ListItem>
                    <asp:ListItem Value="4">到款金额>= 项目金额，发票状态=未开票--4</asp:ListItem>
                    <asp:ListItem Value="5">到款金额>项目金额--5</asp:ListItem>
                    <asp:ListItem Value="6">项目不含税且已开全票 + 未开全票--6 </asp:ListItem>
                    <asp:ListItem Value="7">项目含税+(项目金额<5)--7</asp:ListItem>                     
                </asp:DropDownList>
                

                 预付未到票金额:
                <asp:DropDownList ID="ddlYFWDTotal" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem>&gt;</asp:ListItem>
                    <asp:ListItem>&lt;</asp:ListItem>
                    <asp:ListItem>&gt;=</asp:ListItem>
                    <asp:ListItem>&lt;=</asp:ListItem>
                    <asp:ListItem>=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtYFWDTotal" runat="server" Width="57px"></asp:TextBox>
                预付未到票单位:<asp:TextBox ID="txtYFWDUnit" runat="server" Width="163px"></asp:TextBox>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>

            </td>

        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td style="height: 20px; background-color: #336699; color: White;">出库签回单列表
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">发票签回单列表
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">项目未出清单
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">采库未出清单
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">出库发票清单
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">采购未检验清单
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">应收款
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">催帐提醒
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">发票有误
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">发票漏填
            </td>
            <td style="height: 20px; background-color: #336699; color: White;">预付未到票
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
            var dataTable10 = ds.Tables[9];
            var dataTable11 = ds.Tables[10];
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
                    <%=dr[0]%>—<%=dr[1]%>-<%=dr[3] %>
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

                        var days = 0;
                        var MaxDaoKuanDate = dr[2];
                        var minOutTime = dr[3];
                        var POTotal = Convert.ToDecimal(dr[4]);
                        var Total = Convert.ToDecimal(dr[5]);
                        if (POTotal != 0)
                        {
                            if (MaxDaoKuanDate != null && minOutTime != null && MaxDaoKuanDate != DBNull.Value && minOutTime != DBNull.Value && Total >= POTotal)
                            {
                                TimeSpan ts = (Convert.ToDateTime(Convert.ToDateTime(MaxDaoKuanDate).ToString("yyyy-MM-dd")) - Convert.ToDateTime(Convert.ToDateTime(minOutTime).ToString("yyyy-MM-dd")));

                                days = ts.Days;
                            }
                            else if (minOutTime != null && minOutTime != DBNull.Value)
                            {
                                //TimeSpan ts=(DateTime.Now - model.MinOutTime.Value);
                                TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(Convert.ToDateTime(minOutTime).ToString("yyyy-MM-dd")));

                                days = ts.Days;
                            }
                        }

                %>
                <a href="/ReportForms/WFToInvoiceList.aspx?PONo=<%=dr[0]%>" target="_blank">
                    <%=dr[0]%>—<%=dr[1]%>-<%=days%>
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
                    <%=dr[0]%>—<%=dr[1]%>-<%=dr[4]%>天-<%=dr[5]%> </a>
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

            <td style="vertical-align: top;">
                <%
                    foreach (DataRow dr in dataTable11.Rows)
                    {
                        string font = "";
                        //显示的字体黑色表示 30天内（<=30）的数据，红色表示30天以上(>30)的数据.
                        if (Convert.ToInt32(dr["diffDays"]) > 30)
                        {
                            font = "color:red;";
                        }
                %>
                <a href="#" style="<%=font%>">
                    <%=dr[0]%>—<%=dr[2]%>-<%=dr[1]%>
                </a>
                <br />
                <%
                    }
                %>
                &nbsp;
            </td>

            <td style="vertical-align: top;">
                <%
                    foreach (DataRow dr in dataTable10.Rows)
                    {
                %>
                <a href="/JXC/WFSupplierInvoiceToAllFpList.aspx?PONo=<%=dr[0]%>" target="_blank">
                    <%=dr[0]%>—<%=dr[1]%>-<%=dr[2] %>-<%=string.Format("{0:n2}",dr[3]) %>
                </a>
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
</asp:Content>
