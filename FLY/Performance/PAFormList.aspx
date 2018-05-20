<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAFormList.aspx.cs" Inherits="VAN_OA.Performance.PAFormList"
    MasterPageFile="~/DefaultMaster.Master" Title="绩效考核清单" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                评估绩效考核
            </td>
        </tr>
        <tr>
            <td>
                用户
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" Height="16px" AppendDataBoundItems="True">
                    <asp:ListItem Value="">--选择--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                年月
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" Height="16px" AppendDataBoundItems="True">
                    <asp:ListItem Value="">--选择--</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMonth" runat="server">
                    <asp:ListItem Value="">--选择--</asp:ListItem>
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
                <asp:Button ID="btnQuery" runat="server" BackColor="Yellow" Text="查询" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnDeleted" runat="server" Text="批量删除" BackColor="Yellow" OnClick="btnDeleted_Click" />&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="Chk_All" runat="server" Text="全部显示" />

                
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="PAFormID" Width="100%" AutoGenerateColumns="False" OnRowEditing="gvList_RowEditing"
        OnRowDataBound="gvList_RowDataBound" OnRowDeleting="gvList_RowDeleting" AllowPaging="false"
        OnPageIndexChanging="gvList_PageIndexChanging">
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
                        详细
                    </td>
                    <td>
                        删除
                    </td>
                    <td>
                        用户名
                    </td>
                    <td>
                        职位
                    </td>
                    <td>
                        年月
                    </td>
                    <td>
                        状态
                    </td>
                    <td>
                        出勤天数
                    </td>
                    <td>
                        假期
                    </td>
                    <td>
                        全勤奖
                    </td>
                    <td>
                        初评分
                    </td>
                    <td>
                        复评分
                    </td>
                    <td>
                        众评分
                    </td>
                    <td>
                        奖惩
                    </td>
                    <td>
                        注释
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

            <asp:TemplateField HeaderText="删除">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbIsDeleted" runat="server" Text="" AutoPostBack="True" OnCheckedChanged="cbIsDeleted_CheckedChanged"
                         />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsDeleted" runat="server" Checked='<% #Eval("IsDeleted") %>'
                         />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="详细">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Approve.png"
                        CommandName="Edit" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                        CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="loginName" HeaderText="用户名">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="zhiwu" HeaderText="职位" />
            <asp:BoundField HeaderText="年月" DataField="Month" />
            <asp:BoundField DataField="Status" HeaderText="状态" />
            <asp:BoundField DataField="AttendDays" HeaderText="出勤天数" />
            <asp:BoundField DataField="LeaveDays" HeaderText="假期" />
            <asp:BoundField DataField="FullAttendBonus" HeaderText="全勤奖" />
            <asp:BoundField HeaderText="初评分" DataField="PAFirstScoreSum" HtmlEncode="False" />
            <asp:BoundField HeaderText="复评分" DataField="PASecondScoreSum" HtmlEncode="False" />
            <asp:BoundField HeaderText="众评分" DataField="PAMultiScoreSum" HtmlEncode="False" />
            <asp:BoundField HeaderText="平均值" DataField="PASumAVG" HtmlEncode="False" />
            <asp:BoundField HeaderText="奖惩" DataField="PAAmountSum" />
            <asp:BoundField HeaderText="注释" DataField="Note2" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>

    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
</asp:Content>
