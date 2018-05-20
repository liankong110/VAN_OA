<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_PAUserTemplateCmd.aspx.cs"
    Inherits="VAN_OA.Performance.A_PAUserTemplateCmd" MasterPageFile="~/DefaultMaster.Master"
    Title="��Ч����ģ�����" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                ��Ч����ģ����Ϣ
            </td>
        </tr>
        <tr>
            <td class="style1">
                �û����ƣ�
            </td>
            <td>
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                <asp:Button ID="btnSelectAll" runat="server" Text="ȫѡ" BackColor="Yellow" OnClick="btnSelectAll_Click" />
                <asp:Button ID="btnUnSelectAll" runat="server" Text="ȫ��ѡ" BackColor="Yellow" OnClick="btnUnSelectAll_Click" />
                <asp:Button ID="btnDisSelect" runat="server" Text="��ѡ" BackColor="Yellow" OnClick="btnDisSelect_Click" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td>
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="PAItemID" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    OnRowCommand="gvList_RowCommand">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    �༭
                                </td>
                                <td>
                                    ѡ��
                                </td>
                                <td>
                                    ��Ŀ
                                </td>
                                <td>
                                    ������
                                </td>
                                <td>
                                    ��ֵ
                                </td>
                                <td>
                                    ���ͽ��
                                </td>
                                <td>
                                    �Ƿ����
                                </td>
                                <td>
                                    ������
                                </td>
                                <td>
                                    �Ƿ���
                                </td>
                                <td>
                                    ������
                                </td>
                                <td>
                                    �Ƿ�����
                                </td>
                                <td>
                                    ������
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" align="center" style="height: 80%">
                                    ---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="ѡ��">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect" runat="server" Checked='<%# (DataBinder.Eval(Container.DataItem, "PAItemID").ToString()!=""? true:false)%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="��ʾ˳��">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSequence" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="��Ŀ">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlPASection" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="������" DataField="A_PAItemName"></asp:BoundField>
                        <asp:TemplateField HeaderText="��ֵ">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPAItemScore" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "PAItemScore")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���ͽ��">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPAItemAmount" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "PAItemAmount")%>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�Ƿ����">
                            <ItemTemplate>
                               <dx:ASPxCheckBox ID="ASPxcbFirstReview" Checked='<%# (DataBinder.Eval(Container.DataItem, "IsFirstReview").ToString()=="True"? true:false)%>' runat="server">
                                </dx:ASPxCheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="������">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlFirstReviewUserID" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�Ƿ���">
                            <ItemTemplate>
                                <dx:ASPxCheckBox
                                        ID="ASPxcbSecondReview" Checked='<%# (DataBinder.Eval(Container.DataItem, "IsSecondReview").ToString()=="True"? true:false)%>' runat="server">
                                    </dx:ASPxCheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="������">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSecondReviewUserID" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�Ƿ�����">
                            <ItemTemplate>
                                <dx:ASPxCheckBox
                                        ID="ASPxcbMultiReview" Checked='<%# (DataBinder.Eval(Container.DataItem, "IsMultiReview").ToString()=="True"? true:false)%>' runat="server">
                                    </dx:ASPxCheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="������">
                            <ItemTemplate>
                                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td rowspan="3">
                                            <dx:ASPxListBox ID="albMultiReview" runat="server" SelectionMode="CheckColumn">
                                            </dx:ASPxListBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnReviewSelectAll" runat="server" BackColor="Yellow" Text="ȫ" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                CommandName="MultiSelectAll" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnReviewUnSelectAll" runat="server" BackColor="Yellow" Text="��"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="MultiUnSelectAll" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnReviewDisSelect" runat="server" BackColor="Yellow" Text="��" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                CommandName="MultiDisSelectAll" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                &nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" �޸� " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnSet" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnSet_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
            width: 70px;
        }
    </style>
</asp:Content>
