<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovePAFormDetail.aspx.cs"
    Inherits="VAN_OA.Performance.ApprovePAFormDetail" MasterPageFile="~/DefaultMaster.Master"
    Title="绩效考核模版管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                绩效考核评估信息
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                姓名
            </td>
            <td class="style1">
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
            <td class="style1">
                部门
            </td>
            <td>
                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
            </td>
            <td>
                评定月份
            </td>
            <td>
                <asp:Label ID="lblMonth" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                出勤天数
            </td>
            <td class="style1">
                <asp:Label ID="lblAttendDays" runat="server" Visible="False"></asp:Label>
                <asp:TextBox ID="txtAttendDays" runat="server" Enabled="False" Visible="False"></asp:TextBox>
            </td>
            <td class="style1">
                假期
            </td>
            <td>
                <asp:Label ID="lblLeaveDays" runat="server" Visible="False"></asp:Label>
                <asp:TextBox ID="txtLeaveDays" runat="server" Enabled="False" Visible="False"></asp:TextBox>
            </td>
            <td>
                全勤奖
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
                                    选择
                                </td>
                                <td>
                                    科目
                                </td>
                                <td>
                                    评分项
                                </td>
                                <td>
                                    分值
                                </td>
                                <td>
                                    奖惩金额
                                </td>
                                <td>
                                    是否初评
                                </td>
                                <td>
                                    初评人
                                </td>
                                <td>
                                    是否复评
                                </td>
                                <td>
                                    初评人
                                </td>
                                <td>
                                    众评进度
                                </td>
                                <td>
                                    众评分值
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
                        <asp:BoundField DataField="PASectionName" HeaderText="科目" />
                        <asp:BoundField HeaderText="评分项" DataField="PAItemName"></asp:BoundField>
                        <asp:BoundField DataField="PAItemScore" HeaderText="分值" />
                        <asp:BoundField DataField="FirstReviewUserName" HeaderText="初评人" />
                        <asp:TemplateField HeaderText="初评值">
                            <ItemTemplate>
                                <asp:Label ID="lblFirstReviewScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstReviewScore")%>'></asp:Label>
                                <asp:TextBox ID="txtFirstReviewScore" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "FirstReviewScore")%>'></asp:TextBox><asp:RegularExpressionValidator
                                    ID="revFirstReviewScore" runat="server" ErrorMessage="请输入数字" ControlToValidate="txtFirstReviewScore"
                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="ravFirstReviewScore" runat="server" 
                                    ControlToValidate="txtFirstReviewScore" ErrorMessage="不得大于分值" 
                                    MaximumValue='<%# DataBinder.Eval(Container.DataItem, "PAItemScore")%>' Type="Double" MinimumValue="-10000"></asp:RangeValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SecondReviewUserName" HeaderText="复评人" />
                        <asp:TemplateField HeaderText="复评值">
                            <ItemTemplate>
                                <asp:Label ID="lblSecondReviewScore" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SecondReviewScore")%>'></asp:Label>
                                <asp:TextBox ID="txtSecondReviewScore" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "SecondReviewScore")%>'></asp:TextBox><asp:RegularExpressionValidator
                                    ID="revSecondReviewScore" runat="server" ErrorMessage="请输入数字" ControlToValidate="txtSecondReviewScore"
                                    ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator></asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="ravSecondReviewScore" runat="server" 
                                    ControlToValidate="txtSecondReviewScore" ErrorMessage="不得大于分值" 
                                    MaximumValue='<%# DataBinder.Eval(Container.DataItem, "PAItemScore")%>' Type="Double" MinimumValue="-10000"></asp:RangeValidator></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖惩金额">
                            <ItemTemplate>
                                <asp:Label ID="lblPAItemAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReviewAmount")%>'></asp:Label><asp:TextBox
                                    ID="txtPAItemAmount" runat="server" Width="30px" Text='<%# DataBinder.Eval(Container.DataItem, "ReviewAmount")%>'></asp:TextBox><asp:RegularExpressionValidator
                                        ID="revAmountScore" runat="server" ErrorMessage="请输入数字" ControlToValidate="txtPAItemAmount"
                                        ValidationExpression="^\d+(\.\d+)?$"></asp:RegularExpressionValidator> </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="众评进度" DataField="MultiProgress" />
                        <asp:HyperLinkField HeaderText="众评分值" DataTextField="MultiScore" DataNavigateUrlFields="PAFormID,PAItemID"
                            DataNavigateUrlFormatString="MyPAFormMulti.aspx?PAFormID={0}&PAItemID={1}" />
                        <asp:TemplateField HeaderText="注释">
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
                <asp:Button ID="btnSave" runat="server" Text="保存" BackColor="Yellow" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnSet" runat="server" Text=" 重置 " BackColor="Yellow" OnClick="btnSet_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="6" align="left">
                全部分数打完后点击保存，保存完毕后进入下一个流程。
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
