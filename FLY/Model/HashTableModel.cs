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
using System.Collections.Generic;
using System.Collections;

namespace VAN_OA.Model
{
    public class HashTableModel : IComparable<HashTableModel>
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public List<HashTableModel> HashTableToList(Hashtable hs)
        {
            List<HashTableModel> modelList = new List<HashTableModel>();
            foreach (var k in hs.Keys)
            {
                modelList.Add(new HashTableModel { Key = k.ToString(), Value = hs[k].ToString() });
            }
            return modelList;
        }

        #region IComparable 成员

        public int CompareTo(HashTableModel other)
        {
            return this.Key.CompareTo(other.Key);
        }

        public int CompareTo1(HashTableModel x,HashTableModel y)
        {
            return x.Value.CompareTo(y.Value);
        }

        #endregion


    }
}
