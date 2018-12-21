<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KPI_Sell.aspx.cs"
    Inherits="VAN_OA.JXC.KPI_Sell" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="销售KPI 量化指标" %>

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

            var url = "../JXC/JXC_REPORTList.aspx?PONo=" + PONO + "&IsSpecial=true";  //+ document.getElementById('<%= cbIsSpecial.ClientID %>').value;

            window.open(url, 'newwindow')
        }
    </script>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">销售KPI 量化指标
            </td>
        </tr>
        <tr>
            <td>项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
            </td>
            <td>项目名称:
            </td>
            <td>
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                    DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
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
            <td>不含特殊
            </td>
            <td>
                <asp:CheckBox ID="cbIsSpecial" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
            <td colspan="2">客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                DataTextField="GuestType">
            </asp:DropDownList>
                客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Style="left: 0px;">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td colspan="2">项目金额
                <asp:DropDownList ID="ddlFuHao" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                销售金额&nbsp;&nbsp;&nbsp;&nbsp; 项目金额
                <asp:DropDownList ID="ddlPOFaTotal" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                发票金额 项目金额
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
            <td colspan="4">项目关闭：
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
                项目类别：
                <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                    <%-- <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1">零售</asp:ListItem>
                    <asp:ListItem Value="2">工程</asp:ListItem>--%>
                </asp:DropDownList>
                实际帐期:
                <asp:DropDownList ID="ddlTrueZhangQI" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<=30天" Value="1"></asp:ListItem>
                    <asp:ListItem Text="30天<实际帐期<=60天" Value="2"></asp:ListItem>
                    <asp:ListItem Text="60天<实际帐期<=90天" Value="3"></asp:ListItem>
                    <asp:ListItem Text="90天<实际帐期<=120天" Value="4"></asp:ListItem>
                    <asp:ListItem Text="实际帐期>120天" Value="5"></asp:ListItem>
                </asp:DropDownList>
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
                <br />

                <table style="border: 0px; margin: 0px;">
                    <tr>
                        <td>
                            考核帐期:<asp:DropDownList ID="ddlZhangQI" runat="server">
                                <asp:ListItem Text="实际帐期 >帐期截止期 " Value="2"></asp:ListItem>
                                <asp:ListItem Text="实际帐期>=帐期截止期" Value="1"></asp:ListItem>

                                <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <font style="color: Red;">[考核]</font>项目类别：
                        </td>
                        <td>
                            <asp:CheckBox ID="cbKaoAll" runat="server" Text="全部" AutoPostBack="true" OnCheckedChanged="cbKaoAll_CheckedChanged" />
                        </td>
                        <td>
                            <asp:CheckBoxList ID="cbKaoList" CssClass="ckblstEffect" runat="server" DataTextField="BasePoType"
                                DataValueField="Id" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br />
    <asp:Label ID="lblDateMess" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="lblProjectInfo" runat="server" Text=""></asp:Label>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid" Width="100%"
            AllowPaging="false" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound" OnRowCommand="gvMain_RowCommand">
            <PagerTemplate>
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>AE
                        </td>
                        <td>超期项目数
                        </td>
                        <td>新客户拜访数
                        </td>
                        <td>老客户拜访次数
                        </td>
                        <td>项目金额
                        
                        <td>销售总金额
                        </td>
                        <td>到账总金额
                        </td>
                        <td>项目总利润
                        </td>
                        <td>实际总利润
                        </td>
                        <td>开票率%
                        </td>
                        <td>到款率%
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>

                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
             
                <asp:BoundField DataField="TimeOutCount" HeaderText="超期项目数" SortExpression="TimeOutCount"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="NewContractCount" HeaderText="新客户拜访数" SortExpression="NewContractCount"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="OldContractCount" HeaderText="老客户拜访次数" SortExpression="OldContractCount"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="POTotal" HeaderText="项目金额" DataFormatString="{0:n2}"
                    SortExpression="POTotal" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SellTotal" HeaderText="销售总金额" DataFormatString="{0:n2}"
                    SortExpression="SellTotal" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="InvoiceTotal" HeaderText="到账总金额" DataFormatString="{0:n2}"
                    SortExpression="InvoiceTotal" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ProfitTotal" HeaderText="项目总利润" DataFormatString="{0:n2}"
                    SortExpression="ProfitTotal" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="LastProfitTotal" HeaderText="实际总利润" SortExpression="LastProfitTotal"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="KP_Percent" HeaderText="开票率%" SortExpression="KP_Percent"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DK_Percent" HeaderText="到款率%" SortExpression="DK_Percent"
                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right">
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
        <br />
        <%--   <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>--%>
    </asp:Panel>

</asp:Content>
