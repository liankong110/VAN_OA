<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sell_OrderPFList.aspx.cs"
    Inherits="VAN_OA.JXC.Sell_OrderPFList" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="�ɹ������б�" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">���۷�Ʊ�б�
            </td>
        </tr>
        <tr>
            <td>��Ŀ���:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                ��Ŀģ��:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
            </td>
            <td>��Ŀ����:
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
            <td>����:
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
            <td>��Ʊ״̬:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatue" runat="server" Width="160px">
                    <asp:ListItem></asp:ListItem>
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
            </td>
        </tr>
        <tr>
            <td>���ݺ�:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server" Width="350px"></asp:TextBox>
            </td>
            <td>�ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">��Ʊ��:
            
                <asp:TextBox ID="txtFPNo" runat="server" Width="100px"></asp:TextBox>
                ��Ʊ���:
                 <asp:DropDownList ID="ddlPOFaTotal" runat="server">
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                     <asp:ListItem Text=">" Value=">"></asp:ListItem>
                     <asp:ListItem Text="=" Value="="></asp:ListItem>
                     <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                 </asp:DropDownList>
                <asp:TextBox ID="txtFPTotal" runat="server" Width="100px"></asp:TextBox>
                ԭ��Ʊ����:
                <asp:TextBox ID="txtOldFPNo" runat="server" Width="100px"></asp:TextBox>
                ԭ���:
                 <asp:DropDownList ID="ddlOldPOFaTotal" runat="server">
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                     <asp:ListItem Text=">" Value=">"></asp:ListItem>
                     <asp:ListItem Text="=" Value="="></asp:ListItem>
                     <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                 </asp:DropDownList>
                <asp:TextBox ID="txtOldFPTotal" runat="server"  Width="100px"></asp:TextBox>


                ��Ŀ & ��Ʊ����
            
                <asp:DropDownList ID="ddlPOInvoiceState" runat="server">
                    <asp:ListItem Value="0">ȫ��</asp:ListItem>
                    <asp:ListItem Value="1">��Ʊ�� < ��Ŀ�� </asp:ListItem>
                    <asp:ListItem Value="2">��Ʊ�� > ��Ŀ��</asp:ListItem>
                    <asp:ListItem Value="3">��Ʊ�� = ��Ŀ��</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                 ��Ʊ���ͣ�<asp:DropDownList ID="ddlFPType" runat="server" DataValueField="Id" DataTextField="FpType"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            
         
            <td colspan="4">
                AE��
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
                ��Ŀ�رգ�
                <asp:DropDownList ID="ddlIsClose" runat="server">
                    <asp:ListItem Text="ȫ��" Value="-1"> </asp:ListItem>
                    <asp:ListItem Text="�ر�" Value="1"> </asp:ListItem>
                    <asp:ListItem Text="δ�ر�" Value="0"> </asp:ListItem>
                </asp:DropDownList>
                ��Ŀѡ�У�
                <asp:DropDownList ID="ddlIsSelect" runat="server">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">δѡ��</asp:ListItem>
                    <asp:ListItem Value="1">ѡ��</asp:ListItem>
                </asp:DropDownList>
                ����ѡ�У�
                   <asp:DropDownList ID="ddlJieIsSelected" runat="server">
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
                  <asp:DropDownList ID="ddlType" runat="server" Width="160px" Visible="false">
                        <asp:ListItem Text="" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="�յ�" Value="0"></asp:ListItem>
                        <asp:ListItem Text="δ�յ�" Value="1"></asp:ListItem>
                    </asp:DropDownList>
           
                <div align="right">
                  
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />
                    <asp:Button ID="Button1" runat="server" Text="����EXCEL" BackColor="Yellow" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;
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
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>���ݺ�
                    </td>
                    <td>����
                    </td>
                    <td>��Ŀ����
                    </td>
                    <td>��Ŀ����
                    </td>
                    <td>�ͻ�����
                    </td>
                    <td>������
                    </td>
                    <td>��Ʊ����
                    </td>
                    <td>��Ʊ��
                    </td>
                    <td>�Ƶ���
                    </td>
                    <td>״̬
                    </td>
                    <td>��ע
                    </td>
                    <td>ԭ��Ʊ
                    </td>
                    <td>ԭ���
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">---��������---
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnReEdit" runat="server" CommandName="ReEdit" CommandArgument='<% #Eval("Id")%>'
                        OnClientClick='return confirm( "ȷ��Ҫ�����ύ�˵�����") '>�༭</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("Id")%>'>�鿴</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Del" CommandArgument='<% #Eval("Id")%>'
                        OnClientClick='return confirm( "ȷ��Ҫɾ����") '>ɾ��</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
            <asp:BoundField DataField="ProNo" HeaderText="���ݺ�" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="RuTime" HeaderText="����" SortExpression="RuTime" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="PONo" HeaderText="��Ŀ����" SortExpression="PONo" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="POName" HeaderText="��Ŀ����" SortExpression="POName" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="���">
                <ItemTemplate>
                    <asp:Label ID="Total" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��Ŀ���">
                <ItemTemplate>
                    <asp:Label ID="AllPoTotal" runat="server" Text='<%# Eval("AllPoTotal") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��Ʊ����">
                <ItemTemplate>
                    <asp:Label ID="FPNoStyle" runat="server" Text='<%# Eval("FPNoStyle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��Ʊ��">
                <ItemTemplate>
                    <asp:Label ID="FPNo" runat="server" Text='<%# Eval("FPNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DoPer" HeaderText="������" SortExpression="DoPer" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="�Ƶ���">
                <ItemTemplate>
                    <asp:Label ID="CreateName" runat="server" Text='<%# Eval("CreateName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="״̬">
                <ItemTemplate>
                    <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��ע">
                <ItemTemplate>
                    <asp:Label ID="Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ԭ��Ʊ">
                <ItemTemplate>
                    <asp:Label ID="TopFPNo" runat="server" Text='<%# Eval("TopFPNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ԭ���">
                <ItemTemplate>
                    <asp:Label ID="TopTotal" runat="server" Text='<%# Eval("TopTotal") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
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
            <td>��Ʊ�ܶ�:<asp:Label ID="lblAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
                ��Ŀ�ܶ�:<asp:Label ID="lblAllPoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>
                <br />
                <br />
                ��Ʊ�ܽ��:<asp:Label ID="lblTotal" runat="server" Text="0" ForeColor="Red"></asp:Label><asp:Label
                    ID="Label2" runat="server" Text="��Ŀ�ܽ�"></asp:Label><asp:Label ID="lblPOTotal" runat="server"
                        Text="0" ForeColor="Red"></asp:Label><br />
                <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
                    DataKeyNames="Ids" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound"
                    ShowFooter="true">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>���ⵥ��
                                </td>
                                <td>����
                                </td>
                                <td>����
                                </td>
                                <td>С��
                                </td>
                                <td>���
                                </td>
                                <td>�ͺ�
                                </td>
                                <td>��λ
                                </td>
                                <td>����
                                </td>
                                <td>�ɱ�����
                                </td>
                                <td>�ɱ����
                                </td>
                                <td>���۵���
                                </td>
                                <td>���۽��
                                </td>
                                <td>��ע
                                </td>
                            </tr>
                            <tr>
                                <td colspan="11" align="center" style="height: 80%">---��������---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <%-- <asp:BoundField DataField="SellOutPONO" HeaderText="���ⵥ��" SortExpression="SellOutPONO" />--%>
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
                        <%--  <asp:BoundField DataField="Good_Model" HeaderText="�ͺ�" SortExpression="Good_Model" />--%>
                        <asp:BoundField DataField="GoodUnit" HeaderText="��λ" SortExpression="GoodUnit" />
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
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
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
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal1" runat="server" Text='<%# Eval("GoodSellPriceTotal") %>'></asp:Label>
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
    </table>
</asp:Content>
