<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="WFOAInvoiceList.aspx.cs"
    Inherits="VAN_OA.KingdeeInvoice.WFOAInvoiceList" MasterPageFile="~/DefaultMaster.Master"
    Title="项目发票销帐" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                项目发票销帐
            </td>
        </tr>
        <tr>
            <td>
                项目日期:
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
                <asp:TextBox ID="txtGuestName" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                发票编号:
            </td>
            <td>
                <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox><asp:CheckBox ID="cbInvoiceNo"
                    runat="server" Text="无发票" />
                项目编号:
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
            </td>
            <td>
                发票金额:
            </td>
            <td>
                <asp:DropDownList ID="ddlInvTotal" runat="server">
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                    <asp:ListItem Value="<=">&lt;=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtInvoice" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
                项目名称:
                <asp:TextBox ID="txtPOName" runat="server"></asp:TextBox>
            </td>
            <td>
                是否销帐:
            </td>
            <td>
                <asp:DropDownList ID="ddlIsorder" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                  <asp:ListItem Value="0">未销帐</asp:ListItem>
                    <asp:ListItem Value="1">已销帐</asp:ListItem>
                </asp:DropDownList>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                &nbsp;
                <asp:Button ID="btnIsSelected" runat="server" Text="保存" BackColor="Yellow" OnClick="btnIsSelected_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AutoGenerateColumns="False" AllowPaging="true"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td height="25" align="center">
                        是否销帐
                    </td>
                    <td height="25" align="center">
                        项目编号
                    </td>
                    <td height="25" align="center">
                        项目日期
                    </td>
                    <td height="25" align="center">
                        项目名称
                    </td>
                    <td height="25" align="center">
                        客户名称
                    </td>
                    <td height="25" align="center">
                        AE
                    </td>
                    <td height="25" align="center">
                        金额
                    </td>
                    <td height="25" align="center">
                        开具日期
                    </td>
                    <td height="25" align="center">
                        金额
                    </td>
                    <tr>
                        <td colspan="4" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <Columns>
            <asp:TemplateField HeaderText="是否销帐 ">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbIsIsorder" runat="server" Text="是否销帐 " AutoPostBack="True"
                        OnCheckedChanged="cbIsSelected_CheckedChanged" Enabled="<%# IsSelectedEdit() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsIsorder" runat="server" Checked='<% #Eval("Isorder") %>' Enabled="<%# IsSelectedEdit() %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="是否销帐 ">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsIsorder1" runat="server" Checked='<% #Eval("Isorder") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
             
            <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate"
                ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:yyyy-MM-dd}"/>
            <asp:BoundField DataField="CreateName" HeaderText="AE" SortExpression="CreateName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="FPNo" HeaderText="发票号码" SortExpression="FPNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Total" HeaderText="金额" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateTime" HeaderText="开具日期" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
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
    <asp:Label ID="Label3" runat="server" Text="发票金额合计:"></asp:Label>
    <asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
</asp:Content>
