<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFBandFlow.aspx.cs"
    Inherits="VAN_OA.Fin.WFBandFlow" MasterPageFile="~/DefaultMaster.Master"
    Title="财务审核" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="System.Web.UI" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">财务审核
            </td>
        </tr>
        <tr>
            <td>交易流水号：
            </td>
            <td>
                <asp:TextBox ID="txtTransactionReferenceNumber" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>交易日期：
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server" Width="70px"></asp:TextBox>
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
            <td>交易类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlTransactionType" runat="server">
                    <asp:ListItem>全部</asp:ListItem>
                    <asp:ListItem>来账</asp:ListItem>
                    <asp:ListItem>往账</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2">

                <asp:TextBox ID="txtTradeAmountFrom" runat="server" Width="100px"></asp:TextBox>
                <asp:DropDownList ID="ddlTradeAmountFrom" runat="server">
                    <asp:ListItem>&gt;</asp:ListItem>
                    <asp:ListItem>&gt;=</asp:ListItem>
                </asp:DropDownList>
                交易金额 
                <asp:DropDownList ID="ddlTradeAmountTo" runat="server">
                    <asp:ListItem>&gt;</asp:ListItem>
                    <asp:ListItem>&gt;=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtTradeAmountTo" runat="server" Width="100px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>付款人：
            </td>
            <td>
                <asp:TextBox ID="txtOutPayerName" runat="server" Width="200px"></asp:TextBox>
                <asp:CheckBox ID="cbCompany" Text="本公司" runat="server" AutoPostBack="True" OnCheckedChanged="cbCompany_CheckedChanged" />
            </td>
            <td>收款人：
            </td>
            <td>
                <asp:TextBox ID="txtInPayeeName" runat="server" Width="200px"></asp:TextBox>
                <asp:CheckBox ID="cbCompany1" Text="本公司" runat="server" AutoPostBack="True" OnCheckedChanged="cbCompany1_CheckedChanged" />
            </td>
        </tr>

        <tr>
            <td>发票号码：
            </td>
            <td>
                <asp:TextBox ID="txtFPNo" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>进账类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlIncomeType" runat="server">
                    <asp:ListItem>全部</asp:ListItem>
                    <asp:ListItem>发票回款</asp:ListItem>
                    <asp:ListItem>客户预付</asp:ListItem>
                    <asp:ListItem>汇款退款</asp:ListItem>
                    <asp:ListItem>保证金退款</asp:ListItem>
                    <asp:ListItem>过单</asp:ListItem>
                    <asp:ListItem>银行利息</asp:ListItem>
                    <asp:ListItem>其他</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>

            <td>出账类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlPaymentType" runat="server">
                    <asp:ListItem Value="全部">全部</asp:ListItem>
                    <asp:ListItem Value="支付单">1.支付单</asp:ListItem>
                    <asp:ListItem Value="预付款单">2.预付款单</asp:ListItem>
                    <asp:ListItem Value="申请请款单">3.申请请款单</asp:ListItem>
                    <asp:ListItem Value="预期报销单">4.预期报销单</asp:ListItem>
                    <asp:ListItem Value="国税缴税">5.国税缴税</asp:ListItem>
                    <asp:ListItem Value="2%地方教育附加+3%教育附加+7%城市维护建设">6.2%地方教育附加+3%教育附加+7%城市维护建设</asp:ListItem>
                    <asp:ListItem Value="印花税">7.印花税</asp:ListItem>
                    <asp:ListItem Value="企业所得税">8.企业所得税</asp:ListItem>
                    <asp:ListItem Value="社保缴费">9.社保缴费</asp:ListItem>
                    <asp:ListItem Value="公积金缴费">10.公积金缴费</asp:ListItem>
                    <asp:ListItem Value="个人所得税">11.个人所得税</asp:ListItem>
                    <asp:ListItem Value="银行贷款利息">12.银行贷款利息</asp:ListItem>
                    <asp:ListItem Value="工资支出">13.工资支出</asp:ListItem>
                    <asp:ListItem Value="奖金支付">14.奖金支付</asp:ListItem>
                    <asp:ListItem Value="电信费代扣">15.电信费代扣</asp:ListItem>
                    <asp:ListItem Value="水费代扣">16.水费代扣</asp:ListItem>
                    <asp:ListItem Value="电费代扣">17.电费代扣</asp:ListItem>
                    <asp:ListItem Value="其他">18.其他</asp:ListItem>
                    <asp:ListItem Value="银行手续费">19.银行手续费</asp:ListItem>
                    <asp:ListItem Value="投标保证金">20.投标保证金</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>单据号：
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" Width="100px"></asp:TextBox>交易流水单完成:
                <asp:DropDownList ID="ddlProgress" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">开始未完成</asp:ListItem>
                    <asp:ListItem Value="1">未开始</asp:ListItem>
                    <asp:ListItem Value="2">未完成</asp:ListItem>
                    <asp:ListItem Value="3">已完成</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>注释：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtNotes" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                     <asp:Button ID="btnExcel" runat="server" Text=" 导 出 " BackColor="Yellow" OnClick="btnExcel_Click" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="导入Excel" BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                 <asp:Button ID="btnReport" runat="server" Text="银行往来月报表" BackColor="Yellow" OnClick="btnReport_Click" />
                <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="id" Width="180%" AutoGenerateColumns="False" AllowPaging="true" OnRowEditing="gvList_RowEditing"
            OnPageIndexChanging="gvList_PageIndexChanging" OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvMain_RowCommand">
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">

                        <td height="25" align="center">交易类型
                        </td>
                        <td height="25" align="center">业务类型
                        </td>
                        <td height="25" align="center">付款人开户行名
                        </td>
                        <td height="25" align="center">付款人账号
                        </td>
                        <td height="25" align="center">付款人名称
                        </td>
                        <td height="25" align="center">收款人开户行名
                        </td>
                        <td height="25" align="center">收款人账号
                        </td>
                        <td height="25" align="center">收款人名称
                        </td>
                        <td height="25" align="center">交易时间
                        </td>
                        <td height="25" align="center">交易货币
                        </td>
                        <td height="25" align="center">交易金额
                        </td>
                        <td height="25" align="center">交易后余额
                        </td>
                        <td height="25" align="center">交易流水号
                        </td>
                        <td height="25" align="center">凭证类型
                        </td>
                        <td height="25" align="center">摘要
                        </td>
                        <td height="25" align="center">用途
                        </td>
                        <td height="25" align="center">交易附言
                        </td>
                        <td height="25" align="center">备注
                        </td>
                        <%--   <td height="25" align="center">注释
                        </td>
                        <td height="25" align="center">进账类型
                        </td>
                        <td height="25" align="center">付款类型
                        </td>
                        <td height="25" align="center">预留备注
                        </td>--%>
                        <tr>
                            <td colspan="4" align="center" style="height: 80%">---暂无数据---
                            </td>
                        </tr>
                </table>
            </EmptyDataTemplate>
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <Columns>

                <asp:TemplateField HeaderText="新增">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                            AlternateText="新增" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("TransactionReferenceNumber") %>'>查看</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="TransactionType" HeaderText="交易类型" SortExpression="TransactionType" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="BusinessType" HeaderText="业务类型" SortExpression="BusinessType" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PayerAccountBank" HeaderText="付款人开户行名" SortExpression="PayerAccountBank" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DebitAccountNo" HeaderText="付款人账号" SortExpression="DebitAccountNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="OutPayerName" HeaderText="付款人名称" SortExpression="OutPayerName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="BeneficiaryAccountBank" HeaderText="收款人开户行名" SortExpression="BeneficiaryAccountBank" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PayeeAccountNumber" HeaderText="收款人账号" SortExpression="PayeeAccountNumber" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="InPayeeName" HeaderText="收款人名称" SortExpression="InPayeeName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TransactionDate" HeaderText="交易时间" SortExpression="TransactionDate" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TradeCurrency" HeaderText="交易货币" SortExpression="TradeCurrency" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TradeAmount" HeaderText="交易金额" SortExpression="TradeAmount" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AfterTransactionBalance" HeaderText="交易后余额" SortExpression="AfterTransactionBalance" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TransactionReferenceNumber" HeaderText="交易流水号" SortExpression="TransactionReferenceNumber" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="VoucherType" HeaderText="凭证类型" SortExpression="VoucherType" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Reference" HeaderText="摘要" SortExpression="Reference" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Purpose" HeaderText="用途" SortExpression="Purpose" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Remark" HeaderText="交易附言" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Remarks" HeaderText="备注" SortExpression="Remarks" ItemStyle-HorizontalAlign="Center" />
                <%--    <asp:BoundField DataField="Notes" HeaderText="注释" SortExpression="Notes" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="IncomeType" HeaderText="进账类型" SortExpression="IncomeType" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PaymentType" HeaderText="付款类型" SortExpression="PaymentType" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="YuRemarks" HeaderText="预留备注" SortExpression="YuRemarks" ItemStyle-HorizontalAlign="Center" />--%>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
    </asp:Panel>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>

    <table width="100%" border="0">
        <tr>
            <td>
                <cc1:TabContainer ID="TabContainer1" runat="server">
                    <cc1:TabPanel ID="TabPanel1" runat="server">
                        <HeaderTemplate>
                            进账
                        </HeaderTemplate>
                        <ContentTemplate>

                            <table width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_In" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                                            DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                                            ShowFooter="true" OnPageIndexChanging="gvList_PageIndexChanging" AllowPaging="False" OnRowEditing="gv_In_RowEditing"
                                            PageSize="20" OnRowDeleting="gv_In_RowDeleting">
                                            <PagerTemplate>
                                                <br />
                                            </PagerTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%">
                                                    <tr style="height: 20px; background-color: #336699; color: White;">
                                                        <td>交易流水号
                                                        </td>
                                                        <td>进账类型
                                                        </td>
                                                        <td>发票号码
                                                        </td>
                                                        <td>发票金额
                                                        </td>
                                                        <td>注释
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="11" align="center" style="height: 80%">---暂无数据---
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText=" 编辑">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                                                            AlternateText="编辑" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="删除">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                                                            CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Number" HeaderText="序号" SortExpression="Number" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ReferenceNumber" HeaderText="交易流水号" SortExpression="ReferenceNumber" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="InType" HeaderText="进账类型" SortExpression="InType" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="FPNo" HeaderText="发票号码" SortExpression="FPNo" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="FPTotal" HeaderText="进账金额" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Remark" HeaderText="注释" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                                            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                                                HorizontalAlign="Center" />
                                            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                                            <RowStyle CssClass="InfoDetail1" />
                                            <FooterStyle BackColor="#D7E8FF" />
                                        </asp:GridView>


                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>

                    <cc1:TabPanel ID="TabPanel2" runat="server">
                        <HeaderTemplate>
                            出账
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_Out" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                                            DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                                            ShowFooter="true" OnPageIndexChanging="gvList_PageIndexChanging" AllowPaging="False" OnRowEditing="gv_Out_RowEditing"
                                            OnRowDeleting="gv_Out_RowDeleting"
                                            PageSize="20">
                                            <PagerTemplate>
                                                <br />
                                            </PagerTemplate>
                                            <EmptyDataTemplate>
                                                <table width="100%">
                                                    <tr style="height: 20px; background-color: #336699; color: White;">
                                                        <td>序号</td>
                                                        <td>交易流水号
                                                        </td>
                                                        <td>出账类型
                                                        </td>
                                                        <td>单据号
                                                        </td>
                                                        <td>出账金额
                                                        </td>
                                                        <td>注释
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="11" align="center" style="height: 80%">---暂无数据---
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText=" 编辑">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                                                            AlternateText="编辑" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="删除">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                                                            CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Number" HeaderText="序号" SortExpression="Number" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ReferenceNumber" HeaderText="交易流水号" SortExpression="ReferenceNumber" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="OutType" HeaderText="出账类型" SortExpression="OutType" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="OutTotal" HeaderText="出账金额" SortExpression="OutTotal" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Remark" HeaderText="注释" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                                            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                                                HorizontalAlign="Center" />
                                            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                                            <RowStyle CssClass="InfoDetail1" />
                                            <FooterStyle BackColor="#D7E8FF" />
                                        </asp:GridView>


                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>

            </td>
        </tr>
    </table>
</asp:Content>
