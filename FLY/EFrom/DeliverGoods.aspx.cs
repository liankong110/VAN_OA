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
namespace VAN_OA.EFrom
{
    public partial class DeliverGoods : System.Web.UI.Page
    {
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
            if (txtDateTime.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                txtDateTime.Focus();

                return false;
            }
            else
            {
                if (CommHelp.VerifesToDateTime(txtDateTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return false;
                }
            }

            if (txtCompName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写公司名称！');</script>");
                txtCompName.Focus();

                return false;
            }


            if (txtInvName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写货物名称！');</script>");
                txtInvName.Focus();

                return false;
            }

            if (txtGoTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写外出时间！');</script>");
                txtGoTime.Focus();
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtGoTime.Text) && CommHelp.VerifesToDateTime(txtGoTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外出时间 格式错误！');</script>");
                    return false;
                }
            }
            //txtGoTime.Text = txtGoTime.Text.Trim().Replace('：', ':');
            //txtGoTime.Text = txtGoTime.Text.Trim().Replace('。', ':');
            //txtGoTime.Text = txtGoTime.Text.Trim().Replace('.', ':');

            //txtBackTime.Text = txtBackTime.Text.Trim().Replace('：', ':');
            //txtBackTime.Text = txtBackTime.Text.Trim().Replace('.', ':');
            //txtBackTime.Text = txtBackTime.Text.Trim().Replace('。', ':');
            if (ddlPers.Visible == false && ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
            {

               

                if (txtBackTime.Text.Trim() != "")
                {
                    //txtGoTime.Text.Trim().Replace('：', ':');
                    if (CommHelp.VerifesToDateTime(txtGoTime.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外出时间 格式错误！');</script>");
                        return false;
                    }
                     
                }
                else
                {
                    //txtBackTime.Text.Trim().Replace('：', ':');

                    if (!string.IsNullOrEmpty(txtDateTime.Text) && CommHelp.VerifesToDateTime(txtDateTime.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('回来时间 格式错误！');</script>");
                        return false;
                    }
                }

            }
            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }

            try
            {
                //if (txtGoTime.Text != "")
                //{
                //    Convert.ToDateTime(Convert.ToDateTime( txtDateTime.Text).ToString("yyyy-MM-dd") + " " + txtGoTime.Text);
                //}


                //if (txtBackTime.Text != "")
                //{
                //    Convert.ToDateTime(Convert.ToDateTime(txtDateTime.Text).ToString("yyyy-MM-dd") + " " + txtBackTime.Text);
                //}

                if (txtBackTime.Text != "" && txtGoTime.Text != "")
                {
                    if (Convert.ToDateTime(txtGoTime.Text) >= Convert.ToDateTime(txtBackTime.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外出时间不能大于回来时间！');</script>");
                        txtGoTime.Focus();

                        return false;

                    }
                }
                //Convert.ToDateTime(txtDateTime.Text + " " + txtGoTime.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtGoTime.Focus();

                return false;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtName.Focus();

                return false;
            }
            #endregion


            return true;
        }



        private void setEnable(bool result)
        {
            if (result && Request["allE_id"]!=null)
            {
                txtBackTime.Enabled = true;
            }
            txtDepartName.ReadOnly = !result;
            txtSongHuo.ReadOnly = !result;
            //txtName.ReadOnly =true;
            txtInvName.ReadOnly = !result;
            //txtGoTime.ReadOnly = !result;
            txtDepartName.ReadOnly = !result;
            txtDateTime.ReadOnly = true;
            txtCompName.ReadOnly = !result;
            //txtBackTime.ReadOnly = !result;
            txtAddress.ReadOnly = !result;
            //Image1.Enabled = result;
            if (result == true)
            {
                btnEdit.Visible = true;
            }
            //if (result == true)
            //{
            //    if (txtGoTime.Text == "")
            //    {
            //        txtGoTime.ReadOnly = false;
            //        txtBackTime.ReadOnly = true;
            //    }
            //    else if (txtBackTime.Text == "")
            //    {
            //        txtGoTime.ReadOnly = true;
            //        txtBackTime.ReadOnly = false;
            //    }
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!base.IsPostBack)
            {
                //请假单子   
               

                btnEdit.Visible = false;
                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;

                    txtGoTime.Enabled = false;
                    txtBackTime.Enabled = false;
                    //txtGoTime.ReadOnly = true;
                    //txtBackTime.ReadOnly = true;
                    txtDepartName.Text = use.LoginIPosition;

                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtGoTime.Enabled = true;
                        txtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人

                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

                            ViewState["ids"] = ids;
                            if (roleUserList != null)
                            {
                                ddlPers.DataSource = roleUserList;
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";

                                try
                                {
                                    ddlPers.Text = use.ReportTo.ToString();
                                }
                                catch (Exception)
                                {


                                }
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
                        tb_DeliverGoodsService goodsSer = new tb_DeliverGoodsService();

                        tb_DeliverGoods goodmodel = goodsSer.GetModel(Convert.ToInt32(Request["allE_id"]));

                        txtAddress.Text = goodmodel.Address;

                        if (goodmodel.BackTime != null)
                            txtBackTime.Text = goodmodel.BackTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        txtCompName.Text = goodmodel.CompName;

                        txtDateTime.Text = goodmodel.dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        txtDepartName.Text = goodmodel.DepartName;

                        if (goodmodel.GoTime != null)
                            txtGoTime.Text = goodmodel.GoTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        txtInvName.Text = goodmodel.InvName;
                        txtName.Text = goodmodel.Name;
                        txtSongHuo.Text = goodmodel.SongHuo;
                        lblProNo.Text = goodmodel.ProNo;

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
                                    {
                                       
                                        //从获取出的审核中 获取上级信息
                                        //List<A_Role_User> newList = new List<A_Role_User>();
                                        //for (int i = 0; i < roleUserList.Count; i++)
                                        //{
                                        //    if (roleUserList[i].UserId == use.ReportTo)
                                        //    {
                                        //        A_Role_User a = roleUserList[i];
                                        //        newList.Add(a);
                                        //        break;
                                        //    }
                                        //}

                                        //if (newList.Count > 0)
                                        //{
                                        //    ddlPers.DataSource = newList;
                                        //}
                                        //else
                                        //{
                                        //    ddlPers.DataSource = roleUserList;
                                        //}
                                        ddlPers.DataSource = roleUserList;
                                        ddlPers.DataBind();
                                        ddlPers.DataTextField = "UserName";
                                        ddlPers.DataValueField = "UserId";

                                        try
                                        {
                                            ddlPers.Text = use.ReportTo.ToString();
                                        }
                                        catch (Exception)
                                        {


                                        }
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
                                            ////从获取出的审核中 获取上级信息
                                            //List<A_Role_User> newList = new List<A_Role_User>();
                                            //for (int i = 0; i < roleUserList.Count; i++)
                                            //{
                                            //    if (roleUserList[i].UserId == use.ReportTo)
                                            //    {
                                            //        A_Role_User a = roleUserList[i];
                                            //        newList.Add(a);
                                            //        break;
                                            //    }
                                            //}

                                            //if (newList.Count > 0)
                                            //{
                                            //    ddlPers.DataSource = newList;
                                            //}
                                            //else
                                            //{
                                            //    ddlPers.DataSource = roleUserList;
                                            //}
                                            ddlPers.DataSource = roleUserList;
                                            ddlPers.DataBind();
                                            ddlPers.DataTextField = "UserName";
                                            ddlPers.DataValueField = "UserId";

                                            try
                                            {
                                                ddlPers.Text = use.ReportTo.ToString();
                                            }
                                            catch (Exception)
                                            {


                                            }
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
                    tb_DeliverGoods goodInfo = new tb_DeliverGoods();
                    goodInfo.Address = txtAddress.Text;
                    if (txtBackTime.Text != "")
                    {
                        goodInfo.BackTime = Convert.ToDateTime(txtBackTime.Text);
                    }
                    goodInfo.CompName = txtCompName.Text;
                    goodInfo.dateTime = Convert.ToDateTime(txtDateTime.Text);
                    goodInfo.DepartName = txtDepartName.Text;

                    if (txtGoTime.Text != "")
                        goodInfo.GoTime = Convert.ToDateTime(txtGoTime.Text);
                    goodInfo.InvName = txtInvName.Text;
                    goodInfo.Name = txtName.Text;
                    goodInfo.SongHuo = txtSongHuo.Text;

                    #endregion
                    if (Request["allE_id"] == null)//单据增加
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
                        tb_DeliverGoodsService goodIngoSer = new tb_DeliverGoodsService();

                        if (goodIngoSer.addTran(goodInfo, eform) > 0)
                        {

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
                        goodInfo.id = Convert.ToInt32(Request["allE_id"]);
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
                        tb_DeliverGoodsService goodIngoSer = new tb_DeliverGoodsService();
                        if (goodIngoSer.updateTran(goodInfo, eform, forms))
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {





            if (txtDepartName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写部门！');</script>");
                txtDepartName.Focus();

                return;
            }
            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写姓名！');</script>");
                txtName.Focus();

                return;
            }
            if (txtDateTime.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                txtDateTime.Focus();

                return;
            }
            else
            {
                try
                {
                    Convert.ToDateTime(txtDateTime.Text);
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                    txtDateTime.Focus();

                    return;

                }
            }

            if (txtCompName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写公司名称！');</script>");
                txtCompName.Focus();

                return;
            }


            if (txtInvName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写货物名称！');</script>");
                txtInvName.Focus();

                return;
            }



            try
            {


                if (txtBackTime.Text != "" && txtGoTime.Text != "")
                {
                    if (Convert.ToDateTime(txtGoTime.Text) >= Convert.ToDateTime(txtBackTime.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外出时间不能大于回来时间！');</script>");
                        txtGoTime.Focus();

                        return;

                    }
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtGoTime.Focus();

                return;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtName.Focus();

                return;
            }

            #region 获取单据基本信息
            tb_DeliverGoods goodInfo = new tb_DeliverGoods();
            goodInfo.Address = txtAddress.Text;
            if (txtBackTime.Text != "")
            {
                goodInfo.BackTime = Convert.ToDateTime(txtBackTime.Text);
            }
            goodInfo.CompName = txtCompName.Text;
            goodInfo.dateTime = Convert.ToDateTime(txtDateTime.Text);
            goodInfo.DepartName = txtDepartName.Text;
            if (txtGoTime.Text != "")
                goodInfo.GoTime = Convert.ToDateTime(txtGoTime.Text);
            goodInfo.InvName = txtInvName.Text;
            goodInfo.Name = txtName.Text;
            goodInfo.SongHuo = txtSongHuo.Text;

            #region 本单据的ID
            goodInfo.id = Convert.ToInt32(Request["allE_id"]);
            #endregion

            tb_DeliverGoodsService googdSer = new tb_DeliverGoodsService();
            googdSer.Update(goodInfo);
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");


            #endregion
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {


            //base.ClientScript.RegisterStartupScript(base.GetType(), null,string.Format( "<script>alert('{0}！');</script>",TextBox3.Text));



        }



    }
}
