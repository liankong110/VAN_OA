<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFStocking.aspx.cs" Inherits="VAN_OA.JXC.WFStocking"
    MasterPageFile="~/DefaultMaster.Master" Title="商品档案" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                仓库盘点
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
                商品均价:
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtGoodAvgPrice" runat="server" Width="200px"></asp:TextBox>
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
                    <asp:ListItem Value="W">W</asp:ListItem>
                    <asp:ListItem Value="Z">Z</asp:ListItem>                    
                </asp:DropDownList>
                <asp:DropDownList ID="ddlNumber" runat="server">
                </asp:DropDownList>
                -<asp:DropDownList ID="ddlRow" runat="server">
                </asp:DropDownList>
                -<asp:DropDownList ID="ddlCol" runat="server">
                </asp:DropDownList>
                盘点日期：
                  <asp:TextBox ID="txtFrom" runat="server" Width="100px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server" Width="100px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>

                <asp:DropDownList ID="ddlQiMo" runat="server">
                <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">不包含期初期末均为零</asp:ListItem>
                        <asp:ListItem Value="2">不包含期末为零</asp:ListItem>
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
    <br>
     <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Both">
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid" AllowPaging="true"  OnPageIndexChanging="gvList_PageIndexChanging"
        DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"  PageSize="50"
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
         <PagerTemplate>
            <br />           
        </PagerTemplate>
        <Columns>
            <asp:BoundField DataField="HouseName" HeaderText="仓库" SortExpression="HouseName" />
             <asp:BoundField DataField="GoodAreaNumber" HeaderText="仓位" SortExpression="GoodAreaNumber" ItemStyle-HorizontalAlign="Center" />
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
            <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec"  HeaderStyle-Width="200px"/>
            <asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" />
            <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
             <asp:BoundField DataField="Nums" HeaderText="期初数量" SortExpression="Nums" />
              <asp:BoundField DataField="InNums" HeaderText="本期入库" SortExpression="InNums" />
               <asp:BoundField DataField="OutNums" HeaderText="本期出库" SortExpression="OutNums" />
            <asp:TemplateField HeaderText="本期结余">
                <ItemTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
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
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
        <FooterStyle BackColor="#D7E8FF" />
    </asp:GridView>
    </asp:Panel>

     <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="50" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
</asp:Content>
