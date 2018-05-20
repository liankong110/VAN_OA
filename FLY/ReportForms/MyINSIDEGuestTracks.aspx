<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="MyINSIDEGuestTracks.aspx.cs"
    Inherits="VAN_OA.ReportForms.MyINSIDEGuestTracks" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="INSIDE客户联系跟踪表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                INSIDE客户联系跟踪表
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
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                单据号:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" Width="300PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                客户名称
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="300PX"></asp:TextBox>
            </td>
            <td>
                AE
            </td>
            <td>
                <asp:DropDownList ID="ddlAE" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="300PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
        <PagerTemplate>
            <br />
            <%--  <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
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
                        AE
                    </td>
                    <td>
                        %
                    </td>
                    <td>
                        单据号
                    </td>
                    <td>
                        登记日期
                    </td>
                    <td>
                        客户名称
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
                        上季度INSIDE备注
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
            <asp:BoundField DataField="AEName" HeaderText="AE" SortExpression="AEName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AEPer" HeaderText="%" SortExpression="AEPer" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Time" HeaderText="登记日期" SortExpression="Time" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="SimpGuestName" HeaderText="简称" SortExpression="SimpGuestName"
                                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestTotal" HeaderText="上季度销售额" SortExpression="GuestTotal"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestLiRun" HeaderText="上季度利润" SortExpression="GuestLiRun"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestDays" HeaderText="上季度收款期/天" SortExpression="GuestDays"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="INSIDERemark" HeaderText="INSIDE备注" SortExpression="INSIDERemark"
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
</asp:Content>
