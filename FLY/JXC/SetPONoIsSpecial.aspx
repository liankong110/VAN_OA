<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPONoIsSpecial.aspx.cs"
    Inherits="VAN_OA.JXC.SetPONoIsSpecial" Culture="auto" UICulture="auto" MasterPageFile="~/DefaultMaster.Master"
    Title="��Ŀ����" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                ��Ŀ����
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ���:
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtPONo" runat="server"></asp:TextBox>��˾���ƣ�
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComSimpName"
                    Width="200PX">
                </asp:DropDownList>
            </td>
            <td>
                ��Ŀ���ݺ�:
            </td>
            <td>
                <asp:TextBox ID="txtProNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ��Ŀ����:
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
                AE��
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
             <%--   <asp:CheckBox ID="cbClose" runat="server" Text="δ�ر�" ForeColor="Red" />--%>
                ��Ŀ�ر�:
                 <asp:DropDownList ID="ddlColse" runat="server" ForeColor="Red">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="�ر�"></asp:ListItem>
                    <asp:ListItem Value="0" Text="δ�ر�"></asp:ListItem>
                </asp:DropDownList>
                ��Ŀ����:
               <%-- <asp:CheckBox ID="cbSpecial" runat="server" Text="����" ForeColor="Red" />--%>
                  <asp:DropDownList ID="ddlSpecial" runat="server" ForeColor="Red">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="����"></asp:ListItem>        
                         <asp:ListItem Value="0" Text="������"></asp:ListItem>           
                </asp:DropDownList>
              <%--  <asp:CheckBox ID="cbIsPoFax1" runat="server" Text="��˰" AutoPostBack="True" OnCheckedChanged="cbIsPoFax_CheckedChanged"
                    ForeColor="Red" />--%>
                ��Ŀ��˰:
                    <asp:DropDownList ID="ddlIsPoFax" runat="server" ForeColor="Red" 
                    AutoPostBack="true" onselectedindexchanged="ddlIsPoFax_SelectedIndexChanged" >
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="��˰"></asp:ListItem>
                    <asp:ListItem Value="0" Text="����˰"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="Id" DataTextField="FpType"
                    Enabled="false">
                </asp:DropDownList>
                ��Ŀѡ�У�
                <asp:DropDownList ID="ddlNoSelected" runat="server">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="ѡ��"></asp:ListItem>
                    <asp:ListItem Value="0" Text="δѡ��"></asp:ListItem>
                </asp:DropDownList>
                ����ѡ�У�
                <asp:DropDownList ID="ddlJieIsSelected" runat="server">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="ѡ��"></asp:ListItem>
                    <asp:ListItem Value="0" Text="δѡ��"></asp:ListItem>
                </asp:DropDownList>
                ��Ŀ���
                <asp:DropDownList ID="ddlPOTyle" runat="server" DataTextField="BasePoType" DataValueField="Id">
                    <%--   <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1">����</asp:ListItem>
                    <asp:ListItem Value="2">����</asp:ListItem>--%>
                </asp:DropDownList> <br />
                ��Ŀ���ƣ�
                <asp:TextBox ID="txtPOName" runat="server" Width="150px"></asp:TextBox>
                 
                ��Ŀ��
                <asp:DropDownList ID="ddlFuHao" runat="server">
                    <asp:ListItem Value=">" Text=">"></asp:ListItem>
                    <asp:ListItem Value="<" Text="<"></asp:ListItem>
                    <asp:ListItem Value=">=" Text=">="></asp:ListItem>
                     <asp:ListItem Value="<=" Text="<="></asp:ListItem>
                       <asp:ListItem Value="=" Text="="></asp:ListItem>
                </asp:DropDownList>
                 <asp:TextBox ID="txtPoTotal" runat="server" ></asp:TextBox>
                 �ͻ����ƣ� <asp:TextBox ID="txtGuestName" runat="server" ></asp:TextBox>
                 ��Ʊ���ͣ�<asp:DropDownList ID="ddlFPType" runat="server" DataValueField="Id" DataTextField="FpType"></asp:DropDownList>
                <asp:TextBox ID="txtPlanDayForm" runat="server" Width="50px"></asp:TextBox>
                <asp:DropDownList ID="ddlPlanDayForm" runat="server">
                    <asp:ListItem Text=">" Value=">"></asp:ListItem>
                    <asp:ListItem Text=">=" Value=">="></asp:ListItem>
                    <asp:ListItem Text="<" Value="<"></asp:ListItem>
                    <asp:ListItem Text="<=" Value="<="></asp:ListItem>
                    <asp:ListItem Text="=" Value="="></asp:ListItem>
                </asp:DropDownList>
                �ƻ��깤����
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
                </asp:DropDownList>��Ŀ���<asp:DropDownList ID="ddlEque2" runat="server">
                    <asp:ListItem Value="&gt;"></asp:ListItem>
                    <asp:ListItem>&gt;=</asp:ListItem>
                </asp:DropDownList>
                 <asp:TextBox ID="txtEque2" runat="server"  Width="100"></asp:TextBox>

                 �ͻ�����:<asp:DropDownList ID="ddlGuestTypeList" runat="server" DataValueField="GuestType"
                    DataTextField="GuestType"  Width="50px">
                </asp:DropDownList>
                �ͻ�����:<asp:DropDownList ID="ddlGuestProList" runat="server" DataValueField="GuestPro"
                    DataTextField="GuestProString"  Width="50px">
                </asp:DropDownList>��Ŀģ��:
                 <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName" runat="server"></asp:DropDownList>
                 �Ʊ�������
                <asp:DropDownList ID="ddlJiLiang" runat="server">
                    <asp:ListItem Value="-1" Text="ȫ��"></asp:ListItem>
                    <asp:ListItem Value="1" Text="����"></asp:ListItem>
                    <asp:ListItem Value="0" Text="������"></asp:ListItem>
                </asp:DropDownList>
                <br />
                ע����Ŀ���=0����Ŀ����=0 ����Ŀ�����������Զ��ڹرհ�ť�����
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div align="right">
                    <asp:Button ID="btnSelect" runat="server" Text=" �� ѯ " BackColor="Yellow" OnClick="btnSelect_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveIsClose" runat="server" Text="����(�ر�)" BackColor="Yellow" OnClick="btnSaveIsClose_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnIsSelected" runat="server" Text="����(ѡ��)" BackColor="Yellow" OnClick="btnIsSelected_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text=" �� �� " BackColor="Yellow" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnJieIsSelected" runat="server" Text="����(����ѡ��)" BackColor="Yellow"
                        OnClick="btnJieIsSelected_Click" />&nbsp;&nbsp;&nbsp;                        
                        <asp:Button ID="btnGuestPro" runat="server" Text="����(�ͻ�����)" BackColor="Yellow"
                        OnClick="btnGuestPro_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnGuestType" runat="server" Text="����(�ͻ�����)" BackColor="Yellow"
                        OnClick="btnGuestType_Click" />&nbsp;&nbsp;&nbsp;
                </div>
            </td>
        </tr>
    </table>
    �������н��б༭���������󼴿�(���浱ǰҳ����Ϣ);�����Ŀ���<�ɱ� ���������ⶩ����������ʾ�ۺ�ɫ

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
                        ���ݺ�
                    </td>
                    <td>
                        ��Ŀ����
                    </td>
                    <td>
                        ��Ŀ����
                    </td>
                    <td>
                        �ͻ�����
                    </td>
                    <td>
                        ��Ŀ����
                    </td>
                    <td>
                        AE
                    </td>
                    <td>
                        ����
                    </td>
                    <td>
                        ��˰
                    </td>
                    <td>
                        ��Ʊ����
                    </td>
                    <td>
                        ��Ŀ���
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
<%--
             <asp:TemplateField HeaderText="��Ŀ���">
                <ItemTemplate>
                    <asp:Label ID="SumPOTotal" runat="server" Text='<%# GetNum(Eval("SumPOTotal")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="��Ŀ����">
                <ItemTemplate>
                    <asp:Label ID="maoliTotal" runat="server" Text='<%# GetNum(Eval("maoliTotal")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>

              <asp:BoundField DataField="SumPOTotal" HeaderText="��Ŀ���" DataFormatString="{0:n2}"
                    SortExpression="SumPOTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="goodTotal" HeaderText="�ɱ�" DataFormatString="{0:n2}"
                    SortExpression="goodTotal" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                  <asp:BoundField DataField="maoliTotal" HeaderText="��Ŀ����" SortExpression="maoliTotal" Visible="false"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
            <asp:BoundField DataField="POName" HeaderText="��Ŀ����" SortExpression="POName" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="GuestName" HeaderText="�ͻ�����" SortExpression="GuestName"
                ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PODate" HeaderText="��Ŀ����" SortExpression="PODate" ItemStyle-HorizontalAlign="Center"
                DataFormatString="{0:yyyy-MM-dd}">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AE" HeaderText="AE" SortExpression="AE" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="�ر�">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbHanShui" runat="server" Text="�ر�" AutoPostBack="True" OnCheckedChanged="cbHanShui_CheckedChanged"
                        Enabled="<%# IsCloseEdist() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsClose" runat="server" Checked='<% #Eval("IsClose") %>' Enabled="<%# IsCloseEdist() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����ѡ�� ">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbJieIsSelected" runat="server" Text="����ѡ��" AutoPostBack="True"
                        OnCheckedChanged="cbJieIsSelected_CheckedChanged" Enabled="<%# IsJieIsSelected() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbJieIsSelected" runat="server" Checked='<% #Eval("JieIsSelected") %>'
                        Enabled="<%# IsJieIsSelected() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ѡ��">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbIsSelected1" runat="server" Text="ѡ��" AutoPostBack="True" OnCheckedChanged="cbIsSelected_CheckedChanged"
                        Enabled="<%# IsSelectedEdit() %>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSelected" runat="server" Checked='<% #Eval("IsSelected") %>'
                        Enabled="<%# IsSelectedEdit() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsSpecial" runat="server" Checked='<% #Eval("IsSpecial") %>'
                        Enabled="<%# IsSpecialEdit() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��˰">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsPoFax" runat="server" Checked='<% #Eval("IsPoFax") %>' Enabled="<%# IsFaxEdist() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:BoundField DataField="FpType" HeaderText="��Ʊ����" SortExpression="FpType" >
                <ItemStyle HorizontalAlign="Center" Font-Size="7"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="��Ʊ����">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidtxt" Value='<%#Eval("FpType")%>' />
                    <asp:DropDownList ID="dllFPstye" runat="server" DataValueField="FpType" DataTextField="FpType" style="font-size:8px;" >
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��Ŀģ��">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidModeltxt" Value='<%#Eval("Model")%>' />                    

                    <asp:DropDownList ID="ddlModel" DataTextField="ModelName" DataValueField="ModelName"  Enabled="<%# IsModelEdit() %>" runat="server"></asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="��Ŀ���">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidPOTypetxt" Value='<%#Eval("POType")%>' />
                    <asp:DropDownList ID="dllPOType" runat="server" Enabled="<%# IsPOType() %>" DataTextField="BasePoType"
                        DataValueField="Id">
                        <%-- <asp:ListItem Value="1">����</asp:ListItem>
                    <asp:ListItem Value="2">����</asp:ListItem>--%>
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="�ͻ�����">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidGuestProtxt" Value='<%#Eval("GuestPro")%>' />
                    <asp:DropDownList ID="dllGuestPro" runat="server" Enabled="<%# IsGuestPro() %>" DataTextField="GuestProString"
                        DataValueField="GuestPro">                      
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="�ͻ�����">
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hidGuestTypetxt" Value='<%#Eval("GuestType")%>' />
                    <asp:DropDownList ID="dllGuestType" runat="server" Enabled="<%# IsGuestType() %>" DataTextField="GuestType"
                        DataValueField="GuestType">                      
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

              <asp:TemplateField HeaderText="�Ʊ�����">
                <ItemTemplate>
                    <asp:CheckBox ID="cbChengBenJiLiang" runat="server" Checked='<% #Eval("ChengBenJiLiang") %>'
                        Enabled="<%# IsChengBenJiLiang() %>" />
                </ItemTemplate>
                <ItemStyle BorderColor="#E5E5E5" HorizontalAlign="Center" />
            </asp:TemplateField>

               <asp:TemplateField HeaderText="�ƻ��깤����">
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
        TextBeforePageIndexBox="��ת����" TextAfterPageIndexBox="ҳ" CustomInfoSectionWidth="40%"
        CurrentPageButtonPosition="Center" ShowCustomInfoSection="Left" ButtonImageAlign="Middle"
        CustomInfoHTML="��<font color='red'><b>%currentPageIndex%</b></font>ҳ����%PageCount%ҳ��ÿҳ��ʾ%PageSize%����¼"
        PageSize="10" CurrentPageIndex="1" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ"
        NextPageText="��ҳ" OnPageChanged="AspNetPager1_PageChanged">
    </webdiyer:AspNetPager>

    ��Ŀģ��˵���� 
       <asp:GridView ID="gvModel" runat="server" BorderColor="#FBFBFB" BorderStyle="Solid"
           ShowFooter="false" Width="100%" AutoGenerateColumns="False"
           ShowHeader="false"
           Style="border-collapse: collapse;">
           <Columns>
               <asp:BoundField DataField="ModelName" HeaderText="ģ������" SortExpression="MyPoType"  />
               <asp:BoundField DataField="ModelRemark" HeaderText="ģ��˵��" SortExpression="XiShu"  />
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
