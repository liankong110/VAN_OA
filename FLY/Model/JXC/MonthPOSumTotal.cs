using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
	public class MonthPOSumTotal
	{
		public string AE { get; set; }
		public decimal January { get; set; }
		public decimal February { get; set; }
		public decimal March { get; set; }
		public decimal April { get; set; }
		public decimal May { get; set; }
		public decimal June { get; set; }
		public decimal July { get; set; }
		public decimal August { get; set; }
		public decimal September { get; set; }
		public decimal October { get; set; }
		public decimal November { get; set; }
		public decimal December { get; set; }

		public decimal AllTotal
		{
			get
			{
				return January + February + March + April + May + June + July + August + September + October + November + December;
			}
		}

	}
}