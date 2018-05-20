<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_PATemplateCmd1.aspx.cs"
    Inherits="VAN_OA.Performance.A_PATemplateCmd1" MasterPageFile="~/DefaultMaster.Master"
    Title="绩效考核模版管理" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                绩效考核模版信息
            </td>
        </tr>
        <tr>
            <td class="style1">
                模版名称：
            </td>
            <td>
                <asp:TextBox ID="txtPATemplateName" runat="server" Width="300px"></asp:TextBox>
                <asp:Button ID="btnNameUpdate" runat="server" Text="保存名称" BackColor="Yellow" 
                    OnClick="btnNameUpdate_Click" />
                <asp:Button ID="btnClear" runat="server" Text="清空" BackColor="Yellow" 
                    onclick="btnClear_Click" />
                <asp:Button ID="btnInsert" runat="server" Text="插入" BackColor="Yellow" 
                    OnClick="btnInsert_Click" />
                <asp:Button ID="btnModify" runat="server" Text="更新" BackColor="Yellow" 
                    OnClick="btnModify_Click" />
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="2">
                <asp:UpdatePanel ID="upPAItem" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="1" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                    顺序
                                </td>
                                <td>
                                    科目
                                </td>
                                <td>
                                    评分项
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbClass" runat="server" Visible="False"></asp:Label>
                                    <asp:DropDownList ID="ddlSequence" runat="server" Height="16px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPASection" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPAItem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPAItem_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table border="1" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
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
                                <td class="style4">
                                    是否复评
                                </td>
                                <td>
                                    复评人
                                </td>
                                <td>
                                    是否众评
                                </td>
                                <td class="style3">
                                    众评人
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtPAItemScore" runat="server" Width="30px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPAItemAmount" runat="server" Width="30px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbFirstReview" runat="server" OnCheckedChanged="cbFirstReview_CheckedChanged"
                                        AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFirstReviewUserID" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="style4">
                                    <asp:CheckBox ID="cbSecondReview" runat="server" AutoPostBack="True" OnCheckedChanged="cbSecondReview_CheckedChanged" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSecondReviewUserID" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbMultiReview" runat="server" AutoPostBack="True" OnCheckedChanged="cbMultiReview_CheckedChanged" />
                                </td>
                                <td class="style3">
                                    <asp:Panel ID="plMulti" runat="server" Height="100px" ScrollBars="Vertical" Width="100px">
                                        <asp:CheckBoxList ID="cblMultiReviewUserID" runat="server">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="style1" colspan="2">
                &nbsp;
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="A_PATemplateID,A_PAItemID" Width="100%" AutoGenerateColumns="False" 
                    onrowediting="gvList_RowEditing" onrowdeleting="gvList_RowDeleting">
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
                                    复评人
                                </td>
                                <td>
                                    初评人
                                </td>
                                <td>
                                    是否众评
                                </td>
                                <td>
                                    众评人
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
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="编辑" CommandName="Edit"
                                    ImageUrl="~/Image/IconEdit.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDel" runat="server" AlternateText="删除" CommandName="Delete"
                                    ImageUrl="~/Image/IconDelete.gif" OnClientClick="return confirm( &quot;确定删除吗？&quot;) " />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Sequence" HeaderText="显示顺序" />
                        <asp:BoundField DataField="A_PASectionName" HeaderText="科目" />
                        <asp:BoundField HeaderText="评分项" DataField="A_PAItemName"></asp:BoundField>
                        <asp:BoundField DataField="A_PAItemScore" HeaderText="分值" />
                        <asp:BoundField DataField="A_PAItemAmount" HeaderText="奖惩金额" />
                        <asp:BoundField DataField="A_PAIsFirstReview" HeaderText="是否初评" />
                        <asp:BoundField DataField="A_PAFirstReviewUserID" HeaderText="初评人" />
                        <asp:BoundField DataField="A_PAIsSecondReview" HeaderText="是否复评" />
                        <asp:BoundField DataField="A_PASecondReviewUserID" HeaderText="复评人" />
                        <asp:BoundField DataField="A_PAIsMultiReview" HeaderText="是否众评" />
                        <asp:BoundField DataField="A_PAMultiReviewUserID" HeaderText="众评人" />
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
                &nbsp;
                <asp:Button ID="btnSet" runat="server" Text=" 重置 " BackColor="Yellow" OnClick="btnSet_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .style1
        {
        }
        .style3
        {
            width: 4px;
        }
        .style4
        {
            width: 151px;
        }
    </style>
</asp:Content>
