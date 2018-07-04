<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="WFXianJinliuList.aspx.cs"
    Inherits="VAN_OA.JXC.WFXianJinliuList" MasterPageFile="~/DefaultMaster.Master"
    Title="现金流考核" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style type="text/css">
        .item
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                现金流考核
            </td>
        </tr>
        <tr>
            <td>
                项目编码:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONO" runat="server" Width="200px"></asp:TextBox>
                项目名称:
                <asp:TextBox ID="txtPOName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>
         AE：
        </td>
        <td> <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList></td>     
            <td>
                发票号:
            </td>
            <td>
                <asp:TextBox ID="txtFPNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                订单时间:
            </td>
            <td>
                <asp:TextBox ID="txtPoDateFrom" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtPoDateTo" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                发票状态:
            </td>
            <td>
                <asp:DropDownList ID="ddlFPState" runat="server" Width="200px">
                    <asp:ListItem Value="0">所有</asp:ListItem>
                    <asp:ListItem Value="1">已开全票</asp:ListItem>
                    <asp:ListItem Value="2">未开全票</asp:ListItem>
                    <asp:ListItem Value="3">未开票</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                项目金额：
            </td>
            <td colspan="3"> 
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Text=">="  Value=">=" ></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtPOTotal" runat="server" Width="200px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;  <asp:CheckBox ID="cbPOHeBing" runat="server" Text="到款金额合并"  Enabled="false" Checked="true" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel1" runat="server" Enabled="true" Style="display: inline">
                    项目金额
                    <asp:DropDownList ID="ddlJinECha" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlJinECha_SelectedIndexChanged">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                    </asp:DropDownList>
                     到款金额&nbsp;&nbsp;&nbsp;&nbsp; 未到款时间:
                    <asp:DropDownList ID="ddlDiffDays" runat="server" Enabled="false">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<=30天" Value="1"></asp:ListItem>
                        <asp:ListItem Text=">30天AND<=60天" Value="2"></asp:ListItem>
                        <asp:ListItem Text=">60天AND <=90天" Value="3"></asp:ListItem>
                        <asp:ListItem Text=">90天AND <=120天" Value="4"></asp:ListItem>
                        <asp:ListItem Text=">90天" Value="5"></asp:ListItem>
                        <asp:ListItem Text=">120天" Value="6"></asp:ListItem>
                        <asp:ListItem Text=">180天" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                &nbsp;&nbsp;&nbsp;&nbsp; 到款单金额:
                 <asp:DropDownList ID="ddlDaoKuanTotal" runat="server">
                    <asp:ListItem Text=">" Value=">">
                    </asp:ListItem>
                    <asp:ListItem Text="<" Value="<">
                    </asp:ListItem>
                    <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                     <asp:ListItem Text="<=" Value="<=" ></asp:ListItem>
                    <asp:ListItem Text="=" Value="=">
                    </asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtDaoKuanTotal" runat="server"></asp:TextBox><br />
                项目关闭 :
                <asp:DropDownList ID="ddlPoClose" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="关闭"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未关闭"></asp:ListItem>
                </asp:DropDownList>
                项目选中：
                <asp:DropDownList ID="ddlIsSelect" runat="server" Width="70px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未选中</asp:ListItem>
                    <asp:ListItem Value="1">选中</asp:ListItem>
                </asp:DropDownList>
                结算选中：
                <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="选中"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                </asp:DropDownList>
                项目类别：
                <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                </asp:DropDownList>
                客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType">
                </asp:DropDownList>
                客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Style="left: 0px;">
                </asp:DropDownList>
                项目模型:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>

                &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <div style="display: inline">
                    <asp:DropDownList ID="ddlNoSpecial" runat="server">
                        <asp:ListItem Value="0">不含特殊</asp:ListItem>
                        <asp:ListItem Value="1">特殊</asp:ListItem>
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlShui" runat="server">
                        <asp:ListItem Value="-1">全部</asp:ListItem>
                        <asp:ListItem Value="1">含税</asp:ListItem>
                        <asp:ListItem Value="0">不含税</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox ID="cbHadJiaoFu" runat="server" Text="已交付" />
                    <asp:CheckBox ID="cbPoNoZero" runat="server" Text="项目不为0" />
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;项目金额
                    <asp:DropDownList ID="dllCompareSell" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                    </asp:DropDownList>
                    销售金额&nbsp;&nbsp; 项目金额
                    <asp:DropDownList ID="dllCompareFP" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                    </asp:DropDownList>
                    发票金额 &nbsp;&nbsp; 项目金额
                    <asp:DropDownList ID="dllCompareInvoice" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                    </asp:DropDownList>
                    实到帐 
                    <br />
                    到款率: 
                    <asp:TextBox ID="txtDaoKLvFrom" runat="server" Width="50px" Text="0"></asp:TextBox><label style=" color:Red">%</label>~<asp:TextBox ID="txtDaoKLvTo" Text="100" runat="server" Width="50px"></asp:TextBox><label style=" color:Red">%</label>
                    盈利能力:
                    <asp:TextBox ID="txtYLFrom" runat="server" Width="50px"></asp:TextBox><label style=" color:Red">%</label>~<asp:TextBox ID="txtYLTo" runat="server" Width="50px"></asp:TextBox><label style=" color:Red">%</label>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    注：资金回笼率=到款额/（支付总额+库存出库成本总额+费用总额），资金占用=支付总额+库存出库成本总额+费用总额-到款额，盈利能力=实际利润/（支付总额+库存出库成本总额+费用总额），盈利效率=盈利能力×到款率 ；库存出库成本总额是该项目采购来自库存的商品的已出库金额。
    <div>
    1.     资金投入=支付总额+库存出库成本总额+费用总额；未出库总额=入库未出数量×库存均价+采库未出数量×库存均价，这里的入库未出数量扣除采购未检验的数量；未收款 是 项目金额-到款额 ；到账比率=到帐额/项目金额（如果项目金额=0；该项按空白）；支付比率=支付总额/采购单的总金额 （如果采购金额=0；该项按空白）；
    </div>
        <div>
