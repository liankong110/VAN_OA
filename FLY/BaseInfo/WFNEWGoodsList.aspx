<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFNEWGoodsList.aspx.cs"
    Inherits="VAN_OA.BaseInfo.WFNEWGoodsList" MasterPageFile="~/DefaultMaster.Master"
    Title="商品档案" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <meta http-equiv="content-type" content="application/ms-excel; charset=UTF-8" />
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">商品查询
            </td>
        </tr>
        <tr>
            <td>商品（输入助记词）：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtZhuJi" runat="server" Width="400px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetGoods" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtZhuJi">
                </cc1:AutoCompleteExtender>
            </td>
            <td>编码
            </td>
            <td>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="200px"></asp:TextBox>
                特殊:
                <asp:DropDownList ID="ddlSpecial" runat="server">
                    <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="特殊" Value="1"></asp:ListItem>
                    <asp:ListItem Text="非特殊" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>仓库：
            </td>
            <td>
                <asp:DropDownList ID="ddlHouse" DataTextField="houseName" DataValueField="id" runat="server"
                    Width="200px">
                </asp:DropDownList>
                品牌:
                <asp:TextBox ID="txtGoodBrand" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>类别：
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
            <td>名称/小类/规格:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="400PX"></asp:TextBox>
                或者
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="400PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>商品均价:
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtGoodAvgPrice" runat="server" Width="200px"></asp:TextBox>
                仓位:
                <asp:DropDownList ID="ddlArea" runat="server">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="A">A</asp:ListItem>
                    <asp:ListItem Value="B">B</asp:ListItem>
                    <asp:ListItem Value="C">C</asp:ListItem>
                    <asp:ListItem Value="D">D</asp:ListItem>
                    <asp:ListItem Value="E">E</asp:ListItem>
                    <asp:ListItem Value="F">F</asp:ListItem>
                    <asp:ListItem Value="G">G</asp:ListItem>
                    <asp:ListItem Value="H">H</asp:ListItem>
                    <asp:ListItem Value="I">I</asp:ListItem>
                    <asp:ListItem Value="J">J</asp:ListItem>
                    <asp:ListItem Value="K">K</asp:ListItem>
                    <asp:ListItem Value="L">L</asp:ListItem>
                    <asp:ListItem Value="M">M</asp:ListItem>
                    <asp:ListItem Value="N">N</asp:ListItem>
                    <asp:ListItem Value="O">O</asp:ListItem>
                    <asp:ListItem Value="P">P</asp:ListItem>
                    <asp:ListItem Value="Q">Q</asp:ListItem>
                    <asp:ListItem Value="R">R</asp:ListItem>
                    <asp:ListItem Value="S">S</asp:ListItem>
                    <asp:ListItem Value="T">T</asp:ListItem>
                    <asp:ListItem Value="U">U</asp:ListItem>
                    <asp:ListItem Value="V">V</asp:ListItem>
                    <asp:ListItem Value="W">W</asp:ListItem>
                    <asp:ListItem Value="X">X</asp:ListItem>
                    <asp:ListItem Value="Y">Y</asp:ListItem>
                    <asp:ListItem Value="Z">Z</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlNumber" runat="server">
                </asp:DropDownList>
                -<asp:DropDownList ID="ddlRow" runat="server">
                </asp:DropDownList>
                -<asp:DropDownList ID="ddlCol" runat="server">
                </asp:DropDownList>
                状态：
                 <asp:DropDownList ID="ddlStatus" runat="server">
                     <asp:ListItem Text="通过" Value="通过"></asp:ListItem>
                     <asp:ListItem Text="执行中" Value="执行中"></asp:ListItem>
                     <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                     <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                 </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
                <asp:Button ID="btnExcel" runat="server" Text="导出" BackColor="Yellow" OnClick="btnExcel_Click" />
            </td>
        </tr>
    </table>
    <br />
    注：淡红色背景的项表示有滞留库存，即非订单采购库存
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="GoodId" Width="100%" AutoGenerateColumns="False" AllowPaging="true"
        PageSize="20" OnPageIndexChanging="gvList_PageIndexChanging" OnDataBinding="gvList_DataBinding"
        OnRowDataBound="gvList_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>特殊
                    </td>
                    <td height="25" align="center">编号
                    </td>
                    <td height="25" align="center">名称
                    </td>
                    <td height="25" align="center">助记词
                    </td>
                    <td height="25" align="center">类别
                    </td>
                    <td height="25" align="center">小类
                    </td>
                    <td height="25" align="center">规格
                    </td>
                    <td height="25" align="center">型号
                    </td>
                    <td height="25" align="center">修改人
                    </td>
                    <td height="25" align="center">修改时间
                    </td>
                    <tr>
                        <td colspan="9" align="center" style="height: 80%">---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <Columns>
            <asp:TemplateField HeaderText="特殊">
                <ItemTemplate>
                    <asp:Label ID="lblSpec" runat="server" Text='<%# IfSepc(Eval("IfSpec")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodAreaNumber" HeaderText="仓位" SortExpression="GoodAreaNumber"
                ItemStyle-HorizontalAlign="Center" />
            <%--   <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Center" />--%>
            <asp:TemplateField HeaderText="编码">
                <ItemTemplate>
                    <%# Eval("GoodNo")%>
                    <a href="/JXC/Pro_JSXDetailInfoList.aspx?GoodNo=<%#Eval("goodNo") %>" target="_blank">进销 </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodBrand" HeaderText="品牌" SortExpression="GoodBrand"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ZhuJi" HeaderText="助记词" SortExpression="ZhuJi" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodTypeName" HeaderText="类别" SortExpression="GoodTypeName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodModel" HeaderText="型号" SortExpression="GoodModel"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodNum" HeaderText="库存数量" SortExpression="GoodNum" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:n4}" />
                  <asp:BoundField DataField="SumKuXuCai" HeaderText="采购需出" SortExpression="SumKuXuCai" />
              <asp:BoundField DataField="ZhiLiuKuCun" HeaderText="滞留库存" SortExpression="ZhiLiuKuCun" />
            <asp:BoundField DataField="GoodPrice" HeaderText="均价" SortExpression="GoodPrice"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n4}" />
            <asp:BoundField DataField="GoodTotal" HeaderText="金额" SortExpression="GoodTotal"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n4}" />
            <asp:BoundField DataField="CreateUserName" HeaderText="创建人" SortExpression="CreateUserName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="HadInvoice" HeaderText="已支付" SortExpression="GoodOutNum" DataFormatString="{0:n2}"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="NoInvoice" HeaderText="未支付" SortExpression="GoodResultNum" DataFormatString="{0:n2}"
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
        PageSize="20" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
    已支付合计：<asp:Label ID="lblHadInvoice" runat="server" Text="0" ForeColor="Red" Style="margin-right: 20px"></asp:Label>
    未支付合计：<asp:Label ID="lblNoInvoice" runat="server" Text="0" ForeColor="Red" Style="margin-right: 20px"></asp:Label>
     库存总金额合计：<asp:Label ID="lblHouseTotal" runat="server" Text="0" ForeColor="Red" Style="margin-right: 20px"></asp:Label>
</asp:Content>
