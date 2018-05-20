using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Dal;

namespace VAN_OA.JXC
{
    public class SysObj
    {
        /// <summary>
        /// 项目订单列表
        /// </summary>
        public static string CG_OrderList = "项目订单列表";

        /// <summary>
        /// 采购订单列表
        /// </summary>
        public static string CAI_OrderList = "采购订单列表";


        /// <summary>
        /// 采购订单检验列表
        /// </summary>
        public static string CAI_OrderCheckList = "采购订单检验列表";


        /// <summary>
        /// 采购入库列表
        /// </summary>
        public static string CAI_OrderInHouseList = "采购入库列表";

        /// <summary>
        /// 采购退货列表
        /// </summary>
        public static string CAI_OrderOutHouseList = "采购退货列表";

        /// <summary>
        /// 销售出库列表
        /// </summary>
        public static string Sell_OrderOutHouseList = "销售出库列表";

        /// <summary>
        /// 出库单签回列表
        /// </summary>
        public static string Sell_OrderOutHouseBackList="出库单签回列表";
        /// <summary>
        /// 销售退货列表
        /// </summary>
        public static string Sell_OrderInHouseList = "销售退货列表";


        /// <summary>
        /// 销售报表明细
        /// </summary>
        public static string JXC_REPORTList = "销售报表明细";

        /// <summary>
        /// 销售报表汇总
        /// </summary>
        public static string JXC_REPORTTotalList = "销售报表汇总";


        /// <summary>
        /// 采购订单列表2
        /// </summary>
        public static string WFvAllCaiOrderList = "采购订单列表2";


        /// <summary>
        /// 销售发票列表
        /// </summary>
        public static string Sell_OrderPFList = "销售发票列表";


        /// <summary>
        /// 发票单签回列表
        /// </summary>
        public static string Sell_OrderPFBackList = "发票单签回列表";


        /// <summary>
        /// 到款单列表
        /// </summary>
        public static string WFToInvoiceList = "到款单列表";


        /// <summary>
        /// 查看部门
        /// </summary>
        public static string WFScanDepartList = "查看部门";


        

        public static bool IfShowAll(string type, object userId,string objName)
        {
//            SysObjectService model = new SysObjectService();
//            string sql = "";
//            //            string.Format(@"select count(*) from sys_Object where roleId in (select roleId from Role_User where userId={1})
//            //and formId in(select formID from sys_form where displayName='{0}') ", type, userId);


//            sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id
//where  role_Id in (select roleId from Role_User where userId={1}) and sys_form_Id in(select formID from sys_form where displayName='{0}') and sys_Object.AutoID is not null and sys_Object.Name='{2}'", type, userId,
//                                                                                                                                                                                                      objName);
//            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
//            {
//                return false;
//            }
//            return true;

            return NewShowAll_Name(type, userId, objName);
        }


        /// <summary>
        /// 对于'不能编辑' 来说 如果返回为0 说明 不能编辑  反正 可以
        /// 对于'查看所有' 来说 如果返回为0 说明 不能查看所有  反正 能查看所有
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="displayName"></param>
        /// <param name="textName"></param>
        /// <returns></returns>
        public static int NewRole(string currentUserId, string displayName, string textName)
        {
            string sql = string.Format(@"declare @result int;
set @result=0;
select @result=COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
and sys_Object.AutoID is not null;
select @result=COUNT(*)-@result from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
select @result;
", currentUserId, displayName, textName);
            return Convert.ToInt32(DBHelp.ExeScalar(sql));             
        }

        /// <summary>
        /// 对于'不能编辑' 来说 如果返回为0 说明 不能编辑  反正 可以
        /// 对于'查看所有' 来说 如果返回为0 说明 不能查看所有  反正 能查看所有
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="displayName"></param>
        /// <param name="textName"></param>
        /// <returns></returns>
        public static bool NewShowAll_Name(string displayName, object currentUserId, string objName)
        {
            string sql = string.Format(@"declare @result int;
set @result=0;
select @result=COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and Name='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
and sys_Object.AutoID is not null;
select @result=COUNT(*)-@result from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and Name='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
select @result;
", currentUserId, displayName, objName);

            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) == 0)
            {
                return false;
            }
            return true;
           
        }
        /// <summary>
        /// 对于'不能编辑' 来说 如果返回为0 说明 不能编辑  反正 可以
        /// 对于'查看所有' 来说 如果返回为0 说明 不能查看所有  反正 能查看所有
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="displayName"></param>
        /// <param name="textName"></param>
        /// <returns></returns>
        public static bool NewShowAll_textName(string displayName, object currentUserId, string textName)
        {
            string sql = string.Format(@"declare @result int;
set @result=0;
select @result=COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
and sys_Object.AutoID is not null;
select @result=COUNT(*)-@result from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
select @result;
", currentUserId, displayName, textName);

            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) == 0)
            {
                return false;
            }
            return true;

        }
    }


}
