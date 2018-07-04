<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CG_OrderList.aspx.cs" Inherits="VAN_OA.JXC.CG_OrderList"
    Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master" Title="����������" %>
<%@ Import Namespace="VAN_OA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <style type="text/css">
        .item
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>

    <script src="../Scripts/tinybox.js" type="text/javascript"></script>

    <script src="../Scripts/tinyboxCu.js" type="text/javascript"></script>

    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                ��Ŀ�����б�
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ���:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
            </td>
            <td>
                ��Ŀ����:
            </td>
            <td>                        
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>��˾���ƣ�
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀʱ��:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server" Width="70px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>            
            <td colspan="2"> ����״̬:
                <asp:DropDownList ID="ddlStatue" runat="server" Width="50px" >
                    <asp:ListItem>ͨ��+ִ��</asp:ListItem>
                    <asp:ListItem>ִ����</asp:ListItem>
                    <asp:ListItem>ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
                ��Ŀ����:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px" >
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">������</asp:ListItem>
                    <asp:ListItem Value="1">����</asp:ListItem>
                </asp:DropDownList>
                ��Ŀ�رգ�
                <asp:DropDownList ID="ddlClose" runat="server" Width="50px">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">δ�ر�</asp:ListItem>
                    <asp:ListItem Value="1">�ر�</asp:ListItem>
                </asp:DropDownList>
                 ��Ŀѡ�У�
                <asp:DropDownList ID="ddlIsSelect" runat="server" Width="50px" >
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">δѡ��</asp:ListItem>
                    <asp:ListItem Value="1">ѡ��</asp:ListItem>
                </asp:DropDownList>
                ����ѡ�У�
                   <asp:DropDownList ID="ddlJieIsSelected" runat="server" Width="50px">
                <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                <asp:ListItem Value="1" Text="ѡ��"></asp:ListItem>
                <asp:ListItem Value="0" Text="δѡ��"></asp:ListItem>
                </asp:DropDownList>
                  �ͻ�����:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType"  Width="50px">
                </asp:DropDownList>
                �ͻ�����:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString"  Width="50px">
                </asp:DropDownList>
                ��Ŀģ��:
                 <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                �ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>
            </td>
            <td>
                ��Ŀ״̬
            </td>
            <td> 
                <asp:CheckBox ID="CheckBox1" runat="server" Text="��ʼ״̬" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="�ѽ���" />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="�ѿ�Ʊ" />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="�ѽ���" />
                <asp:CheckBox ID="CheckBox5" runat="server" Text="���ⵥǩ��" />
                <asp:CheckBox ID="CheckBox6" runat="server" Text="��Ʊ��ǩ��" />
                <asp:CheckBox ID="cbIsPoFax" runat="server" Text="����˰" />
            </td>
        </tr>
        <tr>
            <td>
                AE��
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>��Ʒ����: <asp:TextBox ID="txtGoodNo" runat="server" Width="100px"></asp:TextBox>����/С��/���: <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="100PX"></asp:TextBox>
                ����
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="100PX"></asp:TextBox>  ��Ŀ���
                  <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                  <%--  <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1">����</asp:ListItem>
                    <asp:ListItem Value="2">����</asp:ListItem>--%>
                </asp:DropDownList>
                ���ڷ���:   <asp:DropDownList ID="ddlZhangqi" runat="server">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value=">">ʵ������>���ڽ�ֹ��</asp:ListItem>
                    <asp:ListItem Value="<=">ʵ������<=���ڽ�ֹ��</asp:ListItem>
                </asp:DropDownList>

                ��Ʊ״̬�� <asp:DropDownList ID="ddlFPState" runat="server" >
                    <asp:ListItem Value="0">ȫ��</asp:ListItem>
                     <asp:ListItem Value="3">δ��Ʊ</asp:ListItem>
                       <asp:ListItem Value="2">δ��ȫƱ</asp:ListItem>
                    <asp:ListItem Value="1">�ѿ�ȫƱ</asp:ListItem> 
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
     <table width="100%" border="0">
        <tr>
            <td>
        <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Id" Width="160%" AllowPaging="True" AutoGenerateColumns="False"
            OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound"
            OnRowCommand="gvMain_RowCommand">
            <PagerTemplate>
                <br />
             
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            ���ݺ�
                        </td>
                        <td>
                            ��Ŀ״̬
                        </td>
                        <td>
                            ��Ŀ����
                        </td>
                        <td>
                            ��Ŀ����
                        </td>
                        <td>
                            ��Ŀ����
                        </td>
                        <td>
                            ��Ŀ���
                        </td>
                        <td>
                            ����
                        </td>
                        <td>
                            �ͻ�ID
                        </td>
                        <td>
                            �ͻ�����
                        </td>
                        <td>
                            AE
                        </td>
                        <td>
                            INSIDE
                        </td>
                        <td>
                            ��Ʊ
                        </td>
                        <td>
                            ״̬
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">
                            ---��������---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("PONo") %>'>�鿴</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="ProNo" HeaderText="���ݺ�" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblSpecial" runat="server" Text='<%# IsSpecial(Eval("IsSpecial")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="POStatue" HeaderText="��Ŀ״̬" SortExpression="POStatue"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <a href="/JXC/Sell_OrderOutHouseBackList.aspx?Type=<%# GetStateType(Eval("POStatue5")) %>&PONo=<%# Eval("PONo") %>">
                            <%# Eval("POStatue5") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��Ʊ">
                    <ItemTemplate>
                        <a href="/JXC/Sell_OrderPFBackList.aspx?Type=-1&PONo=<%# Eval("PONo") %>">
                            <%# Eval("POStatue6") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��Ŀ����">
                    <ItemTemplate>
                        <a href="javascript:tinybox_Showiframe('WFPOEnclosure.aspx?PONo=<%# Eval("PONo") %>',1000,600);">
                            <%# Eval("PONo")%></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <%--  <asp:BoundField DataField="PONo" HeaderText="��Ŀ����" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />--%>
                <asp:BoundField DataField="POName" HeaderText="��Ŀ����" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PODate" HeaderText="��Ŀ����" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="POTotal" HeaderText="��Ŀ���" SortExpression="POTotal" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:f2}"/>
                <asp:BoundField DataField="POPayStype" HeaderText="����" SortExpression="POPayStype"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="˰">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsPoFax" runat="server" Checked='<% #Eval("IsPoFax") %>' Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="GuestNo" HeaderText="�ͻ�ID" SortExpression="GuestNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestType" HeaderText="�ͻ�����" SortExpression="GuestType"
                    ItemStyle-HorizontalAlign="Center" />                
                    
                     <asp:TemplateField HeaderText="�ͻ�����" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# GetGestProInfo(Eval("GuestPro"))%>
                </ItemTemplate>
            </asp:TemplateField>
            
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="INSIDE" HeaderText="INSIDE" SortExpression="INSIDE" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Status" HeaderText="״̬" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="POTypeString" HeaderText="����" SortExpression="POTypeString" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Model" HeaderText="ģ��" SortExpression="Model" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FPTotal" HeaderText="��Ʊ" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="40%" ItemStyle-CssClass="item" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" ForeColor="Black" />
        </asp:GridView>
         <webdiyer:AspNetPager ID="AspNetPager6" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
                    PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
                    NextPageText="��ҳ" OnPageChanged="AspNetPager6_PageChanged">
                </webdiyer:AspNetPager>
                  </td>
        </tr>
        </table>
    </asp:Panel>
    --��Ŀ��Ϣ<br/>
    ��Ʒ����: <asp:TextBox ID="txtGoodNo1" runat="server" Width="100px"></asp:TextBox>����/С��/���: <asp:TextBox ID="txtNameOrTypeOrSpec1" runat="server" Width="100PX"></asp:TextBox>
                ����
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo1" runat="server" Width="100PX"></asp:TextBox> 
                ������   <asp:DropDownList ID="ddlFuHao" runat="server">
                      <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                      <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>                  
                   <asp:ListItem Text="<" Value="<"></asp:ListItem>  
                </asp:DropDownList>
                <asp:TextBox ID="txtGoodNum" runat="server" Width="100px"></asp:TextBox>
                ��λ��
    <asp:DropDownList ID="ddlGoodUnit" runat="server">
    </asp:DropDownList>
                �ɱ����ۣ�
                   <asp:DropDownList ID="ddlFuHao1" runat="server">
                      <asp:ListItem Text="=" Value="="></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                      <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>                  
                   <asp:ListItem Text="<" Value="<"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtChenBen" runat="server" Width="100px"></asp:TextBox>
                ��ע: <asp:TextBox ID="txtRemark" runat="server" Width="100px"></asp:TextBox>
                 <asp:Button ID="Button1" runat="server" Text=" �� ѯ " 
        BackColor="Yellow" onclick="Button1_Click"  />
     <table width="100%" border="0">
        <tr>
            <td>
    <cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel ID="TabPanel1" runat="server">
            <HeaderTemplate>
                ����</HeaderTemplate>
            <ContentTemplate>
                <%--<asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal">--%>
                
                 <table width="100%" border="0">
        <tr>
            <td>
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    ShowFooter="true" OnPageIndexChanging="gvList_PageIndexChanging" AllowPaging="true"
                    PageSize="20">
                    <PagerTemplate>
                        <br />
                        <%--<asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
                        <asp:LinkButton ID="lbnFirst" runat="Server" Text="��ҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="First"></asp:LinkButton>
                        <asp:LinkButton ID="lbnPrev" runat="server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                        <asp:LinkButton ID="lbnNext" runat="Server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                        <asp:LinkButton ID="lbnLast" runat="Server" Text="βҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                        <br />--%>
                    </PagerTemplate>
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    С��
                                </td>
                                <td>
                                    ���
                                </td>
                                <td>
                                    ��λ
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    �ɱ�����
                                </td>
                                <td>
                                    �����
                                </td>
                                <td>
                                    ��������
                                </td>
                                <td>
                                    ����%
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">
                                    ---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="Type" HeaderText="" SortExpression="Type" />
                        <asp:BoundField DataField="MyProNo" HeaderText="���ݺ�" SortExpression="MyProNo" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="PODate" HeaderText="����" SortExpression="PODate" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="80" />
                        <asp:BoundField DataField="States" SortExpression="States" HeaderText="״̬" ItemStyle-Width="2%" />
                        <asp:BoundField DataField="GoodNo" HeaderText="����" SortExpression="GoodNo" />
                        <asp:TemplateField HeaderText="����" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" HeaderStyle-Width="8%" />
                        <%--  <asp:BoundField DataField="Good_Model" HeaderText="�ͺ�" SortExpression="Good_Model" />--%>
                        <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" ItemStyle-Width="2%" />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ɱ�����" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblCostPrice" runat="server" Text='<%# ConvertToObj(Eval("CostPrice")) %>'></asp:Label>
                            </ItemTemplate>
                          <%--  <FooterTemplate>
                                <asp:Label ID="lblCostPrice" runat="server" Text='<%# ConvertToObj(Eval("CostPrice")) %>'></asp:Label>
                            </FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ɱ��ܼ�" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# NumHelp.FormatTwo(Eval("CostTotal")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCostTotal" runat="server" Text='<%# NumHelp.FormatFour(Eval("CostTotal")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���۵���" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# ConvertToObj(Eval("SellPrice")) %>'></asp:Label>
                            </ItemTemplate>
                            <%--<FooterTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# ConvertToObj(Eval("SellPrice")) %>'></asp:Label>
                            </FooterTemplate>--%>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="�����ܼ�" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# NumHelp.FormatFour(Eval("SellTotal")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# ConvertToObj(Eval("SellTotal")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�����">
                            <ItemTemplate>
                                <asp:Label ID="lblOtherCost" runat="server" Text='<%# ConvertToObj(Eval("OtherCost")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblOtherCost" runat="server" Text='<%# ConvertToObj(Eval("OtherCost")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���۾���">
                            <ItemTemplate>
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# NumHelp.FormatFour(Eval("YiLiTotal")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# NumHelp.FormatFour(Eval("YiLiTotal")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ToTime" HeaderText="��������" SortExpression="ToTime" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="5%" />
                        <asp:TemplateField HeaderText="����%">
                            <ItemTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj1(Eval("Profit")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblProfit" runat="server" Text='<%# ConvertToObj1(Eval("Profit")) %>'></asp:Label>
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
                 <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
                    PageSize="20" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
                    NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
                </webdiyer:AspNetPager>
                </td>
                </tr>
                </table>
                <%-- </asp:Panel>--%>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server">
            <HeaderTemplate>
                �ɹ�</HeaderTemplate>
            <ContentTemplate>
             <table width="100%" border="0">
        <tr>
            <td>
                <asp:GridView ID="gvCai" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" ShowFooter="true" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvCai_RowDataBound" Style="border-collapse: collapse;" OnPageIndexChanging="gvCai_PageIndexChanging"
                    AllowPaging="true" PageSize="20">
                    <PagerTemplate>
                        <br />
                        <%--<asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
                        <asp:LinkButton ID="lbnFirst" runat="Server" Text="��ҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="First"></asp:LinkButton>
                        <asp:LinkButton ID="lbnPrev" runat="server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                        <asp:LinkButton ID="lbnNext" runat="Server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                        <asp:LinkButton ID="lbnLast" runat="Server" Text="βҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                        <br />--%>
                    </PagerTemplate>
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    ����
                                </td>
                                <td>
                                    С��
                                </td>
                                <td>
                                    ���
                                </td>
                                <td>
                                    �ͺ�
                                </td>
                                <td>
                                    ��λ
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ��Ӧ��1
                                </td>
                                <td>
                                    ѯ��1
                                </td>
                                <td>
                                    С��1
                                </td>
                                <td>
                                    ��Ӧ��2
                                </td>
                                <td>
                                    ѯ��2
                                </td>
                                <td>
                                    С��2
                                </td>
                                <td>
                                    ��Ӧ��3
                                </td>
                                <td>
                                    ѯ��3
                                </td>
                                <td>
                                    С��3
                                </td>
                                <td>
                                    ������
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">
                                    ---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="Type" HeaderText="" SortExpression="Type" />
                        <asp:BoundField DataField="States" SortExpression="States" HeaderText="״̬" />
                        <asp:TemplateField HeaderText="GoodId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodId" runat="server" Text='<%# Eval("GoodId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodNo" HeaderText="����" SortExpression="GoodNo" />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" HeaderStyle-Width="8%" />
                        <%--<asp:BoundField DataField="Good_Model" HeaderText="�ͺ�" SortExpression="Good_Model" />--%>
                        <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Supplier" HeaderText="��Ӧ��1" SortExpression="Supplier"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="ѯ��1">
                            <ItemTemplate>
                                <asp:Label ID="lblSupperPrice" runat="server" Text='<%# Eval("SupperPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="С��1">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# NumHelp.FormatFour(Eval("Total1")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# NumHelp.FormatFour(Eval("Total1")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Supplier1" HeaderText="��Ӧ��2" SortExpression="Supplier1"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="ѯ��2">
                            <ItemTemplate>
                                <asp:Label ID="lblSupperPrice1" runat="server" Text='<%# Eval("SupperPrice1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="С��2">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal2" runat="server" Text='<%# NumHelp.FormatFour(Eval("Total2")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal2" runat="server" Text='<%#  NumHelp.FormatFour(Eval("Total2")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Supplier2" HeaderText="��Ӧ��3" SortExpression="Supplier2"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="ѯ��3">
                            <ItemTemplate>
                                <asp:Label ID="lblSupperPrice2" runat="server" Text='<%# Eval("SupperPrice2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="С��3">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal3" runat="server" Text='<%#  NumHelp.FormatFour(Eval("Total3")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal3" runat="server" Text='<%#  NumHelp.FormatFour(Eval("Total3")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Idea" HeaderText="�������" SortExpression="Idea" ItemStyle-HorizontalAlign="Center"
                            Visible="false" />
                        <asp:BoundField DataField="UpdateUser" HeaderText="������" SortExpression="UpdateUser"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                       <%-- <asp:TemplateField HeaderText="����%">
                            <ItemTemplate>
                                <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>--%>
                         <asp:TemplateField HeaderText="���۵���" >
                            <ItemTemplate>
                                <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="��������" FooterStyle-BackColor="Black" FooterStyle-ForeColor="White">
                            <ItemTemplate>
                                <asp:Label ID="lblIniProfit" runat="server" Text='<%# Eval("IniProfit") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblIniProfit" runat="server" Text='<%# Eval("IniProfit") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="����%" FooterStyle-BackColor="Black" ItemStyle-BackColor="GreenYellow" FooterStyle-ForeColor="White">
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
                 <webdiyer:AspNetPager ID="AspNetPager2" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
                    PageSize="20" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
                    NextPageText="��ҳ" OnPageChanged="AspNetPager2_PageChanged">
                </webdiyer:AspNetPager>
                 </td>
                </tr>
                </table>

            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="TabPanel33">
            <HeaderTemplate>
                ��ע</HeaderTemplate>
            <ContentTemplate>
             <table width="100%" border="0">
        <tr>
            <td>
                <asp:GridView ID="gvAllPoRemark" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvAllPoRemark_RowDataBound"
                    Style="border-collapse: collapse;" OnPageIndexChanging="gvAllPoRemark_PageIndexChanging"
                    AllowPaging="true" PageSize="20">
                    <PagerTemplate>
                        <br />
                       <%-- <asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
                        <asp:LinkButton ID="lbnFirst" runat="Server" Text="��ҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="First"></asp:LinkButton>
                        <asp:LinkButton ID="lbnPrev" runat="server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                        <asp:LinkButton ID="lbnNext" runat="Server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                        <asp:LinkButton ID="lbnLast" runat="Server" Text="βҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                        <br />--%>
                    </PagerTemplate>
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    ���ݺ�
                                </td>
                                <td>
                                    ��ע
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">
                                    ---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="MyProNo" HeaderText="���ݺ�" SortExpression="MyProNo" />
                        <asp:BoundField DataField="PORemark" HeaderText="��ע" SortExpression="PORemark" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                    <FooterStyle BackColor="#D7E8FF" />
                </asp:GridView>
                 <webdiyer:AspNetPager ID="AspNetPager3" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
                    PageSize="20" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
                    NextPageText="��ҳ" OnPageChanged="AspNetPager3_PageChanged">
                </webdiyer:AspNetPager>
                 </td>
                </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    </td>
    </tr>
 <tr>
            <td>
    <cc1:TabContainer ID="TabContainer2" runat="server">
        <cc1:TabPanel ID="TabPanel3" runat="server">
            <HeaderTemplate>
                �����˻�</HeaderTemplate>
            <ContentTemplate>
             <table width="100%" border="0">
        <tr>
            <td>
                <asp:GridView ID="gvTui" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" ShowFooter="true"
                    OnRowDataBound="gvTui_RowDataBound" OnPageIndexChanging="gvTui_PageIndexChanging"
                    AllowPaging="true" PageSize="20">
                    <PagerTemplate>
                        <br />
                      <%--  <asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
                        <asp:LinkButton ID="lbnFirst" runat="Server" Text="��ҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="First"></asp:LinkButton>
                        <asp:LinkButton ID="lbnPrev" runat="server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                        <asp:LinkButton ID="lbnNext" runat="Server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                        <asp:LinkButton ID="lbnLast" runat="Server" Text="βҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                        <br />--%>
                    </PagerTemplate>
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    �ֿ�
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    С��
                                </td>
                                <td>
                                    ���
                                </td>
                                <td>
                                    �ͺ�
                                </td>
                                <td>
                                    ��λ
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    �ɱ�����
                                </td>
                                <td>
                                    �ɱ����
                                </td>
                                <td>
                                    �ɱ�ȷ�ϼ�
                                </td>
                                <td>
                                    ��ʧ���
                                </td>
                                <td>
                                    ���۵���
                                </td>
                                <td>
                                    ���۽��
                                </td>
                                <td>
                                    ��ע
                                </td>
                                <td>
                                    ״̬
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">
                                    ---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="HouseName" HeaderText="�ֿ�" SortExpression="HouseName" />
                        <asp:BoundField DataField="GoodNo" HeaderText="����" SortExpression="GoodNo" />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" HeaderStyle-Width="8%" />
                    
                        <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" />
                            <asp:BoundField DataField="RuTime" HeaderText="��������" SortExpression="RuTime"  DataFormatString="{0:yyyy-MM-dd}"  />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ɱ�����">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ɱ��ܼ�">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%#  NumHelp.FormatFour(Eval("Total")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%#  NumHelp.FormatFour(Eval("Total")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ɱ�ȷ�ϼ�">
                            <ItemTemplate>
                                <asp:Label ID="txtGoodPriceSecond" runat="server" Text='<%# Eval("GoodPriceSecond") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodTotalCha" runat="server" Text='<%#  NumHelp.FormatFour(Eval("GoodTotalCha")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodTotalCha" runat="server" Text='<%#  NumHelp.FormatFour(Eval("GoodTotalCha")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���۵���">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCheckPrice1" runat="server" Text='<%# Eval("GoodSellPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�����ܼ�">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%#  NumHelp.FormatFour(Eval("GoodSellPriceTotal")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%#  NumHelp.FormatFour(Eval("GoodSellPriceTotal")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="��ע">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodRemark" runat="server" Text='<%# Eval("GoodRemark") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Status" HeaderText="״̬" SortExpression="Status" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                    <FooterStyle BackColor="#D7E8FF" />
                </asp:GridView>
                 <webdiyer:AspNetPager ID="AspNetPager4" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
                    PageSize="20" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
                    NextPageText="��ҳ" OnPageChanged="AspNetPager4_PageChanged">
                </webdiyer:AspNetPager>
                 </td>
                </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" runat="server">
            <HeaderTemplate>
                �ɹ��˻�</HeaderTemplate>
            <ContentTemplate>
             <table width="100%" border="0">
        <tr>
            <td>
                <asp:GridView ID="gvCaiOut" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvCaiOut_RowDataBound"
                    ShowFooter="true" OnPageIndexChanging="gvCaiOut_PageIndexChanging" AllowPaging="true"
                    PageSize="20">
                    <PagerTemplate>
                        <br />
                        <%--<asp:Label ID="lblPage" runat="server" Text='<%# "��" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "ҳ/��" + (((GridView)Container.NamingContainer).PageCount) + "ҳ" %> '></asp:Label>
                        <asp:LinkButton ID="lbnFirst" runat="Server" Text="��ҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="First"></asp:LinkButton>
                        <asp:LinkButton ID="lbnPrev" runat="server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                            CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                        <asp:LinkButton ID="lbnNext" runat="Server" Text="��һҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                        <asp:LinkButton ID="lbnLast" runat="Server" Text="βҳ" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                            CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                        <br />--%>
                    </PagerTemplate>
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    С��
                                </td>
                                <td>
                                    ���
                                </td>
                                <td>
                                    ��λ
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ���
                                </td>
                                <td>
                                    ��ע
                                </td>
                                <td>
                                    ״̬
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">
                                    ---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="GoodNo" HeaderText="����" SortExpression="GoodNo" />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblGoodName" runat="server" Text='<%# Eval("GoodName") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName" />
                        <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" HeaderStyle-Width="8%" />
                        <%--            <asp:BoundField DataField="Good_Model" HeaderText="�ͺ�" SortExpression="Good_Model" />--%>
                        <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" />
                          <asp:BoundField DataField="RuTime" HeaderText="��������" SortExpression="RuTime"  DataFormatString="{0:yyyy-MM-dd}"  />
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lbVislNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblVisNum" runat="server" Text='<%# Eval("GoodNum") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblCheckPrice" runat="server" Text='<%# Eval("GoodPrice") %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�ܼ�">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%#  NumHelp.FormatFour(Eval("Total")) %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%#  NumHelp.FormatFour(Eval("Total")) %>'></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodRemark" HeaderText="��ע" SortExpression="GoodRemark" />
                        <asp:BoundField DataField="QingGouPer" HeaderText="�빺��" SortExpression="QingGouPer" />
                        <asp:BoundField DataField="Status" HeaderText="״̬" SortExpression="Status" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                    <FooterStyle BackColor="#D7E8FF" />
                </asp:GridView>
                 <webdiyer:AspNetPager ID="AspNetPager5" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
                    PageSize="20" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
                    NextPageText="��ҳ" OnPageChanged="AspNetPager5_PageChanged">
                </webdiyer:AspNetPager>
                 </td>
                </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
     </td>
    </tr>
    </table>
    �ܽ�
    <asp:Label ID="lblTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
    <br />
    ������Ŀ���:<asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
</asp:Content>
