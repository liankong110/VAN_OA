using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.Fin
{
    public partial class WFJieSuanType : System.Web.UI.Page
    {
        List<ListItem> items = new List<ListItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                items = new List<ListItem>();
                for (int i = (DateTime.Now.Year-1); i < (DateTime.Now.Year+10); i++)
                {
                    items.Add(new ListItem { Text=i.ToString(),Value=i.ToString() });
                }
                gvMain.DataSource = basePoTypeList;
                gvMain.DataBind();
            }
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                try
                {
                    DropDownList dllYear = (DropDownList)e.Row.FindControl("dllYear");
                    dllYear.DataSource = items;
                    dllYear.DataBind();                    

                    var hidYeartxt = ((HiddenField)e.Row.FindControl("hidYeartxt")).Value;
                    if (hidYeartxt != "-1")
                    {
                        dllYear.SelectedIndex = items.FindIndex(t => t.ToString() == hidYeartxt);
                    }                  

                }
                catch (Exception)
                {


                }
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }      
    }
}