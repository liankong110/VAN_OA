<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteSellInInfo.aspx.cs"
    Inherits="VAN_OA.JXC.DeleteSellInInfo" MasterPageFile="~/DefaultMaster.Master"
    Title="销售退货还原 " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                销售退货还原
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text=" 销售退货单号  ：" Style="margin-right: 10px;"></asp:Label>
                <asp:TextBox ID="txtProNo" runat="server" Width="200px"></asp:TextBox>
                <asp:Button ID="btnSub" runat="server" Text="确认删除 销售退货单！" BackColor="Yellow" Width="200px"
                    OnClientClick='return confirm( "确认删除 销售退货单吗？ 如果条件全部满足 该销售退货单的所有信息将被删除，库存会回滚？")'
                    OnClick="btnSub_Click" />
                    &nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
            </td>
        </tr>
    </table>
     <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%"  AutoGenerateColumns="False"
         OnRowDataBound="gvMain_RowDataBound" >
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
                        日期
                    </td>
                    <td>
                        项目编码
                    </td>
                    <td>
                        项目名称
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td>
                        经手人
                    </td>
                    <td>
                        代理人
                    </td>
                    <td>
                        发票号
                    </td>
                    <td>
                        销售单号
                    </td>
                    <td>
                        制单人
                    </td>
                    <td>
                        状态
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
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("Id") %>'>查看</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="RuTime" HeaderText="日期" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Supplier" HeaderText="客户名称" SortExpression="Supplier"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="DoPer" HeaderText="经手人" SortExpression="DoPer" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="代理人">
                <ItemTemplate>
                    <asp:Label ID="DaiLi" runat="server" Text='<%# Eval("DaiLi") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发票号">
                <ItemTemplate>
                    <asp:Label ID="FPNo" runat="server" Text='<%# Eval("FPNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ChcekProNo" HeaderText="销售单号" SortExpression="ChcekProNo"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="制单人">
                <ItemTemplate>
                    <asp:Label ID="CreateName" runat="server" Text='<%# Eval("CreateName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:Label ID="Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <br />
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
        ShowFooter="true">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        仓库
                    </td>
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
                        成本金额
                    </td>
                    <td>
                        成本确认价
                    </td>
                    <td>
                        损失差额
                    </td>
                    <td>
                        销售单价
                    </td>
                    <td>
                        销售金额
                    </td>
                    <td>
                        备注
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
            <asp:BoundField DataField="HouseName" HeaderText="仓库" SortExpression="HouseName" />
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
            <%--  <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />--%>
            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
            <asp:TemplateField HeaderText="数量">
                <ItemTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="成本单价">
                <ItemTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="成本总价">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="成本确认价">
                <ItemTemplate>
                    <asp:Label ID="txtGoodPriceSecond" runat="server" Text='<%# Eval("GoodPriceSecond") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="差额">
                <ItemTemplate>
                    <asp:Label ID="lblGoodTotalCha" runat="server" Text='<%# Eval("GoodTotalCha") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblGoodTotalCha" runat="server" Text='<%# Eval("GoodTotalCha") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="销售单价">
                <ItemTemplate>
                    <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="销售总价">
                <ItemTemplate>
                    <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:Label ID="lblGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>'></asp:Label>
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
</asp:Content>
