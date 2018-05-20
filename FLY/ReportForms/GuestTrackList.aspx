<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="GuestTrackList.aspx.cs"
    Inherits="VAN_OA.ReportForms.GuestTrackList" MasterPageFile="~/DefaultMaster.Master"
    Title="客户联系跟踪表管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel ID="TabPanel1" runat="server">
            <HeaderTemplate>
                客户信息</HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                            客户联系跟踪表管理
                        </td>
                    </tr>
                    <tr>
                        <td>
                            登记日期:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="Image1"
                                Format="yyyy-MM-dd" TargetControlID="txtFrom">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton1"
                                Format="yyyy-MM-dd" TargetControlID="txtTo">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            客户名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtGuestName" runat="server" Width="300PX"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            AE:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAE" runat="server" DataTextField="LoginName" DataValueField="Id"
                                Width="300PX">
                            </asp:DropDownList>
                        </td>
                        <td>
                            INSIDE:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlINSIDE" runat="server" DataTextField="LoginName" DataValueField="Id"
                                Width="300PX">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            单据号:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProNo" runat="server" Width="300PX"></asp:TextBox>
                        </td>
                        <td>
                            季度:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSelectYears" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlJidu" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div align="right">
                                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                                <asp:Button ID="btnAdd" runat="server" Text="添加" BackColor="Yellow" OnClick="btnAdd_Click"
                                    Width="98px" Visible="false" /></div>
                        </td>
                    </tr>
                </table>
                <br>
                <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal" Height="100%">
                    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                        DataKeyNames="Id" Width="250%" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
                        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
                        <PagerTemplate>
                            <br />
                            <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
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
                                        编辑
                                    </td>
                                    <td>
                                        删除
                                    </td>
                                    <td>
                                        AE
                                    </td>
                                    <td>
                                        %
                                    </td>
                                    <td>
                                        INSIDE
                                    </td>
                                    <td>
                                        %
                                    </td>
                                    <td>
                                        单据号
                                    </td>
                                    <td>
                                        日期
                                    </td>
                                    <td>
                                        客户名称
                                    </td>
                                    <td>
                                        客户类型
                                    </td>
                                    <td>
                                        客户属性
                                    </td>
                                    <td>
                                        电话/手机
                                    </td>
                                    <td>
                                        联系人
                                    </td>
                                    <td>
                                        职务
                                    </td>
                                    <td>
                                        传真或邮箱
                                    </td>
                                    <td>
                                        是否留资料
                                    </td>
                                    <td>
                                        QQ/MSN联系
                                    </td>
                                    <td>
                                        客户ID
                                    </td>
                                    <td>
                                        联系人地址
                                    </td>
                                    <td>
                                        联系人网址
                                    </td>
                                    <td>
                                        客户税务登记号
                                    </td>
                                    <td>
                                        客户工商注册号
                                    </td>
                                    <td>
                                        银行账号
                                    </td>
                                    <td>
                                        开户行
                                    </td>
                                    <td>
                                        上季度销售额
                                    </td>
                                    <td>
                                        上季度利润
                                    </td>
                                    <td>
                                        上季度收款期/天
                                    </td>
                                    <td>
                                        年
                                    </td>
                                    <td>
                                        季度
                                    </td>
                                    <td>
                                        备注
                                    </td>
                                    <td>
                                        INSIDE备注
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
                            <asp:BoundField DataField="AEName" HeaderText="AE" SortExpression="AEName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="AEPer" HeaderText="%" SortExpression="AEPer" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="INSIDEName" HeaderText="INSIDE" SortExpression="INSIDEName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="INSIDEPer" HeaderText="%" SortExpression="INSIDEPer" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Time" HeaderText="登记日期" SortExpression="Time" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                                ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="SimpGuestName" HeaderText="简称" SortExpression="SimpGuestName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MyGuestType" HeaderText="客户类型" SortExpression="MyGuestType"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MyGuestProString" HeaderText="客户属性" SortExpression="MyGuestProString"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Phone" HeaderText="电话/手机" SortExpression="Phone" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="LikeMan" HeaderText="联系人" SortExpression="LikeMan" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Job" HeaderText="职务" SortExpression="Job" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FoxOrEmail" HeaderText="传真或邮箱" SortExpression="FoxOrEmail"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Save_Name" HeaderText="是否留资料" SortExpression="Save_Name"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="QQMsn" HeaderText="QQ/MSN联系" SortExpression="QQMsn" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestId" HeaderText="客户ID" SortExpression="GuestId" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestAddress" HeaderText="联系人地址" SortExpression="GuestAddress"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestHttp" HeaderText="联系人网址" SortExpression="GuestHttp"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestShui" HeaderText="客户税务登记号" SortExpression="GuestShui"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestGong" HeaderText="客户工商注册号" SortExpression="GuestGong"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestBrandNo" HeaderText="银行账号" SortExpression="GuestBrandNo"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestBrandName" HeaderText="开户行" SortExpression="GuestBrandName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestTotal" HeaderText="上季度销售额" SortExpression="GuestTotal"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestLiRun" HeaderText="上季度利润" SortExpression="GuestLiRun"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestDays" HeaderText="上季度收款期/天" SortExpression="GuestDays"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="YearNo" HeaderText="年" SortExpression="YearNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="QuartNo" HeaderText="季度" SortExpression="QuartNo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="INSIDERemark" HeaderText="INSIDE备注" SortExpression="INSIDERemark"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime"
                                ItemStyle-HorizontalAlign="Center" />
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
        <cc1:TabPanel ID="TabPanel2" runat="server">
            <HeaderTemplate>
                新客户信息</HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                            客户联系跟踪表管理
                        </td>
                    </tr>
                    <tr>
                        <td>
                            日期:
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldDateFrom" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            -<asp:TextBox ID="txtOldTo" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton2"
                                Format="yyyy-MM-dd" TargetControlID="txtOldDateFrom">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton3"
                                Format="yyyy-MM-dd" TargetControlID="txtOldTo">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            客户名称
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldGuestName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            制单人:
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div align="right">
                                <asp:Button ID="btnOldSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnOldSelect_Click" />&nbsp;
                            </div>
                        </td>
                    </tr>
                </table>
                <br>
                <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal" Height="100%">
                    <asp:GridView ID="gvOld" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                        DataKeyNames="Id" Width="200%" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvOld_PageIndexChanging" OnRowDataBound="gvOld_RowDataBound">
                        <PagerTemplate>
                            <br />
                            <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
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
                                        日期
                                    </td>
                                    <td>
                                        客户名称
                                    </td>
                                    <td>
                                        电话/手机
                                    </td>
                                    <td>
                                        联系人
                                    </td>
                                    <td>
                                        职务
                                    </td>
                                    <td>
                                        传真或邮箱
                                    </td>
                                    <td>
                                        是否留资料
                                    </td>
                                    <td>
                                        QQ/MSN联系
                                    </td>
                                    <td>
                                        1次约见
                                    </td>
                                    <td>
                                        2次约见
                                    </td>
                                    <td>
                                        面访
                                    </td>
                                    <td>
                                        询价
                                    </td>
                                    <td>
                                        成单
                                    </td>
                                    <td>
                                        自我评价
                                    </td>
                                    <td>
                                        主管评价
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
                            <asp:BoundField DataField="UserName" HeaderText="创建人" SortExpression="UserName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Time" HeaderText="日期" SortExpression="Time" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Phone" HeaderText="电话/手机" SortExpression="Phone" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="LikeMan" HeaderText="联系人" SortExpression="LikeMan" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Job" HeaderText="职务" SortExpression="Job" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FoxOrEmail" HeaderText="传真或邮箱" SortExpression="FoxOrEmail"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Save_Name" HeaderText="是否留资料" SortExpression="Save_Name"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="QQMsn" HeaderText="QQ/MSN联系" SortExpression="QQMsn" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FristMeet" HeaderText="1次约见" SortExpression="FristMeet"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SecondMeet" HeaderText="2次约见" SortExpression="SecondMeet"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="FaceMeet" HeaderText="面访" SortExpression="FaceMeet" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Price" HeaderText="询价" SortExpression="Price" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Success_Name" HeaderText="成单" SortExpression="Success_Name"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MyAppraise" HeaderText="自我评价" SortExpression="MyAppraise"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ManAppraise" HeaderText="主管评价" SortExpression="ManAppraise"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime"
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
                        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
                    </webdiyer:AspNetPager>
                </asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server">
            <HeaderTemplate>
                复制</HeaderTemplate>
            <ContentTemplate>
                原季度:
                <asp:DropDownList ID="ddlYears" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlOrl" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
                目标季度:
                <asp:DropDownList ID="ddlNextYears" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlBar" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnCopy" runat="server" Text="复制" OnClick="btnCopy_Click" BackColor="Yellow" />
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
