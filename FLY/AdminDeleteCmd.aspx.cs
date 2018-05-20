using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal;
using VAN_OA.Model;
using VAN_OA.Bll.OA;
using System.Data.SqlClient;

namespace VAN_OA
{
    public partial class AdminDeleteCmd : System.Web.UI.Page
    {
        private TB_AdminDeleteService roleSer = new TB_AdminDeleteService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    TB_AdminDelete role = new TB_AdminDelete();
                    role.UserId =Convert.ToInt32( this.ddlUser.SelectedItem.Value);
                     
                    if (this.roleSer.Add(role) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                                           
                        this.ddlUser.Focus();
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
            base.Response.Redirect("~/WFTAdminDelete.aspx");
        }

        

     

        public bool FormCheck()
        {


            if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from TB_AdminDelete where UserId='{0}'", this.ddlUser.SelectedItem.Value))) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该用户已经存在,请重新填写！');</script>");
                this.ddlUser.Focus();
                return false;
            }                
             
            return true;
        }


        public List<User> getAllUserByLoginName(string where)
        {
            string sql = "select ID,loginName from tb_User where 1=1  " + where + " order by loginName";
            List<User> list = new List<User>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        User User = new User();
                        User.Id = objReader.GetInt32(0);

                        User.LoginName = objReader.GetString(1);

                        list.Add(User);
                    }
                    objReader.Close();
                }
            }
            return list;

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                
                     
                    List<VAN_OA.Model.User> user =getAllUserByLoginName("");
                    ddlUser.DataSource = user;
                    ddlUser.DataBind();
                    ddlUser.DataTextField = "LoginName";
                    ddlUser.DataValueField = "Id";


                    
                   
               
            }
        }
    }
}
