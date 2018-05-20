<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="WFToInvoiceList.aspx.cs"
    Inherits="VAN_OA.ReportForms.WFToInvoiceList" MasterPageFile="~/DefaultMaster.Master"
    Title="到款单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style type="text/css">
        .item {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">到款单管理
            </td>
        </tr>
        <tr>
            <td>到款时间:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>单据号:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>项目编码:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONO" runat="server" Width="200px"></asp:TextBox>
                项目名称: 
                <asp:TextBox ID="txtPOName" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td>客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>到款状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="200px">
                    <asp:ListItem>通过</asp:ListItem>
                    <asp:ListItem>执行中</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                    <asp:ListItem>全部</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>发票号:
            </td>
            <td>
                <asp:TextBox ID="txtFPNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>订单时间:
            </td>
            <td>
                <asp:TextBox ID="txtPoDateFrom" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtPoDateTo" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateTo">
                </cc1:CalendarExtender>
            </td>
            <td>发票状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlFPState" runat="server" Width="200px">
                    <asp:ListItem Value="0">所有</asp:ListItem>
                    <asp:ListItem Value="1">已开全票</asp:ListItem>
                    <asp:ListItem Value="2">未开全票</asp:ListItem>
                    <asp:ListItem Value="3">未开票</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>AE：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;项目金额
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtPOTotal" runat="server" Width="200px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="cbPOHeBing" runat="server" Text="到款金额合并" AutoPostBack="true" OnCheckedChanged="cbPOHeBing_CheckedChanged" Checked="true" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel1" runat="server" Enabled="true" Style="display: inline">
                    到款金额
                    <asp:DropDownList ID="ddlJinECha" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    </asp:DropDownList>
                    项目金额 &nbsp;&nbsp;&nbsp;&nbsp; 最长到款期:
                    <asp:DropDownList ID="ddlDiffDays" runat="server">

                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<=30天" Value="1"></asp:ListItem>
                        <asp:ListItem Text=">30天AND<=60天" Value="2"></asp:ListItem>
                        <asp:ListItem Text=">60天AND <=90天" Value="3"></asp:ListItem>
                        <asp:ListItem Text=">90天AND <=120天" Value="4"></asp:ListItem>
                        <asp:ListItem Text=">90天" Value="5"></asp:ListItem>
                        <asp:ListItem Text=">120天" Value="6"></asp:ListItem>
                        <asp:ListItem Text=">180天" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                &nbsp;&nbsp;&nbsp;&nbsp; 到款单金额:
                <asp:DropDownList ID="ddlDaoKuanTotal" runat="server">
                    <asp:ListItem Text=">" Value=">">
                    </asp:ListItem>
                    <asp:ListItem Text="<" Value="<">
                    </asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="=" Value="=">
                    </asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtDaoKuanTotal" runat="server"></asp:TextBox>
                到款类型：
                <asp:DropDownList ID="ddlBusType" runat="server" Enabled="false">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="0" Text="发票"></asp:ListItem>
                    <asp:ListItem Value="1" Text="预付款"></asp:ListItem>
                </asp:DropDownList>
                最长开票天数:
                    <asp:DropDownList ID="ddlFPDays" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<=30天" Value="1"></asp:ListItem>
                        <asp:ListItem Text=">30天AND<=60天" Value="2"></asp:ListItem>
                        <asp:ListItem Text=">60天AND <=90天" Value="3"></asp:ListItem>
                        <asp:ListItem Text=">90天AND <=120天" Value="4"></asp:ListItem>
                        <asp:ListItem Text=">120天AND <=180天" Value="5"></asp:ListItem>
                        <asp:ListItem Text=">180天" Value="6"></asp:ListItem>
                    </asp:DropDownList>

                未开票天数:
                    <asp:DropDownList ID="ddlWeiFPDays" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<=30天" Value="1"></asp:ListItem>
                        <asp:ListItem Text=">30天AND<=60天" Value="2"></asp:ListItem>
                        <asp:ListItem Text=">60天AND <=90天" Value="3"></asp:ListItem>
                        <asp:ListItem Text=">90天AND <=120天" Value="4"></asp:ListItem>
                        <asp:ListItem Text=">120天AND <=180天" Value="5"></asp:ListItem>
                        <asp:ListItem Text=">180天" Value="6"></asp:ListItem>
                    </asp:DropDownList>
                <br />
                项目关闭 :
                <asp:DropDownList ID="ddlPoClose" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="关闭"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未关闭"></asp:ListItem>
                </asp:DropDownList>
                项目选中：
                <asp:DropDownList ID="ddlIsSelect" runat="server" Width="70px">
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

                项目类别：
                  <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                  </asp:DropDownList>
                客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType">
                </asp:DropDownList>
                客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Style="left: 0px;">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;

                <br />
                <div style="display: inline">
                    <asp:DropDownList ID="ddlNoSpecial" runat="server">

                        <asp:ListItem Value="0">不含特殊</asp:ListItem>
                        <asp:ListItem Value="1">特殊</asp:ListItem>
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                    </asp:DropDownList>

                    <asp:CheckBox ID="cbHadJiaoFu" runat="server" Text="已交付" />
                    <asp:CheckBox ID="cbIsPoFax" runat="server" Text="不含税" AutoPostBack="True" OnCheckedChanged="cbIsPoFax_CheckedChanged" />
                    <asp:CheckBox ID="cbClose" runat="server" Text="未关闭" ForeColor="Red" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbHanShui" runat="server" Text="含税" AutoPostBack="True" OnCheckedChanged="cbHanShui_CheckedChanged" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="cbPoNoZero" runat="server" Text="项目不为0" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                    DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:Label ID="Label2" runat="server" Text="项目总金额："></asp:Label>
    <asp:Label ID="lblPOTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="到款总金额:"></asp:Label>
    <asp:Label ID="lblMess" runat="server" Text="0" Style="color: Red;"></asp:Label>
    <asp:Label ID="Label3" runat="server" Text="未到款总金额："></asp:Label>
    <asp:Label ID="lblWeiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>

    未到款比例:
     <asp:Label ID="lblWeiTotalBiLi" runat="server" Text="0" ForeColor="Red"></asp:Label>%

    <asp:Label ID="Label4" runat="server" Text="开票总金额："></asp:Label>
    <asp:Label ID="lblKaiPiaoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <br />
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Id" Width="160%" AllowPaging="True" AutoGenerateColumns="False"
            OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound"
            OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
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
                        <td>类型
                        </td>
                        <td>单据号
                        </td>
                        <td>申请人
                        </td>
                        <td>申请日期
                        </td>
                        <td>到款日期
                        </td>
                        <td>金额
                        </td>
                        <td>上期账期系数
                        </td>
                        <td>项目编码
                        </td>
                        <td>项目名称
                        </td>
                        <td>客户名称
                        </td>
                        <td>备注
                        </td>
                        <td>状态
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnReEdit" runat="server" CommandName="ReEdit" CommandArgument='<% #Eval("Id") %>'
                            OnClientClick='return confirm( "确定要重新提交此单据吗？") '>编辑</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目编号">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" Text='<% #Eval("PONo") %>'
                            CommandArgument='<% #Eval("PONo_Id")%>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="PoName" HeaderText="项目名称" SortExpression="PoName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MinPoDate" HeaderText="项目日期" SortExpression="MinPoDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="税">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsPoFax" runat="server" Checked='<% #Eval("IsPoFax") %>' Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="MinOutTime" HeaderText="出库日期" SortExpression="MinOutTime"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="BusTypeStr" HeaderText="类型" SortExpression="BusTypeStr"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FPDate" HeaderText="开票日期" SortExpression="FPDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                  <asp:TemplateField HeaderText="未开票天数">
                    <ItemTemplate>
                        <asp:Label ID="lblWeiFPDays" runat="server" Text='<% #Eval("WeiFPDays") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="开票天数">
                    <ItemTemplate>
                        <asp:Label ID="lblFPDays" runat="server" Text='<% #Eval("FPDays") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DaoKuanDate1" HeaderText="到款日期" SortExpression="DaoKuanDate1"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:n2}"/>
                <asp:BoundField DataField="Total" HeaderText="到款金额" SortExpression="Total" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:n2}"/>
                   <asp:BoundField DataField="DaoTotal" HeaderText="到款比例" SortExpression="DaoTotal" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="WeiDaoTotal" HeaderText="未到比例" SortExpression="WeiDaoTotal" ItemStyle-HorizontalAlign="Center" />

                <%--      <asp:BoundField DataField="Days" HeaderText="天数" SortExpression="Days" ItemStyle-HorizontalAlign="Center" />--%>

                <asp:TemplateField HeaderText="天数">
                    <ItemTemplate>
                        <asp:Label ID="lblDays" runat="server" Text='<% #Eval("Days") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField DataField="FPNo" HeaderText="发票号" SortExpression="FPNo" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="40%" ItemStyle-CssClass="item" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle ForeColor="Black" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%" CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle" CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
    项目金额合计:
    <asp:Label ID="lblAllPoTotal" runat="server" Text="0"></asp:Label>
    总到款金额:
    <asp:Label ID="lblALLInvoiceTotal" runat="server" Text="0"></asp:Label>
    总应收:
    <asp:Label ID="lblAllWeiTotal" runat="server" Text="0"></asp:Label>
    含税项目未开票金额:
    <asp:Label ID="lblhsWKpTotal" runat="server" Text="0"></asp:Label>
    <br />
    <asp:Label ID="lblFPDetail" runat="server" Text=""></asp:Label>
    <%
        DataTable dt = ViewState["fpList"] as DataTable;

        if (dt != null)
        {

            int rowCount = 0;
            if (dt.Rows.Count % 5 == 0)
            {
                rowCount = dt.Rows.Count / 5;
            }
            else
            {
                rowCount = dt.Rows.Count / 5 + 1;
            }

            int rows = dt.Rows.Count;
            int index = 0;
    %>
    <table border="1 solid ">

        <%
            for (int i = 0; i < rowCount; i++)
            {

        %>
        <tr>

            <%
                for (int j = 1; j <= 5; j++)
                {
                    //int index=(i+1) * j-1;
                    if (index < rows)
                    {
                        DataRow dr = dt.Rows[index];
                        index++;
            %>
            <td><%= string.Format("{0}：{1}-{2}",dr[0],dr[1],dr[2])%></td>

            <%
                }
                else
                {  %>


            <%

                    }
                }


            %>
        </tr>

        <%
            }

        %>
    </table>

    <%
        }

    %>
    <br />
    <asp:GridView ID="gvDetail" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="PoNo" HeaderText="项目编号" SortExpression="PoNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateUser" HeaderText="申请人" SortExpression="CreateUser"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AppleDate" HeaderText="申请日期" SortExpression="AppleDate"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="ZhangQi" HeaderText="账期系数" SortExpression="ZhangQi" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
</asp:Content>
