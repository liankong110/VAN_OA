<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sell_TuiSunCha_ReportTotalList.aspx.cs"
    Inherits="VAN_OA.JXC.Sell_TuiSunCha_ReportTotalList" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="销售退货损差表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

    <script type="text/javascript">
     function GetDetail(PONO) {

         var url = "../JXC/Sell_TuiSunCha_ReportList.aspx?PONo=" + PONO + "&IsSpecial=true";  //+ document.getElementById('<%= cbIsSpecial.ClientID %>').value;

         window.open(url, 'newwindow')
     }
    </script>

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                销售退货损差表
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
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX">
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
                <asp:TextBox ID="txtGuestName" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>
                不含特殊
            </td>
            <td>
                <asp:CheckBox ID="cbIsSpecial" runat="server" Checked="true" />
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
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
                        项目日期
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td>
                        销售额
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
                        AE
                    </td>
                    <td>
                        INSIDE
                    </td>
                    <td>
                        AE比例%
                    </td>
                    <td>
                        INSIDE比例%
                    </td>
                    <td>
                        AE金额
                    </td>
                    <td>
                        INSIDE金额
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
            <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Left">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="goodSellTotal" HeaderText="销售额" DataFormatString="{0:n0}"
                SortExpression="goodSellTotal" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="goodTotal" HeaderText="总成本" SortExpression="goodTotal"
                DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="maoliTotal" HeaderText="项目净利润" SortExpression="maoliTotal"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="TrueLiRun" HeaderText="实际利润" SortExpression="TrueLiRun"
                DataFormatString="{0:n0}" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="INSIDE" HeaderText="INSIDE" SortExpression="INSIDE" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AEPer" HeaderText="AE比例%" SortExpression="AEPer" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="INSIDEPer" HeaderText="INSIDE比例%" SortExpression="INSIDEPer"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AETotal" HeaderText="AE金额" SortExpression="AETotal" DataFormatString="{0:n0}"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="InsidTotal" HeaderText="INSIDE金额" DataFormatString="{0:n0}"
                SortExpression="InsidTotal" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
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
</asp:Content>
