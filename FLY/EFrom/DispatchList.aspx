<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchList.aspx.cs" Inherits="VAN_OA.EFrom.DispatchList" MasterPageFile="~/DefaultMaster.Master" Title="预  期  报  销  单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">


    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
        <tr>
            <td colspan="8" style="height: 20px; background-color: #336699; color: White;">预期报销单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>

                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                    <asp:ListItem Value="0">普通报销</asp:ListItem>
                    <asp:ListItem Value="1">小额采购报销</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>事件发生日期：<font style="color: Red">*</font></td>
            <td>

                <asp:TextBox ID="txtEvTime" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtEvTime" PopupButtonID="Image1">
                </cc1:CalendarExtender>
            </td>
            <td>报销人：<font style="color: Red">*</font></td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox></td>


            <td>填写日期：<font style="color: Red">*</font></td>
            <td>

                <asp:TextBox ID="txtCreatime" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtCreatime" PopupButtonID="ImageButton2">
                </cc1:CalendarExtender>
            </td>

            <td>报销序列号：
            </td>

            <td>
                <asp:TextBox ID="txtCardNo" ReadOnly="true" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>

            <td>项目编码：<font style="color: Red">*</font></td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="lbtnSelNo1" runat="server"
                    OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOList.aspx?AE=1',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton2_Click">选择</asp:LinkButton>

                <asp:LinkButton ID="lbtnSelNo2" runat="server"
                    OnClientClick="javascript:window.showModalDialog('../JXC/DioCAI_OrderInHouseList.aspx',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="lbtnSelNo2_Click">选择</asp:LinkButton>
                AE:<asp:Label ID="lblAe" runat="server" Text=""></asp:Label>
            </td>

            <td>项目名称:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
            </td>

            <td colspan="4">客户名称：
         
             
                <asp:TextBox ID="txtSupplier" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>


            </td>
        </tr>


        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">公交费
          &nbsp; &nbsp; &nbsp;
          
                    
            </td>
        </tr>

        <tr>

            <td>地点：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtBusFromAddress" runat="server" Width="220px"></asp:TextBox>&nbsp;&nbsp;至&nbsp;&nbsp;<asp:TextBox ID="txtBusToAddress"
                    runat="server" Width="221px"></asp:TextBox>
            </td>
            <td>金额
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtBusTotal" runat="server"></asp:TextBox>元
            </td>

        </tr>


        <tr>


            <td>起始时间：
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
                <asp:CheckBox ID="cbIfTexi" runat="server" Text="打的" /><asp:CheckBox ID="cbIfBus" runat="server" Style="margin-left: 20px" Text="公交" />
            </td>
        </tr>

        <tr>
            <td>备注</td>
            <td colspan="7">
                <asp:TextBox ID="txtBusRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">餐饮费
             &nbsp; &nbsp; &nbsp;
          
            </td>
        </tr>


        <tr>

            <td>地点：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtRepastAddress" runat="server" Width="220px"></asp:TextBox>
            </td>
            <td>金额
            </td>
            <td colspan="1">
                <asp:TextBox ID="txtRepastTotal" runat="server"></asp:TextBox>元
            </td>

        </tr>


        <tr>


            <td>参与者：
            </td>
            <td colspan="2">

                <asp:TextBox ID="txtRepastPers" runat="server" Width="220px"></asp:TextBox>


            </td>
            <td colspan="2">参与人数：
         
            <asp:TextBox ID="txtRepastPerNum" runat="server" Width="50px"></asp:TextBox>
            </td>

            <td colspan="3">类型：
         
            <asp:RadioButton ID="rdoRepastType1" runat="server" GroupName="RepastType" Text="工作餐(每人限额30元)" />
                <asp:RadioButton ID="rdoRepastType2" runat="server" GroupName="RepastType" Text="商务用餐" />
            </td>
        </tr>


        <tr>
            <td>备注</td>
            <td colspan="7">
                <asp:TextBox ID="txtRepastRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">住宿费
              &nbsp; &nbsp; &nbsp;
          
            </td>
        </tr>

        <tr>

            <td>地点：
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtHotelAddress" runat="server" Width="220px"></asp:TextBox>
            </td>

            <td colspan="3">酒店名称：
       
            <asp:TextBox ID="txtHotelName" runat="server" Width="220px"></asp:TextBox>
            </td>
            <td>金额：
            </td>
            <td>
                <asp:TextBox ID="txtHotelTotal" runat="server"></asp:TextBox>元
            </td>

        </tr>


        <tr>


            <td>类型：
            </td>
            <td colspan="7">

                <asp:RadioButton ID="cbHotelType1" runat="server" GroupName="HotelType" Text="标准间" />
                <asp:RadioButton ID="cbHotelType2" runat="server" GroupName="HotelType" Text="单人间" />
            </td>
        </tr>

        <tr>
            <td>备注</td>
            <td colspan="7">
                <asp:TextBox ID="txtHotelRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">汽油补贴&nbsp; &nbsp; &nbsp;
            </td>
        </tr>

        <tr>

            <td>地点：
            </td>
            <td colspan="4">
                <asp:TextBox ID="txtOilFromAddress" runat="server" Width="220px"></asp:TextBox>至
                <asp:TextBox ID="txtOilToAddress" runat="server" Width="220px"></asp:TextBox>
            </td>

            <td>里程： 
            <asp:TextBox ID="txtOilLiCheng" runat="server" Width="100px"></asp:TextBox>
            </td>
            <td>金额：
            </td>
            <td>
                <asp:TextBox ID="txtOilTotal" runat="server"></asp:TextBox>元
            </td>

        </tr>

        <tr>
            <td>备注</td>
            <td colspan="7">
                <asp:TextBox ID="txtOilRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">过路费
            
              ---
                &nbsp; &nbsp; &nbsp;
          
            </td>
        </tr>

        <tr>

            <td>地点：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtGuoBeginAddress" runat="server" Width="220px"></asp:TextBox>至
                <asp:TextBox ID="txtGuoToAddress" runat="server" Width="220px"></asp:TextBox>
            </td>


            <td>金额：
            </td>
            <td>
                <asp:TextBox ID="txtGuoTotal" runat="server"></asp:TextBox>元
            </td>

        </tr>



        <tr>
            <td>备注</td>
            <td colspan="7">
                <asp:TextBox ID="txtGuoRemark" runat="server" Style="width: 95%" ForeColor="Red"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">邮寄费<asp:LinkButton
                ID="lbtnPostNo" runat="server" Style="color: Red;"
                OnClick="lbtnPostNo_Click"></asp:LinkButton>
                <asp:Label ID="lblPost_Id" runat="server" Visible="False"></asp:Label>


            </td>
        </tr>

        <tr>
            <td>邮寄编号：</td>
            <td>

                <asp:TextBox ID="txtPostNo" runat="server"></asp:TextBox>
            </td>
            <td>快递公司：</td>
            <td>
                <asp:TextBox ID="txtPostCompany" runat="server"></asp:TextBox></td>


            <td>内容：</td>
            <td>

                <asp:TextBox ID="txtPostContext" runat="server" Style="width: 95%"></asp:TextBox>
            </td>

            <td>寄件人：
            </td>

            <td>
                <asp:TextBox ID="txtPostToPer" ReadOnly="true" runat="server"></asp:TextBox>
            </td>
        </tr>


        <tr>

            <td>
                <asp:CheckBox ID="cbPostFrom" runat="server" Text="寄出" Style="margin-right: 10px" />
                苏州至
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtPostFromAddress" runat="server" Width="220px"></asp:TextBox>
            </td>

            <td colspan="3">
                <asp:CheckBox ID="cbPostTo" runat="server" Text="到付" Style="margin-right: 10px" />

                <asp:TextBox ID="txtPostToAddress" runat="server" Width="220px"></asp:TextBox>至苏州
            </td>


            <td>金额：
            </td>
            <td>
                <asp:TextBox ID="txtPostTotal" runat="server"></asp:TextBox>元
            </td>

        </tr>


        <tr>
            <td>备注</td>
            <td colspan="7">
                <asp:TextBox ID="txtPostRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>


        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">小额采购---&nbsp;入库单号：<asp:LinkButton
                ID="lbtnCaiNo" runat="server" Style="color: Red;" OnClick="lbtnCaiNo_Click"></asp:LinkButton>
                <asp:Label ID="lblCaiID" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>


        <tr>

            <td>内容：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtPoContext" runat="server" Width="90%"></asp:TextBox>
            </td>


            <td>金额：
            </td>
            <td>
                <asp:TextBox ID="txtPoTotal" runat="server"></asp:TextBox>元
            </td>

        </tr>


        <tr>
            <td>备注</td>
            <td colspan="7">
                <asp:TextBox ID="txtPoRemark" runat="server" Style="width: 95%"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="8" style="height: 20px; background-color: #D7E8FF;">其它费用
                &nbsp; &nbsp;
            </td>
        </tr>


        <tr>

            <td>内容：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtOtherContext" runat="server" Width="90%"></asp:TextBox>
            </td>


            <td>金额：
            </td>
            <td>
                <asp:TextBox ID="txtOtherTotal" runat="server"></asp:TextBox>元
            </td>

        </tr>


        <tr>
            <td>备注</td>
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
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="8">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
            </td>
            <td colspan="8">


                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserName">
                    <asp:ListItem Selected="True">通过</asp:ListItem>
                    <asp:ListItem>不通过</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="lblYiJian" runat="server" Text="本次审批意见:"></asp:Label>
            </td>
            <td colspan="8">


                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="8" align="center">

                <asp:Button ID="btnSave" runat="server" Text="保存" Visible="false" BackColor="Yellow"
                    Width="51px" OnClick="btnSet_Click" />

                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow"
                    OnClick="Button1_Click" Width="51px" />&nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow"
                    OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>



</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>

