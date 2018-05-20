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
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;


namespace VAN_OA.EFrom
{
    public partial class UseCarDetailMan : System.Web.UI.Page
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

            if (txtGuestName.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户名称！');</script>");
                txtGuestName.Focus();

                return false;
            }


            

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
            if (ddlPers.Visible == false)
            {
                

                if (txtDriver.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写司机人！');</script>");
                    txtDriver.Focus();
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
                    Convert.ToDateTime( txtgoTime.Text);
                }


                if (txtendTime.Text != "")
                {
                    Convert.ToDateTime( txtendTime.Text);
                }

                if (txtendTime.Text != "" && txtgoTime.Text != "")
                {
                    if (Convert.ToDateTime( txtgoTime.Text) >= Convert.ToDateTime(txtendTime.Text))
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

                if (txtroadLong.Text != "")
                {
                    Convert.ToDecimal(txtroadLong.Text);
                }
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的实际里程数格式有误！');</script>");
                txtgoTime.Focus();

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
            txtDateTime.Enabled = false;

            
            //txtName.ReadOnly =true;
            Image1.Enabled = false;  
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {     
                DataTable carInfos = DBHelp.getDataTable("select ''as CarNO union select CarNO from TB_CarInfo");
                ddlCarNo.DataSource = carInfos;
                ddlCarNo.DataBind();             

                //请假单子              

                btnEdit.Visible = false;
                btnSub.Visible = false;
                btnEdit.Visible = true;

                if (base.Request["ProId"] != null)
                {
                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;


                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                       
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
                        txtDateTime.Text = carModel.AppTime.ToLongDateString().ToString();
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
                        txtDriver.Text = carModel.Driver;

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
                        //if (carModel.Type == "单反")
                        //{
                        //    rdoDan.Checked = true;
                        //}
                        //else
                        //{
                        //    rdoWang.Checked = true;
                        //}
                        lblProNo.Text = carModel.ProNo;
                        #endregion
                        //判断单据是否已经结束
                        if (eformSer.ifFinish(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                        {
                            btnSub.Visible = false;
                            lblPer.Visible = false;
                            ddlPers.Visible = false;
                          
                           


                            setEnable(false);
                        }
                        else
                        {
                            //是否为审核人


                         
                            int ids = 0;

                            List<A_Role_User> roleUserList = eformSer.getUserToCurrnetEform(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]), out ids);

                            ViewState["ids"] = ids;
                            if (roleUserList != null)
                            { 
                                ddlPers.DataSource = roleUserList;
                                
                                ddlPers.DataBind();
                                ddlPers.DataTextField = "UserName";
                                ddlPers.DataValueField = "UserId";
                                try
                                {
                                    string perID=string.Format("select toPer from tb_EForm where id={0}", Request["EForm_Id"]);

                                    ddlPers.Text = DBHelp.ExeScalar(perID).ToString();
                                }
                                catch (Exception)
                                {
                                    
                                     
                                }
                            }

                               
                                setEnable(true);                            
                           
                        }
                    }





                }

            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        { 
        }
        //{
        //    if (FormCheck())
        //    {

        //        btnSub.Enabled = false;
        //        if (base.Request["ProId"] != null)
        //        {

        //            #region 获取单据基本信息

        //            TB_UseCarDetail carInfo = new TB_UseCarDetail();
        //            carInfo.AppUser = Convert.ToInt32(Session["currentUserId"]);
        //            carInfo.AppTime = Convert.ToDateTime(txtDateTime.Text);
        //            carInfo.Area = txtdeArea.Text;
        //            if (txtendTime.Text != "")
        //            {
        //                carInfo.EndTime = Convert.ToDateTime(txtDateTime.Text + " " + txtendTime.Text);
        //            }
        //            carInfo.GoAddress = txtgoAddress.Text;
        //            carInfo.Area = txtdeArea.Text;
        //            if (txtgoTime.Text != "")
        //            {
        //                carInfo.GoTime = Convert.ToDateTime(txtDateTime.Text + " " + txtgoTime.Text);
        //            }
        //            carInfo.GuestName = txtGuestName.Text;
        //            carInfo.ByCarPers = txtpers_car.Text;
        //            if (txtroadLong.Text != "")
        //                carInfo.RoadLong = Convert.ToDecimal(txtroadLong.Text);
        //            carInfo.ToAddress = txttoAddress.Text;
        //            carInfo.Remark = txtRemark.Text;
        //            carInfo.CarNo = ddlCarNo.Text;
        //            carInfo.Driver = txtDriver.Text;
        //            if (rdoDan.Checked)
        //            {
        //                carInfo.Type = "单程";
        //            }
        //            else
        //            {
        //                carInfo.Type = "往返";
        //            }

        //            #endregion
        //            if (Request["allE_id"] == null)//单据增加
        //            {
        //                VAN_OA.Model.EFrom.tb_EForm eform = new tb_EForm();

        //                int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
        //                eform.appPer = userId;
        //                eform.appTime = Convert.ToDateTime(txtDateTime.Text);
        //                eform.createPer = Convert.ToInt32(Session["currentUserId"].ToString());
        //                eform.createTime = DateTime.Now;
        //                eform.proId = Convert.ToInt32(Request["ProId"]);

        //                if (ddlPers.Visible == false)
        //                {
        //                    eform.state = "通过";
        //                    eform.toPer = 0;
        //                    eform.toProsId = 0;
        //                }
        //                else
        //                {

        //                    eform.state = "执行中";
        //                    eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
        //                    eform.toProsId = Convert.ToInt32(ViewState["ids"]);
        //                }
        //                TB_UseCarDetailService UseCarDetailSer = new TB_UseCarDetailService();

        //                if (UseCarDetailSer.addTran(carInfo, eform) > 0)
        //                {

        //                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");

        //                    if (Session["backurl"] != null)
        //                    {
        //                        base.Response.Redirect("~" + Session["backurl"]);
        //                    }
        //                    else
        //                    {
        //                        base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
        //                    }
        //                }
        //                else
        //                {
        //                    btnSub.Enabled = false;
        //                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");
        //                }
        //            }
        //            else//审核
        //            {




        //                #region 本单据的ID
        //                carInfo.Id = Convert.ToInt32(Request["allE_id"]);
        //                #endregion
        //                tb_EForm eform = new tb_EForm();
        //                tb_EForms forms = new tb_EForms();


        //                eform.id = Convert.ToInt32(Request["EForm_Id"]);
        //                eform.proId = Convert.ToInt32(Request["ProId"]);
        //                eform.allE_id = Convert.ToInt32(Request["allE_id"]);
        //                int userId = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
        //                eform.appPer = userId;
        //                eform.appTime = Convert.ToDateTime(txtDateTime.Text);



        //                tb_EFormService fromSer = new tb_EFormService();
        //                if (ViewState["ifConsignor"] != null && Convert.ToBoolean(ViewState["ifConsignor"]) == true)
        //                {
        //                    forms.audPer = fromSer.getCurrentAuPer(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
        //                    forms.consignor = Convert.ToInt32(Session["currentUserId"]);
        //                }
        //                else
        //                {
        //                    forms.audPer = Convert.ToInt32(Session["currentUserId"]);
        //                    forms.consignor = 0;
        //                }

        //                forms.doTime = DateTime.Now;
        //                forms.e_Id = Convert.ToInt32(Request["EForm_Id"]); //fromSer.getCurrentid(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
        //                forms.idea = txtResultRemark.Text;
        //                forms.prosIds = fromSer.getCurrenttoProsId(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
        //                forms.resultState = ddlResult.Text;
        //                forms.RoleName = fromSer.getCurrenttoRoleName(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
        //                if (ddlPers.Visible == false)//说明为最后一次审核
        //                {





        //                    eform.state = ddlResult.Text;
        //                    eform.toPer = 0;
        //                    eform.toProsId = 0;






        //                }
        //                else
        //                {
        //                    if (ddlResult.Text == "不通过")
        //                    {

        //                        eform.state = ddlResult.Text;
        //                        eform.toPer = 0;
        //                        eform.toProsId = 0;




        //                    }
        //                    else
        //                    {

        //                        eform.state = "执行中";
        //                        eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
        //                        eform.toProsId = Convert.ToInt32(ViewState["ids"]);

        //                    }
        //                }
        //                TB_UseCarDetailService UseCarDetailSer = new TB_UseCarDetailService();
        //                if (UseCarDetailSer.updateTran(carInfo, eform, forms))
        //                {
        //                    // btnSub.Enabled = true;
        //                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");
        //                    if (Session["backurl"] != null)
        //                    {
        //                        base.Response.Redirect("~" + Session["backurl"]);
        //                    }
        //                    else
        //                    {
        //                        base.Response.Redirect("~/EFrom/MyRequestEForms.aspx");
        //                    }
        //                }
        //                else
        //                {
        //                    btnSub.Enabled = false;
        //                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交失败！');</script>");

        //                }
        //            }
        //        }
        //    }
        //}

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
                    Convert.ToDateTime( txtgoTime.Text);
                }


                if (txtendTime.Text != "")
                {
                    Convert.ToDateTime( txtendTime.Text);
                }

                if (txtendTime.Text != "" && txtgoTime.Text != "")
                {
                    if (Convert.ToDateTime(txtgoTime.Text) >= Convert.ToDateTime( txtendTime.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('外出时间不能大于回来时间！');</script>");
                        txtgoTime.Focus();

                        return ;

                    }
                }

            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                txtgoTime.Focus();

                return ;

            }



            try
            {

                if (txtroadLong.Text != "")
                {
                    Convert.ToDecimal(txtroadLong.Text);
                }
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的实际里程数格式有误！');</script>");
                txtgoTime.Focus();

                return ;
            }
            if (DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)) == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写申请用户不存在！');</script>");
                txtName.Focus();

