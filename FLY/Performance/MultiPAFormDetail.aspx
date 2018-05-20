<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiPAFormDetail.aspx.cs"
    Inherits="VAN_OA.Performance.MultiPAFormDetail" MasterPageFile="~/DefaultMaster.Master"
    Title="��Ч����ģ�����" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                ��Ч����ģ����Ϣ ������������
            </td>
        </tr>
        <tr>
            <td>
                �û�
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" Height="16px" AppendDataBoundItems="True">
                    <asp:ListItem Value="">--ѡ��--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ����
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" Height="16px" AppendDataBoundItems="True">
                    <asp:ListItem Value="">--ѡ��--</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem Value="">--ѡ��--</asp:ListItem>
                    <asp:ListItem Value="01">01</asp:ListItem>
                    <asp:ListItem Value="02">02</asp:ListItem>
                    <asp:ListItem Value="03">03</asp:ListItem>
                    <asp:ListItem Value="04">04</asp:ListItem>
                    <asp:ListItem Value="05">05</asp:ListItem>
                    <asp:ListItem Value="06">06</asp:ListItem>
                    <asp:ListItem Value="07">07</asp:ListItem>
                    <asp:ListItem Value="08">08</asp:ListItem>
                    <asp:ListItem Value="09">09</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnQuery" runat="server" BackColor="Yellow" Text="��ѯ" OnClick="btnQuery_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnYuShe" runat="server" BackColor="Yellow" Text="����Ĭ��ֵ" Visible="false"
                    OnClick="btnYuShe_Click" />
                &nbsp;&nbsp;
                <asp:CheckBox ID="Chk_All" runat="server" Text="ȫ����ʾ" />
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="2">
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="PAFormId,PAItemId" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    AllowSorting="True" OnSorting="gvList_Sorting">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    ����
                                </td>
                                <td>
                                    �·�
                                </td>
                                <td>
                                    ��Ŀ
                                </td>
                                <td>
                                    ������
                                </td>
                                <td>
                                    ����ֵ
                                </td>
                                <td>
                                    ע��
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
                        <asp:BoundField DataField="loginName" HeaderText="����" SortExpression="loginName" />
                        <asp:BoundField DataField="Month" HeaderText="�·�" SortExpression="Month" />
                        <asp:BoundField DataField="PASectionName" HeaderText="��Ŀ" SortExpression="PASectionName" />
                        <asp:BoundField HeaderText="������" DataField="PAItemName" SortExpression="PAItemName">
                        </asp:BoundField>
                        <%-- <asp:BoundField DataField="PAItemScore" HeaderText="��ֵ" 
                            SortExpression="PAItemScore" />--%>
                        <asp:TemplateField HeaderText="��ֵ">
                            <ItemTemplate>
                                <asp:Label ID="lblPAItemScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PAItemScore")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����ֵ">
                            <ItemTemplate>
                                <asp:Label ID="lblMultiReviewScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReviewScore")%>'></asp:Label>
                                <asp:TextBox ID="txtMultiReviewScore" runat="server" Width="35px" Text='<%# DataBinder.Eval(Container.DataItem, "ReviewScore")%>'></asp:TextBox><asp:RegularExpressionValidator
                                    ID="revAmountScore" runat="server" ErrorMessage="����������" ControlToValidate="txtMultiReviewScore"
                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator><asp:RangeValidator
                                        ID="ravAmountScore" runat="server" ControlToValidate="txtMultiReviewScore" ErrorMessage="���ô��ڷ�ֵ"
                                        MaximumValue='<%# DataBinder.Eval(Container.DataItem, "PAItemScore")%>' Type="Double"></asp:RangeValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ע��">
                            <ItemTemplate>
                                <asp:Label ID="lblNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Note")%>'></asp:Label>
                                <asp:TextBox ID="txtNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Note")%>'></asp:TextBox>
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
            <td align="center" colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="����" BackColor="Yellow" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnSet" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnSet_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click"
                    Style="height: 26px" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
        }
    </style>
</asp:Content>
