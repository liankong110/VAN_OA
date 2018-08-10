<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFHouseGoods.aspx.cs" Inherits="VAN_OA.JXC.WFHouseGoods"
    MasterPageFile="~/DefaultMaster.Master" Title="商品档案" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
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
            <td colspan="1">
                <asp:TextBox ID="txtZhuJi" runat="server" Width="400px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetGoods" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtZhuJi">
                </cc1:AutoCompleteExtender>
            </td>
            <td>
                编码
            </td>
            <td>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="200px"></asp:TextBox>
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
            <td>
                名称/小类/规格:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="400PX"></asp:TextBox>
                或者
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="400PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                名称/小类/规格:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtNameOrTypeOrSpec1" runat="server" Width="400PX"></asp:TextBox>
                并且
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo1" runat="server" Width="400PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                商品均价:
            
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtGoodAvgPrice" runat="server" Width="100px"></asp:TextBox>
                    仓位:   <asp:DropDownList ID="ddlArea" runat="server">
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
                <asp:CheckBox ID="cbEmpty" runat="server" Text="仓位空白" AutoPostBack="True" 
                    oncheckedchanged="cbEmpty_CheckedChanged" />
                    库存数量:
                <asp:DropDownList ID="ddlFuHao" runat="server">
                <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtNum" runat="server" Width="100px"></asp:TextBox>

                   采购需出：
                <asp:DropDownList ID="ddlCaiKuNum" runat="server">
                   <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                  <asp:TextBox ID="txtCaiKuNum" runat="server" Width="100px"></asp:TextBox>
                滞留库存：
                <asp:DropDownList ID="ddlZhiLiuNum" runat="server">
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                </asp:DropDownList>
                   <asp:TextBox ID="txtZhiLiuNum" runat="server" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;
            </td>
        </tr>
    </table>
    说明：A：二楼仓库 B：一楼西仓库和一楼公共区域   C：一楼东仓库 D: 3层阁楼仓库 E:二楼公共区域 F：一楼机房 G:三楼公共区域  H：三楼会议室
    <br />
     注：淡红色背景的项表示有滞留库存，即非订单采购库存
    <br/>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"  DataKeyNames="GoodId"
         Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound" OnRowEditing="gvList_RowEditing"
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
                        数量
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
          <asp:BoundField DataField="No" HeaderText="序" SortExpression="No"  ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="HouseName" HeaderText="仓库" SortExpression="HouseName" />
             <asp:BoundField DataField="GoodAreaNumber" HeaderText="仓位" SortExpression="GoodAreaNumber" ItemStyle-HorizontalAlign="Center" />
          
             <asp:TemplateField HeaderText="编码">
                <ItemTemplate>
                        <asp:LinkButton CommandName="Edit" runat="server" ID="btnEdit" Text='<%# Eval("GoodNo") %>' ></asp:LinkButton>
                <a href="/JXC/Pro_JSXDetailInfoList.aspx?goodNo=<%#Eval("GoodNo") %>" target="_blank">
                           进销</a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="70px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                    <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>' ></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" />
            <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec"  HeaderStyle-Width="200px"/>
            <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />
            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
            <asp:TemplateField HeaderText="数量">
                <ItemTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
              <asp:BoundField DataField="SumKuXuCai" HeaderText="采购需出" SortExpression="SumKuXuCai" />
              <asp:BoundField DataField="ZhiLiuKuCun" HeaderText="滞留库存" SortExpression="ZhiLiuKuCun" />

            <asp:TemplateField HeaderText="均价">
                <ItemTemplate>
                    <asp:Label ID="lblCheckPrice" runat="server" Text='<%# GetValue(Eval("GoodAvgPrice")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="总价">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text=' <%# GetValue(Eval("Total")) %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%# GetValue(Eval("Total")) %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
              <asp:BoundField DataField="HadInvoice" HeaderText="已支付" SortExpression="GoodOutNum" DataFormatString="{0:n2}"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="NoInvoice" HeaderText="未支付" SortExpression="GoodResultNum" DataFormatString="{0:n2}"
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
        <FooterStyle BackColor="#D7E8FF" />
    </asp:GridView>
     已支付合计：<asp:Label ID="lblHadInvoice" runat="server" Text="0" ForeColor="Red" style="margin-right:20px"></asp:Label>
     未支付合计：<asp:Label ID="lblNoInvoice" runat="server" Text="0" ForeColor="Red" style="margin-right:20px"></asp:Label>
</asp:Content>
