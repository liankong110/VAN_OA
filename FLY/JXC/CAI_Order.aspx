<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CAI_Order.aspx.cs" Inherits="VAN_OA.JXC.CAI_Order"
    Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master" Title="订单报批表" %>

<%@ Import Namespace="VAN_OA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <script type="text/javascript">


        function SetKuCun() {
            if (document.getElementById('<%= kucun1.ClientID %>').checked) {

                document.getElementById('<%= txtSupplier.ClientID %>').value = "库存";
                document.getElementById('<%= txtSupplier.ClientID %>').readOnly = true;
                document.getElementById('<%= txtSupperPrice.ClientID %>').value = document.getElementById('<%= lblGoodAvgPrice.ClientID %>').innerHTML;
                document.getElementById('<%= txtSupperPrice.ClientID %>').readOnly = true;
                document.getElementById('<%= txtTureSupperPrice1.ClientID %>').readOnly = true;
                document.getElementById('<%= txtTureSupperPrice1.ClientID %>').value = document.getElementById('<%= lblGoodAvgPrice.ClientID %>').innerHTML;

            }
            else {
                document.getElementById('<%= txtSupplier.ClientID %>').value = "";
                document.getElementById('<%= txtSupplier.ClientID %>').readOnly = false;
                document.getElementById('<%= txtSupperPrice.ClientID %>').readOnly = false;
                document.getElementById('<%= txtTureSupperPrice1.ClientID %>').readOnly = false;
            }
        }
        function SetKuCun2() {
            if (document.getElementById('<%= kucun2.ClientID %>').checked) {

                document.getElementById('<%= txtSupper2.ClientID %>').value = "库存";
                document.getElementById('<%= txtSupper2.ClientID %>').readOnly = true;

                document.getElementById('<%= txtPrice2.ClientID %>').value = document.getElementById('<%= lblGoodAvgPrice.ClientID %>').innerHTML;
                document.getElementById('<%= txtPrice2.ClientID %>').readOnly = true;
                document.getElementById('<%= txtTureSupperPrice2.ClientID %>').readOnly = true;
                document.getElementById('<%= txtTureSupperPrice2.ClientID %>').value = document.getElementById('<%= lblGoodAvgPrice.ClientID %>').innerHTML;

            }
            else {
                document.getElementById('<%= txtSupper2.ClientID %>').value = "";
                document.getElementById('<%= txtSupper2.ClientID %>').readOnly = false;
                document.getElementById('<%= txtPrice2.ClientID %>').readOnly = false;
                document.getElementById('<%= txtTureSupperPrice2.ClientID %>').readOnly = false;
            }
        }
        function SetKuCun3() {
            if (document.getElementById('<%= kucun3.ClientID %>').checked) {

                document.getElementById('<%= txtSupper3.ClientID %>').value = "库存";
                document.getElementById('<%= txtSupper3.ClientID %>').readOnly = true;

                document.getElementById('<%= txtPrice3.ClientID %>').value = document.getElementById('<%= lblGoodAvgPrice.ClientID %>').innerHTML;
                document.getElementById('<%= txtPrice3.ClientID %>').readOnly = true;
                document.getElementById('<%= txtTureSupperPrice3.ClientID %>').readOnly = true;
                document.getElementById('<%= txtTureSupperPrice3.ClientID %>').value = document.getElementById('<%= lblGoodAvgPrice.ClientID %>').innerHTML;
            }
            else {
                document.getElementById('<%= txtSupper3.ClientID %>').value = "";
                document.getElementById('<%= txtSupper3.ClientID %>').readOnly = false;
                document.getElementById('<%= txtPrice3.ClientID %>').readOnly = false;
                document.getElementById('<%= txtTureSupperPrice3.ClientID %>').readOnly = false;
            }
        }


        function count1() {

            var sl = document.getElementById('<%= txtNum.ClientID %>').value;


            var dj = document.getElementById('<%= txtSupperPrice.ClientID %>').value;
            if ((sl != "") && (dj != "")) {
                var total = sl * dj;
                document.getElementById('<%= txtTotal1.ClientID %>').value = total.toFixed(3).toString();
                document.getElementById('<%= txtTureSupperPrice1.ClientID %>').value = dj;
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
                document.getElementById('<%= txtTureSupperPrice2.ClientID %>').value = dj;
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
                document.getElementById('<%= txtTureSupperPrice3.ClientID %>').value = dj;

            }
            else {
                document.getElementById('<%= txtTotal3.ClientID %>').value = "0.00";


            }
        }



        function cou1() {

            var sl = document.getElementById('<%= txtNum.ClientID %>').value;

            var dj = document.getElementById('<%= txtFinPrice1.ClientID %>').value;
            if ((sl != "") && (dj != "")) {
                var total = sl * dj;
                document.getElementById('<%= txtTotal1.ClientID %>').value = total.toFixed(3).toString();
                //document.getElementById('<%= txtTureSupperPrice1.ClientID %>').value = dj;
            }
            else {
                document.getElementById('<%= txtTotal1.ClientID %>').value = "0.00";


            }
        }



        function cou2() {

            var sl = document.getElementById('<%= txtNum.ClientID %>').value;
            var dj = document.getElementById('<%= txtFinPrice2.ClientID %>').value;
            if ((sl != "") && (dj != "")) {

                var total = sl * dj;
                document.getElementById('<%= txtTotal2.ClientID %>').value = total.toFixed(3).toString();
                //document.getElementById('<%= txtTureSupperPrice2.ClientID %>').value = dj;
            }
            else {
                document.getElementById('<%= txtTotal2.ClientID %>').value = "0.00";


            }

        }
        function cou3() {

            var sl = document.getElementById('<%= txtNum.ClientID %>').value;
            var dj = document.getElementById('<%= txtFinPrice1.ClientID %>').value;
            if ((sl != "") && (dj != "")) {

                var total = sl * dj;
                document.getElementById('<%= txtTotal3.ClientID %>').value = total.toFixed(3).toString();
                //document.getElementById('<%= txtTureSupperPrice3.ClientID %>').value = dj;
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

                if (arr.length == 4) {


                    document.getElementById('<%= txtGuestNo.ClientID %>').value = arr[0];
                    document.getElementById('<%= txtGuestName.ClientID %>').value = arr[1];
                    document.getElementById('<%= txtAE.ClientID %>').value = arr[2];
                    document.getElementById('<%= txtINSIDE.ClientID %>').value = arr[3];

                }
            }
        }

    </script>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">采购订单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
                <asp:CheckBox ID="cbMingYiCaiGou" runat="server" Text="名义采购"
                    AutoPostBack="true" OnCheckedChanged="cbMingYiCaiGou_CheckedChanged" />
                <asp:LinkButton ID="LinkButton1" ForeColor="Red" runat="server" OnClick="LinkButton1_Click">查看历史相关订单</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>采购人：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
            <td>请购人:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtCaiGou" runat="server" Width="200px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetUserName" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtCaiGou">
                </cc1:AutoCompleteExtender>
            </td>
        </tr>
        <tr>
            <td>业务类型：
            </td>
            <td colspan="1">
                <asp:DropDownList ID="ddlBusType" runat="server" Width="200px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlBusType_SelectedIndexChanged">
                    <asp:ListItem Value="0">项目订单采购</asp:ListItem>
                    <asp:ListItem Value="1">库存采购</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>单据号:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtCG_ProNo" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="lbtnPoList" runat="server" OnClientClick="javascript:window.showModalDialog('DioPOList.aspx',null,'dialogWidth:860px;dialogHeight:500px;help:no;status:no')"
                    ForeColor="Red" OnClick="lbtnPoList_Click">
                选择</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>客户ID：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtGuestNo" runat="server" Width="200px" onblur="CheckId();" ReadOnly="true"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetGuestList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtGuestNo">
                </cc1:AutoCompleteExtender>
            </td>
            <td>项目单号:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
                <asp:Label ID="lblZhui" runat="server" ForeColor="Red" Text=""></asp:Label>
                  <asp:Label ID="lblSpecial" runat="server" ForeColor="Red" Text="" style="margin-left:20px;"> </asp:Label>
            </td>
        </tr>
        <tr>
            <td>客户名称：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
            <td>项目名称:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPOName" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>AE：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtAE" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>

                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="LoginName"
                    Width="200PX" Visible="false">
                </asp:DropDownList>
                <font style="color: Red">*</font>
            </td>
            <td>项目日期：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPODate" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtPODate">
                </cc1:CalendarExtender>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>INSIDE：
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtINSIDE" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
            <td>项目金额:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPOTotal" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td colspan="1">&nbsp;
            </td>
            <td>结算方式:
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPOPayStype" runat="server" Width="200px" ReadOnly="true"></asp:TextBox><font
                    style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>备注
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" Width="95%"></asp:TextBox>
                <font style="color: Red">*</font>
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
                <asp:LinkButton ID="lbtnAddFiles" runat="server" OnClientClick="javascript:window.showModalDialog('CAI_Orders.aspx',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')"
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
                                <td>单位
                                </td>
                                <td>数量
                                </td>
                                <td>单位
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


                        <asp:TemplateField HeaderText="仓位">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodAreaNumber" runat="server" Text='<%# Eval("GoodAreaNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="编码">
                            <ItemTemplate>
                                <a href="/BaseInfo/GoodTemp.aspx?GoodNo=<%#Eval("GoodNo") %>" target="_blank">
                                    <%# Eval("GoodNo")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--   <asp:BoundField DataField="GoodNo" HeaderText="编码" SortExpression="GoodNo" />--%>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="规格" ItemStyle-Width="200px" SortExpression="GoodSpec" />
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
                                <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成本总价">
                            <ItemTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售单价">
                            <ItemTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售总价">
                            <ItemTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
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
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ToTime" HeaderText="到帐日期" SortExpression="ToTime" DataFormatString="{0:yyyy-MM-dd}" Visible="false" />
                        <asp:TemplateField HeaderText="利润%">
                            <ItemTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj(Eval("Profit")) %>'></asp:Label>
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
                <asp:FileUpload ID="fuAttach" runat="server" Width="400px" />
                <br />
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal">
                            <asp:GridView ID="gvCai" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                                DataKeyNames="Ids" ShowFooter="true" AutoGenerateColumns="False"
                                OnRowDataBound="gvCai_RowDataBound" Width="130%" OnRowDeleting="gvCai_RowDeleting" OnRowEditing="gvCai_RowEditing"
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
                                            <td>单位
                                            </td>
                                            <td>数量
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
                                            <td>审批意见
                                            </td>
                                            <td>更新人
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
                                        <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
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
                                    <%--<asp:BoundField DataField="Good_Model" HeaderText="型号" SortExpression="Good_Model" /> --%>
                                    <asp:BoundField DataField="GoodUnit" HeaderText="单位" SortExpression="GoodUnit" />
                                    <asp:TemplateField HeaderText="数量">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox11" runat="server" Checked='<%# Eval("cbifDefault1") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Supplier" HeaderText="供应商1" SortExpression="Supplier"
                                        ItemStyle-HorizontalAlign="Center" />

                                    

                                    <asp:TemplateField HeaderText="实价1/询价1">
                                        <ItemTemplate>

                                            <asp:Label ID="lblTruePrice1" runat="server" Text='<%# Eval("TruePrice1") %>' Style="background-color: yellow;"></asp:Label>

                                             <asp:Label ID="lblTopTruePrice" runat="server" Text='<%# GetTopPrice(Eval("TopTruePrice")) %>' Style="background-color:greenyellow;"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblSupperPrice" runat="server" Text='<%# Eval("SupperPrice") %>'></asp:Label>
                                               <asp:Label ID="lblTopSupplierPrice" runat="server" Text='<%# GetTopPrice(Eval("TopSupplierPrice")) %>' Style="background-color:greenyellow;"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="确认价1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinPrice1" runat="server" Text='<%# Eval("FinPrice1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="SupperPrice" HeaderText="询价1" SortExpression="SupperPrice" ItemStyle-HorizontalAlign="Center"  /> 
		
		<asp:BoundField DataField="FinPrice1" HeaderText="确认价1" SortExpression="FinPrice1" ItemStyle-HorizontalAlign="Center"  /> --%>
                                    <%--<asp:BoundField DataField="Total1" HeaderText="小计1" SortExpression="Total1" ItemStyle-HorizontalAlign="Center"  /> --%>
                                    <asp:TemplateField HeaderText="小计1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("Total1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("Total1") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox11cbifDefault2" runat="server" Checked='<%# Eval("cbifDefault2") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Supplier1" HeaderText="供应商2" SortExpression="Supplier1"
                                        ItemStyle-HorizontalAlign="Center" />

                                    <asp:TemplateField HeaderText="实价2/询价2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTruePrice2" runat="server" Text='<%# Eval("TruePrice2") %>' Style="background-color: yellow;"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblSupperPrice1" runat="server" Text='<%# Eval("SupperPrice1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="确认价2">
                                        <ItemTemplate>
                                            <asp:Label ID="FinPrice2" runat="server" Text='<%# Eval("FinPrice2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="SupperPrice1" HeaderText="询价2" SortExpression="SupperPrice1" ItemStyle-HorizontalAlign="Center"  /> 
		<asp:BoundField DataField="FinPrice2" HeaderText="确认价2" SortExpression="FinPrice2" ItemStyle-HorizontalAlign="Center"  /> --%>
                                    <%--<asp:BoundField DataField="Total2" HeaderText="小计2" SortExpression="Total2" ItemStyle-HorizontalAlign="Center"  /> --%>
                                    <asp:TemplateField HeaderText="小计2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal2" runat="server" Text='<%# Eval("Total2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal2" runat="server" Text='<%# Eval("Total2") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox11cbifDefault3" runat="server" Checked='<%# Eval("cbifDefault3") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Supplier2" HeaderText="供应商3" SortExpression="Supplier2"
                                        ItemStyle-HorizontalAlign="Center" />

                                    <asp:TemplateField HeaderText="实价3/询价3">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTruePrice3" runat="server" Text='<%# Eval("TruePrice3") %>' Style="background-color: yellow;"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblSupperPrice2" runat="server" Text='<%# Eval("SupperPrice2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="确认价3">
                                        <ItemTemplate>
                                            <asp:Label ID="FinPrice3" runat="server" Text='<%# Eval("FinPrice3") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--		<asp:BoundField DataField="SupperPrice2" HeaderText="询价3" SortExpression="SupperPrice2" ItemStyle-HorizontalAlign="Center"  /> 
			<asp:BoundField DataField="FinPrice3" HeaderText="确认价3" SortExpression="FinPrice3" ItemStyle-HorizontalAlign="Center"  /> --%>
                                    <%--	<asp:BoundField DataField="Total3" HeaderText="小计3" SortExpression="Total3" ItemStyle-HorizontalAlign="Center"  /> --%>
                                    <asp:TemplateField HeaderText="小计3">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal3" runat="server" Text='<%# Eval("Total3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal3" runat="server" Text='<%# Eval("Total3") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="含税">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CBIsHanShui" runat="server" Checked='<%# Eval("IsHanShui") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CaiFpType" HeaderText="发票类型" SortExpression="CaiFpType"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Idea" HeaderText="审批意见" SortExpression="Idea" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="UpdateUser" HeaderText="更新人" SortExpression="UpdateUser"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <%-- <asp:TemplateField HeaderText="利润%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>--%>

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
                        </asp:Panel>

                        <br />
                        <asp:Panel ID="plCiGou" runat="server">
                            <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
                                border="1">
                                <tr>
                                    <td colspan="8" style="height: 20px; background-color: #336699; color: White;">采购
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" width="30%" align="right">商品 ：
                                    </td>
                                    <td height="25" align="left" colspan="5">&nbsp;
                                        <asp:Label ID="lblInvDetail" runat="server" Text=""></asp:Label>&nbsp;
                                        <div>
                                            &nbsp;
                                            <asp:Label ID="lblGoodMess" runat="server"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblGoodAvgPrice" runat="server"></asp:Label>
                                            &nbsp;
                                            <asp:LinkButton ID="lblNextCaiPrice" runat="server" target="_blank" OnClick="lblNextCaiPrice_Click"></asp:LinkButton>
                                            <%--  <asp:Label ID="lblNextCaiPrice" runat="server" ></asp:Label>--%>
                                        </div>
                                        &nbsp;
                                          &nbsp;
                                         <asp:Label ID="lblCaiId" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblXX" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblAA" runat="server" Text=""></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblZZ" runat="server" Text=""></asp:Label>

                                        &nbsp;
                                        <asp:Label ID="lblYY" runat="server" Text=""></asp:Label>

                                        &nbsp;
                                        <asp:Label ID="lblTopPrice" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <br />
                                        <asp:CheckBox ID="cbIsHanShui" runat="server" Text="含税" ForeColor="Red" AutoPostBack="True"
                                            OnCheckedChanged="cbIsHanShui_CheckedChanged" />
                                        <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="Id" DataTextField="FpType">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvgPrice" runat="server" Text="0" Visible="False"></asp:Label>
                                        <asp:Label ID="lbllastPrice" runat="server" Text="0" Visible="False"></asp:Label>
                                    </td>
                                    <td height="25" align="right">数量 ：
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
                                    <td height="25" width="20%" align="right">采购询供应商1 ：
                                    </td>
                                    <td height="25" width="20%" align="left">
                                        <asp:TextBox ID="txtSupplier" runat="server" Width="160px"></asp:TextBox><asp:CheckBox ID="cbifDefault1" runat="server" AutoPostBack="True" OnCheckedChanged="cbifDefault1_CheckedChanged" />
                                        <div id="divKucu">
                                            <asp:CheckBox ID="kucun1" runat="server" Text="库存" onclick='SetKuCun();' />
                                        </div>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                                            ServiceMethod="GetSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                                            TargetControlID="txtSupplier">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td width="13%">实采单价:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTureSupperPrice1" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td height="25" width="15%" align="right">采购询价1 ：
                                    </td>
                                    <td height="25" width="*" align="left">
                                        <asp:TextBox ID="txtSupperPrice" runat="server" Width="100px" onKeyUp="count1();"></asp:TextBox>
                                        <asp:TextBox ID="txtFinPrice1" runat="server" Width="100px" onKeyUp="cou1();"></asp:TextBox>
                                    </td>
                                    <td height="25" width="30%" align="right">小计1 ：
                                    </td>
                                    <td height="25" width="*" align="left">
                                        <asp:TextBox ID="txtTotal1" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" width="20%" align="right">采购询供应商2 ：
                                    </td>
                                    <td height="25" width="20%" align="left">
                                        <asp:TextBox ID="txtSupper2" runat="server" Width="160px"></asp:TextBox><asp:CheckBox
                                            ID="cbifDefault2" runat="server" AutoPostBack="True" OnCheckedChanged="cbifDefault2_CheckedChanged" />
                                        <asp:CheckBox ID="kucun2" runat="server" Text="库存" onclick='SetKuCun2();' />
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                                            ServiceMethod="GetSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                                            TargetControlID="txtSupper2">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td width="13%">实采单价:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTureSupperPrice2" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td height="25" width="15%" align="right">采购询价2 ：
                                    </td>
                                    <td height="25" width="210" align="left">
                                        <asp:TextBox ID="txtPrice2" runat="server" Width="100px" onKeyUp="count2();"></asp:TextBox>
                                        <asp:TextBox ID="txtFinPrice2" runat="server" Width="100px" onKeyUp="cou2();"></asp:TextBox>
                                    </td>
                                    <td height="25" width="30%" align="right">小计2 ：
                                    </td>
                                    <td height="25" width="*" align="left">
                                        <asp:TextBox ID="txtTotal2" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" width="20%" align="right">采购询供应商3 ：
                                    </td>
                                    <td height="25" width="23%" align="left">
                                        <asp:TextBox ID="txtSupper3" runat="server" Width="160px"></asp:TextBox><asp:CheckBox
                                            ID="cbifDefault3" runat="server" AutoPostBack="True" OnCheckedChanged="cbifDefault3_CheckedChanged" />
                                        <asp:CheckBox ID="kucun3" runat="server" Text="库存" onclick='SetKuCun3();' />
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                                            ServiceMethod="GetSuplierList" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                                            TargetControlID="txtSupper3">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td width="13%">实采单价:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTureSupperPrice3" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td height="25" width="15%" align="right">采购询价3 ：
                                    </td>
                                    <td height="25" width="*" align="left">
                                        <asp:TextBox ID="txtPrice3" runat="server" Width="100px" onKeyUp="count3();"></asp:TextBox>
                                        <asp:TextBox ID="txtFinPrice3" runat="server" Width="100px" onKeyUp="cou3();"></asp:TextBox>
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
                                    <td height="25" align="left" colspan="7">
                                        <asp:TextBox ID="txtIdea" runat="server" Width="95%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" align="center" style="height: 80%">
                                        <asp:Button ID="btnSave" runat="server" Text="保存" BackColor="Yellow" OnClick="btnSave_Click"
                                            ValidationGroup="a" Width="74px" />&nbsp; &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
            <td colspan="6" align="center">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" OnClientClick="return confirm('确定要提交吗？')" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />
                &nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
    <br />
    <script type="text/javascript">

     
    </script>
</asp:Content>
