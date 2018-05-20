<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BorrowInvName.aspx.cs" Culture="auto" UICulture="auto" Inherits="VAN_OA.EFrom.BorrowInvName" MasterPageFile="~/DefaultMaster.Master" Title="仓库借货单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <script type="text/javascript">
        function show() {
            alert("1");
            document.getElementById("btnSub").disabled = false;

        }
    </script>
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF" border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">仓库借货单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label></td>
        </tr>

        <tr>

            <td>借货人：<font style="color: Red">*</font></td>
            <td>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td>借出日期：</td>
            <td>
                <asp:TextBox ID="txtBorrowTime" runat="server"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="Image1" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtBorrowTime">
                </cc1:CalendarExtender>

            </td>
            <td>归还日期：</td>
            <td>
                <asp:TextBox ID="txtBackTime" runat="server"></asp:TextBox>

                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server" Format="yyyy-MM-dd hh:mm:ss" TargetControlID="txtBackTime">
                </cc1:CalendarExtender>
            </td>


        </tr>
        <tr>
            <td>货物名称：<font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtInvName" runat="server" Width="95%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>借出事由：
             <font style="color: Red">*</font></td>
            <td colspan="6">
                <asp:TextBox ID="txtReason" runat="server" Width="95%" Height="100px" TextMode="MultiLine"></asp:TextBox>

            </td>


        </tr>




        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName" DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
            </td>
            <td colspan="6">


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
            <td colspan="6">


                <asp:TextBox ID="txtResultRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="btnFinSub" runat="server" Text="修改" BackColor="Yellow"
                    OnClick="btnFinSub_Click" Width="60px" />


                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
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

