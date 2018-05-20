<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFAllotAccountCompare.aspx.cs"
    Inherits="VAN_OA.KingdeeInvoice.WFAllotAccountCompare" Title="到款发票分配系统 " MasterPageFile="~/DefaultMaster.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">
                到款发票分配系统 
            </td>
        </tr>
        <tr>
            <td>
                客户名称:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                发票编号:
            </td>
            <td>
                <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox>
                项目编号:
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                发票金额:
            </td>
            <td>
                <asp:DropDownList ID="ddlInvTotal" runat="server">
                    <asp:ListItem Value="=">=</asp:ListItem>
                    <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                    <asp:ListItem Value="<=">&lt;=</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtInvoice" runat="server"></asp:TextBox>
                   <asp:CheckBox ID="cbZhengShu" Text="原数"  runat="server"  Checked="true"/>
            </td>
            <td>
                发票日期:
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
        </tr>
        <tr>
            <td>
                匹配:
            </td>
            <td>
                <asp:DropDownList ID="ddlCompare" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlCompare_SelectedIndexChanged">
                <asp:ListItem Value="1">匹配</asp:ListItem>                  
                </asp:DropDownList>
            </td>
            <td>
                项目关闭：
            </td>
            <td>
                <asp:DropDownList ID="ddlClose" runat="server" Width="70px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未关闭</asp:ListItem>
                    <asp:ListItem Value="1">关闭</asp:ListItem>
                </asp:DropDownList>
                项目日期:
                <asp:TextBox ID="txtPOFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtPOTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPOFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPOTo">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                项目选中:
            </td>
            <td>
                <asp:DropDownList ID="ddlIsSelect" runat="server" Width="70px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未选中</asp:ListItem>
                    <asp:ListItem Value="1">选中</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2">
                税：<asp:DropDownList ID="ddlHanShui" runat="server" Width="70px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">不含税</asp:ListItem>
                    <asp:ListItem Value="1">含税</asp:ListItem>
                </asp:DropDownList>
                Isorder:
                <asp:DropDownList ID="ddlIsorder" runat="server">
                    <asp:ListItem Value="0">真实</asp:ListItem>
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="1">非真实</asp:ListItem>
                </asp:DropDownList>
                <asp:CheckBox ID="cbIsSpecial" runat="server" Text="不含特殊" Checked="true" />
                <asp:CheckBox ID="cbJiaoFu" runat="server" Text="已交付" />
                <asp:CheckBox ID="cbInvoTotalToge" runat="server" Text="不匹配-发票金额合并" Checked="true" />
                   <asp:CheckBox ID="cbSameFP" runat="server" Text="相同发票到帐金额不同" 
                    AutoPostBack="True" oncheckedchanged="cbSameFP_CheckedChanged" />
            
            </td>
        </tr>
        <tr>
            <td>
                AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    >
                </asp:DropDownList>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td colspan="2" align="left">  比对结果类型：<asp:DropDownList ID="ddlCompareList" runat="server">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0" Text="金蝶到帐<发票金额AND OA到帐<=发票金额"></asp:ListItem>
                  <%--  <asp:ListItem Value="1">勿</asp:ListItem>
                      <asp:ListItem Value="2">OA到帐需要警示和核对</asp:ListItem>--%>
                      <%--  <asp:ListItem Value="3">OA到帐需要核对</asp:ListItem>--%>
                        <%--  <asp:ListItem Value="4">OA到帐需要警示</asp:ListItem>--%>
                </asp:DropDownList>&nbsp;&nbsp;时间:<asp:DropDownList ID="ddlTime" runat="server" Width="70px">
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0"><=30</asp:ListItem>
                    <asp:ListItem Value="1">30<未到全款<=60</asp:ListItem>
                      <asp:ListItem Value="2">60<未到全款<=90</asp:ListItem>
                        <asp:ListItem Value="3">90<未到全款<=120</asp:ListItem>
                          <asp:ListItem Value="4">未到全款>120</asp:ListItem></asp:DropDownList>

                <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" style="float:right" />
                &nbsp;<asp:Label ID="lblQuanXian" runat="server" Text="" Visible="false"></asp:Label>

            </td>
        </tr>
    </table>
    <br />
  
    到款金额:    <asp:TextBox ID="txtDaoTotal" runat="server" Width="200px"></asp:TextBox> 
    <asp:Button ID="Button1" runat="server" Text=" 计算 " BackColor="Yellow" 
        onclick="Button1_Click"  />
    <br />
   
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        Width="100%" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvList_PageIndexChanging"
        OnDataBinding="gvList_DataBinding" OnRowDataBound="gvList_RowDataBound" OnRowCreated="gvList_RowCreated"
        OnRowCommand="gvList_RowCommand">
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td colspan="9">
                        OA系统+金蝶系统
                    </td>
                    
                </tr>
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td height="25" align="center">
                        发票号
                    </td>
                    <td height="25" align="center">
                        项目编码
                    </td>
                    <td height="25" align="center">
                        OA客户名称
                    </td>
                    <td height="25" align="center">
                        金额
                    </td>
                    <td height="25" align="center">
                        OA到帐金额
                    </td>
                    <td height="25" align="center">
                        金蝶到帐金额
                    </td>
                     <td height="25" align="center">
                        OA发票未到账金额
                    </td>
                     <td height="25" align="center">
                        金蝶发票未到帐金额
                    </td>
                    <td height="25" align="center">
                        记录
                    </td>
                   
                    <tr>
                        <td colspan="4" align="center" style="height: 80%">
                            ---暂无数据---
                        </td>
                    </tr>
            </table>
        </EmptyDataTemplate>
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <Columns>
            <asp:TemplateField HeaderText="发票号码" ItemStyle-BorderColor="Black" ItemStyle-BorderWidth="1px"
                ItemStyle-BorderStyle="Solid">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="selectAll" Text='<% #Eval("All_InvoiceNo") %>'
                        CommandArgument='<% #Eval("All_InvoiceNo") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="MaxDate" HeaderText="发票日期" SortExpression="MaxDate" DataFormatString="{0:yyyy-MM-dd}" />
          <%--  <asp:BoundField DataField="All_PONO" HeaderText="项目编码" SortExpression="All_PONO"
                ItemStyle-HorizontalAlign="Center" />--%>

                  <asp:TemplateField HeaderText="项目编码">
                <ItemTemplate>
                    <asp:Label ID="lblAll_PONO" runat="server" Text='<% #Eval("All_PONO") %>'></asp:Label>                  
                </ItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField HeaderText="客户名称">
                <ItemTemplate>
                    <asp:Label ID="lblAll_OAGuestName" runat="server" Text='<% #Eval("All_OAGuestName") %>'></asp:Label>                  
                </ItemTemplate>
            </asp:TemplateField>

            <%--<asp:BoundField DataField="All_OAGuestName" HeaderText="客户名称" SortExpression="All_OAGuestName"
                ItemStyle-HorizontalAlign="Center" />--%>
            <asp:BoundField DataField="All_InvoiceTotal" HeaderText="金额" SortExpression="All_InvoiceTotal"
                DataFormatString="{0:n4}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="All_OATotal" HeaderText="OA到帐金额" SortExpression="All_OATotal"
                DataFormatString="{0:n4}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="All_AccountTotal" HeaderText="金蝶到帐金额" SortExpression="All_AccountTotal"
                DataFormatString="{0:n4}" ItemStyle-HorizontalAlign="Right" />     
                
                  <asp:BoundField DataField="All_WeiOATotal" HeaderText="OA发票未到账金额" SortExpression="All_WeiOATotal"
                DataFormatString="{0:n4}" ItemStyle-HorizontalAlign="Right" />     
                
                  <asp:BoundField DataField="All_WeiInvoiceTotal" HeaderText="金蝶发票未到帐金额" SortExpression="All_WeiInvoiceTotal"
                DataFormatString="{0:n4}" ItemStyle-HorizontalAlign="Right" />          

             <asp:TemplateField HeaderText="记录">
                <ItemTemplate>
                    <asp:LinkButton ID="lblRecord" runat="server" Text=""></asp:LinkButton>                  
                </ItemTemplate>
            </asp:TemplateField>

            
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#336699" Font-Bold="True" ForeColor="White" Font-Size="12px" />
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
    </webdiyer:AspNetPager><br /><br />
    <asp:Label ID="Label3" runat="server" Text="(OA系统+金蝶系统)"></asp:Label>
    <asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red" Visible="false"></asp:Label>
     &nbsp; &nbsp;
    <asp:Label ID="Label2" runat="server" Text="发票金额合计:"></asp:Label>
    <asp:Label ID="lblAllFPTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
     &nbsp; &nbsp;
    <asp:Label ID="Label6" runat="server" Text="OA到帐金额合计:"></asp:Label>
    <asp:Label ID="lblAllOATotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
     &nbsp; &nbsp;
    <asp:Label ID="Label8" runat="server" Text="金蝶到帐金额合计:"></asp:Label>
    <asp:Label ID="lblAllKinDaoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
     &nbsp; &nbsp;
      <asp:Label ID="Label5" runat="server" Text="OA发票未到账金额合计:"></asp:Label>
    <asp:Label ID="lblWeiAllOATotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
     &nbsp; &nbsp;
    <asp:Label ID="Label9" runat="server" Text="金蝶发票未到账金额合计:"></asp:Label>
    <asp:Label ID="lblWeiAllKinDaoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
     &nbsp; &nbsp;

    <asp:Label ID="Label10" runat="server" Text="OA和金蝶到帐金额合计差异:"></asp:Label>
    <asp:Label ID="lblALLDiffTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>

    <br /><br />
    <asp:Label ID="Label1" runat="server" Text="合计(OA系统):"></asp:Label>
    <asp:Label ID="lblOATotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label4" runat="server" Text="合计(金蝶系统):"></asp:Label>
    <asp:Label ID="lblKingdeeTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    &nbsp; &nbsp;
    <asp:TextBox ID="lblSumTotal" runat="server" TextMode="MultiLine" Width="100%" Height="200px"></asp:TextBox>
</asp:Content>
