<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPONoIsSelected.aspx.cs"
    Inherits="VAN_OA.JXC.SetPONoIsSelected" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="�Զ�ѡ��" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">�Զ�ѡ��
            </td>
        </tr>
        <tr>
            <td>��Ŀ����:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" BackColor="#7FFF00"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server" BackColor="#7FFF00"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>��˾���ƣ�
            </td>
            <td>
                <asp:DropDownList ID="ddlCompany" BackColor="#7FFF00" runat="server" DataTextField="ComName" DataValueField="ComCode"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>��Ŀ��ţ�
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>
                ��Ŀ���ƣ�
                <asp:TextBox ID="ttxPOName" runat="server"></asp:TextBox>
                ��Ŀ�رգ�
                <asp:DropDownList ID="ddlClose" runat="server" Width="50px">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">δ�ر�</asp:ListItem>
                    <asp:ListItem Value="1">�ر�</asp:ListItem>
                </asp:DropDownList>
                ��Ŀѡ�У�
                <asp:DropDownList ID="ddlIsSelect" runat="server" Width="50px">
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
                ��˰��
                <asp:DropDownList ID="ddlHanShui" runat="server" Width="50px">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="��˰"></asp:ListItem>
                    <asp:ListItem Value="0" Text="����˰"></asp:ListItem>
                </asp:DropDownList>
                �ͻ����ƣ�
                <asp:TextBox ID="txtGuestName" runat="server"></asp:TextBox>
                AE��
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <div align="left" style="display: inline;">
                    ��Ŀ����:
                <asp:DropDownList ID="ddlIsSpecial" runat="server">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">������</asp:ListItem>
                    <asp:ListItem Value="1">����</asp:ListItem>
                </asp:DropDownList>
                    ��Ŀ���<asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id" BackColor="#7FFF00">
                    </asp:DropDownList>
                    �ͻ�����:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                        DataTextField="GuestType" Width="50px">
                    </asp:DropDownList>
                    �ͻ�����:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro" BackColor="#7FFF00"
                        DataTextField="GuestProString" Width="50px">
                    </asp:DropDownList>
                    ������
                    <asp:DropDownList ID="ddlFuHao" runat="server" BackColor="#7FFF00">
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBai" runat="server" Width="21px" Text="90" BackColor="#7FFF00"></asp:TextBox>%����Ŀ������Ŀ

                   <br />
                    <asp:TextBox ID="txtLeftJingLi" runat="server" Width="100"></asp:TextBox>
                  
                    <asp:DropDownList ID="ddlLeftJingLi" runat="server" >
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    </asp:DropDownList>
                    ��Ŀ����
                     <asp:DropDownList ID="ddlRightJingLi" runat="server" >
                         <asp:ListItem Text=">" Value=">"></asp:ListItem>
                         <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     </asp:DropDownList>
                    <asp:TextBox ID="txtRightJingLi" runat="server" Width="100"></asp:TextBox>


                    <asp:TextBox ID="txtLeftPoTotal" runat="server" Width="100"></asp:TextBox>
                    
                    <asp:DropDownList ID="ddlLeftPoTotal" runat="server" >
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    </asp:DropDownList>
                    ��Ŀ���
                       <asp:DropDownList ID="ddlRightPoTotal" runat="server" >
                           <asp:ListItem Text=">" Value=">"></asp:ListItem>
                           <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                       </asp:DropDownList>
                    <asp:TextBox ID="txtRightPoTotal" runat="server" Width="100"></asp:TextBox>
                </div>
                <div align="right" style="display: inline; float: right;">
                    <asp:Button ID="btnQuery" runat="server" Text=" ��ѯ " BackColor="Yellow" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSelect" runat="server" Text=" �Զ�ѡ�� " OnClientClick="return confirm('ȷ��Ҫ�ύ��')"
                        BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text=" ȡ��ѡ�� " OnClientClick="return confirm('ȷ��Ҫ�ύ��')"
                        BackColor="Yellow" OnClick="btnCancel_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" �� �� " BackColor="Yellow" OnClick="Button1_Click" />
                </div>
            </td>
        </tr>
    </table>
    ע����Ŀ���=0����Ŀ����=0 ����Ŀ�������������Զ�ѡ�а�ť�����
      <br />
    1��  �Զ�ѡ�к� ȡ���Զ�ѡ�� ������1����Ŀ����+��˾����+ ��Ŀ���+�ͻ�����+�ͻ����ԣ����ⶩ����������ѡ�к���Ŀѡ��  
        <br />
    2��  �Զ�ѡ�к� ȡ���Զ�ѡ�� ������2��a.���ǵ����� �����򡰴�С�� �ٷֱȡ���Ŀ������Ŀ������Ŀ���� �е�ѡ�����Դ��Ϲ���  b.������Ŀ�����������Ŀ����Ŀ�����еĽ���ѡ�е����Ծ����Ϲ���c.������Ŀ���=0����Ŀ������Ŀ���� �е�ѡ�����Դ��Ϲ���d. ������Ŀ�ܳɱ�>=��Ŀ��� ����Ŀ������Ŀ���� �е�ѡ�����Դ��Ϲ�
    <br />
    3.�Զ�ѡ�к�ȡ���Զ�ѡ�е������������ �� �����򲿷ֱ���Ϊ����ɫ
    <asp:GridView ID="gvMain" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="Id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvMain_PageIndexChanging" OnRowDataBound="gvMain_RowDataBound"
        OnRowCommand="gvMain_RowCommand">
        <PagerTemplate>
        </PagerTemplate>
        <EmptyDataTemplate>
            <table width="100%">
                <tr style="height: 20px; background-color: #336699; color: White;">
                    <td>���ݺ�
                    </td>
                    <td>��Ŀ����
                    </td>
                    <td>��Ŀ����
                    </td>
                    <td>�ͻ�����
                    </td>
                    <td>��Ŀ����
                    </td>
                    <td>AE
                    </td>
                    <td>����
                    </td>
                    <td>��˰
                    </td>
                    <td>��Ʊ����
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="height: 80%">---��������---
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
            <asp:BoundField DataField="ProNo" HeaderText="���ݺ�" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="��Ŀ����">
                <ItemTemplate>
                    <asp:Label ID="PONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="POName" HeaderText="��Ŀ����" SortExpression="POName" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="POTypeString" HeaderText="����" SortExpression="POTypeString" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PODate" HeaderText="��Ŀ����" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="�ر�">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsClose" runat="server" Checked='<% #Eval("IsClose") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����ѡ�� ">
                <ItemTemplate>
                    <asp:CheckBox ID="cbJieIsSelected" runat="server" Checked='<% #Eval("JieIsSelected") %>'
                        Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ѡ��">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSelected" runat="server" Checked='<% #Eval("IsSelected") %>'
                        Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSpecial" runat="server" Checked='<% #Eval("IsSpecial") %>'
                        Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��˰">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsPoFax" runat="server" Checked='<% #Eval("IsPoFax") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��Ʊ����">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidtxt" Value='<%#Eval("FpType")%>' />
                    <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="FpType" DataTextField="FpType" Width="90px"
                        Enabled="false">
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="maoliTotal" HeaderText="��Ŀ����" SortExpression="maoliTotal"
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="InvoiceTotal" HeaderText="������" SortExpression="InvoiceTotal" DataFormatString="{0:n2}" />
            <asp:BoundField DataField="POTotal" HeaderText="��Ŀ���" SortExpression="POTotal" DataFormatString="{0:n2}" />
            <asp:BoundField DataField="GoodTotal" HeaderText="�ܳɱ�" SortExpression="GoodTotal"
                DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="BILI" HeaderText="������" SortExpression="BILI" DataFormatString="{0:n2}" />
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
        <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
            HorizontalAlign="Center" />
        <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
        <RowStyle CssClass="InfoDetail1" />
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
        TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
        PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
        NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>
</asp:Content>
