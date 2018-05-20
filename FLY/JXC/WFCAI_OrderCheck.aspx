<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCAI_OrderCheck.aspx.cs"
    Inherits="VAN_OA.JXC.WFCAI_OrderCheck" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="订单报批表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                采购订单检验-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                申请人：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>
                检验人：
            </td>
            <td>
                <asp:TextBox ID="txtCheckPer" runat="server" Width="200px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetUserName" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtCheckPer">
                </cc1:AutoCompleteExtender>
                <font style="color: Red">*</font>
            </td>
            <td>
                检验时间:
            </td>
            <td>
                <asp:TextBox ID="txtCheckTime" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtCheckTime">
                </cc1:CalendarExtender>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>
                备注：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtCheckRemark" runat="server" Width="95%"></asp:TextBox>
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
                <asp:LinkButton ID="lbtnAddFiles" runat="server" OnClientClick="javascript:window.showModalDialog('DioCaiPOList.aspx',null,'dialogWidth:1000px;dialogHeight:550px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
      添加文件</asp:LinkButton>
                <br />
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" ShowFooter="true">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    删除
                                </td>
                                <td>
                                    项目编码
                                </td>
                                <td>
                                    项目名称
                                </td>
                                <td>
                                    请购人
                                </td>
                                <td>
                                    客户名称
                                </td>
                                <td>
                                    供应商
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
                                    单价
                                </td>
                                <td>
                                    金额
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
                        <asp:BoundField DataField="CaiProNo" HeaderText="单据编码" SortExpression="CaiProNo" />
                        <asp:TemplateField HeaderText="项目编码">
                            <ItemTemplate>
                                <asp:Label ID="lblPONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblPONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" />
                        <asp:BoundField DataField="QingGou" HeaderText="请购人" SortExpression="QingGou" />
                       
                        <asp:BoundField DataField="SupplierName" HeaderText="供应商" SortExpression="SupplierName" />
                        <asp:BoundField DataField="GoodAreaNumber" HeaderText="仓位" SortExpression="GoodAreaNumber" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" />
                        <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" />
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" />                       
                        <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNum" runat="server" Text='<%# Eval("CheckNum") %>' Width="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtNum"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtNum" ValidationExpression="^[0-9]+(.[0-9]{2})?$"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("CheckNum") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="lblVisNum" runat="server" Text='<%# Eval("CheckNum") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblVisNum" runat="server" Text='<%# Eval("CheckNum") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单价">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("CheckPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("CheckPrice") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="采购人">
                            <ItemTemplate>
                                <asp:Label ID="lblCaiGouPer" runat="server" Text='<%# Eval("CaiGouPer") %>'></asp:Label>
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
                &nbsp; &nbsp; &nbsp;                 <asp:Button ID="Button1" runat="server" Text="补预付转支付" OnClick="Button1_Click1"  />
                &nbsp; &nbsp;
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
