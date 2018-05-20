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
    public partial class QPInvs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["index"] != null)
                {
                    int index = Convert.ToInt32(Request["index"]);
                    if (Session["Invs"] != null)
                    {

                        List<tb_QuotePrice_Invs> invDetails = Session["Invs"] as List<tb_QuotePrice_Invs>;
                        tb_QuotePrice_Invs model = invDetails[index];
                        setValue(model);
                    }
                }
            }
        }

        private void setValue(tb_QuotePrice_Invs model)
        {          
            txtCostPrice.Text = Convert.ToDecimal(model.InvPrice).ToString();             
            txtInvName.Text = model.InvName;
            txtNum.Text = model.InvNum.ToString();

            txtCostTotal.Text = string.Format("{0:n2}", model.InvNum * model.InvPrice);
            
        }
        private void clear()
        {              
            txtNum.Text = "";
            txtInvName.Text = "";
            txtCostPrice.Text = "";          
            txtCostTotal.Text = "";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {                 
                Convert.ToDecimal(txtCostPrice.Text);
            }
            catch (Exception)
            {
                
                 base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的单价格式有误！');</script>");
                 return;
            }
            tb_QuotePrice_Invs s = new tb_QuotePrice_Invs();
             
            s.InvPrice = Convert.ToDecimal(txtCostPrice.Text);
          
            s.InvName = txtInvName.Text;
            s.InvNum = Convert.ToDecimal(txtNum.Text);
            s.Total = s.InvNum * s.InvPrice;
            //修改
            if (Request["index"] != null)
            {
                int index = Convert.ToInt32(Request["index"]);
                if (Session["Invs"] != null)
                {
                    List<tb_QuotePrice_Invs> invDetails = Session["Invs"] as List<tb_QuotePrice_Invs>;

                    tb_QuotePrice_Invs model = invDetails[index];
                    tb_QuotePrice_Invs newSche = s;
                    s.Id = model.Id;
                    newSche.IfUpdate = true;
                    invDetails[index] = newSche;
                    Session["Invs"] = invDetails;

                }
                this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");
            }
            else
            {

                if (Session["Invs"] == null)
                {
                    List<tb_QuotePrice_Invs> invDetails = new List<tb_QuotePrice_Invs>();
                    invDetails.Insert(invDetails.Count, s);
                    Session["Invs"] = invDetails;
                }
                else
                {
                    List<tb_QuotePrice_Invs> invDetails = Session["Invs"] as List<tb_QuotePrice_Invs>;
                    invDetails.Insert(invDetails.Count, s);
                    Session["Invs"] = invDetails;
                }
                clear();
            }
           // this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
           
            Response.Write("<script>window.close();window.opener=null;</script>"); 
            
        }

        protected void txtCostPrice_TextChanged(object sender, EventArgs e)
        {

            if (txtNum.Text != "" && txtCostPrice.Text != "")
            {
                try
                {
                    txtCostTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtCostPrice.Text)).ToString();

                }
                catch (Exception)
                {
                    
                    
                }
            }
            
        }

        protected void txtNum_TextChanged(object sender, EventArgs e)
        {
            if (txtNum.Text != "" && txtCostPrice.Text != "")
            {
                try
                {
                    txtCostTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtCostPrice.Text)).ToString();
                }
                catch (Exception)
                {


                }
            }             
        }

        protected void txtSellTotal_TextChanged(object sender, EventArgs e)
        {

          
        }

        protected void txtSellPrice_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
