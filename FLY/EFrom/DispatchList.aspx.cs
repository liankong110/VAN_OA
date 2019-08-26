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
using System;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;

namespace VAN_OA.EFrom
{
    public partial class DispatchList : System.Web.UI.Page
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
            if (FormCheck())
            {

                btnSub.Enabled = false;
                if (base.Request["ProId"] != null)
                {

                    Tb_DispatchList timeModel = new Tb_DispatchList();
                    try
                    {
                        #region 获取单据基本信息

                        timeModel.UserId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));

                        timeModel.BusFromAddress = txtBusFromAddress.Text;

                        if (txtBusFromTime.Text != "")
                            timeModel.BusFromTime = Convert.ToDateTime(txtBusFromTime.Text);

                        timeModel.BusToAddress = txtBusToAddress.Text;

                        if (txtBusTotal.Text != "")
                            timeModel.BusTotal = Convert.ToDecimal(txtBusTotal.Text);


                        if (txtBusToTime.Text != "")
                            timeModel.BusToTime = Convert.ToDateTime(txtBusToTime.Text);

                        timeModel.CardNo = txtCardNo.Text;

                        if (timeModel.CreateTime != null)
                            timeModel.CreateTime = Convert.ToDateTime(txtCreatime.Text);

                        if (txtCreatime.Text != "")
                            timeModel.CreateTime = Convert.ToDateTime(txtCreatime.Text);


                        if (txtEvTime.Text != "")
                            timeModel.EvTime = Convert.ToDateTime(txtEvTime.Text);




                        timeModel.GuoBeginAddress = txtGuoBeginAddress.Text;
                        timeModel.GuoToAddress = txtGuoToAddress.Text;

                        if (txtGuoTotal.Text != "")
                            timeModel.GuoTotal = Convert.ToDecimal(txtGuoTotal.Text);



                        timeModel.HotelAddress = txtHotelAddress.Text;
                        timeModel.HotelName = txtHotelName.Text;

                        if (txtHotelTotal.Text != "")
                            timeModel.HotelTotal = Convert.ToDecimal(txtHotelTotal.Text);

                        timeModel.OilFromAddress = txtOilFromAddress.Text;

                        if (txtOilLiCheng.Text != "")
                            timeModel.OilLiCheng = Convert.ToDecimal(txtOilLiCheng.Text);

                        timeModel.OilToAddress = txtOilToAddress.Text;

                        if (txtOilTotal.Text != "")
                            timeModel.OilTotal = Convert.ToDecimal(txtOilTotal.Text);

                        //if (txtOilXiShu.Text != "")
                        //    timeModel.OilXiShu = Convert.ToDecimal(txtOilXiShu.Text);

                        timeModel.OtherContext = txtOtherContext.Text;

                        if (txtOtherTotal.Text != "")
                            timeModel.OtherTotal = Convert.ToDecimal(txtOtherTotal.Text);

                        timeModel.PoContext = txtPoContext.Text;
                        timeModel.PostFromAddress = txtPostFromAddress.Text;
                        timeModel.PostToAddress = txtPostToAddress.Text;

                        if (txtPostTotal.Text != "")
                            timeModel.PostTotal = Convert.ToDecimal(txtPostTotal.Text);

                        if (txtPoTotal.Text != "")
                            timeModel.PoTotal = Convert.ToDecimal(txtPoTotal.Text);

                        timeModel.RepastAddress = txtRepastAddress.Text;

                        if (txtRepastPerNum.Text != "")
                            timeModel.RepastPerNum = Convert.ToDecimal(txtRepastPerNum.Text);

                        timeModel.RepastPers = txtRepastPers.Text;

                        if (txtRepastTotal.Text != "")
                            timeModel.RepastTotal = Convert.ToDecimal(txtRepastTotal.Text);


                        if (cbHotelType1.Checked)
                        {
                            timeModel.HotelType = cbHotelType1.Text;
                        }
                        if (cbHotelType2.Checked)
                        {
                            timeModel.HotelType = cbHotelType2.Text;
                        }



                        timeModel.IfBus = cbIfBus.Checked;
                        timeModel.IfTexi = cbIfTexi.Checked;
                        timeModel.PostFrom = cbPostFrom.Checked;
                        timeModel.PostTo = cbPostTo.Checked;


                        if (rdoRepastType1.Checked)
                        {
                            timeModel.RepastType = rdoRepastType1.Text;
                        }
                        if (rdoRepastType2.Checked)
                        {
                            timeModel.RepastType = rdoRepastType2.Text;
                        }


                        //===
                        timeModel.BusRemark = txtBusRemark.Text;
                        timeModel.RepastRemark = txtRepastRemark.Text;
                        timeModel.HotelRemark = txtHotelRemark.Text;
                        timeModel.OilRemark = txtOilRemark.Text;
                        timeModel.GuoRemark = txtGuoRemark.Text;
                        timeModel.PostRemark = txtPostRemark.Text;
                        timeModel.PoRemark = txtPoRemark.Text;
                        timeModel.OtherRemark = txtOtherRemark.Text;
                        timeModel.PostNo = txtPostNo.Text;
                        timeModel.PostCompany = txtPostCompany.Text;
                        timeModel.PostContext = txtPostContext.Text;
                        timeModel.PostToPer = txtPostToPer.Text;

                        //===

                        #endregion

                        if (lbtnPostNo.Text != "")
                        {
                            timeModel.Post_No = lbtnPostNo.Text;
                        }
                        if (lblPost_Id.Text != "")
                        {
                            timeModel.Post_Id = Convert.ToInt32(lblPost_Id.Text);
                        }

                        if (lblCaiID.Text != "")
                        {
                            timeModel.CaiId = Convert.ToInt32(lblCaiID.Text);
                        }
                        timeModel.PoName = txtPOName.Text;
                        timeModel.PoNo = txtPONo.Text;
                        timeModel.GuestName = txtSupplier.Text;
                        timeModel.CaiPoNo = lbtnCaiNo.Text;
                    }
                    catch (Exception)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的格式有误,请检查后在提交！');</script>");
                        btnSub.Enabled = true;
                        return;
                    } 

                    #region 本单据的ID
                    timeModel.Id = Convert.ToInt32(Request["allE_id"]);
                    #endregion  


                    Tb_DispatchListService OverTimeSer = new Tb_DispatchListService();
                    if (OverTimeSer.Update(timeModel))
                    {
                        // btnSub.Enabled = true;
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");
                        
                    }
                    else
                    {
                        btnSub.Enabled = false;
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");

                    }
                }

            }
        }



        public bool FormCheck()
        {
            #region 设置自己要判断的信息
            //if (txtDepartName.Text.Trim() == "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写部门！');</script>");
            //    txtDepartName.Focus();

            //    return false;
            //}
            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写报销人！');</script>");
                txtName.Focus();

                return false;
            }

            if (txtEvTime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写事件发生日期！');</script>");
                txtEvTime.Focus();
                return false;
            }

            if (CommHelp.VerifesToDateTime(txtEvTime.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('事件发生日期 格式错误！');</script>");
                return false;
            }

            if (txtCreatime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写填写日期！');</script>");
                txtCreatime.Focus();
                return false;
            }
            if (CommHelp.VerifesToDateTime(txtCreatime.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                return false;
            }


            if (txtPONo.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择项目信息！');</script>");
                txtPONo.Focus();
                return false;
            }


            if (ddlPers.Visible == true && ddlPers.SelectedItem == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择下一步审核人！');</script>");
                ddlPers.Focus();

                return false;
            }


            try
            {
                Convert.ToDateTime(txtEvTime.Text);
                Convert.ToDateTime(txtCreatime.Text);
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtEvTime.Focus();

                return false;

            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtName.Focus();

                return false;
            }
            #endregion

            if (new CG_POOrderService().ExistPONO(txtPONo.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目信息不存在！');</script>");
                return false;
            }
            if (CG_POOrderService.IsClosePONO(txtPONo.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                        "<script>alert('此项目已经关闭！');</script>");
                return false;
            }
            if (CG_POOrderService.IsSpecialPONO(txtPONo.Text,txtPOName.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                        "<script>alert('特殊订单无法计入费用！');</script>");
                return false;
            }
            return true;
        }



        private void setEnable(bool result)
        {
            txtBusFromAddress.ReadOnly = !result;
            txtBusFromTime.ReadOnly = !result;
            txtBusToAddress.ReadOnly = !result;
            txtBusTotal.ReadOnly = !result;
            txtBusToTime.ReadOnly = !result;
            txtCardNo.ReadOnly = !result;
            txtCreatime.ReadOnly = !result;
            txtEvTime.ReadOnly = !result;
            txtGuoBeginAddress.ReadOnly = !result;
            txtGuoToAddress.ReadOnly = !result;
            txtGuoTotal.ReadOnly = !result;
            txtHotelAddress.ReadOnly = !result;
            txtHotelName.ReadOnly = !result;
            txtHotelTotal.ReadOnly = !result;
            txtOilFromAddress.ReadOnly = !result;
            txtOilLiCheng.ReadOnly = !result;
            txtOilToAddress.ReadOnly = !result;
            txtOilTotal.ReadOnly = !result;
            //txtOilXiShu.ReadOnly = !result;
            txtOtherContext.ReadOnly = !result;
            txtOtherTotal.ReadOnly = !result;
            txtPoContext.ReadOnly = !result;
            txtPostFromAddress.ReadOnly = !result;
            txtPostToAddress.ReadOnly = !result;
            txtPostTotal.ReadOnly = !result;
            txtPoTotal.ReadOnly = !result;
            txtRepastAddress.ReadOnly = !result;
            txtRepastPerNum.ReadOnly = !result;
            txtRepastPers.ReadOnly = !result;
            txtRepastTotal.ReadOnly = !result;
            Image1.Enabled = result;
            ImageButton1.Enabled = result;
            ImageButton2.Enabled = result;
            ImageButton3.Enabled = result;
            cbHotelType1.Enabled = result;
            cbHotelType2.Enabled = result;
            cbIfBus.Enabled = result;
            cbIfTexi.Enabled = result;
            cbPostFrom.Enabled = result;
            cbPostTo.Enabled = result;
            rdoRepastType1.Enabled = result;
            rdoRepastType2.Enabled = result;


            txtBusRemark.ReadOnly = !result;
            txtRepastRemark.ReadOnly = !result;
            txtHotelRemark.ReadOnly = !result;
            txtOilRemark.ReadOnly = !result;
            txtGuoRemark.ReadOnly = !result;
            txtPostRemark.ReadOnly = !result;
            txtPoRemark.ReadOnly = !result;
            txtOtherRemark.ReadOnly = !result;
            txtPostNo.ReadOnly = !result;
            txtPostCompany.ReadOnly = !result;
            txtPostContext.ReadOnly = !result;
            txtPostToPer.ReadOnly = !result;




            //txtName.ReadOnly = true;
            lbtnSelNo1.Visible = false;
            lbtnSelNo2.Visible = false;
            DropDownList1.Visible = false;
        }



        private void setEnableType(bool result)
        {
            txtBusFromAddress.ReadOnly = !result;
            txtBusFromTime.ReadOnly = !result;
            txtBusToAddress.ReadOnly = !result;
            txtBusTotal.ReadOnly = !result;
            txtBusToTime.ReadOnly = !result;
            txtGuoBeginAddress.ReadOnly = !result;
            txtGuoToAddress.ReadOnly = !result;
            txtGuoTotal.ReadOnly = !result;
            txtHotelAddress.ReadOnly = !result;
            txtHotelName.ReadOnly = !result;
            txtHotelTotal.ReadOnly = !result;

            txtOtherContext.ReadOnly = !result;
            txtOtherTotal.ReadOnly = !result;
            txtPoContext.ReadOnly = !result;
            txtPostFromAddress.ReadOnly = !result;
            txtPostToAddress.ReadOnly = !result;
            txtPostTotal.ReadOnly = !result;
            txtPoTotal.ReadOnly = !result;
            txtRepastAddress.ReadOnly = !result;
            txtRepastPerNum.ReadOnly = !result;
            txtRepastPers.ReadOnly = !result;
            txtRepastTotal.ReadOnly = !result;
            // Image1.Enabled = result;
            ImageButton1.Enabled = result;
            // ImageButton2.Enabled = result;
            ImageButton3.Enabled = result;
            cbHotelType1.Enabled = result;
            cbHotelType2.Enabled = result;
            cbIfBus.Enabled = result;
            cbIfTexi.Enabled = result;
            cbPostFrom.Enabled = result;
            cbPostTo.Enabled = result;
            rdoRepastType1.Enabled = result;
            rdoRepastType2.Enabled = result;



            txtBusRemark.ReadOnly = !result;
            txtRepastRemark.ReadOnly = !result;
            txtHotelRemark.ReadOnly = !result;

            txtGuoRemark.ReadOnly = !result;
            txtPostRemark.ReadOnly = !result;
            txtPoRemark.ReadOnly = !result;
            txtOtherRemark.ReadOnly = !result;
            txtPostNo.ReadOnly = !result;
            txtPostCompany.ReadOnly = !result;
            txtPostContext.ReadOnly = !result;
            txtPostToPer.ReadOnly = !result;



            //油费
            txtOilFromAddress.ReadOnly = result;
            txtOilLiCheng.ReadOnly = result;
            txtOilToAddress.ReadOnly = result;
            txtOilTotal.ReadOnly = result;
            txtOilRemark.ReadOnly = result;

            //lbtnSelNo1.Visible = false;
            //lbtnSelNo2.Visible = false;
            //DropDownList1.Visible = false;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {


                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;



                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtCreatime.Text = DateTime.Now.ToString();
                        txtPoContext.Enabled = false;
                        txtPoRemark.Enabled = false;
                        txtPoTotal.Enabled = false;
                        lbtnSelNo2.Visible = false;

                        lbtnPostNo.Enabled = false;
                        #region 单据增加
                        if (Request["ProId"] != null && Request["Post"] == null)
                        {
                            object proName = DBHelp.ExeScalar(string.Format("select pro_Type from A_ProInfo where pro_Id={0}", Request["ProId"]));
                            if (proName != null)
                            {
                                if (proName.ToString() == "预期报销单")
                                {
                                    setEnableType(true);
                                }
                                if (proName.ToString() == "预期报销单(油费报销)")
                                {
                                    DropDownList1.Visible = false;
                                    setEnableType(false);
                                }
                            }
                        }
                        else
                        {
                            if (Session["ToDispatchList"] != null)//邮寄单信息保存到报销单中
                            {
                                setEnableType(true);
                                tb_Post postModel = Session["ToDispatchList"] as tb_Post;
                                if (postModel != null)
                                {
                                    if (!string.IsNullOrEmpty(postModel.PONo))
                                    {
                                        string sql = string.Format("select AE FROM CG_POOrder WHERE IFZhui=0 AND PONo='{0}'", postModel.PONo);
                                        var AE = DBHelp.ExeScalar(sql);
                                        if (AE != DBNull.Value)
                                        {
                                            lblAe.Text = AE.ToString();
                                        }
                                    }
                                    txtPONo.Text = postModel.PONo;

                                    txtPOName.Text = postModel.POName;
                                    txtSupplier.Text = postModel.POGuestName;

                                    txtPostContext.Text = postModel.PostContext;
                                    if (postModel.PostFrom != null)
                                        cbPostFrom.Checked = Convert.ToBoolean(postModel.PostFrom);
                                    txtPostFromAddress.Text = postModel.PostFromAddress;
                                    if (postModel.PostTo != null)
                                        cbPostTo.Checked = Convert.ToBoolean(postModel.PostTo);
                                    txtPostToAddress.Text = postModel.PostToAddress;
                                    if (postModel.PostTotal != null)
                                        txtPostTotal.Text = postModel.PostTotal.ToString();

                                    txtPostRemark.Text = postModel.PostRemark;
                                    lbtnPostNo.Text = postModel.ProNo;
                                    lblPost_Id.Text = postModel.Id.ToString();


                                    txtPostNo.Text = postModel.PostCode;
                                    txtPostToPer.Text = postModel.FromPer;
                                    txtPostCompany.Text = postModel.WuliuName;

                                    txtCreatime.Text = DateTime.Now.ToString();
                                    txtEvTime.Text = DateTime.Now.ToString();
                                    Session["ToDispatchList"] = null;

                                    txtPostNo.Enabled = false;
                                    txtPostCompany.Enabled = false;
                                    txtPostContext.Enabled = false;
                                    txtPostToPer.Enabled = false;
                                    cbPostFrom.Enabled = false;
                                    txtPostFromAddress.Enabled = false;
                                    cbPostTo.Enabled = false;
                                    txtPostToAddress.Enabled = false;
                                    txtPostTotal.Enabled = false;
                                    txtPostRemark.Enabled = false;
                                }
                            }
                        }
                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;

                        //rdo1.Checked = true;

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

                        if (Request["IsEdit"] != null)
                        {
                            btnSave.Visible = true;
                            setEnableType(true);
                            //所有的金额不能修改
                            txtBusTotal.Enabled = false;
                            txtRepastTotal.Enabled = false;
                            txtHotelTotal.Enabled = false;
                            txtOilTotal.Enabled = false;
                            txtGuoTotal.Enabled = false;
                            txtPostTotal.Enabled = false;
                            txtPoTotal.Enabled = false;
                            txtOtherTotal.Enabled = false;
                            btnClose.Visible = false;

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
                        Tb_DispatchListService dispatchListSer = new Tb_DispatchListService();

                        Tb_DispatchList timeModel = dispatchListSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        if (!string.IsNullOrEmpty(timeModel.PoNo))
                        {
                            string sql = string.Format("select AE FROM CG_POOrder WHERE IFZhui=0 AND PONo='{0}'", timeModel.PoNo);
                            var AE = DBHelp.ExeScalar(sql);
                            if (AE != DBNull.Value)
                            {
                                lblAe.Text = AE.ToString();
                            }
                        }
                        txtName.Text = timeModel.UserName;

                        txtBusFromAddress.Text = timeModel.BusFromAddress;
                        if (timeModel.BusFromTime != null)
                            txtBusFromTime.Text = timeModel.BusFromTime.ToString();


                        txtBusToAddress.Text = timeModel.BusToAddress;

                        if (timeModel.BusTotal != null)
                            txtBusTotal.Text = timeModel.BusTotal.ToString();

                        if (timeModel.BusToTime != null)
                            txtBusToTime.Text = timeModel.BusToTime.ToString(); ;
                        txtCardNo.Text = timeModel.CardNo;

                        if (timeModel.CreateTime != null)
                            txtCreatime.Text = timeModel.CreateTime.ToString(); ;

                        if (timeModel.EvTime != null)
                            txtEvTime.Text = timeModel.EvTime.ToString(); ;
                        txtGuoBeginAddress.Text = timeModel.GuoBeginAddress;
                        txtGuoToAddress.Text = timeModel.GuoToAddress;

                        if (timeModel.GuoTotal != null)
                            txtGuoTotal.Text = timeModel.GuoTotal.ToString();
                        txtHotelAddress.Text = timeModel.HotelAddress;
                        txtHotelName.Text = timeModel.HotelName;

                        if (timeModel.HotelTotal != null)
                            txtHotelTotal.Text = timeModel.HotelTotal.ToString();
                        txtOilFromAddress.Text = timeModel.OilFromAddress;

                        if (timeModel.OilLiCheng != null)
                            txtOilLiCheng.Text = timeModel.OilLiCheng.ToString();
                        txtOilToAddress.Text = timeModel.OilToAddress;

                        if (timeModel.OilTotal != null)
                            txtOilTotal.Text = timeModel.OilTotal.ToString();

                        //if (timeModel.OilXiShu != null)
                        //    txtOilXiShu.Text = timeModel.OilXiShu.ToString();
                        txtOtherContext.Text = timeModel.OtherContext;

                        if (timeModel.OtherTotal != null)
                            txtOtherTotal.Text = timeModel.OtherTotal.ToString();
                        txtPoContext.Text = timeModel.PoContext;
                        txtPostFromAddress.Text = timeModel.PostFromAddress;
                        txtPostToAddress.Text = timeModel.PostToAddress;

                        if (timeModel.PostTotal != null)
                            txtPostTotal.Text = timeModel.PostTotal.ToString();

                        if (timeModel.PoTotal != null)
                            txtPoTotal.Text = timeModel.PoTotal.ToString();
                        txtRepastAddress.Text = timeModel.RepastAddress;

                        if (timeModel.RepastPerNum != null)
                            txtRepastPerNum.Text = timeModel.RepastPerNum.ToString();
                        txtRepastPers.Text = timeModel.RepastPers;

                        if (timeModel.RepastTotal != null)
                            txtRepastTotal.Text = timeModel.RepastTotal.ToString();



                        if (timeModel.HotelType == "标准间")
                        {
                            cbHotelType1.Checked = true;
                        }
                        else if (timeModel.HotelType == "单人间")
                        {
                            cbHotelType2.Checked = true;
                        }


                        lblCaiID.Text = timeModel.CaiId.ToString();

                        cbIfBus.Checked = timeModel.IfBus;
                        cbIfTexi.Checked = timeModel.IfTexi;
                        cbPostFrom.Checked = timeModel.PostFrom;
                        cbPostTo.Checked = timeModel.PostTo;

                        //===
                        txtBusRemark.Text = timeModel.BusRemark;
                        txtRepastRemark.Text = timeModel.RepastRemark;
                        txtHotelRemark.Text = timeModel.HotelRemark;
                        txtOilRemark.Text = timeModel.OilRemark;
                        txtGuoRemark.Text = timeModel.GuoRemark;
                        txtPostRemark.Text = timeModel.PostRemark;
                        txtPoRemark.Text = timeModel.PoRemark;
                        txtOtherRemark.Text = timeModel.OtherRemark;
                        txtPostNo.Text = timeModel.PostNo;
                        txtPostCompany.Text = timeModel.PostCompany;
                        txtPostContext.Text = timeModel.PostContext;
                        txtPostToPer.Text = timeModel.PostToPer;

                        //===

                        if (timeModel.RepastType == "工作餐(每人限额30元)")
                        {
                            rdoRepastType1.Checked = true;
                        }
                        else if (timeModel.RepastType == "商务用餐")
                        {
                            rdoRepastType2.Checked = true;
                        }
                        txtName.ReadOnly = true;

                        lblProNo.Text = timeModel.CardNo;
                        lblTotal.Text = "总计：" + timeModel.Total.ToString();


                        //记录邮寄信息
                        if (timeModel.Post_Id != null)
                        {
                            lblPost_Id.Text = timeModel.Post_Id.ToString();
                        }
                        if (timeModel.Post_No != null)
                        {
                            lbtnPostNo.Text = timeModel.Post_No;
                        }

                        txtPOName.Text = timeModel.PoName;
                        txtPONo.Text = timeModel.PoNo;
                        txtSupplier.Text = timeModel.GuestName;

                        lbtnCaiNo.Text = timeModel.CaiPoNo;

                        //加载项目信息
                        //if (timeModel.OtherGuestName!=null&&timeModel.OtherPoNo!=null&&timeModel.OtherName!=null)
                        //    lblOtherPo.Text = timeModel.OtherPoNo + "/" + timeModel.OtherName + "/" + timeModel.OtherGuestName;


                        //if (timeModel.PoGuestName != null && timeModel.PoPoNo != null && timeModel.PoPoName != null)
                        //    lblPoPoInfo.Text = timeModel.PoPoNo + "/" + timeModel.PoPoName + "/" + timeModel.PoGuestName;

                        //if (timeModel.PostPoNo != null && timeModel.PostPoName != null && timeModel.PostGuestName != null)
                        //    lblPostPo.Text = timeModel.PostPoNo + "/" + timeModel.PostPoName + "/" + timeModel.PostGuestName;

                        //if (timeModel.GuoPoNo != null && timeModel.GuoPoName != null && timeModel.GuoGuestName != null)
                        //    lblGuoPo.Text = timeModel.GuoPoNo + "/" + timeModel.GuoPoName + "/" + timeModel.GuoGuestName;

                        //if (timeModel.OilPoNo != null && timeModel.OilPoName != null && timeModel.OilGuestName != null)
                        //    lblOilPo.Text = timeModel.OilPoNo + "/" + timeModel.OilPoName + "/" + timeModel.OilGuestName;

                        //if (timeModel.HotelPoNo != null && timeModel.HotelPoName != null && timeModel.HotelGuestName != null)
                        //    lblHotelPo.Text = timeModel.HotelPoNo + "/" + timeModel.HotelPoName + "/" + timeModel.HotelGuestName;

                        //if (timeModel.RepastPoNo != null && timeModel.RepastPoName != null && timeModel.RepastGuestName != null)
                        //    lblCaiYin.Text = timeModel.RepastPoNo + "/" + timeModel.RepastPoName + "/" + timeModel.RepastGuestName;

                        //if (timeModel.BusPoNo != null && timeModel.BusPoName != null && timeModel.BusGuestName != null)
                        //    lblBusPoInfo.Text = timeModel.BusPoNo + "/" + timeModel.BusPoName + "/" + timeModel.BusGuestName;




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
                                    setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
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
                                        ddlPers.DataSource = roleUserList;

                                        ddlPers.DataBind();
                                        ddlPers.DataTextField = "UserName";
                                        ddlPers.DataValueField = "UserId";
                                    }
                                    setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                }

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
                                        setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                    }
                                    else
                                    {
                                        int ids = 0;
                                        List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);
                                        ViewState["ids"] = ids;
                                        if (roleUserList != null)
                                        {
                                            ddlPers.DataSource = roleUserList;

                                            ddlPers.DataBind();
                                            ddlPers.DataTextField = "UserName";
                                            ddlPers.DataValueField = "UserId";
                                        }
                                        setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
                                    }
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
                            btnSave.Visible = true;
                            setEnableType(true);
                            //所有的金额不能修改
                            txtBusTotal.Enabled = false;
                            txtRepastTotal.Enabled = false;
                            txtHotelTotal.Enabled = false;
                            txtOilTotal.Enabled = false;
                            txtGuoTotal.Enabled = false;
                            txtPostTotal.Enabled = false;
                            txtPoTotal.Enabled = false;
                            txtOtherTotal.Enabled = false;
                            btnClose.Visible = false;
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

                    Tb_DispatchList timeModel = new Tb_DispatchList();
                    try
                    {
                        #region 获取单据基本信息

                        timeModel.UserId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));

                        timeModel.BusFromAddress = txtBusFromAddress.Text;

                        if (txtBusFromTime.Text != "")
                            timeModel.BusFromTime = Convert.ToDateTime(txtBusFromTime.Text);

                        timeModel.BusToAddress = txtBusToAddress.Text;

                        if (txtBusTotal.Text != "")
                            timeModel.BusTotal = Convert.ToDecimal(txtBusTotal.Text);


                        if (txtBusToTime.Text != "")
                            timeModel.BusToTime = Convert.ToDateTime(txtBusToTime.Text);

                        timeModel.CardNo = txtCardNo.Text;

                        if (timeModel.CreateTime != null)
                            timeModel.CreateTime = Convert.ToDateTime(txtCreatime.Text);

                        if (txtCreatime.Text != "")
                            timeModel.CreateTime = Convert.ToDateTime(txtCreatime.Text);


                        if (txtEvTime.Text != "")
                            timeModel.EvTime = Convert.ToDateTime(txtEvTime.Text);




                        timeModel.GuoBeginAddress = txtGuoBeginAddress.Text;
                        timeModel.GuoToAddress = txtGuoToAddress.Text;

                        if (txtGuoTotal.Text != "")
                            timeModel.GuoTotal = Convert.ToDecimal(txtGuoTotal.Text);



                        timeModel.HotelAddress = txtHotelAddress.Text;
                        timeModel.HotelName = txtHotelName.Text;

                        if (txtHotelTotal.Text != "")
                            timeModel.HotelTotal = Convert.ToDecimal(txtHotelTotal.Text);

                        timeModel.OilFromAddress = txtOilFromAddress.Text;

                        if (txtOilLiCheng.Text != "")
                            timeModel.OilLiCheng = Convert.ToDecimal(txtOilLiCheng.Text);

                        timeModel.OilToAddress = txtOilToAddress.Text;

                        if (txtOilTotal.Text != "")
                            timeModel.OilTotal = Convert.ToDecimal(txtOilTotal.Text);

                        //if (txtOilXiShu.Text != "")
                        //    timeModel.OilXiShu = Convert.ToDecimal(txtOilXiShu.Text);

                        timeModel.OtherContext = txtOtherContext.Text;

                        if (txtOtherTotal.Text != "")
                            timeModel.OtherTotal = Convert.ToDecimal(txtOtherTotal.Text);

                        timeModel.PoContext = txtPoContext.Text;
                        timeModel.PostFromAddress = txtPostFromAddress.Text;
                        timeModel.PostToAddress = txtPostToAddress.Text;

                        if (txtPostTotal.Text != "")
                            timeModel.PostTotal = Convert.ToDecimal(txtPostTotal.Text);

                        if (txtPoTotal.Text != "")
                            timeModel.PoTotal = Convert.ToDecimal(txtPoTotal.Text);

                        timeModel.RepastAddress = txtRepastAddress.Text;

                        if (txtRepastPerNum.Text != "")
                            timeModel.RepastPerNum = Convert.ToDecimal(txtRepastPerNum.Text);

                        timeModel.RepastPers = txtRepastPers.Text;

                        if (txtRepastTotal.Text != "")
                            timeModel.RepastTotal = Convert.ToDecimal(txtRepastTotal.Text);


                        if (cbHotelType1.Checked)
                        {
                            timeModel.HotelType = cbHotelType1.Text;
                        }
                        if (cbHotelType2.Checked)
                        {
                            timeModel.HotelType = cbHotelType2.Text;
                        }



                        timeModel.IfBus = cbIfBus.Checked;
                        timeModel.IfTexi = cbIfTexi.Checked;
                        timeModel.PostFrom = cbPostFrom.Checked;
                        timeModel.PostTo = cbPostTo.Checked;


                        if (rdoRepastType1.Checked)
                        {
                            timeModel.RepastType = rdoRepastType1.Text;
                        }
                        if (rdoRepastType2.Checked)
                        {
                            timeModel.RepastType = rdoRepastType2.Text;
                        }


                        //===
                        timeModel.BusRemark = txtBusRemark.Text;
                        timeModel.RepastRemark = txtRepastRemark.Text;
                        timeModel.HotelRemark = txtHotelRemark.Text;
                        timeModel.OilRemark = txtOilRemark.Text;
                        timeModel.GuoRemark = txtGuoRemark.Text;
                        timeModel.PostRemark = txtPostRemark.Text;
                        timeModel.PoRemark = txtPoRemark.Text;
                        timeModel.OtherRemark = txtOtherRemark.Text;
                        timeModel.PostNo = txtPostNo.Text;
                        timeModel.PostCompany = txtPostCompany.Text;
                        timeModel.PostContext = txtPostContext.Text;
                        timeModel.PostToPer = txtPostToPer.Text;

                        //===

                        #endregion

                        if (lbtnPostNo.Text != "")
                        {
                            timeModel.Post_No = lbtnPostNo.Text;
                        }
                        if (lblPost_Id.Text != "")
                        {
                            timeModel.Post_Id = Convert.ToInt32(lblPost_Id.Text);
                        }

                        if (lblCaiID.Text != "")
                        {
                            timeModel.CaiId = Convert.ToInt32(lblCaiID.Text);
                        }
                        timeModel.PoName = txtPOName.Text;
                        timeModel.PoNo = txtPONo.Text;
                        timeModel.GuestName = txtSupplier.Text;

                        timeModel.CaiPoNo = lbtnCaiNo.Text;


                        //获取项目信息
                        //if (lblOtherPo.Text != "")
                        //{
                        //    string[] pos = lblOtherPo.Text.Split('/');
                        //    timeModel.OtherGuestName = pos[2];
                        //    timeModel.OtherName = pos[1];
                        //    timeModel.OtherPoNo = pos[0];
                        //}

                        //if (lblPoPoInfo.Text != "")
                        //{
                        //    string[] pos = lblPoPoInfo.Text.Split('/');
                        //    timeModel.PoGuestName = pos[2];
                        //    timeModel.PoPoName = pos[1];
                        //    timeModel.PoPoNo = pos[0];
                        //}

                        //if (lblPostPo.Text != "")
                        //{
                        //    string[] pos = lblPostPo.Text.Split('/');
                        //    timeModel.PostGuestName = pos[2];
                        //    timeModel.PostPoName = pos[1];
                        //    timeModel.PostPoNo = pos[0];
                        //}


                        //if (lblGuoPo.Text != "")
                        //{
                        //    string[] pos = lblGuoPo.Text.Split('/');
                        //    timeModel.GuoGuestName = pos[2];
                        //    timeModel.GuoPoName = pos[1];
                        //    timeModel.GuoPoNo = pos[0];
                        //}

                        //if (lblOilPo.Text != "")
                        //{
                        //    string[] pos = lblOilPo.Text.Split('/');
                        //    timeModel.OilGuestName = pos[2];
                        //    timeModel.OilPoName = pos[1];
                        //    timeModel.OilPoNo = pos[0];
                        //}

                        //if (lblHotelPo.Text != "")
                        //{
                        //    string[] pos = lblHotelPo.Text.Split('/');
                        //    timeModel.HotelGuestName = pos[2];
                        //    timeModel.HotelPoName = pos[1];
                        //    timeModel.HotelPoNo = pos[0];
                        //}

                        //if (lblCaiYin.Text != "")
                        //{
                        //    string[] pos = lblCaiYin.Text.Split('/');
                        //    timeModel.RepastGuestName = pos[2];
                        //    timeModel.RepastPoName = pos[1];
                        //    timeModel.RepastPoNo = pos[0];
                        //}

                        //if (lblBusPoInfo.Text != "")
                        //{
                        //    string[] pos = lblBusPoInfo.Text.Split('/');
                        //    timeModel.BusGuestName = pos[2];
                        //    timeModel.BusPoName = pos[1];
                        //    timeModel.BusPoNo = pos[0];
                        //}

                    }
                    catch (Exception)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的格式有误,请检查后在提交！');</script>");
                        btnSub.Enabled = true;
                        return;
                    }
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
                        Tb_DispatchListService OverTimeSer = new Tb_DispatchListService();
                        if (OverTimeSer.addTran(timeModel, eform) > 0)
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
                        timeModel.Id = Convert.ToInt32(Request["allE_id"]);
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


                        Tb_DispatchListService OverTimeSer = new Tb_DispatchListService();
                        if (OverTimeSer.updateTran(timeModel, eform, forms))
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

        protected void lbtnPostNo_Click(object sender, EventArgs e)
        {
            if (lblPost_Id.Text != "" && lblPost_Id.Text != "0" && Request["ProId"] != null && Request["allE_id"] != null && Request["EForm_Id"] != null)
            {
                string url = "/EFrom/DispatchList.aspx?ProId=" + Request["ProId"] + "&allE_id=" + Request["allE_id"] + "&EForm_Id=" + Request["EForm_Id"];
                Session["backurl"] = url;

                object proId = DBHelp.ExeScalar("select pro_Id from A_ProInfo where pro_Type='邮寄文档快递表'");
                object EForm_Id = DBHelp.ExeScalar(string.Format("select id from tb_EForm where proId={0} and allE_id={1}", proId, lblPost_Id.Text));
                string toUrl = "~/EFrom/WFPost.aspx?ProId=" + proId + "&allE_id=" + lblPost_Id.Text + "&EForm_Id=" + EForm_Id;
                Response.Redirect(toUrl);
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
                if (!string.IsNullOrEmpty(model.PONo))
                {
                    string sql = string.Format("select AE FROM CG_POOrder WHERE IFZhui=0 AND PONo='{0}'", model.PONo);
                    var AE = DBHelp.ExeScalar(sql);
                    if (AE != DBNull.Value)
                    {
                        lblAe.Text = AE.ToString();
                    }
                }
                Session["Comm_CGPONo"] = null;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtSupplier.Text = "";
            txtPOName.Text = "";
            txtPONo.Text = "";

            lbtnCaiNo.Text = "";
            lblCaiID.Text = "0";
            if (DropDownList1.SelectedValue == "0")
            {
                txtPoContext.Enabled = false;
                txtPoRemark.Enabled = false;
                txtPoTotal.Enabled = false;
                lbtnSelNo2.Visible = false;
                lbtnSelNo1.Visible = true;
            }
            else if (DropDownList1.SelectedValue == "1")
            {
                txtPoContext.Enabled = true;
                txtPoRemark.Enabled = true;
                txtPoTotal.Enabled = true;
                lbtnSelNo1.Visible = false;
                lbtnSelNo2.Visible = true;
            }
        }

        protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DropDownList1_SelectedIndexChanged(sender, e);
        }

        CAI_OrderInHouseService CaiPOSer = new CAI_OrderInHouseService();
        protected void lbtnSelNo2_Click(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                CAI_OrderInHouse model = CaiPOSer.GetModel(Convert.ToInt32(Session["Comm_CGPONo"]));
                txtSupplier.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;

                lbtnCaiNo.Text = model.ProNo;
                lblCaiID.Text = model.Id.ToString();

                if (!string.IsNullOrEmpty(model.PONo))
                {
                    string sql = string.Format("select AE FROM CG_POOrder WHERE IFZhui=0 AND PONo='{0}'", model.PONo);
                    var AE = DBHelp.ExeScalar(sql);
                    if (AE != DBNull.Value)
                    {
                        lblAe.Text = AE.ToString();
                    }
                }
                Session["Comm_CGPONo"] = null;
            }
        }

        protected void lbtnCaiNo_Click(object sender, EventArgs e)
        {

            if (lblCaiID.Text != "" && lblCaiID.Text != "0" && Request["ProId"] != null && Request["allE_id"] != null && Request["EForm_Id"] != null)
            {
                string url = "/EFrom/DispatchList.aspx?ProId=" + Request["ProId"] + "&allE_id=" + Request["allE_id"] + "&EForm_Id=" + Request["EForm_Id"];
                Session["backurl"] = url;

                object proId = DBHelp.ExeScalar("select pro_Id from A_ProInfo where pro_Type='采购入库'");
                object EForm_Id = DBHelp.ExeScalar(string.Format("select id from tb_EForm where proId={0} and allE_id={1}", proId, lblCaiID.Text));
                string toUrl = "~/JXC/WFCAI_OrderInHouse.aspx?ProId=" + proId + "&allE_id=" + lblCaiID.Text + "&EForm_Id=" + EForm_Id;
                Response.Redirect(toUrl);
            }
        }







    }
}
