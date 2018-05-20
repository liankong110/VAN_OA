<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFOut_BankFlow.aspx.cs" Inherits="VAN_OA.Fin.WFOut_BankFlow"
    MasterPageFile="~/DefaultMaster.Master" Title="出账明细" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:content id="Content1" runat="server" contentplaceholderid="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">出账明细
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">序号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblNumber" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">交易流水号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblReferenceNumber" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                -收款人名称：
                <asp:Label ID="lblInPayeeName" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                出账金额：  
                <asp:Label ID="lblTradeAmount" runat="server" Text="0" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                  交易流水剩余可添加金额：  
                <asp:Label ID="lblLastTotal" runat="server" Text="0"></asp:Label>

                <asp:Label ID="lblTime" Visible="false" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">出账类型 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlOutType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOutType_SelectedIndexChanged">
                    <asp:ListItem value="支付单">1.支付单</asp:ListItem>
                    <asp:ListItem value="预付款单">2.预付款单</asp:ListItem>
                    <asp:ListItem value="申请请款单">3.申请请款单</asp:ListItem>
                    <asp:ListItem value="预期报销单">4.预期报销单</asp:ListItem>
                    <asp:ListItem value="国税缴税">5.国税缴税</asp:ListItem>
                    <asp:ListItem value="2%地方教育附加+3%教育附加+7%城市维护建设">6.2%地方教育附加+3%教育附加+7%城市维护建设</asp:ListItem>
                    <asp:ListItem value="印花税">7.印花税</asp:ListItem>
                    <asp:ListItem value="企业所得税">8.企业所得税</asp:ListItem>
                    <asp:ListItem value="社保缴费">9.社保缴费</asp:ListItem>
                    <asp:ListItem value="公积金缴费">10.公积金缴费</asp:ListItem>
                    <asp:ListItem value="个人所得税">11.个人所得税</asp:ListItem>
                    <asp:ListItem value="银行贷款利息">12.银行贷款利息</asp:ListItem>
                    <asp:ListItem value="工资支出">13.工资支出</asp:ListItem>
                    <asp:ListItem value="奖金支付">14.奖金支付</asp:ListItem>
                    <asp:ListItem value="电信费代扣">15.电信费代扣</asp:ListItem>
                    <asp:ListItem value="水费代扣">16.水费代扣</asp:ListItem>
                    <asp:ListItem value="电费代扣">17.电费代扣</asp:ListItem>
                    <asp:ListItem value="其他">18.其他</asp:ListItem>
                    <asp:ListItem Value="银行手续费">19.银行手续费</asp:ListItem>
                    <asp:ListItem Value="投标保证金">20.投标保证金</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">单据号 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtProNo" runat="server" Width="200px"></asp:TextBox>
                <asp:TextBox ID="txtTotal" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                <asp:TextBox ID="txtName" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                <asp:Button ID="btnTotal" runat="server" Text="获取金额" BackColor="Yellow" OnClick="btnTotal_Click" />
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">出账金额 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtOutTotal" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">注释 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtRemark" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" OnClientClick="return save();" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " BackColor="Yellow" OnClick="btnUpdate_Click" OnClientClick="return save();" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">注：单据号格式 国税缴税--投标保证金 按序号58888888---20888888填写</td>
        </tr>
    </table>

    <cc1:TabContainer ID="TabContainer1" runat="server">
                    <cc1:TabPanel ID="TabPanel1" runat="server">
                        <HeaderTemplate>
                            单据号列表
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:GridView ID="gv_Out" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
        ShowFooter="true" AllowPaging="False"
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
                    <td>交易时间
                    </td>
                    <td>出账类型
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
            <asp:BoundField DataField="Number" HeaderText="序号" SortExpression="Number" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ReferenceNumber" HeaderText="交易流水号" SortExpression="ReferenceNumber" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Time" HeaderText="交易时间" SortExpression="Time" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OutType" HeaderText="出账类型" SortExpression="OutType" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OutTotal" HeaderText="出账金额" SortExpression="OutTotal" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
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
                            </ContentTemplate>
                        </cc1:TabPanel>

        <cc1:TabPanel ID="TabPanel2" runat="server">
                        <HeaderTemplate>
                            交易流水号列表
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:GridView ID="gvLiuShui" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
        ShowFooter="true" AllowPaging="False"
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
                    <td>交易时间
                    </td>
                    <td>出账类型
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
            <asp:BoundField DataField="Number" HeaderText="序号" SortExpression="Number" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ReferenceNumber" HeaderText="交易流水号" SortExpression="ReferenceNumber" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Time" HeaderText="交易时间" SortExpression="Time" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OutType" HeaderText="出账类型" SortExpression="OutType" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OutTotal" HeaderText="出账金额" SortExpression="OutTotal" ItemStyle-HorizontalAlign="Center" />
               <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
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
                            </ContentTemplate>
                        </cc1:TabPanel>
 </cc1:TabContainer>
    

    <script type="text/javascript">
        function save() {

            //当下拉框选择 预付款单和支付单 时,点击获取样金额 后，单据的抬头必须严格和收款人名称一（除了单据抬头是淘宝，本部门，本部门（含税），淘宝(含1.33税) ，
            //京东 ，京东商城 可以不一样） ，修改或添加 才能保存数据，否则提示预付款单和支付单 抬头必须一致

            var InType = document.getElementById('<%= ddlOutType.ClientID %>').value;
            if (InType == "申请请款单" || InType == "预期报销单") {
                var outPayerName = document.getElementById('<%= lblInPayeeName.ClientID %>').innerText;
                var GuestName = document.getElementById('<%= txtName.ClientID %>').value;
                console.log(outPayerName + "---" + GuestName);
                if (outPayerName != GuestName) {
                    if (confirm("收款人和单据抬头有不一致,是否继续保存")) {
                        return true;
                    } else {
                        return false;
                    }
                }
            }
            return true;
        }
    </script>

</asp:content>
