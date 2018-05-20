<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFPONotCaiReport.aspx.cs"
    Inherits="VAN_OA.JXC.WFPONotCaiReport" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="项目未采购清单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                项目未采购清单
            </td>
        </tr>
        <tr>
            <td>
                项目编号:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="160px"></asp:TextBox> 单据号: <asp:TextBox ID="txtProNo" runat="server" Width="160px"></asp:TextBox>
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
                AE:
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList> 公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>
                订单时间:
            </td>
            <td>
                <asp:TextBox ID="txtPOTimeFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtPOTimeTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPOTimeFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPOTimeTo">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                商品编码:
                <asp:TextBox ID="txtGoodNo" runat="server" Width="200px"></asp:TextBox>
                名称/小类/规格:
                <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="100PX"></asp:TextBox>
                或者
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="100PX"></asp:TextBox>
                &nbsp;&nbsp;&nbsp; 供应商:  <asp:TextBox ID="txtSupplier" runat="server" Width="200px"></asp:TextBox>
                  <asp:CheckBox ID="cbPiPei" runat="server" Text="全匹配" />
                <asp:CheckBox ID="cbZero" runat="server" Checked="true" Text="需采数量不为0" />
            </td>
        </tr>
        <tr>
           <td>
                &nbsp;项目名称：
            </td>
            <td>
              <asp:TextBox ID="txtPOName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td colspan="2">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
        OnRowDataBound="gvMain_RowDataBound">
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
                        项目日期
                    </td>
                    <td>
                        项目金额
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td>
                        AE
                    </td>
                    <td>
                        商品编号
                    </td>
                     <td>
                        小类
                    </td>
                    <td>
                        名称
                    </td>
                    <td>
                        规格
                    </td>
                    <td>
                        项目数量
                    </td>
                    <td>
                        已采数量
                    </td>
                    <td>
                        需采数量
                    </td>
                    <td>
                        供应商
                    </td>
                    <td>
                        初步价格
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
          <asp:TemplateField HeaderText="项目编码">
                    <ItemTemplate>
                      <a href="/JXC/CG_OrderList.aspx?PONo=<%# Eval("PONo")%>" target="_blank">                    
                            <%# Eval("PONo")%></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>

            <asp:BoundField DataField="PONo" HeaderText="项目编号" SortExpression="PONo" ItemStyle-HorizontalAlign="Left" />
             <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="POTotal" HeaderText="项目金额" SortExpression="POTotal" ItemStyle-HorizontalAlign="Right"
                DataFormatString="{0:n2}" />
            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="GoodNo" HeaderText="商品编码" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Left" />
               <asp:BoundField DataField="GoodTypeSmName" HeaderText="小类" SortExpression="GoodTypeSmName" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="GoodName" HeaderText="名称" SortExpression="GoodName" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="GoodSpec" HeaderText="规格" SortExpression="GoodSpec" ItemStyle-HorizontalAlign="Left"
                DataFormatString="{0:n2}" />
            <asp:BoundField DataField="Num" HeaderText="项目数量" SortExpression="Num" ItemStyle-HorizontalAlign="Right"
                DataFormatString="{0:n2}" />
            <asp:BoundField DataField="CaiGoodSum" HeaderText="已采数量" SortExpression="CaiGoodSum"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
            <asp:BoundField DataField="lastNum" HeaderText="需采数量" SortExpression="lastNum" ItemStyle-HorizontalAlign="Right"
                DataFormatString="{0:n2}" />
            <asp:BoundField DataField="lastSupplier" HeaderText="供应商" SortExpression="lastSupplier"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="lastPrice" HeaderText="初步价格" SortExpression="lastPrice"
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n3}" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
</asp:Content>
