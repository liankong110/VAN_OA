<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="WFSupplierList.aspx.cs"
    Inherits="VAN_OA.ReportForms.WFSupplierList" MasterPageFile="~/DefaultMaster.Master"
    Title="供应商管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">供应商归类
            </td>
        </tr>
        <tr>
            <td>登记日期:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="Image1"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton1"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>供应商名称
            </td>
            <td>
                <asp:TextBox ID="txtSupplierName" runat="server" Width="300PX"></asp:TextBox>
                  <asp:CheckBox ID="cbPiPei" runat="server" Text="全匹配" />
            </td>
        </tr>
        <tr>
            <td>单据号:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" Width="300PX"></asp:TextBox>
            </td>
            <td colspan="2">显示:            
                <asp:DropDownList ID="ddlShow" runat="server" Width="100px">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="显示"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不显示"></asp:ListItem>
                </asp:DropDownList>
                特殊:            
                <asp:DropDownList ID="ddlSpecial" runat="server" Width="100px">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="特殊"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不特殊"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div style="float: left; display: inline;">
                    供应商简称: 
                    <asp:TextBox ID="txtSupplieSimpeName" runat="server"></asp:TextBox>
                    联系人: 
                    <asp:TextBox ID="txtLikeMan" runat="server"></asp:TextBox>
                    电话手机:  
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                </div>
                <div style="float: right; display: inline;">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text=" 保 存(显示) " BackColor="Yellow" OnClick="btnSave_Click" />
                    <asp:Button ID="btnSpecial" runat="server" Text=" 保 存(特殊) " BackColor="Yellow"
                        OnClick="btnSpecial_Click" />
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal" Height="100%">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Id" Width="200%" AllowPaging="True" AutoGenerateColumns="False"
            OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
            OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
            <PagerTemplate>
                <br />

            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>编辑
                        </td>
                        <td>删除
                        </td>
                        <td>单据号
                        </td>
                        <td>日期
                        </td>
                        <td>供应商名称
                        </td>
                        <td>供应商简称
                        </td>
                        <td>电话/手机
                        </td>
                        <td>联系人
                        </td>
                        <td>职务
                        </td>
                        <td>传真或邮箱
                        </td>
                        <td>是否留资料
                        </td>
                        <td>QQ/MSN联系
                        </td>
                        <td>供应商ID
                        </td>
                        <td>联系人地址
                        </td>
                        <td>联系人网址
                        </td>
                        <td>供应商税务登记号
                        </td>
                        <td>供应商工商注册号
                        </td>
                        <td>银行账号
                        </td>
                        <td>开户行
                        </td>
                        <td>备注
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---暂无数据---
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
                <asp:TemplateField HeaderText="显示">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsUse" runat="server" Checked='<% #Eval("IsUse")%>' />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="显示">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbMyIsUse" runat="server" Checked='<% #Eval("IsUse")%>' Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="特殊">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsSpecial" runat="server" Checked='<% #Eval("IsSpecial")%>' />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="特殊">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbMyIsSpecial" runat="server" Checked='<% #Eval("IsSpecial")%>' Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="myId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Time" HeaderText="登记日期" SortExpression="Time" ItemStyle-HorizontalAlign="Center" />
                   <asp:BoundField DataField="ZhuJi" HeaderText="助记词" SortExpression="ZhuJi" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierName" HeaderText="供应商名称" SortExpression="SupplierName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplieSimpeName" HeaderText="供应商简称" SortExpression="SupplieSimpeName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Phone" HeaderText="电话/手机" SortExpression="Phone" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="LikeMan" HeaderText="联系人" SortExpression="LikeMan" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Job" HeaderText="职务" SortExpression="Job" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FoxOrEmail" HeaderText="传真或邮箱" SortExpression="FoxOrEmail"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Save_Name" HeaderText="是否留资料" SortExpression="Save_Name"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="QQMsn" HeaderText="QQ/MSN联系" SortExpression="QQMsn" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierId" HeaderText="供应商ID" SortExpression="SupplierId"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierAddress" HeaderText="联系人地址" SortExpression="SupplierAddress"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierHttp" HeaderText="联系人网址" SortExpression="SupplierHttp"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierShui" HeaderText="供应商税务登记号" SortExpression="SupplierShui"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierGong" HeaderText="供应商工商注册号" SortExpression="SupplierGong"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierBrandNo" HeaderText="银行账号" SortExpression="SupplierBrandNo"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SupplierBrandName" HeaderText="开户行" SortExpression="SupplierBrandName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%" CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle" CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
</asp:Content>
