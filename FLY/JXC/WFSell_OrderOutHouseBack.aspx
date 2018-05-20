<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSell_OrderOutHouseBack.aspx.cs"
    Inherits="VAN_OA.JXC.WFSell_OrderOutHouseBack" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="出库单签回" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 <script type="text/javascript">
        function ChuKu() {
            var url = "../JXC/DioSellOutOrderBackList.aspx";
            window.showModalDialog(url, null, 'dialogWidth:850px;dialogHeight:450px;help:no;status:no')
        }
</script>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                出库单签回-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                申请人：
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td>
                日期:
            </td>
            <td>
                <asp:TextBox ID="txtRuTime" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRuTime"
                    Format="yyyy-MM-dd hh:mm:ss" PopupButtonID="Image1">
                </cc1:CalendarExtender>  <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>
                项目编码：
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                出库单号:
            </td>
            <td>
                <asp:TextBox ID="txtSellProNo" runat="server" Width="200px"  Enabled="false"></asp:TextBox>
                <asp:LinkButton ID="lbtnAddFiles" runat="server" OnClientClick="ChuKu()" ForeColor="Red"
                    OnClick="LinkButton1_Click1">  <font style="color: Red">*</font>
      选择</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                项目名称:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true" Width="250px"></asp:TextBox>
            </td>
            <td>
                签收状态 ：
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:RadioButton ID="rdoA" runat="server" GroupName="a" Text="收到" />
                    <asp:RadioButton ID="rdoB" runat="server" GroupName="a" Text="未收到" />
                    <font style="color: Red">*</font>
                </asp:Panel>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                客户名称：
            </td>
            <td>
                <asp:TextBox ID="txtSupplier" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
                <asp:Label ID="lblTotal" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right" style="width: 100%;">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <br />
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" ShowFooter="true">
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
                                    销售单价
                                </td>
                                <td>
                                    销售金额
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
                        <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售单价">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                            </ItemTemplate>                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售总价">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
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
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserName">
                    <asp:ListItem Selected="True">通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="本次审批意见:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnPrint" runat="server" Text="打印" BackColor="Yellow" Visible="false"
                    Width="51px" OnClick="btnPrint_Click" />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
</asp:Content>
