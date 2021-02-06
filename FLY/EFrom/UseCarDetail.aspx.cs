using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model.EFrom;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using System.Data;


namespace VAN_OA.EFrom
{
    public partial class UseCarDetail : System.Web.UI.Page
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

            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写姓名！');</script>");
                txtName.Focus();

                return false;
            }

            if (txtDateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写申请日期！');</script>");
                txtDateTime.Focus();

                return false;
            }

            if (txtUseDate.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写使用时间！');</script>");
                txtUseDate.Focus();

                return false;
            }
            if (CommHelp.VerifesToDateTime(txtUseDate.Text)==false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('使用时间 格式错误！');</script>");
                txtUseDate.Focus();

                return false;
            }
            if (txtPONo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户名称！');</script>");
                txtGuestName.Focus();

                return false;
            }


            //if (txtroadLong.Text.Trim() == "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写实际里程数！');</script>");
            //    txtroadLong.Focus();

            //    return false;
            //}

            if (txtgoAddress.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写出发地！');</script>");
                txtgoAddress.Focus();

                return false;
            }


            if (txttoAddress.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写到达地！');</script>");
                txttoAddress.Focus();

                return false;
            }

            txtgoTime.Text = txtgoTime.Text.Trim().Replace('：', ':');
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('。', ':');
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('.', ':');

            txtendTime.Text = txtendTime.Text.Trim().Replace('：', ':');
            txtendTime.Text = txtendTime.Text.Trim().Replace('.', ':');
            txtendTime.Text = txtendTime.Text.Trim().Replace('。', ':');
            if (ddlPers.Visible == false && ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
            {
                //if (txtpers_car.Text.Trim() == "")
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写乘车人！');</script>");
                //    txtpers_car.Focus();

                //    return false;
                //}


                if (ddlUser.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择司机人！');</script>");
                    ddlUser.Focus();
                    return false;
                }

                //if (txtroadLong.Text == "")
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写路程！');</script>");
                //    txtroadLong.Focus();
                //    return false;
                //}
                if (txtFromRoadLong.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写启程里程数！');</script>");
                    txtFromRoadLong.Focus();
                    return false;
                }

                if (txtToRoadLong.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写回来里程数！');</script>");
                    txtToRoadLong.Focus();
                    return false;
                }
                try
                {
                    if (Convert.ToDecimal(txtFromRoadLong.Text) <= 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写启程里程数！');</script>");
                        txtFromRoadLong.Focus();
                        return false;
                    }

                    if (Convert.ToDecimal(txtToRoadLong.Text) <= 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写回来里程数！');</script>");
                        txtToRoadLong.Focus();
                        return false;
                    }
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的路程格式有误！');</script>");
                    txtFromRoadLong.Focus();
                    return false;

                }


                if (txtgoTime.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写出发时间！');</script>");
                    txtgoTime.Focus();
                    return false;
                }


                if (txtendTime.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写结束时间！');</script>");
                    txtgoTime.Focus();

                    return false;
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
                if (txtgoTime.Text != "")
                {
                    Convert.ToDateTime(txtgoTime.Text);
                }


                if (txtendTime.Text != "")
                {
                    Convert.ToDateTime(txtendTime.Text);
                }

                if (txtendTime.Text != "" && txtgoTime.Text != "")
                {
                    if (Convert.ToDateTime(txtgoTime.Text) >= Convert.ToDateTime(txtendTime.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外出时间不能大于回来时间！');</script>");
                        txtgoTime.Focus();

                        return false;

                    }
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtgoTime.Focus();

                return false;

            }


            try
            {

                if (txtFromRoadLong.Text != "")
                {
                    Convert.ToDecimal(txtFromRoadLong.Text);
                }


                if (txtToRoadLong.Text != "")
                {
                    Convert.ToDecimal(txtToRoadLong.Text);
                }

                if (txtFromRoadLong.Text != "" && txtToRoadLong.Text != "")
                {
                    if (Convert.ToDecimal(txtFromRoadLong.Text) >= Convert.ToDecimal(txtToRoadLong.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('启程里程数不能大于回来里程数！');</script>");
                        txtFromRoadLong.Focus();

                        return false;

                    }
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的里程数格式有误！');</script>");
                txtFromRoadLong.Focus();

                return false;

            }

            if (ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
            {
                if ((txtFromRoadLong.Text == "" && txtgoTime.Text != "") || (txtFromRoadLong.Text != "" && txtgoTime.Text == ""))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('启程里程数和外出时间要一起填写！');</script>");
                    txtFromRoadLong.Focus();

                    return false;
                }


                if ((txtToRoadLong.Text == "" && txtendTime.Text != "") || (txtToRoadLong.Text != "" && txtendTime.Text == ""))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('回来里程数和回来时间要一起填写！');</script>");
                    txtToRoadLong.Focus();

                    return false;
                }
            }

            //try
            //{
            //    Convert.ToDateTime(txtDateTime.Text);
            //    if (Convert.ToDateTime(txtgoTime.Text) >= Convert.ToDateTime(txtendTime.Text))
            //    {
            //        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('开始时间不能大于结束时间！');</script>");
            //        txtgoTime.Focus();

            //        return false;

            //    }

            //}
            //catch (Exception)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
            //    txtgoTime.Focus();

            //    return false;

            //}


            //try
            //{

            //    if (txtroadLong.Text != "")
            //    {
            //        Convert.ToDecimal(txtroadLong.Text);
            //    }
            //}
            //catch (Exception)
            //{

            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的实际里程数格式有误！');</script>");
            //    txtgoTime.Focus();

            //    return false;
            //}
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
            if (CG_POOrderService.IsSpecialPONO(txtPONo.Text, txtPOName.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                                        "<script>alert('特殊订单无法计入费用！');</script>");
                return false;
            }
            return true;
        }



        private void setEnable(bool result)
        {
            txtToRoadLong.ReadOnly = !result;

            if (ddlCarNo.Text.Trim() == "苏EAT085" || ddlCarNo.Text.Trim() == "苏E2N756")
            {
                txtFromRoadLong.ReadOnly = true;

            }
            else
            {
                txtFromRoadLong.ReadOnly = !result;
            }



            txtDateTime.ReadOnly = true;

            txtdeArea.ReadOnly = !result;
            txtendTime.ReadOnly = !result;
            txtgoAddress.ReadOnly = !result;
            txtgoTime.ReadOnly = !result;
            //txtName.ReadOnly = true;
            txtpers_car.ReadOnly = !result;
            //txtroadLong.ReadOnly = !result;
            txttoAddress.ReadOnly = !result;


            txtdeArea.ReadOnly = !result;
            txtGuestName.ReadOnly = !result;
            txtRemark.ReadOnly = !result;

            //  txtCarNo.ReadOnly = !result;
            ddlUser.Enabled = result;
            ddlCarNo.Enabled = result;
            txtUseDate.ReadOnly = !result;

            Image1.Enabled = result;
            imgendTime.Enabled = false;
            imggoTime.Enabled = false;

            if (result == true)
            {
                btnEdit.Visible = true;
            }
            if (result == true)
            {

                if (txtgoTime.Text == "")
                {
                    txtgoTime.ReadOnly = false;
                    imggoTime.Enabled = true;

                    if (ddlCarNo.Text.Trim() == "苏EAT085" || ddlCarNo.Text.Trim() == "苏E2N756")
                    {
                        txtFromRoadLong.ReadOnly = true;

                    }
                    else
                    {
                        txtFromRoadLong.ReadOnly = false;
                    }


                    txtendTime.ReadOnly = true;
                    imgendTime.Enabled = false;
                    txtToRoadLong.ReadOnly = true;
                }
                else if (txtendTime.Text == "")
                {
                    txtgoTime.ReadOnly = true;
                    imggoTime.Enabled = false;
                    txtFromRoadLong.ReadOnly = true;
                    txtToRoadLong.ReadOnly = false;
                    txtendTime.ReadOnly = false;
                    imgendTime.Enabled = true;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");
                user.Insert(0, new Model.User { LoginName = "" });
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "LoginName";

            

                DataTable carInfos = DBHelp.getDataTable("select CarNO from TB_CarInfo where IsStop=0");
                ddlCarNo.DataSource = carInfos;
                ddlCarNo.DataBind();
                


                //隐藏 发放用车人的信息
                lblDoPer.Visible = false;
                lblDoPerDesc.Visible = false;
                lblDoTime.Visible = false;
                lblDoTimeDesc.Visible = false;
                btnOtherClose.Visible = false;
                btnOtherSub.Visible = false;
                lblDoMess.Visible = true;
                //请假单子             

                btnEdit.Visible = false;

                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;

                    //获取油价信息
                    //if (tb_UseCar.CarOliPrice == 0)
                    //{
                    //    try
                    //    {
                    //        string url = System.Web.HttpContext.Current.Request.MapPath("CarOilPrice.txt");
                    //        System.IO.StreamReader my = new System.IO.StreamReader(url, System.Text.Encoding.Default);
                    //        string line;

                    //        line = my.ReadLine();

                    //        TB_UseCarDetail.CarOliPrice = Convert.ToDecimal(line);
                    //        my.Close();
                    //    }
                    //    catch (Exception)
                    //    {


                    //    }
                    //}


                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        txtFromRoadLong.ReadOnly = true;
                        txtToRoadLong.ReadOnly = true;
                        txtgoTime.ReadOnly = true;
                        imggoTime.Enabled = false;
                        txtToRoadLong.ReadOnly = true;
                        imgendTime.Enabled = false;

                        //检查自己是否在请假单中流程设置中，来确定自己下一个审批人

                        lblResult.Visible = false;
                        lblYiJian.Visible = false;
                        ddlResult.Visible = false;
                        txtResultRemark.Visible = false;


                        if (eformSer.ifHasNodes(Convert.ToInt32(Request["ProId"])))
                        {

                            //获取审批人

                            int ids = 0;
                            List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(Convert.ToInt32(Session["currentUserId"].ToString()), Convert.ToInt32(Request["ProId"]), out ids);

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
                                ddlPers.DataSource = roleUserList;
                                //}
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


                        TB_UseCarDetailService carSer = new TB_UseCarDetailService();
                        TB_UseCarDetail carModel = carSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtDateTime.Text = carModel.AppTime.ToString("yyyy-MM-dd hh:mm:ss");
                        txtUseDate.Text = carModel.UseDate.ToString("yyyy-MM-dd");


                        if (carModel.EndTime != null)
                            txtendTime.Text = carModel.EndTime.Value.ToString();
                        txtgoAddress.Text = carModel.GoAddress;
                        if (carModel.GoTime != null)
                            txtgoTime.Text = carModel.GoTime.Value.ToString();

                        txtName.Text = carModel.AppUser.ToString();
                        txtpers_car.Text = carModel.ByCarPers;
                        txtroadLong.Text = carModel.RoadLong.ToString();
                        txttoAddress.Text = carModel.ToAddress;
                        txtGuestName.Text = carModel.GuestName;
                        txtRemark.Text = carModel.Remark;
                        txtdeArea.Text = carModel.Area;
                        txtName.Text = carModel.AppUserName;
                        //txtDriver.Text = carModel.Driver;

                        try
                        {
                            ddlUser.Text = carModel.Driver;
                        }
                        catch (Exception)
                        {


                        }

                        txtPONo.Text = carModel.PONo;
                        txtPOName.Text = carModel.POName;
                        if (carModel.FromRoadLong != null)
                        {
                            txtFromRoadLong.Text = carModel.FromRoadLong.ToString();
                        }

                        if (carModel.ToRoadLong != null)
                        {
                            txtToRoadLong.Text = carModel.ToRoadLong.ToString();
                        }
                        try
                        {
                            ddlCarNo.Text = carModel.CarNo;
                        }
                        catch (Exception)
                        {


                        }

                        lblProNo.Text = carModel.ProNo;
                        if (carModel.DoTime != null)
                        {
                            lblDoPer.Visible = true;
                            lblDoPerDesc.Visible = true;
                            lblDoTime.Visible = true;
                            lblDoTimeDesc.Visible = true;

                            lblDoMess.Visible = false;
                            lblDoPer.Text = carModel.DoPer.ToString();
                            lblDoTime.Text = carModel.DoTime.ToString();
                        }
                        //if (carModel.Type == "单反")
                        //{
                        //    rdoDan.Checked = true;
                        //}
                        //else
                        //{
                        //    rdoWang.Checked = true;
                        //}

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
                                    if (carModel.DoTime == null)
                                    { 
                                        btnOtherSub.Visible = true;
                                        btnOtherClose.Visible = true;
                                        btnClose.Visible = false;
                                        btnEdit.Visible = false;
                                        btnSub.Visible = false;
                                    }
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
                                        //    List<A_Role_User> newList = new List<A_Role_User>();
                                        //    for (int i = 0; i < roleUserList.Count; i++)
                                        //    {
                                        //        if (roleUserList[i].UserId == use.ReportTo)
                                        //        {
                                        //            A_Role_User a = roleUserList[i];
                                        //            newList.Add(a);
                                        //            break;
                                        //        }
                                        //    }

                                        //    if (newList.Count > 0)
                                        //    {
                                        //        ddlPers.DataSource = newList;
                                        //    }
                                        //    else
                                        //    {
                                        ddlPers.DataSource = roleUserList;
                                        // }
                                        // ddlPers.DataSource = roleUserList;
                                        ddlPers.DataBind();
                                        ddlPers.DataTextField = "UserName";
                                        ddlPers.DataValueField = "UserId";
                                    }

                                } setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));

                            }
                            else
                            {
                                //是否为代理人
                                if (eformSer.ifAudiPerByUserName(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                    btnSub.Visible = false;
                                    lblPer.Visible = false;
                                    ddlPers.Visible = false;

                                    lblResult.Visible = false;
                                    lblYiJian.Visible = false;
                                    ddlResult.Visible = false;
                                    txtResultRemark.Visible = false;
                                    setEnable(false);

                                    if (carModel.DoTime == null)
                                    {
                                        ////获取本次审批人的级别 是否为第二级别
                                        //string sql = string.Format("select a_Index from A_ProInfos where  ids=(select toProsId from tb_EForm where proId={0} and allE_id={1})",
                                        //    Request["ProId"], Request["allE_id"]);
                                        //object a_index = DBHelp.ExeScalar(sql);
                                        //if (a_index != null && Convert.ToInt32(a_index) == 1)
                                        //{
                                        //    //btnOtherSub.Visible = true;
                                        //    //btnOtherClose.Visible = true;
                                        //    //btnClose.Visible = false;
                                        //    btnOtherSub.Text = "1";//1 说明需要发放钥匙
                                        //}

                                    }
                                    else
                                    {

                                        btnOtherSub.Visible = false;
                                        btnOtherClose.Visible = false;
                                    }
                                    //ViewState["ifConsignor"] = true;
                                    //if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                    //{
                                    //    lblPer.Visible = false;
                                    //    ddlPers.Visible = false;
                                    //}
                                    //else
                                    //{
                                    //    int ids = 0;
                                    //    List<A_Role_User> roleUserList = eformSer.getUserToAdu(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);
                                    //    ViewState["ids"] = ids;
                                    //    if (roleUserList != null)
                                    //    {

                                    //        ddlPers.DataSource = roleUserList;

                                    //        ddlPers.DataBind();
                                    //        ddlPers.DataTextField = "UserName";
                                    //        ddlPers.DataValueField = "UserId";
                                    //    }

                                    //}
                                    //setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
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
                            if (carModel.DoTime == null)
                            {
                                txtgoTime.ReadOnly = true;
                                imggoTime.Enabled = false;
                                txtFromRoadLong.ReadOnly = true;
                            }
                            else
                            {


                                btnOtherSub.Visible = false;
                                btnOtherClose.Visible = false;

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

                    TB_UseCarDetail carInfo = new TB_UseCarDetail();
                    carInfo.AppUser = Convert.ToInt32(Session["currentUserId"]);
                    carInfo.AppTime = Convert.ToDateTime(txtDateTime.Text);
                    carInfo.Area = txtdeArea.Text;
                    if (txtendTime.Text != "")
                    {
                        carInfo.EndTime = Convert.ToDateTime(txtendTime.Text);
                    }
                    carInfo.GoAddress = txtgoAddress.Text;
                    carInfo.Area = txtdeArea.Text;
                    if (txtgoTime.Text != "")
                    {
                        carInfo.GoTime = Convert.ToDateTime(txtgoTime.Text);
                    }
                    carInfo.GuestName = txtGuestName.Text;
                    carInfo.ByCarPers = txtpers_car.Text;
                    if (txtroadLong.Text != "")
                        carInfo.RoadLong = Convert.ToDecimal(txtroadLong.Text);
                    carInfo.ToAddress = txttoAddress.Text;
                    carInfo.Remark = txtRemark.Text;
                    carInfo.CarNo = ddlCarNo.Text;
                    carInfo.Driver = ddlUser.Text;

                    if (txtToRoadLong.Text != "")
                    {
                        carInfo.ToRoadLong = Convert.ToDecimal(txtToRoadLong.Text);
                    }

                    if (txtFromRoadLong.Text != "")
                    {
                        carInfo.FromRoadLong = Convert.ToDecimal(txtFromRoadLong.Text);
                    }

                    if (txtToRoadLong.Text != "" && txtFromRoadLong.Text != "")
                    {
                        carInfo.RoadLong = Convert.ToDecimal(txtToRoadLong.Text) - Convert.ToDecimal(txtFromRoadLong.Text);
                    }
                    //if (rdoDan.Checked)
                    //{
                    //    carInfo.Type = "单程";
                    //}
                    //else
                    //{
                    //    carInfo.Type = "往返";
                    //}


                    carInfo.PONo = txtPONo.Text;
                    carInfo.POName = txtPOName.Text;
                    carInfo.UseDate = Convert.ToDateTime(txtUseDate.Text);
                    string getXs = string.Format("select OilNumber from TB_CarInfo where CarNo='{0}'", carInfo.CarNo);
                    var oilXs = DBHelp.ExeScalar(getXs);
                    carInfo.OilPrice = Convert.ToDecimal(oilXs);

                    if (carInfo.OilPrice <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('油价系数必须大于0！');</script>");
                        return;
                    }
                    //carInfo.OilPrice = TB_UseCarDetail.CarOliPrice;
                    #endregion
                    if (Request["allE_id"] == null)//单据增加
                    {


                        VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

                        int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                        eform.appPer = userId;
                        eform.appTime = Convert.ToDateTime(txtDateTime.Text);
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
                        TB_UseCarDetailService UseCarDetailSer = new TB_UseCarDetailService();

                        if (UseCarDetailSer.addTran(carInfo, eform) > 0)
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
                        carInfo.Id = Convert.ToInt32(Request["allE_id"]);
                        #endregion
                        tb_EForm eform = new tb_EForm();
                        tb_EForms forms = new tb_EForms();


                        eform.id = Convert.ToInt32(Request["EForm_Id"]);
                        eform.proId = Convert.ToInt32(Request["ProId"]);
                        eform.allE_id = Convert.ToInt32(Request["allE_id"]);
                        int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                        eform.appPer = userId;
                        eform.appTime = Convert.ToDateTime(txtDateTime.Text);



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

                       
                        TB_UseCarDetailService UseCarDetailSer = new TB_UseCarDetailService();
                        if (UseCarDetailSer.updateTran(carInfo, eform, forms))
                        {
                            FaYaoShi();
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

            #region MyRegion
            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写姓名！');</script>");
                txtName.Focus();

                return;
            }

            if (txtDateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写申请日期！');</script>");
                txtDateTime.Focus();

                return;
            }

            if (txtGuestName.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户名称！');</script>");
                txtGuestName.Focus();

                return;
            }



            if (txtgoAddress.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写出发地！');</script>");
                txtgoAddress.Focus();

                return;
            }


            if (txttoAddress.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写到达地！');</script>");
                txttoAddress.Focus();

                return;
            }

            txtgoTime.Text = txtgoTime.Text.Trim().Replace('：', ':');
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('。', ':');
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('.', ':');

            txtendTime.Text = txtendTime.Text.Trim().Replace('：', ':');
            txtendTime.Text = txtendTime.Text.Trim().Replace('.', ':');
            txtendTime.Text = txtendTime.Text.Trim().Replace('。', ':');

            try
            {
                if (txtgoTime.Text != "")
                {
                    Convert.ToDateTime(txtgoTime.Text);
                }


                if (txtendTime.Text != "")
                {
                    Convert.ToDateTime(txtendTime.Text);
                }

                if (txtendTime.Text != "" && txtgoTime.Text != "")
                {
                    if (Convert.ToDateTime(txtgoTime.Text) >= Convert.ToDateTime(txtendTime.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外出时间不能大于回来时间！');</script>");
                        txtgoTime.Focus();

                        return;

                    }
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtgoTime.Focus();

                return;

            }

            try
            {

                if (txtFromRoadLong.Text != "")
                {
                    Convert.ToDecimal(txtFromRoadLong.Text);
                }


                if (txtToRoadLong.Text != "")
                {
                    Convert.ToDecimal(txtToRoadLong.Text);
                }

                if (txtFromRoadLong.Text != "" && txtToRoadLong.Text != "")
                {
                    if (Convert.ToDecimal(txtFromRoadLong.Text) >= Convert.ToDecimal(txtToRoadLong.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('启程里程数不能大于回来里程数！');</script>");
                        txtFromRoadLong.Focus();

                        return;

                    }
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的里程数格式有误！');</script>");
                txtFromRoadLong.Focus();

                return;

            }
            if ((txtFromRoadLong.Text == "" && txtgoTime.Text != "") || (txtFromRoadLong.Text != "" && txtgoTime.Text == ""))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('启程里程数和外出时间要一起填写！');</script>");
                txtFromRoadLong.Focus();

                return;
            }


            if ((txtToRoadLong.Text == "" && txtendTime.Text != "") || (txtToRoadLong.Text != "" && txtendTime.Text == ""))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('回来里程数和回来时间要一起填写！');</script>");
                txtToRoadLong.Focus();

                return;
            }

            //try
            //{

            //    if (txtroadLong.Text != "")
            //    {
            //        Convert.ToDecimal(txtroadLong.Text);
            //    }
            //}
            //catch (Exception)
            //{

            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的实际里程数格式有误！');</script>");
            //    txtgoTime.Focus();

            //    return ;
            //}
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtName.Focus();

                return;
            }

            #endregion



            TB_UseCarDetail carInfo = new TB_UseCarDetail();
            carInfo.AppUser = Convert.ToInt32(Session["currentUserId"]);
            carInfo.AppTime = Convert.ToDateTime(txtDateTime.Text);
            carInfo.Area = txtdeArea.Text;
            if (txtendTime.Text != "")
            {
                carInfo.EndTime = Convert.ToDateTime(txtendTime.Text);
            }
            carInfo.GoAddress = txtgoAddress.Text;
            carInfo.Area = txtdeArea.Text;
            if (txtgoTime.Text != "")
            {
                carInfo.GoTime = Convert.ToDateTime(txtgoTime.Text);
            }
            carInfo.GuestName = txtGuestName.Text;
            carInfo.ByCarPers = txtpers_car.Text;
            if (txtroadLong.Text != "")
                carInfo.RoadLong = Convert.ToDecimal(txtroadLong.Text);
            carInfo.ToAddress = txttoAddress.Text;
            carInfo.Remark = txtRemark.Text;
            carInfo.CarNo = ddlCarNo.Text;
            carInfo.Driver = ddlUser.Text;

            if (txtToRoadLong.Text != "")
            {
                carInfo.ToRoadLong = Convert.ToDecimal(txtToRoadLong.Text);
            }

            if (txtFromRoadLong.Text != "")
            {
                carInfo.FromRoadLong = Convert.ToDecimal(txtFromRoadLong.Text);
            }

            if (txtToRoadLong.Text != "" && txtFromRoadLong.Text != "")
            {
                carInfo.RoadLong = Convert.ToDecimal(txtToRoadLong.Text) - Convert.ToDecimal(txtFromRoadLong.Text);
            }
            //if (rdoDan.Checked)
            //{
            //    carInfo.Type = "单程";
            //}
            //else
            //{
            //    carInfo.Type = "往返";
            //}
            #region 本单据的ID
            carInfo.Id = Convert.ToInt32(Request["allE_id"]);
            #endregion
            TB_UseCarDetailService UseCarDetailSer = new TB_UseCarDetailService();

            if (UseCarDetailSer.Update(carInfo))
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");

            }
            else
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");

            }
        }

        /// <summary>
        /// 发放钥匙
        /// </summary>
        private void FaYaoShi()
        {
            string sql = string.Format("select a_Index from A_ProInfos where  ids=(select toProsId from tb_EForm where proId={0} and allE_id={1})",
                                            Request["ProId"], Request["allE_id"]);
            object a_index = DBHelp.ExeScalar(sql);
            if (a_index != null && Convert.ToInt32(a_index) == 0)
            {
                if (Request["ProId"] != null && Request["allE_id"] != null)
                {
                    if (ddlCarNo.Text.Trim() == "苏EAT085" || ddlCarNo.Text.Trim() == "苏E2N756")
                    {
                        //===2012-01-10
                        //查询这辆车最后一次的已经发放钥匙的 申请时间
                        string updateSql = string.Format("select top 1 ToRoadLong from TB_UseCarDetail where CarNo='{0}' and EndTime is not null and ToRoadLong is not null order by EndTime desc", ddlCarNo.Text);
                        object obj = DBHelp.ExeScalar(updateSql);
                        decimal fromRoadLong = 1;
                        if (obj != null)
                        {
                            fromRoadLong = Convert.ToDecimal(obj);
                        }
                        //===

                        sql = string.Format(" update TB_UseCarDetail set DoPer='{0}',DoTime=getdate(),FromRoadLong={4} where Id={1};update tb_EForm set e_Remark='已发放' where proId={2} and allE_id={3};",
                           base.Session["LoginName"].ToString(), Request["allE_id"], Request["ProId"], Request["allE_id"], fromRoadLong);
                        DBHelp.ExeCommand(sql);

                    }
                    else
                    {
                        sql = string.Format(" update TB_UseCarDetail set DoPer='{0}',DoTime=getdate() where Id={1};update tb_EForm set e_Remark='已发放' where proId={2} and allE_id={3};",
                           base.Session["LoginName"].ToString(), Request["allE_id"], Request["ProId"], Request["allE_id"]);
                        DBHelp.ExeCommand(sql);
                    }
                }
            }
        }

        protected void btnOtherSub_Click(object sender, EventArgs e)
        {
            FaYaoShi();
            if (Session["backurl"] != null)
            {
                base.Response.Redirect("~" + Session["backurl"]);
            }
            else
            {
                base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
            }
        }
        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                CG_POOrder model = POSer.GetModel(Convert.ToInt32(Session["Comm_CGPONo"]));
                txtGuestName.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
                Session["Comm_CGPONo"] = null;
            }

        }

        protected void rdoPhone_CheckedChanged(object sender, EventArgs e)
        {
            txtPONo.ReadOnly = false;         
            lbtnSelectPONo.Visible = false;
        }

        protected void rdoSelect_CheckedChanged(object sender, EventArgs e)
        {
            txtPONo.ReadOnly = true;         
            lbtnSelectPONo.Visible = true;
        }

        protected void txtPONo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPONo.Text))
            {
                CG_POOrder model = POSer.GetModel(txtPONo.Text);
                if (model == null)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目编码不存在！');</script>");
                    return;
                }
                txtGuestName.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
            }
        }



    }
}