2.     费用率=费用总额/项目金额（如果项目金额=0；该项按空白）
</div>    <div>
3.     预估成本、管理费、预估利润 是项目订单中的 成本估算总额，管理费和利润总额；在途库存是 采购后未入库的金额（只含外采部分）；未出库总额是 （含外采入库后未出额 和 内采未出库额：这里均按 库存均价计）； 出库成本为出库的总成本；采购总额是采购的总金额；
净预估利润=原预估利润-
（销售退货总销售额-销售退货总成本）；净出库成本=原出库成本-销售退货引起的出
库成本；净采购总额=原采购额-采购退货产生的金额；
净支付额是该项目通过供应商预付和支付的总金额（转支付单不能重复计入）=原支付
额-事后采购退货生成的即将或已完成负数支付单金额；
</div>
4.     支付总额是该项目通过供应商预付和支付的总金额（转支付单不能重复计入）；费用总额是指通过 公交车费，私车油耗费，用车申请油耗费，申请请款单，预期报销单，预期报销单（油费），加班单 的总额。 库存出库成本总额=Σ（净库存出库数量-扣除数量）×该商品的出库成本，
净库存出库数量=MIN(该商品出库数量, 采购来自库存的数量), 扣除数量= MIN(该商品
销售退货的数量,采购来自库存的数量)
   <div> 5.所有的列表中显示名称为 比率和率的值单位均是 “<label style=" color:Red">%</label>”，注意百分号用红色显示</div>
   <div>6.净支价总额=SUM(支付数量×支付单价×采购单价/实采单价)- SUM（事后采购退货生成的即将或已完成负数支付单金额×采购金额/实采单价）</div>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
             Width="200%" AllowPaging="True" AutoGenerateColumns="False"
            OnPageIndexChanging="gvList_PageIndexChanging" OnRowDataBound="gvList_RowDataBound"
            OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            项目编号
                        </td>
                        <td>
                            项目名称
                        </td>
                        <td>
                            AE
                        </td>
                        <td>
                            项目金额
                        </td>
                        <td>
                            含税
                        </td>
                        <td>
                            预估成本
                        </td>
                        <td>
                            管理费
                        </td>
                        <td>
                            预估利润
                        </td>
                        <td>
                            在途库存
                        </td>
                        <td>
                            未出库总额
                        </td>
                        <td>
                            出库成本
                        </td>
                        <td>
                            采购总额
                        </td>
                        <td>
                            支付总额
                        </td>
                        <td>
                            费用总额
                        </td>
                        <td>
                            开票总额
                        </td>
                        <td>
                            到账额
                        </td>
                        <td>
                            利润
                        </td>
                        <td>
                            未收款
                        </td>
                        <td>
                            到账比率
                        </td>
                        <td>
                            支付比率
                        </td>
                        <td>
                            费用率
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
                <asp:BoundField DataField="PONO" HeaderText="项目编号" SortExpression="PONO" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PoName" HeaderText="项目名称" SortExpression="PoName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
               
               
                <asp:TemplateField HeaderText="税">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsHanShui" runat="server" Checked='<% #Eval("IsHanShui") %>'
                            Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Right" />
                </asp:TemplateField>
                 <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" ItemStyle-HorizontalAlign="Right" />
                  <asp:BoundField DataField="GoodSellPriceTotal" HeaderText="销售金额" SortExpression="GoodSellPriceTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="CostPrice" HeaderText="预估成本" SortExpression="CostPrice"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="OtherCostm" HeaderText="管理费" SortExpression="OtherCostm"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Profit" HeaderText="净预估利润" SortExpression="Profit" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="NotRuTotal" HeaderText="在途库存" SortExpression="NotRuTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="NotRuSellTotal" HeaderText="未出库总额" SortExpression="NotRuSellTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="SellOutTotal" HeaderText="净出库成本" SortExpression="SellOutTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="LastCaiTotal" HeaderText="净采购总额" SortExpression="LastCaiTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="SupplierTotal" HeaderText="净支付总额" SortExpression="SupplierTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="LastSupplierTotal" HeaderText="净支价总额" SortExpression="LastSupplierTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="ItemTotal" HeaderText="费用总额" SortExpression="ItemTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FPTotal" HeaderText="开票总额" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="InvoiceTotal" HeaderText="到账额" SortExpression="InvoiceTotal"
                    ItemStyle-HorizontalAlign="Right" />

                <asp:TemplateField HeaderText="实际利润">
                    <HeaderTemplate>
                       <span>实际利润</span>
                        <asp:Button ID="Button1" runat="server" Text="-" Width="20px" OnClick="btnAdd_Click"  BackColor="Yellow" />
                         
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLiRunTotal" runat="server" Text='<%# Eval("LiRunTotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

               
                  <asp:BoundField DataField="NotKuCunTotal" HeaderText="库存出库成本总额" SortExpression="NotKuCunTotal"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                       <asp:BoundField DataField="ZJTouRu" HeaderText="资金投入" SortExpression="ZJTouRu"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>

                      <asp:BoundField DataField="ZJHLV" HeaderText="资金回笼率" SortExpression="ZJHLV"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="TempTopZJHLV" HeaderText="最高资金回笼率" SortExpression="TopZJHLV"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="ZJZY" HeaderText="资金占用" SortExpression="ZJZY"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="TempTopZJZY" HeaderText="最高资金占用" SortExpression="TopZJZY"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="YLNL" HeaderText="盈利能力" SortExpression="YLNL"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="TempTopYLNL" HeaderText="最高盈利能力" SortExpression="TopYLNL"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="YLLV" HeaderText="盈利效率" SortExpression="YLLV"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                      <asp:BoundField DataField="TempTopYLLV" HeaderText="最高盈利效率" SortExpression="TopYLLV"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>

                <asp:BoundField DataField="NotShouTotal" HeaderText="未收款" SortExpression="NotShouTotal"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="InvoiceBiLiTotal" HeaderText="到账比率" SortExpression="InvoiceBiLiTotal"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                <asp:BoundField DataField="SupplierBiliTotal" HeaderText="支付比率" SortExpression="SupplierBiliTotal"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" />
                <asp:BoundField DataField="FeiYongTotal" HeaderText="费用率" SortExpression="FeiYongTotal"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                     <asp:BoundField DataField="FPNoTotal" HeaderText="发票号" SortExpression="FPNoTotal" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="30%" ItemStyle-CssClass="item" />
            </Columns>
            <PagerStyle HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle ForeColor="Black" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
            PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
            NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
    项目金额合计:
    <asp:Label ID="lblAllPoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    总在途库存:
    <asp:Label ID="lblNotRuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    总未出库总额:
    <asp:Label ID="lblNotRuSellTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    总出库成本:
    <asp:Label ID="lblSellOutTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     总采购额:
    <asp:Label ID="lblLastCaiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label><br />
     总支付金额:
    <asp:Label ID="lblSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    净支价合计:
    <asp:Label ID="lblLastSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     总费用额:
    <asp:Label ID="lblItemTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     总开票额:
    <asp:Label ID="lblFPTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     总到款金额:
    <asp:Label ID="lblInvoiceTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    总利润:
    <asp:Label ID="lblLiRunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    总未收:
    <asp:Label ID="lblNotShouTotal" runat="server" Text="0" ForeColor="Red"></asp:Label><br />
    总库存出库成本额：  <asp:Label ID="lblKuCunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;  
    总资金投入额：  <asp:Label ID="lblZiJinTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    <br />
  
     到帐总比率:
    <asp:Label ID="lblInvoiceBiLiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
     支付总比率:
    <asp:Label ID="lblSupplierBiliTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
     总费用率:
    <asp:Label ID="lblFeiYongTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
    <br />


      资金回笼率:
    <asp:Label ID="lblZJHLV" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
      资金占用:
    <asp:Label ID="lblZJZY" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      盈利能力:
    <asp:Label ID="lblYLNL" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
      盈利效率:
    <asp:Label ID="lblYLLV" runat="server" Text="0" ForeColor="Red"></asp:Label>%&nbsp;&nbsp;
    
</asp:Content>
