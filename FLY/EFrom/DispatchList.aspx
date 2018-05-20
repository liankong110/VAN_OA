<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchList.aspx.cs" Inherits="VAN_OA.EFrom.DispatchList" MasterPageFile="~/DefaultMaster.Master" Title="Ԥ  ��  ��  ��  ��" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">


    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">Ԥ�ڱ�����-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>

                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                    <asp:ListItem Value="0">��ͨ����</asp:ListItem>
                    <asp:ListItem Value="1">С��ɹ�����</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>�¼��������ڣ�<font style="color: Red">*</font></td>
            <td>

                <asp:TextBox ID="txtEvTime" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtEvTime" PopupButtonID="Image1">
                </cc1:CalendarExtender>
            </td>
            <td>�����ˣ�<font style="color: Red">*</font></td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox></td>


            <td>��д���ڣ�<font style="color: Red">*</font></td>
            <td>

                <asp:TextBox ID="txtCreatime" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtCreatime" PopupButtonID="ImageButton2">
                </cc1:CalendarExtender>
            </td>

            <td>�������кţ�
            </td>

            <td>
                <asp:TextBox ID="txtCardNo" ReadOnly="true" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>

            <td>��Ŀ���룺<font style="color: Red">*</font></td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="lbtnSelNo1" runat="server"
                    OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOList.aspx?AE=1',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton2_Click">ѡ��</asp:LinkButton>

                <asp:LinkButton ID="lbtnSelNo2" runat="server"
                    OnClientClick="javascript:window.showModalDialog('../JXC/DioCAI_OrderInHouseList.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="lbtnSelNo2_Click">ѡ��</asp:LinkButton>
                AE:<asp:Label ID="lblAe" runat="server" Text=""></asp:Label>
            </td>

            <td>��Ŀ����:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
            </td>

            <td colspan="4">�ͻ����ƣ�
         
             
                <asp:TextBox ID="txtSupplier" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>


            </td>
        </tr>


        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">������
          &nbsp; &nbsp; &nbsp;
          
                    
            </td>
        </tr>

        <tr>

            <td>�ص㣺
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtBusFromAddress" runat="server" Width="220px"></asp:TextBox>&nbsp;&nbsp;��&nbsp;&nbsp;<asp:TextBox ID="txtBusToAddress"
                    runat="server" Width="221px"></asp:TextBox>
            </td>
            <td>���
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtBusTotal" runat="server"></asp:TextBox>Ԫ
            </td>

        </tr>


        <tr>


            <td>��ʼʱ�䣺
            </td>
            <td colspan="5">



                <asp:TextBox ID="txtBusFromTime" runat="server" Width="220px"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtBusFromTime" PopupButtonID="ImageButton1">
                </cc1:CalendarExtender>
                -<asp:TextBox ID="txtBusToTime" Width="220px"
                    runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtBusToTime" PopupButtonID="ImageButton3">
                </cc1:CalendarExtender>





            </td>
            <td colspan="2">
                <asp:CheckBox ID="cbIfTexi" runat="server" Text="���" /><asp:CheckBox ID="cbIfBus" runat="server" Style="margin-left: 20px" Text="����" />
            </td>
        </tr>

        <tr>
            <td>��ע</td>
            <td colspan="7">
                <asp:TextBox ID="txtBusRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">������
             &nbsp; &nbsp; &nbsp;
          
            </td>
        </tr>


        <tr>

            <td>�ص㣺
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtRepastAddress" runat="server" Width="220px"></asp:TextBox>
            </td>
            <td>���
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtRepastTotal" runat="server"></asp:TextBox>Ԫ
            </td>

        </tr>


        <tr>


            <td>�����ߣ�
            </td>
            <td colspan="2">

                <asp:TextBox ID="txtRepastPers" runat="server" Width="220px"></asp:TextBox>


            </td>
            <td colspan="2">����������
         
            <asp:TextBox ID="txtRepastPerNum" runat="server" Width="50px"></asp:TextBox>
            </td>

            <td colspan="3">���ͣ�
         
            <asp:RadioButton ID="rdoRepastType1" runat="server" GroupName="RepastType" Text="������(ÿ���޶�30Ԫ)" />
                <asp:RadioButton ID="rdoRepastType2" runat="server" GroupName="RepastType" Text="�����ò�" />
            </td>
        </tr>


        <tr>
            <td>��ע</td>
            <td colspan="7">
                <asp:TextBox ID="txtRepastRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">ס�޷�
              &nbsp; &nbsp; &nbsp;
          
            </td>
        </tr>

        <tr>

            <td>�ص㣺
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtHotelAddress" runat="server" Width="220px"></asp:TextBox>
            </td>

            <td colspan="3">�Ƶ����ƣ�
       
            <asp:TextBox ID="txtHotelName" runat="server" Width="220px"></asp:TextBox>
            </td>
            <td>��
            </td>
            <td>
                <asp:TextBox ID="txtHotelTotal" runat="server"></asp:TextBox>Ԫ
            </td>

        </tr>


        <tr>


            <td>���ͣ�
            </td>
            <td colspan="7">

                <asp:RadioButton ID="cbHotelType1" runat="server" GroupName="HotelType" Text="��׼��" />
                <asp:RadioButton ID="cbHotelType2" runat="server" GroupName="HotelType" Text="���˼�" />
            </td>
        </tr>

        <tr>
            <td>��ע</td>
            <td colspan="7">
                <asp:TextBox ID="txtHotelRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">���Ͳ���&nbsp; &nbsp; &nbsp;
            </td>
        </tr>

        <tr>

            <td>�ص㣺
            </td>
            <td colspan="4">
                <asp:TextBox ID="txtOilFromAddress" runat="server" Width="220px"></asp:TextBox>��
                <asp:TextBox ID="txtOilToAddress" runat="server" Width="220px"></asp:TextBox>
            </td>

            <td>��̣� 
            <asp:TextBox ID="txtOilLiCheng" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td>��
            </td>
            <td>
                <asp:TextBox ID="txtOilTotal" runat="server"></asp:TextBox>Ԫ
            </td>

        </tr>

        <tr>
            <td>��ע</td>
            <td colspan="7">
                <asp:TextBox ID="txtOilRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">��·��
            
              ---
                &nbsp; &nbsp; &nbsp;
          
            </td>
        </tr>

        <tr>

            <td>�ص㣺
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtGuoBeginAddress" runat="server" Width="220px"></asp:TextBox>��
                <asp:TextBox ID="txtGuoToAddress" runat="server" Width="220px"></asp:TextBox>
            </td>


            <td>��
            </td>
            <td>
                <asp:TextBox ID="txtGuoTotal" runat="server"></asp:TextBox>Ԫ
            </td>

        </tr>



        <tr>
            <td>��ע</td>
            <td colspan="7">
                <asp:TextBox ID="txtGuoRemark" runat="server" Style="width: 95%" ForeColor="Red"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">�ʼķ�<asp:LinkButton
                ID="lbtnPostNo" runat="server" Style="color: Red;"
                OnClick="lbtnPostNo_Click"></asp:LinkButton>
                <asp:Label ID="lblPost_Id" runat="server" Visible="False"></asp:Label>


            </td>
        </tr>

        <tr>
            <td>�ʼı�ţ�</td>
            <td>

                <asp:TextBox ID="txtPostNo" runat="server"></asp:TextBox>
            </td>
            <td>��ݹ�˾��</td>
            <td>
                <asp:TextBox ID="txtPostCompany" runat="server"></asp:TextBox></td>


            <td>���ݣ�</td>
            <td>

                <asp:TextBox ID="txtPostContext" runat="server" Style="width: 95%"></asp:TextBox>
            </td>

            <td>�ļ��ˣ�
            </td>

            <td>
                <asp:TextBox ID="txtPostToPer" ReadOnly="true" runat="server"></asp:TextBox>
            </td>
        </tr>


        <tr>

            <td>
                <asp:CheckBox ID="cbPostFrom" runat="server" Text="�ĳ�" Style="margin-right: 10px" />
                ������
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPostFromAddress" runat="server" Width="220px"></asp:TextBox>
            </td>

            <td colspan="3">
                <asp:CheckBox ID="cbPostTo" runat="server" Text="����" Style="margin-right: 10px" />

                <asp:TextBox ID="txtPostToAddress" runat="server" Width="220px"></asp:TextBox>������
            </td>


            <td>��
            </td>
            <td>
                <asp:TextBox ID="txtPostTotal" runat="server"></asp:TextBox>Ԫ
            </td>

        </tr>


        <tr>
            <td>��ע</td>
            <td colspan="7">
                <asp:TextBox ID="txtPostRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>


        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">С��ɹ�---&nbsp;��ⵥ�ţ�<asp:LinkButton
                ID="lbtnCaiNo" runat="server" Style="color: Red;" OnClick="lbtnCaiNo_Click"></asp:LinkButton>
                <asp:Label ID="lblCaiID" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>


        <tr>

            <td>���ݣ�
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtPoContext" runat="server" Width="90%"></asp:TextBox>
            </td>


            <td>��
            </td>
            <td>
                <asp:TextBox ID="txtPoTotal" runat="server"></asp:TextBox>Ԫ
            </td>

        </tr>


        <tr>
            <td>��ע</td>
            <td colspan="7">
                <asp:TextBox ID="txtPoRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">��������
                &nbsp; &nbsp;
            </td>
        </tr>


        <tr>

            <td>���ݣ�
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtOtherContext" runat="server" Width="90%"></asp:TextBox>
            </td>


            <td>��
            </td>
            <td>
                <asp:TextBox ID="txtOtherTotal" runat="server"></asp:TextBox>Ԫ
            </td>

        </tr>


        <tr>
            <td>��ע</td>
            <td colspan="7">
                <asp:TextBox ID="txtOtherRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td colspan="8">
                <asp:Label ID="lblTotal" runat="server" Text="" Font-Size="14px" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="��һ��������:"></asp:Label>
            </td>
            <td colspan="8">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td colspan="8">


                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserName">
                    <asp:ListItem Selected="True">ͨ��</asp:ListItem>
                    <asp:ListItem>��ͨ��</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="�����������:"></asp:Label>
            </td>
            <td colspan="8">


                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="8" align="center">

                <asp:Button ID="btnSave" runat="server" Text="����" Visible="false" BackColor="Yellow"
                    Width="51px" OnClick="btnSet_Click" />

                <asp:Button ID="btnSub" runat="server" Text="�ύ" BackColor="Yellow"
                    OnClick="Button1_Click" Width="51px" />&nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" ���� " BackColor="Yellow"
                    OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>



</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>

