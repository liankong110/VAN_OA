﻿<%@ Page Language="C#" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeBehind="MyRequestEForms.aspx.cs"
    Inherits="VAN_OA.EFrom.MyRequestEForms" MasterPageFile="~/DefaultMaster.Master"
    Title="我申请的单据" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                我申请的单据
            </td>
        </tr>
        <tr>
            <td>
                审批类型
            </td>
            <td>
                <asp:DropDownList ID="ddlProType" runat="server" Width="200px" DataTextField="pro_Type"
                    DataValueField="pro_Id">
                </asp:DropDownList>
            </td>
            <td>
                申请时间:
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
        </tr>
        <tr>
            <td>
                状态
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="200px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>执行中</asp:ListItem>
                    <asp:ListItem>通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
            </td>
             <td>
                公司名称:
            </td>
            <td>
               <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                单据号:
            </td>
            <td>
                <asp:TextBox ID="txtNo" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                项目编码:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
         <td>
                审批时间:
            </td>
            <td>
                <asp:TextBox ID="txtSPForm" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtSPTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtSPForm">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton3"
                    Format="yyyy-MM-dd" TargetControlID="txtSPTo">
                </cc1:CalendarExtender>
            </td>
            <td colspan="2">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
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
                        查看
                    </td>
                    <td>
                        文件类型
                    </td>
                    <td>
                        单据号
                    </td>
                    <td>
                        创建人
                    </td>
                    <td>
                        创建时间
                    </td>
                    <td>
                        申请时间
                    </td>
                    <td>
                        等待审批人
                    </td>
                    <td>
                        状态
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
            <asp:TemplateField HeaderText=" 查看">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="编辑" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
            <asp:BoundField DataField="ProTyleName" HeaderText="文件类型">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="E_No" HeaderText="单据号">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="CreatePer_Name" HeaderText="创建人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="createTime" HeaderText="创建时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="appTime" HeaderText="申请时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
             <asp:BoundField DataField="maxDoTime" HeaderText="审批时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ToPer_Name" HeaderText="等待审批人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="state" HeaderText="状态" Visible="false">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <asp:Label ID="lblstate" runat="server" Text='<%# Eval("state") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="供应商简称" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblPONOS" runat="server" Text='<%# GetDemo(Eval("allE_id"),Eval("proId")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
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
