<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="UseCarDetailList.aspx.cs"
    Inherits="VAN_OA.ReportForms.UseCarDetailList" MasterPageFile="~/DefaultMaster.Master"
    Title="司机公里数统计" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="用车明细表">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td colspan="8" style="height: 20px; background-color: #336699; color: White;">司机公里数统计-个人油费明细
                        </td>
                    </tr>
                    <tr>
                        <td>申请日期:
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
                            <asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>申请人:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAppName" runat="server"></asp:TextBox>
                        </td>
                        <td>司机:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDriver" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>车牌号:
                        </td>
                        <td colspan="1">
                            <%-- <asp:TextBox ID="txtCarNo" runat="server"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlCarNo" runat="server" Width="200px" DataTextField="CarNO"
                                DataValueField="CarNO">
                            </asp:DropDownList>
                        </td>
                        <td>单据号:
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="txtProNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>使用日期:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUseFromTime" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            -<asp:TextBox ID="txtUseToTime" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton4" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtUseFromTime">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton5" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtUseToTime">
                            </cc1:CalendarExtender>
                        </td>
                        <td>项目编号:
                        </td>
                        <td colspan="1">
                            <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>公司名称:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                                Width="200PX">
                            </asp:DropDownList>
                        </td>
                        <td>AE:  
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="3">项目归类:
                            <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px">
                                <asp:ListItem Value="-1">全部</asp:ListItem>
                                <asp:ListItem Value="0">非特殊</asp:ListItem>
                                <asp:ListItem Value="1">特殊</asp:ListItem>
                            </asp:DropDownList>
                            项目关闭：
                            <asp:DropDownList ID="ddlClose" runat="server" Width="50px">
                                <asp:ListItem Value="-1">全部</asp:ListItem>
                                <asp:ListItem Value="0">未关闭</asp:ListItem>
                                <asp:ListItem Value="1">关闭</asp:ListItem>
                            </asp:DropDownList>
                            项目选中：
                            <asp:DropDownList ID="ddlIsSelect" runat="server" Width="50px">
                                <asp:ListItem Value="-1">全部</asp:ListItem>
                                <asp:ListItem Value="0">未选中</asp:ListItem>
                                <asp:ListItem Value="1">选中</asp:ListItem>
                            </asp:DropDownList>
                            结算选中：
                            <asp:DropDownList ID="ddlJieIsSelected" runat="server" Width="50px">
                                <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                                <asp:ListItem Value="1" Text="选中"></asp:ListItem>
                                <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                            </asp:DropDownList>
                            客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                                DataTextField="GuestType" Width="50px">
                            </asp:DropDownList>
                            客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                                DataTextField="GuestProString" Width="50px">
                            </asp:DropDownList></td>
                        <td>
                            <div align="right">
                                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="Label1" runat="server" Text="公里数(KM)统计:" Style="color: Red; font-size: 14px"></asp:Label>
                <asp:Label ID="lblTotal" runat="server" Text="0" Style="font-size: 14px"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="金额:"
                    Style="color: Red; font-size: 14px"></asp:Label>
                <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style="font-size: 14px"></asp:Label>
                <br />
                <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal" Height="100%">
                    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                        DataKeyNames="Id" Width="150%" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
                        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
                        <PagerTemplate>
                            <br />
                            <%--  <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
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
                                    <td>项目编号
                                    </td>
                                    <td>单据号
                                    </td>
                                    <td>日期
                                    </td>
                                    <td>时间
                                    </td>
                                    <td>公司名称
                                    </td>
                                    <td>申请人
                                    </td>
                                    <td>司机
                                    </td>
                                    <td>始(KM)
                                    </td>
                                    <td>终(KM)
                                    </td>
                                    <td>公里数(KM)
                                    </td>
                                    <td>金额
                                    </td>
                                    <td>车牌号
                                    </td>
                                    <td>乘车人
                                    </td>
                                    <td>备注
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" style="height: 80%">---暂无数据---
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="AppTime" HeaderText="日期" SortExpression="AppTime" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="GoEndTime" HeaderText="时间" SortExpression="GoEndTime"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Area" HeaderText="地址" SortExpression="Area" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="AppUserName" HeaderText="申请人" SortExpression="AppUserName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Driver" HeaderText="司机" SortExpression="Driver" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FromRoadLong" HeaderText="始(KM)" SortExpression="FromRoadLong"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ToRoadLong" HeaderText="终(KM)" SortExpression="ToRoadLong"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="RoadLong" HeaderText="公里数" SortExpression="RoadLong" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="TotalPrice" HeaderText="金额" SortExpression="TotalPrice"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="CarNo" HeaderText="车牌号" SortExpression="CarNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ByCarPers" HeaderText="乘车人" SortExpression="ByCarPers"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Left" />
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
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="私车公用申请单">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td colspan="8" style="height: 20px; background-color: #336699; color: White;">私车公用明细清单
                        </td>
                    </tr>
                    <tr>
                        <td>日期:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSiFrom" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            -<asp:TextBox ID="txtSiTo" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtSiFrom">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtSiTo">
                            </cc1:CalendarExtender>
                        </td>
                        <td>申请人:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSiApper" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>单据号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProNo1" runat="server"></asp:TextBox>
                        </td>
                        <td>项目编号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSecondPONo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <tr>
                            <td>公司名称:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="dllCompayList" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                                    Width="200PX">
                                </asp:DropDownList>AE
                             <asp:DropDownList ID="ddlAE" runat="server" DataTextField="LoginName" DataValueField="Id">
                             </asp:DropDownList>
                                <div align="right">
                                    <asp:Button ID="btnSiSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSiSelect_Click" />&nbsp;
                                </div>
                            </td>
                        </tr>
                </table>
                <br />
                <asp:Label ID="Label2" runat="server" Text="里程数(KM)统计:" Style="color: Red; font-size: 14px"></asp:Label>
                <asp:Label ID="lblSiTotal" runat="server" Text="0" Style="font-size: 14px"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="金额:" Style="color: Red; font-size: 14px"></asp:Label>
                <asp:Label ID="lblSiTotalPrice" runat="server" Text="0" Style="font-size: 14px"></asp:Label>
                <br />
                <asp:GridView ID="GvSi" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="GvSi_PageIndexChanging" OnRowDataBound="GvSi_RowDataBound">
                    <PagerTemplate>
                        <br />
                        <%--  <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
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
                                <td>项目编号
                                </td>
                                <td>单据号
                                </td>
                                <td>日期
                                </td>
                                <td>时间
                                </td>
                                <td>申请人
                                </td>
                                <td>乘车人
                                </td>
                                <td>里程数(KM)
                                </td>
                                <td>金额
                                </td>
                                <td>使用事由
                                </td>
                                <td>详细地址
                                </td>
                                <td>出发地
                                </td>
                                <td>到达地
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" align="center" style="height: 80%">---暂无数据---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="datetime" HeaderText="日期" SortExpression="datetime" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="GoEndTime" HeaderText="时间" SortExpression="GoEndTime"
                            ItemStyle-HorizontalAlign="Center" />
                          <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="LoginName" HeaderText="申请人" SortExpression="LoginName"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="pers_car" HeaderText="乘车人" SortExpression="pers_car" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="type" HeaderText="单/反" SortExpression="type" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="useReason" HeaderText="使用事由" SortExpression="useReason"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="roadLong" HeaderText="里程数" SortExpression="roadLong" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="TotalPrice" HeaderText="金额" SortExpression="TotalPrice"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="deAddress" HeaderText="详细地址" SortExpression="deAddress"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="goAddress" HeaderText="出发地" SortExpression="goAddress"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="toAddress" HeaderText="到达地" SortExpression="toAddress"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager2" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
                    PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
                    NextPageText="下页" OnPageChanged="AspNetPager2_PageChanged">
                </webdiyer:AspNetPager>
                <br />    <br />    <br />
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
