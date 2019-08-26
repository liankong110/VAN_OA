<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyEFormsTodo_New.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.MyEFormsTodo_New" MasterPageFile="~/DefaultMaster.Master"
    Title="我要审批的单据" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style type="text/css">
        .item {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">我要审批的单据
            </td>
        </tr>
        <tr>
            <td>审批类型
            </td>
            <td>
                <asp:DropDownList ID="ddlProType" runat="server" Width="200px" DataTextField="pro_Type"
                    DataValueField="pro_Id">
                </asp:DropDownList>
            </td>
            <td>申请时间:
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
            <td>申请人:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtApper" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>委托人:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtWeiTuo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>单据号:
            </td>
            <td>
                <asp:TextBox ID="txtNo" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>项目编码:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
                AE：
                <asp:DropDownList ID="ddlAEUsers" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>公司名称:
            </td>
            <td colspan="3">
                <div style="display: inline">
                    <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                        DataValueField="ComSimpName"
                        Width="200PX">
                    </asp:DropDownList>
                    审批时间:
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
                </div>
                <div align="right" style="display: inline; float: right;">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                </div>
            </td>
        </tr>

        <tr>
            <td colspan="4">项目归类:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                </asp:DropDownList>
                项目含税：
                <asp:DropDownList ID="ddlHanShui" runat="server" Width="50px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">含税</asp:ListItem>
                    <asp:ListItem Value="0">不含税</asp:ListItem>
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
                项目金额
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text=">0"></asp:ListItem>
                    <asp:ListItem Value="0" Text="=0"></asp:ListItem>
                </asp:DropDownList>
                供应商简称:<asp:TextBox ID="txtPOSupplier" runat="server"></asp:TextBox>
                <asp:CheckBox ID="cbPiPei" runat="server" Text="全匹配" />
            </td>
        </tr>
    </table>
    <br />

    <cc1:TabContainer ID="TabContainer1" runat="server" Font-Size="12px" style="font-family:Tahoma, Arial, sans-serif;">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="常用" style="font-family:Tahoma, Arial, sans-serif;">
            <ContentTemplate>
                    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AllowPaging="false" AutoGenerateColumns="False"
        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound"  Font-Size="12px" style="font-family:Tahoma, Arial, sans-serif;">
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
                    <td>查看
                    </td>
                    <td>文件类型
                    </td>
                    <td>单据号
                    </td>
                    <td>创建人
                    </td>
                    <td>创建时间
                    </td>
                    <td>申请人
                    </td>
                    <td>申请时间
                    </td>
                    <td>委托人
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">---暂无数据---
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
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
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
            <asp:BoundField DataField="AppPer_Name" HeaderText="申请人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="appTime" HeaderText="申请时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="maxDoTime" HeaderText="审批时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ToPer_Name" HeaderText="委托人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="E_Remark" HeaderText="备注">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="供应商简称" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目编号" ItemStyle-CssClass="item" ItemStyle-Width="20%"
                ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblE_Remark" runat="server" Text='<%# Eval("E_Remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" Font-Size="12px" />
    </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
          <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="项目订单及采购">
            <ContentTemplate>
                  <asp:GridView ID="GridView2" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AllowPaging="false" AutoGenerateColumns="False"
        OnRowEditing="GridView2_RowEditing" OnRowDataBound="gvList_RowDataBound" style="font-family:Tahoma, Arial, sans-serif;">
        <PagerTemplate>
            <br />

        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>查看
                    </td>
                    <td>文件类型
                    </td>
                    <td>单据号
                    </td>
                    <td>创建人
                    </td>
                    <td>创建时间
                    </td>
                    <td>申请人
                    </td>
                    <td>申请时间
                    </td>
                    <td>委托人
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">---暂无数据---
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
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
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
            <asp:BoundField DataField="AppPer_Name" HeaderText="申请人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="appTime" HeaderText="申请时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="maxDoTime" HeaderText="审批时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ToPer_Name" HeaderText="委托人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="E_Remark" HeaderText="备注">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目编号" ItemStyle-CssClass="item" ItemStyle-Width="20%"
                ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblE_Remark" runat="server" Text='<%# Eval("E_Remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
       <RowStyle CssClass="InfoDetail1" Font-Size="12px" />
    </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
          <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="预付款转支付单">
            <ContentTemplate>
                 <asp:GridView ID="GridView3" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AllowPaging="false" AutoGenerateColumns="False"
        OnRowEditing="GridView3_RowEditing" OnRowDataBound="gvList_RowDataBound" style="font-family:Tahoma, Arial, sans-serif;">
        <PagerTemplate>
            <br />

        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>查看
                    </td>
                    <td>文件类型
                    </td>
                    <td>单据号
                    </td>
                    <td>创建人
                    </td>
                    <td>创建时间
                    </td>
                    <td>申请人
                    </td>
                    <td>申请时间
                    </td>
                    <td>委托人
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">---暂无数据---
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
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
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
            <asp:BoundField DataField="AppPer_Name" HeaderText="申请人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="appTime" HeaderText="申请时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="maxDoTime" HeaderText="审批时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ToPer_Name" HeaderText="委托人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="E_Remark" HeaderText="备注">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目编号" ItemStyle-CssClass="item" ItemStyle-Width="20%"
                ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblE_Remark" runat="server" Text='<%# Eval("E_Remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
     <RowStyle CssClass="InfoDetail1" Font-Size="12px" />
    </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
          <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="销售发票">
            <ContentTemplate>
                 <asp:GridView ID="GridView4" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AllowPaging="false" AutoGenerateColumns="False"
        OnRowEditing="GridView4_RowEditing" OnRowDataBound="gvList_RowDataBound" style="font-family:Tahoma, Arial, sans-serif;">
        <PagerTemplate>
            <br />

        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;" >
                    <td>查看
                    </td>
                    <td>文件类型
                    </td>
                    <td>单据号
                    </td>
                    <td>创建人
                    </td>
                    <td>创建时间
                    </td>
                    <td>申请人
                    </td>
                    <td>申请时间
                    </td>
                    <td>委托人
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">---暂无数据---
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
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
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
            <asp:BoundField DataField="AppPer_Name" HeaderText="申请人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="appTime" HeaderText="申请时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="maxDoTime" HeaderText="审批时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ToPer_Name" HeaderText="委托人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="E_Remark" HeaderText="备注">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目编号" ItemStyle-CssClass="item" ItemStyle-Width="20%"
                ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblE_Remark" runat="server" Text='<%# Eval("E_Remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
       <RowStyle CssClass="InfoDetail1" Font-Size="12px" />
    </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
          <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="用车明细表">
            <ContentTemplate>
                <asp:GridView ID="GridView5" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AllowPaging="false" AutoGenerateColumns="False"
        OnRowEditing="GridView5_RowEditing" OnRowDataBound="gvList_RowDataBound" style="font-family:Tahoma, Arial, sans-serif;">
        <PagerTemplate>
            <br />

        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>查看
                    </td>
                    <td>文件类型
                    </td>
                    <td>单据号
                    </td>
                    <td>创建人
                    </td>
                    <td>创建时间
                    </td>
                    <td>申请人
                    </td>
                    <td>申请时间
                    </td>
                    <td>委托人
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">---暂无数据---
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
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
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
            <asp:BoundField DataField="AppPer_Name" HeaderText="申请人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="appTime" HeaderText="申请时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="maxDoTime" HeaderText="审批时间">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ToPer_Name" HeaderText="委托人">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="E_Remark" HeaderText="备注">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目编号" ItemStyle-CssClass="item" ItemStyle-Width="20%"
                ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblE_Remark" runat="server" Text='<%# Eval("E_Remark") %>'></asp:Label>
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
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>


  

   

   

    

</asp:Content>
