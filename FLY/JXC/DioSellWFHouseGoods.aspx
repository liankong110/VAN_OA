<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DioSellWFHouseGoods.aspx.cs"
    Inherits="VAN_OA.JXC.DioSellWFHouseGoods" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>库存查询</title>

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

</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                库存查询
            </td>
        </tr>
        <tr>
            <td>
                商品（输入助记词）：
            </td>
            <td>
                <asp:TextBox ID="txtZhuJi" runat="server" Width="300px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetGoods" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtZhuJi">
                </cc1:AutoCompleteExtender>
            </td>
             <td>
               规格：
            </td>
             <td>
                   <asp:TextBox ID="txtGuiGe" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                仓库：
            </td>
            <td>
                <asp:DropDownList ID="ddlHouse" DataTextField="houseName" DataValueField="id" runat="server"
                    Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                类别：
            </td>
            <td>
                <asp:DropDownList ID="ddlGoodType" runat="server" DataTextField="GoodTypeName" DataValueField="GoodTypeName"
                    Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlGoodType_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlGoodSmType" runat="server" DataTextField="GoodTypeSmName"
                    DataValueField="GoodTypeSmName" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlGoodSmType_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Text="提示 红色代表 库存数量可能不够了" ForeColor="Red"></asp:Label>
            </td>
            <td colspan="2" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="Button1" runat="server" Text=" 选中 " BackColor="Yellow" OnClick="Button1_Click" />
                &nbsp;
                <asp:Button ID="Button2" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="Button2_Click" />
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
        ShowFooter="true">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        仓库
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
                        项目需出数量
                    </td>
                    <td>
                        采购需出数量
                    </td>
                    <td>
                        库存数量
                    </td>
                    <td>
                        均价
                    </td>
                    <td>
                        金额
                    </td>
                </tr>
                <tr>
                    <td colspan="11" align="center" style="height: 80%">
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
            </asp:TemplateField>
            <asp:TemplateField HeaderText="仓库ID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="HouseId" runat="server" Text='<%# Eval("HouseId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="仓库">
                <ItemTemplate>
                    <asp:Label ID="HouseName" runat="server" Text='<%# Eval("HouseName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="仓位">
                <ItemTemplate>
                    <asp:Label ID="GoodAreaNumber" runat="server" Text='<%# Eval("GoodAreaNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GoodId" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="GoodId" runat="server" Text='<%# Eval("GoodId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编码">
                <ItemTemplate>
                    <asp:Label ID="GoodNo" runat="server" Text='<%# Eval("GoodNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                    <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                </FooterTemplate>
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
            <asp:TemplateField HeaderText="型号">
                <ItemTemplate>
                    <asp:Label ID="Good_Model" runat="server" Text='<%# Eval("Good_Model") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单位">
                <ItemTemplate>
                    <asp:Label ID="GoodUnit" runat="server" Text='<%# Eval("GoodUnit") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目需出数量">
                <ItemTemplate>
                    <asp:Label ID="lblLastNum" runat="server" Text='<%# Eval("LastNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="采购需出数量">
                <ItemTemplate>
                    <asp:Label ID="lblCaiNum" runat="server" Text='<%# Eval("CaiNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="锁定数量">
                <ItemTemplate>
                    <asp:Label ID="lblDoingNum" runat="server" Text='<%# Eval("DoingNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="库存数量">
                <ItemTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="项目数量">
                <ItemTemplate>
                    <asp:Label ID="lblPONum" runat="server" Text='<%# Eval("PONums") %>'></asp:Label>
                </ItemTemplate>               
            </asp:TemplateField>
             <asp:TemplateField HeaderText="项目价格">
                <ItemTemplate>
                    <asp:Label ID="lblPOGoodPrice" runat="server" Text='<%# Eval("POGoodPrice") %>'></asp:Label>
                </ItemTemplate>               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="库存均价">
                <ItemTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# GetValue(Eval("GoodAvgPrice")) %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# GetValue(Eval("GoodAvgPrice")) %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="总价">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# GetValue(Eval("Total")) %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# GetValue(Eval("Total")) %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
        <FooterStyle BackColor="#D7E8FF" />
    </asp:GridView>
    --选中商品
    <asp:GridView ID="gvSelectedWares" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnDataBound="gvSelectedWares_DataBound"
        OnRowDataBound="gvSelectedWares_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        仓库
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
                        均价
                    </td>
                </tr>
                <tr>
                    <td colspan="11" align="center" style="height: 80%">
                        ---暂无数据---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="HouseName" HeaderText="仓库" SortExpression="HouseName" />
              <asp:TemplateField HeaderText="仓位">
                <ItemTemplate>
                    <asp:Label ID="GoodAreaNumber" runat="server" Text='<%# Eval("GoodAreaNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编码">
                <ItemTemplate>
                    <asp:Label ID="GoodNo" runat="server" Text='<%# Eval("GoodNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
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
            <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />
            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
            <asp:TemplateField HeaderText="均价">
                <ItemTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# GetValue(Eval("GoodPrice")) %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# GetValue(Eval("GoodPrice")) %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目需出数量">
                <ItemTemplate>
                    <asp:Label ID="lblLastNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="项目数量">
                <ItemTemplate>
                    <asp:Label ID="lblPONum" runat="server" Text='<%# Eval("PONum") %>'></asp:Label>
                </ItemTemplate>               
            </asp:TemplateField>
             <asp:TemplateField HeaderText="项目价格">
                <ItemTemplate>
                    <asp:Label ID="lblPOGoodPrice" runat="server" Text='<%# Eval("POGoodTotal") %>'></asp:Label>
                </ItemTemplate>               
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
        <FooterStyle BackColor="#D7E8FF" />
    </asp:GridView>
    </form>
</body>
</html>
