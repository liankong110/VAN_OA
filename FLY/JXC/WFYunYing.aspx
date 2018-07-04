<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="WFYunYing.aspx.cs"
    Inherits="VAN_OA.JXC.WFYunYing" MasterPageFile="~/DefaultMaster.Master" Title="��Ӫָ��" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc2" %>
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
                ��Ӫָ��
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ����:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONO" runat="server" Width="200px"></asp:TextBox>
                ��Ŀ����:
                <asp:TextBox ID="txtPOName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                �ͻ�����:
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>
         AE��
        </td>
        <td> <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList></td>     
            <td>
                ��Ʊ��:
            </td>
            <td>
                <asp:TextBox ID="txtFPNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ����ʱ��:
            </td>
            <td>
                <asp:TextBox ID="txtPoDateFrom" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtPoDateTo" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtPoDateTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                ��Ʊ״̬:
            </td>
            <td>
                <asp:DropDownList ID="ddlFPState" runat="server" Width="200px">
                    <asp:ListItem Value="0">����</asp:ListItem>
                    <asp:ListItem Value="1">�ѿ�ȫƱ</asp:ListItem>
                    <asp:ListItem Value="2">δ��ȫƱ</asp:ListItem>
                    <asp:ListItem Value="3">δ��Ʊ</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ��
            </td>
            <td colspan="3"> 
                <asp:DropDownList ID="ddlPrice" runat="server">
                    <asp:ListItem Text=">="  Value=">=" ></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtPOTotal" runat="server" Width="200px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;  <asp:CheckBox ID="cbPOHeBing" runat="server" Text="������ϲ�"  Enabled="false" Checked="true" />
                ��˾���ƣ�
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName"
                  DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel1" runat="server" Enabled="true" Style="display: inline">
                    ��Ŀ���
                    <asp:DropDownList ID="ddlJinECha" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlJinECha_SelectedIndexChanged">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                    </asp:DropDownList>
                     ������&nbsp;&nbsp;&nbsp;&nbsp; δ����ʱ��:
                    <asp:DropDownList ID="ddlDiffDays" runat="server" Enabled="false">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="<=30��" Value="1"></asp:ListItem>
                        <asp:ListItem Text=">30��AND<=60��" Value="2"></asp:ListItem>
                        <asp:ListItem Text=">60��AND <=90��" Value="3"></asp:ListItem>
                        <asp:ListItem Text=">90��AND <=120��" Value="4"></asp:ListItem>
                        <asp:ListItem Text=">90��" Value="5"></asp:ListItem>
                        <asp:ListItem Text=">120��" Value="6"></asp:ListItem>
                        <asp:ListItem Text=">180��" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                &nbsp;&nbsp;&nbsp;&nbsp; ������:
                 <asp:DropDownList ID="ddlDaoKuanTotal" runat="server" >
                    <asp:ListItem Text=">" Value=">">
                    </asp:ListItem>
                    <asp:ListItem Text="<" Value="<">
                    </asp:ListItem>
                    <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                     <asp:ListItem Text="<=" Value="<=" ></asp:ListItem>
                    <asp:ListItem Text="=" Value="=">
                    </asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtDaoKuanTotal" runat="server" Width="120px"></asp:TextBox>
               <%-- ���δ��:
                 <asp:DropDownList ID="ddlKuCunWeiTotal" runat="server">
                     <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                    <asp:ListItem Text=">" Value=">">
                    </asp:ListItem>
                    <asp:ListItem Text="<" Value="<">
                    </asp:ListItem>
                    <asp:ListItem Text=">=" Value=">=" ></asp:ListItem>
                     <asp:ListItem Text="<=" Value="<=" ></asp:ListItem>
                    <asp:ListItem Text="=" Value="=">
                    </asp:ListItem>
                </asp:DropDownList>--%>
               <%-- <asp:TextBox ID="txtKuCunWeiTotal" runat="server" Width="120px"></asp:TextBox>--%>
                <br />
                ��Ŀ�ر� :
                <asp:DropDownList ID="ddlPoClose" runat="server">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="�ر�"></asp:ListItem>
                    <asp:ListItem Value="0" Text="δ�ر�"></asp:ListItem>
                </asp:DropDownList>
                ��Ŀѡ�У�
                <asp:DropDownList ID="ddlIsSelect" runat="server" Width="70px">
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
                ��Ŀ���
                <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                </asp:DropDownList>
                �ͻ�����:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType">
                </asp:DropDownList>
                �ͻ�����:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString" Style="left: 0px;">
                </asp:DropDownList>
                ��Ŀģ��:  <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <div style="display: inline">
                    <asp:DropDownList ID="ddlNoSpecial" runat="server">
                        <asp:ListItem Value="0">��������</asp:ListItem>
                        <asp:ListItem Value="1">����</asp:ListItem>
                        <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlShui" runat="server">
                        <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                        <asp:ListItem Value="1">��˰</asp:ListItem>
                        <asp:ListItem Value="0">����˰</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox ID="cbHadJiaoFu" runat="server" Text="�ѽ���" />
                    <asp:CheckBox ID="cbPoNoZero" runat="server" Text="��Ŀ��Ϊ0" />
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;��Ŀ���
                    <asp:DropDownList ID="dllCompareSell" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                    </asp:DropDownList>
                    ���۽��&nbsp;&nbsp; ��Ŀ���
                    <asp:DropDownList ID="dllCompareFP" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                    </asp:DropDownList>
                    ��Ʊ��� &nbsp;&nbsp; ��Ŀ���
                    <asp:DropDownList ID="dllCompareInvoice" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                        <asp:ListItem Text="<>" Value="<>"></asp:ListItem>
                    </asp:DropDownList>
                    ʵ���� 
                    <br />
                    ������: 
                    <asp:TextBox ID="txtDaoKLvFrom" runat="server" Width="50px" Text="0"></asp:TextBox><label style=" color:Red">%</label>~<asp:TextBox ID="txtDaoKLvTo" Text="100" runat="server" Width="50px"></asp:TextBox><label style=" color:Red">%</label>
                    ӯ������:
                    <asp:TextBox ID="txtYLFrom" runat="server" Width="50px"></asp:TextBox><label style=" color:Red">%</label>~<asp:TextBox ID="txtYLTo" runat="server" Width="50px"></asp:TextBox><label style=" color:Red">%</label>
                
                 <asp:DropDownList ID="ddlIsPoFax" runat="server">
                   <asp:ListItem Value="-1">ȫ��</asp:ListItem>                       
                        <asp:ListItem Value="1">��˰</asp:ListItem>
                         <asp:ListItem Value="0">����˰</asp:ListItem>                      
                    </asp:DropDownList>
                   
                    ���ɹ��ܶ�:
                      <asp:DropDownList ID="ddlJCGTotal" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtJCGTotal" runat="server" Width="50px"></asp:TextBox>
                     ��֧���ܶ�:
                      <asp:DropDownList ID="ddlJZFTotal" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtJZFTotal" runat="server" Width="50px"></asp:TextBox>
                     Ӧ���ܶ�:
                      <asp:DropDownList ID="dllYFTotal" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtYFTotal" runat="server" Width="50px"></asp:TextBox><br />
                     Ӧ�����:
                      <asp:DropDownList ID="ddlYFKCTotal" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtYFKCTotal" runat="server" Width="50px"></asp:TextBox>
                     Ԥ��δ����:
                      <asp:DropDownList ID="dllYFWDKTotal" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtYFWDKTotal" runat="server" Width="50px"></asp:TextBox>
                     ��Ŀ����:
                      <asp:DropDownList ID="ddlXMJLTotal" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtXMJLTotal" runat="server" Width="50px"></asp:TextBox>
                     ʵ������:
                      <asp:DropDownList ID="ddlSJLRTotal" runat="server">
                        <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                        <asp:ListItem Text=">" Value=">"></asp:ListItem>
                        <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                        <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                        <asp:ListItem Text="=" Value="="></asp:ListItem>
                    </asp:DropDownList>
                     <asp:TextBox ID="txtSJLRTotal" runat="server" Width="50px"></asp:TextBox>
                </div>
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
   
            <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
            
        <tr>
            <td>
                ����������:<asp:CheckBox ID="cbFeiTS" runat="server"  Checked="true"/>
            </td>
            <td colspan="1">
               Ԥ��Ӧ�գ�
            </td>
            <td><asp:DropDownList ID="ddlYQYS" runat="server">
               <asp:ListItem Text=">" Value=">"></asp:ListItem>
                  <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                           <asp:ListItem Text="=" Value="="></asp:ListItem>
                              <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtYQYS" runat="server" Text="0"></asp:TextBox>
            </td>
            <td>Ӧ���ܶ</td>
            <td>
                <asp:DropDownList ID="ddlYFZE" runat="server">
               <asp:ListItem Text=">" Value=">"></asp:ListItem>
                  <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                           <asp:ListItem Text="=" Value="="></asp:ListItem>
                              <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtYFZE" runat="server" Text="0"></asp:TextBox>
            </td>
           
        </tr>

            <tr>
            <td>
                �������� :<asp:CheckBox ID="cbTS" runat="server" Checked="true"/>
            </td>
            <td colspan="1">
               Ԥ��Ӧ�գ�
            </td>
            <td><asp:DropDownList ID="ddlYQYS1" runat="server">
               <asp:ListItem Text=">" Value=">"></asp:ListItem>
                  <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                           <asp:ListItem Text="=" Value="="></asp:ListItem>
                              <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtYQYS1" runat="server" Text="0"></asp:TextBox>
            </td>
            <td>Ӧ���ܶ</td>
            <td>
                <asp:DropDownList ID="ddlYFZE1" runat="server">
                     <asp:ListItem Text="ȫ��" Value="-1"></asp:ListItem>
               <asp:ListItem Text=">" Value=">"></asp:ListItem>
                  <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                     <asp:ListItem Text="<" Value="<"></asp:ListItem>
                        <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                           <asp:ListItem Text="=" Value="="></asp:ListItem>
                             
                </asp:DropDownList>
                <asp:TextBox ID="txtYFZE1" runat="server"></asp:TextBox>
            </td>
           
        </tr>
         <tr>
            <td colspan="5">
                <div align="right">
                    <asp:Button ID="Button1" runat="server" Text=" �� ѯ2 " BackColor="Yellow" OnClick="btnSelect2_Click" />
                </div>
            </td>
        </tr>
       </table>
           
    ע��ӯ������=ʵ������/��֧���ܶ�+��������+�����ܶ
     ��ĿӦ��=���۽��-���ʶԤ��Ӧ��=��Ŀ���--���ʶ�
