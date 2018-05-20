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
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using VAN_OA.Model;

namespace VAN_OA.EFrom
{
    public partial class UseCar : System.Web.UI.Page
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


        public void GetTotal()
        {
            try
            {
                lblTotal.Text = (Convert.ToDecimal(txtroadLong.Text) * Convert.ToDecimal(txtOiLXiShu.Text)).ToString();
            }
            catch (Exception)
            {
                
               
            }
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

            if (CommHelp.VerifesToDateTime(txtDateTime.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('申请日期 格式错误！');</script>");
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

            if (txtgoTime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写出发时间！');</script>");
                txtgoTime.Focus();
                return false;
            }
           
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('：', ':');
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('。', ':');
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('.', ':');

            txtendTime.Text = txtendTime.Text.Trim().Replace('：', ':');
            txtendTime.Text = txtendTime.Text.Trim().Replace('.', ':');
            txtendTime.Text = txtendTime.Text.Trim().Replace('。', ':');

            User user = Session["userInfo"] as User;
            if ((user.Zhiwu == "总经理" || user.Zhiwu == "副总经理")&&ddlResult.SelectedItem.Text == "通过")
            {
                if (txtOiLXiShu.Text == "0")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写油价系数！');</script>");
                    txtOiLXiShu.Focus();
                    return false;
                }

            }

            if (ddlPers.Visible == false && ddlResult.SelectedItem != null && ddlResult.SelectedItem.Text == "通过")
            {

                //if (txtOiLXiShu.Text == "0")
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写油价系数！');</script>");
                //    txtOiLXiShu.Focus();
                //    return false;
                //}

                //if (txtroadLong.Text == "")
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写路程！');</script>");
                //    txtroadLong.Focus();
                //    return false;
                //}

                try
                {
                    if (txtroadLong.Text == "")
                        txtroadLong.Text = "0";
                    if (Convert.ToDecimal(txtroadLong.Text) < 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写路程！');</script>");
                        txtroadLong.Focus();
                        return false;
                    }
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的路程格式有误！');</script>");
                    txtroadLong.Focus();
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

            if (Request["allE_id"] == null)//单据增加
            {
                if (txtPONo.Text == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择项目信息！');</script>");
                    txtPONo.Focus();
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

 
                    //try
                    //{
                    //    TimeSpan ts = Convert.ToDateTime(txtDateTime.Text) - Convert.ToDateTime(txtendTime.Text);
                    //    if (ts.Days != 0)
                    //    {
                    //        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('回来时间要与申请时间要在同一天！');</script>");
                    //        txtendTime.Focus();
                    //        return false;
                    //    }
                    //}
                    //catch (Exception)
                    //{

                    //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                    //    txtendTime.Focus();
                    //    return false;
                    //}
                
                }

                if (txtendTime.Text != "" && txtgoTime.Text != "")
                {
                    if (Convert.ToDateTime(txtgoTime.Text) >= Convert.ToDateTime( txtendTime.Text))
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
                if (txtroadLong.Text == "")
                    txtroadLong.Text = "0";
                Convert.ToDecimal(txtroadLong.Text);
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

            return true;
        }



        private void setEnable(bool result)
        {
            txtDateTime.ReadOnly =true;
            //Image1.Enabled =false;
            txtdeAddress.ReadOnly = !result;
            txtendTime.ReadOnly = !result;
            txtgoAddress.ReadOnly = !result;
            txtgoTime.ReadOnly = !result;
            //txtName.ReadOnly =true;
            txtpers_car.ReadOnly = !result;
            txtroadLong.ReadOnly = !result;
            txttoAddress.ReadOnly = !result;
            txtuseReason.ReadOnly = !result;
            rdoDan.Enabled = result;
            rdoWang.Enabled = result;

            txtgoTime.ReadOnly =true;
            imggoTime.Enabled = false;

            txtendTime.ReadOnly =true;
            imgendTime.Enabled = false;


            lbtnSelectPONo.Visible = false;
            if (result == true)
            {
                btnEdit.Visible = true;
            }
            if (result == true)
            {

                if (txtgoTime.Text == "")
                {
                    txtgoTime.ReadOnly =false;
                    imggoTime.Enabled = true;

                    txtendTime.ReadOnly =true;
                    imgendTime.Enabled = false;

                }
                else if (txtendTime.Text == "")
                {
                    txtgoTime.ReadOnly =true;
                    imggoTime.Enabled = false;

                    txtendTime.ReadOnly =false;
                    imgendTime.Enabled = true;
                }
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //请假单子              
                btnEdit.Visible = false;
                btnFinSub.Visible = false;

                txtOiLXiShu.ReadOnly = true;
                txtOiLXiShu.Text = "0";
                //获取油价信息
                if (tb_UseCar.CarOliPrice == 0)
                {
                    try
                    {
                        string url = System.Web.HttpContext.Current.Request.MapPath("CarOilPrice.txt");
                        System.IO.StreamReader my = new System.IO.StreamReader(url, System.Text.Encoding.Default);
                        string line;

                        line = my.ReadLine();
                       
                        tb_UseCar.CarOliPrice = Convert.ToDecimal(line);
                        my.Close();
                    }
                    catch (Exception)
                    {
                        
                        
                    }             
                }
                if (base.Request["ProId"] != null)
                {

                    txtendTime.ReadOnly = true;
                    imgendTime.Enabled = false;

                    //加载基本数据
                    VAN_OA.Model.User use = Session["userInfo"] as VAN_OA.Model.User;
                    txtName.Text = use.LoginName;
                  

                    tb_EFormService eformSer = new tb_EFormService();
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        txtroadLong.ReadOnly = true;
                        rdoDan.Checked = true;

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

                        tb_UseCarService carSer = new tb_UseCarService();

                        tb_UseCar carModel = carSer.GetModel(Convert.ToInt32(Request["allE_id"]));
                        txtDateTime.Text = carModel.datetime.ToString("yyyy-MM-dd hh:mm:ss");
                        txtdeAddress.Text = carModel.deAddress;

                        if (carModel.endTime!=null)
                        txtendTime.Text = carModel.endTime.Value.ToString();
                        txtgoAddress.Text = carModel.goAddress;

                        if(carModel.goTime!=null)
                        txtgoTime.Text = carModel.goTime.Value.ToString();


                        txtName.Text = carModel.LoginName;
                        txtpers_car.Text = carModel.pers_car;
                        txtroadLong.Text = carModel.roadLong.ToString();

                        txttoAddress.Text = carModel.toAddress;
                        txtuseReason.Text = carModel.useReason;
                        if (carModel.type == "单反")
                        {
                            rdoDan.Checked = true;
                        }
                        else
                        {
                            rdoWang.Checked = true;
                        } 
                        
                        lblProNo.Text = carModel.ProNo;

                        txtPOGuestName.Text = carModel.POGuestName;
                        txtPOName.Text = carModel.POName;
                        txtPONo.Text = carModel.PONo;
                        txtOiLXiShu.Text = carModel.OilPrice.ToString();
                        #endregion
                        GetTotal();

                        
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
                            User user = Session["userInfo"] as User;
                            if (user.Zhiwu == "总经理" || user.Zhiwu == "副总经理")
                            {
                                txtOiLXiShu.ReadOnly = false;
                            }
                            //是否为审核人
                            if (eformSer.ifAudiPer(Convert.ToInt32(Session["currentUserId"]), Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                            {

                                if (eformSer.ifLastNode(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])))
                                {
                                   
                                    lblPer.Visible = false;
                                    ddlPers.Visible = false;
                                    //setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
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
                                        //    ddlPers.DataSource = newList;
                                        //}
                                        //else
                                        //{
                                            ddlPers.DataSource = roleUserList;
                                       // }
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
                                        //txtOiLXiShu.ReadOnly = false;
                                        lblPer.Visible = false;
                                        ddlPers.Visible = false;
                                        //setEnable(eformSer.ifEdit(Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"])));
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
                                                ddlPers.DataSource = roleUserList;
                                           // }
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


                        //txtgoTime.Enabled = false;
                        //imggoTime.Enabled = false;
                        if (txtendTime.Text == "")
                        {
                            //判断该单据是否为自己申请
                            string sql = string.Format("select  appPer from tb_EForm where proId={0} and allE_id={1}", Convert.ToInt32(Request["ProId"]), Convert.ToInt32(Request["allE_id"]));
                            if (Session["currentUserId"].ToString() == DBHelp.ExeScalar(sql).ToString())
                            {
                                setEnable(false);
                                
                                btnFinSub.Visible = true;


                                if (txtgoTime.Text == "")
                                {
                                    txtgoTime.ReadOnly = false;
                                    imggoTime.Enabled = true;

                                    txtendTime.ReadOnly = true;
                                    imgendTime.Enabled = false;

                                }
                                else if (txtendTime.Text == "")
                                {
                                    txtgoTime.ReadOnly = true;
                                    imggoTime.Enabled = false;

                                    txtendTime.ReadOnly = false;
                                    imgendTime.Enabled = true;
                                }
                                //txtendTime.Enabled = true;
                                //imgendTime.Enabled = true;
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

                    tb_UseCar carInfo = new tb_UseCar();
                    carInfo.appName = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
                    carInfo.datetime = Convert.ToDateTime(txtDateTime.Text);
                    carInfo.deAddress = txtdeAddress.Text;
                 

                    if (txtendTime.Text != "")
                    {
                        carInfo.endTime = Convert.ToDateTime( txtendTime.Text);
                    }
                    if (txtgoTime.Text != "")
                    {
                        carInfo.goTime = Convert.ToDateTime( txtgoTime.Text);
                    }
                    carInfo.goAddress = txtgoAddress.Text;                   
                    carInfo.pers_car = txtpers_car.Text;
                    if (txtroadLong.Text!="")
                    carInfo.roadLong = Convert.ToDecimal(txtroadLong.Text);
                    carInfo.toAddress = txttoAddress.Text;
                    carInfo.useReason = txtuseReason.Text;
                    if (rdoDan.Checked)
                    {
                        carInfo.type = "单程";
                    }
                    else
                    {
                        carInfo.type = "往返";
                    }

                    carInfo.POGuestName = txtPOGuestName.Text;
                    carInfo.POName = txtPOName.Text;
                    carInfo.PONo = txtPONo.Text;

                    carInfo.OilPrice =Convert.ToDecimal( txtOiLXiShu.Text);
                    #endregion
                    if (Request["allE_id"] == null)//单据增加
                    {
                        txtendTime.Enabled = false;

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
                        tb_UseCarService carInfoSer = new tb_UseCarService();

                        if (carInfoSer.addTran(carInfo, eform) > 0)
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
                        carInfo.id = Convert.ToInt32(Request["allE_id"]);
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
                        tb_UseCarService carInfoSer = new tb_UseCarService();
                        if (carInfoSer.updateTran(carInfo, eform, forms))
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
            if (txtName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写姓名！');</script>");
                txtName.Focus();

                return ;
            }

            if (txtDateTime.Text.Trim() == "")
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写申请日期！');</script>");
                txtDateTime.Focus();

                return ;
            }
        
 

            if (txtgoAddress.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写出发地！');</script>");
                txtgoAddress.Focus();

                return ;
            }


            if (txtgoTime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写出发时间！');</script>");
                txtgoTime.Focus();
                return;
            }

            else
            {
                try
                {
                    TimeSpan ts = Convert.ToDateTime(txtDateTime.Text) - Convert.ToDateTime(txtgoTime.Text);
                    if (ts.Days != 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('出发时间要与申请时间要在同一天！');</script>");
                        txtgoTime.Focus();
                        return ;
                    }
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                    txtgoTime.Focus();
                    return ;
                }
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

                    try
                    {
                        TimeSpan ts = Convert.ToDateTime(txtDateTime.Text) - Convert.ToDateTime(txtendTime.Text);
                        if (ts.Days != 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('回来时间要与申请时间要在同一天！');</script>");
                            txtendTime.Focus();
                            return ;
                        }
                    }
                    catch (Exception)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                        txtendTime.Focus();
                        return ;
                    }
                }

                if (txtendTime.Text != "" && txtgoTime.Text != "")
                {
                    if (Convert.ToDateTime( txtgoTime.Text) >= Convert.ToDateTime( txtendTime.Text))
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
                if (txtroadLong.Text == "")
                    txtroadLong.Text = "0";
                Convert.ToDecimal(txtroadLong.Text);
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



            tb_UseCar carInfo = new tb_UseCar();
            carInfo.appName = Convert.ToInt32(DBHelp.ExeScalar(string.Format("select ID from tb_User where loginName='{0}'", txtName.Text)));
            carInfo.datetime = Convert.ToDateTime(txtDateTime.Text);
            carInfo.deAddress = txtdeAddress.Text;


            if (txtendTime.Text != "")
            {
                carInfo.endTime = Convert.ToDateTime( txtendTime.Text);
            }
            if (txtgoTime.Text != "")
            {
                carInfo.goTime = Convert.ToDateTime(txtgoTime.Text);
            }
            carInfo.goAddress = txtgoAddress.Text;

            carInfo.pers_car = txtpers_car.Text;
            if (txtroadLong.Text != "")
                carInfo.roadLong = Convert.ToDecimal(txtroadLong.Text);
            carInfo.toAddress = txttoAddress.Text;
            carInfo.useReason = txtuseReason.Text;
            if (rdoDan.Checked)
            {
                carInfo.type = "单程";
            }
            else
            {
                carInfo.type = "往返";
            }
                   
            #region 本单据的ID
            carInfo.id = Convert.ToInt32(Request["allE_id"]);
            #endregion

            tb_UseCarService UseCarDetailSer = new tb_UseCarService();
          
            if (UseCarDetailSer.Update(carInfo))
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");

            }
            else
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");

            }
        }

        protected void btnFinSub_Click(object sender, EventArgs e)
        {
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('：', ':');
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('。', ':');
            txtgoTime.Text = txtgoTime.Text.Trim().Replace('.', ':');

            txtendTime.Text = txtendTime.Text.Trim().Replace('：', ':');
            txtendTime.Text = txtendTime.Text.Trim().Replace('.', ':');
            txtendTime.Text = txtendTime.Text.Trim().Replace('。', ':');


            if (txtgoTime.Enabled == true && txtgoTime.Text.Trim()=="")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写出发时间！');</script>");
                txtName.Focus();
                return;
            }
            if (txtendTime.Enabled == true && txtendTime.Text.Trim()=="")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写结束时间！');</script>");
                txtendTime.Focus();
                return ;

            } 
            try
            {
                if (txtgoTime.Enabled == true && txtgoTime.Text.Trim() != "")
                {
                    Convert.ToDateTime(txtgoTime.Text);

                    try
                    {
                        TimeSpan ts = Convert.ToDateTime(txtDateTime.Text) - Convert.ToDateTime(txtgoTime.Text);
                        if (ts.Days != 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('出发时间要与申请时间要在同一天！');</script>");
                            txtendTime.Focus();
                            return ;
                        }
                    }
                    catch (Exception)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                        txtendTime.Focus();
                        return ;
                    }
                }


                if (txtendTime.Enabled == true && txtendTime.Text.Trim() != "")
                {

                    Convert.ToDateTime(txtendTime.Text);

                    try
                    {
                        TimeSpan ts = Convert.ToDateTime(txtDateTime.Text) - Convert.ToDateTime(txtendTime.Text);
                        if (ts.Days != 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('回来时间要与申请时间要在同一天！');</script>");
                            txtendTime.Focus();
                            return ;
                        }
                    }
                    catch (Exception)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的时间格式有误！');</script>");
                        txtendTime.Focus();
                        return ;
                    }
                }

                if (txtgoTime.Text.Trim() != "" && txtendTime.Text != "")
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
            string sql = "";
            if (txtgoTime.Enabled == true)
            {
                sql = string.Format("update tb_UseCar set goTime='{0}' where id={1}", Convert.ToDateTime(txtgoTime.Text), Convert.ToInt32(Request["allE_id"]));
            }
            else if (txtendTime.Enabled == true)
            {
                sql = string.Format("update tb_UseCar set endTime='{0}' where id={1}", Convert.ToDateTime(txtendTime.Text), Convert.ToInt32(Request["allE_id"]));
            }

            DBHelp.ExeCommand(sql);
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            
            txtgoTime.Focus();
        
        }
        CG_POOrderService POSer = new CG_POOrderService();
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            if (Session["Comm_CGPONo"] != null)
            {
                CG_POOrder model = POSer.GetModel(Convert.ToInt32(Session["Comm_CGPONo"]));
                txtPOGuestName.Text = model.GuestName;
                txtPOName.Text = model.POName;
                txtPONo.Text = model.PONo;
                Session["Comm_CGPONo"] = null;
            }
        }

        

        
    }
}
