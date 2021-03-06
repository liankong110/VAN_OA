﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCarInfoList.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFCarInfoList" MasterPageFile="~/DefaultMaster.Master"
    Title="发票类型列表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                车辆管理列表
            </td>
        </tr>
        <tr>
            <td>
                车牌号：
            </td>
            <td>
                <asp:TextBox ID="txtCarNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="添加车辆信息" BackColor="Yellow" OnClick="btnAdd_Click" />
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
                    <td>
                        编辑
                    </td>
                    <td>
                        删除
                    </td>

                    <td height="25" align="center">
                        车牌号
                    </td>
                    <td height="25" align="center">
                        品牌型号
                    </td>
                    <td height="25" align="center">
                        发动机号
                    </td>
                    <td height="25" align="center">
                        车辆识别代号
                    </td>
                    <td height="25" align="center">
                        车架号
                    </td>
                    <td height="25" align="center">
                        行驶证号
                    </td>
                    <td height="25" align="center">
                        年检时间
                    </td>
                    <td height="25" align="center">
                        保险时间
                    </td>
                    <td height="25" align="center">
                        油耗系数
                    </td>
                    <tr>
                        <td colspan="4" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <PagerTemplate>
            <br />
            <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
            <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                CommandName="Page" CommandArgument="First"></asp:LinkButton>
            <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
            <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                CommandName="Page" CommandArgument="Next"></asp:LinkButton>
            <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                CommandName="Page" CommandArgument="Last"></asp:LinkButton>
            <br />--%>
        </PagerTemplate>
        <Columns>
            <asp:TemplateField HeaderText=" 编辑">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="编辑" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                        CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="是否停用">               
                <ItemTemplate>
                    <asp:CheckBox ID="IsStop" runat="server" Checked='<% #Eval("IsStop") %>'   />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="CarNo" HeaderText="车牌号" SortExpression="CarNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CarModel" HeaderText="品牌型号" SortExpression="CarModel"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CarEngine" HeaderText="发动机号" SortExpression="CarEngine"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CarShiBieNO" HeaderText="车辆识别代号" SortExpression="CarShiBieNO"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CarJiaNo" HeaderText="车架号" SortExpression="CarJiaNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="NianJian" HeaderText="年检时间" SortExpression="NianJian"
                DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Baoxian" HeaderText="保险时间" SortExpression="Baoxian" DataFormatString="{0:yyyy-MM-dd}"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OilNumber" HeaderText="油耗系数" SortExpression="OilNumber"
                ItemStyle-HorizontalAlign="Center" />
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
