<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSell_OrderFP.aspx.cs"
    Inherits="VAN_OA.JXC.WFSell_OrderFP" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="订单报批表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

    <script type="text/javascript"> 
    
 function CheckId() {
    
     var sl = document.getElementById('<%= txtSupplier.ClientID %>').value;
   
     if (sl != "") {
         sl = sl.replace(/\\/g, ",");
         var arr = sl.split(',');

         if (arr.length == 4) { 
             document.getElementById('<%= txtSupplier.ClientID %>').value = arr[1];           
         }
     }
 }
 
    </script>

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                销售发票<asp:Label ID="lblDelete" runat="server" Text="删除" ForeColor="Red" Visible="false"></asp:Label>-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>

                 <asp:Label ID="lbl12" runat="server" Text="预付款结转" ForeColor="Red"></asp:Label>
                  <asp:TextBox  ID="txtZhuanJie" runat="server" Enabled="false" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                制单人：
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font><asp:Label ID="lblHiddGuid" runat="server" Text="" Visible="false"></asp:Label><asp:Label ID="lblHiddFpGuid" runat="server" Text="" Visible="false"></asp:Label>
            </td>
            <td>
                日期:
            </td>
            <td>
                <asp:TextBox ID="txtRuTime" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRuTime"
                    Format="yyyy-MM-dd hh:mm:ss" PopupButtonID="Image1">
                </cc1:CalendarExtender>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>
                项目编码：
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <font style="color: Red">*</font>
                <asp:LinkButton ID="lbtnAddFiles" runat="server" OnClientClick="javascript:window.showModalDialog('DioSellOutOrderToFPList.aspx',null,'dialogWidth:900px;dialogHeight:520px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
      选择</asp:LinkButton>
            </td>
            <td>
                经手人:
            </td>
            <td>
                <asp:TextBox ID="txtDoPer" runat="server" Width="200px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetUserName" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtDoPer">
                </cc1:AutoCompleteExtender>
                <font style="color: Red">*</font><asp:HiddenField ID="hfZhengFu" Value="1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                项目名称:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
            </td>
            <td>
                发票类型：
            </td>
            <td>
                <asp:DropDownList ID="dllFPstye" runat="server" Width="200px" DataValueField="FpType"
                    DataTextField="FpType">
                    <%-- <asp:ListItem>增值税发票</asp:ListItem>
                  <asp:ListItem>普通发票</asp:ListItem>
                   <asp:ListItem>其它</asp:ListItem>--%>
                </asp:DropDownList>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>
                客户名称：
            </td>
            <td>
                <asp:TextBox ID="txtSupplier" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                发票号:
            </td>
            <td>
                <asp:TextBox ID="txtFPNo" runat="server" Width="200px" AutoPostBack="True" OnTextChanged="txtFPNo_TextChanged"></asp:TextBox>
                <font style="color: Red">*</font>
                <asp:Label ID="lblTopFPNo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblTopTotal" runat="server" Visible="False"></asp:Label>
                 <asp:Label ID="Label1" runat="server" Text="发票金额:" style="margin-left:10px;" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblInvoiceTotal" runat="server" Text=""  ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="95%" TextMode="MultiLine" Height="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="left" style="width: 100%;">

                    <asp:Label ID="lblTopMess" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnReCala" runat="server" Text="重新计算" BackColor="Yellow" Width="100px"
                    OnClick="btnReCala_Click" />
                <%
                    System.Data.DataTable dt = null;
                    if (ViewState["Diff"] != null && ViewState["oriModel"] != null)
                    {
                        dt = ViewState["Diff"] as System.Data.DataTable;
                        if (dt.Rows.Count == 1)
                        {
                            VAN_OA.Model.JXC.Sell_OrderFP model = ViewState["oriModel"] as VAN_OA.Model.JXC.Sell_OrderFP;
                %>
                <table cellpadding="0" cellspacing="0" width="50%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                        </td>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            原发票
                        </td>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            新发票
                        </td>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            金额差异
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            号码
                        </td>
                        <td>
                            <%=dt.Rows[0]["FPNo"]%>
                        </td>
                      
                        <td>  <%=model.FPNo%>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            金额
                        </td>
                        <td>
                            <%=dt.Rows[0]["Total"]%>
                        </td>
                        <td>
                            <%=model.Total%>
                        </td>
                        <td>
                            <%= (model.Total - Convert.ToDecimal(dt.Rows[0]["Total"]))%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            类型
                        </td>
                        <td>
                            <%=dt.Rows[0]["FPNoStyle"]%>
                        </td>
                        <td>
                            <%=model.FPNoStyle%>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px; background-color: #336699; color: White;">
                            项目编号
                        </td>
                        <td>
                            <%=dt.Rows[0]["PONo"]%>
                        </td>
                        <td>
                            <%=model.PONo%>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <%
                    }
                    }
                       
                %>
                <br />
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" ShowFooter="true">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    编辑
                                </td>
                                <td>
                                    删除
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
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                                    CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="40px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SellOutPONO" HeaderText="出库单号" SortExpression="SellOutPONO"
                            Visible="false" />
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
                        <%--   <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />--%>
                        <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNum" runat="server" Text='<%# Eval("GoodNum") %>' Width="50px"></asp:TextBox>
                            </ItemTemplate>
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
                        <asp:TemplateField HeaderText="销售单价">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售单价">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'
                                    Width="80px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="txtCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'
                                    Width="80px"></asp:Label>
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
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>' Style="width: 100%;"></asp:TextBox>
                            </ItemTemplate>
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
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" OnClientClick="return confirm('确定要提交吗？')" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
</asp:Content>
