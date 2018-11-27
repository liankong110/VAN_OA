<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFAEPromiseTotalList.aspx.cs"
    Inherits="VAN_OA.Fin.WFAEPromiseTotalList" MasterPageFile="~/DefaultMaster.Master"
    Title="销售指标" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">销售指标
            </td>
        </tr>
        <tr>
            <td>公司名称：
            </td>
            <td>
               <asp:DropDownList ID="ddlCompany" AutoPostBack="true" runat="server" DataTextField="ComName"
                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" DataValueField="ComCode"
                    Width="200PX">
                </asp:DropDownList>
            </td>
             <td>AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="LoginName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
             <td>年份：
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="添加项公交卡" BackColor="Yellow" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AutoGenerateColumns="False" AllowPaging="true"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>序号</td>
                    <td>编辑
                    </td>
                    <td>公司名称
                    </td>
                    <td height="25" align="center">AE
                    </td>
                    <td height="25" align="center">年份
                    </td>
                    <td height="25" align="center">承诺销售额指标
                    </td>
                    <td height="25" align="center">实际完成销售额指标
                    </td>
                    <td height="25" align="center">承诺利润
                    </td>
                    <td height="25" align="center">实际完成利润
                    </td>
                    <td height="25" align="center">利润达成率
                    </td>
                    <td height="25" align="center">新客户承诺销售额
                    </td>
                    <td height="25" align="center">新客户承诺利润
                    </td>
                    <td height="25" align="center">新客户实际利润
                    </td>
                    <tr>
                        <td colspan="4" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <Columns>
            <asp:BoundField DataField="NO" HeaderText="序号" SortExpression="NO" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="编辑" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>

            <asp:BoundField DataField="Company" HeaderText="公司名称" SortExpression="Company" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="YearNo" HeaderText="年份" SortExpression="YearNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PromiseSellTotal" HeaderText="承诺销售额指标" SortExpression="PromiseSellTotal" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ActualPromiseSellTotal" HeaderText="实际完成销售额指标" SortExpression="ActualPromiseSellTotal" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PromiseProfit" HeaderText="承诺利润" SortExpression="PromiseProfit" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ActualPromiseProfit" HeaderText="实际完成利润" SortExpression="ActualPromiseProfit" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AddGuetSellTotal" HeaderText="利润达成率" SortExpression="AddGuetSellTotal" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ActualAddGuetSellTotal" HeaderText="新客户承诺销售额" SortExpression="ActualAddGuetSellTotal" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AddGuestProfit" HeaderText="新客户承诺利润" SortExpression="AddGuestProfit" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ActualAddGuestProfit" HeaderText="新客户实际利润" SortExpression="ActualAddGuestProfit" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />

    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
</asp:Content>
