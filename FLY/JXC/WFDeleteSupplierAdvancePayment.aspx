<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFDeleteSupplierAdvancePayment.aspx.cs"
    Inherits="VAN_OA.JXC.WFDeleteSupplierAdvancePayment" MasterPageFile="~/DefaultMaster.Master"
    Title="预付款删除" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td style="height: 20px; background-color: #336699; color: White;">
                预付款删除
            </td>
        </tr>
        <tr>
            <td>
                预付款单号:<asp:TextBox ID="txtProNo" Width="200px" runat="server"></asp:TextBox>
                <asp:Button ID="btnSub" runat="server" Text="删除 预付款！" BackColor="Yellow" Width="200px"
                    OnClientClick='return confirm( "确认删除对应的预付款信息吗？ 如果条件全部满足将被删除？")' OnClick="btnSub_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="False" AutoGenerateColumns="False"
        ShowFooter="true">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        单号
                    </td>
                    <td>
                        制单人
                    </td>
                    <td>
                        制单时间
                    </td>
                    <td>
                        备注
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" style="height: 80%">
                        ---暂无数据---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="ProNo" HeaderText="单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateName" HeaderText="制单人" SortExpression="CreateName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreteTime" HeaderText="日期" SortExpression="CreteTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
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
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Ids" Width="100%" AllowPaging="False" AutoGenerateColumns="False"
        ShowFooter="true" OnRowDataBound="gvList_RowDataBound">
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
                        采购单号
                    </td>
                    <td>
                        供应商
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
                        AE
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
                        单位
                    </td>
                    <td>
                        采购数
                    </td>
                    <td>
                        采购单价
                    </td>
                    <td>
                        金额
                    </td>
                    <td>
                        发票号
                    </td>
                    <td>
                        出款日期
                    </td>
                    <td>
                        付款金额
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
            <asp:BoundField DataField="ProNo" HeaderText="采购单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestName" HeaderText="供应商" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POGuestName" HeaderText="客户名称" SortExpression="POGuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POAE" HeaderText="AE" SortExpression="POAE" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodNum" HeaderText="采购数" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="lastGoodNum" HeaderText="净数量" SortExpression="lastGoodNum"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodPrice" HeaderText="采购单价" SortExpression="GoodPrice"
                ItemStyle-HorizontalAlign="Center" />
            <%--       
                        <asp:BoundField DataField="LastTotal" HeaderText="金额" SortExpression="LastTotal"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />--%>
            <asp:TemplateField HeaderText="金额">
                <ItemTemplate>
                    <asp:Label ID="lblLastTotal" runat="server" Text='<%# Eval("LastTotal") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblLastTotal" runat="server" Text='<%# Eval("LastTotal") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SupplierFPNo" HeaderText="发票号" SortExpression="SupplierFPNo"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="出款日期" SortExpression="SupplierInvoiceDate"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="SupplierInvoicePrice" HeaderText="预付单价" SortExpression="SupplierInvoicePrice"
                DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="已支付">
                <ItemTemplate>
                    <a href="/JXC/WFSupplierInvoiceList.aspx?PayIds=<%# Eval("Ids") %>" target="_blank">
                        <%# Eval("HadSupplierInvoiceTotal")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="预付金额">
                <ItemTemplate>
                    <asp:Label ID="lblSupplierInvoiceTotal" runat="server" Text='<%# Eval("SupplierInvoiceTotal") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblSupplierInvoiceTotal" runat="server" Text='<%# Eval("SupplierInvoiceTotal") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
</asp:Content>
