<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dispatching.aspx.cs" Culture="auto"
    UICulture="auto" Inherits="VAN_OA.EFrom.Dispatching" MasterPageFile="~/DefaultMaster.Master"
    Title="派工单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="100%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="6" style="height: 20px; background-color: #336699; color: White;">外出派工单-<asp:Label ID="lblProNo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>派工人：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtName" ReadOnly="true" runat="server"></asp:TextBox>
            </td>
            <td>派工日期：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtDisDate" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="Image1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtDisDate">
                </cc1:CalendarExtender>
            </td>
            <td>拟派工日期：<font style="color: Red">*</font></td>
            <td>
                <asp:TextBox ID="txtNiDate" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton1" runat="server"
                    Format="yyyy-MM-dd" TargetControlID="txtNiDate">
                </cc1:CalendarExtender>
            </td>
        </tr>

        <tr>
            <td>项目编号:<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtPONo" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:LinkButton ID="lbtnSelectPONo" runat="server" OnClientClick="javascript:window.showModalDialog('../JXC/DioCommPOList.aspx?AE=1',null,'dialogWidth:700px;dialogHeight:450px;help:no;status:no')"
                    ForeColor="Red" OnClick="LinkButton1_Click1">
        选择</asp:LinkButton>
            </td>
            <td>项目名称:
            </td>
            <td>
                <asp:TextBox ID="txtPOName" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
            <td>项目类别：
            </td>
            <td>
                <asp:TextBox ID="txtPOType" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>客户类型:
            </td>
            <td>
                <asp:TextBox ID="txtGuestType" runat="server" ReadOnly="true"></asp:TextBox>

            </td>
            <td>客户属性:
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtGuestPro" runat="server" ReadOnly="true"></asp:TextBox></td>
        </tr>

        <tr>
            <td>被派工人： <font style="color: Red">*</font>
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" DataTextField="LoginName" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>随同人:
            </td>
            <td>
                <asp:TextBox ID="txtSuiTongRen" runat="server"></asp:TextBox>
            </td>

            <td>拟派工时间： <font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtNiHours" runat="server"></asp:TextBox>小时
            </td>
        </tr>
        <tr>
            <td>客户名称： <font style="color: Red">*</font>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtGueName" runat="server" Width="95%" ReadOnly="true"></asp:TextBox>
            </td>
            <td>联系电话：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>具体地址： <font style="color: Red">*</font>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtAddress" runat="server" Width="95%"></asp:TextBox>
            </td>
            <td>联系人：<font style="color: Red">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtContacter" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>外派出去时间：<font style="color:blue">*</font>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtGoDate" runat="server"></asp:TextBox>
                <asp:ImageButton ID="imggoTime" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    TargetControlID="txtGoDate" PopupButtonID="imggoTime">
                </cc1:CalendarExtender>
            </td>
            <td>外派回来时间：<font style="color:blue">*</font>
            </td>
            <td>
                <asp:TextBox ID="txtBackDate" runat="server"></asp:TextBox>
                <asp:ImageButton ID="imgendTime" runat="server" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd hh:mm:ss"
                    TargetControlID="txtBackDate" PopupButtonID="imgendTime">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>工作描述及故障现象： <font style="color: Red">*</font>
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtQuestion" runat="server" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>故障问题分析：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtQuestionRemark" runat="server" Height="100px" Width="99%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>考核评分：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtMyValue" Enabled="false" runat="server"></asp:TextBox>
                项目类别考核系数:<asp:Label ID="lblBaseSkillValue" runat="server" Text=""></asp:Label>
            </td>

        </tr>
        <tr>
            <td>技能考核系数：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtMyXiShu" Enabled="false" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="7">
                <div align="left" style="width: 100%;">
                    <asp:LinkButton ID="lblAttName" runat="server" OnClick="lblAttName_Click" ForeColor="Red"></asp:LinkButton>
                    <asp:Label ID="lblAttName_Vis" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <br />
                <asp:FileUpload ID="fuAttach" runat="server" Visible="false" Width="400px" />
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
                <asp:Button ID="btnUpdate" runat="server" Text="修改" BackColor="Yellow"  Visible="false" OnClick="btnUpdate_Click" />
               
                &nbsp;
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnEdit" runat="server" Text="修改->保存" BackColor="Yellow" OnClick="btnEdit_Click" />&nbsp;
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnSub" runat="server" Text="提交" BackColor="Yellow" OnClick="Button1_Click"
                    Width="51px" OnClientClick="return confirm('确定要提交吗？')"/>&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnClose" runat="server" Text=" 返回 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
</asp:Content>
