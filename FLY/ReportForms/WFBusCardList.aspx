<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="WFBusCardList.aspx.cs"
    Inherits="VAN_OA.ReportForms.WFBusCardList" MasterPageFile="~/DefaultMaster.Master"
    Title="公交卡充值/使用记录" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="System.Web.UI" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel ID="TabPanel1" runat="server">
            <HeaderTemplate>
                公交卡充值记录
            </HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td>日期:
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
                        <td>公交卡号
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCardNo" runat="server" Width="200px" DataTextField="CardNo"
                                DataValueField="CardNo">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div align="right">
                                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                                <asp:Button ID="btnAdd" runat="server" Text="添加" BackColor="Yellow" OnClick="btnAdd_Click"
                                    Width="98px" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br>
                <asp:GridView ID="gvCardRecordList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Id" Width="100%" AllowPaging="true" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
                    OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound" PageSize="20">
                    <PagerTemplate>
                        <br />
                    </PagerTemplate>
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>编辑
                                </td>
                                <td>删除
                                </td>
                                <td>公交卡号
                                </td>
                                <td>充值日期
                                </td>
                                <td>金额
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
                        <asp:TemplateField HeaderText=" 编辑">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                                    AlternateText="编辑" />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                                    CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="BusCardNo" HeaderText="公交卡号" SortExpression="BusCardNo"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BusCardDate" HeaderText="充值日期" SortExpression="BusCardDate"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="BusCardTotal" HeaderText="金额" SortExpression="BusCardTotal"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BusCardRemark" HeaderText="备注" SortExpression="BusCardRemark"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                </asp:GridView>

                <br />

                <webdiyer:AspNetPager ID="AspNetPager2" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
                    PageSize="20" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
                    NextPageText="下页" OnPageChanged="AspNetPager2_PageChanged">
                </webdiyer:AspNetPager>
                <br />
                <br />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server">
            <HeaderTemplate>
                公交卡使用记录
            </HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td>日期:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromUse" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            -<asp:TextBox ID="txtToUse" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton2" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtFromUse">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender7" PopupButtonID="ImageButton3" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtToUse">
                            </cc1:CalendarExtender>
                        </td>
                        <td>公交卡号
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCardNo1" runat="server" Width="200px" DataTextField="CardNo"
                                DataValueField="CardNo">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>项目编号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                            AE：<asp:DropDownList ID="ddlAE" runat="server" DataTextField="LoginName" DataValueField="Id"
                                Width="200PX">
                            </asp:DropDownList>
                        </td>
                        <td>客户名称 :
                        </td>
                        <td>
                            <asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">项目归类:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                </asp:DropDownList>
                            项目关闭：
                <asp:DropDownList ID="ddlClose" runat="server">
                    <asp:ListItem Text="全部" Value="-1"> </asp:ListItem>
                    <asp:ListItem Text="关闭" Value="1"> </asp:ListItem>
                    <asp:ListItem Text="未关闭" Value="0"> </asp:ListItem>
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
                                DataTextField="GuestType">
                            </asp:DropDownList>
                            客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                                DataTextField="GuestProString" Style="left: 0px;">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div align="right">
                                <asp:Button ID="btnSelect1" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect1_Click" />&nbsp;
                              <%--  <asp:Button ID="btnAdd1" runat="server" Text="添加" BackColor="Yellow" OnClick="btnAdd1_Click"
                                    Width="98px" />
                                    <asp:Button ID="btnDo" runat="server" Text=" 修复数据 " BackColor="Yellow" OnClick="btnDo_Click" />&nbsp;--%>
                            </div>
                        </td>
                    </tr>
                </table>
                <br>
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="100%">
                    <asp:GridView ID="gvCardNOUse" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                        DataKeyNames="Id" Width="130%" AllowPaging="True" AutoGenerateColumns="False"
                        OnRowDeleting="gvCardNOUse_RowDeleting" OnRowEditing="gvCardNOUse_RowEditing"
                        OnRowDataBound="gvCardNOUse_RowDataBound" OnPageIndexChanging="gvCardNOUse_PageIndexChanging">
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
                                    <td>编辑
                                    </td>
                                    <td>删除
                                    </td>
                                    <td>公交卡号
                                    </td>
                                    <td>日期
                                    </td>
                                    <td>地点
                                    </td>
                                    <td>客户人员
                                    </td>
                                    <td>金额
                                    </td>
                                    <td>项目编码
                                    </td>
                                    <td>项目名称
                                    </td>
                                    <td>客户名称
                                    </td>
                                    <td>备注
                                    </td>
                                    <td>创建人
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" style="height: 80%">---暂无数据---
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText=" 编辑">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                                        AlternateText="编辑" />
                                </ItemTemplate>
                                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                                        CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                                </ItemTemplate>
                                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="BusCardNo" HeaderText="公交卡号" SortExpression="BusCardNo"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="BusCardDate" HeaderText="日期" SortExpression="BusCardDate"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="Address" HeaderText="地点" SortExpression="Address" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="UseName" HeaderText="使用人" SortExpression="UseName" ItemStyle-HorizontalAlign="Center" />

                            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestName" HeaderText="客户人员" SortExpression="GuestName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="UseTotal" HeaderText="金额" SortExpression="UseTotal" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="POGuestName" HeaderText="客户名称" SortExpression="POGuestName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="BusUseRemark" HeaderText="备注" SortExpression="BusUseRemark"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="CreateUserName" HeaderText="创建人" SortExpression="CreateUserName"
                                ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                            HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                        <RowStyle CssClass="InfoDetail1" />
                    </asp:GridView>
                    <br />
                    合计:<asp:Label ID="lblTotal" runat="server" Text="0" Style="color: Red;"></asp:Label>
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
    </cc1:TabContainer>
</asp:Content>
