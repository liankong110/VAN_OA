<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JXCItems_REPORTTotalList.aspx.cs"
    Inherits="VAN_OA.JXC.JXCItems_REPORTTotalList" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="项目费用汇总统计" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <style type="text/css">
        .item
        {
            word-break: break-all;
            word-wrap: break-word;
        }
        .gridview_m
        {
            border-collapse: collapse;
            border: solid 1px #93c2f1;
            width: 98%;
            font-size: 10pt;
        }
    </style>
    <script type="text/javascript">
        function GetDetail(PONO) {

            var url = "../JXC/JXC_REPORTList.aspx?PONo=" + PONO + "&IsSpecial=true";  //+ document.getElementById('<%= cbIsSpecial.ClientID %>').value;

            window.open(url, 'newwindow')
        }

        function checkAll() {
            document.getElementById("Panel1").disabled = false;
        }
    </script>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                项目费用汇总统计
            </td>
        </tr>
        <tr>
            <td>
                项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
            </td>
            <td>
                项目名称:
            </td>
            <td>
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                    Width="150PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                项目时间:
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
            <td>
                客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                AE：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
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
                </asp:DropDownList>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
                不含特殊
                <asp:CheckBox ID="cbIsSpecial" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="初始状态" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="已交付" />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="已开票" />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="已结清" />
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox5" runat="server" Text="发票金额不一致" /><asp:CheckBox
                    ID="CheckBox6" runat="server" Text="实际金额不一致" />
            </td>
            <td colspan="2">
                <asp:CheckBox ID="CheckBox7" runat="server" Text="全部" AutoPostBack="True" OnCheckedChanged="CheckBox7_CheckedChanged" />
                <asp:Panel ID="Panel1" runat="server">
                    <asp:CheckBox ID="CheckBox8" runat="server" Text="管理费" />
                    <asp:CheckBox ID="CheckBox9" runat="server" Text="非材料报销（邮寄费）" />
                    <asp:CheckBox ID="CheckBox10" runat="server" Text="非材料报销（非邮寄费）" />
                    <asp:CheckBox ID="CheckBox11" runat="server" Text="公交车费" />
                    <asp:CheckBox ID="CheckBox12" runat="server" Text="私车油耗费" /><br />
                    <asp:CheckBox ID="CheckBox13" runat="server" Text="用车申请油耗费" />
                    <asp:CheckBox ID="CheckBox14" runat="server" Text="行政采购金额" />
                    <asp:CheckBox ID="CheckBox15" runat="server" Text="会务费" />
                    <asp:CheckBox ID="CheckBox16" runat="server" Text="人工费" />
                    <asp:CheckBox ID="CheckBox17" runat="server" Text="加班单" />
                </asp:Panel>
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            Width="120%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand">
            <PagerTemplate>
                <br />
                <%--<asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
                <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="First"></asp:LinkButton>
                <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                    CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                    CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                <br />--%>
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            项目编号
                        </td>
                        <td>
                            项目名称
                        </td>
                        <td>
                            项目日期
                        </td>
                        <td>
                            客户名称
                        </td>
                        <td>
                            销售额
                        </td>
                        <td>
                            发票总额
                        </td>
                        <td>
                            实际到账
                        </td>
                        <td>
                            总成本
                        </td>
                        <td>
                            项目净利润
                        </td>
                        <td>
                            实际利润
                        </td>
                        <td>
                            发票号码
                        </td>
                        <td>
                            账期（天）
                        </td>
                        <td>
                            实际到款期
                        </td>
                        <td>
                            AE
                        </td>
                        <td>
                            INSIDE
                        </td>
                        <td>
                            费用类型
                        </td>
                        <td>
                            费用金额
                        </td>
                        <td>
                            会务费用
                        </td>
                        <td>
                            总费用
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="项目编号" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton11" runat="server" CommandName="PoNo" UnderLine="1"
                            CommandArgument='<% #Eval("PoNo_FpTotal") %>' Text='<% #Eval("PONo") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" CssClass="item"></ItemStyle>
                </asp:TemplateField>
                <%-- <asp:BoundField  ItemStyle-Width="5%" DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>--%>
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="6%" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Left">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="goodSellTotal" HeaderText="销售额" DataFormatString="{0:n0}"
                    SortExpression="goodSellTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SellFPTotal" HeaderText="发票总额" DataFormatString="{0:n0}"
                    SortExpression="SellFPTotal" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="InvoiceTotal" HeaderText="实际到账" DataFormatString="{0:n0}"
                    SortExpression="InvoiceTotal" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="goodTotal" HeaderText="总成本" SortExpression="goodTotal"
                    DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="maoliTotal" HeaderText="净利润" SortExpression="maoliTotal"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" DataFormatString="{0:n0}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TrueLiRun" ItemStyle-Width="5%" HeaderText="实际利润" SortExpression="TrueLiRun"
                    DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ZhangQiTotal" ItemStyle-Width="3%" HeaderText="账期/天" SortExpression="ZhangQiTotal"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
          <%--      <asp:BoundField DataField="trueZhangQi" ItemStyle-Width="3%" HeaderText="实际到款期" SortExpression="trueZhangQi"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>--%>

                  <asp:TemplateField HeaderText="实际到款期">
                    <ItemTemplate>
                        <asp:Label ID="lblDays" runat="server" Text='<% #Eval("trueZhangQi") %>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="3%" ItemStyle-CssClass="item">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="INSIDE" HeaderText="INSIDE" ItemStyle-Width="3%" SortExpression="INSIDE"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <%-- <asp:BoundField DataField="potype" HeaderText="费用类型" SortExpression="potype" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="5%" ItemStyle-CssClass="item">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="费用类型" ItemStyle-Width="40px">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="selectPoNo" UnderLine="1"
                            CommandArgument='<% #Eval("PONO_ProType") %>' Text='<% #Eval("potype") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" CssClass="item"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="itemTotal" HeaderText="费用金额" SortExpression="itemTotal"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="HuiWuTotal" HeaderText="会务费" SortExpression="HuiWuTotal"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="allItemTotal" HeaderText="总费用" DataFormatString="{0:n2}"
                    SortExpression="allItemTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <%-- <asp:TemplateField HeaderText="费用明细">
                    <ItemTemplate>
                        <asp:Table ID="Table3" style=" vertical-align:top;" runat="server" cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
                        </asp:Table>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
        OnRowCommand="gvList_RowCommand">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        订单号
                    </td>
                    <td>
                        申请人
                    </td>
                    <td>
                        项目名称
                    </td>
                    <td height="25" align="center">
                        审批日期
                    </td>
                    <td height="25" align="center">
                        金额
                    </td>
                    <td height="25" align="center">
                        费用类型
                    </td>
                    <td height="25" align="center">
                        AE
                    </td>
                    <tr>
                        <td colspan="4" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="订单号">
                <ItemTemplate>
                    <a target="_blank" href="/JXC/WFTemp.aspx?proId=<%# Eval("proId") %>&allE_id=<%# Eval("allE_id") %>">
                        <%# Eval("ProNo")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="loginName" HeaderText="申请人" SortExpression="loginName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="poName" HeaderText="项目名称" SortExpression="poName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="createTime" HeaderText="审批日期" SortExpression="createTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="total" HeaderText="金额" SortExpression="total" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="poType" HeaderText="费用类型" SortExpression="poType" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <%--  <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />--%>
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <asp:Label ID="Label3" runat="server" Text="费用金额合计"></asp:Label>
    <asp:Label ID="lblItemTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label6" runat="server" Text="会务费用合计"></asp:Label>
    <asp:Label ID="lblHuiWuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label9" runat="server" Text="总费用合计"></asp:Label>
    <asp:Label ID="lblAllItemTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <br />
    <asp:Label ID="Label8" runat="server" Text="项目金额"></asp:Label>
    <asp:Label ID="lblCurrentPOTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label7" runat="server" Text="销售额合计"></asp:Label>
    <asp:Label ID="lblXSE" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label2" runat="server" Text="发票总额合计"></asp:Label>
    <asp:Label ID="lblFP" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <asp:Label ID="lblVisAllPONO" runat="server" Text="" Visible="false"></asp:Label>
    <br />
    <asp:Label ID="Label5" runat="server" Text="到款总额"></asp:Label>
    <asp:Label ID="lblDaoKuanTotal" runat="server" Text="" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label10" runat="server" Text="到款日期"></asp:Label>
    <asp:Label ID="lblDaoKuanDate" runat="server" Text="" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label12" runat="server" Text="实际结算期"></asp:Label>
    <asp:Label ID="lblTrueDate" runat="server" Text="" ForeColor="Red"></asp:Label>
    <br />
    <asp:Label ID="Label11" runat="server" Text="项目未到到款金额"></asp:Label>
    <asp:Label ID="lblPoWeiDaoTotal" runat="server" Text="" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label14" runat="server" Text="已开票未到款"></asp:Label>
    <asp:Label ID="lblInvoiceNoDao" runat="server" Text="" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label16" runat="server" Text="未开票金额"></asp:Label>
    <asp:Label ID="lblWeiKaiPiao" runat="server" Text="" ForeColor="Red"></asp:Label>
    <br />
    <asp:Label ID="Label1" runat="server" Text="项目净利合计"></asp:Label>
    <asp:Label ID="lblJLR" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label4" runat="server" Text="实际利润合计"></asp:Label>
    <asp:Label ID="lblSJLR" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label13" runat="server" Text="扣管理费后利润率 PP"></asp:Label>
    <asp:Label ID="lblPP" runat="server" Text="" ForeColor="Red"></asp:Label>
</asp:Content>
