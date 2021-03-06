﻿<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="FundList.aspx.cs"
    Inherits="VAN_OA.ReportForms.FundList" MasterPageFile="~/DefaultMaster.Master"
    Title="请款单列表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                请款单列表
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
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="Image1"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton1"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                客户名称
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>
            </td>
            <td>
                项目名称
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                项目编号
            </td>
            <td>
                <asp:TextBox ID="txtPONO" runat="server"></asp:TextBox>
                状态:
                    <asp:DropDownList ID="ddlState" runat="server" Width="200px">   
                     <asp:ListItem>通过</asp:ListItem>                 
                    <asp:ListItem>执行中</asp:ListItem>                
                    <asp:ListItem>不通过</asp:ListItem>   
                    <asp:ListItem>全部</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                请款摘要
            </td>
            <td>
                <asp:TextBox ID="txtShuoMing" runat="server"></asp:TextBox>
            </td>
            <td>
                款项用途
            </td>
            <td>
                <asp:TextBox ID="txtYongTu" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                请款金额
            </td>
            <td>
                <asp:DropDownList ID="ddlFuHao" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
            </td>
            <td>
                单据号
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtProNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                AE
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
            <td>
                请款类型
            </td>
            <td>
                <asp:DropDownList ID="ddlFundType" runat="server">
                     <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="人工费" Value="人工费"></asp:ListItem>
                    <asp:ListItem Text="会务费" Value="会务费"></asp:ListItem>
                    <asp:ListItem Text="行政采购" Value="行政采购"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                公司
            </td>
            <td>
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
          <tr>
                        <td colspan="4">项目归类:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">非特殊</asp:ListItem>
                    <asp:ListItem Value="1">特殊</asp:ListItem>
                </asp:DropDownList>
                            项目关闭：
                <asp:DropDownList ID="ddlClose" runat="server">
                    <asp:ListItem Text="全部" Value="-1"> </asp:ListItem>
                    <asp:ListItem Text="关闭" Value="1"> </asp:ListItem>
                    <asp:ListItem Text="未关闭" Value="0"> </asp:ListItem>
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
                            客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                                DataTextField="GuestType">
                            </asp:DropDownList>
                            客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                                DataTextField="GuestProString" Style="left: 0px;">
                            </asp:DropDownList>
                        </td>
                    </tr>
        <tr>
            <td colspan="6">
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
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        单据号
                    </td>
                    <td>
                        日期
                    </td>
                    <td>
                        项目编号
                    </td>
                    <td>
                        项目名称
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td>
                        AE
                    </td>
                    <td>
                        请款摘要
                    </td>
                    <td>
                        总金额
                    </td>
                    <td>
                        实际金额
                    </td>
                    <td>
                        请款费率+税制
                    </td>
                    <td>
                        请款类型
                    </td>
                    <td>
                        所属公司
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

             <asp:TemplateField HeaderText="编辑" ItemStyle-Width="30px">
                <ItemTemplate>
                        <a href="/EFrom/FundsUse.aspx?ProId=9&allE_id=<%# Eval("id") %>&IsEdit=true" target="_blank">
                            编辑 </a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"  />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="查看" ItemStyle-Width="30px">
                <ItemTemplate>
                        <a href="/EFrom/FundsUse.aspx?ProId=9&allE_id=<%# Eval("id") %>" target="_blank">
                            查看</a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"  />
            </asp:TemplateField>
            <asp:BoundField DataField="ProNo" HeaderText="单据号">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="datetiem" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="65px">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5"  />
            </asp:BoundField>
            <asp:BoundField DataField="PONo" HeaderText="项目编号" ItemStyle-Width="60px"> 
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="POName" HeaderText="项目名称" ItemStyle-Width="180px">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="GuestName" HeaderText="客户名称">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="AE" HeaderText="AE" ItemStyle-Width="40px">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ShuoMing" HeaderText="请款摘要">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="AllTotal" HeaderText="总金额">
                <ItemStyle HorizontalAlign="Right" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="AllTrueTotal" HeaderText="实际金额">
                <ItemStyle HorizontalAlign="Right" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="XishuDes" HeaderText="请款费率+税制" ItemStyle-Width="90px">
                <ItemStyle HorizontalAlign="Left" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="FundType" HeaderText="请款类型">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ComName" HeaderText="所属公司" ItemStyle-Width="150px">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
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

        总金额合计:
    <asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      实际金额合计:
    <asp:Label ID="lblAllTrueTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
</asp:Content>
