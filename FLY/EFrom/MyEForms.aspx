<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyEForms.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.MyEForms" MasterPageFile="~/DefaultMaster.Master"
    Title="�ҵĹ���" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="VAN_OA.Model.JXC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <style type="text/css">
        .item
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                �����б�
            </td>
        </tr>
        <tr>
            <td>
                ��������
            </td>
            <td>
                <asp:DropDownList ID="ddlProType" runat="server" Width="200px" DataTextField="pro_Type"
                    DataValueField="pro_Id">
                </asp:DropDownList>
            </td>
            <td>
                ����ʱ��:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton1"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                ״̬
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="200px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>ִ����</asp:ListItem>
                    <asp:ListItem>ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                ������:
            </td>
            <td>
                <%-- <asp:TextBox ID="txtApper" runat="server" Width="200px"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ������:
            </td>
            <td>
                <asp:TextBox ID="txtAuper" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                �ȴ�������:
            </td>
            <td>
                <asp:TextBox ID="txtWating" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ���ݺ�:
            </td>
            <td>
                <asp:TextBox ID="txtNo" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                ��Ŀ����:
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                �ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                ����ʱ��:
            </td>
            <td>
                <asp:TextBox ID="txtSPForm" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtSPTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtSPForm">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton3"
                    Format="yyyy-MM-dd" TargetControlID="txtSPTo">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                ��Ŀ����:
                <asp:DropDownList ID="ddlIsSpecial" runat="server" Width="50px">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="0">������</asp:ListItem>
                    <asp:ListItem Value="1">����</asp:ListItem>
                </asp:DropDownList>
                ��Ŀ��˰��
                <asp:DropDownList ID="ddlHanShui" runat="server" Width="50px">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="1">��˰</asp:ListItem>
                    <asp:ListItem Value="0">����˰</asp:ListItem>
                </asp:DropDownList>
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
                ��Ŀ���
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text=">0"></asp:ListItem>
                    <asp:ListItem Value="0" Text="=0"></asp:ListItem>
                </asp:DropDownList>  AE��
                <asp:DropDownList ID="ddlAEUsers" runat="server" DataTextField="LoginName" DataValueField="Id">
                </asp:DropDownList>
                ��Ӧ�̼��:<asp:TextBox ID="txtPOSupplier" runat="server"></asp:TextBox>    <asp:CheckBox ID="cbPiPei" runat="server" Text="ȫƥ��" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="��ʼ״̬" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="�ѽ���" />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="�ѿ�Ʊ" />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="�ѽ���" />
                <asp:CheckBox ID="CheckBox5" runat="server" Text="���ⵥǩ��" />
                <asp:CheckBox ID="CheckBox6" runat="server" Text="��Ʊ��ǩ��" />
            </td>
        </tr>
        <tr>
           <td>
                ��˾����:
            </td>
            <td>
               <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX"></asp:DropDownList>
            </td>
            <td colspan="2">              
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
        DataKeyNames="id" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
        OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
        OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
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
                        �鿴
                    </td>
                    <td>
                        ɾ��
                    </td>
                    <td>
                        �ļ�����
                    </td>
                    <td>
                        ���ݺ�
                    </td>
                    <td>
                        ������
                    </td>
                    <td>
                        ����ʱ��
                    </td>
                    <td>
                        ����ʱ��
                    </td>
                    <td>
                        ������
                    </td>
                    <td>
                        ����ʱ��
                    </td>
                    <td>
                        �ȴ�������
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
            <asp:TemplateField HeaderText=" �鿴" HeaderStyle-Width="40">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Image/IconEdit.gif" CommandName="Edit"
                        AlternateText="�༭" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ɾ��">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Image/IconDelete.gif" AlternateText="ɾ��"
                        CommandName="Delete" OnClientClick='return confirm( "ȷ��ɾ����") ' />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="ProTyleName" HeaderText="�ļ�����">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="E_No" HeaderText="���ݺ�">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="CreatePer_Name" HeaderText="������">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="createTime" HeaderText="����ʱ��">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="AppPer_Name" HeaderText="������">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="appTime" HeaderText="����ʱ��">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="maxDoTime" HeaderText="����ʱ��">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="ToPer_Name" HeaderText="�ȴ�������">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:BoundField DataField="state" HeaderText="״̬">
                <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="��Ӧ�̼��" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��Ŀ" ItemStyle-CssClass="item" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblPONOS" runat="server" Text='<%# GetDemo(Eval("allE_id"),Eval("proId")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
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
