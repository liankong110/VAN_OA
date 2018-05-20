using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class FINProperty : System.Web.UI.Page
    {
        private FIN_PropertyService goodSer = new FIN_PropertyService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from FIN_Property where CostType='{0}'",
                txtCostType.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('类型[{0}]，已经存在！');</script>", txtCostType.Text));
                        return;
                    }
                    FIN_Property model = getModel();
                    if (this.goodSer.Add(model) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        Clear();
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/FINPropertyList.aspx");
        }

        private void Clear()
        {
            txtCostType.Text = "";
            txtProNo.Text = "";         
        }


        public FIN_Property getModel()
        {
            VAN_OA.Model.BaseInfo.FIN_Property model = new VAN_OA.Model.BaseInfo.FIN_Property();

            model.CostType = txtCostType.Text;
            model.ProNo = txtProNo.Text;
            model.MyProperty =ddlPro.Text;         
            
            if (Request["Id"] != null)
            {
                model.Id = Convert.ToInt32(Request["Id"]);
            }
           
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from FIN_Property where CostType='{0}' and id<>{1}",
                  txtCostType.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('类型[{0}]，已经存在！');</script>", txtCostType.Text));
                        return;
                    }
                    FIN_Property model = getModel();
                    if (this.goodSer.Update(model))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {
            string strErr = "";
            if (this.txtCostType.Text.Trim().Length == 0)
            {
                strErr += "类型不能为空！\\n";
            } 
            if (strErr != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
            }

            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    FIN_Property model = this.goodSer.GetModel(Convert.ToInt32(base.Request["Id"]));                    
                    this.txtProNo.Text = model.ProNo;
                    this.txtCostType.Text = model.CostType;
                    this.ddlPro.Text = model.MyProperty;
                   
                }
                else
                {


                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
