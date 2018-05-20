<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFUpdatePOName.aspx.cs" Inherits="VAN_OA.JXC.WFUpdatePOName"
    Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master" Title="项目名称更新" %>
<%@ Import Namespace="VAN_OA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <style type="text/css">
        .item
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>

    <script src="../Scripts/tinybox.js" type="text/javascript"></script>

    <script src="../Scripts/tinyboxCu.js" type="text/javascript"></script>

    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                项目名称更新
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
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 新项目名称:<asp:TextBox ID="txtNewPOName" runat="server" ForeColor="Red"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="更新 "  
                    OnClientClick="return confirm('确定要提交吗？')"  BackColor="Yellow" 
                    onclick="Button1_Click"  />
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
                订单状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatue" runat="server" Width="100px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>执行中</asp:ListItem>
                    <asp:ListItem>通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
                项目归类:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="70px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                </asp:DropDownList>
                项目关闭：
                <asp:DropDownList ID="ddlClose" runat="server" Width="70px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未关闭</asp:ListItem>
                    <asp:ListItem Value="1">关闭</asp:ListItem>
                </asp:DropDownList>
                 项目选中：
                <asp:DropDownList ID="ddlIsSelect" runat="server" Width="70px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未选中</asp:ListItem>
                    <asp:ListItem Value="1">选中</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>
                项目状态
            </td>
            <td>
                <%--<asp:DropDownList ID="ddlPOStatue" runat="server" Width="200px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>初始状态</asp:ListItem>
                 <asp:ListItem>已交付</asp:ListItem>
                  <asp:ListItem>已开票</asp:ListItem>
                   <asp:ListItem>已结清</asp:ListItem>
                </asp:DropDownList>--%>
                <asp:CheckBox ID="CheckBox1" runat="server" Text="初始状态" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="已交付" />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="已开票" />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="已结清" />
                <asp:CheckBox ID="CheckBox5" runat="server" Text="出库单签回" />
                <asp:CheckBox ID="CheckBox6" runat="server" Text="发票单签回" />
                <asp:CheckBox ID="cbIsPoFax" runat="server" Text="不含税" />
            </td>
        </tr>
        <tr>
            <td>
                AE：
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>商品编码: <asp:TextBox ID="txtGoodNo" runat="server" Width="100px"></asp:TextBox>名称/小类/规格: <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="100PX"></asp:TextBox>
                或者
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="100PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
     <table width="100%" border="0">
        <tr>
            <td>
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Id" Width="160%" AllowPaging="True" AutoGenerateColumns="False"
            OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound"
            OnRowCommand="gvMain_RowCommand">
            <PagerTemplate>
                <br />
               
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            单据号
                        </td>
                        <td>
                            项目状态
                        </td>
                        <td>
                            项目编码
                        </td>
                        <td>
                            项目名称
                        </td>
                        <td>
                            项目日期
                        </td>
                        <td>
                            项目金额
                        </td>
                        <td>
                            结算
                        </td>
                        <td>
                            客户ID
                        </td>
                        <td>
                            客户名称
                        </td>
                        <td>
                            AE
                        </td>
                        <td>
                            INSIDE
                        </td>
                        <td>
                            发票
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
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("Id") %>'>查看</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblSpecial" runat="server" Text='<%# IsSpecial(Eval("IsSpecial")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="POStatue" HeaderText="项目状态" SortExpression="POStatue"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="出库">
                    <ItemTemplate>
                        <a href="/JXC/Sell_OrderOutHouseBackList.aspx?Type=<%# GetStateType(Eval("POStatue5")) %>&PONo=<%# Eval("PONo") %>">
                            <%# Eval("POStatue5") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发票">
                    <ItemTemplate>
                        <a href="/JXC/Sell_OrderPFBackList.aspx?Type=<%# GetStateType(Eval("POStatue6")) %>&PONo=<%# Eval("PONo") %>">
                            <%# Eval("POStatue6") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="项目编码">
                    <ItemTemplate>
                        <a href="javascript:tinybox_Showiframe('WFPOEnclosure.aspx?PONo=<%# Eval("PONo") %>',1000,600);">
                            <%# Eval("PONo")%></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <%--  <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:f2}"/>
                <asp:BoundField DataField="POPayStype" HeaderText="结算" SortExpression="POPayStype"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="税">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsPoFax" runat="server" Checked='<% #Eval("IsPoFax") %>' Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="GuestNo" HeaderText="客户ID" SortExpression="GuestNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestType" HeaderText="客户类型" SortExpression="GuestType"
                    ItemStyle-HorizontalAlign="Center" />                
                    
                     <asp:TemplateField HeaderText="客户属性" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# GetGestProInfo(Eval("GuestPro"))%>
                </ItemTemplate>
            </asp:TemplateField>
            
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="INSIDE" HeaderText="INSIDE" SortExpression="INSIDE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FPTotal" HeaderText="发票" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="40%" ItemStyle-CssClass="item" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" ForeColor="Black" />
        </asp:GridView>
         <webdiyer:AspNetPager ID="AspNetPager6" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
                    PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
                    NextPageText="下页" OnPageChanged="AspNetPager6_PageChanged">
                </webdiyer:AspNetPager>
                  </td>
        </tr>
        </table>
    </asp:Panel>   
</asp:Content>
