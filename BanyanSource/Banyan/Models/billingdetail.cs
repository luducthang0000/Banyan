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
    
    public partial class billingdetail
    {
        public System.Guid id { get; set; }
        public System.Guid billingid { get; set; }
        public Nullable<System.Guid> foodid { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> count { get; set; }
        public Nullable<decimal> totalprice { get; set; }
    
        public virtual billing billing { get; set; }
        public virtual food food { get; set; }
    }
}
