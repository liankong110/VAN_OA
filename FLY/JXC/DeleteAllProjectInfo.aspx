<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteAllProjectInfo.aspx.cs"
    Inherits="VAN_OA.JXC.DeleteAllProjectInfo" MasterPageFile="~/DefaultMaster.Master"
    Title="订单报批表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
   <style type="text/css">
        .item
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
    <cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel runat="server">
            <HeaderTemplate>
                项目订单</HeaderTemplate>
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="项目编号  ：" Style="margin-right: 10px;"></asp:Label>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="查询" BackColor="Yellow" Style="margin-right: 20px;"
                    OnClick="Button1_Click" />
                <asp:Button ID="btnSub" runat="server" Text="确认删除所有项目信息！" BackColor="Yellow" Width="200px"
                    OnClientClick='return confirm( "确认删除所有项目信息吗？ 如果条件全部满足 该项目关联的所有检验单，采购订单，项目订单这3个订单的所有信息将被删除？")'
                    OnClick="btnSub_Click" />
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" Text="项目单据号："></asp:Label>
                <asp:TextBox ID="txtPOProNo" runat="server" Width="200px"></asp:TextBox>
                  
                <asp:Button ID="Button2" runat="server" Text="查询" BackColor="Yellow" Style="margin-right: 20px;"   OnClick="Button2_Click" />
                <asp:Button ID="btnDeleteProNo" runat="server" Text="确认删除所有关联项目信息！" BackColor="Yellow"
                    Width="200px" OnClientClick='return confirm( "确认删除吗？ 如果条件全部满足 该项目关联的所有检验单，采购订单，项目订单这3个订单的所有信息将被删除？")'
                    OnClick="btnDeleteProNo_Click" />
                <asp:Label ID="Label3" runat="server" Text="只能是追加订单" ForeColor="Red"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label6" runat="server" Text="项目单据号："></asp:Label>
                <asp:TextBox ID="txtPOProNo1" runat="server" Width="200px"></asp:TextBox>
                 <asp:Label ID="Label8" runat="server" Text="商品代码："></asp:Label>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="200px"></asp:TextBox>
                <asp:Button ID="Button5" runat="server" Text="查询" BackColor="Yellow" Style="margin-right: 20px;"
                    OnClick="Button5_Click" />
                <asp:Button ID="Button6" runat="server"  Text="确认删除所有关联项目信息！" BackColor="Yellow"
                    Width="200px" OnClientClick='return confirm( "确认删除吗？ 如果条件全部满足 该项目关联的所有检验单，采购订单，项目订单这3个订单的所有信息将被删除？")'
                    OnClick="btnDeletePro_GoodNo_Click" /> 
                <br />
                <br />
                <br />
                --项目信息
                <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
                    <PagerTemplate>
                        <br />
                        <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
                        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="First"></asp:LinkButton>
                        <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                        <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Last"></asp:LinkButton>
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
                                    结算方式
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
                        <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
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
                        <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                            DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="POPayStype" HeaderText="结算方式" SortExpression="POPayStype"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="GuestNo" HeaderText="客户ID" SortExpression="GuestNo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="INSIDE" HeaderText="INSIDE" SortExpression="INSIDE" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FPTotal" HeaderText="发票" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="40%" ItemStyle-CssClass="item" />
                        <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                </asp:GridView>
                --销售信息
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    编码
                                </td>
                                <td>
                                    名称
                                </td>
                                <td>
                                    小类
                                </td>
                                <td>
                                    规格
                                </td>
                                <td>
                                    型号
                                </td>
                                <td>
                                    单位
                                </td>
                                <td>
                                    数量
                                </td>
                                <td>
                                    成本单价
                                </td>
                                <td>
                                    管理费
                                </td>
                                <td>
                                    到帐日期
                                </td>
                                <td>
                                    利润%
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">
                                    ---暂无数据---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="Type" HeaderText="" SortExpression="Type" />
                        <asp:BoundField DataField="States" SortExpression="States" HeaderText="状态" />
                        <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" />
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" />
                        <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />
                        <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本单价">
                            <ItemTemplate>
                                <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本总价">
                            <ItemTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售单价">
                            <ItemTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售总价">
                            <ItemTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="管理费">
                            <ItemTemplate>
                                <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售净利">
                            <ItemTemplate>
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ToTime" HeaderText="到帐日期" SortExpression="ToTime" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField HeaderText="利润%">
                            <ItemTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                    <FooterStyle BackColor="#D7E8FF" />
                </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server">
        <HeaderTemplate>采购项目</HeaderTemplate>
        <ContentTemplate>
          
                <asp:Label ID="Label4" runat="server" Text="库存采购项目编号："></asp:Label>
                <asp:TextBox ID="txtKCPOno" runat="server" Width="200px"></asp:TextBox>
                 <asp:Button ID="Button3" runat="server" Text="查询" BackColor="Yellow" Style="margin-right: 20px;"
                    OnClick="Button3_Click" />
                    
                <asp:Button ID="Button4" runat="server" Text="确认删除所有关联项目信息！" BackColor="Yellow" Style="margin-left: 35px"
                    Width="200px" OnClientClick='return confirm( "确认删除吗？ 如果条件全部满足 该项目关联的所有检验单，采购订单，这2个订单的所有信息将被删除？")'
                    OnClick="Button4_Click" />
                <asp:Label ID="Label5" runat="server" Text="库存采购项目订单 编号为KC******" ForeColor="Red"></asp:Label>
        <br />
        <br />
          --项目信息
        <asp:GridView ID="gvCaiMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%"  AutoGenerateColumns="False"
        OnRowDataBound="gvCaiMain_RowDataBound"
         >
        
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        单据号
                    </td>
                                <td>采购人</td>    
                    <td>请购人</td>
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
                        结算方式
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
                        业务类型
                    </td>
                    <td>单据号</td>   
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
         
              <asp:BoundField DataField="ProNo" HeaderText="采购单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="LoginName" HeaderText="采购人" SortExpression="LoginName" ItemStyle-HorizontalAlign="Center" />
          
             <asp:BoundField DataField="CaiGou" HeaderText="请购人" SortExpression="CaiGou" ItemStyle-HorizontalAlign="Center" />
             
            <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POPayStype" HeaderText="结算方式" SortExpression="POPayStype"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestNo" HeaderText="客户ID" SortExpression="GuestNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="INSIDE" HeaderText="INSIDE" SortExpression="INSIDE" ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField DataField="BusType" HeaderText="业务类型" SortExpression="BusType" ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField DataField="CG_ProNo" HeaderText="项目单据号" SortExpression="CG_ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
      --销售信息
        <asp:GridView ID="gvCaiXiaoShou" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvCaiXiaoShou_RowDataBound"
                    ShowFooter="true">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                 <td>
                                   编码
                                </td> <td>
                                    名称
                                </td>
                                <td>
                                    小类
                                </td>
                                <td>
                                    规格
                                </td>
                                 
                                <td>
                                    单位
                                </td>
                                <td>
                                    数量
                                </td>
                                <td>
                                    成本单价
                                </td>
                                <td>
                                    管理费
                                </td>
                                <td>
                                    到帐日期
                                </td>
                                <td>
                                    利润%
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">
                                    ---暂无数据---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                      <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo"  /> 
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" />
                     <%--   <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />--%>
                        <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本单价">
                            <ItemTemplate>
                                <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本总价">
                            <ItemTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售单价">
                            <ItemTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售总价">
                            <ItemTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="管理费">
                            <ItemTemplate>
                                <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售净利">
                            <ItemTemplate>
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ToTime" HeaderText="到帐日期" SortExpression="ToTime" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField HeaderText="利润%">
                            <ItemTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                    <FooterStyle BackColor="#D7E8FF" />
                </asp:GridView>
    
        </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
