<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CAI_OrderList.aspx.cs"
    Inherits="VAN_OA.JXC.CAI_OrderList" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="�ɹ������б�" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                �ɹ������б�
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ���:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>��Ŀģ��:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
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
                �ɹ���״̬:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatue" runat="server" Width="160px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>ִ����</asp:ListItem>
                    <asp:ListItem>ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                �ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>
                ��Ŀ���ݺ�:
            </td>
            <td>
                <asp:TextBox ID="txtPoProNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �빺��:
            </td>
            <td>
                <asp:TextBox ID="txtCaiugou" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>
                ҵ������:
            </td>
            <td>
                <asp:DropDownList ID="ddlBusType" runat="server" Width="160px">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="0">��Ŀ�����ɹ�</asp:ListItem>
                    <asp:ListItem Value="1">���ɹ�</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                AE��
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>
                �ɹ����ţ�
            </td>
            <td>
                <asp:TextBox ID="txtCaiGouNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            <div style="float:left; display:inline" >
                ��˰:
           
                <asp:DropDownList ID="ddlIsHanShui" runat="server" Width="50px">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="��˰"></asp:ListItem>
                    <asp:ListItem Value="0" Text="����˰"></asp:ListItem>
                </asp:DropDownList>
               ��������:<asp:TextBox ID="txtAuditDate" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                  <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtAuditDate">
                </cc1:CalendarExtender>
                </div>
                <div style="float:right; display:inline" >
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound"
        OnRowCommand="gvMain_RowCommand">
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
                        �ɹ���
                    </td>
                    <td>
                        �빺��
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
                        ���㷽ʽ
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
                        ҵ������
                    </td>
                    <td>
                        ���ݺ�
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
                    <asp:LinkButton ID="lbtnReEdit" runat="server" CommandName="ReEdit" CommandArgument='<% #Eval("Id") %>'
                        OnClientClick='return confirm( "ȷ��Ҫ�����ύ�˵�����") '>�༭</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("Id") %>'>�鿴</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCopy" runat="server" CommandName="Copy" CommandArgument='<% #Eval("Id") %>'
                        OnClientClick='return confirm( "ȷ��Ҫ�ύ�˵�����") '>����</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
              <asp:BoundField DataField="LastTime" HeaderText="����ʱ��" SortExpression="LastTime" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ProNo" HeaderText="�ɹ����ݺ�" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="LoginName" HeaderText="�ɹ���" SortExpression="LoginName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CaiGou" HeaderText="�빺��" SortExpression="CaiGou" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PONo" HeaderText="��Ŀ����" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POName" HeaderText="��Ŀ����" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PODate" HeaderText="��Ŀ����" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="POTotal" HeaderText="��Ŀ���" SortExpression="POTotal" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POPayStype" HeaderText="���㷽ʽ" SortExpression="POPayStype"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestNo" HeaderText="�ͻ�ID" SortExpression="GuestNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="INSIDE" HeaderText="INSIDE" SortExpression="INSIDE" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BusType" HeaderText="ҵ������" SortExpression="BusType" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CG_ProNo" HeaderText="��Ŀ���ݺ�" SortExpression="CG_ProNo"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Status" HeaderText="״̬" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <table width="100%" border="0">
        <tr>
            <td>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
                    TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
                    CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
                    CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
                    PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
                    NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
                </webdiyer:AspNetPager>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:TabContainer ID="TabContainer1" runat="server">
                    <cc1:TabPanel ID="TabPanel1" runat="server">
                        <HeaderTemplate>
                            ����</HeaderTemplate>
                        <ContentTemplate>
                            <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                                DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                                ShowFooter="true">
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
                                    <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" />
                                    <%--   <asp:BoundField DataField="Good_Model" HeaderText="�ͺ�" SortExpression="Good_Model" />--%>
                                    <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" />
                                    <asp:TemplateField HeaderText="����">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNum" runat="server" Text='<%# Eval("Num") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�ɱ�����">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblCostPrice" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�ɱ��ܼ�">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostTotal") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblCostTotal" runat="server" Text='<%# Eval("CostPrice") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="���۵���">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSellPrice" runat="server" Text='<%# Eval("SellPrice") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�����ܼ�">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblSellTotal" runat="server" Text='<%# Eval("SellTotal") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�����">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblOtherCost" runat="server" Text='<%# Eval("OtherCost") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="���۾���">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblYiLiTotal" runat="server" Text='<%# Eval("YiLiTotal") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ToTime" HeaderText="��������" SortExpression="ToTime" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:TemplateField HeaderText="����%">
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
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server">
                        <HeaderTemplate>
                            �ɹ�</HeaderTemplate>
                        <ContentTemplate>
                            <asp:GridView ID="gvCai" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                                DataKeyNames="Ids" ShowFooter="true" Width="100%" AutoGenerateColumns="False"
                                OnRowDataBound="gvCai_RowDataBound" Style="border-collapse: collapse;">
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
                                               ʵ�ɼ�1/ѯ��1
                                            </td>
                                            <td>
                                                С��1
                                            </td>
                                            <td>
                                                ��Ӧ��2
                                            </td>
                                            <td>
                                               ʵ�ɼ�2/ѯ��2
                                            </td>
                                            <td>
                                                С��2
                                            </td>
                                            <td>
                                                ��Ӧ��3
                                            </td>
                                            <td>
                                                ʵ�ɼ�3/ѯ��3
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
                                    <asp:TemplateField HeaderText="GoodId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGoodId" runat="server" Text='<%# Eval("GoodId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="��˰">
                            <ItemTemplate>
                                <asp:CheckBox ID="CBIsHanShui" runat="server" Checked='<%# Eval("IsHanShui") %>'
                                    Enabled="false" />
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
                                    <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" />
                                    <asp:BoundField DataField="Good_Model" HeaderText="�ͺ�" SortExpression="Good_Model" />
                                    <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" />
                                    <asp:TemplateField HeaderText="����">
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
                                    <asp:BoundField DataField="Supplier" HeaderText="��Ӧ��1" SortExpression="Supplier"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="ʵ�ɼ�1/ѯ��1">
                                        <ItemTemplate>
                                           <asp:Label ID="lblTruePrice1" runat="server" Text='<%# Eval("TruePrice1") %>'></asp:Label>/
                                            <asp:Label ID="lblSupperPrice" runat="server" Text='<%# Eval("SupperPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="С��1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("Total1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("Total1") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox12" runat="server" Checked='<%# Eval("cbifDefault2") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Supplier1" HeaderText="��Ӧ��2" SortExpression="Supplier1"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="ʵ�ɼ�2/ѯ��2">
                                        <ItemTemplate>
                                          <asp:Label ID="lblTruePrice2" runat="server" Text='<%# Eval("TruePrice2") %>'></asp:Label>/
                                            <asp:Label ID="lblSupperPrice1" runat="server" Text='<%# Eval("SupperPrice1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="С��2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal2" runat="server" Text='<%# Eval("Total2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal2" runat="server" Text='<%# Eval("Total2") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox13" runat="server" Checked='<%# Eval("cbifDefault3") %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Supplier2" HeaderText="��Ӧ��3" SortExpression="Supplier2"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="ʵ�ɼ�3/ѯ��3">
                                        <ItemTemplate>
                                          <asp:Label ID="lblTruePrice3" runat="server" Text='<%# Eval("TruePrice3") %>'></asp:Label>/
                                            <asp:Label ID="lblSupperPrice2" runat="server" Text='<%# Eval("SupperPrice2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="С��3">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal3" runat="server" Text='<%# Eval("Total3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal3" runat="server" Text='<%# Eval("Total3") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Idea" HeaderText="�������" SortExpression="Idea" ItemStyle-HorizontalAlign="Center"
                                        Visible="false" />
                                    <asp:BoundField DataField="UpdateUser" HeaderText="������" SortExpression="UpdateUser"
                                        ItemStyle-HorizontalAlign="Center" Visible="false" />
                                    <asp:TemplateField HeaderText="����%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCaiLiRun" runat="server" Text='<%# Eval("CaiLiRun") %>'></asp:Label>
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
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
