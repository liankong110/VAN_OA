<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPONoIsSelected.aspx.cs"
    Inherits="VAN_OA.JXC.SetPONoIsSelected" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="自动选中" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">自动选中
            </td>
        </tr>
        <tr>
            <td>项目日期:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" BackColor="#7FFF00"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server" BackColor="#7FFF00"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>公司名称：
            </td>
            <td>
                <asp:DropDownList ID="ddlCompany" BackColor="#7FFF00" runat="server" DataTextField="ComName" DataValueField="ComCode"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>项目编号：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                项目名称：
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>
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
                含税：
                <asp:DropDownList ID="ddlHanShui" runat="server" Width="50px">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="含税"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不含税"></asp:ListItem>
                </asp:DropDownList>
                客户名称：
                <asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>
                AE：
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <div align="left" style="display: inline;">
                    项目归类:
                <asp:DropDownList ID="ddlIsSpecial" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                </asp:DropDownList>
                    项目类别：<asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id" BackColor="#7FFF00">
                    </asp:DropDownList>
                    客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                        DataTextField="GuestType" Width="50px">
                    </asp:DropDownList>
                    客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro" BackColor="#7FFF00"
                        DataTextField="GuestProString" Width="50px">
                    </asp:DropDownList>
                    到款金额
                    <asp:DropDownList ID="ddlFuHao" runat="server" BackColor="#7FFF00">
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBai" runat="server" Width="21px" Text="90" BackColor="#7FFF00"></asp:TextBox>%×项目金额的项目

                   <br />
                    <asp:TextBox ID="txtLeftJingLi" runat="server" Width="100"></asp:TextBox>
                  
                    <asp:DropDownList ID="ddlLeftJingLi" runat="server" >
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    </asp:DropDownList>
                    项目净利
                     <asp:DropDownList ID="ddlRightJingLi" runat="server" >
                         <asp:ListItem Text=">" Value=">"></asp:ListItem>
                         <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     </asp:DropDownList>
                    <asp:TextBox ID="txtRightJingLi" runat="server" Width="100"></asp:TextBox>


                    <asp:TextBox ID="txtLeftPoTotal" runat="server" Width="100"></asp:TextBox>
                    
                    <asp:DropDownList ID="ddlLeftPoTotal" runat="server" >
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    </asp:DropDownList>
                    项目金额
                       <asp:DropDownList ID="ddlRightPoTotal" runat="server" >
                           <asp:ListItem Text=">" Value=">"></asp:ListItem>
                           <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                       </asp:DropDownList>
                    <asp:TextBox ID="txtRightPoTotal" runat="server" Width="100"></asp:TextBox>
                </div>
                <div align="right" style="display: inline; float: right;">
                    <asp:Button ID="btnQuery" runat="server" Text=" 查询 " BackColor="Yellow" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSelect" runat="server" Text=" 自动选中 " OnClientClick="return confirm('确定要提交吗？')"
                        BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text=" 取消选中 " OnClientClick="return confirm('确定要提交吗？')"
                        BackColor="Yellow" OnClick="btnCancel_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 返 回 " BackColor="Yellow" OnClick="Button1_Click" />
                </div>
            </td>
        </tr>
    </table>
    注：项目金额=0，项目净利=0 ，项目的特殊属性在自动选中按钮中添加
      <br />
    1．  自动选中和 取消自动选中 的条件1：项目日期+公司名称+ 项目类别+客户类型+客户属性，特殊订单不做结算选中和项目选中  
        <br />
    2．  自动选中和 取消自动选中 的条件2：a.凡是到款金额 下拉框“大小“ 百分比×项目金额的项目，在项目归类 中的选中属性打上勾，  b.凡是项目日期区间的项目，项目归类中的结算选中的属性均打上勾。c.凡是项目金额=0的项目，在项目归类 中的选中属性打上勾。d. 凡是项目总成本>=项目金额 的项目，在项目归类 中的选中属性打上勾
    <br />
    3.自动选中和取消自动选中的条件的输入框 和 下拉框部分背景为淡绿色
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound"
        OnRowCommand="gvMain_RowCommand">
        <PagerTemplate>
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>单据号
                    </td>
                    <td>项目编码
                    </td>
                    <td>项目名称
                    </td>
                    <td>客户名称
                    </td>
                    <td>项目日期
                    </td>
                    <td>AE
                    </td>
                    <td>特殊
                    </td>
                    <td>含税
                    </td>
                    <td>发票类型
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">---暂无数据---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="项目编码">
                <ItemTemplate>
                    <asp:Label ID="PONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="POTypeString" HeaderText="类型" SortExpression="POTypeString" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="关闭">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsClose" runat="server" Checked='<% #Eval("IsClose") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="结算选中 ">
                <ItemTemplate>
                    <asp:CheckBox ID="cbJieIsSelected" runat="server" Checked='<% #Eval("JieIsSelected") %>'
                        Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="选中">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSelected" runat="server" Checked='<% #Eval("IsSelected") %>'
                        Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="特殊">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSpecial" runat="server" Checked='<% #Eval("IsSpecial") %>'
                        Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="含税">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsPoFax" runat="server" Checked='<% #Eval("IsPoFax") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发票类型">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidtxt" Value='<%#Eval("FpType")%>' />
                    <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="FpType" DataTextField="FpType" Width="90px"
                        Enabled="false">
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="maoliTotal" HeaderText="项目净利" SortExpression="maoliTotal"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="InvoiceTotal" HeaderText="到款金额" SortExpression="InvoiceTotal" DataFormatString="{0:n2}" />
            <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" DataFormatString="{0:n2}" />
            <asp:BoundField DataField="GoodTotal" HeaderText="总成本" SortExpression="GoodTotal"
                DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="BILI" HeaderText="到款率" SortExpression="BILI" DataFormatString="{0:n2}" />
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
