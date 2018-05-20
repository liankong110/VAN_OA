<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="OverTimeList.aspx.cs"
    Inherits="VAN_OA.ReportForms.OverTimeList" MasterPageFile="~/DefaultMaster.Master"
    Title="加班记录管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                加班记录管理
            </td>
        </tr>
        <tr>
            <td>
                日期:
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
                AE:
            </td>
            <td>
                <%--<asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>--%>
                 <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                申请人:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtAppName" runat="server"></asp:TextBox>
            </td>
            <td>
                单据号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtProNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                项目单号：
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
               
            </td>
             <td>
                公司名称：
            </td>
            <td>
                  <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX"></asp:DropDownList>
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
    <br />
    <div>
        共计:<asp:Label ID="lblTotalHour" runat="server" Text="0" Style="color: Red"></asp:Label>小时
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        共计(金额):<asp:Label ID="lbltotal" runat="server" Text="0" Style="color: Red"></asp:Label></div>
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
                        单据号
                    </td>
                    <td>
                        日期(从)
                    </td>
                    <td>
                        日期(到)
                    </td>
                    <td>
                        加班/调休人
                    </td>
                    <td>
                        小时
                    </td>
                    <td>
                        地点
                    </td>
                    <td>
                        加班/调休原因
                    </td>
                    <td>
                        AE
                    </td>
                    <td>
                        随行人
                    </td>
                    <td>
                        类型
                    </td>
                    <td>
                        金额
                    </td>
                    <td>
                        备注
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
            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="formTime" HeaderText="日期(从)" SortExpression="formTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="toTime" HeaderText="日期(到)" SortExpression="toTime" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="LoginName" HeaderText="加班人" SortExpression="LoginName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BetweenHours" HeaderText="小时" SortExpression="BetweenHours"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Address" HeaderText="地点" SortExpression="Address" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="reason" HeaderText="加班原因" SortExpression="reason" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="guestDai" HeaderText="AE" SortExpression="guestDai"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="SuixingRen" HeaderText="随行人" SortExpression="SuixingRen"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Total" HeaderText="金额" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POGuestName" HeaderText="客户名称" SortExpression="POGuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Reamrk" HeaderText="备注" SortExpression="Reamrk" ItemStyle-HorizontalAlign="Center" />
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
