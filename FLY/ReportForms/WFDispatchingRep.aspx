<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="WFDispatchingRep.aspx.cs"
    Inherits="VAN_OA.ReportForms.WFDispatchingRep" MasterPageFile="~/DefaultMaster.Master"
    Title="各月公司派工单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                各月公司派工单
            </td>
        </tr>
        <tr>
            <td>
                时间:
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
                随同人
            </td>
            <td>
                <asp:TextBox ID="txtSuiTong" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                被派工人
            </td>
            <td>
                <asp:TextBox ID="txtOutDispater" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                派工人:
            </td>
            <td>
                <asp:TextBox ID="txtDispatcher" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AutoGenerateColumns="false"  
        OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound" 
        onrowcommand="gvList_RowCommand">
        <PagerTemplate>
            <br />
            <%--<asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
        <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页"  Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First" ></asp:LinkButton>
         <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"  ></asp:LinkButton>
        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next" ></asp:LinkButton>
         <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页"   Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last" ></asp:LinkButton>
         <br />--%>
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        月
                    </td>
                    <td>
                        客户地址
                    </td>
                    <td>
                        工作内容
                    </td>
                    <td>
                        外出时间
                    </td>
                    <td>
                        返回时间
                    </td>
                    <td>
                        派遣人
                    </td>
                    <td>
                        外派人
                    </td>
                    <td>
                        随同人
                    </td>
                    
                    <td>
                        附件
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
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblM" runat="server" Text='<%# Eval("Mouth") %>' Width="70px"></asp:Label>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="GuestAddress">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="Content">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="GoTime">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="BackTime">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="Name">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="OutDispater">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            
              <asp:BoundField DataField="SuiTongRen">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1"  Width="100px" runat="server" CommandName="SeeFile" CommandArgument='<% #Eval("HiddFileName") %>' Text='<% #Eval("FileName") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
   <%-- <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="跳转到第" TextAfterPageIndexBox="页" CustomInfoSectionWidth="40%" Visible="false"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录"
        PageSize="10" CurrentPageIndex="1" FirstPageText="首页" LastPageText="尾页" PrevPageText="上页"
        NextPageText="下页" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>--%>
</asp:Content>