                return ;
            }

            #endregion



            TB_UseCarDetail carInfo = new TB_UseCarDetail();
            carInfo.AppUser = Convert.ToInt32(Session["currentUserId"]);
            carInfo.AppTime = Convert.ToDateTime(txtDateTime.Text);
            carInfo.Area = txtdeArea.Text;
            if (txtendTime.Text != "")
            {
                carInfo.EndTime = Convert.ToDateTime( txtendTime.Text);
            }
            carInfo.GoAddress = txtgoAddress.Text;
            carInfo.Area = txtdeArea.Text;
            if (txtgoTime.Text != "")
            {
                carInfo.GoTime = Convert.ToDateTime( txtgoTime.Text);
            }
            carInfo.GuestName = txtGuestName.Text;
            carInfo.ByCarPers = txtpers_car.Text;
            if (txtroadLong.Text != "")
                carInfo.RoadLong = Convert.ToDecimal(txtroadLong.Text);
            carInfo.ToAddress = txttoAddress.Text;
            carInfo.Remark = txtRemark.Text;
            carInfo.CarNo = ddlCarNo.Text;
            carInfo.Driver = txtDriver.Text;

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


            string sqlCheck = string.Format("select toProsId from tb_EForm where id={0}", Request["EForm_Id"]);
            object toProsId = DBHelp.ExeScalar(sqlCheck);
            if (toProsId != null)
            {
                if (Convert.ToInt32(toProsId) != Convert.ToInt32(ViewState["ids"]))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('本次单据信息已经被修改,请重新进入！');</script>");
                    return;
                }
            
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('本次单据信息已经被修改,请重新进入！');</script>");
                return;
            }
          
            TB_UseCarDetailService UseCarDetailSer = new TB_UseCarDetailService();

            tb_EForm eform = new tb_EForm();
            


            eform.id = Convert.ToInt32(Request["EForm_Id"]);

            eform.toPer = Convert.ToInt32(ddlPers.SelectedItem.Value);
            eform.toProsId = Convert.ToInt32(ViewState["ids"]);

            if (UseCarDetailSer.updateTran(carInfo, eform))
            {
                 
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                
            }
            else
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");

            }
        }

    }
}
