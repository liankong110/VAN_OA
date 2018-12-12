﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JXC_YuShouKuanTotal.aspx.cs"
    Inherits="VAN_OA.JXC.JXC_YuShouKuanTotal" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="项目预收账款系统" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <style type="text/css">
        .item {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>

    <script type="text/javascript">
        function GetDetail(PONO) {

            var url = "../JXC/JXC_REPORTList.aspx?PONo=" + PONO + "&IsSpecial=true";

            window.open(url, 'newwindow')
        }
    </script>

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">项目预收账款系统
            </td>
        </tr>
        <tr>
            <td>项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                    DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>项目名称:
            </td>
            <td>
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>项目时间:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="300px"></asp:TextBox>
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
            <td>项目归类
            </td>
            <td>
                <asp:DropDownList ID="ddlIsSpecial" runat="server">
                    <asp:ListItem Value="0">不含特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                </asp:DropDownList>
                <%-- <asp:CheckBox ID="cbIsSpecial" runat="server" Checked="true" /> --%>
            </td>
        </tr>
        <tr>
            <td colspan="4">客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                DataTextField="GuestType">
            </asp:DropDownList>
                客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Style="left: 0px;">
                </asp:DropDownList>

                项目关闭：
                <asp:DropDownList ID="ddlIsClose" runat="server">
                    <asp:ListItem Text="全部" Value="-1"> </asp:ListItem>
                    <asp:ListItem Text="关闭" Value="1"> </asp:ListItem>
                    <asp:ListItem Text="未关闭" Value="0"> </asp:ListItem>
                </asp:DropDownList>
                项目选中：
                <asp:DropDownList ID="ddlIsSelect" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未选中</asp:ListItem>
                    <asp:ListItem Value="1">选中</asp:ListItem>
                </asp:DropDownList>
                结算选中：
                   <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                       <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                       <asp:ListItem Value="1" Text="选中"></asp:ListItem>
                       <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                   </asp:DropDownList>项目类别：
                  <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                      <%-- <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1">零售</asp:ListItem>
                    <asp:ListItem Value="2">工程</asp:ListItem>--%>
                  </asp:DropDownList>
                项目金额
                <asp:DropDownList ID="ddlFuHao" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                销售金额&nbsp;&nbsp;&nbsp;&nbsp;
                 项目金额
                <asp:DropDownList ID="ddlPOFaTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                发票金额

                  项目金额
                <asp:DropDownList ID="ddlShiJiDaoKuan" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                实到帐
            </td>
        </tr>
        <tr>
            <td colspan="4">项目模型: 
                <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
                实际帐期: 
                <asp:DropDownList ID="ddlTrueZhangQI" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<=30天" Value="1"></asp:ListItem>
                    <asp:ListItem Text="30天<实际帐期<=60天" Value="2"></asp:ListItem>
                    <asp:ListItem Text="60天<实际帐期<=90天" Value="3"></asp:ListItem>
                    <asp:ListItem Text="90天<实际帐期<=120天" Value="4"></asp:ListItem>
                    <asp:ListItem Text="实际帐期>120天" Value="5"></asp:ListItem>
                </asp:DropDownList>
                预估到款日:
                 <asp:TextBox ID="txtYuDaoDate" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtYuDaoDateTo" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtYuDaoDate">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtYuDaoDateTo">
                </cc1:CalendarExtender>
                预估到款:
                  <asp:DropDownList ID="ddlYuGuDaoKuan" runat="server">
                      <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                      <asp:ListItem Text="<" Value="<"></asp:ListItem>
                      <asp:ListItem Text=">" Value=">"></asp:ListItem>
                      <asp:ListItem Text="=" Value="="></asp:ListItem>
                      <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                  </asp:DropDownList>
                <asp:TextBox ID="txtYuGuDaoKuan" runat="server" Width="80px"></asp:TextBox>

                最近开票日:
                  <asp:TextBox ID="txtBillDate" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtBillDateTo" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton4" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtBillDate">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton5" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtBillDateTo">
                </cc1:CalendarExtender>
                <asp:CheckBox ID="cbWuKaiPiaoRi" runat="server"  Text="无最近开票日" AutoPostBack="True" OnCheckedChanged="cbWuKaiPiaoRi_CheckedChanged"/>
                  <br />
                最近计算开票日:
                 <asp:TextBox ID="txtJSKaiPiaoDate" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtJSKaiPiaoDateTo" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender7" PopupButtonID="ImageButton6" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtJSKaiPiaoDate">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender8" PopupButtonID="ImageButton7" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtJSKaiPiaoDateTo">
                </cc1:CalendarExtender>

                经验账期:
                 <asp:DropDownList ID="ddlAvg_ZQ" runat="server">
                     <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                     <asp:ListItem Text=">" Value=">"></asp:ListItem>
                     <asp:ListItem Text="=" Value="="></asp:ListItem>
                     <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                 </asp:DropDownList>
                <asp:TextBox ID="txtAvg_ZQ" runat="server" Width="40px"></asp:TextBox>

                最近实际到款日:
                  <asp:TextBox ID="txtShiJiDate" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtShiJiDateTo" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender9" PopupButtonID="ImageButton8" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtShiJiDate">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender10" PopupButtonID="ImageButton9" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtShiJiDateTo">
                </cc1:CalendarExtender>


                  预估位序:
                 <asp:DropDownList ID="ddlDaoKuanNumber" runat="server">
                     <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                      <asp:ListItem Text=">" Value=">"></asp:ListItem>
                      <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                       <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                     <asp:ListItem Text="=" Value="="></asp:ListItem>
                     <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                      <asp:ListItem Text="无" Value="无"></asp:ListItem>
                 </asp:DropDownList>
                <asp:TextBox ID="txtDaoKuanNumber" runat="server" Width="40px"></asp:TextBox>

                预估到款日期：
                  <asp:DropDownList ID="ddlYuGuDaoKuan_1" runat="server">
                     <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                     <asp:ListItem Text=">" Value=">"></asp:ListItem>
                     <asp:ListItem Text="=" Value="="></asp:ListItem>
                     <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                       <asp:ListItem Text="无" Value="无"></asp:ListItem>
                 </asp:DropDownList>
                <asp:TextBox ID="txtYuGuDaoKuan_1" runat="server" Width="80px"></asp:TextBox>
                     <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender11" PopupButtonID="ImageButton10" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtYuGuDaoKuan_1">
                </cc1:CalendarExtender>
                <br />
                <asp:CheckBox ID="CheckBox1" runat="server" Text="初始状态" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="已交付" />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="已开票" />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="已结清" />
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox5" runat="server" Text="发票金额不一致" /><asp:CheckBox
                    ID="CheckBox6" runat="server" Text="实际金额不一致" />

                项目净利
                <asp:DropDownList ID="ddlProProfit" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtProProfit" runat="server" Width="80px"></asp:TextBox>
                实际净利
                <asp:DropDownList ID="ddlProTureProfit" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtProTureProfit" runat="server" Width="80px"></asp:TextBox>
                项目净利
                <asp:DropDownList ID="ddlJingLiTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                实际净利

               项目金额
                <asp:DropDownList ID="ddlEquPOTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtEquTotal" runat="server" Width="80px"></asp:TextBox>

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
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            Width="160%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand">
            <PagerTemplate>
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>项目编号
                        </td>
                        <td>项目名称
                        </td>
                        <td>项目日期
                        </td>
                        <td>客户名称
                        </td>
                        <td>销售额
                        </td>
                        <td>项目金额
                        </td>
                        <td>发票总额
                        </td>
                        <td>实到账</td>
                        <td>实际到账
                        </td>
                        <td>总成本
                        </td>
                        <td>项目净利润
                        </td>
                        <td>实际利润
                        </td>
                        <td>发票号码
                        </td>
                        <td>经验账期
                        </td>
                        <td>预估到款日
                        </td>
                        <td>实际到款期
                        </td>
                        <td>AE
                        </td>
                        <td>INSIDE
                        </td>

                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="项目编号" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" Text='<% #Eval("PONo") %>'
                            CommandArgument='<% #Eval("PONo") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目编码">
                    <ItemTemplate>
                        <a href="#" onclick="javascript:GetDetail('<%# Eval("PONo") %>');">
                            <%# Eval("PONo")%></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="5%" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                    HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Left">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="GuestType" HeaderText="类型" SortExpression="GuestType"
                    HeaderStyle-Width="30" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestProString" HeaderText="属性" SortExpression="GuestProString"
                    HeaderStyle-Width="30" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="goodSellTotal" HeaderText="销售额" DataFormatString="{0:n2}"
                    SortExpression="goodSellTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                  <asp:BoundField DataField="Model" HeaderText="模型"
                    SortExpression="Model" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="SumPOTotal" HeaderText="项目金额" DataFormatString="{0:n2}"
                    SortExpression="SumPOTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SellFPTotal" HeaderText="发票额" DataFormatString="{0:n2}"
                    SortExpression="SellFPTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                  
                  <asp:BoundField DataField="MinFPTime" HeaderText="首次开票日"
                    SortExpression="MinFPTime" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                

                <asp:BoundField DataField="BillDate" HeaderText="最近开票日"
                    SortExpression="BillDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                
                  <asp:BoundField DataField="MinBillDate" HeaderText="首次计算开票日"
                    SortExpression="MinBillDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="JSKaiPiaoDate" HeaderText="最近计算开票日"
                    SortExpression="JSKaiPiaoDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="InvoiceTotal" HeaderText="实到账" DataFormatString="{0:n2}"
                    SortExpression="InvoiceTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="goodTotal" HeaderText="总成本" SortExpression="goodTotal"
                    DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="maoliTotal" HeaderText="项目净利" SortExpression="maoliTotal"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TrueLiRun" HeaderText="实际利润" SortExpression="TrueLiRun"
                    DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="JingLi" HeaderText="净利润率" SortExpression="JingLi"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Avg_ZQ" HeaderText="经验账期" SortExpression="Avg_ZQ"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="YuGuDaoKuanDate" HeaderText="预估到款日" SortExpression="YuGuDaoKuanDate" DataFormatString="{0:yyyy-MM-dd}"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                   <asp:BoundField DataField="MinDaoKuanTime_ZQ" HeaderText="首次实际到款日" SortExpression="MinDaoKuanTime_ZQ" DataFormatString="{0:yyyy-MM-dd}"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                  <asp:BoundField DataField="SecondDaoKuanDate" HeaderText="其次实际到款日" SortExpression="SecondDaoKuanDate" DataFormatString="{0:yyyy-MM-dd}"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="MaxDaoKuanDate" HeaderText="最近实际到款日" SortExpression="MaxDaoKuanDate" DataFormatString="{0:yyyy-MM-dd}"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
             
                 <asp:BoundField DataField="DaoKuanNumber" HeaderText="预估位序" 
                    SortExpression="DaoKuanNumber" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="YuGuDaoKuanTotal" HeaderText="预估到款" DataFormatString="{0:n2}"
                    SortExpression="YuGuDaoKuanTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>

                  <asp:TemplateField HeaderText="实际账期">
                    <ItemTemplate>
                        <asp:Label ID="lblZQ" runat="server" Text='<% #Eval("ZQ") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="到款期">
                    <ItemTemplate>
                        <asp:Label ID="lblDays" runat="server" Text='<% #Eval("trueZhangQi") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="3%" ItemStyle-CssClass="item">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="INSIDE" HeaderText="INSIDE" SortExpression="INSIDE" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="FPTotal" HeaderText="发票号码" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="40%" ItemStyle-CssClass="item"></asp:BoundField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
        <br />
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
    <asp:Label ID="Label1" runat="server" Text="项目净利合计"></asp:Label>
    <asp:Label ID="lblJLR" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label4" runat="server" Text="实际利润合计"></asp:Label>
    <asp:Label ID="lblSJLR" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label7" runat="server" Text="销售额合计"></asp:Label>
    <asp:Label ID="lblXSE" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label2" runat="server" Text="发票总额合计"></asp:Label>
    <asp:Label ID="lblFP" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <asp:Label ID="lblVisAllPONO" runat="server" Text="" Visible="false"></asp:Label>
    <br />
    项目总额：<asp:Label ID="lblAllPoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
     项目总利润率：<asp:Label ID="lblAllLRLv" runat="server" Text="0" ForeColor="Red"></asp:Label>%  &nbsp; &nbsp;
     实际总利润率：<asp:Label ID="lblAllTrunLv" runat="server" Text="0" ForeColor="Red"></asp:Label>% &nbsp; &nbsp;
     项目到款总额：<asp:Label ID="lblAllDaoKuan" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
     到款率：<asp:Label ID="lblAllDaoKuanLv" runat="server" Text="0" ForeColor="Red"></asp:Label>%    &nbsp; &nbsp;
     开票率：<asp:Label ID="lblAllFaPiaoLv" runat="server" Text="0" ForeColor="Red"></asp:Label>%  &nbsp; &nbsp;
      <br />
    预估到款合计:<asp:Label ID="lblYuGuDaoKuanTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
      <br />  <br />
    <br />
    项目模型说明： 
       <asp:GridView ID="gvModel" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
           ShowFooter="false" Width="100%" AutoGenerateColumns="False"
           ShowHeader="false"
           Style="border-collapse: collapse;">
           <Columns>
               <asp:BoundField DataField="ModelName" HeaderText="模型名称" SortExpression="MyPoType"  />
               <asp:BoundField DataField="ModelRemark" HeaderText="模型说明" SortExpression="XiShu"  />
           </Columns>
           <PagerStyle HorizontalAlign="Center" />
           <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
           <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
               HorizontalAlign="Center" />
           <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
           <RowStyle CssClass="InfoDetail1" />
           <FooterStyle BackColor="#D7E8FF" />
       </asp:GridView>
</asp:Content>
