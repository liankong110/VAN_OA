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
using VAN_OA.Model.EFrom;
using System.Collections.Generic;

namespace VAN_OA.EFrom
{
    public partial class WFPOCai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["indexcai"] != null)
                {
                    int indexcai = Convert.ToInt32(Request["indexcai"]);
                    if (Session["Cais"] != null)
                    {

                        List<TB_POCai> POOrders = Session["Cais"] as List<TB_POCai>;
                        TB_POCai model = POOrders[indexcai];
                        setValue(model);
                    }
                }
                else
                {
                    txtUpdateUser.Text = Session["LoginName"].ToString();
                }
            }
        }

        private void setValue(TB_POCai model)
        {


            if (model.SupperPrice != null)
                txtSupperPrice.Text = string.Format("{0:n2}", model.SupperPrice.Value);

            if (model.Total1 != null)
                txtTotal1.Text = string.Format("{0:n2}", model.Total1);




            //if (model.SupperPrice1 != null)
            //    txtPrice2.Text = string.Format("{0:n2}", model.SupperPrice1.Value);

            //if (model.Total2 != null)
            //    txtTotal2.Text = string.Format("{0:n2}", model.Total2);

            //if (model.SupperPrice2 != null)
            //    txtPrice3.Text = string.Format("{0:n2}", model.SupperPrice2.Value);

            //if (model.Total3 != null)
            //    txtTotal3.Text = string.Format("{0:n2}", model.Total3);


            //txtSupper2.Text = model.Supplier1;
            //txtSupper3.Text = model.Supplier2;
            txtInvName.Text = model.InvName;
            txtGuestName.Text = model.GuestName;
            txtNum.Text = model.Num.Value.ToString();

            if (model.CaiTime != null)
                txtCaiTime.Text = model.CaiTime.Value.ToShortDateString();

            txtIdea.Text = model.Idea;
            txtUpdateUser.Text = Session["LoginName"].ToString();

            txtSupplier.Text = model.Supplier;



        }
        private void clear()
        {

            txtUpdateUser.Text = Session["LoginName"].ToString();
            txtSupplier.Text = "";
            txtSupperPrice.Text = "";
            txtIdea.Text = "";
            txtCaiTime.Text = "";
            
        }

        public bool check()
        {
            if (txtCaiTime.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                return false;
            }

            if (txtSupplier.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写采购询供应商1！');</script>");
                return false;
            }
            if (txtSupperPrice.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写采购询价1！');</script>");
                return false;
            }
            try
            {
                Convert.ToDateTime(txtCaiTime.Text);
            }
            catch (Exception)
            {
                
                 base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写时间格式有误！');</script>");
                return false;
            }
            try
            {
                Convert.ToDecimal(txtSupperPrice.Text);
            }
            catch (Exception)
            {
                
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的单价格式有误！');</script>");
                return false;
            }
            return true;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (check() == false)
            {
                return;
            }
            TB_POCai s = new TB_POCai();
            s.CaiTime = Convert.ToDateTime(txtCaiTime.Text);
            s.Idea = txtIdea.Text;
            s.SupperPrice = Convert.ToDecimal(txtSupperPrice.Text);
            s.Supplier = txtSupplier.Text;
            s.UpdateUser =txtUpdateUser.Text;
            s.Num = Convert.ToDecimal(txtNum.Text);
            s.Total1 = s.SupperPrice * s.Num.Value;

            s.Num = Convert.ToDecimal(txtNum.Text);
            s.InvName = txtInvName.Text;
            s.GuestName = txtGuestName.Text;


            //s.Supplier1 = txtSupper2.Text;
            //s.Supplier2 = txtSupper3.Text;

            //if (txtPrice2.Text != "")
            //{
            //    s.SupperPrice1 = Convert.ToDecimal(txtPrice2.Text);
            //    s.Total2 = s.SupperPrice1.Value * s.Num.Value;
            //}


            //if (txtPrice3.Text != "")
            //{
            //    s.SupperPrice2 = Convert.ToDecimal(txtPrice3.Text);
            //    s.Total3 = s.SupperPrice2.Value * s.Num.Value;
            //}
           
            //修改
            if (Request["indexcai"] != null)
            {
                int indexcai = Convert.ToInt32(Request["indexcai"]);
                if (Session["Cais"] != null)
                {
                    s.UpdateUser = Session["LoginName"].ToString();
                    List<TB_POCai> POOrders = Session["Cais"] as List<TB_POCai>;

                    TB_POCai model = POOrders[indexcai];
                    TB_POCai newSche = s;
                    s.Ids = model.Ids;
                    newSche.IfUpdate = true;
                    POOrders[indexcai] = newSche;
                    Session["Cais"] = POOrders;

                }
                this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");
            }
            else
            {

                if (Session["Cais"] == null)
                {
                    List<TB_POCai> POOrders = new List<TB_POCai>();
                    POOrders.Insert(POOrders.Count, s);
                    Session["Cais"] = POOrders;
                }
                else
                {
                    List<TB_POCai> POOrders = Session["Cais"] as List<TB_POCai>;
                    POOrders.Insert(POOrders.Count, s);
                    Session["Cais"] = POOrders;
                }
                clear();
            }
            // this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Write("<script>window.close();window.opener=null;</script>");

        }

         
    }
}
