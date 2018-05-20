<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentList.aspx.cs" Inherits="VAN_OA.HR.PaymentList"
    MasterPageFile="~/DefaultMaster.Master" Title="人员查询" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                月度工资计算
            </td>
        </tr>
        <tr>
            <td>
                姓名
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX" AppendDataBoundItems="True">
                    <asp:ListItem Value="">--选择--</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="新增" BackColor="Yellow"/>
            </td>
        </tr>
        <tr>
            <td>
                计算年月             </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" Height="16px">
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
                <asp:Button ID="btnCalc" runat="server" Text="查询" BackColor="Yellow" OnClick="btnSelect_Click" />
                <asp:Button ID="btnExport" runat="server" Text="导出" BackColor="Yellow" OnClick="btnExport_Click" />
            </td>
        </tr>
        <tr>
            <td>
                月合计
            </td>
            <td>
                <asp:Label ID="lblMonthSummary" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br>
    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <asp:GridView ID="gvList_Temp" runat="server" BorderStyle="None" 
    Width="100%" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="年月" DataField="YearMonth" />
                <asp:BoundField DataField="Name" HeaderText="姓名" />
                <asp:BoundField DataField="Position" HeaderText="岗位" />
                <asp:BoundField HeaderText="基本工资" DataField="BasicSalary" />
                <asp:BoundField HeaderText="全勤奖" DataField="FullAttendence" />
                <asp:BoundField HeaderText="通讯费" DataField="MobileFee" />
                <asp:BoundField HeaderText="特殊奖励" DataField="SpecialAward" />
                <asp:BoundField DataField="GongLin" HeaderText="工龄工资" />
                <asp:BoundField DataField="PositionPerformance" HeaderText="岗位考核" />
                <asp:BoundField DataField="PositionFee" HeaderText="职级津贴" />
                <asp:BoundField DataField="WorkPerformance" HeaderText="工作绩效" />
                <asp:BoundField DataField="FullPayment" HeaderText="合计工资" />
                <asp:BoundField DataField="WorkDays" HeaderText="出勤天数" />
                <asp:BoundField DataField="ShouldPayment" HeaderText="应得工资" />
                <asp:BoundField DataField="UnionFee" HeaderText="工会费" />
                <asp:BoundField DataField="Deduction" HeaderText="扣款" />
                <asp:BoundField DataField="YangLaoJin" HeaderText="养老金" />
                <asp:BoundField DataField="ActualPayment" HeaderText="实发工资" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="ID,YearMonth" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
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
                        编辑
                    </td>
                    <td>
                        删除
                    </td>
                    <td height="25" align="center">
                        编号
                    </td>
                    <td height="25" align="center">
                        部门
                    </td>
                    <td height="25" align="center">
                        岗位
                    </td>
                    <td height="25" align="center">
                        姓名
                    </td>
                    <td height="25" align="center">
                        性别
                    </td>
                    <td height="25" align="center">
                        身份证号码
                    </td>
                    <td height="25" align="center">
                        私人联系方式
                    </td>
                    <td height="25" align="center">
                        家庭电话
                    </td>
                    <tr>
                        <td colspan="9" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="计算">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="编辑" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="年月" ItemStyle-HorizontalAlign="Center" DataField="YearMonth">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Position" HeaderText="岗位" SortExpression="Position" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderText="基本工资" ItemStyle-HorizontalAlign="Center" DataField="BasicSalary">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderText="全勤奖" ItemStyle-HorizontalAlign="Center" DataField="FullAttendence">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderText="通讯费" ItemStyle-HorizontalAlign="Center" DataField="MobileFee">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderText="特殊奖励" ItemStyle-HorizontalAlign="Center" DataField="SpecialAward">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderText="特殊奖励说明" DataField="SpecialAwardNote" Visible="False" />
            <asp:BoundField DataField="GongLin" ItemStyle-HorizontalAlign="Center" HeaderText="工龄工资">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PositionPerformance" ItemStyle-HorizontalAlign="Center"
                HeaderText="岗位考核">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PositionFee" ItemStyle-HorizontalAlign="Center" HeaderText="职级津贴">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="WorkPerformance" ItemStyle-HorizontalAlign="Center" HeaderText="工作绩效">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="FullPayment" ItemStyle-HorizontalAlign="Center" HeaderText="合计工资">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="WorkDays" ItemStyle-HorizontalAlign="Center" HeaderText="出勤天数">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ShouldPayment" ItemStyle-HorizontalAlign="Center" HeaderText="应得工资">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="UnionFee" ItemStyle-HorizontalAlign="Center" HeaderText="工会费">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Deduction" ItemStyle-HorizontalAlign="Center" HeaderText="扣款">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderText="扣款说明" DataField="DeductionNote" Visible="False" />
            <asp:BoundField DataField="YangLaoJin" ItemStyle-HorizontalAlign="Center" HeaderText="养老金">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="ActualPayment" ItemStyle-HorizontalAlign="Center" HeaderText="实发工资">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <div id="div" style="visibility: hidden; display: none">
    </div>
</asp:Content>
