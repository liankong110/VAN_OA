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
using VAN_OA.Model.EFrom;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace VAN_OA.Dal.EFrom
{
    public class tb_QuotePriceService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_QuotePrice model, 
            List<tb_QuotePrice_InvDetails> invsDetails, string invDetails_IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                tb_QuotePrice_InvsService InvSer = new tb_QuotePrice_InvsService();              

                tb_QuotePrice_InvDetailsService InvDetailSer = new tb_QuotePrice_InvDetailsService();
                try
                {

                    objCommand.Parameters.Clear();
                    if (model.IsYH)
                    {
                        model.Total = model.LastYH;
                    }
                    else
                    {
                        model.Total = invsDetails.Sum(t => t.Total) + model.LaborCost + model.EngineeringTax;
                    }

                    Update(model, objCommand);           


                    #region 报价内容
                    for (int i = 0; i < invsDetails.Count; i++)
                    {
                        invsDetails[i].QuoteId = model.Id;
                        if (invsDetails[i].IfUpdate == true && invsDetails[i].Id != 0)
                        {

                            InvDetailSer.Update(invsDetails[i], objCommand);

                        }
                        else if (invsDetails[i].Id == 0)
                        {
                            InvDetailSer.Add(invsDetails[i], objCommand);

                        }
                    }
                    if (invDetails_IDS != "")
                    {
                        invDetails_IDS = invDetails_IDS.Substring(0, invDetails_IDS.Length - 1);
                        InvDetailSer.DeleteByIds(invDetails_IDS, objCommand);
                    }
                    #endregion


                    tan.Commit();
                }
                catch (Exception ex)
                {
                    tan.Rollback();
                    return false;

                }

            }

            return true;
        }
        public int addTran(VAN_OA.Model.EFrom.tb_QuotePrice model,  List<tb_QuotePrice_InvDetails> invsDetails)
        {
            int id = 0;
            int MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
               
                try
                {


                    objCommand.Parameters.Clear();
                    if (model.IsYH)
                    {
                        model.Total = model.LastYH;
                    }
                    else
                    {
                        model.Total = invsDetails.Sum(t => t.Total) + model.LaborCost + model.EngineeringTax;
                    }
                    id = Add(model, objCommand);
                    MainId = id;

                    

                    tb_QuotePrice_InvDetailsService InvDetailSer = new tb_QuotePrice_InvDetailsService();
                    for (int i = 0; i < invsDetails.Count; i++)
                    {
                        invsDetails[i].QuoteId = id;
                        InvDetailSer.Add(invsDetails[i], objCommand);
                    }
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
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.tb_QuotePrice model, SqlCommand objCommand)
        {
            //报价单号：YYYYMMDDTTT-N 表示，YYYYMMDD是生成界面的当年月日，TTT是客户表中的快速代码，N 是当天这个客户的第几份报价单
            string resultNo = "";
            string Qian =  DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00")+model.GuestNo+"-";
            string getNo = string.Format(@"select right('0000000000000'+CONVERT(varchar, MAX(CONVERT(int, RIGHT(QuoteNo,2)))+1),2) from tb_QuotePrice 
where QuoteNo like '{0}%'", Qian);
            objCommand.CommandText = getNo;
            object objNo=objCommand.ExecuteScalar();
            if (objNo == null||objNo.ToString()=="")
            {
                resultNo = Qian + "01";
            }
            else
            {
                resultNo = Qian+objNo.ToString();
            }
            model.QuoteNo = resultNo;
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.QuoteNo != null)
            {
                strSql1.Append("QuoteNo,");
                strSql2.Append("'" + resultNo + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.GuestNo != null)
            {
                strSql1.Append("GuestNo,");
                strSql2.Append("'" + model.GuestNo + "',");
            }
            if (model.GuestId != null)
            {
                strSql1.Append("GuestId,");
                strSql2.Append("" + model.GuestId + ",");
            }
            if (model.QuoteDate != null)
            {
                strSql1.Append("QuoteDate,");
                strSql2.Append("'" + model.QuoteDate + "',");
            }
            if (model.ResultGuestName != null)
            {
                strSql1.Append("ResultGuestName,");
                strSql2.Append("'" + model.ResultGuestName + "',");
            }
            if (model.ResultGuestNo != null)
            {
                strSql1.Append("ResultGuestNo,");
                strSql2.Append("'" + model.ResultGuestNo + "',");
            }
            if (model.PayStyle != null)
            {
                strSql1.Append("PayStyle,");
                strSql2.Append("'" + model.PayStyle + "',");
            }
            if (model.GuestNameToInv != null)
            {
                strSql1.Append("GuestNameToInv,");
                strSql2.Append("'" + model.GuestNameToInv + "',");
            }
            if (model.ContactPerToInv != null)
            {
                strSql1.Append("ContactPerToInv,");
                strSql2.Append("'" + model.ContactPerToInv + "',");
            }
            if (model.telToInv != null)
            {
                strSql1.Append("telToInv,");
                strSql2.Append("'" + model.telToInv + "',");
            }
            if (model.AddressToInv != null)
            {
                strSql1.Append("AddressToInv,");
                strSql2.Append("'" + model.AddressToInv + "',");
            }
            if (model.InvoHeader != null)
            {
                strSql1.Append("InvoHeader,");
                strSql2.Append("'" + model.InvoHeader + "',");
            }
            if (model.InvContactPer != null)
            {
                strSql1.Append("InvContactPer,");
                strSql2.Append("'" + model.InvContactPer + "',");
            }
            if (model.InvAddress != null)
            {
                strSql1.Append("InvAddress,");
                strSql2.Append("'" + model.InvAddress + "',");
            }
            if (model.InvTel != null)
            {
                strSql1.Append("InvTel,");
                strSql2.Append("'" + model.InvTel + "',");
            }
            if (model.NaShuiPer != null)
            {
                strSql1.Append("NaShuiPer,");
                strSql2.Append("'" + model.NaShuiPer + "',");
            }
            if (model.brandNo != null)
            {
                strSql1.Append("brandNo,");
                strSql2.Append("'" + model.brandNo + "',");
            }
            if (model.GuestNameTofa != null)
            {
                strSql1.Append("GuestNameTofa,");
                strSql2.Append("'" + model.GuestNameTofa + "',");
            }
            if (model.ContactPerTofa != null)
            {
                strSql1.Append("ContactPerTofa,");
                strSql2.Append("'" + model.ContactPerTofa + "',");
            }
            if (model.telTofa != null)
            {
                strSql1.Append("telTofa,");
                strSql2.Append("'" + model.telTofa + "',");
            }
            if (model.AddressTofa != null)
            {
                strSql1.Append("AddressTofa,");
                strSql2.Append("'" + model.AddressTofa + "',");
            }
            if (model.BuessName != null)
            {
                strSql1.Append("BuessName,");
                strSql2.Append("'" + model.BuessName + "',");
            }
            if (model.BuessEmail != null)
            {
                strSql1.Append("BuessEmail,");
                strSql2.Append("'" + model.BuessEmail + "',");
            }
            if (model.ComTel != null)
            {
                strSql1.Append("ComTel,");
                strSql2.Append("'" + model.ComTel + "',");
            }
            if (model.ComChuanZhen != null)
            {
                strSql1.Append("ComChuanZhen,");
                strSql2.Append("'" + model.ComChuanZhen + "',");
            }
            if (model.ComBusTel != null)
            {
                strSql1.Append("ComBusTel,");
                strSql2.Append("'" + model.ComBusTel + "',");
            }
            if (model.ComName != null)
            {
                strSql1.Append("ComName,");
                strSql2.Append("'" + model.ComName + "',");
            }
            if (model.NaShuiNo != null)
            {
                strSql1.Append("NaShuiNo,");
                strSql2.Append("'" + model.NaShuiNo + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.ComBrand != null)
            {
                strSql1.Append("ComBrand,");
                strSql2.Append("'" + model.ComBrand + "',");
            }
            if (model.CreateUser != null)
            {
                strSql1.Append("CreateUser,");
                strSql2.Append("" + model.CreateUser + ",");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }

            if (model.Total != null)
            {
                strSql1.Append("Total,");
                strSql2.Append("" + model.Total + ",");
            }
            if (model.LIlv != null)
            {
                strSql1.Append("LIlv,");
                strSql2.Append("" + model.LIlv + ",");
            }
            if (model.ZLBZ != null)
            {
                strSql1.Append("ZLBZ,");
                strSql2.Append("'" + model.ZLBZ + "',");
            }

            if (model.YSBJ != null)
            {
                strSql1.Append("YSBJ,");
                strSql2.Append("'" + model.YSBJ + "',");
            }

            if (model.FWBXDJ != null)
            {
                strSql1.Append("FWBXDJ,");
                strSql2.Append("'" + model.FWBXDJ + "',");
            }
            if (model.JFQ != null)
            {
                strSql1.Append("JFQ,");
                strSql2.Append("'" + model.JFQ + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }

             
            if (model.LaborCost != null)
            {
                strSql1.Append("LaborCost,");
                strSql2.Append("" + model.LaborCost + ",");
            }
            if (model.EngineeringTax != null)
            {
                strSql1.Append("EngineeringTax,");
                strSql2.Append("" + model.EngineeringTax + ",");
            }
            if (model.QPType != null)
            {
                strSql1.Append("QPType,");
                strSql2.Append("" + model.QPType + ",");
            }
            if (model.IsBrand != null)
            {
                strSql1.Append("IsBrand,");
                strSql2.Append("" + (model.IsBrand?1:0) + ",");
            }
            if (model.IsProduct != null)
            {
                strSql1.Append("IsProduct,");
                strSql2.Append("" + (model.IsProduct ? 1 : 0) + ",");
            }
             if (model.IsYH != null)
            {
                strSql1.Append("IsYH,");
                strSql2.Append("" + (model.IsYH ? 1 : 0) + ",");
            }
             if (model.IsRemark != null)
             {
                 strSql1.Append("IsRemark,");
                 strSql2.Append("" + (model.IsRemark ? 1 : 0) + ",");
             }
             if (model.LastYH != null)
             {
                 strSql1.Append("LastYH,");
                 strSql2.Append("" + model.LastYH + ",");
             }

             if (model.IsShuiYin != null)
             {
                 strSql1.Append("IsShuiYin,");
                 strSql2.Append("" + (model.IsShuiYin ? 1 : 0) + ",");
             }
             if (model.IsGaiZhang != null)
             {
                 strSql1.Append("IsGaiZhang,");
                 strSql2.Append("" + (model.IsGaiZhang ? 1 : 0) + ",");
             }
            strSql.Append("insert into tb_QuotePrice(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");


            int result;
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.tb_QuotePrice model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_QuotePrice set ");
            //if (model.QuoteNo != null)
            //{
            //    strSql.Append("QuoteNo='" + model.QuoteNo + "',");
            //}
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.GuestId != null)
            {
                strSql.Append("GuestId=" + model.GuestId + ",");
            }
            if (model.GuestNo != null)
            {
                strSql.Append("GuestNo='" + model.GuestNo + "',");
            }
            if (model.QuoteDate != null)
            {
                strSql.Append("QuoteDate='" + model.QuoteDate + "',");
            }
            if (model.ResultGuestName != null)
            {
                strSql.Append("ResultGuestName='" + model.ResultGuestName + "',");
            }
            if (model.ResultGuestNo != null)
            {
                strSql.Append("ResultGuestNo='" + model.ResultGuestNo + "',");
            }
            if (model.PayStyle != null)
            {
                strSql.Append("PayStyle='" + model.PayStyle + "',");
            }
            if (model.GuestNameToInv != null)
            {
                strSql.Append("GuestNameToInv='" + model.GuestNameToInv + "',");
            }
            else
            {
                strSql.Append("GuestNameToInv= null ,");
            }
            if (model.ContactPerToInv != null)
            {
                strSql.Append("ContactPerToInv='" + model.ContactPerToInv + "',");
            }
            else
            {
                strSql.Append("ContactPerToInv= null ,");
            }
            if (model.telToInv != null)
            {
                strSql.Append("telToInv='" + model.telToInv + "',");
            }
            else
            {
                strSql.Append("telToInv= null ,");
            }
            if (model.AddressToInv != null)
            {
                strSql.Append("AddressToInv='" + model.AddressToInv + "',");
            }
            else
            {
                strSql.Append("AddressToInv= null ,");
            }
            if (model.InvoHeader != null)
            {
                strSql.Append("InvoHeader='" + model.InvoHeader + "',");
            }
            else
            {
                strSql.Append("InvoHeader= null ,");
            }
            if (model.InvContactPer != null)
            {
                strSql.Append("InvContactPer='" + model.InvContactPer + "',");
            }
            else
            {
                strSql.Append("InvContactPer= null ,");
            }
            if (model.InvAddress != null)
            {
                strSql.Append("InvAddress='" + model.InvAddress + "',");
            }
            else
            {
                strSql.Append("InvAddress= null ,");
            }
            if (model.InvTel != null)
            {
                strSql.Append("InvTel='" + model.InvTel + "',");
            }
            else
            {
                strSql.Append("InvTel= null ,");
            }
            if (model.NaShuiPer != null)
            {
                strSql.Append("NaShuiPer='" + model.NaShuiPer + "',");
            }
            else
            {
                strSql.Append("NaShuiPer= null ,");
            }
            if (model.brandNo != null)
            {
                strSql.Append("brandNo='" + model.brandNo + "',");
            }
            else
            {
                strSql.Append("brandNo= null ,");
            }
            if (model.GuestNameTofa != null)
            {
                strSql.Append("GuestNameTofa='" + model.GuestNameTofa + "',");
            }
            else
            {
                strSql.Append("GuestNameTofa= null ,");
            }
            if (model.ContactPerTofa != null)
            {
                strSql.Append("ContactPerTofa='" + model.ContactPerTofa + "',");
            }
            else
            {
                strSql.Append("ContactPerTofa= null ,");
            }
            if (model.telTofa != null)
            {
                strSql.Append("telTofa='" + model.telTofa + "',");
            }
            else
            {
                strSql.Append("telTofa= null ,");
            }
            if (model.AddressTofa != null)
            {
                strSql.Append("AddressTofa='" + model.AddressTofa + "',");
            }
            else
            {
                strSql.Append("AddressTofa= null ,");
            }
            if (model.BuessName != null)
            {
                strSql.Append("BuessName='" + model.BuessName + "',");
            }
            else
            {
                strSql.Append("BuessName= null ,");
            }
            if (model.BuessEmail != null)
            {
                strSql.Append("BuessEmail='" + model.BuessEmail + "',");
            }
            else
            {
                strSql.Append("BuessEmail= null ,");
            }
            if (model.ComTel != null)
            {
                strSql.Append("ComTel='" + model.ComTel + "',");
            }
            else
            {
                strSql.Append("ComTel= null ,");
            }
            if (model.ComChuanZhen != null)
            {
                strSql.Append("ComChuanZhen='" + model.ComChuanZhen + "',");
            }
            else
            {
                strSql.Append("ComChuanZhen= null ,");
            }
            if (model.ComBusTel != null)
            {
                strSql.Append("ComBusTel='" + model.ComBusTel + "',");
            }
            else
            {
                strSql.Append("ComBusTel= null ,");
            }
            if (model.ComName != null)
            {
                strSql.Append("ComName='" + model.ComName + "',");
            }
            else
            {
                strSql.Append("ComName= null ,");
            }
            if (model.NaShuiNo != null)
            {
                strSql.Append("NaShuiNo='" + model.NaShuiNo + "',");
            }
            else
            {
                strSql.Append("NaShuiNo= null ,");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            else
            {
                strSql.Append("Address= null ,");
            }
            if (model.ComBrand != null)
            {
                strSql.Append("ComBrand='" + model.ComBrand + "',");
            }
            else
            {
                strSql.Append("ComBrand= null ,");
            }

            if (model.Total != null)
            {
                strSql.Append("Total=" + model.Total + ",");
            } 
            if (model.LIlv != null)
            {
                strSql.Append("LIlv=" + model.LIlv + ",");
            }

            if (model.ZLBZ != null)
            {
                strSql.Append("ZLBZ='" + model.ZLBZ + "',");
            }
            if (model.YSBJ != null)
            {
                strSql.Append("YSBJ='" + model.YSBJ + "',");
            }
            if (model.FWBXDJ != null)
            {
                strSql.Append("FWBXDJ='" + model.FWBXDJ + "',");
            }
            if (model.JFQ != null)
            {
                strSql.Append("JFQ='" + model.JFQ + "',");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            if (model.LaborCost != null)
            {
                strSql.Append("LaborCost=" + model.LaborCost + ",");
            }
            if (model.EngineeringTax != null)
            {
                strSql.Append("EngineeringTax=" + model.EngineeringTax + ",");
            }
            if (model.QPType != null)
            {
                strSql.Append("QPType=" + model.QPType + ",");
            }
            if (model.IsBrand != null)
            {
                strSql.Append("IsBrand=" + (model.IsBrand?1:0 )+ ",");
            }
            if (model.IsProduct != null)
            {
                strSql.Append("IsProduct=" +( model.IsProduct ? 1 : 0) + ",");
            }
            if (model.IsYH != null)
            {
                strSql.Append("IsYH=" + (model.IsYH ? 1 : 0) + ",");
            }
            if (model.IsRemark != null)
            {
                strSql.Append("IsRemark=" + (model.IsRemark ? 1 : 0) + ",");
            }
            
            if (model.LastYH != null)
            {
                strSql.Append("LastYH=" + model.LastYH + ",");
            }
            if (model.IsGaiZhang != null)
            {
                strSql.Append("IsGaiZhang=" + (model.IsGaiZhang ? 1 : 0) + ",");
            }
            if (model.IsShuiYin != null)
            {
                strSql.Append("IsShuiYin=" + (model.IsShuiYin ? 1 : 0) + ",");
            }
           // strSql.Append(" CreateTime=getdate(),");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_QuotePrice ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_QuotePrice GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append("IsShuiYin,IsGaiZhang,IsRemark,LastYH,IsYH,LIlv,LaborCost,EngineeringTax,QPType,IsBrand,IsProduct, Remark,ZLBZ,YSBJ,FWBXDJ,JFQ,Total,loginName,tb_QuotePrice.Id,QuoteNo,GuestName,GuestNo,QuoteDate,ResultGuestName,ResultGuestNo,PayStyle,GuestNameToInv,ContactPerToInv,telToInv,AddressToInv,InvoHeader,InvContactPer,InvAddress,InvTel,NaShuiPer,brandNo,GuestNameTofa,ContactPerTofa,telTofa,AddressTofa,BuessName,BuessEmail,ComTel,ComChuanZhen,ComBusTel,ComName,NaShuiNo,Address,ComBrand,CreateUser,CreateTime ");
             strSql.Append(" from tb_QuotePrice left join tb_User on tb_User.id=tb_QuotePrice.CreateUser");
            strSql.Append(" where tb_QuotePrice.Id=" + id + "");

            VAN_OA.Model.EFrom.tb_QuotePrice model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                    }
                }
            }
            return model;
        }
      



        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_QuotePrice> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();          
            strSql.Append("select   ");
            strSql.Append("IsShuiYin,IsGaiZhang,ComName as AllName,IsRemark,LastYH,IsYH,LIlv, LaborCost,EngineeringTax,QPType,IsBrand,IsProduct, Remark,ZLBZ,YSBJ,FWBXDJ,JFQ,Total,loginName,tb_QuotePrice.Id,QuoteNo,GuestName,GuestNo,QuoteDate,ResultGuestName,ResultGuestNo,PayStyle,GuestNameToInv,ContactPerToInv,telToInv,AddressToInv,InvoHeader,InvContactPer,InvAddress,InvTel,NaShuiPer,brandNo,GuestNameTofa,ContactPerTofa,telTofa,AddressTofa,BuessName,BuessEmail,ComTel,ComChuanZhen,ComBusTel,tb_QuotePrice.ComName,NaShuiNo,Address,ComBrand,CreateUser,tb_QuotePrice.CreateTime ");
            strSql.Append(" from tb_QuotePrice left join tb_User on tb_User.id=tb_QuotePrice.CreateUser");
            //strSql.Append("  left join TB_Company on TB_Company.ComCode=tb_User.CompanyCode");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CreateTime desc");
            List<VAN_OA.Model.EFrom.tb_QuotePrice> list = new List<VAN_OA.Model.EFrom.tb_QuotePrice>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                      
                        model.AllName = dataReader["AllName"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.tb_QuotePrice ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_QuotePrice model = new VAN_OA.Model.EFrom.tb_QuotePrice();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.QuoteNo = dataReader["QuoteNo"].ToString();
            model.GuestName = dataReader["GuestName"].ToString();
            model.GuestNo = dataReader["GuestNo"].ToString();
            ojb = dataReader["QuoteDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.QuoteDate = (DateTime)ojb;
            }
            model.ResultGuestName = dataReader["ResultGuestName"].ToString();
            model.ResultGuestNo = dataReader["ResultGuestNo"].ToString();
            model.PayStyle = dataReader["PayStyle"].ToString();
            model.GuestNameToInv = dataReader["GuestNameToInv"].ToString();
            model.ContactPerToInv = dataReader["ContactPerToInv"].ToString();
            model.telToInv = dataReader["telToInv"].ToString();
            model.AddressToInv = dataReader["AddressToInv"].ToString();
            model.InvoHeader = dataReader["InvoHeader"].ToString();
            model.InvContactPer = dataReader["InvContactPer"].ToString();
            model.InvAddress = dataReader["InvAddress"].ToString();
            model.InvTel = dataReader["InvTel"].ToString();
            model.NaShuiPer = dataReader["NaShuiPer"].ToString();
            model.brandNo = dataReader["brandNo"].ToString();
            model.GuestNameTofa = dataReader["GuestNameTofa"].ToString();
            model.ContactPerTofa = dataReader["ContactPerTofa"].ToString();
            model.telTofa = dataReader["telTofa"].ToString();
            model.AddressTofa = dataReader["AddressTofa"].ToString();
            model.BuessName = dataReader["BuessName"].ToString();
            model.BuessEmail = dataReader["BuessEmail"].ToString();
            model.ComTel = dataReader["ComTel"].ToString();
            model.ComChuanZhen = dataReader["ComChuanZhen"].ToString();
            model.ComBusTel = dataReader["ComBusTel"].ToString();
            model.ComName = dataReader["ComName"].ToString();
            model.NaShuiNo = dataReader["NaShuiNo"].ToString();
            model.Address = dataReader["Address"].ToString();
            model.ComBrand = dataReader["ComBrand"].ToString();
            ojb = dataReader["CreateUser"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUser = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserName = ojb.ToString();
            }
            model.Total =Convert.ToDecimal(dataReader["Total"]);

            model.ZLBZ = dataReader["ZLBZ"].ToString();
            model.YSBJ = dataReader["YSBJ"].ToString();
            model.FWBXDJ = dataReader["FWBXDJ"].ToString();
            model.JFQ = dataReader["JFQ"].ToString();
            model.Remark = dataReader["Remark"].ToString();

            model.LaborCost = Convert.ToDecimal(dataReader["LaborCost"]);
            model.EngineeringTax = Convert.ToDecimal(dataReader["EngineeringTax"]);
            model.QPType = Convert.ToInt32(dataReader["QPType"]);
            model.IsBrand = Convert.ToBoolean(dataReader["IsBrand"]);
            model.IsProduct = Convert.ToBoolean(dataReader["IsProduct"]);
            model.LIlv = Convert.ToDecimal(dataReader["LIlv"]);
            model.LastYH = Convert.ToDecimal(dataReader["LastYH"]);
            model.IsYH = Convert.ToBoolean(dataReader["IsYH"]);
            model.IsRemark = Convert.ToBoolean(dataReader["IsRemark"]);
            model.IsShuiYin = Convert.ToBoolean(dataReader["IsShuiYin"]);
            model.IsGaiZhang = Convert.ToBoolean(dataReader["IsGaiZhang"]);
            
            return model;
        }

    }
}
