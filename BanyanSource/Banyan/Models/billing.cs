//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Banyan.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class billing
    {
        public billing()
        {
            this.billingdetail = new HashSet<billingdetail>();
        }
    
        public System.Guid id { get; set; }
        public System.Guid customerid { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public Nullable<int> count { get; set; }
        public Nullable<decimal> totalprice { get; set; }
        public string note { get; set; }
        public Nullable<System.DateTime> deliverydatetime { get; set; }
        public string address { get; set; }
        public string customername { get; set; }
        public string phone { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual ICollection<billingdetail> billingdetail { get; set; }
    }
}