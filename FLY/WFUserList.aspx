<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFUserList.aspx.cs" Inherits="VAN_OA.WFUserList"
    MasterPageFile="~/DefaultMaster.Master" Title="用户查询" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                用户查询
            </td>
        </tr>
        <tr>
            <td>
                用户名
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
            <td>
                用户帐号
            </td>
            <td>
                <asp:TextBox ID="txtLoginId" runat="server"></asp:TextBox>
            </td>
            <td>
                编号
            </td>
            <td>
                <asp:TextBox ID="txtLoginUserNO" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                状态：
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="155PX">
                    <asp:ListItem>全部</asp:ListItem>
                    <asp:ListItem>在职</asp:ListItem>
                    <asp:ListItem>离职</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                部门：
            </td>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server" Width="155PX">
                </asp:DropDownList>
            </td>
            <td>
                公司：
            </td>
            <td>
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComCode"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                AE显示:
            </td>
            <td>
                <asp:DropDownList ID="ddlSepcList" runat="server"  Width="155PX">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="显示" Value="1"></asp:ListItem>
                    <asp:ListItem Text="不显示" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right" colspan="4">
                &nbsp;
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="添加用户" BackColor="Yellow" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound"
        OnSelectedIndexChanged="gvList_SelectedIndexChanged">
        <PagerTemplate>
            <br />
            <%--  <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />--%>
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
                        编号
                    </td>
                    <td>
                        用户账号
                    </td>
                    <td>
                        用户名称
                    </td>
                    <td>
                        状态
                    </td>
                    <td>
                        性别
                    </td>
                    <td>
                        部门
                    </td>
                    <td>
                        所属上级
                    </td>
                    <td>
                        电话
                    </td>
                    <td>
                        Email
                    </td>
                    <td>
                        公司代码
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
            <asp:BoundField DataField="LoginUserNO" HeaderText="编号">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="LoginId" HeaderText="用户账号">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="LoginName" HeaderText="用户名称">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="AE">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSpecialUser" runat="server" Enabled="false" Checked='<% #Eval("IsSpecialUser") %>' />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="LoginStatus" HeaderText="状态">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="LoginTmpPwd" HeaderText="性别">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="LoginIPosition" HeaderText="部门">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ReportToName" HeaderText="所属上级">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="LoginPhone" HeaderText="电话">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="LoginMemo" HeaderText="Email">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="LoginCreateTime" HeaderText="建档时间" DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="CompanyCode" HeaderText="公司代码">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
              <asp:BoundField DataField="SheBaoCode" HeaderText="社保代码">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
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
