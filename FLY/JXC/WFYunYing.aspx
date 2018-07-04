<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="WFYunYing.aspx.cs"
    Inherits="VAN_OA.JXC.WFYunYing" MasterPageFile="~/DefaultMaster.Master" Title="运营指标" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style type="text/css">
        .item
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>

   
     <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                运营指标
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
                公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
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
                 <asp:DropDownList ID="ddlDaoKuanTotal" runat="server" >
                    <asp:ListItem Text=">" Value=">">
                    </asp:ListItem>
                    <asp:ListItem Text="<" Value="<">
                    </asp:ListItem>
                    <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                     <asp:ListItem Text="<=" Value="<=" ></asp:ListItem>
                    <asp:ListItem Text="=" Value="=">
                    </asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtDaoKuanTotal" runat="server" Width="120px"></asp:TextBox>
               <%-- 库存未付:
                 <asp:DropDownList ID="ddlKuCunWeiTotal" runat="server">
                     <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">">
                    </asp:ListItem>
                    <asp:ListItem Text="<" Value="<">
                    </asp:ListItem>
                    <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                     <asp:ListItem Text="<=" Value="<=" ></asp:ListItem>
                    <asp:ListItem Text="=" Value="=">
                    </asp:ListItem>
                </asp:DropDownList>--%>
               <%-- <asp:TextBox ID="txtKuCunWeiTotal" runat="server" Width="120px"></asp:TextBox>--%>
                <br />
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
                
                 <asp:DropDownList ID="ddlIsPoFax" runat="server">
                   <asp:ListItem Value="-1">全部</asp:ListItem>                       
                        <asp:ListItem Value="1">含税</asp:ListItem>
                         <asp:ListItem Value="0">不含税</asp:ListItem>                      
                    </asp:DropDownList>
                   
                    净采购总额:
                      <asp:DropDownList ID="ddlJCGTotal" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtJCGTotal" runat="server" Width="50px"></asp:TextBox>
                     净支付总额:
                      <asp:DropDownList ID="ddlJZFTotal" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtJZFTotal" runat="server" Width="50px"></asp:TextBox>
                     应付总额:
                      <asp:DropDownList ID="dllYFTotal" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtYFTotal" runat="server" Width="50px"></asp:TextBox><br />
                     应付库存:
                      <asp:DropDownList ID="ddlYFKCTotal" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtYFKCTotal" runat="server" Width="50px"></asp:TextBox>
                     预付未到库:
                      <asp:DropDownList ID="dllYFWDKTotal" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtYFWDKTotal" runat="server" Width="50px"></asp:TextBox>
                     项目净利:
                      <asp:DropDownList ID="ddlXMJLTotal" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtXMJLTotal" runat="server" Width="50px"></asp:TextBox>
                     实际利润:
                      <asp:DropDownList ID="ddlSJLRTotal" runat="server">
                        <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtSJLRTotal" runat="server" Width="50px"></asp:TextBox>
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
   
            <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
            
        <tr>
            <td>
                包含非特殊:<asp:CheckBox ID="cbFeiTS" runat="server"  Checked="true"/>
            </td>
            <td colspan="1">
               预期应收：
            </td>
            <td><asp:DropDownList ID="ddlYQYS" runat="server">
               <asp:ListItem Text=">" Value=">"></asp:ListItem>
                  <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                           <asp:ListItem Text="=" Value="="></asp:ListItem>
                              <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtYQYS" runat="server" Text="0"></asp:TextBox>
            </td>
            <td>应付总额：</td>
            <td>
                <asp:DropDownList ID="ddlYFZE" runat="server">
               <asp:ListItem Text=">" Value=">"></asp:ListItem>
                  <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                           <asp:ListItem Text="=" Value="="></asp:ListItem>
                              <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtYFZE" runat="server" Text="0"></asp:TextBox>
            </td>
           
        </tr>

            <tr>
            <td>
                包含特殊 :<asp:CheckBox ID="cbTS" runat="server" Checked="true"/>
            </td>
            <td colspan="1">
               预期应收：
            </td>
            <td><asp:DropDownList ID="ddlYQYS1" runat="server">
               <asp:ListItem Text=">" Value=">"></asp:ListItem>
                  <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                           <asp:ListItem Text="=" Value="="></asp:ListItem>
                              <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtYQYS1" runat="server" Text="0"></asp:TextBox>
            </td>
            <td>应付总额：</td>
            <td>
                <asp:DropDownList ID="ddlYFZE1" runat="server">
                     <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
               <asp:ListItem Text=">" Value=">"></asp:ListItem>
                  <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                           <asp:ListItem Text="=" Value="="></asp:ListItem>
                             
                </asp:DropDownList>
                <asp:TextBox ID="txtYFZE1" runat="server"></asp:TextBox>
            </td>
           
        </tr>
         <tr>
            <td colspan="5">
                <div align="right">
                    <asp:Button ID="Button1" runat="server" Text=" 查 询2 " BackColor="Yellow" OnClick="btnSelect2_Click" />
                </div>
            </td>
        </tr>
       </table>
           
    注：盈利能力=实际利润/（支付总额+库存出库金额+费用总额）
     项目应收=销售金额-到帐额；预期应收=项目金额--到帐额
