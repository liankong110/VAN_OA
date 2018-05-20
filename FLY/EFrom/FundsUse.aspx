<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FundsUse.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.FundsUse" MasterPageFile="~/DefaultMaster.Master"
    Title="请款单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">

    <script type="text/javascript">
         function RuKu() {             
                var url="../JXC/DioCAI_OrderInHouseList.aspx?Funds=true&PoNo="+document.getElementById('<%= txtPONo.ClientID %>').value;
               
             window.showModalDialog(url, null, 'dialogWidth:700px;dialogHeight:450px;help:no;status:no');
         }
         function ChuKu() 
         {
         var url="../JXC/DioXiao_OrderOutHouseList.aspx?Funds=true&PoNo="+document.getElementById('<%= txtPONo.ClientID %>').value;
             window.showModalDialog(url, null, 'dialogWidth:700px;dialogHeight:450px;help:no;status:no')
         }
         
    </script>

    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">
                请款单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                申请部门：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtDepartName" runat="server" Style="margin-left: 11px" Width="200px"></asp:TextBox>
            </td>
            <td>
                姓名：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtName" ReadOnly="true" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                日期：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtDateTime" runat="server" Width="200px"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    TargetControlID="txtDateTime" PopupButtonID="Image1">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                客户名称：
            </td>
            <td>
                <asp:TextBox ID="txtSupplier" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
            </td>
            <td>
                项目名称:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
            </td>
            <td>
                项目编码：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOList.aspx?AE=1',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton2_Click">选择</asp:LinkButton>
            </td>
        </tr>
        <tr>
        <td colspan="6">       
         <asp:Panel ID="Panel6" runat="server">
            <asp:RadioButton ID="tBtnRenGong" runat="server" Text="人工费" GroupName="b"  
                Checked="true" AutoPostBack="True" 
                oncheckedchanged="tBtnRenGong_CheckedChanged" />
            <asp:RadioButton ID="tBtnHuiWu" runat="server" Text="会务费" GroupName="b" 
                AutoPostBack="True" oncheckedchanged="tBtnHuiWu_CheckedChanged"  />
            <asp:RadioButton ID="tBtnXingZheng" runat="server" Text="行政采购" GroupName="b" 
                AutoPostBack="True" oncheckedchanged="tBtnXingZheng_CheckedChanged"  />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请款概要说明: <font style="color: Red">*</font>
              <asp:TextBox ID="txtShuoMing" runat="server"  Width="60%"></asp:TextBox>
             费用人:<asp:DropDownList ID="ddlTeamInfo"  runat="server" DataTextField="TeamLever" DataValueField="TeamLever"></asp:DropDownList>
                </asp:Panel>
              
        </td>
       
        </tr>
        <tr>
            <td>
                款项用途： <font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtUseTo" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                发票号：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtInvoce" runat="server" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                入库单号：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtHouseNo" runat="server" Width="90%" Enabled="false"></asp:TextBox>
                <asp:LinkButton OnClientClick="RuKu()" ID="ltbnRuku" runat="server" ForeColor="Red"
                    OnClick="ltbnRuku_Click">选择</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                出库单号：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtExpNO" runat="server" Width="90%" Enabled="false"></asp:TextBox>
                <asp:LinkButton OnClientClick="ChuKu()" ID="lbtnChuKu" runat="server" ForeColor="Red"
                    OnClick="lbtnChuKu_Click">选择</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                款项支付 <font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:Panel ID="Panel1" runat="server">
                    <asp:RadioButton ID="rdoXianJin" runat="server" Text="现金" GroupName="a" />
                    <asp:RadioButton ID="rdoZhip" runat="server" Text="支票" GroupName="a" />
                    <asp:RadioButton ID="rdoZhuanZhang" runat="server" Text="转账" GroupName="a" />
                    <asp:RadioButton ID="rdoYueJie" runat="server" Text="月结" GroupName="a" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="background-color: #336699; color: White;">
                项目采购金额（小写）：<font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:Panel ID="Panel2" runat="server">
                    <asp:TextBox ID="txtTotal" runat="server" Width="200px" AutoPostBack="true" Enabled="false" OnTextChanged="txtTotal_TextChanged"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;X
                    <asp:Label ID="lblXiShu1" runat="server" Text="0"></asp:Label>=
                    <asp:Label ID="lblCAITotal" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                        Font-Bold="true"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlShuiNo1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlShuiNo1_SelectedIndexChanged"
                        DataValueField="FpType" DataTextField="FpType">
                        <asp:ListItem>含VAT税</asp:ListItem>
                        <asp:ListItem>含LT税</asp:ListItem>
                        <asp:ListItem>不含税a</asp:ListItem>
                        <asp:ListItem>不含税b</asp:ListItem>
                        <asp:ListItem>不含税c</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                项目采购金额（大写）：
            </td>
            <td colspan="5">
                <asp:Label ID="lblTotalDa" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                    Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:FileUpload ID="fuAttach" runat="server" Width="95%" />
                <asp:LinkButton ID="lblAttName" runat="server" OnClick="lblAttName_Click" ForeColor="Red"></asp:LinkButton>
                <asp:Label ID="lblAttName_Vis" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color: #336699; color: White;">
                行政采购金额：
            </td>
            <td colspan="5">
                <asp:Panel ID="Panel5" runat="server">
                    <asp:TextBox ID="txtXingCai" Enabled="false" runat="server" Width="200px" AutoPostBack="True" OnTextChanged="txtTotal_TextChanged"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;X
                    <asp:Label ID="lblXiShuXing" runat="server" Text="0" />=
                    <asp:Label ID="lblXingCaiTotal" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                        Font-Bold="true"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlXingCaiXS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlXingCaiXS_SelectedIndexChanged"
                        DataValueField="FpType" DataTextField="FpType">
                        <asp:ListItem>含VAT税</asp:ListItem>
                        <asp:ListItem>含LT税</asp:ListItem>
                        <asp:ListItem>不含税a</asp:ListItem>
                        <asp:ListItem>不含税b</asp:ListItem>
                        <asp:ListItem>不含税c</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                行政采购金额（大写）：
            </td>
            <td colspan="5">
                <asp:Label ID="lblXingCaiTotalDa" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                    Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color: #336699; color: White;">
                会务费：
            </td>
            <td colspan="5">
                <asp:Panel ID="Panel3" runat="server">
                    <asp:TextBox ID="txtHuiWu" Enabled="false" runat="server" Width="200px" AutoPostBack="True" OnTextChanged="txtTotal_TextChanged"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;X
                    <asp:Label ID="lblXiShu2" runat="server" Text="0" />=
                    <asp:Label ID="lblHuiTotal" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                        Font-Bold="true"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlShuiNo2" runat="server" OnSelectedIndexChanged="ddlShuiNo2_SelectedIndexChanged"
                        DataValueField="FpType" DataTextField="FpType" AutoPostBack="True">
                        <asp:ListItem>含VAT税</asp:ListItem>
                        <asp:ListItem>含LT税</asp:ListItem>
                        <asp:ListItem>不含税a</asp:ListItem>
                        <asp:ListItem>不含税b</asp:ListItem>
                        <asp:ListItem>不含税c</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                会务费（大写）：
            </td>
            <td colspan="5">
                <asp:Label ID="lblHuiTotalDa" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                    Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color: #336699; color: White;" class="auto-style1">
                人工费：
            </td>
            <td colspan="5" class="auto-style1">
                <asp:Panel ID="Panel4" runat="server">
                    <asp:TextBox ID="txtRen" runat="server" Width="200px" AutoPostBack="True" OnTextChanged="txtTotal_TextChanged"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;X
                    <asp:Label ID="lblXiShu3" runat="server" Text="0" />=
                    <asp:Label ID="lblRenTotal" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                        Font-Bold="true"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlShuiNo3" runat="server" OnSelectedIndexChanged="ddlShuiNo3_SelectedIndexChanged"
                        DataValueField="FpType" DataTextField="FpType" AutoPostBack="True">
                        <asp:ListItem>含VAT税</asp:ListItem>
                        <asp:ListItem>含LT税</asp:ListItem>
                        <asp:ListItem>不含税a</asp:ListItem>
                        <asp:ListItem>不含税b</asp:ListItem>
                        <asp:ListItem>不含税c</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                人工费（大写）：
            </td>
            <td colspan="5">
                <asp:Label ID="lblRenTotalDa" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                    Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color: #336699; color: White;">
                实际请款金额：
            </td>
            <td colspan="5">
                <asp:Label ID="lblTrueTotal" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                    Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color: #336699; color: White;">
                总金额（小写）：
            </td>
            <td colspan="2">
                <asp:Label ID="lblALLTotal" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                    Font-Bold="true"></asp:Label>
            </td>
            <td style="background-color: #336699; color: White;">
                总金额（大写）：
            </td>
            <td colspan="2">
                <asp:Label ID="lblALLTotalDa" runat="server" Text="0" Font-Size="16px" ForeColor="Red"
                    Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="审批意见:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:TextBox ID="txtIdea" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPer" runat="server" Text="下一步审批人:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlPers" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server" Text="本次审批结果:"></asp:Label>
            </td>
            <td colspan="6">
                <asp:DropDownList ID="ddlResult" runat="server" Width="300px" DataTextField="UserName"
                    DataValueField="UserName">
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
              <%--  <asp:Button ID="btnReSubEdit" runat="server" Text="再次编辑" BackColor="Yellow" OnClick="btnReSubEdit_Click" />&nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp;--%>

                <asp:Button ID="btnSave" runat="server" Text="保存" Visible="false" BackColor="Yellow" 
                    Width="51px" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnPrint" runat="server" Text="打印预览" BackColor="Yellow" OnClick="btnPrint_Click" />&nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }
    </style>
</asp:Content>

