<%@ Page Language="C#" AutoEventWireup="True" Culture="auto" UICulture="auto" CodeBehind="GuestTrackGuiLeiList.aspx.cs"
    Inherits="VAN_OA.ReportForms.GuestTrackGuiLeiList" MasterPageFile="~/DefaultMaster.Master"
    Title="�ͻ���ϵ���ٱ����" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="server">
    <table cellpadding="0" cellspacing="0" width="90%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">
                �ͻ�����
            </td>
        </tr>
        <tr>
            <td>
                �Ǽ�����:
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                -<asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="Image1"
                    Format="yyyy-MM-dd" TargetControlID="txtFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton1"
                    Format="yyyy-MM-dd" TargetControlID="txtTo">
                </cc1:CalendarExtender>
            </td>
            <td>
                �ͻ�����
            </td>
            <td>
                <asp:TextBox ID="txtGuestName" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                AE:
            </td>
            <td>
                <asp:DropDownList ID="ddlAE" runat="server" DataTextField="LoginName" DataValueField="Id"
                  >
                </asp:DropDownList>
            </td>
            <td>
                INSIDE:
            </td>
            <td>
                <asp:DropDownList ID="ddlINSIDE" runat="server" DataTextField="LoginName" DataValueField="Id"
                  >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ���ݺ�:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server"></asp:TextBox>
            </td>
            <td>
                ����:
            </td>
            <td>
                <asp:DropDownList ID="ddlSelectYears" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlJidu" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

         <tr>
            <td colspan="4">
                ����: <asp:DropDownList ID="ddlSpecial" runat="server">
                    <asp:ListItem Value="-1">ȫ��</asp:ListItem>
                    <asp:ListItem Value="1">����</asp:ListItem>
                    <asp:ListItem Value="0">������</asp:ListItem>                   
                </asp:DropDownList>
                �ͻ����� :  <asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType">
                </asp:DropDownList>
                �ͻ����� :  <asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro" DataTextField="GuestProString" style="left:0px;">
                </asp:DropDownList>
                ��ϵ�� : <asp:TextBox ID="txtLikeMan" runat="server" ></asp:TextBox>
                �绰/�ֻ�: <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            </td>             
            </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />
                    &nbsp;
                    <br />
                    <asp:Button ID="btnSpecial" runat="server" Text=" �� ��(����) " BackColor="Yellow" OnClick="btnSpecial_Click" />
                
                     <asp:Button ID="btnGuestType" runat="server" Text=" �� ��(����) " 
                        BackColor="Yellow" onclick="btnGuestType_Click"  />
                          <asp:Button ID="btnCopyGuestType" runat="server" Text="ͬ���ͻ�����" 
                        BackColor="Yellow" onclick="btnCopyGuestType_Click"  />
                        
                          <asp:Button ID="btnSaveGuestPro" runat="server" Text=" �� ��(����) " 
                        BackColor="Yellow" onclick="btnSaveGuestPro_Click"  />
                         <asp:Button ID="btnCopyGuestPro" runat="server" Text="ͬ���ͻ�����" 
                        BackColor="Yellow" onclick="btnCopyGuestPro_Click"  />
                </div>
            </td>
        </tr>
    </table>
    <br>
    <asp:Panel ID="Panel1" runat="server" Width="100%" ScrollBars="Horizontal" Height="100%">
        <asp:GridView ID="gvList" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
            DataKeyNames="Id" Width="200%" AllowPaging="True" AutoGenerateColumns="False"
            OnPageIndexChanging="gvList_PageIndexChanging" OnRowDeleting="gvList_RowDeleting"
            OnRowEditing="gvList_RowEditing" OnRowDataBound="gvList_RowDataBound">
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
                            AE
                        </td>
                        <td>
                            %
                        </td>
                        <td>
                            INSIDE
                        </td>
                        <td>
                            %
                        </td>
                        <td>
                            ���ݺ�
                        </td>
                        <td>
                            ����
                        </td>
                        <td>
                            �ͻ�����
                        </td>
                        <td>
                            �绰/�ֻ�
                        </td>
                        <td>
                            ��ϵ��
                        </td>
                        <td>
                            ְ��
                        </td>
                        <td>
                            ���������
                        </td>
                        <td>
                            �Ƿ�������
                        </td>
                        <td>
                            QQ/MSN��ϵ
                        </td>
                        <td>
                            �ͻ�ID
                        </td>
                        <td>
                            ��ϵ�˵�ַ
                        </td>
                        <td>
                            ��ϵ����ַ
                        </td>
                        <td>
                            �ͻ�˰��ǼǺ�
                        </td>
                        <td>
                            �ͻ�����ע���
                        </td>
                        <td>
                            �����˺�
                        </td>
                        <td>
                            ������
                        </td>
                        <td>
                            �ϼ������۶�
                        </td>
                        <td>
                            �ϼ�������
                        </td>
                        <td>
                            �ϼ����տ���/��
                        </td>
                        <td>
                            ��
                        </td>
                        <td>
                            ����
                        </td>
                        <td>
                            ��ע
                        </td>
                        <td>
                            INSIDE��ע
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
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbIsSpecial" runat="server" Checked='<% #Eval("IsSpecial") %>' />
                    </ItemTemplate>
                    <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="myId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AEName" HeaderText="AE" SortExpression="AEName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AEPer" HeaderText="%" SortExpression="AEPer" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="INSIDEName" HeaderText="INSIDE" SortExpression="INSIDEName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="INSIDEPer" HeaderText="%" SortExpression="INSIDEPer" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ProNo" HeaderText="���ݺ�" SortExpression="ProNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Time" HeaderText="�Ǽ�����" SortExpression="Time" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                    ItemStyle-HorizontalAlign="Center" />
                       <asp:TemplateField HeaderText="�ͻ�����">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidtxt" Value='<%#Eval("MyGuestType")%>' />
                    <asp:DropDownList ID="dllMyGuestType" runat="server" DataValueField="GuestType" DataTextField="GuestType">
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="�ͻ�����">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidMyGuestProTxt" Value='<%#Eval("MyGuestPro")%>' />
                    <asp:DropDownList ID="dllMyGuestPro" runat="server" DataValueField="GuestPro" DataTextField="GuestProString">
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            
                <asp:BoundField DataField="Phone" HeaderText="�绰/�ֻ�" SortExpression="Phone" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="LikeMan" HeaderText="��ϵ��" SortExpression="LikeMan" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Job" HeaderText="ְ��" SortExpression="Job" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FoxOrEmail" HeaderText="���������" SortExpression="FoxOrEmail"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Save_Name" HeaderText="�Ƿ�������" SortExpression="Save_Name"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="QQMsn" HeaderText="QQ/MSN��ϵ" SortExpression="QQMsn" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestId" HeaderText="�ͻ�ID" SortExpression="GuestId" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestAddress" HeaderText="��ϵ�˵�ַ" SortExpression="GuestAddress"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestHttp" HeaderText="��ϵ����ַ" SortExpression="GuestHttp"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestShui" HeaderText="�ͻ�˰��ǼǺ�" SortExpression="GuestShui"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestGong" HeaderText="�ͻ�����ע���" SortExpression="GuestGong"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestBrandNo" HeaderText="�����˺�" SortExpression="GuestBrandNo"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestBrandName" HeaderText="������" SortExpression="GuestBrandName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Remark" HeaderText="��ע" SortExpression="Remark" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestTotal" HeaderText="�ϼ������۶�" SortExpression="GuestTotal"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestLiRun" HeaderText="�ϼ�������" SortExpression="GuestLiRun"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GuestDays" HeaderText="�ϼ����տ���/��" SortExpression="GuestDays"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="YearNo" HeaderText="��" SortExpression="YearNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="QuartNo" HeaderText="����" SortExpression="QuartNo" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="INSIDERemark" HeaderText="INSIDE��ע" SortExpression="INSIDERemark"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreateTime" HeaderText="����ʱ��" SortExpression="CreateTime"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
            <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                HorizontalAlign="Center" />
            <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
            <RowStyle CssClass="InfoDetail1" />
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always"
            TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%" CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left"   ButtonImageAlign="Middle"  CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
        PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
            NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager> 
    </asp:Panel>
</asp:Content>
