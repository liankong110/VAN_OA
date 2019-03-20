<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="WFInvoiceList.aspx.cs"
    Inherits="VAN_OA.KingdeeInvoice.WFInvoiceList" MasterPageFile="~/DefaultMaster.Master"
    Title="金蝶发票清单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">金蝶应收发票清单
            </td>
        </tr>
        <tr>
            <td>开具日期:
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
            <td>客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>发票编号:
            </td>
            <td>
                <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox><asp:CheckBox ID="cbInvoiceNo"
                    runat="server" Text="无发票" />
            </td>
            <td>发票金额:
            </td>
            <td>
                <asp:DropDownList ID="ddlInvTotal" runat="server">
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem>&gt;</asp:ListItem>
                    <asp:ListItem>&lt;</asp:ListItem>
                    <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                    <asp:ListItem Value="<=">&lt;=</asp:ListItem>
                    <asp:ListItem>&lt;&gt;</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtInvoice" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>到帐情况:
            </td>
            <td>
                <asp:DropDownList ID="ddlDaoZhang" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未到账</asp:ListItem>
                    <asp:ListItem Value="1">已全到帐</asp:ListItem>
                    <asp:ListItem Value="2">未全到帐</asp:ListItem>
                </asp:DropDownList>
                Isorder:
                 <asp:DropDownList ID="ddlIsorder" runat="server">
                     <asp:ListItem Value="-1">全部</asp:ListItem>
                     <asp:ListItem Value="0">真实</asp:ListItem>
                     <asp:ListItem Value="1">非真实</asp:ListItem>
                 </asp:DropDownList>
                删除标记:
                 <asp:DropDownList ID="ddlIsDeleted" runat="server">
                     <asp:ListItem Value="0">未删除</asp:ListItem>
                     <asp:ListItem Value="1">删除</asp:ListItem>
                     <asp:ListItem Value="-1">全部</asp:ListItem>

                 </asp:DropDownList>
            </td>
            <td>到款金额:
            </td>
            <td>
                <asp:DropDownList ID="ddlDaoKuanTotal" runat="server">
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem>&gt;</asp:ListItem>
                    <asp:ListItem>&lt;</asp:ListItem>
                    <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                    <asp:ListItem Value="<=">&lt;=</asp:ListItem>
                    <asp:ListItem>&lt;&gt;</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtDaoKuanTotal" runat="server"></asp:TextBox>

                到款金额
                <asp:DropDownList ID="ddlEQTotal" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem>&gt;</asp:ListItem>
                    <asp:ListItem>&lt;</asp:ListItem>
                    <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                    <asp:ListItem Value="<=">&lt;=</asp:ListItem>
                    <asp:ListItem>=</asp:ListItem>
                     <asp:ListItem><></asp:ListItem>
                </asp:DropDownList>
                发票金额
            </td>
        </tr>
        <tr>
            <td>发票日期:
            </td>
            <td>
                <asp:TextBox ID="txtBillDateFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtBillDateTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtBillDateFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtBillDateTo">
                </cc1:CalendarExtender>
            </td>
            <td colspan="2" align="right">

                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                &nbsp;
               <asp:Button ID="btnIsSelected" runat="server" Text="保存" BackColor="Yellow" OnClick="btnIsSelected_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDeleted" runat="server" Text="保存(删除)" BackColor="Yellow" OnClick="btnDeleted_Click" />&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnClear" runat="server" Text="自动清理" BackColor="Yellow" OnClick="btnClear_Click" />&nbsp;&nbsp;&nbsp;

                 <asp:Button ID="btnEdit" runat="server" Text="编辑" BackColor="Yellow" OnClick="btnEdit_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <br />

    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AutoGenerateColumns="False" AllowPaging="true"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td height="25" align="center">Isorder
                    </td>
                    <td height="25" align="center">客户名称
                    </td>
                    <td height="25" align="center">激励系数
                    </td>
                    <td height="25" align="center">金额
                    </td>
                    <td height="25" align="center">开具日期
                    </td>
                    <td height="25" align="center">到账否
                    </td>
                    <td height="25" align="center">金额
                    </td>
                    <td height="25" align="center">未到款金额 </td>
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
            <asp:TemplateField HeaderText="物理删除">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                        CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="删除标记">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbIsDeleted" runat="server" Text="删除标记 " AutoPostBack="True" OnCheckedChanged="cbIsDeleted_CheckedChanged"
                        Enabled="<%# IsDeletedEdit() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsDeleted" runat="server" Checked='<% #Eval("IsDeleted") %>'
                        Enabled="<%# IsDeletedEdit() %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Isorder ">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbIsIsorder" runat="server" Text="Isorder " AutoPostBack="True" OnCheckedChanged="cbIsSelected_CheckedChanged"
                        Enabled="<%# IsSelectedEdit() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsIsorder" runat="server" Checked='<% #Eval("Isorder") %>'
                        Enabled="<%# IsSelectedEdit() %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Isorder ">

                <ItemTemplate>
                    <asp:CheckBox ID="cbIsIsorder1" runat="server" Checked='<% #Eval("Isorder") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

          

           <asp:TemplateField  HeaderText="客户名称">
                <ItemTemplate>
                    <asp:Label ID="GuestName" runat="server" Text='<%# Eval("GuestName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="客户名称" Visible="false">
                <ItemTemplate>
                    <asp:TextBox ID="EditGuestName" runat="server" Text='<% #Eval("GuestName") %>' Width="100%"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="InvoiceNumber" HeaderText="发票号码" SortExpression="InvoiceNumber" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="FPNoStyle" HeaderText="发票类型" SortExpression="FPNoStyle" ItemStyle-HorizontalAlign="Center" />
            <%--<asp:BoundField DataField="Total" HeaderText="金额" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />--%>
              <asp:TemplateField  HeaderText="发票金额">
                <ItemTemplate>
                    <asp:Label ID="Total" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreateDate" HeaderText="开具日期" SortExpression="CreateDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="BillDate" HeaderText="发票日期" SortExpression="BillDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="IsAccountString" HeaderText="到账否" SortExpression="IsAccountString" ItemStyle-HorizontalAlign="Center" />
            <%--<asp:BoundField DataField="Received" HeaderText="到款金额" SortExpression="Received" ItemStyle-HorizontalAlign="Center" />--%>
            <asp:TemplateField  HeaderText="到款金额">
                <ItemTemplate>
                    <asp:Label ID="Received" runat="server" Text='<%# Eval("Received") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="到款金额" Visible="false" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:TextBox ID="EditReceived" runat="server" Text='<% #Eval("Received") %>' Width="100%"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField  HeaderText="到款比例">
                <ItemTemplate>
                    <asp:Label ID="DaoKuanBL" runat="server" Text='<%# GetValue(Eval("DaoKuanBL")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField  HeaderText="未到款金额">
                <ItemTemplate>
                    <asp:Label ID="NoReceived" runat="server" Text='<%# Eval("NoReceived") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField  HeaderText="未到款比例">
                <ItemTemplate>
                    <asp:Label ID="WeiDaoKuanBL" runat="server" Text='<%# GetValue(Eval("WeiDaoKuanBL")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              
