<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFJieSuanType.aspx.cs"
    Inherits="VAN_OA.Fin.WFJieSuanType" MasterPageFile="~/DefaultMaster.Master" Title="结算类别定义" %>

<%@ Import Namespace="VAN_OA.Model.BaseInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td style="height: 20px; background-color: #336699; color: White;">
                结算类别定义
            </td>
        </tr>
        <tr>
           
            <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                DataKeyNames="Id" Width="100%" AllowPaging="false" AutoGenerateColumns="False"
                OnRowDataBound="gvMain_RowDataBound"
                OnRowCommand="gvMain_RowCommand">
                <PagerTemplate>
                    <br />
                </PagerTemplate>
                <EmptyDataTemplate>
                    <table width="100%">
                        
                        <tr>
                            <td colspan="6" align="center" style="height: 80%">
                                ---暂无数据---
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                
                    <asp:TemplateField HeaderText="考核类别">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text='<%# Eval("BasePoType") %>' />
                        </ItemTemplate>
                         <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="年">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hidYeartxt" Value='<%#Eval("Year")%>' />
                            <asp:DropDownList ID="dllYear" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="功能" >
                        <ItemTemplate>
                            <asp:Button ID="btnSelect" runat="server" Text=" 保存 " BackColor="Yellow" CommandName="Edit" />
                        </ItemTemplate>
                        <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
                <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                    HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                <RowStyle CssClass="InfoDetail1" />
            </asp:GridView>
        </tr>
    </table>
</asp:Content>
