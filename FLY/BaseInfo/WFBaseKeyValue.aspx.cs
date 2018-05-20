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
    public partial class WFBaseKeyValue : BasePage
    {
        private BaseKeyValueService invSer = new BaseKeyValueService();


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from TB_GoodsType where GoodTypeName='{0}'", txtTypeValue.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('类别[{0}],已经存在！');</script>", txtTypeValue.Text));
                        return;
                    }
                    BaseKeyValue per = getModel();
                    if (this.invSer.Add(per) > 0)
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

       

        private void Clear()
        {
            
            txtTypeValue.Text = "";
            
            txtTypeValue.Focus();
        }


        public BaseKeyValue getModel()
        { 
            VAN_OA.Model.BaseInfo.BaseKeyValue model = new VAN_OA.Model.BaseInfo.BaseKeyValue();
            model.TypeValue = this.txtTypeValue.Text;
            model.TypeName = "Num";
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

                    BaseKeyValue per = getModel();
                    if (this.invSer.Update(per))
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
            if (this.txtTypeValue.Text.Trim().Length == 0)
            {
                strErr = "信息不能为空！\\n";
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                this.txtTypeValue.Focus();
                return false;
            }

            if (CommHelp.VerifesToNum(txtTypeValue.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数字(天) 格式错误！');</script>");
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
                    BaseKeyValue model = this.invSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.txtTypeValue.Text = model.TypeValue;
                   
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
