<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFToInvoice.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.WFToInvoice" MasterPageFile="~/DefaultMaster.Master"
    Title="到款单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <script type="text/javascript">
        function show() {
            alert("1");
            document.getElementById("btnSub").disabled = false;

        }
    </script>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                到款单<asp:Label ID="lblDelete" runat="server" Text="删除" ForeColor="Red" Visible="false"/>-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlStyle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="实际发票到款"></asp:ListItem>
                    <asp:ListItem Value="1" Text="预付款"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                申请人：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                项目名称：
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="btnFp" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioSell_OrderFP.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton2_Click">选择</asp:LinkButton>
                <asp:LinkButton ID="btnYuFu" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioOrderToInvoice.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" Visible="false" OnClick="btnYuFu_Click">选择</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                到款日期：
            </td>
            <td>
                <asp:TextBox ID="txtDaoKuanDate" runat="server" Width="200px" OnTextChanged="txtDaoKuanDate_TextChanged"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                    Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtDaoKuanDate">
                </cc1:CalendarExtender>
            </td>
            <td>
                项目编码：
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="总金额：" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                金额： <font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtTotal" runat="server" Width="200px"></asp:TextBox>
                
            </td>
            <td>
                客户名称：
            </td>
            <td>
                <asp:TextBox ID="txtSupplier" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                实际账期系数：
            </td>
            <td>
                <asp:TextBox ID="txtUpAccount" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                账期：
            </td>
            <td>
                <asp:TextBox ID="txtZhangQi" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                发票号:
            </td>
            <td>
                <asp:Label ID="lblFPNo" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblFPId" runat="server" Text="0" Visible="False"></asp:Label>
                
                  <asp:Label ID="Label1" runat="server" Text="发票金额:" style="margin-left:20px;" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblInvoiceTotal" runat="server" Text=""  ForeColor="Red"></asp:Label>
            </td>
             <td>
                <asp:Label ID="LastPayTotal" runat="server" Text="原预付款:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLastPayTotal" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Height="100px" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
            </td>
            <td colspan="6">
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
            <td colspan="6">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click" OnClientClick="return confirm('确定要提交吗？')"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False">
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            类型
                        </td>
                        <td>
                            单据号
                        </td>
                        <td>
                            申请人
                        </td>
                        <td>
                            申请日期
                        </td>
                        <td>
                            到款日期
                        </td>
                        <td>
                            金额
                        </td>
                        <td>
                            上期账期系数
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
                            备注
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
                <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PoName" HeaderText="项目名称" SortExpression="PoName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="BusTypeStr" HeaderText="类型" SortExpression="BusTypeStr"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DaoKuanDate" HeaderText="到款日期" SortExpression="DaoKuanDate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Total" HeaderText="到款金额" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FPNo" HeaderText="原发票号" SortExpression="FPNo" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-CssClass="item" />
                 <asp:BoundField DataField="NewFPNo" HeaderText="新发票号" SortExpression="NewFPNo" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-CssClass="item" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle ForeColor="Black" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
