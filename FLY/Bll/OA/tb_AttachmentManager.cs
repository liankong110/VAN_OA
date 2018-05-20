using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using VAN_OA.Dal.OA;
using VAN_OA.Model.OA;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace VAN_OA.Bll.OA
{
    public class tb_AttachmentManager
    {
        tb_AttachmentService attachmentSer = new tb_AttachmentService();
        public int AddSomAttachments(tb_Attachment attachments)
        {
            int id = 0;
            
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                  
                     
                        objCommand.Parameters.Clear();                        
                       id= attachmentSer.Add(attachments, objCommand);
                    

                    
                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return 0;

                }
                return id;
            }
        }

        public bool UpdateSomAttachments(tb_Attachment attachments)
        {


            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {



                    objCommand.Parameters.Clear();
                    attachmentSer.Update(attachments, objCommand);
                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return false;

                }
                return true;
            }
        }
    }
}
