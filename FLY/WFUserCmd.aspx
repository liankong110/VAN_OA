<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFUserCmd.aspx.cs" Inherits="VAN_OA.WFUserCmd"
    MasterPageFile="~/DefaultMaster.Master" Title="用户信息" %>

<%@ Import Namespace="System" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="SampleContent">
    <table cellpadding="0" cellspacing="0" width="70%" bordercolorlight="#999999" bordercolordark="#FFFFFF"
        border="1">
        <tr>
            <td colspan="3" style="height: 20px; background-color: #336699; color: White;">
                用户信息
            </td>
        </tr>
        <tr>
            <td>
                编号：
            </td>
            <td>
                <asp:TextBox ID="txtNo" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td rowspan="14" style="width: 300px; vertical-align: top">
                <asp:GridView ID="gvList" runat="server" BorderColor="#E5E5E5" BorderStyle="Solid"
                    DataKeyNames="RID" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvList_RowDataBound">
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr style="height: 20px; background-color: #336699; color: White;">
                                <td>
                                    编辑
                                </td>
                                <td>
                                    删除
                                </td>
                                <td>
                                    角色编码
                                </td>
                                <td>
                                    角色名称
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" align="center" style="height: 80%">
                                    ---暂无数据---
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderColor="#E5E5E5">
                            <ItemTemplate>
                                <asp:CheckBox ID="IfSelected" runat="server" Checked='<%# Eval("IfSelected") %>'
                                    Enabled='<%# SetEnable() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RoleCode" HeaderText="角色编码">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RoleName" HeaderText="角色名称">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#E5E5E5" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B2C3E1" Font-Bold="True" ForeColor="White" Font-Size="12px" />
                    <HeaderStyle CssClass="GV_header" BackColor="#336699" Height="24px" ForeColor="White"
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="InfoDetail2" BackColor="#FBFBFB" />
                    <RowStyle CssClass="InfoDetail1" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                用户账号：
            </td>
            <td>
                <asp:TextBox ID="txtUserid" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                用户名称：
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox><asp:CheckBox ID="cbIsSpecialUser"
                    runat="server" Text="AE显示" />
            </td>
        </tr>
        <tr>
            <td>
                用户密码：
            </td>
            <td>
                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                <asp:Label ID="lblPwd" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                状态：
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="155PX">
                    <asp:ListItem>在职</asp:ListItem>
                    <asp:ListItem>离职</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                性别：
            </td>
            <td>
                <asp:DropDownList ID="ddlSex" runat="server" Width="155PX">
                    <asp:ListItem>男</asp:ListItem>
                    <asp:ListItem>女</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                职务：
            </td>
            <td>
                <asp:TextBox ID="txtZhiwu" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                部门：
            </td>
            <td>
                <%--  <asp:TextBox ID="txtDeptment" runat="server"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlDeptment" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                所属上级：
            </td>
            <td>
                <asp:DropDownList ID="ddlReportTo" runat="server" DataValueField="Id" DataTextField="LoginName"
                    Width="155PX">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                电话：
            </td>
            <td>
                <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                手机号码：
            </td>
            <td>
                <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                身份证号码：
            </td>
            <td>
                <asp:TextBox ID="txtCardNO" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                社保编号：
            </td>
            <td>
                <asp:TextBox ID="txtCityNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                学历：
            </td>
            <td>
                <asp:DropDownList ID="ddlEducation" runat="server" Width="155PX">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>博士研究生</asp:ListItem>
                    <asp:ListItem>硕士研究生 </asp:ListItem>
                    <asp:ListItem>本科生</asp:ListItem>
                    <asp:ListItem>大专</asp:ListItem>
                    <asp:ListItem>高职或中专</asp:ListItem>
                    <asp:ListItem>高中</asp:ListItem>
                    <asp:ListItem>初中</asp:ListItem>
                    <asp:ListItem>小学及以下</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                毕业学校：
            </td>
            <td>
                <asp:TextBox ID="txtSchool" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                毕业时间：
            </td>
            <td>
                <asp:TextBox ID="txtSchoolDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                职称：
            </td>
            <td>
                <asp:DropDownList ID="txtTitle" runat="server" Width="155PX">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>高级职称</asp:ListItem>
                    <asp:ListItem>中级职称</asp:ListItem>
                    <asp:ListItem>初级职称</asp:ListItem>
                    <asp:ListItem>其他</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                政治面貌：
            </td>
            <td>
                <asp:DropDownList ID="ddlPolitical" runat="server" Width="155PX">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>党员</asp:ListItem>
                    <asp:ListItem>民主党派</asp:ListItem>
                    <asp:ListItem>群众</asp:ListItem>
                    <asp:ListItem>其他</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                家庭住址：
            </td>
            <td>
                <asp:TextBox ID="txtHomeAdd" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                参加工作时间：
            </td>
            <td>
                <asp:TextBox ID="txtWorkDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                E-Mail：
            </td>
            <td>
                <asp:TextBox ID="txtEMail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                建档时间：
            </td>
            <td>
                <asp:TextBox ID="txtCreateTime" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                公司名称：
            </td>
            <td>
                <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="ComName" DataValueField="ComCode"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>
          <tr>
            <td>
                社保单位：
            </td>
            <td>
                <asp:DropDownList ID="ddlSheBao" runat="server" DataTextField="ComName" DataValueField="ComCode"
                    Width="200PX">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAdd" runat="server" Text=" 添加 " BackColor="Yellow" OnClick="btnAdd_Click" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text=" 修改保存 " BackColor="Yellow" OnClick="btnUpdate_Click" />&nbsp;
                <%--  <asp:Button ID="btnSet" runat="server" Text=" 重置 " BackColor="Yellow" OnClick="btnSet_Click" />&nbsp;--%>
                <asp:Button ID="btnClose" runat="server" Text=" 关闭 " BackColor="Yellow" OnClick="btnClose_Click" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
