<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPONoIsSpecial.aspx.cs"
    Inherits="VAN_OA.JXC.SetPONoIsSpecial" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="项目归类" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                项目归类
            </td>
        </tr>
        <tr>
            <td>
                项目编号:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>公司名称：
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>
                项目单据号:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                项目日期:
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
                AE：
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
             <%--   <asp:CheckBox ID="cbClose" runat="server" Text="未关闭" ForeColor="Red" />--%>
                项目关闭:
                 <asp:DropDownList ID="ddlColse" runat="server" ForeColor="Red">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="关闭"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未关闭"></asp:ListItem>
                </asp:DropDownList>
                项目归类:
               <%-- <asp:CheckBox ID="cbSpecial" runat="server" Text="特殊" ForeColor="Red" />--%>
                  <asp:DropDownList ID="ddlSpecial" runat="server" ForeColor="Red">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="特殊"></asp:ListItem>        
                         <asp:ListItem Value="0" Text="非特殊"></asp:ListItem>           
                </asp:DropDownList>
              <%--  <asp:CheckBox ID="cbIsPoFax1" runat="server" Text="含税" AutoPostBack="True" OnCheckedChanged="cbIsPoFax_CheckedChanged"
                    ForeColor="Red" />--%>
                项目含税:
                    <asp:DropDownList ID="ddlIsPoFax" runat="server" ForeColor="Red" 
                    AutoPostBack="true" onselectedindexchanged="ddlIsPoFax_SelectedIndexChanged" >
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="含税"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不含税"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="Id" DataTextField="FpType"
                    Enabled="false">
                </asp:DropDownList>
                项目选中：
                <asp:DropDownList ID="ddlNoSelected" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="选中"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                </asp:DropDownList>
                结算选中：
                <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="选中"></asp:ListItem>
                    <asp:ListItem Value="0" Text="未选中"></asp:ListItem>
                </asp:DropDownList>
                项目类别：
                <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                    <%--   <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1">零售</asp:ListItem>
                    <asp:ListItem Value="2">工程</asp:ListItem>--%>
                </asp:DropDownList> <br />
                项目名称：
                <asp:TextBox ID="txtPOName" runat="server" Width="150px"></asp:TextBox>
                 
                项目金额：
                <asp:DropDownList ID="ddlFuHao" runat="server">
                    <asp:ListItem Value=">" Text=">"></asp:ListItem>
                    <asp:ListItem Value="<" Text="<"></asp:ListItem>
                    <asp:ListItem Value=">=" Text=">="></asp:ListItem>
                     <asp:ListItem Value="<=" Text="<="></asp:ListItem>
                       <asp:ListItem Value="=" Text="="></asp:ListItem>
                </asp:DropDownList>
                 <asp:TextBox ID="txtPoTotal" runat="server" ></asp:TextBox>
                 客户名称： <asp:TextBox ID="txtGuestName" runat="server" ></asp:TextBox>
                 发票类型：<asp:DropDownList ID="ddlFPType" runat="server" DataValueField="Id" DataTextField="FpType"></asp:DropDownList>
                <asp:TextBox ID="txtPlanDayForm" runat="server" Width="50px"></asp:TextBox>
                <asp:DropDownList ID="ddlPlanDayForm" runat="server">
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                计划完工天数
                  <asp:DropDownList ID="ddlPlanDayTo" runat="server">
                      <asp:ListItem Text=">" Value=">"></asp:ListItem>
                      <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                      <asp:ListItem Text="<" Value="<"></asp:ListItem>
                      <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                      <asp:ListItem Text="=" Value="="></asp:ListItem>
                  </asp:DropDownList>
                <asp:TextBox ID="txtPlanDayTo" runat="server" Width="50px"></asp:TextBox>
                
                <br />
                <asp:TextBox ID="txtEque1" runat="server" Width="100"></asp:TextBox>
                <asp:DropDownList ID="ddlEque1" runat="server">
                    <asp:ListItem Value="&gt;"></asp:ListItem>
                    <asp:ListItem>&gt;=</asp:ListItem>
                </asp:DropDownList>项目金额<asp:DropDownList ID="ddlEque2" runat="server">
                    <asp:ListItem Value="&gt;"></asp:ListItem>
                    <asp:ListItem>&gt;=</asp:ListItem>
                </asp:DropDownList>
                 <asp:TextBox ID="txtEque2" runat="server"  Width="100"></asp:TextBox>

                 客户类型:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType"  Width="50px">
                </asp:DropDownList>
                客户属性:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString"  Width="50px">
                </asp:DropDownList>项目模型:
                 <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
                 财本计量：
                <asp:DropDownList ID="ddlJiLiang" runat="server">
                    <asp:ListItem Value="-1" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="1" Text="计量"></asp:ListItem>
                    <asp:ListItem Value="0" Text="不计量"></asp:ListItem>
                </asp:DropDownList>
                <br />
                注：项目金额=0，项目净利=0 ，项目的特殊属性自动在关闭按钮中添加
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveIsClose" runat="server" Text="保存(关闭)" BackColor="Yellow" OnClick="btnSaveIsClose_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnIsSelected" runat="server" Text="保存(选中)" BackColor="Yellow" OnClick="btnIsSelected_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text=" 保 存 " BackColor="Yellow" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnJieIsSelected" runat="server" Text="保存(结算选中)" BackColor="Yellow"
                        OnClick="btnJieIsSelected_Click" />&nbsp;&nbsp;&nbsp;                        
                        <asp:Button ID="btnGuestPro" runat="server" Text="保存(客户属性)" BackColor="Yellow"
                        OnClick="btnGuestPro_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnGuestType" runat="server" Text="保存(客户类型)" BackColor="Yellow"
                        OnClick="btnGuestType_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    对特殊列进行编辑，点击保存后即可(保存当前页面信息);如果项目金额<成本 并且是特殊订单，背景显示粉红色

    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound"
        OnRowCommand="gvMain_RowCommand">
        <PagerTemplate>
            <br />
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>
                        单据号
                    </td>
                    <td>
                        项目编码
                    </td>
                    <td>
                        项目名称
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td>
                        项目日期
                    </td>
                    <td>
                        AE
                    </td>
                    <td>
                        特殊
                    </td>
                    <td>
                        含税
                    </td>
                    <td>
                        发票类型
                    </td>
                    <td>
                        项目类别
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
            <asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProNo" HeaderText="单据号" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="项目编码">
                <ItemTemplate>
                    <asp:Label ID="PONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
