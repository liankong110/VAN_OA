<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DioCaiPOList.aspx.cs" Inherits="VAN_OA.JXC.DioCaiPOList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>采购订单信息</title>
</head>

<script language="javascript" type="text/javascript">
    function GetAllCheckBox(parentItem) {
        var items = document.getElementsByTagName("input");
        for (i = 0; i < items.length; i++) {
            if (parentItem.checked) {
                if (items[i].type == "checkbox") {
                    items[i].checked = true;
                }
            }
            else {
                if (items[i].type == "checkbox") {
                    items[i].checked = false;
                }
            }
        }
    }



</script>

<body style="font-size: 12px;">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                border="1">
                <tr>
                    <td colspan="8" style="height: 20px; background-color: #336699; color: White;">采购订单信息查询
                    </td>
                </tr>
                <tr>
                    <td>项目编号:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                    </td>
                    <td>项目名称:
                    </td>
                    <td>
                        <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>
                    </td>

                    <td>采购类型：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBusType" runat="server" Width="180px" OnSelectedIndexChanged="ddlBusType_SelectedIndexChanged">
                            <asp:ListItem Value="-1">全部</asp:ListItem>
                            <asp:ListItem Value="0">非库存采购</asp:ListItem>
                            <asp:ListItem Value="1">库存采购</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>AE：</td>
                    <td>
                        <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>供应商简称: </td>
                    <td>
                        <asp:TextBox ID="txtSupplier" runat="server"></asp:TextBox>
                    </td>
                    <td>商品编码: </td>
                    <td>
                        <asp:TextBox ID="txtGoodNo1" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>名称/小类/规格:</td>
                    <td>
                        <asp:TextBox ID="txtNameOrTypeOrSpec1" runat="server" Width="100PX"></asp:TextBox>
                    </td>
                    <td>客户名称</td>
                    <td>
                        <asp:TextBox ID="txtGuestName" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr><td>采购单据号</td>
                    <td>
                        <asp:TextBox ID="txtCaiProNo" runat="server" Width="100px"></asp:TextBox>


                    </td>
                    <td colspan="6">  <div style="float: right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="  确定 " BackColor="Yellow" OnClick="Button1_Click" />&nbsp;&nbsp;&nbsp;
            </div></td>
                </tr>
            </table>
          
            <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvList_PageIndexChanging"
                OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand">
                <PagerTemplate>
                    <br />
                    <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />--%>
                </PagerTemplate>
                <EmptyDataTemplate>
                    <table width="100%">
                        <tr style="height: 20px; background-color: #336699; color: White;">
                            <td>采购单据号
                            </td>
                            <td>项目编码
                            </td>
                            <td>项目名称
                            </td>
                            <td>客户名称
                            </td>
                            <td>AE
                            </td>
                            <td>INSIDE
                            </td>
                            <td>编码
                            </td>
                            <td>名称
                            </td>
                            <td>规格
                            </td>
                            <td>型号
                            </td>
                            <td>总数量
                            </td>
                            <td>已检数量
                            </td>
                            <td>未检数量
                            </td>
                            <td>单价
                            </td>
                            <td>金额
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" style="height: 80%">---暂无数据---
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                            &nbsp;
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="cbAll" type="checkbox" value="" onclick="GetAllCheckBox(this)"></input>
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="采购单据号">
                        <ItemTemplate>
                            <asp:Label ID="ProNo" runat="server" Text='<%# Eval("ProNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目编码">
                        <ItemTemplate>
                            <asp:Label ID="PONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目名称">
                        <ItemTemplate>
                            <asp:Label ID="POName" runat="server" Text='<%# Eval("POName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="请购人">
                        <ItemTemplate>
                            <asp:Label ID="CaiGou" runat="server" Text='<%# Eval("CaiGou") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="采购人" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="loginName" runat="server" Text='<%# Eval("loginName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="客户名称">
                        <ItemTemplate>
                            <asp:Label ID="GuestName" runat="server" Text='<%# Eval("GuestName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AE">
                        <ItemTemplate>
                            <asp:Label ID="AE" runat="server" Text='<%# Eval("AE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="INSIDE">
                        <ItemTemplate>
                            <asp:Label ID="INSIDE" runat="server" Text='<%# Eval("INSIDE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="供应商">
                        <ItemTemplate>
                            <asp:Label ID="Supplier" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="名称No" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="GuestNo" runat="server" Text='<%# Eval("GuestNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="仓位" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="GoodAreaNumber" runat="server" Text='<%# Eval("GoodAreaNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="编码" >
                        <ItemTemplate>
                            <asp:Label ID="GoodNo" runat="server" Text='<%# Eval("GoodNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="名称">
                        <ItemTemplate>
                            <asp:Label ID="GoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="小类">
                        <ItemTemplate>
                            <asp:Label ID="GoodTypeSmName" runat="server" Text='<%# Eval("GoodTypeSmName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格">
                        <ItemTemplate>
                            <asp:Label ID="GoodSpec" runat="server" Text='<%# Eval("GoodSpec") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="型号" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="Good_Model" runat="server" Text='<%# Eval("Good_Model") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位">
                        <ItemTemplate>
                            <asp:Label ID="GoodUnit" runat="server" Text='<%# Eval("GoodUnit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总数">
                        <ItemTemplate>
                            <asp:Label ID="Num" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="totalOrderNum" HeaderText="已检数" SortExpression="totalOrderNum" />
                    <asp:TemplateField HeaderText="未检数">
                        <ItemTemplate>
                            <asp:Label ID="ResultNum" runat="server" Text='<%# Eval("ResultNum") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实价">
                        <ItemTemplate>
                            <asp:Label ID="LastTruePrice" runat="server" Text='<%# Eval("LastTruePrice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单价">
                        <ItemTemplate>
                            <asp:Label ID="Price" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="金额">
                        <ItemTemplate>
                            <asp:Label ID="Total" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GoodId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="GoodId" runat="server" Text='<%# Eval("GoodId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="POCaiID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="POCaiID" runat="server" Text='<%# Eval("POCaiID") %>'></asp:Label>
                        </ItemTemplate>
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
        </div>
    </form>
</body>
</html>
