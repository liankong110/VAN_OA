<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="MyWFQuotePriceList.aspx.cs"
    Inherits="VAN_OA.ReportForms.MyWFQuotePriceList" MasterPageFile="~/DefaultMaster.Master"
    Title="报价单列表" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
<script type="text/javascript">
    function checkAll() {
        if (document.getElementById('<%= cbAll.ClientID %>').checked == false) {
            document.getElementById('<%= ddlCompany.ClientID %>').value = "苏州万邦电脑系统有限公司";
        }
        else {
            document.getElementById('<%= ddlCompany.ClientID %>').value = "全部";
        }
          
    }
</script>
    <style>
        table
        {
            border-collapse: collapse;
            border-spacing: 0;
            border-left: 1px solid #888;
            border-top: 1px solid #888;
        }
        th, td
        {
            border-right: 1px solid #888;
            border-bottom: 1px solid #888;
            padding: 1px 1px;
        }
        th
        {
            font-weight: bold;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                报价单列表
            </td>
        </tr>
        <tr>
            <td>
                日期:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                客户名称
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                客户联系人:
            </td>
            <td>
                <asp:TextBox ID="txtLinkMan" runat="server"></asp:TextBox>
            </td>
            <td>
                报价单号:
            </td>
            <td>
                <asp:TextBox ID="txtProno" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                客户地址:
            </td>
            <td>
                <asp:TextBox ID="txtGuestAddress" runat="server"></asp:TextBox>
            </td>
            <td>
                产品名称:
            </td>
            <td>
                <asp:TextBox ID="txtGoodName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                产品型号:
            </td>
            <td>
                <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>
                 报价摘要: <asp:TextBox ID="txtZhaiYao" runat="server"></asp:TextBox>
            </td>
            <td>
                品牌:
            </td>
            <td>
                <asp:TextBox ID="txtGoodBrand" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                AE:
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
                类别:
                    <asp:DropDownList ID="ddlType" runat="server" Width="100PX">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="含税" Value="1"></asp:ListItem>
                         <asp:ListItem Text="不含税" Value="2"></asp:ListItem>
                              <asp:ListItem Text="工程" Value="3"></asp:ListItem>                  
                </asp:DropDownList>公司:
                  <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComName"
                    Width="200PX">
                </asp:DropDownList>
                <asp:CheckBox ID="cbAll" Text="全部" runat="server" onchange="checkAll()" />
            </td>
            <td colspan="2">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="添加" BackColor="Yellow" OnClick="btnAdd_Click" /></div>
            </td>
        </tr>
    </table>
    <br>
    <asp:Label ID="lblMess" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand">
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        日期
                    </td>
                    <td>
                        报价单号
                    </td>
                    <td>
                        报价概要
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td>
                        总价
                    </td>
                    <td>
                        AE
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
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText=" 复制">
                <ItemTemplate>
                    <asp:ImageButton ID="btnCopy" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Copy" CommandArgument='<%# Eval("Id") %>'
                        AlternateText="复制" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                        CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"/>
            </asp:TemplateField>
              <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="QuoteDate" HeaderText="日期" SortExpression="QuoteDate"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                   <asp:BoundField DataField="QPTypeString" HeaderText=" 类别" SortExpression="QPTypeString"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="报价单号">
                <ItemTemplate>
                    <asp:Label ID="lblQuoteNo" runat="server" Text='<%# Eval("QuoteNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Remark" HeaderText="报价概要" SortExpression="Remark" ItemStyle-HorizontalAlign="Left"
                />
        <asp:BoundField DataField="AllName" HeaderText="单位" SortExpression="AllName" ItemStyle-HorizontalAlign="Left"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="Total" HeaderText="总价" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateUserName" HeaderText="AE" SortExpression="CreateUserName"
                ItemStyle-HorizontalAlign="Center" />

                  <asp:TemplateField HeaderText="导出">
                <ItemTemplate>
                    <asp:ImageButton ID="btnPDF" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="PDF"
                        AlternateText=" PDF" Visible="false" CommandArgument='<%# Eval("Id") %>' OnClientClick="state()" />
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="PDF" CommandArgument='<%# Eval("Id") %>'>PDF</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="导出">
                <ItemTemplate>
                    <asp:ImageButton ID="btnWord" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Word"
                        AlternateText="Word" Visible="false" CommandArgument='<%# Eval("Id") %>' OnClientClick="state()" />
                    <asp:LinkButton ID="lbtnWord" runat="server" CommandName="Word" CommandArgument='<%# Eval("Id") %>'>Word</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
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
