<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DioSellOutOrderToFPList.aspx.cs"
    Inherits="VAN_OA.JXC.DioSellOutOrderToFPList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>销售出库信息</title>

    <script language="javascript" type="text/javascript">
 function GetAllCheckBox(parentItem)
  {
     var items = document.getElementsByTagName("input");     
     for(i=0; i<items.length;i++)
     {       
       if(parentItem.checked)
       {
         if(items[i].type=="checkbox")
          {
           items[i].checked = true;
          }
       }
       else
       {
          if(items[i].type=="checkbox")
          {
           items[i].checked = false;
          }
       }
     }
  }
  
  

    </script>

</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
            border="1">
            <tr>
                <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                    销售出库信息
                </td>
            </tr>
            <tr>
                <td>
                    项目编号:
                </td>
                <td colspan="1">
                    <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                </td>
                <td>
                    项目名称:
                </td>
                <td>
                    <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <div align="right">
                        <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text=" 确定 " BackColor="Yellow" OnClick="Button1_Click" /></div>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="100%" Height="100%">
            <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                DataKeyNames="Id" Width="160%" AutoGenerateColumns="False" OnRowEditing="gvList_RowEditing"
                OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand" PageSize="15"
                AllowPaging="True" OnPageIndexChanging="gvList_PageIndexChanging1">
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
                <EmptyDataTemplate>
                    <table width="100%">
                        <tr style="height: 20px; background-color: #336699; color: White;">
                            <td>
                                项目编码
                            </td>
                            <td>
                                项目名称
                            </td>
                            <td>
                                客户名称
                            </td>
                            <td>
                                编码
                            </td>
                            <td>
                                名称
                            </td>
                            <td>
                                小类
                            </td>
                            <td>
                                规格
                            </td>
                            <td>
                                型号
                            </td>
                            <td>
                                单位
                            </td>
                            <td>
                                数量
                            </td>
                            <td>
                                成本单价
                            </td>
                            <td>
                                成本金额
                            </td>
                            <td>
                                销售单价
                            </td>
                            <td>
                                销售金额
                            </td>
                            <td>
                                备注
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
                    <asp:TemplateField HeaderText="" HeaderStyle-Width="50px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                            &nbsp;
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="cbAll" type="checkbox" value="" onclick="GetAllCheckBox(this)"></input>
                        </HeaderTemplate>
                        <HeaderStyle Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="Ids" runat="server" Text='<%# Eval("Ids") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PONo" HeaderText="项目编码" SortExpression="PONo" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fpTotal" HeaderText="已开发票" SortExpression="fpTotal" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="WEITotals" HeaderText="未开发票" SortExpression="WEITotals"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" />
                    <asp:TemplateField HeaderText="名称">
                        <ItemTemplate>
                            <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" />
                    <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" />
                    <%-- <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />--%>
                    <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                    <asp:TemplateField HeaderText="数量">
                        <ItemTemplate>
                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成本单价">
                        <ItemTemplate>
                            <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成本总价">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("CostTotal") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("CostTotal") %>'></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="销售单价">
                        <ItemTemplate>
                            <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="销售总价">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                        </FooterTemplate>
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
        </asp:Panel>
    </div>
    </form>
</body>
</html>