���ɹ��ܶ�=ԭ�ɹ���-�ɹ��˻������Ľ���֧�����Ǹ���Ŀͨ����Ӧ��Ԥ����֧�����ܽ�ת֧���������ظ����룩=ԭ֧����-�º�ɹ��˻����ɵļ���������ɸ���֧������
Ӧ����淴Ӧ��=�����Ŀ�ɹ����Կ��� �ܽ�����ͨ��KC ֧����ȥ�Ľ�;Ӧ���ܶ� =���ɹ��ܶ�-��֧���ܶ�-Ӧ�����;��Ӫ������=����ܽ��+��ĿӦ�պϼ�-��ĿӦ���ϼ�+Ԥ��δ����ϼ�+ KCԤ��δ����ϼ�-���δ֧���ϼƣ�
    1. δ����ϵ������=����ܽ��+��ĿӦ�պϼ�-��ĿӦ���ϼ�+Ԥ��δ����ϼ�+ KCԤ��δ����ϼ�-���δ֧���ϼƣ��������Ŀ������Ĺ������������淽���ڵ����������������������Ŀ��Ԥ��Ӧ��>0��Ӧ���ܶ�>0,�Լ�������Ŀ��Ԥ��Ӧ��>0 ���ģ������Խ��浽�����Ϸ����������������ԣ���������Ļ�ϵ������վ�
    <asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Horizontal">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvList_PageIndexChanging"
            OnRowDataBound="gvList_RowDataBound" OnRowCommand="gvList_RowCommand" OnRowDeleting="gvList_RowDeleting">
            <PagerTemplate>
                <br />
            </PagerTemplate>
            <EmptyDataTemplate>
                <table width="100%">
                    <tr style="height: 20px; background-color: #336699; color: White;">
                        <td>
                            ��Ŀ���
                        </td>
                        <td>
                            ��Ŀ����
                        </td>
                        <td>
                            AE
                        </td>
                        <td>
                            ˰
                        </td>
                        <td>
                            ��Ŀ���
                        </td>
                        <td>
                            ���۽��
                        </td>
                        <td>
                            ��Ʊ���
                        </td>
                        <td>
                            δ��Ʊ��
                        </td>
                        <td>
                            �����
                        </td>
                        <td>
                            ��ĿӦ��
                        </td>
                        <td>
                            ���ɹ��ܶ�
                        </td>
                        <td>
                             ��֧���ܶ�
                        </td>
                        <td>
                            Ӧ���ܶ�
                        </td>
                        <td>
                            ��Ŀ����
                        </td>
                        <td>
                            ʵ������
                        </td>
                        <td>
                            ������
                        </td>
                        <td>
                            ӯ������
                        </td>
                    </tr>
                    <tr>
                        <td colspan="17" align="center" style="height: 80%">
                            ---��������---
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="PONO" HeaderText="��Ŀ���" SortExpression="PONO" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PoName" HeaderText="��Ŀ����" SortExpression="PoName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="˰">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsHanShui" runat="server" Checked='<% #Eval("IsHanShui") %>'
                            Enabled="false" />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:BoundField DataField="POTotal" HeaderText="��Ŀ���" SortExpression="POTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <asp:BoundField DataField="GoodSellPriceTotal" HeaderText="���۽��" SortExpression="GoodSellPriceTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FPTotal" HeaderText="��Ʊ�ܶ�" SortExpression="FPTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <asp:BoundField DataField="NoFpTotal" HeaderText="δ��Ʊ��" SortExpression="NoFpTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <asp:BoundField DataField="InvoiceTotal" HeaderText="���˶�" SortExpression="InvoiceTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="YingShouTotal" HeaderText="��ĿӦ��" SortExpression="YingShouTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                       <asp:BoundField DataField="YuQiYingShou" HeaderText="Ԥ��Ӧ��" SortExpression="YuQiYingShou" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="LastCaiTotal" HeaderText="���ɹ��ܶ�" SortExpression="LastCaiTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="SupplierTotal" HeaderText="��֧���ܶ�" SortExpression="SupplierTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                          <asp:BoundField DataField="LastSupplierTotal" HeaderText="��֧���ܶ�" SortExpression="LastSupplierTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="YingFuTotal" HeaderText="Ӧ���ܶ�" SortExpression="YingFuTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                     <asp:BoundField DataField="YingFuKuCun" HeaderText="Ӧ�����" SortExpression="YingFuKuCun" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                      <asp:BoundField DataField="NoInvoice" HeaderText="���δ��" SortExpression="NoInvoice" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />

                      <asp:BoundField DataField="YuFuWeiTotal" HeaderText="Ԥ��δ����" SortExpression="YuFuWeiTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />

                     <asp:BoundField DataField="XuJianTotal" HeaderText="��֧����;" SortExpression="XuJianTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="MaoLiTotal" HeaderText="��Ŀ����" SortExpression="MaoLiTotal" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <asp:BoundField DataField="LiRunTotal" HeaderText="ʵ������" SortExpression="LiRunTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="InvoiceBiLiTotal" HeaderText="������" SortExpression="InvoiceBiLiTotal" DataFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right"  />
                <asp:BoundField DataField="YLNL" HeaderText="ӯ������" SortExpression="YLNL" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                     />
            </Columns>
            <PagerStyle HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="Black" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle ForeColor="Black" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
            CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
            CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
            PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
            NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </asp:Panel>
    ��Ŀ���ϼ�:
    <asp:Label ID="lblAllPoTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    ���۽��ϼ�:
     <asp:Label ID="lblSellTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      ��Ʊ���ϼ�:
    <asp:Label ID="lblFPTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      δ��Ʊ��ϼ�:
    <asp:Label ID="lblNoFpTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
       �����ϼ�:
    <asp:Label ID="lblInvoiceTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
       ��ĿӦ�պϼ�:
    <asp:Label ID="lblYingShouTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      Ԥ��Ӧ�պϼ�:
    <asp:Label ID="lblYuQiYingShouTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

    <br />

     �ɹ����ϼ�:
    <asp:Label ID="lblLastCaiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      ֧�����ϼ�:
    <asp:Label ID="lblSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      ��֧�ۺϼ�:
    <asp:Label ID="lblLastSupplierTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      Ӧ���ϼ�:
    <asp:Label ID="lblYingFuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     Ӧ�����ϼ�:
    <asp:Label ID="lblYingFuKuCunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
         Ԥ��δ����ϼ�:
    <asp:Label ID="lblXuJianTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

       ��Ŀ�����ϼ�:
    <asp:Label ID="lblMaoLi" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
       ʵ������ϼ�:
    <asp:Label ID="lblLiRunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

    <br />
     �����ܱ���:
    <asp:Label ID="lblInvoiceBiLiTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      ӯ�������ܱ���:
    <asp:Label ID="lblYLNL" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     <br />
      ����ܽ��:
    <asp:Label ID="lblKuCunTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

       KCԤ��δ����ϼ�:
    <asp:Label ID="lblKCXuJianTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
    ���δ֧���ϼ�:
    <asp:Label ID="lblKCWeiZhiFuTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
      ��Ӫ������:
    <asp:Label ID="lblYingYunAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
     δ����ϵ������:
    <asp:Label ID="WeilblYingYunAllTotal" runat="server" Text="0" ForeColor="Red"></asp:Label>&nbsp;&nbsp;

</asp:Content>
