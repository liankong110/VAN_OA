<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CG_Order.aspx.cs" Inherits="VAN_OA.JXC.CG_Order"
    Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master" Title="订单报批表" %>

<%@ Import Namespace="VAN_OA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

    <script type="text/javascript">
        function count1() {

            var sl = document.getElementById('<%= txtNum.ClientID %>').value;

            var dj = document.getElementById('<%= txtSupperPrice.ClientID %>').value;
            if ((sl != "") && (dj != "")) {
                var total = sl * dj;
                document.getElementById('<%= txtTotal1.ClientID %>').value = total.toFixed(3).toString();
            }
            else {
                document.getElementById('<%= txtTotal1.ClientID %>').value = "0.00";


            }
        }



        function count2() {

            var sl = document.getElementById('<%= txtNum.ClientID %>').value;
            var dj = document.getElementById('<%= txtPrice2.ClientID %>').value;
            if ((sl != "") && (dj != "")) {

                var total = sl * dj;
                document.getElementById('<%= txtTotal2.ClientID %>').value = total.toFixed(3).toString();
            }
            else {
                document.getElementById('<%= txtTotal2.ClientID %>').value = "0.00";


            }

        }
        function count3() {

            var sl = document.getElementById('<%= txtNum.ClientID %>').value;
            var dj = document.getElementById('<%= txtPrice3.ClientID %>').value;
            if ((sl != "") && (dj != "")) {

                var total = sl * dj;
                document.getElementById('<%= txtTotal3.ClientID %>').value = total.toFixed(3).toString();
            }
            else {
                document.getElementById('<%= txtTotal3.ClientID %>').value = "0.00";


            }
        }


        function CheckId() {

            var sl = document.getElementById('<%= txtGuestNo.ClientID %>').value;

            if (sl != "") {
                sl = sl.replace(/\\/g, ",");
                var arr = sl.split(',');

                if (arr.length == 5) {


                    document.getElementById('<%= txtGuestNo.ClientID %>').value = arr[0];
                    document.getElementById('<%= txtGuestName.ClientID %>').value = arr[1];
                    document.getElementById('<%= txtAE.ClientID %>').value = arr[2];
                    document.getElementById('<%= txtINSIDE.ClientID %>').value = arr[3];
                    document.getElementById('<%= txtPOPayStype.ClientID %>').value = arr[4];

                }
            }
        }



        function setColor(_parent) {

            if (_parent.value == 1) {
                _parent.style.backgroundColor = '';
            }
            //工程
            if (_parent.value == 2) {
                _parent.style.backgroundColor = 'red';

            }
            //系统
            if (_parent.value == 3) {
                _parent.style.backgroundColor = 'green';

            }


        }



    </script>

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">项目订单申请-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlIfZhui" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Value="0">普通订单</asp:ListItem>
                    <asp:ListItem Value="1">订单追加</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>申请人：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtName" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
            <td>项目编号:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPONo" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:CheckBox ID="cbSpecial" runat="server" Text="特殊" ForeColor="Red" />
                <asp:CheckBox ID="cbIsPoFax" runat="server" Text="含税" AutoPostBack="True" OnCheckedChanged="cbIsPoFax_CheckedChanged"
                    Checked="True" />
                <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="Id" DataTextField="FpType">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>客户ID：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtGuestNo" runat="server" Width="300px" onblur="CheckId();"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetGuestAllList" MinimumPrefixLength="1" EnableCaching="true"
                    CompletionSetCount="10" TargetControlID="txtGuestNo">
                </cc1:AutoCompleteExtender>
                <font style="color: Red">*</font>
            </td>
            <td>项目名称:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPOName" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
                <asp:LinkButton ID="lblSelect" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOList.aspx?CG_Order=true',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="lblSelect_Click">选择</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>客户名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtGuestName" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td>项目日期：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPODate" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtPODate">
                </cc1:CalendarExtender>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>AE：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtAE" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td>项目金额:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPOTotal" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>INSIDE：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtINSIDE" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td>结算方式:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPOPayStype" runat="server"></asp:TextBox><font
                    style="color: Red">*</font>
                <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id" onchange="setColor(this);">
                    <asp:ListItem Value="1">零售</asp:ListItem>
                    <asp:ListItem Value="2">工程</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>备注:
            </td>
            <td>
                <asp:TextBox ID="txtPORemark" runat="server" Width="95%"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
            <td>项目模型:
            </td>
            <td>
                <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList><font
                    style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <div align="right" style="width: 100%;">
                    <asp:LinkButton ID="lblAttName" runat="server" OnClick="lblAttName_Click" ForeColor="Red"></asp:LinkButton>
                    <asp:Label ID="lblAttName_Vis" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:LinkButton ID="lbtnAddFiles" runat="server" OnClientClick="javascript:window.showModalDialog('CG_Orders.aspx',null,'dialogWidth:500px;dialogHeight:550px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
      添加文件</asp:LinkButton>
                <br />
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    OnRowDeleting="gvList_RowDeleting" OnRowEditing="gvList_RowEditing" ShowFooter="true">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>编辑
                                </td>
                                <td>删除
                                </td>
                                <td>编码
                                </td>
                                <td>名称
                                </td>
                                <td>小类
                                </td>
                                <td>规格
                                </td>
                                <td>型号
                                </td>
                                <td>单位
                                </td>
                                <td>数量
                                </td>
                                <td>成本单价
                                </td>
                                <td>管理费
                                </td>
                                <td>到帐日期
                                </td>
                                <td>利润%
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">---暂无数据---
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
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                                    CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="40px" />
                        </asp:TemplateField>
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
                                <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本总价">
                            <ItemTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# NumHelp.FormatTwo(Eval("CostTotal")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# NumHelp.FormatFour(Eval("CostPrice")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售单价">
                            <ItemTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售总价">
                            <ItemTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# NumHelp.FormatFour(Eval("SellTotal")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# NumHelp.FormatFour(Eval("SellTotal")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="管理费">
                            <ItemTemplate>
                                <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售净利">
                            <ItemTemplate>
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# NumHelp.FormatFour(Eval("YiLiTotal")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# NumHelp.FormatFour(Eval("YiLiTotal")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ToTime" HeaderText="到帐日期" SortExpression="ToTime" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField HeaderText="利润%">
                            <ItemTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="DetailRemark" HeaderText="备注" SortExpression="DetailRemark" />

                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                    <FooterStyle BackColor="#D7E8FF" />
                </asp:GridView>
                <br />
                <asp:FileUpload ID="fuAttach" runat="server" Width="400px" />
                <br />
                <br />
                说明：淡红色背景的项表示有滞留库存，即非订单采购库存
                <asp:GridView ID="gvCai" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" ShowFooter="true" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvCai_RowDataBound" OnRowDeleting="gvCai_RowDeleting" OnRowEditing="gvCai_RowEditing"
                    Style="border-collapse: collapse;">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>编辑
                                </td>
                                <td>编码
                                </td>
                                <td>名称
                                </td>
                                <td>小类
                                </td>
                                <td>规格
                                </td>
                                <td>型号
                                </td>
                                <td>单位
                                </td>
                                <td>数量
                                </td>
                                <td>日期
                                </td>
                                <td>供应商1
                                </td>
                                <td>询价1
                                </td>
                                <td>小计1
                                </td>
                                <td>供应商2
                                </td>
                                <td>询价2
                                </td>
                                <td>小计2
                                </td>
                                <td>供应商3
                                </td>
                                <td>询价3
                                </td>
                                <td>小计3
                                </td>
                                <td>利润率
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">---暂无数据---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText=" 编辑">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditCai" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                                    AlternateText="编辑" />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GoodId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodId" runat="server" Text='<%# Eval("GoodId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                        <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Supplier" HeaderText="供应商1" SortExpression="Supplier"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="询价1">
                            <ItemTemplate>
                                <asp:Label ID="lblSupperPrice" runat="server" Text='<%# Eval("SupperPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="小计1">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# NumHelp.FormatFour(Eval("Total1")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# NumHelp.FormatFour(Eval("Total1")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Supplier1" HeaderText="供应商2" SortExpression="Supplier1"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="询价2">
                            <ItemTemplate>
                                <asp:Label ID="lblSupperPrice1" runat="server" Text='<%# Eval("SupperPrice1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="小计2">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal2" runat="server" Text='<%# NumHelp.FormatFour(Eval("Total2")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal2" runat="server" Text='<%# NumHelp.FormatFour(Eval("Total2")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Supplier2" HeaderText="供应商3" SortExpression="Supplier2"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="询价3">
                            <ItemTemplate>
                                <asp:Label ID="lblSupperPrice2" runat="server" Text='<%# Eval("SupperPrice2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="小计3">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal3" runat="server" Text='<%# NumHelp.FormatFour(Eval("Total3")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal3" runat="server" Text='<%# NumHelp.FormatFour(Eval("Total3")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Idea" HeaderText="审批意见" SortExpression="Idea" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="UpdateUser" HeaderText="更新人" SortExpression="UpdateUser"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />


                        <asp:TemplateField HeaderText="销售单价">
                            <ItemTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="初步利润" FooterStyle-BackColor="Black" FooterStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="lblIniProfit" runat="server" Text='<%# Eval("IniProfit") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblIniProfit" runat="server" Text='<%# Eval("IniProfit") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="利润%" FooterStyle-BackColor="Black" ItemStyle-BackColor="GreenYellow" FooterStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# NumHelp.FormatTwo(Eval("CaiLiRun")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
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
                <br />
                <asp:Panel ID="plCiGou" runat="server">
                    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                        border="1">
                        <tr>
                            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">采购<asp:HiddenField ID="hfGoodId" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">商品 ：
                            </td>
                            <td height="25" align="left" colspan="3">
                                <asp:Label ID="lblInvDetail" runat="server" Text=""></asp:Label>
                            </td>
                            <td height="25" width="30%" align="right">数量 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtNum" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <%--   <tr>
	<td height="25" width="30%" align="right">
		日期：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtCaiTime" runat="server" Width="200px"></asp:TextBox>
		
		
		    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png"/>		  
		 
		 <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  TargetControlID="txtCaiTime" Format="yyyy-MM-dd"  PopupButtonID="Image1" >
                      </cc1:CalendarExtender>        
   
	    <font style="color:Red">*</font></td></tr>--%>
                        <tr>
                            <td height="25" width="30%" align="right">采购询供应商1 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtSupplier" runat="server" Width="200px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                                    ServiceMethod="GetSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                                    TargetControlID="txtSupplier">
                                </cc1:AutoCompleteExtender>
                                <asp:CheckBox ID="cbKuCun1" runat="server" Text="库存" AutoPostBack="True" OnCheckedChanged="cbKuCun1_CheckedChanged" />
                            </td>
                            <td height="25" width="30%" align="right">采购询价1 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtSupperPrice" runat="server" Width="100px" onKeyUp="count1();"></asp:TextBox>
                            </td>
                            <td height="25" width="30%" align="right">小计1 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTotal1" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">采购询供应商2 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtSupper2" runat="server" Width="200px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                                    ServiceMethod="GetSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                                    TargetControlID="txtSupper2">
                                </cc1:AutoCompleteExtender>
                                <asp:CheckBox ID="cbKuCun2" runat="server" Text="库存" AutoPostBack="True" OnCheckedChanged="cbKuCun2_CheckedChanged" />
                            </td>
                            <td height="25" width="30%" align="right">采购询价2 ：
                            </td>
                            <td height="25" width="210" align="left">
                                <asp:TextBox ID="txtPrice2" runat="server" Width="100px" onKeyUp="count2();"></asp:TextBox>
                            </td>
                            <td height="25" width="30%" align="right">小计2 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTotal2" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">采购询供应商3 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtSupper3" runat="server" Width="200px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                                    ServiceMethod="GetSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                                    TargetControlID="txtSupper3">
                                </cc1:AutoCompleteExtender>
                                <asp:CheckBox ID="cbKuCun3" runat="server" Text="库存" AutoPostBack="True" OnCheckedChanged="cbKuCun3_CheckedChanged" />
                            </td>
                            <td height="25" width="30%" align="right">采购询价3 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtPrice3" runat="server" Width="100px" onKeyUp="count3();"></asp:TextBox>
                            </td>
                            <td height="25" width="30%" align="right">小计3 ：
                            </td>
                            <td height="25" width="*" align="left">
                                <asp:TextBox ID="txtTotal3" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="25" width="30%" align="right">
                                <asp:Label ID="lblIdea" runat="server" Text="审批意见:"></asp:Label>
                            </td>
                            <td height="25" align="left" colspan="5">
                                <asp:TextBox ID="txtIdea" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" style="height: 80%">
                                <asp:Button ID="btnSave" runat="server" Text="保存" BackColor="Yellow" OnClick="btnSave_Click"
                                    ValidationGroup="a" Width="74px" />&nbsp; &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserName">
                    <asp:ListItem Selected="True">通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="本次审批意见:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6">备注：<br />
                1.计划完工天数指项目自建立起至完全交付并验收的自然天数
                <br />
                2.项目模型说明： 
       <asp:GridView ID="gvModel" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
           ShowFooter="false" Width="100%" AutoGenerateColumns="False"
           ShowHeader="false"
           Style="border-collapse: collapse;">
           <Columns>
               <asp:BoundField DataField="ModelName" HeaderText="模型名称" SortExpression="MyPoType"  />
               <asp:BoundField DataField="ModelRemark" HeaderText="模型说明" SortExpression="XiShu"  />
           </Columns>
           <PagerStyle HorizontalAlign="Center" />
           <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
           <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
               HorizontalAlign="Center" />
           <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
           <RowStyle CssClass="InfoDetail1" />
           <FooterStyle BackColor="#D7E8FF" />
       </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">


                <asp:Label ID="lblWarn" Visible="false" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="20px" Text="项目利润为负数，请确认！！！"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnReSubEdit" runat="server" Text="再次编辑" BackColor="Yellow" OnClick="btnReSubEdit_Click" />&nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" OnClientClick="return confirm('确定要提交吗？')" />&nbsp; &nbsp; &nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
</asp:Content>
