<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pro_JSXDetailInfoList.aspx.cs"
    Inherits="VAN_OA.JXC.Pro_JSXDetailInfoList" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="进销存出入库明细" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                进销存出入库明细
            </td>
        </tr>
        <tr>
            <td>
                日期时间:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                仓库:
            </td>
            <td>
                <asp:DropDownList ID="ddlHouse" DataTextField="houseName" DataValueField="id" runat="server"
                    Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                编码
            </td>
            <td>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="200px"></asp:TextBox>
            </td>
             <td>
                项目编码
            </td>
            <td>
                <asp:TextBox ID="txtPONO" runat="server" Width="200px"></asp:TextBox>                  
            </td>
        </tr>
        <tr>
            <td>
                商品
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtInvName" runat="server" Width="95%"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServicePath="../MyWebService/MyWebService.asmx"
                    ServiceMethod="GetGoods" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10"
                    TargetControlID="txtInvName">
                </cc1:AutoCompleteExtender>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                <asp:Label ID="Label1" runat="server" Text="编码\名称\小类\规格\型号\单位\库存数量" ForeColor="Red"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>

    仓位:<asp:Label ID="lblGoodAreaNumber" runat="server"  Text=""></asp:Label>   &nbsp;&nbsp;&nbsp;&nbsp;名称\小类\规格：<asp:Label ID="lblGoodInfo" runat="server" Text=""></asp:Label>
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AllowPaging="false" AutoGenerateColumns="False" OnRowDataBound="gvMain_RowDataBound">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        日期
                    </td>
                    <td>
                        分类
                    </td>
                    <td>
                        摘要
                    </td>
                    <td>
                        项目编码
                    </td>
                    <td>
                        单据号
                    </td>
                    <td>
                        价格
                    </td>
                    <td>
                        入库数
                    </td>
                    <td>
                        发出数
                    </td>
                    <td>
                        结余数
                    </td>
                      <td>
                        已支付
                    </td>
                    <td>
                        未支付
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
            <asp:BoundField DataField="RuTime" HeaderText="日期" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="TypeName" HeaderText="分类" SortExpression="TypeName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PONO" HeaderText="项目编号" SortExpression="PONO" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="allRemark" HeaderText="摘要" SortExpression="allRemark"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Price" HeaderText="出/入价格" SortExpression="Price" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodInNum" HeaderText="入库数" SortExpression="GoodInNum"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodOutNum" HeaderText="发出数" SortExpression="GoodOutNum"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GoodResultNum" HeaderText="结余数" SortExpression="GoodResultNum"
                ItemStyle-HorizontalAlign="Center" />
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
    </asp:GridView>

    已支付合计：<asp:Label ID="lblHadInvoice" runat="server" Text="0" ForeColor="Red" style="margin-right:20px"></asp:Label>
     未支付合计：<asp:Label ID="lblNoInvoice" runat="server" Text="0" ForeColor="Red" style="margin-right:20px"></asp:Label>
</asp:Content>
