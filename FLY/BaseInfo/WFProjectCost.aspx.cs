using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFProjectCost : System.Web.UI.Page
    {
        private TB_ProjectCostService projectCostService = new TB_ProjectCostService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    TB_ProjectCost model = getModel();
                    if (this.projectCostService.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFProjectCostList.aspx");
        }

        private void Clear()
        {
            txtCeSuanDian.Text = "";
            txtMonthLiLv.Text = "";
            txtZhangQi.Text = "";
        }


        public TB_ProjectCost getModel()
        {
            TB_ProjectCost model = new TB_ProjectCost();
            model.CeSuanDian = float.Parse( txtCeSuanDian.Text);
            model.ZhangQi = float.Parse(txtZhangQi.Text);
            model.MonthLiLv = float.Parse(txtMonthLiLv.Text);
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
                    TB_ProjectCost model = getModel();
                    if (this.projectCostService.Update(model))
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
           

            if (CommHelp.VerifesToNum(txtZhangQi.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成本账期 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtCeSuanDian.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成本测算 格式错误！');</script>");
                return false;
            }

            if (CommHelp.VerifesToNum(txtMonthLiLv.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('成本月利率 格式错误！');</script>");
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
                    TB_ProjectCost model = this.projectCostService.GetModel(Convert.ToInt32(base.Request["Id"]));

                    txtCeSuanDian.Text = model.CeSuanDian.ToString();
                    txtMonthLiLv.Text = model.MonthLiLv.ToString();
                    txtZhangQi.Text = model.ZhangQi.ToString();
                }
                else
                {


                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
