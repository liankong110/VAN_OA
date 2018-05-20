<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovePAFormDetail.aspx.cs"
    Inherits="VAN_OA.Performance.ApprovePAFormDetail" MasterPageFile="~/DefaultMaster.Master"
    Title="��Ч����ģ�����" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                ��Ч����������Ϣ
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                ����
            </td>
            <td class="style1">
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
            <td class="style1">
                ����
            </td>
            <td>
                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
            </td>
            <td>
                �����·�
            </td>
            <td>
                <asp:Label ID="lblMonth" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                ��������
            </td>
            <td class="style1">
                <asp:Label ID="lblAttendDays" runat="server" Visible="False"></asp:Label>
                <asp:TextBox ID="txtAttendDays" runat="server" Enabled="False" Visible="False"></asp:TextBox>
            </td>
            <td class="style1">
                ����
            </td>
            <td>
                <asp:Label ID="lblLeaveDays" runat="server" Visible="False"></asp:Label>
                <asp:TextBox ID="txtLeaveDays" runat="server" Enabled="False" Visible="False"></asp:TextBox>
            </td>
            <td>
                ȫ�ڽ�
            </td>
            <td>
                <asp:Label ID="lblFullAttendBonus" runat="server"></asp:Label>
                <asp:TextBox ID="txtFullAttendBonus" runat="server" Enabled="False" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="6">
                &nbsp;
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="PAFormID,PAItemId,FirstReviewUserID,SecondReviewUserID" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    ShowFooter="True">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
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
                                    ��������
                                </td>
                                <td>
                                    ������ֵ
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
                        <asp:BoundField DataField="PASectionName" HeaderText="��Ŀ" />
                        <asp:BoundField HeaderText="������" DataField="PAItemName"></asp:BoundField>
                        <asp:BoundField DataField="PAItemScore" HeaderText="��ֵ" />
                        <asp:BoundField DataField="FirstReviewUserName" HeaderText="������" />
                        <asp:TemplateField HeaderText="����ֵ">
                            <ItemTemplate>
                                <asp:Label ID="lblFirstReviewScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstReviewScore")%>'></asp:Label>
                                <asp:TextBox ID="txtFirstReviewScore" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "FirstReviewScore")%>'></asp:TextBox><asp:RegularExpressionValidator
                                    ID="revFirstReviewScore" runat="server" ErrorMessage="����������" ControlToValidate="txtFirstReviewScore"
                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="ravFirstReviewScore" runat="server" 
                                    ControlToValidate="txtFirstReviewScore" ErrorMessage="���ô��ڷ�ֵ" 
                                    MaximumValue='<%# DataBinder.Eval(Container.DataItem, "PAItemScore")%>' Type="Double" MinimumValue="-10000"></asp:RangeValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SecondReviewUserName" HeaderText="������" />
                        <asp:TemplateField HeaderText="����ֵ">
                            <ItemTemplate>
                                <asp:Label ID="lblSecondReviewScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SecondReviewScore")%>'></asp:Label>
                                <asp:TextBox ID="txtSecondReviewScore" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "SecondReviewScore")%>'></asp:TextBox><asp:RegularExpressionValidator
                                    ID="revSecondReviewScore" runat="server" ErrorMessage="����������" ControlToValidate="txtSecondReviewScore"
                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator></asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="ravSecondReviewScore" runat="server" 
                                    ControlToValidate="txtSecondReviewScore" ErrorMessage="���ô��ڷ�ֵ" 
                                    MaximumValue='<%# DataBinder.Eval(Container.DataItem, "PAItemScore")%>' Type="Double" MinimumValue="-10000"></asp:RangeValidator></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���ͽ��">
                            <ItemTemplate>
                                <asp:Label ID="lblPAItemAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReviewAmount")%>'></asp:Label><asp:TextBox
                                    ID="txtPAItemAmount" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "ReviewAmount")%>'></asp:TextBox><asp:RegularExpressionValidator
                                        ID="revAmountScore" runat="server" ErrorMessage="����������" ControlToValidate="txtPAItemAmount"
                                        ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator> </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="��������" DataField="MultiProgress" />
                        <asp:HyperLinkField HeaderText="������ֵ" DataTextField="MultiScore" DataNavigateUrlFields="PAFormID,PAItemID"
                            DataNavigateUrlFormatString="MyPAFormMulti.aspx?PAFormID={0}&PAItemID={1}" />
                        <asp:TemplateField HeaderText="ע��">
                            <ItemTemplate>
                                <asp:Label ID="lblNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Note")%>'></asp:Label><asp:TextBox
                                    ID="txtNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Note")%>'></asp:TextBox></ItemTemplate>
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
            <td colspan="6" align="center">
                <asp:Button ID="btnSave" runat="server" Text="����" BackColor="Yellow" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnSet" runat="server" Text=" ���� " BackColor="Yellow" OnClick="btnSet_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="6" align="left">
                ȫ����������������棬������Ϻ������һ�����̡�
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
