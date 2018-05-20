<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyList.aspx.cs" Inherits="VAN_OA.BaseInfo.CompanyList"
    MasterPageFile="~/DefaultMaster.Master" Title="公司信息列表" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                公司信息列表
            </td>
        </tr>
        <tr>
            <td>
                公司代码：
            </td>
            <td>
                <asp:TextBox ID="txtComCode" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                公司名称：
            </td>
            <td>
                <asp:TextBox ID="txtComName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="添加公司信息" BackColor="Yellow" OnClick="btnAdd_Click" />
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
                        流水号
                    </td>
                    <td height="25" align="center">
                        公司代码
                    </td>
                    <td height="25" align="center">
                        公司名称
                    </td>
                    <td height="25" align="center">
                        公司简称
                    </td>
                    <td height="25" align="center">
                        住所
                    </td>
                    <td height="25" align="center">
                        类型
                    </td>
                    <td height="25" align="center">
                        电话
                    </td>
                    <td height="25" align="center">
                        传真
                    </td>
                    <td height="25" align="center">
                        信用代码
                    </td>
                    <td height="25" align="center">
                        法人
                    </td>
                    <td height="25" align="center">
                        注册资本
                    </td>
                    <td height="25" align="center">
                        成立日期
                    </td>
                    <td height="25" align="center">
                        经营期限起始
                    </td>
                    <td height="25" align="center">
                        经营期限结束
                    </td>
                      <td height="25" align="center">
                        经营范围
                    </td>
                      <td height="25" align="center">
                        开户行
                    </td>
                      <td height="25" align="center">
                        帐号
                    </td>
                      <td height="25" align="center">
                        对外邮箱
                    </td>
                      <td height="25" align="center">
                        公司网址
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
            <asp:BoundField DataField="ComId" HeaderText="流水号" SortExpression="ComId" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ComCode" HeaderText="公司代码" SortExpression="ComCode" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ComName" HeaderText="公司名称" SortExpression="ComName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ComSimpName" HeaderText="公司简称" SortExpression="ComSimpName"
                ItemStyle-HorizontalAlign="Center" />
                  <asp:BoundField DataField="OrderByIndex" HeaderText="排序" SortExpression="OrderByIndex"
                ItemStyle-HorizontalAlign="Center" />
                	<asp:BoundField DataField="ZhuSuo" HeaderText="住所" SortExpression="ZhuSuo" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="LeiXing" HeaderText="类型" SortExpression="LeiXing" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="DianHua" HeaderText="电话" SortExpression="DianHua" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ChuanZhen" HeaderText="传真" SortExpression="ChuanZhen" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="XinYongCode" HeaderText="信用代码" SortExpression="XinYongCode" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="FaRen" HeaderText="法人" SortExpression="FaRen" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ZhuCeZiBen" HeaderText="注册资本" SortExpression="ZhuCeZiBen" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="CreateTime" HeaderText="成立日期" SortExpression="CreateTime" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}"  /> 
		<asp:BoundField DataField="StartTime" HeaderText="经营期限起始" SortExpression="StartTime" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:yyyy-MM-dd}"/> 
		<asp:BoundField DataField="EndTime" HeaderText="经营期限结束" SortExpression="EndTime" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:yyyy-MM-dd}"/> 
		<asp:BoundField DataField="FanWei" HeaderText="经营范围" SortExpression="FanWei" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="KaiHuHang" HeaderText="开户行" SortExpression="KaiHuHang" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="KaHao" HeaderText="帐号" SortExpression="KaHao" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="Email" HeaderText="对外邮箱" SortExpression="Email" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="ComUrl" HeaderText="公司网址" SortExpression="ComUrl" ItemStyle-HorizontalAlign="Center"  /> 
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
