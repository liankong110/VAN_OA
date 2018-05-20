<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFUpdatePOName.aspx.cs" Inherits="VAN_OA.JXC.WFUpdatePOName"
    Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master" Title="��Ŀ���Ƹ���" %>
<%@ Import Namespace="VAN_OA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                ��Ŀ���Ƹ���
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
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 ����Ŀ����:<asp:TextBox ID="txtNewPOName" runat="server" ForeColor="Red"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="���� "  
                    OnClientClick="return confirm('ȷ��Ҫ�ύ��')"  BackColor="Yellow" 
                    onclick="Button1_Click"  />
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
                ����״̬:
            </td>
            <td>
                <asp:DropDownList ID="ddlStatue" runat="server" Width="100px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>ִ����</asp:ListItem>
                    <asp:ListItem>ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
                ��Ŀ����:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="70px">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">������</asp:ListItem>
                    <asp:ListItem Value="1">����</asp:ListItem>
                </asp:DropDownList>
                ��Ŀ�رգ�
                <asp:DropDownList ID="ddlClose" runat="server" Width="70px">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">δ�ر�</asp:ListItem>
                    <asp:ListItem Value="1">�ر�</asp:ListItem>
                </asp:DropDownList>
                 ��Ŀѡ�У�
                <asp:DropDownList ID="ddlIsSelect" runat="server" Width="70px">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">δѡ��</asp:ListItem>
                    <asp:ListItem Value="1">ѡ��</asp:ListItem>
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
                ��Ŀ״̬
            </td>
            <td>
                <%--<asp:DropDownList ID="ddlPOStatue" runat="server" Width="200px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>��ʼ״̬</asp:ListItem>
                 <asp:ListItem>�ѽ���</asp:ListItem>
                  <asp:ListItem>�ѿ�Ʊ</asp:ListItem>
                   <asp:ListItem>�ѽ���</asp:ListItem>
                </asp:DropDownList>--%>
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
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>��Ʒ����: <asp:TextBox ID="txtGoodNo" runat="server" Width="100px"></asp:TextBox>����/С��/���: <asp:TextBox ID="txtNameOrTypeOrSpec" runat="server" Width="100PX"></asp:TextBox>
                ����
                <asp:TextBox ID="txtNameOrTypeOrSpecTwo" runat="server" Width="100PX"></asp:TextBox>
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
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="select" CommandArgument='<% #Eval("Id") %>'>�鿴</asp:LinkButton>
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
                        <a href="/JXC/Sell_OrderPFBackList.aspx?Type=<%# GetStateType(Eval("POStatue6")) %>&PONo=<%# Eval("PONo") %>">
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
</asp:Content>