<%--            <asp:BoundField DataField="DaoKuanBL" HeaderText="到款比例" SortExpression="DaoKuanBL" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="NoReceived" HeaderText="未到款金额" SortExpression="Received" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="WeiDaoKuanBL" HeaderText="未到款比例" SortExpression="WeiDaoKuanBL" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Center" />--%>
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
    <asp:Label ID="Label3" runat="server" Text="发票金额合计:"></asp:Label>
    <asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label1" runat="server" Text="到款金额合计:"></asp:Label>
    <asp:Label ID="lblDaoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label4" runat="server" Text="到款合计比例:"></asp:Label>
    <asp:Label ID="lblDaoKuanBLTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;

    <asp:Label ID="Label2" runat="server" Text="未到款金额合计:"></asp:Label>
    <asp:Label ID="lblWeiDaoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
     <asp:Label ID="Label5" runat="server" Text="未到款合计比例:"></asp:Label>
    <asp:Label ID="lblWeiDaoKuanBLTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <script type="text/javascript">

        function GetNoReceived(ReceivedId, Total, DaoKuanBL, NoReceived, WeiDaoKuanBL) {

            //alert(document.getElementById(ReceivedId).value);
            if (document.getElementById(ReceivedId).value != "") {
                var receivedTotal = parseFloat(document.getElementById(ReceivedId).value);
                var NoReceivedTotal = Total - receivedTotal;

                document.getElementById(NoReceived).innerHTML = NoReceivedTotal;
                document.getElementById(WeiDaoKuanBL).innerHTML = (NoReceivedTotal / Total*100).toFixed(2);
                document.getElementById(DaoKuanBL).innerHTML = (receivedTotal / Total*100).toFixed(2); 

               

            }
             
        }
    </script>
</asp:Content>
