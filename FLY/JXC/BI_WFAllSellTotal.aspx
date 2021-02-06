<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BI_WFAllSellTotal.aspx.cs"
    Inherits="VAN_OA.JXC.BI_WFAllSellTotal" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="商业BI图表" %>

<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="VAN_OA.Model.JXC" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">




    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">商业BI图表
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="cbCompare" runat="server" Text="对比年度" OnCheckedChanged="cbCompare_CheckedChanged" AutoPostBack="true" />
                <asp:DropDownList ID="ddlNextYear" runat="server" Enabled="false">
                </asp:DropDownList>
                项目年度:
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>


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
                客户属性：<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Width="50px">
                </asp:DropDownList>
                项目类别：
              <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
              </asp:DropDownList>
                -->
                项目归类:<asp:DropDownList ID="ddlIsSpecial" runat="server">
                    <asp:ListItem Value="0">不含特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                </asp:DropDownList>
                项目含税:
                <asp:DropDownList ID="ddlFax" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">含税</asp:ListItem>
                    <asp:ListItem Value="0">不含税</asp:ListItem>
                </asp:DropDownList>

                客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType">
                </asp:DropDownList>

               
            </td>
        </tr>
        <tr>   <td colspan="4">项目选中：
                <asp:DropDownList ID="ddlIsSelect" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未选中</asp:ListItem>
                    <asp:ListItem Value="1">选中</asp:ListItem>
                </asp:DropDownList>                
                项目模型: 
                <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>             
            &nbsp;&nbsp;|&nbsp;&nbsp;
          项目金额
                <asp:DropDownList ID="ddlFuHao" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                销售金额&nbsp;&nbsp;|&nbsp;&nbsp;
                 项目金额
                <asp:DropDownList ID="ddlPOFaTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                发票金额&nbsp;&nbsp;|&nbsp;&nbsp;

                  项目金额
                <asp:DropDownList ID="ddlShiJiDaoKuan" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                实到帐&nbsp;&nbsp; |&nbsp;&nbsp;
          
                <br />
                项目净利
                <asp:DropDownList ID="ddlProProfit" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtProProfit" runat="server" Width="80px"></asp:TextBox>&nbsp;&nbsp;|&nbsp;&nbsp;
                实际净利
                <asp:DropDownList ID="ddlProTureProfit" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtProTureProfit" runat="server" Width="80px"></asp:TextBox>&nbsp;&nbsp;|&nbsp;&nbsp;
                项目净利
                <asp:DropDownList ID="ddlJingLiTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                实际净利
                &nbsp;&nbsp;|&nbsp;&nbsp;
               项目金额
                <asp:DropDownList ID="ddlEquPOTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtEquTotal" runat="server" Width="80px"></asp:TextBox>
                &nbsp;&nbsp;|&nbsp;&nbsp;
                净利润率
                 <asp:DropDownList ID="ddlJingLi" runat="server">
                     <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                     <asp:ListItem Text=">" Value=">"></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                     <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                     <asp:ListItem Text="=" Value="="></asp:ListItem>
                 </asp:DropDownList>
                <asp:TextBox ID="txtJingLi" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>


    </table>

    <div align="right">
        <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" Visible="false" />&nbsp;&nbsp;&nbsp;
    </div>
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Both">
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                    <asp:Label ID="lblSimpName" runat="server" Text=""></asp:Label>
                </td>
                <td colspan="6" style="height: 20px; background-color: #336699; color: White;">企业总计
                </td>
                <td colspan="6" style="height: 20px; background-color: #336699; color: White;">政府总计
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
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目计划PV
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目进度SPI
                </td>

                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润总额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润占比
                </td>
                <%-- 1 --%>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润占比
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">实际利润
                </td>

                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">发票额
                </td>
                <%-- 1 --%>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目总额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">销售额
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">项目利润
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">利润占比
                </td>
                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">实际利润
                </td>

                <td colspan="1" style="height: 20px; background-color: #336699; color: White;">发票额
                </td>

            </tr>
            <% 
                decimal poSumTotal = allList.Sum(t => t.SumPOTotal);
                decimal PoLiRunTotal = allList.Sum(t => t.PoLiRunTotal);
                decimal MaoLi_QZ = allList.Sum(t => t.MaoLi_QZ);
                decimal MaoLi_ZZ = allList.Sum(t => t.MaoLi_ZZ);
                foreach (var model in allList)
                {%>
            <tr>
                <td>
                    <%=model.AE %>
                </td>
                <td style="background-color: lightgreen">
                    <%=string.Format("{0:n2}",model.SumPOTotal) %>
                </td>
                <td style="background-color: lightblue">
                    <%= poSumTotal==0?"0":string.Format("{0:n2}",model.SumPOTotal/poSumTotal) %>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.PoTotal) %>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.PV) %>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.SPI) %>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.PoLiRunTotal) %>
                </td>
                <td style="background-color: Khaki">
                    <%= PoLiRunTotal==0?"0":string.Format("{0:n2}",model.PoLiRunTotal/PoLiRunTotal) %>
                </td>
                <%-- 1 --%>
                <td style="background-color: lightgreen">
                    <%=string.Format("{0:n2}",model.SumPOTotal_QZ) %>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.SellTotal_QZ) %>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.MaoLi_QZ )%>
                </td>
                <td style="background-color: Khaki">
                    <%= MaoLi_QZ==0?"0":string.Format("{0:n2}",model.MaoLi_QZ/MaoLi_QZ) %>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.TureliRun_QZ) %>
                </td>

                <td>
                    <%=string.Format("{0:n2}",model.sellFPTotal_QZ )%>
                </td>
                <%-- 1 --%>
                <td style="background-color: lightgreen">
                    <%=string.Format("{0:n2}",model.SumPOTotal_ZZ )%>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.SellTotal_ZZ) %>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.MaoLi_ZZ) %>
                </td>
                <td style="background-color: Khaki">
                    <%= MaoLi_ZZ==0?"0":string.Format("{0:n2}",model.MaoLi_ZZ/MaoLi_ZZ) %>
                </td>
                <td>
                    <%=string.Format("{0:n2}",model.TureliRun_ZZ) %>
                </td>

                <td>
                    <%=string.Format("{0:n2}",model.sellFPTotal_ZZ )%>
                </td>

            </tr>
            <%} %>
            <tr>
                <td>合计
                </td>
                <td>
                    <%= string.Format("{0:n2}",allList.Sum(t => t.SumPOTotal))%>
                </td>
                <td></td>
                <td>
                    <%= string.Format("{0:n2}",allList.Sum(t => t.PoTotal))%>
                </td>
                <td></td>
                <td></td>
                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.PoLiRunTotal))%>
                </td>
                <td></td>
                <%-- 1 --%>
                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.SumPOTotal_QZ))%>
                </td>
                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.SellTotal_QZ))%>
                </td>
                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.MaoLi_QZ))%>
                </td>
                <td></td>
                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.TureliRun_QZ))%>
                </td>

                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.sellFPTotal_QZ))%>
                </td>
                <%-- 1 --%>
                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.SumPOTotal_ZZ))%>
                </td>
                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.SellTotal_ZZ))%>
                </td>
                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.MaoLi_ZZ))%>
                </td>
                <td></td>
                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.TureliRun_ZZ))%>
                </td>

                <td>
                    <%=string.Format("{0:n2}",allList.Sum(t => t.sellFPTotal_ZZ))%>
                </td>

            </tr>
        </table>





    </asp:Panel>
    <% 

        if (cbCompare.Checked)
        {
    %>
    <style type="text/css">
        .flows table {
            border: 1px solid #CCCCCC
        }

            .flows table thead,
            .flows tbody tr {
                display: table;
                width: calc(200% - 1px);
                table-layout: fixed;
            }

        .flows tbody {
            display: block;
            overflow-y: scroll;
        }

        .layui-table th,
        .layui-table td {
            padding: 9px 0px;
            text-align: center;
        }
    </style>

    <%
        }
    %>

    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Both">
        <br />
        <br />
        <br />
        <div class="flows">

            <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                border="1">
                <tr>
                    <td style="height: 20px; background-color: #336699; color: White;" colspan="<%=cbCompare.Checked?25:13 %>">
                        <asp:Label ID="lblOtherName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px; background-color: #336699; color: White;">
                        <asp:Label ID="Label1" runat="server" Text="">AE</asp:Label>
                    </td>

                    <%        
                        bool isCompare = cbCompare.Checked;
                        int currentYear = Convert.ToInt32(ddlYear.Text);
                        int nextYear = Convert.ToInt32(ddlNextYear.Text);


                        if (isCompare == false)
                        {
                            for (int i = 1; i <= 12; i++)
                            {
                    %>
                    <td style="height: 20px; background-color: #336699; color: White;"><%=i %>月
                    </td>
                    <%
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= 12; i++)
                            {
                    %>
                    <td style="height: 20px; background-color: #336699; color: White;"><%=nextYear+"-"+i %>月
                    </td>
                    <td style="height: 20px; background-color: #336699; color: White;"><%=currentYear+"-"+i %>月
                    </td>
                    <%
                            }
                        }

                    %>
                </tr>

                <%
                    List<BI_Month> monthList = new List<BI_Month>();
                    if (ViewState["BI_MonthList"] != null)
                    {
                        monthList = ViewState["BI_MonthList"] as List<BI_Month>;
                    }

                    List<BI_Bar> barList = new List<BI_Bar>();

                    List<string> x_compareList = new List<string>();

                    //List<List<BI_Bar>> y_compare1_12_barList = new List<List<BI_Bar>>();


                    //List<BI_Bar> y_compare_barList = new List<BI_Bar>();

                    List<string> allAe = monthList.GroupBy(t => t.AE).Select(t => t.Key).ToList();
                    foreach (string ae1 in allAe)
                    {
                        BI_Bar bar = new BI_Bar();
                        bar.name = ae1;
                        bar.type = "bar";
                        List<decimal> barTotal = new List<decimal>();



                %>
                <tr>

                    <td style="height: 20px;">
                        <%= ae1 %>
                    </td>
                    <%
                        var aeMonth = monthList.FindAll(t => t.AE == ae1);
                        for (int i = 1; i <= 12; i++)
                        {

                            decimal total = (aeMonth.FirstOrDefault(t => t.Month == i && t.Year == currentYear) ?? new BI_Month()).SumPOTotal;




                            if (isCompare)
                            {
                                if (x_compareList.Count < 24) x_compareList.Add(nextYear + "-" + i);
                                decimal nextTotal = (aeMonth.FirstOrDefault(t => t.Month == i && t.Year == nextYear) ?? new BI_Month()).SumPOTotal;
                                barTotal.Add(nextTotal);

                                //y_compare_barList.Add(new BI_Bar {  name=ae1,type="bar",data=new List<decimal> { nextTotal } });
                    %>

                    <td style="height: 20px;"><%=string.Format("{0:n2}",nextTotal) %>
                    </td>
                    <%
                        }
                        if (x_compareList.Count < 24) x_compareList.Add(currentYear + "-" + i);
                        //y_compare_barList.Add(new BI_Bar {  name=ae1,type="bar",data=new List<decimal> { total } });
                        barTotal.Add(total);
                    %>

                    <td style="height: 20px;"><%=string.Format("{0:n2}",total) %>
                    </td>
                    <%

                            bar.data = barTotal;


                        }
                    %>
                </tr>

                <%
                        barList.Add(bar);

                        //y_compare1_12_barList.Add( y_compare_barList);
                    }
                %>

                <tr>
                    <td style="height: 20px;">
                        <asp:Label ID="Label3" runat="server" Text="">小计</asp:Label>
                    </td>

                    <%
                        for (int i = 1; i <= 12; i++)
                        {


                            if (isCompare)
                            {
                    %>
                    <td style="height: 20px;"><%= string.Format("{0:n2}",monthList.FindAll(t=>t.Month==i&&t.Year==nextYear).Sum(t=>t.SumPOTotal)) %>
                    </td>
                    <%
                        }

                    %>

                    <td style="height: 20px;"><%= string.Format("{0:n2}",monthList.FindAll(t=>t.Month==i&&t.Year==currentYear).Sum(t=>t.SumPOTotal)) %>
                    </td>

                    <%  }%>
                </tr>


                <%
                    if (cbCompare.Checked)
                    {

                %>

                <tr>
                    <td style="height: 20px;">
                        <asp:Label ID="Label2" runat="server" Text=""><%= ddlNextYear.Text %>全年合计</asp:Label>
                    </td>

                    <td colspan="24" style="height: 20px;"><%= string.Format("{0:n2}",monthList.FindAll(t=>t.Year==nextYear).Sum(t=>t.SumPOTotal)) %>
                    </td>

                </tr>
                <%
                    }
                %>
                <tr>
                    <td style="height: 20px;">
                        <asp:Label ID="Label4" runat="server" Text=""><%= ddlYear.Text %>全年合计</asp:Label>
                    </td>

                    <td colspan="<%=(cbCompare.Checked?24:12) %>" style="height: 20px;"><%= string.Format("{0:n2}",monthList.FindAll(t=>t.Year==currentYear).Sum(t=>t.SumPOTotal)) %>
                    </td>

                </tr>
            </table>
        </div>
        <br />
        <div id="main" style="height: 500px; border: 1px solid #ccc; padding: 10px;"></div>
        <div id="main2" style="height: 500px; border: 1px solid #ccc; padding: 10px;"></div>

        <%
            if (cbCompare.Checked)
            {
                for (int i = 1; i <= allAe.Count; i++)
                {
        %>
        <div id="mainCompare<%=i %>" style="height: 300px; border: 1px solid #ccc; padding: 10px;"></div>
        <%
                }
            }

        %>


        <%
            int tdCount = 12;
            if (cbCompare.Checked)
            {
                tdCount = 24;
            }
        %>

        <table style="margin: 0px; padding: 0px; width: 100%">
            <%
                for (int i = 1; i <= tdCount; i++)
                {
            %>
            <tr>
                <td style="margin: 0px; padding: 0px; width: 50%">
                    <div id="mainpie<%=i %>" style="height: 200px; border: 1px solid #ccc;"></div>
                </td>
                <td style="margin: 0px; padding: 0px; width: 50%">
                    <div id="mainpie<%=++i %>" style="height: 200px; border: 1px solid #ccc;"></div>
                </td>
            </tr>
            <%
                }
            %>
        </table>


        <%
            List<decimal> poSumList = new List<decimal>();




            List<List<BI_Pie>> allPieList = new List<List<BI_Pie>>();



            foreach (var ae in allAe)
            {
                poSumList.Add(monthList.FindAll(t => t.AE == ae).Sum(t => t.SumPOTotal));
            }

            int year = Convert.ToInt32(ddlYear.Text);
            int nextyear = Convert.ToInt32(ddlNextYear.Text);
            for (int i = 1; i <= 12; i++)
            {
                if (cbCompare.Checked)
                {
                    List<BI_Pie> pieList1 = new List<BI_Pie>();
                    var f1 = monthList.FindAll(t => t.Month == i && t.Year == nextyear);
                    foreach (var ae in allAe)
                    {
                        pieList1.Add(new BI_Pie { name = ae, value = ((f1.Find(t => t.AE == ae) ?? new BI_Month()).SumPOTotal) });
                    }
                    allPieList.Add(pieList1);
                }

                List<BI_Pie> pieList = new List<BI_Pie>();
                var f = monthList.FindAll(t => t.Month == i && t.Year == year);
                foreach (var ae in allAe)
                {
                    pieList.Add(new BI_Pie { name = ae, value = ((f.Find(t => t.AE == ae) ?? new BI_Month()).SumPOTotal) });
                }
                allPieList.Add(pieList);
            }

        %>
        <script src="../Echat/www/js/echarts.js" type="text/javascript"></script>
        <script src="../Echat/theme/macarons.js" type="text/javascript"></script>
        <script type="text/javascript">



            var selectYear = document.getElementById('<%= ddlYear.ClientID %>').value;

            var xAxis_data_1 = <%= Newtonsoft.Json.JsonConvert.SerializeObject(allAe)%>;
            var yAxis_data_1 = <%= Newtonsoft.Json.JsonConvert.SerializeObject(barList)%>;
            //图表2
            var xAxis_data = <%= Newtonsoft.Json.JsonConvert.SerializeObject(allAe)%>;
            var yAxis_data = <%= Newtonsoft.Json.JsonConvert.SerializeObject(poSumList)%>;

            //图1-12月集合
            var yAxis_data_1_12 = <%= Newtonsoft.Json.JsonConvert.SerializeObject(allPieList)%>;

            var x_compareList_1_12 =<%= Newtonsoft.Json.JsonConvert.SerializeObject(x_compareList)%>;
            var isShouAE =<%=cbCompare.Checked?1:0%>;


            // 路径配置
            require.config({
                paths: {
                    echarts: '../Echat/www/js'
                }
            });

            // 使用
            require(
                [
                    'echarts',
                    'echarts/chart/bar',
                    'echarts/chart/pie'
                ],
                function (ec) {
                    // 基于准备好的dom，初始化echarts图表
                    var myChart = ec.init(document.getElementById('main'), 'macarons');
                    var myChart2 = ec.init(document.getElementById('main2'), 'macarons');
                    var myChart3 = ec.init(document.getElementById('mainpie1'), 'macarons');
                    //显示所有AE的销售额
                    var option_2 = {
                        title: {
                            x: 'center',
                            text: selectYear + '年项目总额',
                            subtext: '',
                        },
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                            }
                        },
                        calculable: true,
                        grid: {
                            borderWidth: 0,
                            y: 80,
                            y2: 60
                        },
                        xAxis: [
                            {
                                type: 'category',
                                show: true,
                                data: xAxis_data
                            }
                        ],
                        yAxis: [
                            {
                                type: 'value',
                                show: true
                            }
                        ],
                        series: [
                            {
                                name: '',
                                type: 'bar',
                                itemStyle: {
                                    normal: {
                                        color: function (params) {
                                            // build a color map as your need.
                                            var colorList = [
                                                '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                                                '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                                                '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0'
                                            ];
                                            return colorList[params.dataIndex]
                                        },
                                        label: {
                                            show: true,
                                            position: 'top',
                                            formatter: '{b}\n{c}'
                                        }
                                    }
                                },
                                data: yAxis_data,
                                markPoint: {
                                    tooltip: {
                                        trigger: 'item',
                                        backgroundColor: 'rgba(0,0,0,0)',

                                    },
                                    data: []
                                }
                            }
                        ]
                    };

                    //显示所有AE1-12月的销售额
                    var option_1 = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                                type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                            }
                        },
                        legend: {
                            data: xAxis_data_1
                        },

                        calculable: true,
                        xAxis: [
                            {
                                type: 'category',
                                data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                            }
                        ],
                        yAxis: [
                            {
                                type: 'value'
                            }
                        ],
                        series: yAxis_data_1
                    };

                    var monthShow = 1;
                    for (var i = 0; i < yAxis_data_1_12.length; i++) {

                        if (yAxis_data_1_12.length == 24) {
                            if (i != 0 && i % 2 == 0) {
                                monthShow++;
                            }

                        } else {
                            var monthShow = (i + 1);
                        }


                        var myChart3 = ec.init(document.getElementById('mainpie' + (i + 1)), 'macarons');
                        var option_3 = {
                            title: {
                                text: monthShow + '月',
                                subtext: '',
                                x: 'center'
                            },
                            tooltip: {
                                trigger: 'item',
                                formatter: "{a} <br/>{b} : {c} ({d}%)"
                            },
                            legend: {
                                orient: 'vertical',
                                x: 'left',
                                data: xAxis_data_1
                            },
                            toolbox: {
                                show: false,
                                feature: {
                                    mark: { show: true },
                                    dataView: { show: true, readOnly: false },
                                    magicType: {
                                        show: true,
                                        type: ['pie', 'funnel'],
                                        option: {
                                            funnel: {
                                                x: '25%',
                                                width: '50%',
                                                funnelAlign: 'left',
                                                max: 1548
                                            }
                                        }
                                    },
                                    restore: { show: true },
                                    saveAsImage: { show: true }
                                }
                            },
                            calculable: true,
                            series: [
                                {
                                    name: monthShow + '月',
                                    type: 'pie',
                                    radius: '55%',
                                    center: ['50%', '60%'],
                                    data: yAxis_data_1_12[i],
                                    itemStyle: {
                                        emphasis: {
                                            shadowBlur: 10,
                                            shadowOffsetX: 0,
                                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                                        },
                                        normal: {
                                            label: {
                                                show: true,
                                                formatter: '{b} : {c} ({d}%)'
                                            },
                                            labelLine: { show: true }
                                        }
                                    }
                                }
                            ]
                        };
                        myChart3.setOption(option_3);
                    }

                    if (isShouAE == 1) {
                        for (var i = 0; i < xAxis_data_1.length; i++) {

                            var mainCompare1 = ec.init(document.getElementById('mainCompare' + (i + 1)), 'macarons');
                            var option_compare = {
                                title: {
                                    text: '',
                                    subtext: ''
                                },
                                tooltip: {
                                    trigger: 'axis'
                                },
                                legend: {
                                    data: [xAxis_data_1[i]],
                                    orient: 'vertical',  //垂直显示
                                    y: 'center',    //延Y轴居中
                                    x: 'right' //居右显示
                                },

                                calculable: true,
                                xAxis: [
                                    {
                                        type: 'category',
                                        data: x_compareList_1_12
                                    }
                                ],
                                yAxis: [
                                    {
                                        type: 'value'
                                    }
                                ],
                                series: [{
                                    name: yAxis_data_1[i].name, data: yAxis_data_1[i].data, type: "bar", itemStyle: {
                                        normal: {
                                            label: {
                                                show: true, //开启显示
                                                position: 'top', //在上方显示
                                                textStyle: { //数值样式
                                                    color: 'black',
                                                    fontSize: 9
                                                }
                                            }
                                        }
                                    }
                                }]


                            };
                            mainCompare1.setOption(option_compare);
                        }
                    }
                    // 为echarts对象加载数据 
                    myChart.setOption(option_1);
                    myChart2.setOption(option_2);
                }
            );



        </script>
    </asp:Panel>


    <%-- 用来做对比 --%>
    <asp:Panel ID="Panel3" runat="server" Width="100%" ScrollBars="Both">
    </asp:Panel>
</asp:Content>
