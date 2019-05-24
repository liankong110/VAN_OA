<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="CarMaintenanceList.aspx.cs"
    Inherits="VAN_OA.ReportForms.CarMaintenanceList" MasterPageFile="~/DefaultMaster.Master"
    Title="车辆保养记录管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel ID="TabPanel1" runat="server">
            <HeaderTemplate>
                汽车保养记录
            </HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                            车辆保养记录管理
                        </td>
                    </tr>
                    <tr>
                        <td>
                            车牌号
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCarNo" runat="server" Width="200px" DataTextField="CarNO"
                                DataValueField="CarNO">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            保养日期:
                        </td>
                        <td colspan="1">
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
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div align="right">
                                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                                <asp:Button ID="btnAdd" runat="server" Text="添加" BackColor="Yellow" OnClick="btnAdd_Click"
                                    Width="98px" Visible="false" /></div>
                        </td>
                    </tr>
                </table>
                <br>
                共计(金额):<asp:Label ID="lbltotal" runat="server" Text="0" Style="color: Red"></asp:Label>
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
                    OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
                    <PagerTemplate>
                        <br />
                        <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />--%>
                    </PagerTemplate>
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    单号
                                </td>
                                <td>
                                    车牌号
                                </td>
                                <td>
                                    保养时间
                                </td>
                                <td>
                                    公里数
                                </td>
                                <td>
                                    金额
                                </td>
                                <td>
                                    更换内容
                                </td>
                                <td>
                                    维修情况
                                </td>
                                <td>
                                    备注
                                </td>
                                <td>
                                    创建人
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
                        <asp:BoundField DataField="UseState" HeaderText="单号" SortExpression="UseState" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CardNo" HeaderText="车牌号" SortExpression="CardNo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="MaintenanceTime" HeaderText="加油时间" SortExpression="MaintenanceTime"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Distance" HeaderText="公里数" SortExpression="Distance" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Total" HeaderText="金额" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ReplaceRemark" HeaderText="更换内容" SortExpression="ReplaceRemark"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ReplaceStatus" HeaderText="维修情况" SortExpression="ReplaceStatus"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="UserName" HeaderText="创建人" SortExpression="UserName" ItemStyle-HorizontalAlign="Center" />
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
                <br />  <br />  <br />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server">
            <HeaderTemplate>
                汽车加油记录
            </HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                            车辆加油记录管理
                        </td>
                    </tr>
                    <tr>
                        <td>
                            车牌号
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOilCarNo" runat="server" Width="200px" DataTextField="CarNO"
                                DataValueField="CarNO">
                            </asp:DropDownList>
                        </td>
                        <td>
                            充值时间:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromChongZhi" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ibtnChongZhiFrom" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            -<asp:TextBox ID="txtToChongZhi" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="CalendarExtender5" PopupButtonID="ibtnChongZhiFrom" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtFromChongZhi">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton5" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtToChongZhi">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            加油日期:
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtOilFrom" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            -<asp:TextBox ID="txtOilTo" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtOilFrom">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                                Format="yyyy-MM-dd" TargetControlID="txtOilTo">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div align="right">
                                <asp:Button ID="btnOilSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnOilSelect_Click" />&nbsp;
                                <asp:Button ID="btnOilAdd" runat="server" Text="添加" BackColor="Yellow" OnClick="btnOilAdd_Click"
                                    Width="98px" /></div>
                        </td>
                    </tr>
                </table>
                <br>
                <asp:GridView ID="gvOil" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvOil_PageIndexChanging" OnRowDeleting="gvOil_RowDeleting"
                    OnRowEditing="gvOil_RowEditing" OnRowDataBound="gvOil_RowDataBound">
                    <PagerTemplate>
                        <br />
                        <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
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
                                    车牌号
                                </td>
                                <td>
                                    加油时间
                                </td>
                                <td>
                                    上期余额
                                </td>
                                <td>
                                    加油（元）
                                </td>
                                <td>
                                    充值时间
                                </td>
                                <td>
                                    充值（元）
                                </td>
                                <td>
                                    本期余额
                                </td>
                                <td>
                                    备注
                                </td>
                                <td>
                                    创建人
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
                        <asp:BoundField DataField="CardNo" HeaderText="车牌号" SortExpression="CardNo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="MaintenanceTime" HeaderText="加油时间" SortExpression="MaintenanceTime"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="UpTotal" HeaderText="上期余额" SortExpression="UpTotal" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="OilTotal" HeaderText="加油（元）" SortExpression="OilTotal"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ChongZhiDate" HeaderText="充值时间" SortExpression="ChongZhiDate"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AddTotal" HeaderText="充值（元）" SortExpression="AddTotal"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Total" HeaderText="本期余额" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="UserName" HeaderText="创建人" SortExpression="UserName" ItemStyle-HorizontalAlign="Center" />
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
                  <br />  <br />  <br />
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
