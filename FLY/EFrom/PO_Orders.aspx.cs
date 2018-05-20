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
    public partial class PO_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["index"] != null)
                {
                    int index = Convert.ToInt32(Request["index"]);
                    if (Session["Orders"] != null)
                    {

                        List<TB_POOrders> POOrders = Session["Orders"] as List<TB_POOrders>;
                        TB_POOrders model = POOrders[index];
                        setValue(model);
                    }
                }
            }
        }

        private void setValue(TB_POOrders model)
        {          
            txtCostPrice.Text = Convert.ToDecimal(model.CostPrice).ToString();
            txtGuestName.Text = model.GuestName;
            txtInvName.Text = model.InvName;
            txtNum.Text = model.Num.ToString();
            txtOtherCost.Text = model.OtherCost.ToString();
            if (model.Profit!=null)
            txtProfit.Text = string.Format("{0:n2}", model.Profit.Value);
            txtSellPrice.Text = model.SellPrice.ToString();
            txtTime.Text = model.Time.ToShortDateString();
            if(model.ToTime!=null)
            txtToTime.Text = model.ToTime.Value.ToShortDateString();
            txtUnit.Text = model.Unit;


            txtCostTotal.Text = string.Format("{0:n2}", model.Num*model.CostPrice);
            txtSellTotal.Text =string.Format("{0:n2}",  model.SellPrice*model.Num);
        }
        private void clear()
        { 
            txtUnit.Text = "";
            txtToTime.Text = "";
            txtSellPrice.Text = "";
            txtProfit.Text = "";
            txtOtherCost.Text = "";
            txtNum.Text = "";
            txtInvName.Text = "";
            txtCostPrice.Text = "";
           // txtTime.Focus();
            txtSellTotal.Text = "";
            txtCostTotal.Text = "";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                Convert.ToDecimal(txtSellPrice.Text);
                Convert.ToDecimal(txtCostPrice.Text);
            }
            catch (Exception)
            {
                
                   base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的单价格式有误！');</script>");
                   return;
            }
            TB_POOrders s = new TB_POOrders();
            s.Unit = txtUnit.Text;
            s.CostPrice = Convert.ToDecimal(txtCostPrice.Text);
            s.GuestName = txtGuestName.Text;
            s.InvName = txtInvName.Text;
            s.Num = Convert.ToDecimal(txtNum.Text);
            if (txtOtherCost.Text != "")
                s.OtherCost = Convert.ToDecimal(txtOtherCost.Text);
            if (txtProfit.Text != "")
                s.Profit = Convert.ToDecimal(txtProfit.Text);

            s.SellPrice = Convert.ToDecimal(txtSellPrice.Text);
          
            s.Time = Convert.ToDateTime(txtTime.Text);
            if (txtToTime.Text != "")
            {
                s.ToTime = Convert.ToDateTime(txtToTime.Text);
            }

            s.CostTotal = s.CostPrice * s.Num;
            s.SellTotal=s.SellPrice*s.Num;
            s.YiLiTotal = s.SellTotal - s.CostTotal - s.OtherCost;
            if (s.SellTotal != 0)
                s.Profit = s.YiLiTotal / s.SellTotal * 100;
            else if (s.YiLiTotal != 0)
            {
                s.Profit = -100;
            }
            else
            {
                s.Profit = 0;
            }
                //修改
            if (Request["index"] != null)
            {
                int index = Convert.ToInt32(Request["index"]);
                if (Session["Orders"] != null)
                {
                    List<TB_POOrders> POOrders = Session["Orders"] as List<TB_POOrders>;

                    TB_POOrders model = POOrders[index];
                    TB_POOrders newSche = s;
                    s.Ids = model.Ids;
                    newSche.IfUpdate = true;
                    POOrders[index] = newSche;
                    Session["Orders"] = POOrders;

                }
                this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script> window.close();</script>");
            }
            else
            {

                if (Session["Orders"] == null)
                {
                    List<TB_POOrders> POOrders = new List<TB_POOrders>();
                    POOrders.Insert(POOrders.Count, s);
                    Session["Orders"] = POOrders;
                }
                else
                {
                    List<TB_POOrders> POOrders = Session["Orders"] as List<TB_POOrders>;
                    POOrders.Insert(POOrders.Count , s);
                    Session["Orders"] = POOrders;
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

            if (txtNum.Text != "" && txtSellPrice.Text != "")
            {
                try
                {

                    txtSellTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtSellPrice.Text)).ToString();
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
            if (txtNum.Text != "" && txtSellPrice.Text != "")
            {
                try
                {

                    txtSellTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtSellPrice.Text)).ToString();
                }
                catch (Exception)
                {


                }
            }
        }
    }
}
