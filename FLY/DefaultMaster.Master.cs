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


using VAN_OA.Dal;
using VAN_OA.Model; 
using System.Collections.Generic;
using VAN_OA.Dal.EFrom;
 
using VAN_OA.Model.EFrom;
namespace VAN_OA
{
    public partial class DefaultMaster : MasterPage
    {
     
       

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            base.Session.RemoveAll();
            //Request.Path
            Response.Write(string.Format("<script>top.location.href='{0}';</script>","/login.aspx"));  
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.Session["currentUserId"] == null)
            {
                Response.Write(string.Format("<script>top.location.href='{0}';</script>","/login.aspx"));  
            }
            else
            {
                this.lblLoginName.Text = base.Session["LoginName"].ToString();
                if (!IsPostBack)
                {

                    GetTodoCount();
                }
            }

           
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (Session["currentUserId"] != null)
            {
                GetTodoCount();
            }
        }

        private void GetTodoCount()
        {
            string sql = string.Format(" 1=1 and state='执行中'");

            string UserId = Session["currentUserId"].ToString();
            tb_EFormService eformSer = new tb_EFormService();
            int allEForms = eformSer.GetListArray_ToDo_Count(sql, Convert.ToInt32(UserId));
            if (allEForms > 0)
            {
                lblMessTodo.Text = string.Format("你有 {0} 个文件要审批", allEForms);
                imgMess.Visible = true;
            }
            else
            {
                lblMessTodo.Text = "";
                imgMess.Visible = false;
            }
            
            //查询自己所有正在处理的单据


            sql = string.Format("select count(*) from tb_EForm where state='执行中' and appPer={0}", UserId);
            int zhixingzhongCount =Convert.ToInt32(DBHelp.ExeScalar(sql));

            lblDoing.Text = string.Format("你有{0}个申请正在执行中....",zhixingzhongCount);
            
            //查询今天 有多少单据审批成功
             sql = string.Format(@"select count(*) from tb_EForm left join (select e_id,Max(doTime)AS MaxTime from tb_EForms group by e_id) as newTable on tb_EForm.id=newTable.e_id
                                          where state='通过' and appPer={0} and MaxTime between '{1} 00:00:00' and '{1} 23:59:59'", UserId, DateTime.Now.ToShortDateString());

             int succsess = Convert.ToInt32(DBHelp.ExeScalar(sql));




             lblSuccess.Text = string.Format("今天 你有{0}个申请审批 通过", succsess);


            //查询今天 有多少单据审批失败
             sql = string.Format(@"select count(*) from tb_EForm left join (select e_id,Max(doTime)AS MaxTime from tb_EForms group by e_id) as newTable on tb_EForm.id=newTable.e_id
                                          where state='不通过' and appPer={0} and MaxTime between '{1} 00:00:00' and '{1} 23:59:59'", UserId, DateTime.Now.ToShortDateString());

             lblFail.Text = string.Format("今天 你有{0}个申请审批 没有通过审核", DBHelp.ExeScalar(sql));




             try
             {  //查询今天在销售订单中采购人是我的 所有通过的单子

                 sql = string.Format(@"select count(*) from tb_EForm left join (select e_id,Max(doTime)AS MaxTime from tb_EForms group by e_id) as newTable on tb_EForm.id=newTable.e_id
                                          where state='通过'  and MaxTime between '{1} 00:00:00' and '{1} 23:59:59' 
                                    and  tb_EForm.alle_id in (select id from  TB_POOrder where caigou='{0}')
and  proid=(select pro_Id from A_ProInfo where pro_Type='订单报批表')", base.Session["LoginName"].ToString(), DateTime.Now.ToShortDateString());

                 int count = Convert.ToInt32(DBHelp.ExeScalar(sql));

                 if (count > 0)
                 {
                     lblCaiSuccess.Text = string.Format("今天 订单审批通过个数： {0} ", count);
                     lblCaiSuccess.Visible = true;
                 }
                 else
                 {
                     lblCaiSuccess.Visible = false;
                 }
             }
             catch (Exception)
             {


             }
             try
             {  //查询合同是否要过期

                 sql = "select count(*) from HR_Person where ContractCloseTime=convert(varchar(50),getdate(),23)";

                 int count = Convert.ToInt32(DBHelp.ExeScalar(sql));

                 if (count > 0 && Session["LoginName"] == "李琍")
                 {
                     lblContract.Text = string.Format("今天 合同到期个数： {0} ", count);
                     lblContract.Visible = true;
                 }
                 else
                 {
                     lblContract.Visible = false;
                 }
             }
             catch (Exception)
             { 


             }
        }

        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            WebQuerySessin Sess = new WebQuerySessin("");
            Response.Redirect("~/EFrom/MyRequestEForms.aspx?Type=Success");
        }

        protected void lblFail_Click(object sender, EventArgs e)
        {
            WebQuerySessin Sess = new WebQuerySessin("");
            Response.Redirect("~/EFrom/MyRequestEForms.aspx?Type=Fail");
        }

        protected void lblDoing_Click(object sender, EventArgs e)
        {
            WebQuerySessin Sess = new WebQuerySessin("");
            Response.Redirect("~/EFrom/MyRequestEForms.aspx?Type=Doing");
        }

        protected void lblCaiSuccess_Click(object sender, EventArgs e)
        {
            WebQuerySessin Sess = new WebQuerySessin("");
            Response.Redirect("~/EFrom/MyRequestEForms.aspx?Type=SuccessCai");
        }

        protected void lblMessTodo_Click(object sender, EventArgs e)
        {
            

            WebQuerySessin Sess = new WebQuerySessin("");
            Response.Redirect("~/EFrom/MyEFormsTodo.aspx");
        }

        protected void lbtnWare_Click(object sender, EventArgs e)
        {
            WebQuerySessin Sess = new WebQuerySessin("");
            Response.Redirect("~/ReportForms/LeverDispatching.aspx");
        }

        protected void btnExoInv_Click(object sender, EventArgs e)
        {
            WebQuerySessin Sess = new WebQuerySessin("");
            Response.Redirect("~/ReportForms/ExpInvsList.aspx");
        }

        protected void lblContract_Click(object sender, EventArgs e)
        {
            WebQuerySessin Sess = new WebQuerySessin("");
            Response.Redirect("~/HR/PersonList.aspx?ContractRemind=1");
        }
    }
}