<%--
             <asp:TemplateField HeaderText="项目金额">
                <ItemTemplate>
                    <asp:Label ID="SumPOTotal" runat="server" Text='<%# GetNum(Eval("SumPOTotal")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="项目净利">
                <ItemTemplate>
                    <asp:Label ID="maoliTotal" runat="server" Text='<%# GetNum(Eval("maoliTotal")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>

              <asp:BoundField DataField="SumPOTotal" HeaderText="项目金额" DataFormatString="{0:n2}"
                    SortExpression="SumPOTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="goodTotal" HeaderText="成本" DataFormatString="{0:n2}"
                    SortExpression="goodTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                  <asp:BoundField DataField="maoliTotal" HeaderText="项目净利" SortExpression="maoliTotal" Visible="false"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            <asp:BoundField DataField="POName" HeaderText="项目名称" SortExpression="POName" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="GuestName" HeaderText="客户名称" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PODate" HeaderText="项目日期" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="关闭">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbHanShui" runat="server" Text="关闭" AutoPostBack="True" OnCheckedChanged="cbHanShui_CheckedChanged"
                        Enabled="<%# IsCloseEdist() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsClose" runat="server" Checked='<% #Eval("IsClose") %>' Enabled="<%# IsCloseEdist() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="结算选中 ">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbJieIsSelected" runat="server" Text="结算选中" AutoPostBack="True"
                        OnCheckedChanged="cbJieIsSelected_CheckedChanged" Enabled="<%# IsJieIsSelected() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbJieIsSelected" runat="server" Checked='<% #Eval("JieIsSelected") %>'
                        Enabled="<%# IsJieIsSelected() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="选中">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbIsSelected1" runat="server" Text="选中" AutoPostBack="True" OnCheckedChanged="cbIsSelected_CheckedChanged"
                        Enabled="<%# IsSelectedEdit() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSelected" runat="server" Checked='<% #Eval("IsSelected") %>'
                        Enabled="<%# IsSelectedEdit() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="特殊">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSpecial" runat="server" Checked='<% #Eval("IsSpecial") %>'
                        Enabled="<%# IsSpecialEdit() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="含税">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsPoFax" runat="server" Checked='<% #Eval("IsPoFax") %>' Enabled="<%# IsFaxEdist() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:BoundField DataField="FpType" HeaderText="发票类型" SortExpression="FpType" >
                <ItemStyle HorizontalAlign="Center" Font-Size="7"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="发票类型">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidtxt" Value='<%#Eval("FpType")%>' />
                    <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="FpType" DataTextField="FpType" style="font-size:8px;" >
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="项目模型">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidModeltxt" Value='<%#Eval("Model")%>' />                    

                    <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName"  Enabled="<%# IsModelEdit() %>" runat="server"></asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="项目类别">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidPOTypetxt" Value='<%#Eval("POType")%>' />
                    <asp:DropDownList ID="dllPOType" runat="server" Enabled="<%# IsPOType() %>" DataTextField="BasePoType"
                        DataValueField="Id">
                        <%-- <asp:ListItem Value="1">零售</asp:ListItem>
                    <asp:ListItem Value="2">工程</asp:ListItem>--%>
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="客户属性">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidGuestProtxt" Value='<%#Eval("GuestPro")%>' />
                    <asp:DropDownList ID="dllGuestPro" runat="server" Enabled="<%# IsGuestPro() %>" DataTextField="GuestProString"
                        DataValueField="GuestPro">                      
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="客户类型">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidGuestTypetxt" Value='<%#Eval("GuestType")%>' />
                    <asp:DropDownList ID="dllGuestType" runat="server" Enabled="<%# IsGuestType() %>" DataTextField="GuestType"
                        DataValueField="GuestType">                      
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

              <asp:TemplateField HeaderText="财本计量">
                <ItemTemplate>
                    <asp:CheckBox ID="cbChengBenJiLiang" runat="server" Checked='<% #Eval("ChengBenJiLiang") %>'
                        Enabled="<%# IsChengBenJiLiang() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

               <asp:TemplateField HeaderText="计划完工天数">
                <ItemTemplate>
                        <asp:TextBox ID="txtPlanDays" runat="server" Width="70px" Text='<% #Eval("PlanDays") %>'   Enabled="<%# IsPlanDays() %>" ></asp:TextBox>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
    
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

    项目模型说明： 
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
</asp:Content>
