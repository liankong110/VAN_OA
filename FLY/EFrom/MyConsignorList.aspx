<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyConsignorList.aspx.cs"
    Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.MyConsignorList" MasterPageFile="~/DefaultMaster.Master"
    Title="我的委托" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel runat="server">
            <HeaderTemplate>
                我的委托</HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td>
                            流程名称:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProType" runat="server" Width="200px" DataTextField="pro_Type"
                                DataValueField="pro_Id">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                            <asp:Button ID="btnAdd" runat="server" Text="添加委托信息" BackColor="Yellow" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
                <br>
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="con_Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
                    OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand">
                    <PagerTemplate>
                        <br />
                        <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />
                    </PagerTemplate>
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
                                    流程名称
                                </td>
                                <td>
                                    被委托人
                                </td>
                                <td>
                                    有效期
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
                        <asp:TemplateField HeaderText=" 编辑">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                                    AlternateText="编辑" />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                                    CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ProType" HeaderText="流程名称">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Consignor_Name" HeaderText="被委托人">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Youxiaoqi" HeaderText="有效期">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnState" runat="server" Text='<%# Eval("conState") %>' CommandName="state"
                                    CommandArgument='<%# Eval("con_Id") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server">
            <HeaderTemplate>
                被委托</HeaderTemplate>
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                    border="1">
                    <tr>
                        <td>
                            流程名称:
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" DataTextField="pro_Type"
                                DataValueField="pro_Id">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Button ID="Button1" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="Button1_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
                <br>
                <asp:GridView ID="GvBeiWei" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="con_Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="GvBeiWei_PageIndexChanging" OnRowDataBound="GvBeiWei_RowDataBound">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    流程名称
                                </td>
                                <td>
                                    被委托人
                                </td>
                                <td>
                                    有效期
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
                        <asp:BoundField DataField="ProType" HeaderText="流程名称">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Appper_Name" HeaderText="委托人">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Youxiaoqi" HeaderText="有效期">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
