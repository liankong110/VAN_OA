using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class CashFlowReport
    {

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
        /// 未收款 是 项目金额-到款额
        /// </summary>
        public decimal NotShouTotal { get { return POTotal - InvoiceTotal; } }

        /// <summary>
        /// 支付比率=支付总额/采购单的总金额 （如果采购金额=0；该项按空白）
        /// </summary>
        public decimal? SupplierBiliTotal { get { if (LastCaiTotal == 0) { return null; } return SupplierTotal / LastCaiTotal * 100; } }


        /// <summary>
        /// 非库存采购金额
        /// </summary>
        public decimal TrueNotKuCunTotal { get; set; }



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
        /// 净支价总额=SUM(支付数量×支付单价×采购单价/实采单价)- SUM（事后采购退货生成的即将或已完成负数支付单金额×采购金额/实采单价）
        /// </summary>
        public decimal LastSupplierTotal { get; set; }

        /// <summary>
        /// 供应商可支付
        /// </summary>
        public decimal ZhiTotal { get; set; }
        /// <summary>
        /// 支付进行款
        /// </summary>
        public decimal ZhiTotalIng { get; set; }
        /// <summary>
        /// 供应商可预付
        /// </summary>
        public decimal YuTotal { get; set; }
        /// <summary>
        /// 预付进行款
        /// </summary>
        public decimal YuTotalIng { get; set; }
        /// <summary>
        /// 未检验可支付
        /// </summary>
        public decimal ToSupplierTotal { get; set; }

        /// <summary>
        /// 差额=应付总额（运营指标中的应付总额）-供应商可支付-供应商可预付-在检金额-支付进行款-预付进行款，
        /// 理论上差额应该为0，如不等于0，说明我们的数据流转在某个地方有问题
        /// </summary>
        public decimal DiffTotal
        {
            get
            {
                return YingFuTotal - ZhiTotal-YuTotal- ToSupplierTotal- ZhiTotalIng-YuTotalIng- CheckIngTotal;
            }
        }

        public bool IsZhuan { get; set; }

        public decimal CheckIngTotal { get; set; }
    }
}