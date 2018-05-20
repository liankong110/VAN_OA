<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFQuotePrice.aspx.cs" Inherits="VAN_OA.EFrom.WFQuotePrice"
    MasterPageFile="~/DefaultMaster.Master" Title="报价单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="SampleContent">
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
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //YY=设备材料价格+人工费 
        function GetYY() {

            //txtRenGJS=lblTotalDetailsXiao+txtLaborCost
            var sl = document.getElementById('<%= lblTotalDetailsXiao.ClientID %>').innerHTML;          
            var dj = document.getElementById('<%= txtLaborCost.ClientID %>').value;
            if ((sl != "") && (dj != "")) {

                var total = parseFloat(sl) +parseFloat(dj);
                document.getElementById('<%= txtRenGJS.ClientID %>').value = total.toFixed(3).toString();
            }
            else {
                document.getElementById('<%= txtRenGJS.ClientID %>').value = "0.00";


            }

        }

        function GetAllTotal() {

            if (document.getElementById("<%= cbYH.ClientID %>").checked==false) {
                //材料统计
                var sl = document.getElementById('<%= lblTotalDetailsXiao.ClientID %>').innerHTML;
                if (sl == "") {
                    sl = "0";
                }
                //人工费用
                var dj = document.getElementById('<%= txtLaborCost.ClientID %>').value;
                if (dj == "") {
                    dj = "0";
                }
                //工程计税：
                var engineeringTax = document.getElementById('<%= txtEngineeringTax.ClientID %>').value;
                if (engineeringTax == "") {
                    engineeringTax = "0";
                }
                //报价单金额合计
                var total = parseFloat(sl) + parseFloat(dj) + parseFloat(engineeringTax);
                document.getElementById('<%= txtAllTotal.ClientID %>').value = total.toFixed(3).toString();
            }
            else {
                document.getElementById('<%= txtAllTotal.ClientID %>').value = document.getElementById('<%= txtResultYH.ClientID %>').value;
            }             
            
        }

        function CheckId() {

            var sl = document.getElementById('<%= txtGuestName.ClientID %>').value;

            if (sl != "") {
                sl = sl.replace(/\\/g, ",");
                var arr = sl.split(',');

                if (arr.length == 5) {

                    var GuestNo = arr[0];
                    var GuestName = arr[1];
                    var AE = arr[2];
                    var INSIDE = arr[3];
                    var POPayStype = arr[4];

                    document.getElementById('<%= txtGuestName.ClientID %>').value = arr[1];

                    __doPostBack('<%= txtGuestName.ClientID %>', 'GuestName')
                   
                    //                var options = {
                    //                    url: "../MyWebService/MyWebService.asmx/GetGuestByGuestName",
                    //                    contentType: "application/json; charset=utf-8",
                    //                    dataType: "json",
                    //                    type: "POST",
                    //                    //GetClass方法没有参数,所以data可以不设置
                    //                    data: "{prefixText:'" + GuestName + "',count:1}",
                    //                    success: function (response) {
                    //                        var obj = response.d;
                    //                     
                    //                        document.getElementById('<%= txtContactPerToInv.ClientID %>').value = obj.LikeMan; //联系人
                    //                        document.getElementById('<%= lbltelToInv.ClientID %>').innerText = obj.Phone; //电话
                    //                        document.getElementById('<%= txtGuestNameToInv.ClientID %>').value = obj.FoxOrEmail; //传真
                    //                        document.getElementById('<%= txtAddressToInv.ClientID %>').value = obj.GuestAddress; //地址
                    //                        document.getElementById('<%= txtInvoHeader.ClientID %>').value = obj.GuestName; //发票抬头
                    //                        document.getElementById('<%= txtInvAddress.ClientID %>').innerText = obj.GuestAddress; //注册地址=地址
                    //                        document.getElementById('<%= txtInvTel.ClientID %>').innerText = obj.Phone; //注册电话=电话
                    //                        document.getElementById('<%= lblNaShuiPer.ClientID %>').innerText = obj.GuestShui; //社会统一信用代码=税号
                    //                        document.getElementById('<%= lblbrandNo.ClientID %>').innerText = obj.GuestBrandNo; //开户行帐号
                    //                        document.getElementById('<%= txtAddressTofa.ClientID %>').value = obj.GuestAddress; //发票邮寄地址=地址
                    //                        document.getElementById('<%= txtComBusTel.ClientID %>').innerText = obj.Phone; //联系人及电话=电话


                    //                    }
                    //                }
                    // 
                    //                 $.ajax(options)




                }
            }
        }
    </script>
    <table>
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                报价单
            </td>
        </tr>
        <tr>
            <td colspan="6" style="height: 20px; background-color: #D7E8FF;">
                基本信息
            </td>
        </tr>
        <tr>
            <td>
                客户：
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtGuestName" runat="server" Width="500px" onblur="CheckId();"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetGuestAllList" MinimumPrefixLength="1" EnableCaching="true"
                    CompletionSetCount="10" TargetControlID="txtGuestName">
                </cc1:AutoCompleteExtender>
                <font style="color: Red">*</font>
            </td>
        </tr>
        <tr>
            <td>
                报价单号:
            </td>
            <td colspan="5">
                <asp:Label ID="lblQuoteNo" runat="server" Text=""></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                买方编码:
            </td>
            <td>
                <asp:Label ID="lblGuestNo" runat="server" Text=""></asp:Label>
            </td>
            <td>
                报价单:
            </td>
            <td>
                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>&nbsp;
            </td>
            <td>
                报价单日期:
            </td>
            <td>
                <asp:Label ID="lblQuoteDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                最终用户名称:
            </td>
            <td>
                <asp:TextBox ID="txtResultGuestName" runat="server" Width="260PX"></asp:TextBox>
            </td>
            <td>
                报价单有效期:
            </td>
            <td>
                <asp:TextBox ID="txtResultGuestNo" Width="150PX" runat="server" Text="30"></asp:TextBox>
                天
            </td>
            <td>
                付款条件:
            </td>
            <td>
                <asp:TextBox ID="txtPayStyle" Width="150PX" runat="server" Text="30天月结"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                联系人:
            </td>
            <td>
                <asp:TextBox ID="txtContactPerToInv" runat="server" Width="260PX"></asp:TextBox>
            </td>
            <td colspan="2">
                开票信息
            </td>
            <td>
                社会统一信用代码:
            </td>
            <td>
                <asp:Label ID="lblNaShuiPer" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                电话:
            </td>
            <td>
               
                 <asp:TextBox ID="lbltelToInv" runat="server" Width="260PX"></asp:TextBox>
            </td>
            <td>
                开票抬头:
            </td>
            <td>
                <asp:TextBox ID="txtInvoHeader" runat="server" Text="" Width="260PX"></asp:TextBox>
            </td>
            <td>
                开户行/帐号:
            </td>
            <td>
                <asp:Label ID="lblbrandNo" Width="260PX" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                传真:
            </td>
            <td>
                <asp:TextBox ID="txtGuestNameToInv" runat="server" Width="260PX"></asp:TextBox>
            </td>
            <td>
                注册地址:
            </td>
            <td>
                <asp:Label ID="txtInvAddress" runat="server"></asp:Label>
            </td>
            <td>
                发票邮寄地址:
            </td>
            <td>
                <asp:TextBox ID="txtAddressTofa" runat="server" Width="260PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                地址:
            </td>
            <td>
                <asp:TextBox ID="txtAddressToInv" runat="server" Width="260PX"></asp:TextBox>
            </td>
            <td>
                注册电话:
            </td>
            <td>
                <asp:Label ID="txtInvTel" runat="server" Width="260PX"></asp:Label>
            </td>
            <td>
                 发票联系人电话:
            </td>
            <td>
                 
                  <asp:TextBox ID="txtInvContactPer" runat="server" Width="260PX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                卖方业务代表
            </td>
            <td>
                <asp:Label ID="txtBuessName" runat="server"></asp:Label>&nbsp;
            </td>
            <td>
                卖方业务电话:
            </td>
            <td>
                <asp:Label ID="txtComBusTel" runat="server"></asp:Label>
            </td>
            <td rowspan="2">
                卖方地址:
            </td>
            <td rowspan="2">
                <asp:Label ID="txtComTel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                卖方业务代表邮箱:
            </td>
            <td>
                <asp:Label ID="txtBuessEmail" runat="server"></asp:Label>
            </td>
            <td>
                卖方业务代表传真:
            </td>
            <td>
                <asp:Label ID="txtComChuanZhen" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="height: 20px; background-color: #D7E8FF;">
                报价内容--<asp:LinkButton OnClientClick="javascript:window.showModalDialog('QPInvDetails.aspx',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">新增</asp:LinkButton>
                    &nbsp;  &nbsp;  &nbsp;
                    <asp:RadioButton ID="rbtnType1" runat="server" GroupName="QPType" Text="含税格式" Checked="true" />
                    <asp:RadioButton ID="rbtnType2" runat="server" GroupName="QPType" Text="不含税格式"  />
                    <asp:RadioButton ID="rbtnType3" runat="server" GroupName="QPType" Text="工程格式"  />
            
                <asp:CheckBox ID="cbIsBrand" runat="server" Text="含品牌"  Checked="true"/>
                <asp:CheckBox ID="cbIsProduct" runat="server" Text="含产地"   Checked="true"/>
                   <asp:CheckBox ID="cbRemark" runat="server" Text="含备注"   Checked="true"/>
                    <asp:CheckBox ID="cbIsShuiYin" runat="server" Text="含水印"   Checked="true"/>
                     <asp:CheckBox ID="cbIsGaiZhang" runat="server" Text="含印章"   Checked="true"/>
            </td>

        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="gvInvDetails" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Id" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvInvDetails_RowDataBound"
                    OnRowDeleting="gvInvDetails_RowDeleting" OnRowEditing="gvInvDetails_RowEditing">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    编辑
                                </td>
                                <td>
                                    删除
                                </td>
                                <td>
                                    序号
                                </td>
                                <td>
                                    产品名称
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
                                    单价
                                </td>
                                <td>
                                    总价
                                </td>
                                <td>
                                    品牌
                                </td>
                                <td>
                                    产地
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
                        <asp:TemplateField HeaderText=" 编辑">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                                    AlternateText="编辑" />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="删除"
                                    CommandName="Delete" OnClientClick='return confirm( "确定删除吗？") ' />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="No" HeaderText="序号" SortExpression="No" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="InvName" HeaderText="产品名称" SortExpression="InvName" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="InvModel" HeaderText="型号" SortExpression="InvModel" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="InvUnit" HeaderText="单位" SortExpression="InvUnit" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="InvNum" HeaderText="数量" SortExpression="InvNum" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="InvPrice" HeaderText="单价" SortExpression="InvPrice" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Total" HeaderText="金额" SortExpression="Total" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="GoodBrand" HeaderText="品牌" SortExpression="GoodBrand"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Product" HeaderText="产地" SortExpression="Product"
                            ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="InvRemark" HeaderText="备注" SortExpression="InvRemark"
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
            </td>
        </tr>
        <tr>
            <td colspan="2">
                合计：
            </td>
            <td>
                大写：
            </td>
            <td>
                <asp:Label ID="lblTotalDetailsDa" runat="server" Text="0"></asp:Label>
            </td>
            <td>
                小写：
            </td>
            <td>
                <asp:Label ID="lblTotalDetailsXiao" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;人工费用 ： <asp:TextBox ID="txtLaborCost" runat="server"  onKeyUp="GetYY();GetAllTotal();"></asp:TextBox>
            </td>
            <td colspan="2">
               计税设备材料人工基数： <asp:TextBox ID="txtRenGJS" runat="server"  Enabled="false"></asp:TextBox>
            </td>            
            <td>
                工程计税：
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtXX" runat="server" Width="30px" ></asp:TextBox>  %  
                <asp:Button ID="btnXX" runat="server" Text="计算" BackColor="Yellow" 
                    onclick="btnXX_Click"  />&nbsp;
               <asp:TextBox ID="txtEngineeringTax" runat="server"  Width="120px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
            <tr>
            <td>
                最终优惠价：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtResultYH" runat="server" Text=""  onKeyUp="GetAllTotal();"></asp:TextBox><asp:CheckBox
                    ID="cbYH" runat="server" Text="优惠"  onchange="GetAllTotal();"/>
                    报价单金额合计:  <asp:TextBox ID="txtAllTotal" runat="server"  Width="120px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td colspan="6" style="height: 20px; background-color: #D7E8FF;">
                服务条款
            </td>
        </tr>
        <tr>
             <td>
                报价概要:
            </td>
            <td colspan="5">
              
                  <asp:TextBox ID="txtRemark" runat="server" Width="95%" Text=""></asp:TextBox> <font style="color: Red">*</font>
          
            </td>
        </tr>
        <tr>
            <td>
                质量保证：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtZLBZ" runat="server" Width="95%" Text="原厂原装，正品。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                以上报价：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtYSBJ" runat="server" Width="95%" Text="已含安装、调试、运杂费、税费VAT 17%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                服务保修等级：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtFWBXDJ" runat="server" Width="95%" Text="含1年保修，保修条款按原厂保修规定执行。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                交付期：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtJFQ" runat="server" Width="95%" Text="合同签订7个工作日内。"></asp:TextBox>
            </td>
        </tr>
      

        <tr>
            <td colspan="6" style="height: 20px; background-color: #D7E8FF;">
                开票资料/银行转账
            </td>
        </tr>
        <tr>
            <td colspan="2">
                收款人:
            </td>
            <td colspan="4">
                <asp:Label ID="txtComName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                开户行:
            </td>
            <td colspan="4">
                <asp:Label ID="txtComBrand" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                帐号:
            </td>
            <td colspan="4">
                <asp:Label ID="lblZhanghao" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                社会统一信用代码:
            </td>
            <td colspan="4">
                <asp:Label ID="txtNaShuiNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改 " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
