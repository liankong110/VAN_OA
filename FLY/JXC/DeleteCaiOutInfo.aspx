<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteCaiOutInfo.aspx.cs"
    Inherits="VAN_OA.JXC.DeleteCaiOutInfo" MasterPageFile="~/DefaultMaster.Master"
    Title="采购退货单号 " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                采购退货还原
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text=" 采购退货单号  ：" Style="margin-right: 10px;"></asp:Label>
                <asp:TextBox ID="txtProNo" runat="server" Width="200px"></asp:TextBox>
                <asp:Button ID="btnSub" runat="server" Text="确认删除 采购退货单！" BackColor="Yellow" Width="200px"
                    OnClientClick='return confirm( "确认删除 采购退货单？ 如果条件全部满足 该采购退货单的所有信息和支付信息将被删除，库存会回滚？")'
                    OnClick="btnSub_Click" />
                    &nbsp;&nbsp;&nbsp;
                      <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
            </td>
        </tr>
    </table>
      <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False"
        OnRowDataBound="gvMain_RowDataBound">
       
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        退货日期
                    </td>
                    <td>
                        供应商
                    </td>
                    <td>
                        仓库
                    </td>
                    <td>
                        项目编码
                    </td>
                    <td>
                        项目名称
                    </td>
                    <td>AE</td>
                    <td>
                        入库单号
                    </td>
                    <td>
                        制单人
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
            <asp:TemplateField HeaderText="单据号">
                <ItemTemplate>
                    <asp:Label ID="ProNo" runat="server" Text='<%# Eval("ProNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RuTime" HeaderText="退货日期" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField HeaderText="供应商">
                <ItemTemplate>
                    <asp:Label ID="Supplier" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="仓库">
                <ItemTemplate>
                    <asp:Label ID="HouseName" runat="server" Text='<%# Eval("HouseName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目编码">
                <ItemTemplate>
                    <asp:Label ID="PONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目名称">
                <ItemTemplate>
                    <asp:Label ID="POName" runat="server" Text='<%# Eval("POName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="AE">
                <ItemTemplate>
                    <asp:Label ID="AE" runat="server" Text='<%# Eval("AE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="入库单号">
                <ItemTemplate>
                    <asp:Label ID="ChcekProNo" runat="server" Text='<%# Eval("ChcekProNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="采购人">
                <ItemTemplate>
                    <asp:Label ID="DoPer" runat="server" Text='<%# Eval("DoPer") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
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
                        单位
                    </td>
                    <td>
                        数量
                    </td>
                    <td>
                        单价
                    </td>
                    <td>
                        金额
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
            <%--            <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />--%>
            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
            <asp:TemplateField HeaderText="数量">
                <ItemTemplate>
                    <asp:Label ID="lbVislNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblVisNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单价">
                <ItemTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="总价">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GoodRemark" HeaderText="备注" SortExpression="GoodRemark" />
            <asp:BoundField DataField="QingGouPer" HeaderText="请购人" SortExpression="QingGouPer" />
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
