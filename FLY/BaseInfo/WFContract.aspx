<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFContract.aspx.cs" Inherits="VAN_OA.BaseInfo.WFContract"
    MasterPageFile="~/DefaultMaster.Master" Title="��ͬ��������" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
 <style>
        table
        {
            border-collapse: collapse;
            border-spacing: 0;
            border-left: 1px solid #888;
            border-top: 1px solid #888;
        }
        th, td
        {
            border-right: 1px solid #888;
            border-bottom: 1px solid #888;
            padding: 1px 1px;
        }
        th
        {
            font-weight: bold;
          
        }
        .myspan{ color:Red;}
    </style>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="2" style="height: 20px; background-color: #336699; color: White;">
                ��ͬ��������
            </td>
        </tr>
          <tr>
            <td height="25" width="30%" align="right">
                ��ͬ��� ��
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblContract_ProNo" runat="server" Text="" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ͬ���<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlContract_Type" runat="server">
                    <asp:ListItem Value="1">�ɹ���ͬ</asp:ListItem>
                    <asp:ListItem Value="2">���ۺ�ͬ</asp:ListItem>
                </asp:DropDownList>

                <asp:HiddenField ID="Contract_OldType" runat="server" /> 
            
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ͬ����<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlContract_Use" runat="server">
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="ϵͳ����">ϵͳ����</asp:ListItem>
                    <asp:ListItem Value="�˹���">�˹���</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="��ѯ">��ѯ</asp:ListItem>
                    <asp:ListItem Value="���">���</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                    <asp:ListItem Value="����">����</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ͬ���<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_No" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��Լ��λ<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_Unit" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ͬ����<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_Name" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ͬժҪ<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_Summary" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                �ܽ��<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_Total" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ǩ������ʱ��<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_Date" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtContract_Date">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ͬ��ҳ��<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_PageCount" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td height="25" width="30%" align="right">
                ��ͬ�ܷ���<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_AllCount" runat="server" Width="200px">2</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��������<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_BCount" runat="server" Width="200px">1</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��Ŀ���<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPONo" runat="server" Width="200px"  ReadOnly="true"></asp:TextBox>
                 <asp:LinkButton ID="lbtnSelectPONo" runat="server"
                    OnClientClick="javascript:window.showModalDialog('../JXC/DioSimpPOList.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
       ѡ��</asp:LinkButton>
                <asp:CheckBox ID="cbContract_IsRequire" runat="server"  Text="����" 
                    AutoPostBack="True" oncheckedchanged="cbContract_IsRequire_CheckedChanged"/>
                  <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                AE<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtAE" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ������<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlContract_Brokerage" runat="server" DataTextField="LoginName"
                    DataValueField="LoginName">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ǩ��<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:RadioButton ID="rabContract_IsSignA" Checked="true" GroupName="Contract_IsSign"
                    runat="server" Text="��" />
                <asp:RadioButton ID="rabContract_IsSignB" GroupName="Contract_IsSign" runat="server"
                    Text="��" />
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ���λ��<span class="myspan">*</span> ��
            </td>
            <td height="25" width="*" align="left">
                <asp:DropDownList ID="ddlContract_Local" runat="server">
                    <asp:ListItem Value="A">A</asp:ListItem>
                    <asp:ListItem Value="B">B</asp:ListItem>
                    <asp:ListItem Value="C">C</asp:ListItem>
                    <asp:ListItem Value="D">D</asp:ListItem>
                    <asp:ListItem Value="E">E</asp:ListItem>
                    <asp:ListItem Value="F">F</asp:ListItem>
                    <asp:ListItem Value="G">G</asp:ListItem>
                    <asp:ListItem Value="H">H</asp:ListItem>
                    <asp:ListItem Value="I">I</asp:ListItem>
                    <asp:ListItem Value="J">J</asp:ListItem>
                    <asp:ListItem Value="K">K</asp:ListItem>
                    <asp:ListItem Value="L">L</asp:ListItem>
                    <asp:ListItem Value="M">M</asp:ListItem>
                    <asp:ListItem Value="N">N</asp:ListItem>
                    <asp:ListItem Value="O">O</asp:ListItem>
                    <asp:ListItem Value="P">P</asp:ListItem>
                    <asp:ListItem Value="Q">Q</asp:ListItem>
                    <asp:ListItem Value="R">R</asp:ListItem>
                    <asp:ListItem Value="S">S</asp:ListItem>
                    <asp:ListItem Value="T">T</asp:ListItem>
                    <asp:ListItem Value="U">U</asp:ListItem>
                    <asp:ListItem Value="V">V</asp:ListItem>
                    <asp:ListItem Value="W">W</asp:ListItem>
                    <asp:ListItem Value="X">X</asp:ListItem>
                    <asp:ListItem Value="Y">Y</asp:ListItem>
                    <asp:ListItem Value="Z">Z</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlContract_Year" runat="server">
                    <asp:ListItem Value="2012">2012</asp:ListItem>
                    <asp:ListItem Value="2013">2013</asp:ListItem>
                    <asp:ListItem Value="2014">2014</asp:ListItem>
                    <asp:ListItem Value="2015">2015</asp:ListItem>
                    <asp:ListItem Value="2016">2016</asp:ListItem>
                    <asp:ListItem Value="2017">2017</asp:ListItem>
                    <asp:ListItem Value="2018">2018</asp:ListItem>
                    <asp:ListItem Value="2019">2019</asp:ListItem>
                    <asp:ListItem Value="2020">2020</asp:ListItem>
                      <asp:ListItem Value="2021">2021</asp:ListItem>
                    <asp:ListItem Value="2022">2022</asp:ListItem>
                    <asp:ListItem Value="2023">2023</asp:ListItem>
                    <asp:ListItem Value="2024">2024</asp:ListItem>
                    <asp:ListItem Value="2025">2025</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlContract_Month" runat="server">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                ��ע ��
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtContract_Remark" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAdd" runat="server" Text=" ��� " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" �޸� " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" �ر� " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
