<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFSupplierInvoiceVerify.aspx.cs"
    Inherits="VAN_OA.JXC.WFSupplierInvoiceVerify" Culture="auto" UICulture="auto"
    MasterPageFile="~/DefaultMaster.Master" Title="订单报批表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                供应商付款单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                制单人：
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
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
                备注：
            </td>
            <td>
                <asp:TextBox ID="txtRemark" Width="100%" runat="server"></asp:TextBox>
            </td>
             <td colspan="2">原票据号：             
                  <asp:TextBox ID="txtFristFPNo" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                新票据号:
                  <asp:TextBox ID="txtSecondFPNo" runat="server" Width="150px" Enabled="false"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td colspan="4" style="width: 100%">
              
            </td>
        </tr>
        </table>
          <asp:Panel runat="server" ID="Panel1" ScrollBars="Horizontal" Width="100%">
                    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                        DataKeyNames="Ids" Width="170%" AllowPaging="False" AutoGenerateColumns="False"
                        ShowFooter="true" OnRowDataBound="gvList_RowDataBound" Font-Size="11px">
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
                                        采购日期
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
                                        实采单价
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
                                        已付款
                                    </td>
                                    <td>
                                        支付金额
                                    </td>
                                    <td>
                                        采购人
                                    </td>
                                    <td>
                                        支付
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
                            <asp:BoundField DataField="ProNo" HeaderText="入库单号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%"/>
                            <asp:BoundField DataField="RuTime" HeaderText="入库日期" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%"
                                DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="GuestName" HeaderText="供应商" SortExpression="GuestName"
                                ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="3%" />
                        <%--    <asp:BoundField DataField="houseName" HeaderText="仓库" SortExpression="houseName"  HeaderStyle-Width="3%"
                                ItemStyle-HorizontalAlign="Center" Visible="false" />--%>
                            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="3%"/>
                            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="5%" />
                            <asp:BoundField DataField="POGuestName" HeaderText="客户名称" SortExpression="POGuestName"
                                ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="POAE" HeaderText="AE" SortExpression="POAE" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="2%"/>
                            <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="2%"/>
                            <asp:BoundField DataField="GoodName"   ItemStyle-Width="5%" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName"  HeaderStyle-Width="3%"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-Width="5%" />
                            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" />
                           <%-- <asp:BoundField DataField="SupplierInvoiceNum" HeaderText="支付数量" SortExpression="SupplierInvoiceNum" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" />--%>

                            <asp:TemplateField HeaderText="入库/支付数量" HeaderStyle-Width="3%">
                                <ItemTemplate>                                    
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label> /
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("SupplierInvoiceNum") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="支付数量" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSupplierInvoiceNum" runat="server" Text='<%# Eval("SupplierInvoiceNum") %>'
                                        Width="70px"></asp:TextBox> 
                                </ItemTemplate>
                            </asp:TemplateField>

                           <%-- <asp:BoundField DataField="supplierTuiGoodNum" HeaderText="采退数" SortExpression="supplierTuiGoodNum" HeaderStyle-Width="2%"
                                ItemStyle-HorizontalAlign="Center" />--%>
                             <asp:TemplateField HeaderText="采退数"  HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplierTuiGoodNum" runat="server" Text='<%# Eval("supplierTuiGoodNum") %>'></asp:Label>
                                </ItemTemplate>
                                 </asp:TemplateField>
                           <%-- <asp:BoundField DataField="lastGoodNum" HeaderText="净数量" SortExpression="lastGoodNum" HeaderStyle-Width="2%"
                                ItemStyle-HorizontalAlign="Center" Visible="false" />--%>
                            <asp:BoundField DataField="GoodPrice" HeaderText="实采单价" SortExpression="GoodPrice" HeaderStyle-Width="2%"
                                ItemStyle-HorizontalAlign="Center" />
                            <%--  <asp:BoundField DataField="LastTotal" HeaderText="金额" SortExpression="LastTotal"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />--%>
                            <asp:TemplateField HeaderText="剩余金额"  HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastTotal" runat="server" Text='<%# Eval("ResultTotal") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblLastTotal" runat="server" Text='<%# Eval("ResultTotal") %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                          <%--   <asp:BoundField DataField="ActPay" HeaderText="实际支付" SortExpression="ActPay" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="3%"/>--%>
                            <asp:TemplateField HeaderText="实际支付"  HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblActPay2" runat="server" Text='<%# Eval("ActPay") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblActPay2" runat="server" Text='<%# Eval("ActPay") %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                          <%--   <asp:BoundField DataField="HuiKuanLiLv" HeaderText="回款率" SortExpression="HuiKuanLiLv" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:n2}" HeaderStyle-Width="3%"/>--%>
                            
                               <asp:TemplateField HeaderText="回款率" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblHuiKuanLiLv" runat="server" Text='<%# GetNum(Eval("HuiKuanLiLv")) %>'></asp:Label>
                                </ItemTemplate>                               
                            </asp:TemplateField>

                            <asp:BoundField DataField="DoPer" HeaderText="采购人" SortExpression="DoPer" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="3%"/>
                          <%--  19--%>
                            <asp:BoundField DataField="SupplierFPNo" HeaderText="发票号" SortExpression="SupplierFPNo"
                                ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="3%"/>
                                 <asp:BoundField DataField="SupplierFpDate" HeaderText="发票日期" SortExpression="SupplierFpDate"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="3%"/>
                            <asp:BoundField DataField="SupplierInvoiceDate" HeaderText="出款日期" SortExpression="SupplierInvoiceDate"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="3%"/>
                            <asp:TemplateField HeaderText="支付金额" Visible="false" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplierInvoiceTotal1" runat="server" Text='<%# Eval("SupplierInvoiceTotal") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblSupplierInvoiceTotal1" runat="server" Text='<%# Eval("SupplierInvoiceTotal") %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发票号"  HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSupplierFPNo" runat="server" Text='<%# Eval("SupplierFPNo") %>'  
                                        Width="70px"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="aa" runat="server"
                                        ErrorMessage="*" ControlToValidate="txtSupplierFPNo"></asp:RequiredFieldValidator>--%>
                                </ItemTemplate>

                                 <HeaderTemplate>
                                    <asp:CheckBox ID="cbAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbAll_CheckedChanged" Text="发票号" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="发票日期" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSupplierFpDate" runat="server" Text='<%# GetDate(Eval("SupplierFpDate")) %>'
                                        Width="70px"></asp:TextBox>                                  
                                </ItemTemplate>
                                    <HeaderTemplate>
                                    <asp:CheckBox ID="cbAllDate" runat="server" AutoPostBack="True" OnCheckedChanged="cbAllDate_CheckedChanged" Text="发票日期" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="出款日期" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSupplierInvoiceDate" runat="server" Text='<%# GetDate(Eval("SupplierInvoiceDate")) %>'
                                        Width="70px"></asp:TextBox>
                                   <%-- <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSupplierInvoiceDate"
                                        PopupButtonID="txtSupplierInvoiceDate" Format="yyyy-MM-dd">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="aa" runat="server"
                                        ErrorMessage="*" ControlToValidate="txtSupplierInvoiceDate"></asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="支付单价" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSupplierInvoicePrice" runat="server" Text='<%# Eval("SupplierInvoicePrice") %>'
                                        Width="70px"></asp:TextBox>
                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="aa" runat="server"
                                        ErrorMessage="*" ControlToValidate="txtSupplierInvoicePrice"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="aa"
                                        runat="server" ErrorMessage="*" ControlToValidate="txtSupplierInvoicePrice" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="支付单价"  HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplierInvoicePrice" runat="server" Text='<%# Eval("SupplierInvoicePrice") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="支付金额"  HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplierInvoiceTotal" runat="server" Text='<%# Eval("SupplierInvoiceTotal") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblSupplierInvoiceTotal" runat="server" Text='<%# Eval("SupplierInvoiceTotal") %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="负数合计"  HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblFuShuTotal" runat="server" Text='<%# Eval("FuShuTotal") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFuShuTotal" runat="server" Text='<%# Eval("FuShuTotal") %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="实际支付"  HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblActPay" runat="server" Text='<%# Eval("ActPay") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblActPay" runat="server" Text='<%# Eval("ActPay") %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            
                             <asp:TemplateField HeaderText="已支付"  HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <a href="/JXC/WFSupplierInvoiceList.aspx?Ids=<%# Eval("Ids") %>" target="_blank">
                                        <%# Eval("HadSupplierInvoiceTotal")%></a>
                                </ItemTemplate>
                                    <FooterTemplate>
                                    <asp:Label ID="lblHadSupplierInvoiceTotal" runat="server" Text='<%# Eval("HadSupplierInvoiceTotal") %>'></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="供应商全称" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                  <asp:Label ID="lblSupplierAllName" runat="server" Text='<%# Eval("SupplierAllName") %>'></asp:Label> 
                            </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="含税" HeaderStyle-Width="1%" >
                                <ItemTemplate>
                                    <asp:CheckBox ID="CBIsHanShui" runat="server" Checked='<%# Eval("IsHanShui") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否检查" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CBIfCheck" runat="server" Checked='<%# Eval("IfCheck") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="PORemark" HeaderText="备注" SortExpression="PORemark" HeaderStyle-Width="200" />
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
                        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                            HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                        <RowStyle CssClass="InfoDetail1" />
                    </asp:GridView>
                </asp:Panel>
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
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
            <asp:Button ID="btnReStart" runat="server" Text="预付款转支付 重新提交" BackColor="Yellow" 
                     Visible="false" onclick="btnReStart_Click"/>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    ValidationGroup="aa" Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
</asp:Content>
