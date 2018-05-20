using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using System.Collections.Generic;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.EFrom
{
    public partial class FundsUse : System.Web.UI.Page
    {


        //double A = 1;
        //double B = 1.07;
        //double C = 1.17;
        //double D = 1.33;
        private A_RoleService roleSer = new A_RoleService();

        protected void btnClose_Click(object sender, EventArgs e)
        {

            if (Session["backurl"] != null)
            {
                base.Response.Redirect("~" + Session["backurl"]);
            }

        }

        protected void btnSet_Click(object sender, EventArgs e)
        {


        }



        public bool FormCheck()
        {
            #region 设置自己要判断的信息
            if (txtDepartName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写部门！');</script>");
                txtDepartName.Focus();

                return false;
            }
            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写姓名！');</script>");
                txtName.Focus();

                return false;
            }

            if (txtDateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                txtDateTime.Focus();

                return false;
            }
            else
            {

                if (CommHelp.VerifesToDateTime(txtDateTime.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return false;
                }
                
            }

            if (txtPONo.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择项目信息！');</script>");
                txtPONo.Focus();

                return false;
            }

            if (txtShuoMing.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写请款概要说明！');</script>");
                txtShuoMing.Focus();

                return false;
            }

            if (tBtnRenGong.Checked && ddlTeamInfo.Text == "选择"&& tBtnHuiWu.Checked==false&& tBtnXingZheng.Checked==false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择费用人！');</script>");
                ddlTeamInfo.Focus();

                return false;

            }
          
            if (txtTotal.Text != "")
            {
                if (CommHelp.VerifesToNum(txtTotal.Text.Trim()) == false)
                {
                    lblTotalDa.Text = "0";
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('采购金额 格式错误！');</script>");
                    return false;
                }
            }
           

            try
            {
                if (txtHuiWu.Text != "")
                {
                    double total = Convert.ToDouble(txtHuiWu.Text);
                }

            }
            catch (Exception)
            {
                lblHuiTotalDa.Text = "0";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的会务金额格式有误！');</script>");
            }



            try
            {

                if (txtRen.Text != "")
                {

                    double total = Convert.ToDouble(txtRen.Text);

                }

            }
            catch (Exception)
            {
                lblHuiTotalDa.Text = "0";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的人工金额格式有误！');</script>");
            }



            //if (txtTotal.Text.Trim() == "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写用款金额！');</script>");
            //    txtTotal.Focus();

            //    return false;
            //}
            //else
            //{
            //    try
            //    {
            //        Convert.ToDecimal(txtTotal.Text);
            //    }
            //    catch (Exception)
            //    {

            //        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的金额格式有误！');</script>");
            //        txtDateTime.Focus();

            //        return false;
            //    }
            //}
            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }


            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtName.Focus();

                return false;
            }
            #endregion

            if (txtHuiWu.Text != "" && Convert.ToDecimal(txtHuiWu.Text) > 0)
            {
                string sqlCheck = string.Format("select count(*) from FpTaxInfo where TAX={0} and FpType='{1}'", lblXiShu2.Text, ddlShuiNo2.Text);
                if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) != 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('会务费中发票类型和发票税收点数在档案中不存在！');</script>");
                    return false;
                }
            }

            if (txtRen.Text != "" && Convert.ToDecimal(txtRen.Text) > 0)
            {
                string sqlCheck = string.Format("select count(*) from FpTaxInfo where TAX={0} and FpType='{1}'", lblXiShu3.Text, ddlShuiNo3.Text);
                if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) != 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('人工费中发票类型和发票税收点数在档案中不存在！');</script>");
                    return false;
                }
            }


            if (txtXingCai.Text != "" && Convert.ToDecimal(txtXingCai.Text) > 0)
            {
                string sqlCheck = string.Format("select count(*) from FpTaxInfo where TAX={0} and FpType='{1}'", lblXiShuXing.Text, ddlXingCaiXS.Text);
                if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) != 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('行政采购金额中发票类型和发票税收点数在档案中不存在！');</script>");
                    return false;
                }
            }




            if (txtTotal.Text != "" && Convert.ToDecimal(txtTotal.Text) > 0)
            {
                string sqlCheck = string.Format("select count(*) from FpTaxInfo where TAX={0} and FpType='{1}'", lblXiShu1.Text, ddlShuiNo1.Text);
                if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) != 1)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目采购金额中发票类型和发票税收点数在档案中不存在！');</script>");
                    return false;
                }
            }

            //if (new CG_POOrderService().ExistPONO(txtPONo.Text) == false)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
            //    return false;
            //}
            if (CG_POOrderService.IsClosePONO(txtPONo.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                        "<script>alert('此项目已经关闭！');</script>");
                return false;
            }
            return true;
        }



        private void setEnable(bool result)
        {
            txtDepartName.ReadOnly = true;
            //txtName.ReadOnly = true;
            txtDateTime.ReadOnly = true;
            Image1.Enabled = false;

            txtTotal.ReadOnly = !result;
            txtUseTo.ReadOnly = !result;
            Panel1.Enabled = result;


            txtInvoce.ReadOnly = !result;
            txtHouseNo.ReadOnly = !result;
            txtIdea.ReadOnly = !result;
            txtExpNO.ReadOnly = !result;


            txtXingCai.ReadOnly = !result;
            txtRen.ReadOnly = !result;
            txtHuiWu.ReadOnly = !result;
            Panel1.Enabled = result;
            Panel2.Enabled = result;
            Panel3.Enabled = result;
            Panel4.Enabled = result;
            Panel5.Enabled = result;
            Panel6.Enabled = result;


            ddlShuiNo1.Enabled = result;
            ddlShuiNo2.Enabled = result;
            ddlShuiNo3.Enabled = result;
            ddlXingCaiXS.Enabled = result;

            LinkButton2.Visible = false;

            lbtnChuKu.Visible = false;
            ltbnRuku.Visible = false;
        }

        private static void SetDllDataSource(DropDownList ddl, List<FpTaxInfo> gooQGooddList)
        {
            ddl.DataSource = gooQGooddList;
            ddl.DataBind();
            ddl.DataTextField = "FpType";
            ddl.DataValueField = "FpType";

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                var fpTypeBaseInfoService = new FpTaxInfoService();
                //加载默认数据            
                List<FpTaxInfo> gooQGooddList = fpTypeBaseInfoService.GetListArray("");
                gooQGooddList.Insert(0, new FpTaxInfo { FpType = "" });
                SetDllDataSource(ddlShuiNo1, gooQGooddList);
                SetDllDataSource(ddlXingCaiXS, gooQGooddList);
                SetDllDataSource(ddlShuiNo2, gooQGooddList);
                SetDllDataSource(ddlShuiNo3, gooQGooddList);
                var teamList = new TeamInfoService().GetListArray("");
                teamList.Insert(0, new TeamInfo { TeamLever = "选择" });
                ddlTeamInfo.DataSource = teamList;
                ddlTeamInfo.DataBind();
                ddlTeamInfo.DataValueField = "TeamLever";
                ddlTeamInfo.DataTextField = "TeamLever";
                btnPrint.Visible = false;
                //再次编辑
                //btnReSubEdit.Visible = false;

                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;

                    txtDepartName.Text = use.LoginIPosition;
                    fuAttach.Visible = false;
                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtDateTime.Text = DateTime.Now.ToString();

                        fuAttach.Visible = true;

                        rdoXianJin.Checked = true;

                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {
                            rdoXianJin.Checked = true;

                            //获取审批人

                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

                            ViewState["ids"] = ids;
                            if (roleUserList != null)
                            {
                                //从获取出的审核中 获取上级信息
                                List<A_Role_User> newList = new List<A_Role_User>();
                                for (int i = 0; i < roleUserList.Count; i++)
                                {
                                    if (roleUserList[i].UserId == use.ReportTo)
                                    {
                                        A_Role_User a = roleUserList[i];
                                        newList.Add(a);
                                        break;
                                    }
                                }

                                if (newList.Count > 0)
                                {
                                    ddlPers.DataSource = newList;
                                }
                                else
                                {
                                    ddlPers.DataSource = roleUserList;
                                }
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";
                            }
                            else
                            {
                                lblPer.Visible = false;
                                ddlPers.Visible = false;
                            }
                        }
                        else
                        {
                            lblPer.Visible = false;
                            ddlPers.Visible = false;


                        }
                    }
                    else if (Request["ReEdit"] != null)//再次编辑
                    {

                        fuAttach.Visible = true;


                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人
                            int pro_IDs = 0;
                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

                            ViewState["ids"] = ids;
                            if (roleUserList != null)
                            {

                                ddlPers.DataSource = roleUserList;
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";

                            }
                            else
                            {
                                lblPer.Visible = false;
                                ddlPers.Visible = false;
                            }
                        }
                        else
                        {
                            lblPer.Visible = false;
                            ddlPers.Visible = false;


                        }


                        #region  加载 请假单数据
                        tb_FundsUseService fundsSer = new tb_FundsUseService();

                        tb_FundsUse fundsModel = fundsSer.GetModel(Convert.ToInt32(Request["allE_id"]));

                        ddlTeamInfo.Text = fundsModel.MyTeamLever;
                        txtShuoMing.Text = fundsModel.ShuoMing;
                        // txtDepartName.Text = fundsModel.DepartName;
                        txtDateTime.Text = DateTime.Now.ToShortDateString();
                        // txtName.Text = fundsModel.LoginName;
                        txtTotal.Text = fundsModel.total.ToString();
                        txtUseTo.Text = fundsModel.useTo;
                        if (fundsModel.type == "现金")
                        {
                            rdoXianJin.Checked = true;
                        }
                        if (fundsModel.type == "支票")
                        {
                            rdoZhip.Checked = true;
                        }
                        if (fundsModel.type == "转账")
                        {

                            rdoZhuanZhang.Checked = true;
                        }
                        else if (fundsModel.type == "月结")
                        {


                            rdoYueJie.Checked = true;
                        }

                        if (fundsModel.FundType == tBtnRenGong.Text)
                        {
                            tBtnRenGong.Checked = true;
                            txtXingCai.Enabled = false;
                            txtHuiWu.Enabled = false;
                            txtRen.Enabled = true;
                        }
                        if (fundsModel.FundType == tBtnHuiWu.Text)
                        {
                            tBtnHuiWu.Checked = true;
                            txtXingCai.Enabled = false;
                            txtHuiWu.Enabled = true;
                            txtRen.Enabled = false;
                        }
                        if (fundsModel.FundType == tBtnXingZheng.Text)
                        {
                            tBtnXingZheng.Checked = true;

                            txtXingCai.Enabled = true;
                            txtHuiWu.Enabled = false;
                            txtRen.Enabled = false;
                        }

                        if (fundsModel.fileName != null && fundsModel.fileName != "")
                        {
                            lblAttName.Visible = true;
                            lblAttName.Text = fundsModel.fileName;
                            lblAttName_Vis.Text = fundsModel.fileName.Substring(0, fundsModel.fileName.LastIndexOf('.')) + "_" + fundsModel.id + fundsModel.fileName.Substring(fundsModel.fileName.LastIndexOf('.'));
                        }

                        txtInvoce.Text = fundsModel.Invoce;
                        txtHouseNo.Text = fundsModel.HouseNo;
                        txtIdea.Text = fundsModel.Idea;
                        lblTotalDa.Text = ConvertMoney(fundsModel.total);
                        txtExpNO.Text = fundsModel.ExpNo;

                        // lblProNo.Text = fundsModel.ProNo;
                        txtPOName.Text = fundsModel.POName;
                        txtPONo.Text = fundsModel.PONo;
                        txtSupplier.Text = fundsModel.GuestName;

                        if (fundsModel.CAIXS != null)
                        {
                            lblXiShu1.Text = fundsModel.CAIXS.Value.ToString();

                        }

                        decimal allTotal = 0;
                        if (fundsModel.CAIXS != null)
                        {
                            lblXiShu1.Text = fundsModel.CAIXS.Value.ToString();
                            ddlShuiNo1.Text = tb_FundsUse.GetShui(fundsModel.CAIXS.Value);

                        }

                        if (fundsModel.CaiTotal != null)
                        {
                            lblCAITotal.Text = fundsModel.CaiTotal.ToString();
                            allTotal += fundsModel.CaiTotal.Value;
                            lblTotalDa.Text = ConvertMoney(Convert.ToDecimal(fundsModel.CaiTotal));


                        }


                        if (fundsModel.HuiXS != null)
                        {
                            lblXiShu2.Text = fundsModel.HuiXS.Value.ToString();
                            ddlShuiNo2.Text = tb_FundsUse.GetShui(fundsModel.HuiXS.Value);
                        }

                        if (fundsModel.Hui != null)
                        {
                            txtHuiWu.Text = fundsModel.Hui.ToString();

                        }

                        if (fundsModel.HuiTotal != null)
                        {
                            lblHuiTotal.Text = fundsModel.HuiTotal.ToString();
                            allTotal += fundsModel.HuiTotal.Value;

                            lblHuiTotalDa.Text = ConvertMoney(Convert.ToDecimal(fundsModel.HuiTotal));
                        }



                        if (fundsModel.RenXS != null)
                        {
                            lblXiShu3.Text = fundsModel.RenXS.Value.ToString();
                            ddlShuiNo3.Text = tb_FundsUse.GetShui(fundsModel.RenXS.Value);
                        }

                        if (fundsModel.Ren != null)
                        {
                            txtRen.Text = fundsModel.Ren.ToString();
                        }

                        if (fundsModel.RenTotal != null)
                        {
                            lblRenTotal.Text = fundsModel.RenTotal.ToString();
                            allTotal += fundsModel.RenTotal.Value;
                            lblRenTotalDa.Text = ConvertMoney(Convert.ToDecimal(fundsModel.RenTotal));
                        }

                        //lblTrueTotal.Text = (fundsModel.Ren == null ? 0 : fundsModel.Ren.Value + fundsModel.total + fundsModel.Hui == null ? 0 : fundsModel.Hui.Value).ToString();
                        //lblALLTotal.Text = allTotal.ToString();
                        //lblALLTotalDa.Text = ConvertMoney(Convert.ToDecimal(allTotal));



                        if (fundsModel.XingCaiXS != null)
                        {
                            lblXiShuXing.Text = fundsModel.XingCaiXS.Value.ToString();
                            ddlXingCaiXS.Text = tb_FundsUse.GetShui(fundsModel.XingCaiXS.Value);
                        }

                        if (fundsModel.XingCai != null)
                        {
                            txtXingCai.Text = fundsModel.XingCai.ToString();
                        }

                        if (fundsModel.XingCaiTotal != null)
                        {

                            lblXingCaiTotal.Text = fundsModel.XingCaiTotal.ToString();
                            allTotal += fundsModel.XingCaiTotal.Value;
                            lblXingCaiTotalDa.Text = ConvertMoney(Convert.ToDecimal(fundsModel.XingCaiTotal));
                        }

                        SetTotal();

                        if (Request["IsEdit"] != null)
                        {
                            Panel6.Enabled = true;
                            txtUseTo.ReadOnly = false;
                            btnSave.Visible = true;
                            if (tBtnRenGong.Checked)
                            {
                                ddlTeamInfo.Enabled = true;
                            }
                            else
                            {
                                ddlTeamInfo.Enabled = false;
                            }
                        }
                        #endregion

                    }
                    else//单据审批
                    {



                        //加载已经审批的数据
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        List<VAN_OA.Model.EFrom.tb_EForms> eforms = eformsSer.GetListArray(string.Format(" e_Id in (select id from tb_EForm where proId={0} and allE_id={1})",
                            Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                        if (eforms.Count > 0)
                        {
                            string mess = @"<table cellpadding='0' cellspacing='0' width='100%' bordercolorlight='#999999' bordercolordark='#FFFFFF' border='1' ><tr><td colspan='6' style=' height:20px; background-color:#336699; color:White;'>流程图</td></tr>";


                            for (int i = 0; i < eforms.Count; i++)
                            {
                                string per = "";
                                if (eforms[i].consignor != null && eforms[i].consignor != 0)
                                {
                                    per = eforms[i].Consignor_Name + "(委托人：" + eforms[i].Audper_Name + ")";
                                }
                                else
                                {
                                    per = eforms[i].Audper_Name;
                                }
                                mess += string.Format("<tr><td align='center'>第{0}步</td><td>序号{0}：{1}</td><td><span style='color:red;'>{2}</span>[<span style='color:blue;'>{3} {4}</span>]<br/>意见：{5}</td></tr>",
                                    i + 1, eforms[i].RoleName
, per, eforms[i].resultState, eforms[i].doTime, eforms[i].idea);
                            }
                            mess += "</table>";
                            lblMess.Text = mess;
                        }

                        #region  加载 请假单数据
                        tb_FundsUseService fundsSer = new tb_FundsUseService();

                        tb_FundsUse fundsModel = fundsSer.GetModel(Convert.ToInt32(Request["allE_id"]));

                        ddlTeamInfo.Text = fundsModel.MyTeamLever;
                        txtDepartName.Text = fundsModel.DepartName;
                        txtDateTime.Text = fundsModel.datetiem.ToString();
                        txtName.Text = fundsModel.LoginName;
                        txtTotal.Text = fundsModel.total.ToString();
                        txtUseTo.Text = fundsModel.useTo;
                        txtShuoMing.Text = fundsModel.ShuoMing;
                        if (fundsModel.type == "现金")
                        {
                            rdoXianJin.Checked = true;
                        }
                        if (fundsModel.type == "支票")
                        {
                            rdoZhip.Checked = true;
                        }
                        if (fundsModel.type == "转账")
                        {

                            rdoZhuanZhang.Checked = true;
                        }
                        else if (fundsModel.type == "月结")
                        {
                            rdoYueJie.Checked = true;
                        }

                        if (fundsModel.FundType == tBtnRenGong.Text)
                        {
                            tBtnRenGong.Checked = true;
                            txtXingCai.Enabled = false;
                            txtHuiWu.Enabled = false;
                            txtRen.Enabled = true;
                        }
                        if (fundsModel.FundType == tBtnHuiWu.Text)
                        {
                            tBtnHuiWu.Checked = true;
                            txtXingCai.Enabled = false;
                            txtHuiWu.Enabled = true;
                            txtRen.Enabled = false;
                        }
                        if (fundsModel.FundType == tBtnXingZheng.Text)
                        {
                            tBtnXingZheng.Checked = true;

                            txtXingCai.Enabled = true;
                            txtHuiWu.Enabled = false;
                            txtRen.Enabled = false;
                        }

                        if (fundsModel.fileName != null && fundsModel.fileName != "")
                        {
                            lblAttName.Visible = true;
                            lblAttName.Text = fundsModel.fileName;
                            lblAttName_Vis.Text = fundsModel.fileName.Substring(0, fundsModel.fileName.LastIndexOf('.')) + "_" + fundsModel.id + fundsModel.fileName.Substring(fundsModel.fileName.LastIndexOf('.'));
                        }

                        txtInvoce.Text = fundsModel.Invoce;
                        txtHouseNo.Text = fundsModel.HouseNo;
                        txtIdea.Text = fundsModel.Idea;
                        lblTotalDa.Text = ConvertMoney(fundsModel.total);
                        txtExpNO.Text = fundsModel.ExpNo;

                        lblProNo.Text = fundsModel.ProNo;


                        txtPOName.Text = fundsModel.POName;
                        txtPONo.Text = fundsModel.PONo;
                        txtSupplier.Text = fundsModel.GuestName;
                        if (fundsModel.CAIXS != null)
                        {
                            lblXiShu1.Text = fundsModel.CAIXS.Value.ToString();

                        }

                        decimal allTotal = 0;
                        if (fundsModel.CAIXS != null)
                        {
                            lblXiShu1.Text = fundsModel.CAIXS.Value.ToString();
                            ddlShuiNo1.Text = tb_FundsUse.GetShui(fundsModel.CAIXS.Value);

                        }

                        if (fundsModel.CaiTotal != null)
                        {
                            lblCAITotal.Text = fundsModel.CaiTotal.ToString();
                            allTotal += fundsModel.CaiTotal.Value;
                            lblTotalDa.Text = ConvertMoney(Convert.ToDecimal(fundsModel.CaiTotal));


                        }


                        if (fundsModel.HuiXS != null)
                        {
                            lblXiShu2.Text = fundsModel.HuiXS.Value.ToString();
                            ddlShuiNo2.Text = tb_FundsUse.GetShui(fundsModel.HuiXS.Value);
                        }

                        if (fundsModel.Hui != null)
                        {
                            txtHuiWu.Text = fundsModel.Hui.ToString();

                        }

                        if (fundsModel.HuiTotal != null)
                        {
                            lblHuiTotal.Text = fundsModel.HuiTotal.ToString();
                            allTotal += fundsModel.HuiTotal.Value;

                            lblHuiTotalDa.Text = ConvertMoney(Convert.ToDecimal(fundsModel.HuiTotal));
                        }



                        if (fundsModel.RenXS != null)
                        {
                            lblXiShu3.Text = fundsModel.RenXS.Value.ToString();
                            ddlShuiNo3.Text = tb_FundsUse.GetShui(fundsModel.RenXS.Value);
                        }

                        if (fundsModel.Ren != null)
                        {
                            txtRen.Text = fundsModel.Ren.ToString();
                        }

                        if (fundsModel.RenTotal != null)
                        {
                            lblRenTotal.Text = fundsModel.RenTotal.ToString();
                            allTotal += fundsModel.RenTotal.Value;
                            lblRenTotalDa.Text = ConvertMoney(Convert.ToDecimal(fundsModel.RenTotal));
                        }
                        //decimal sumTotal = fundsModel.Ren == null ? 0 : fundsModel.Ren.Value + fundsModel.total + fundsModel.Hui == null ? 0 : fundsModel.Hui.Value;

                        //lblTrueTotal.Text = sumTotal.ToString();

                        //lblALLTotal.Text = allTotal.ToString();
                        //lblALLTotalDa.Text = ConvertMoney(Convert.ToDecimal(allTotal));

                        if (fundsModel.XingCaiXS != null)
                        {
                            lblXiShuXing.Text = fundsModel.XingCaiXS.Value.ToString();
                            ddlXingCaiXS.Text = tb_FundsUse.GetShui(fundsModel.XingCaiXS.Value);
                        }

                        if (fundsModel.XingCai != null)
                        {
                            txtXingCai.Text = fundsModel.XingCai.ToString();
                        }

                        if (fundsModel.XingCaiTotal != null)
                        {

                            lblXingCaiTotal.Text = fundsModel.XingCaiTotal.ToString();
                            allTotal += fundsModel.XingCaiTotal.Value;
                            lblXingCaiTotalDa.Text = ConvertMoney(Convert.ToDecimal(fundsModel.XingCaiTotal));
                        }

                        SetTotal();


                        #endregion
                        //判断单据是否已经结束
                        if (eformSer.ifFinish(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                        {
                            btnSub.Visible = false;
                            lblPer.Visible = false;
                            ddlPers.Visible = false;
                            lblResult.Visible = false;
                            lblYiJian.Visible = false;
                            ddlResult.Visible = false;
                            txtResultRemark.Visible = false;





                            setEnable(false);

                            //打印信息
                            if (eformSer.GetState(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])) == "通过")
                            {
                                btnPrint.Visible = true;
                            }
                            //再次编辑
                            //btnReSubEdit.Visible = true;
                        }
                        else
                        {
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {

                                if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    lblPer.Visible = false;
                                    ddlPers.Visible = false;
                                }
                                else
                                {
                                    int ids = 0;

                                    List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);

                                    ViewState["ids"] = ids;
                                    if (roleUserList != null)
                                    {//从获取出的审核中 获取上级信息
                                        List<A_Role_User> newList = new List<A_Role_User>();
                                        for (int i = 0; i < roleUserList.Count; i++)
                                        {
                                            if (roleUserList[i].UserId == use.ReportTo)
                                            {
                                                A_Role_User a = roleUserList[i];
                                                newList.Add(a);
                                                break;
                                            }
                                        }

                                        if (newList.Count > 0)
                                        {
                                            ddlPers.DataSource = newList;
                                        }
                                        else
                                        {
                                            ddlPers.DataSource = roleUserList;
                                        }
                                        // ddlPers.DataSource = roleUserList;
                                        ddlPers.DataBind();
                                        ddlPers.DataTextField = "UserName";
                                        ddlPers.DataValueField = "UserId";
                                    }

                                }
                                setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));

                            }
                            else
                            {
                                //是否为代理人
                                if (eformSer.ifAudiPerByUserName(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    ViewState["ifConsignor"] = true;
                                    if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                    {
                                        lblPer.Visible = false;
                                        ddlPers.Visible = false;
                                    }
                                    else
                                    {
                                        int ids = 0;
                                        List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);
                                        ViewState["ids"] = ids;
                                        if (roleUserList != null)
                                        {
                                            //从获取出的审核中 获取上级信息
                                            List<A_Role_User> newList = new List<A_Role_User>();
                                            for (int i = 0; i < roleUserList.Count; i++)
                                            {
                                                if (roleUserList[i].UserId == use.ReportTo)
                                                {
                                                    A_Role_User a = roleUserList[i];
                                                    newList.Add(a);
                                                    break;
                                                }
                                            }

                                            if (newList.Count > 0)
                                            {
                                                ddlPers.DataSource = newList;
                                            }
                                            else
                                            {
                                                ddlPers.DataSource = roleUserList;
                                            }
                                            //ddlPers.DataSource = roleUserList;
                                            ddlPers.DataBind();
                                            ddlPers.DataTextField = "UserName";
                                            ddlPers.DataValueField = "UserId";
                                        }

                                    }
                                    setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                }
                                else
                                {
                                    btnSub.Visible = false;
                                    lblPer.Visible = false;
                                    ddlPers.Visible = false;

                                    lblResult.Visible = false;
                                    lblYiJian.Visible = false;
                                    ddlResult.Visible = false;
                                    txtResultRemark.Visible = false;
                                    setEnable(false);
                                }
                            }

                        }

                        if (Request["IsEdit"] != null)
                        {
                            Panel6.Enabled = true;
                            txtUseTo.ReadOnly = false;
                            btnSave.Visible = true;
                            if (tBtnRenGong.Checked)
                            {
                                ddlTeamInfo.Enabled = true;
                            }
                            else
                            {
                                ddlTeamInfo.Enabled = false;
                            }
                        }
                    }
                }

            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FormCheck())
            {

                btnSub.Enabled = false;
                if (base.Request["ProId"] != null)
                {
                    #region 获取单据基本信息

                    tb_FundsUse fundsInfo = new tb_FundsUse();
                    fundsInfo.appUserId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    fundsInfo.datetiem = Convert.ToDateTime(txtDateTime.Text);
                    if (txtTotal.Text != "")
                        fundsInfo.total = Convert.ToDecimal(txtTotal.Text);
                    fundsInfo.useTo = txtUseTo.Text;
                    fundsInfo.ShuoMing = txtShuoMing.Text;

                    if (ddlTeamInfo.Text != "选择")
                    {
                        fundsInfo.MyTeamLever = ddlTeamInfo.Text;
                    }
                    if (rdoXianJin.Checked)
                    {
                        fundsInfo.type = "现金";
                    }
                    else if (rdoYueJie.Checked)
                    {
                        fundsInfo.type = "月结";
                    }
                    else if (rdoZhip.Checked)
                    {
                        fundsInfo.type = "支票";
                    }
                    else if (rdoZhuanZhang.Checked)
                    {
                        fundsInfo.type = "转账";

                    }
                    if (tBtnRenGong.Checked)
                    {
                        fundsInfo.FundType = tBtnRenGong.Text;
                    }
                    if (tBtnHuiWu.Checked)
                    {
                        fundsInfo.FundType = tBtnHuiWu.Text;
                    }
                    if (tBtnXingZheng.Checked)
                    {
                        fundsInfo.FundType = tBtnXingZheng.Text;
                    }

                    fundsInfo.Invoce = txtInvoce.Text;
                    fundsInfo.HouseNo = txtHouseNo.Text;
                    fundsInfo.Idea = txtIdea.Text;

                    fundsInfo.ExpNo = txtExpNO.Text;

                    fundsInfo.POName = txtPOName.Text;
                    fundsInfo.PONo = txtPONo.Text;
                    fundsInfo.GuestName = txtSupplier.Text;


                   



                    if (txtHuiWu.Text != "")
                    {
                        fundsInfo.Hui = Convert.ToDecimal(txtHuiWu.Text);


                        fundsInfo.HuiXS = Convert.ToDecimal(lblXiShu2.Text);


                        // decimal huiTotal = fundsInfo.Hui.Value * fundsInfo.HuiXS.Value;
                        fundsInfo.HuiTotal = Convert.ToDecimal(fundsInfo.Hui.Value * fundsInfo.HuiXS.Value);
                        // allTotal += huiTotal;
                    }

                    if (txtRen.Text != "")
                    {
                        fundsInfo.Ren = Convert.ToDecimal(txtRen.Text);

                        //double huiTotal = 0;
                        //double total = Convert.ToDouble(txtRen.Text);
                        fundsInfo.RenXS = Convert.ToDecimal(lblXiShu3.Text);



                        fundsInfo.RenTotal = fundsInfo.Ren * fundsInfo.RenXS;
                        // allTotal += huiTotal;



                    }


                    if (txtXingCai.Text != "")
                    {
                        fundsInfo.XingCai = Convert.ToDecimal(txtXingCai.Text);

                        //double huiTotal = 0;
                        //double total = Convert.ToDouble(txtRen.Text);
                        fundsInfo.XingCaiXS = Convert.ToDecimal(lblXiShuXing.Text);



                        fundsInfo.XingCaiTotal = fundsInfo.XingCai * fundsInfo.XingCaiXS;
                        // allTotal += huiTotal;



                    }




                    if (txtTotal.Text != "")
                    {
                        // double caiTotal = 0;
                        double total = Convert.ToDouble(txtTotal.Text);
                        fundsInfo.CAIXS = Convert.ToDecimal(lblXiShu1.Text);

                        fundsInfo.total = Convert.ToDecimal(total);
                        fundsInfo.CaiTotal = fundsInfo.total * fundsInfo.CAIXS.Value;
                    }

                    #endregion
                    if (Request["allE_id"] == null || Request["ReEdit"] != null)//单据增加
                    {
                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

                        int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                        eform.appPer = userId;
                        eform.appTime = DateTime.Now;
                        eform.createPer = Convert.ToInt32(Session["currentUserId"].ToString());
                        eform.createTime = DateTime.Now;
                        eform.proId = Convert.ToInt32(Request["ProId"]);

                        if (ddlPers.Visible == false)
                        {
                            eform.state = "通过";
                            eform.toPer = 0;
                            eform.toProsId = 0;
                        }
                        else
                        {

                            eform.state = "执行中";
                            eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
                            eform.toProsId = Convert.ToInt32(ViewState["ids"]);
                        }




                        HttpFileCollection files = HttpContext.Current.Request.Files;
                        //查找是否有文件
                        string fileName, fileExtension;
                        fileExtension = "";
                        HttpPostedFile postedFile = null;
                        string file = "";
                        for (int iFile = 0; iFile < files.Count; iFile++)
                        {


                            ///'检查文件扩展名字
                            postedFile = files[iFile];

                            fileName = System.IO.Path.GetFileName(postedFile.FileName);
                            if (fileName != "")
                            {
                                fundsInfo.fileName = fileName;
                                fileExtension = System.IO.Path.GetExtension(fileName);
                                string fileType = postedFile.ContentType.ToString();//文件类型
                                fundsInfo.fileType = fileType;
                                System.IO.Stream streamFile = postedFile.InputStream;//建立数据流对象
                                int fileLength = postedFile.ContentLength;//文件长度以字节为单位
                                byte[] fileData = new Byte[fileLength];//新建一个数组
                                streamFile.Read(fileData, 0, fileLength);//将这个数据流读取到数组中
                                fundsInfo.fileNo = fileData;

                                file = System.IO.Path.GetFileNameWithoutExtension(fileName);

                            }
                        }

                        int MainId = 0;
                        tb_FundsUseService funsUse = new tb_FundsUseService();

                        if (Request["ReEdit"] != null && Request["allE_id"] != null && (fundsInfo.fileNo == null || fundsInfo.fileNo.Length == 0))
                        {
                            tb_FundsUse File = funsUse.GetModel_File(Convert.ToInt32(Request["allE_id"]));
                            if (File != null)
                            {
                                fundsInfo.fileName = File.fileName;
                                fundsInfo.fileNo = File.fileNo;
                                fundsInfo.fileType = File.fileType;
                            }
                        }

                        if (funsUse.addTran(fundsInfo, eform, out MainId) > 0)
                        {
                            //提交文件
                            if (MainId > 0)
                            {

                                if (fundsInfo.fileNo != null && fileExtension != "")
                                {
                                    string qizhui = System.Web.HttpContext.Current.Request.MapPath("Invoce/") + file + "_" + MainId;
                                    postedFile.SaveAs(qizhui + fileExtension);

                                }
                            }
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");

                            if (Session["backurl"] != null)
                            {
                                base.Response.Redirect("~" + Session["backurl"]);
                            }
                            else
                            {
                                base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
                            }
                        }
                        else
                        {
                            btnSub.Enabled = false;
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");
                        }
                    }

                    else//审核
                    {


                        

                        #region 本单据的ID
                        fundsInfo.id = Convert.ToInt32(Request["allE_id"]);
                        #endregion
                        tb_EForm eform = new tb_EForm();
                        tb_EForms forms = new tb_EForms();


                        eform.id = Convert.ToInt32(Request["EForm_Id"]);
                        eform.proId = Convert.ToInt32(Request["ProId"]);
                        eform.allE_id = Convert.ToInt32(Request["allE_id"]);
                        int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                        eform.appPer = userId; 

                        tb_EFormService fromSer = new tb_EFormService();
                        if (ViewState["ifConsignor"] != null && Convert.ToBoolean(ViewState["ifConsignor"]) == true)
                        {
                            forms.audPer = fromSer.getCurrentAuPer(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                            forms.consignor = Convert.ToInt32(Session["currentUserId"]);
                        }
                        else
                        {
                            forms.audPer = Convert.ToInt32(Session["currentUserId"]);
                            forms.consignor = 0;
                        }
                        if (Request["ReAudit"] == null)
                        {
                            if (fromSer.ifAudiPerAndCon(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])) == false)
                            { 
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('当前人不能审批，请返回！');</script>");
                                 
                                btnSave.Enabled = false;
                                return;
                            }
                        }

                        forms.doTime = DateTime.Now;
                        forms.e_Id = Convert.ToInt32(Request["EForm_Id"]); //fromSer.getCurrentid(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        forms.idea = txtResultRemark.Text;
                        forms.prosIds = fromSer.getCurrenttoProsId(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        forms.resultState = ddlResult.Text;
                        forms.RoleName = fromSer.getCurrenttoRoleName(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                        if (ddlPers.Visible == false)//说明为最后一次审核
                        {





                            eform.state = ddlResult.Text;
                            eform.toPer = 0;
                            eform.toProsId = 0;






                        }
                        else
                        {
                            if (ddlResult.Text == "不通过")
                            {

                                eform.state = ddlResult.Text;
                                eform.toPer = 0;
                                eform.toProsId = 0;




                            }
                            else
                            {

                                eform.state = "执行中";
                                eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
                                eform.toProsId = Convert.ToInt32(ViewState["ids"]);

                            }
                        }
                        tb_FundsUseService funsUse = new tb_FundsUseService();
                        if (funsUse.updateTran(fundsInfo, eform, forms))
                        {
                            // btnSub.Enabled = true;
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");
                            if (Session["backurl"] != null)
                            {
                                base.Response.Redirect("~" + Session["backurl"]);
                            }
                            else
                            {
                                base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
                            }
                        }
                        else
                        {
                            btnSub.Enabled = false;
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");

                        }
                    }
                }
            }
        }

        protected void txtForm_TextChanged(object sender, EventArgs e)
        {
            showTimeSpan();
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            showTimeSpan();
        }

        private void showTimeSpan()
        {
            try
            {
                //if (txtForm.Text != "" && txtTo.Text != "")
                //{
                //    TimeSpan ts = Convert.ToDateTime(txtTo.Text) - Convert.ToDateTime(txtForm.Text);
                //    lblTiemSpan.Text = ts.Days + "天" + ts.Hours + "小时";
                //}
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");


            }
        }

        protected void txtTotal_TextChanged(object sender, EventArgs e)
        {

            SetTotal();

        }

        private void SetTotal()
        {
            double caiTotal = 0;
            double renTotal = 0;
            double huiTotal = 0;
            double xingCaiTotal = 0;

            double caiTureTotal = 0;
            double renTrueTotal = 0;
            double huiTureTotal = 0;
            double xingCaiTrueTotal = 0;


            try
            {

                if (txtXingCai.Text != "")
                {
                    double total = Convert.ToDouble(txtXingCai.Text);
                    xingCaiTrueTotal = total;

                    xingCaiTotal = total * Convert.ToDouble(lblXiShuXing.Text);


                    lblXingCaiTotal.Text = xingCaiTotal.ToString();

                    lblXingCaiTotalDa.Text = ConvertMoney(Convert.ToDecimal(xingCaiTotal));
                }
            }
            catch (Exception)
            {
                lblXingCaiTotalDa.Text = "0";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的行政采购金额格式有误！');</script>");
            }



            try
            {
                if (txtTotal.Text != "")
                {
                    double total = Convert.ToDouble(txtTotal.Text);
                    caiTureTotal = total;
                    caiTotal = total * Convert.ToDouble(lblXiShu1.Text);

                    lblCAITotal.Text = caiTotal.ToString();
                    lblTotalDa.Text = ConvertMoney(Convert.ToDecimal(caiTotal));
                }
            }
            catch (Exception)
            {
                lblTotalDa.Text = "0";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的项目采购金额格式有误！');</script>");
            }

            try
            {
                if (txtHuiWu.Text != "")
                {
                    double total = Convert.ToDouble(txtHuiWu.Text);
                    huiTureTotal = total;
                    huiTotal = total * Convert.ToDouble(lblXiShu2.Text);


                    lblHuiTotal.Text = huiTotal.ToString();
                    lblHuiTotalDa.Text = ConvertMoney(Convert.ToDecimal(huiTotal));
                }

            }
            catch (Exception)
            {
                lblHuiTotalDa.Text = "0";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的会务金额格式有误！');</script>");
            }



            try
            {

                if (txtRen.Text != "")
                {

                    double total = Convert.ToDouble(txtRen.Text);
                    renTrueTotal = total;
                    renTotal = total * Convert.ToDouble(lblXiShu3.Text);
                    lblRenTotal.Text = renTotal.ToString();
                    lblRenTotalDa.Text = ConvertMoney(Convert.ToDecimal(renTotal));
                }

            }
            catch (Exception)
            {
                lblHuiTotalDa.Text = "0";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的人工金额格式有误！');</script>");
            }

            lblTrueTotal.Text = (caiTureTotal + renTrueTotal + huiTureTotal + xingCaiTrueTotal).ToString();
            double allTotal = huiTotal + renTotal + caiTotal + xingCaiTotal;
            lblALLTotal.Text = allTotal.ToString();
            lblALLTotalDa.Text = ConvertMoney(Convert.ToDecimal(allTotal));
        }


        #region 人民币小写金额转大写金额
        /// <summary>
        /// 小写金额转大写金额
        /// </summary>
        /// <param name="Money">接收需要转换的小写金额</param>
        /// <returns>返回大写金额</returns>
        public string ConvertMoney(Decimal Money)
        {
            //金额转换程序
            string MoneyNum = "";//记录小写金额字符串[输入参数]
            string MoneyStr = "";//记录大写金额字符串[输出参数]
            string BNumStr = "零壹贰叁肆伍陆柒捌玖";//模
            string UnitStr = "万仟佰拾亿仟佰拾万仟佰拾圆角分";//模

            MoneyNum = ((long)(Money * 100)).ToString();
            for (int i = 0; i < MoneyNum.Length; i++)
            {
                string DVar = "";//记录生成的单个字符(大写)
                string UnitVar = "";//记录截取的单位
                for (int n = 0; n < 10; n++)
                {
                    //对比后生成单个字符(大写)
                    if (Convert.ToInt32(MoneyNum.Substring(i, 1)) == n)
                    {
                        DVar = BNumStr.Substring(n, 1);//取出单个大写字符
                        //给生成的单个大写字符加单位
                        UnitVar = UnitStr.Substring(15 - (MoneyNum.Length)).Substring(i, 1);
                        n = 10;//退出循环
                    }
                }
                //生成大写金额字符串
                MoneyStr = MoneyStr + DVar + UnitVar;
            }
            //二次处理大写金额字符串
            MoneyStr = MoneyStr + "整";
            while (MoneyStr.Contains("零分") || MoneyStr.Contains("零角") || MoneyStr.Contains("零佰") || MoneyStr.Contains("零仟")
                || MoneyStr.Contains("零万") || MoneyStr.Contains("零亿") || MoneyStr.Contains("零零") || MoneyStr.Contains("零圆")
                || MoneyStr.Contains("亿万") || MoneyStr.Contains("零整") || MoneyStr.Contains("分整") || MoneyStr.Contains("角整"))
            {
                MoneyStr = MoneyStr.Replace("零分", "零");
                MoneyStr = MoneyStr.Replace("零角", "零");
                MoneyStr = MoneyStr.Replace("零拾", "零");
                MoneyStr = MoneyStr.Replace("零佰", "零");
                MoneyStr = MoneyStr.Replace("零仟", "零");
                MoneyStr = MoneyStr.Replace("零万", "万");
                MoneyStr = MoneyStr.Replace("零亿", "亿");
                MoneyStr = MoneyStr.Replace("亿万", "亿");
                MoneyStr = MoneyStr.Replace("零零", "零");
                MoneyStr = MoneyStr.Replace("零圆", "圆零");
                MoneyStr = MoneyStr.Replace("零整", "整");
                MoneyStr = MoneyStr.Replace("角整", "角");
                MoneyStr = MoneyStr.Replace("分整", "分");
            }
            if (MoneyStr == "整")
            {
                MoneyStr = "零元整";
            }
            return MoneyStr;
        }
        #endregion

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string url = string.Format("FundsUsePrint.aspx?ProId={0}&allE_id={1}", Request["ProId"], Request["allE_id"]);

            Response.Write(string.Format("<script>window.open('{0}','_blank')</script>", url));

        }
        protected void lblAttName_Click(object sender, EventArgs e)
        {
            string url = System.Web.HttpContext.Current.Request.MapPath("Invoce/") + lblAttName_Vis.Text;
            down1(lblAttName.Text, url);
        }

        private void down1(string fileName, string url)
        {
            if (System.IO.File.Exists(url) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");

                return;
            }
            string filePath = url;//路径

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 1024 * 500;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];

                Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                Response.Close();
            }


        }


        protected void btnReSubEdit_Click(object sender, EventArgs e)
        {
            if (Request["ProId"] != null && Request["allE_id"] != null && Request["EForm_Id"] != null)
            {
                string url = "~/EFrom/FundsUse.aspx?ProId=" + Request["ProId"] + "&allE_id=" + Request["allE_id"] + "&EForm_Id=" + Request["EForm_Id"] + "&&ReEdit=true";
                Response.Redirect(url);
            }
        }
        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                CG_POOrder model = POSer.GetModel(Convert.ToInt32(Session["Comm_CGPONo"]));

                txtSupplier.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
                Session["Comm_CGPONo"] = null;
            }
        }

        protected void CAIA_CheckedChanged(object sender, EventArgs e)
        {
            SetTotal();
        }

        protected void ltbnRuku_Click(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                txtHouseNo.Text = Session["Comm_CGPONo"].ToString();
                Session["Comm_CGPONo"] = null;
            }
        }

        protected void lbtnChuKu_Click(object sender, EventArgs e)
        {

            if (Session["Comm_CGPONo"] != null)
            {
                txtExpNO.Text = Session["Comm_CGPONo"].ToString();
                Session["Comm_CGPONo"] = null;
            }
        }

        protected void ddlShuiNo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var xishu = tb_FundsUse.GetXiShuo(ddlShuiNo1.Text);
            lblXiShu1.Text = xishu.ToString();
            decimal total = 0;
            if (txtTotal.Text != "")
            {
                total = Convert.ToDecimal(txtTotal.Text);
            }
            lblCAITotal.Text = (total * xishu).ToString();
            SetTotal();
        }

        protected void ddlShuiNo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var xishu = tb_FundsUse.GetXiShuo(ddlShuiNo2.Text);
            lblXiShu2.Text = xishu.ToString();
            decimal a = 0;
            if (txtHuiWu.Text != "")
            {
                a = Convert.ToDecimal(txtHuiWu.Text);
            }
            lblHuiTotal.Text = (a * xishu).ToString();

            SetTotal();
        }

        protected void ddlShuiNo3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var xishu = tb_FundsUse.GetXiShuo(ddlShuiNo3.Text);
            lblXiShu3.Text = xishu.ToString();
            decimal a = 0;
            if (txtRen.Text != "")
            {
                a = Convert.ToDecimal(txtRen.Text);
            }
            lblRenTotal.Text = (a * xishu).ToString();

            SetTotal();
        }

        protected void ddlXingCaiXS_SelectedIndexChanged(object sender, EventArgs e)
        {
            var xishu = tb_FundsUse.GetXiShuo(ddlXingCaiXS.Text);
            lblXiShuXing.Text = xishu.ToString();
            decimal a = 0;
            if (txtXingCai.Text != "")
            {
                a = Convert.ToDecimal(txtXingCai.Text);
            }
            lblXingCaiTotal.Text = (a * xishu).ToString();

            SetTotal();
        }

        protected void tBtnRenGong_CheckedChanged(object sender, EventArgs e)
        {
            txtXingCai.Enabled = false;
            txtHuiWu.Enabled = false;
            txtRen.Enabled = true;
            ddlTeamInfo.Enabled = true;
          
        }

        protected void tBtnHuiWu_CheckedChanged(object sender, EventArgs e)
        {
            txtXingCai.Enabled = false;
            txtHuiWu.Enabled = true;
            txtRen.Enabled = false;
            ddlTeamInfo.Enabled = false;
            ddlTeamInfo.Text = "选择";
        }

        protected void tBtnXingZheng_CheckedChanged(object sender, EventArgs e)
        {
            txtXingCai.Enabled = true;
            txtHuiWu.Enabled = false;
            txtRen.Enabled = false;
            ddlTeamInfo.Enabled = false;
            ddlTeamInfo.Text = "选择";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtShuoMing.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写请款概要说明！');</script>");
                txtShuoMing.Focus();
                return;
            }

            if (tBtnRenGong.Checked && ddlTeamInfo.Text == "选择")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择费用人！');</script>");
                ddlTeamInfo.Focus();
                return ;
            }

            if (txtUseTo.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写款项用途！');</script>");
                txtUseTo.Focus();
                return;
            }



            tb_FundsUse model = new tb_FundsUse();
            model.useTo = txtUseTo.Text;
            if (ddlTeamInfo.Text != "选择")
            {
                model.MyTeamLever = ddlTeamInfo.Text;
            }
            model.ShuoMing = txtShuoMing.Text;

            if (tBtnRenGong.Checked)
            {
                model.FundType = tBtnRenGong.Text;
            }
            if (tBtnHuiWu.Checked)
            {
                model.FundType = tBtnHuiWu.Text;
            }
            if (tBtnXingZheng.Checked)
            {
                model.FundType = tBtnXingZheng.Text;
            }
            model.id= Convert.ToInt32(Request["allE_id"]);
            new tb_FundsUseService().Edit(model);
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
        }
    }
}
