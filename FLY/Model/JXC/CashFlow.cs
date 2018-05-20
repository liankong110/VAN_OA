using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class CashFlow
    {
        /// <summary>
        /// 已支付在途=需检数量*单价 
        /// </summary>
        public decimal XuJianTotal { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string PONO { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string POName { get; set; }
        /// <summary>
        /// AE
        /// </summary>
        public string AE { get; set; }
        /// <summary>
        /// 项目金额
        /// </summary>
        public decimal POTotal { get; set; }
        /// <summary>
        /// 含税
        /// </summary>
        public bool IsHanShui { get; set; }
        /// <summary>
        /// 发票号集合
        /// </summary>
        public string FPNoTOTAL { get; set; }
        /// <summary>
        /// 预估成本
        /// </summary>
        public decimal CostPrice { get; set; }
        public decimal TuiTotal { get; set; }
        public decimal SellInTotal { get; set; }
        /// <summary>
        /// 管理费
        /// </summary>
        public decimal OtherCostm { get; set; }
        /// <summary>
        /// 预估利润
        /// </summary>
        public decimal Profit { get; set; }
        /// <summary>
        /// 毛利
        /// </summary>
        public decimal MaoLiTotal { get; set; }
        /// <summary>
        /// 在途库存
        /// </summary>
        public decimal NotRuTotal { get; set; }
        /// <summary>
        /// 未出库总额
        /// </summary>
        public decimal NotRuSellTotal { get; set; }
        /// <summary>
        /// 出库成本
        /// </summary>
        public decimal SellOutTotal { get; set; }
        /// <summary>
        /// 采购总额
        /// </summary>
        public decimal LastCaiTotal { get; set; }
        /// <summary>
        /// 支付总额
        /// </summary>
        public decimal SupplierTotal { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public decimal ItemTotal { get; set; }
        /// <summary>
        /// 开票总额
        /// </summary>
        public decimal FPTotal { get; set; }
        /// <summary>
        /// 未开票额
        /// 项目金额扣减项目已开发票的金额 ，就是项目未开票金额
        /// </summary>
        public decimal NoFpTotal { get { return FaxPoTotal - FPTotal; } }
        /// <summary>
        /// 含税项目金额
        /// </summary>
        public decimal FaxPoTotal { get; set; }
        /// <summary>
        /// 到账额
        /// </summary>
        public decimal InvoiceTotal { get; set; }
        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal GoodSellPriceTotal { get; set; }
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal GoodTotal { get; set; }
        /// <summary>
        /// 利润 =实际收款-出库
        /// </summary>
        public decimal LiRunTotal
        {
            get
            {
                return InvoiceTotal - GoodTotal;
            }
        }
        /// <summary>
        /// 未收款 是 项目金额-到款额
        /// </summary>
        public decimal NotShouTotal { get { return POTotal - InvoiceTotal; } }
        /// <summary>
        /// 到账比率 =到帐额/项目金额（如果项目金额=0；该项按空白）
        /// </summary>
        public decimal InvoiceBiLiTotal { get { if (POTotal == 0) { return 0; } return InvoiceTotal / POTotal * 100; } }
        /// <summary>
        /// 支付比率=支付总额/采购单的总金额 （如果采购金额=0；该项按空白）
        /// </summary>
        public decimal? SupplierBiliTotal { get { if (LastCaiTotal == 0) { return null; } return SupplierTotal / LastCaiTotal * 100; } }
        /// <summary>
        /// 费用率=费用总额/项目金额（如果项目金额=0；该项按空白）
        /// </summary>
        public decimal? FeiYongTotal { get { if (POTotal == 0) { return null; } return ItemTotal / POTotal * 100; } }

        /// <summary>
        /// 我们的资金投入分为：支付总额+库存出库金额+费用总额 ，营收 分为 到款额，实际利润 两块，
        /// 我们可以设计一个指标： 资金回笼率=到款额/（支付总额+库存出库金额+费用总额）
        /// ，资金占用=支付总额+库存出库金额+费用总额-到款额，盈利能力=实际利润/（支付总额+库存出库金额+费用总额），盈利效率=盈利能力×到款率
        /// 库存出库金额 =（总出库金额-外采数量×采购金额）
        /// 资金回笼率=到款额/（支付总额+库存出库金额+费用总额）
        /// </summary>
        public decimal ZJHLV
        {
            get
            {
                if (SupplierTotal + NotKuCunTotal + ItemTotal == 0)
                {
                    return 0;
                }
                return InvoiceTotal / (SupplierTotal + NotKuCunTotal + ItemTotal) * 100;
            }
        }
        /// <summary>
        /// 最高资金回笼率
        /// </summary>
        public decimal TopZJHLV { get; set; }
        public bool IsNullTopZJHLV { get; set; }
        public decimal TempTopZJHLV { get { if (TopZJHLV > ZJHLV)return TopZJHLV; else return ZJHLV; } }
        /// <summary>
        /// 资金占用=支付总额+库存出库金额+费用总额-到款额
        /// </summary>
        public decimal ZJZY { get { return SupplierTotal + NotKuCunTotal + ItemTotal - InvoiceTotal; } }
        /// <summary>
        /// 最高资金占用
        /// </summary>
        public decimal TopZJZY { get; set; }
        public bool IsNullTopZJZY { get; set; }
        public decimal TempTopZJZY { get { if (TopZJZY > ZJZY)return TopZJZY; else return ZJZY; } }
        /// <summary>
        /// 盈利能力=实际利润/（支付总额+库存出库金额+费用总额）
        /// </summary>
        public decimal YLNL
        {
            get
            {
                if (SupplierTotal + NotKuCunTotal + ItemTotal == 0)
                {
                    return 0;
                }
                return LiRunTotal / (SupplierTotal + NotKuCunTotal + ItemTotal) * 100;
            }
        }
        /// <summary>
        /// 最高盈利能力
        /// </summary>
        public decimal TopYLNL { get; set; }
        public bool IsNullTopYLNL { get; set; }
        public decimal TempTopYLNL { get { if (TopYLNL > YLNL)return TopYLNL; else return YLNL; } }
        /// <summary>
        /// 盈利效率=盈利能力×到款率
        /// </summary>
        public decimal YLLV { get { return YLNL * InvoiceBiLiTotal/100; } }
        /// <summary>
        /// 最高盈利效率
        /// </summary>
        public decimal TopYLLV { get; set; }
        public bool IsNullTopYLLV { get; set; }
        public decimal TempTopYLLV { get { if (TopYLLV > YLLV)return TopYLLV; else return YLLV; } }
        /// <summary>
        /// 外采数量×采购金额  库存出库金额是该项目采购来自库存的商品的已出库金额。 库存出库金额 =（总出库金额-外采数量×采购金额）
        /// 
        /// 净库存出库成本总额
        /// </summary>
        public decimal NotKuCunTotal { get; set; }

        /// <summary>
        /// 非库存采购金额
        /// </summary>
        public decimal TrueNotKuCunTotal { get; set; }

        /// <summary>
        /// 资金投入=支付总额+库存出库金额+费用总额
        /// </summary>
        public decimal ZJTouRu { get { return SupplierTotal + NotKuCunTotal + ItemTotal; } }

        /// <summary>
        /// 项目应收款，我的理解是销售出库金额扣减到款金额，就是项目应收款
        /// </summary>
        public decimal YingShouTotal { get { return GoodSellPriceTotal - InvoiceTotal; } }

        /// <summary>
        /// 项目应付款，我的理解是项目的采购总金额扣减供应商预付款 和 供应商付款 ，就是项目应付款
        /// 原来的应付总额 =采购总额-已支付金额-应付库存 （错误）,
        /// 修改
        /// 应付总额=净采购总额-净支价总额-应付库存（正确）
        /// </summary>
        public decimal YingFuTotal { get { return LastCaiTotal - LastSupplierTotal - YingFuKuCun; } }
        /// <summary>
        /// 运营总盘子（=库存总金额+项目应收合计-项目应付合计）
        /// </summary>
        public decimal YingYunAllTotal { get { return YingShouTotal - YingFuTotal; } }

        /// <summary>
        /// 应付库存应付库存反应的是这个项目采购来自库存的 总金额（就是通过KC 支付出去的金额），
        ///原来的应付总额 =采购总额-已支付金额-应付库存 ,页码下面的应付合计右面也增加一个 应付库存合计:
        /// </summary>
        public decimal YingFuKuCun { get; set; }
        /// <summary>
        /// 采购退货总金额
        /// </summary>
        public decimal CaiOutTotal { get; set; }
        /// <summary>
        /// 预期应收=项目金额扣减到款金额
        /// </summary>
        public decimal YuQiYingShou { get { return POTotal - InvoiceTotal; } }

        /// <summary>
        /// 库存供应商未支付总额
        /// </summary>
        public decimal NoInvoice { get; set; }

        /// <summary>
        /// 净支价总额=SUM(支付数量×支付单价×采购单价/实采单价)- SUM（事后采购退货生成的即将或已完成负数支付单金额×采购金额/实采单价）
        /// </summary>
        public decimal LastSupplierTotal { get; set; }

        /// <summary>
        /// 预付未到库=采购未检验单中的 该项目编号的 预付未到库合计，并在已支付在途的左面增加一列，显示这个值
        /// </summary>
        public decimal YuFuWeiTotal { get; set; }

    }
}