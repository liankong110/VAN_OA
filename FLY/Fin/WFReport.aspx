<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFReport.aspx.cs"
    Inherits="VAN_OA.Fin.WFReport" MasterPageFile="~/DefaultMaster.Master"
    Title="银行往来月报表" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Linq" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">


    <asp:Panel ID="plReport" runat="server">

        <style type="text/css">
            table {
                border-collapse: collapse;
                border-spacing: 0;
                border-left: 1px solid #888;
                border-top: 1px solid #888;
            }

            th, td {
                /*border-right: 1px solid #888;
                border-bottom: 1px solid #888;*/
                /*padding: 10px;*/
                font-weight: bold;
                text-align: center;
            }

                td.yellow {
                    background-color: yellow;
                }

                td.blue {
                    background-color: deepskyblue;
                }

            th {
                font-weight: bold;
            }
        </style>
        <% 

            var ruList = reportList.FindAll(t => t.Type == 0);
            var inList = reportList.FindAll(t => t.Type == 1);

            int ruRows = ruList.Count;
            int inRows = inList.Count;
            var CurrentDate=Convert.ToDateTime(ViewState["CurrentDate"]);
            string topYear =CurrentDate.AddYears(-1).ToString("yyyy/MM");
            string topMonth = CurrentDate.AddMonths(-1).ToString("yyyy/MM");
            string year = CurrentDate.ToString("yyyy/MM");
        %>
      <%--  <meta http-equiv="content-type" content="application/ms-excel; charset=UTF-8"/>--%>
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td colspan="8" style="height: 50px; text-align: center;">
                    <h2>银行往来月报表</h2>
                </td>
            </tr>
            <tr>
                <td style="background-color: red;">年/月</td>
                <td style="background-color: red;">交易类型</td>
                <td style="background-color: red;">业务类型</td>
                <td style="background-color: red;">总金额</td>
                <td style="background-color: red;"><%=topYear %> (去年本月)</td>
                <td style="background-color: red;">同比</td>
                <td style="background-color: red;"><%=topMonth %>(本年上月)</td>
                <td style="background-color: red;">环比</td>
            </tr>

            <% for (int i = 0; i < inRows; i++)
                {
                    decimal topTotal = 0;
                    decimal topLiLv = 0;
                    var topModel = topReportList.Find(t => t.Type == 1 && t.Name == inList[i].Name);
                    if (topModel != null)
                    {
                        topTotal = topModel.Total;
                    }
                    if (topTotal != 0)
                    {
                        topLiLv = (inList[i].Total - topTotal) / topTotal;
                    }

                    decimal topMonthTotal = 0;
                    decimal topMonthLiLv = 0;
                    var topMonthModel = topMonthReportList.Find(t => t.Type == 1 && t.Name == inList[i].Name);
                    if (topModel != null)
                    {
                        topMonthTotal = topMonthModel.Total;
                    }
                    if (topMonthTotal != 0)
                    {
                        topMonthLiLv = (inList[i].Total - topMonthTotal) / topMonthTotal;
                    }

            %>

            <tr>
                <% if (i == 0)
                    {
                %>
                <td rowspan="<%= inRows %>">&nbsp;<%= year %> </td>
                <td rowspan="<%= inRows %>">月入账 </td>
                <td><%= inList[i].Name %></td>
                <td><%= inList[i].Total %></td>
                <td class="yellow"><%= topTotal %></td>
                <td class="yellow"><%= string.Format("{0:n6}",topLiLv )%></td>

                <td class="blue"><%= topMonthTotal %></td>
                <td class="blue"><%= string.Format("{0:n6}",topMonthLiLv) %></td>
                <%
                    }
                    else
                    {
                %>
                <td><%= inList[i].Name %></td>
                <td><%= inList[i].Total %></td>
                <td class="yellow"><%= topTotal %></td>
                <td class="yellow"><%= string.Format("{0:n6}",topLiLv) %></td>

                <td class="blue"><%= topMonthTotal %></td>
                <td class="blue"><%= string.Format("{0:n6}",topMonthLiLv) %></td>
                <%

                    } %>
            </tr>
            <%
                } %>



            <% for (int i = 0; i < ruRows; i++)
                {

                    decimal topTotal = 0;
                    decimal topLiLv = 0;
                    var topModel = topReportList.Find(t => t.Type == 0 && t.Name == ruList[i].Name);
                    if (topModel != null)
                    {
                        topTotal = topModel.Total;
                    }
                    if (topTotal != 0)
                    {
                        topLiLv = (ruList[i].Total - topTotal) / topTotal;
                    }

                    decimal topMonthTotal = 0;
                    decimal topMonthLiLv = 0;
                    var topMonthModel = topMonthReportList.Find(t => t.Type == 0 && t.Name == ruList[i].Name);
                    if (topModel != null)
                    {
                        topMonthTotal = topMonthModel.Total;
                    }
                    if (topMonthTotal != 0)
                    {
                        topMonthLiLv = (ruList[i].Total - topMonthTotal) / topMonthTotal;
                    }

            %>

            <tr>
                <% if (i == 0)
                    {
                %>
                <td rowspan="<%= ruRows %>">&nbsp; <%=year%>  </td>
                <td rowspan="<%= ruRows %>">月出账 </td>
                <td><%= ruList[i].Name %></td>
                <td><%= ruList[i].Total %></td>
                <td class="yellow"><%= topTotal %></td>
                <td class="yellow"><%= string.Format("{0:n6}",topLiLv) %></td>

                <td class="blue"><%= topMonthTotal %></td>
                <td class="blue"><%= string.Format("{0:n6}",topMonthLiLv) %></td>
                <%
                    }
                    else
                    {
                %>
                <td><%= ruList[i].Name %></td>
                <td><%= ruList[i].Total %></td>
                <td class="yellow"><%= topTotal %></td>
                <td class="yellow"><%= string.Format("{0:n6}",topLiLv) %></td>

                <td class="blue"><%= topMonthTotal %></td>
                <td class="blue"><%= string.Format("{0:n6}",topMonthLiLv)%></td>
                <%

                    } %>
            </tr>
            <%
                } %>
        </table>
    </asp:Panel>

    <asp:Button ID="btnUpdate" runat="server" Text=" 导出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
</asp:Content>
