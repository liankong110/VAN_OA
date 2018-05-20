using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VAN_OA.EFrom
{
    public partial class SetOliPrice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string url = System.Web.HttpContext.Current.Request.MapPath("CarOilPrice.txt");
                    System.IO.StreamReader my = new System.IO.StreamReader(url, System.Text.Encoding.Default);
                    string line;

                    line = my.ReadLine();

                    txtPrice.Text = line;
                    my.Close();
                }
                catch (Exception)
                {


                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                Convert.ToDecimal(txtPrice.Text);
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('油价信息填写错误！');</script>");
                txtPrice.Focus();
                return;
            }

            try
            {
                string url = System.Web.HttpContext.Current.Request.MapPath("CarOilPrice.txt");
                System.IO.StreamWriter myWrite = new System.IO.StreamWriter(url, false, System.Text.Encoding.Default);
                myWrite.WriteLine(txtPrice.Text);
            
                myWrite.Flush();
                myWrite.Close();
            }
            catch (Exception)
            {
                
               base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
               return;
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");

        }
    }
}
