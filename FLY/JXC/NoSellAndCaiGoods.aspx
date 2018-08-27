<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoSellAndCaiGoods.aspx.cs"
    Inherits="VAN_OA.JXC.NoSellAndCaiGoods" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="�ɿ�����嵥" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
  
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="4" style="height: 20px; background-color: #336699; color: White;">�ɿ�����嵥
            </td>
        </tr>
        <tr>
            <td>��Ŀ���:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
                  ��Ŀ����:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px" >
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">������</asp:ListItem>
                    <asp:ListItem Value="1">����</asp:ListItem>
                </asp:DropDownList>
                  �ͻ�����:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType"  Width="50px">   </asp:DropDownList>
                ��Ŀģ��:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>

            </td>
            <td>�ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>��˾���ƣ�
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>AE:
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>��Ʒ����:
            </td>
            <td>
                <asp:TextBox ID="txtGoodNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>���ʱ��:
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
            <td>����ʱ��:
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
            <td>δ������ :
            </td>
            <td>
                <asp:DropDownList ID="ddlWeiType" runat="server" Width="200px">
                    <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="δ���߳��ⵥ" Value="0"></asp:ListItem>
                    <asp:ListItem Text="���ⵥִ����" Value="1"></asp:ListItem>
                    <asp:ListItem Text="�ѳ�������" Value="2"></asp:ListItem>
                    <asp:ListItem Text="���ⵥδͨ��" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>��Ŀ����:
            </td>
            <td>
                <asp:DropDownList ID="ddlPoType" runat="server" Width="200px">
                    <asp:ListItem Text="ȫ��" Value="2"></asp:ListItem>
                    <asp:ListItem Text="���ɹ�" Value="1"></asp:ListItem>
                    <asp:ListItem Text="������Ŀ" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:CheckBox ID="cbZero" runat="server" Text="�����ͬʱ��0" Checked="true" />
                <asp:CheckBox ID="cbRuZero" runat="server" Text="����������0" />
                <asp:CheckBox ID="cbCaiKu" runat="server" Text="�ɿ��������0" />
                <asp:CheckBox ID="cbCaiGou" runat="server" Text="�ɹ��������0" />
                ��˰:
                <asp:DropDownList ID="ddlIsHanShui" runat="server" Width="50px">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="��˰"></asp:ListItem>
                    <asp:ListItem Value="0" Text="����˰"></asp:ListItem>
                </asp:DropDownList>
                ��Ŀ���ƣ�
                <asp:TextBox ID="txtPOName" runat="server" Width="150px"></asp:TextBox>
                KCģʽ��
                 <asp:DropDownList ID="ddlKCType" runat="server">
                     <asp:ListItem Value="1" Text="����"></asp:ListItem>
                     <asp:ListItem Value="0" Text="����"></asp:ListItem>
                        <asp:ListItem Value="2" Text="��KC"></asp:ListItem>
                 </asp:DropDownList>
                ��Ӧ�̼�ƣ�
                <asp:TextBox ID="txtSupplier" runat="server" Width="150px"></asp:TextBox>
                <asp:CheckBox ID="cbPiPei" runat="server" Text="ȫƥ��" />
                 ֱ��:
                <asp:DropDownList ID="ddlZhifa" runat="server" Width="50px">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="ֱ��"></asp:ListItem>
                    <asp:ListItem Value="0" Text="��ֱ��"></asp:ListItem>
                </asp:DropDownList>
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="����EXCEL" BackColor="Yellow" OnClick="Button1_Click" />
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal" >
        <asp:GridView ID="gvMain" runat="server"  PagerSettings-Visible="false"
            Width="130%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvMain_PageIndexChanging"
            OnRowDataBound="gvMain_RowDataBound"  >
            <PagerTemplate>
                
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
                        <td>��Ŀ���
                        </td>
                        <td>AE
                        </td>
                        <td>�ͻ�����
                        </td>
                        <td>��Ʒ���
                        </td>
                        <td>����
                        </td>
                        <td>���
                        </td>
                        <td>��Ŀ���
                        </td>
                        <td>������
                        </td>
                        <td>�ѳ�����
                        </td>
                        <td>�������
                        </td>
                        <td>������
                        </td>
                        <td>���۵���
                        </td>
                        <td>���ʱ��
                        </td>
                        <td>����ʱ��
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" style="height: 80%">---��������---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="PONo" HeaderText="��Ŀ���" SortExpression="PONo" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="POName" HeaderText="��Ŀ����" SortExpression="POName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="��˰">
                    <ItemTemplate>
                        <asp:Label ID="lblIsHanShui" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GoodAreaNumber" HeaderText="��λ" SortExpression="GoodAreaNumber"
                    ItemStyle-HorizontalAlign="Center" />
                <%--<asp:BoundField DataField="GoodId" HeaderText="GoodId" SortExpression="GoodId" ItemStyle-HorizontalAlign="Center"  /> --%>
                <asp:BoundField DataField="GoodNo" HeaderText="��Ʒ���" SortExpression="GoodNo" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GoodName" HeaderText="����" SortExpression="GoodName" ItemStyle-HorizontalAlign="Left" />
                  <asp:BoundField DataField="GoodTypeSmName" HeaderText="С��" SortExpression="GoodTypeSmName" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="GoodSpec" HeaderText="���" SortExpression="GoodSpec" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="totalNum" HeaderText="��Ŀ���" SortExpression="totalNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="RuChuNum" HeaderText="������" SortExpression="RuChuNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                  <asp:BoundField DataField="ZHIFA" HeaderText="ֱ��" SortExpression="ZHIFA" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CaIKuNum" HeaderText="�ɿ����" SortExpression="CaIKuNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="CaiGouNum" HeaderText="�ɹ����" SortExpression="CaiGouNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="OutNum" HeaderText="�ѳ�����" SortExpression="OutNum" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <asp:BoundField DataField="HouseNum" HeaderText="�������" SortExpression="HouseNum"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="avgGoodPrice" HeaderText="������" SortExpression="avgGoodPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" />
                <asp:BoundField DataField="avgSellPrice" HeaderText="���۵���" SortExpression="avgSellPrice"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" />
                <asp:TemplateField HeaderText="��Ӧ�̼��" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblPONOS" runat="server" Text='<%# GetlastSupplier(Eval("PONo"),Eval("goodId")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CaiDate" HeaderText="�ɹ�ʱ��" SortExpression="CaiDate" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="minPODate" HeaderText="����ʱ��" SortExpression="minPODate"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>        
    </asp:Panel>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
            PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
            NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    ��Ŀ������ɱ��ϼƣ�<asp:Label ID="lbltotalNum" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    ���������ɱ��ϼƣ�<asp:Label ID="lblRuChuNum" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    �ɿ�������ɱ��ϼƣ�<asp:Label ID="lblCaIKuNum" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    �ɹ�������ɱ��ϼƣ�<asp:Label ID="lblCaiGouNum" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    <br />
    ��Ŀ��������ܼۺϼƣ�<asp:Label ID="lbltotalNum_Sell" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    �����������ܼۺϼƣ�<asp:Label ID="lblRuChuNum_Sell" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    �ɿ���������ܼۺϼƣ�<asp:Label ID="lblCaIKuNum_Sell" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>
    �ɹ���������ܼۺϼƣ�<asp:Label ID="lblCaiGouNum_Sell" runat="server" ForeColor="Red" Text="0" Style="margin-right: 10px;"></asp:Label>

      
</asp:Content>
