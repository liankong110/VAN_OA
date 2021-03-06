﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFElectronicInvoice.aspx.cs" Inherits="VAN_OA.JXC.WFElectronicInvoice" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="电子票据A" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
     
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">电子票据A
            </td>
        </tr>
        <tr>
            <td>项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
            <td>支/预单据号:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>项目日期:
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
            <td>AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>供应商全称:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtSupplierName" runat="server"></asp:TextBox>
                <asp:CheckBox ID="cbSupplierName" runat="server"  Text="匹配"/>
                供应商简称:
                <asp:TextBox ID="txtSupplierSimpName" runat="server"></asp:TextBox>
                <asp:CheckBox ID="cbSupplierSimpName" runat="server"  Text="匹配"/>
            </td>
            <td>支付类型:
            </td>
            <td>
                <asp:DropDownList ID="ddlPayType" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="预" Text="供应商预付款单"></asp:ListItem>
                    <asp:ListItem Value="支" Text="供应商付款单"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td colspan="4">
                  <asp:TextBox ID="txtSmallTotal" runat="server" Width="100PX"></asp:TextBox>
                 <asp:DropDownList ID="ddlLeftTotal" runat="server">
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                </asp:DropDownList>
                单笔金额
                <asp:DropDownList ID="ddlTotal" runat="server">
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtBigTotal" runat="server" Width="100PX"></asp:TextBox>
                供应商特性 ：
                  <asp:DropDownList ID="ddlPeculiarity" runat="server">
                    <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                     <asp:ListItem Text="厂家" Value="厂家"></asp:ListItem>
                     <asp:ListItem Text="代理商" Value="代理商"></asp:ListItem>
                      <asp:ListItem Text="总代理" Value="总代理"></asp:ListItem>
                       <asp:ListItem Text="个人" Value="个人"></asp:ListItem>
                </asp:DropDownList>

                票号：<asp:TextBox ID="txtPFNo" runat="server" Width="100PX"></asp:TextBox>
             <%--   形式票号：<asp:TextBox ID="txtNotPFNo" runat="server" Width="100PX"></asp:TextBox>--%>
                开户行：<asp:TextBox ID="txtSupplierBrandName" runat="server" Width="100PX"></asp:TextBox>
                省份：   <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlCity" runat="server">
                </asp:DropDownList>                
                
                </td>
            
        </tr>
        <tr>
            <td colspan="4"> <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnYuLan" runat="server" Text=" 合并预览 " BackColor="Yellow" OnClick="btnYuLan_Click" Enabled="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btbPrint" runat="server" Text=" 合并打印进账单 " BackColor="Yellow" OnClick="btbPrint_Click" Enabled="false"/>&nbsp;&nbsp;&nbsp;&nbsp;
                </div></td>
        </tr>
    </table>

    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
         Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound"
        OnRowCommand="gvMain_RowCommand" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" >
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>项目编号
                    </td>
                    <td>支/预单据号
                    </td>
                    <td>支付类型
                    </td>
                    <td>供应商全称
                    </td>
                    <td>开户行
                    </td>
                    <td>银行账户
                    </td>
                    <td>省份
                    </td>
                    <td>城市
                    </td>
                    <td>金额
                    </td>
                    <td>票据类型
                    </td>
                    <td>财务票据人员
                    </td>
                    <td>用途
                    </td>
                    <td>公司名称
                    </td>
                </tr>
                <tr>
                    <td colspan="13" align="center" style="height: 80%">---暂无数据---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
             <asp:TemplateField HeaderText="预览">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit" 
                        AlternateText="预览" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="打印进账单">
                <ItemTemplate>
                    <asp:ImageButton ID="btnJinZhangDan" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Delete"
                        AlternateText="打印进账单" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="项目编码" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:Label ID="PONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProNo" HeaderText="支/预单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
             <asp:BoundField DataField="PFNo" HeaderText="票据号" SortExpression="PFNo" ItemStyle-HorizontalAlign="Center"  ItemStyle-ForeColor="Red" HeaderStyle-Font-Bold="true">  
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="busType" HeaderText="类型" SortExpression="busType" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="SupplierName" HeaderText="供应商全称" ItemStyle-Width="150" SortExpression="SupplierName" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
               <asp:BoundField DataField="SupplieSimpeName" HeaderText="供应商简称" SortExpression="SupplieSimpeName" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
               <asp:BoundField DataField="Peculiarity" HeaderText="供应商特性" SortExpression="Peculiarity" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="100px">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="SupplierBrandName" HeaderText="开户行" ItemStyle-Width="120" SortExpression="SupplierBrandName" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="SupplierBrandNo" HeaderText="银行账户" ItemStyle-Width="120" SortExpression="SupplierBrandNo" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Province" HeaderText="省份" SortExpression="Province" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="城市" SortExpression="City" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ActPay" HeaderText="金额" SortExpression="ActPay" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>

            <asp:TemplateField HeaderText="票据类型" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:DropDownList ID="dllBillType" runat="server" DataValueField="Id" DataTextField="BillName">
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="财务票据人员" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:DropDownList ID="dllPerson" runat="server" DataTextField="Name"
                        DataValueField="Id">
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="用途" ItemStyle-Width="70px">
                <ItemTemplate>
                    <asp:DropDownList ID="dllUse" runat="server">
                        <asp:ListItem>货款</asp:ListItem>
                        <asp:ListItem>备用金</asp:ListItem>
                        <asp:ListItem>差旅费</asp:ListItem>
                        <asp:ListItem>还款</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="公司名称">
                <ItemTemplate>
                    <asp:DropDownList ID="dllCompany" runat="server" >
                        <asp:ListItem>苏州万邦电脑系统有限公司</asp:ListItem>
                        <asp:ListItem>苏州工业园区万邦科技有限公司</asp:ListItem>
                        <asp:ListItem>苏州源达万维智能科技有限公司</asp:ListItem>
                        <asp:ListItem>苏州易佳通网络科技有限公司</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
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