净采购总额=原采购额-采购退货产生的金额；净支付额是该项目通过供应商预付和支付的总金额（转支付单不能重复计入）=原支付额-事后采购退货生成的即将或已完成负数支付单金额；
应付库存反应的=这个项目采购来自库存的 总金额（就是通过KC 支付出去的金额）;应付总额 =净采购总额-净支价总额-应付库存;运营总盘子=库存总金额+项目应收合计-项目应付合计+预付未到库合计+ KC预付未到库合计-库存未支付合计；
    1. 未来体系总盘子=库存总金额+项目应收合计-项目应付合计+预付未到库合计+ KC预付未到库合计-库存未支付合计，这里的项目的特殊的过滤条件按下面方框内的特殊的属性来：非特殊项目的预期应收>0和应付总额>0,以及特殊项目的预期应收>0 来的；而忽略界面到款率上方的特殊下拉框属性，其他按屏幕上的条件照旧
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvList_PageIndexChanging"
            OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
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
                            税
                        </td>
                        <td>
                            项目金额
                        </td>
                        <td>
                            销售金额
                        </td>
                        <td>
                            开票金额
                        </td>
                        <td>
                            未开票额
                        </td>
                        <td>
                            到款额
                        </td>
                        <td>
                            项目应收
                        </td>
                        <td>
                            净采购总额
                        </td>
                        <td>
                             净支付总额
                        </td>
                        <td>
                            应付总额
                        </td>
                        <td>
                            项目利润
                        </td>
                        <td>
                            实际利润
                        </td>
                        <td>
                            到款率
                        </td>
                        <td>
                            盈利能力
                        </td>
                    </tr>
                    <tr>
                        <td colspan="17" align="center" style="height: 80%">
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
                <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <asp:BoundField DataField="GoodSellPriceTotal" HeaderText="销售金额" SortExpression="GoodSellPriceTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FPTotal" HeaderText="开票总额" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <asp:BoundField DataField="NoFpTotal" HeaderText="未开票额" SortExpression="NoFpTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="InvoiceTotal" HeaderText="到账额" SortExpression="InvoiceTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="YingShouTotal" HeaderText="项目应收" SortExpression="YingShouTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                       <asp:BoundField DataField="YuQiYingShou" HeaderText="预期应收" SortExpression="YuQiYingShou" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="LastCaiTotal" HeaderText="净采购总额" SortExpression="LastCaiTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="SupplierTotal" HeaderText="净支付总额" SortExpression="SupplierTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                          <asp:BoundField DataField="LastSupplierTotal" HeaderText="净支价总额" SortExpression="LastSupplierTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="YingFuTotal" HeaderText="应付总额" SortExpression="YingFuTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                     <asp:BoundField DataField="YingFuKuCun" HeaderText="应付库存" SortExpression="YingFuKuCun" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                      <asp:BoundField DataField="NoInvoice" HeaderText="库存未付" SortExpression="NoInvoice" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />

                      <asp:BoundField DataField="YuFuWeiTotal" HeaderText="预付未到库" SortExpression="YuFuWeiTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />

                     <asp:BoundField DataField="XuJianTotal" HeaderText="已支付在途" SortExpression="XuJianTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="MaoLiTotal" HeaderText="项目净利" SortExpression="MaoLiTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <asp:BoundField DataField="LiRunTotal" HeaderText="实际利润" SortExpression="LiRunTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="InvoiceBiLiTotal" HeaderText="到账率" SortExpression="InvoiceBiLiTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right"  />
                <asp:BoundField DataField="YLNL" HeaderText="盈利能力" SortExpression="YLNL" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                     />
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
    销售金额合计:
     <asp:Label ID="lblSellTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      开票金额合计:
    <asp:Label ID="lblFPTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      未开票额合计:
    <asp:Label ID="lblNoFpTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
       到款额合计:
    <asp:Label ID="lblInvoiceTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
       项目应收合计:
    <asp:Label ID="lblYingShouTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      预期应收合计:
    <asp:Label ID="lblYuQiYingShouTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

    <br />

     采购金额合计:
    <asp:Label ID="lblLastCaiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      支付金额合计:
    <asp:Label ID="lblSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      净支价合计:
    <asp:Label ID="lblLastSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      应付合计:
    <asp:Label ID="lblYingFuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     应付库存合计:
    <asp:Label ID="lblYingFuKuCunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
         预付未到库合计:
    <asp:Label ID="lblXuJianTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

       项目净利合计:
    <asp:Label ID="lblMaoLi" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
       实际利润合计:
    <asp:Label ID="lblLiRunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

    <br />
     到款总比率:
    <asp:Label ID="lblInvoiceBiLiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      盈利能力总比率:
    <asp:Label ID="lblYLNL" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     <br />
      库存总金额:
    <asp:Label ID="lblKuCunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

       KC预付未到库合计:
    <asp:Label ID="lblKCXuJianTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    库存未支付合计:
    <asp:Label ID="lblKCWeiZhiFuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      运营总盘子:
    <asp:Label ID="lblYingYunAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     未来体系总盘子:
    <asp:Label ID="WeilblYingYunAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

</asp:Content>
