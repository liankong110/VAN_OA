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
using VAN_OA.Model.OA;
using VAN_OA.Dal.OA;

using System.Collections.Generic;
using VAN_OA.Bll.OA;

using System.IO;

using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Office;
using VAN_OA.Dal;
using VAN_OA.Model;
namespace VAN_OA.MyExcels
{
    public partial class AddExcel : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAttName.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写文件名称！');</script>");
                    return;
                }

                //HttpFileCollection files = HttpContext.Current.Request.Files;

                //if (files.Count <= 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择一个文件！');</script>");
                //    return;
                //}

                TB_EXCEL excel = new TB_EXCEL();
                excel.CreateTime = DateTime.Now;
                excel.Name = txtAttName.Text;
                excel.IsParent = 1;
                excel.Remark = txtRemark.Text;
                excel.UserName = Session["LoginName"].ToString();
                excel.UpState = 1;
                excel.FileName = Request["FileName"];

                excel.FileType = Request["lastFileName"];
               

                MyExcelService excelMan = new MyExcelService();
                int id = excelMan.Add(excel);
                if (id > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                    base.Response.Redirect("~/MyExcels/MainExcelList.aspx");
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                }
            }
            catch (Exception EX)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", EX.Message.ToString()));

            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MyExcels/MainExcelList.aspx");
        }


    }
}
