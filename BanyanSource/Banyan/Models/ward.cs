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
    
    public partial class ward
    {
        public ward()
        {
            this.customer = new HashSet<customer>();
        }
    
        public System.Guid id { get; set; }
        public Nullable<System.Guid> district { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<customer> customer { get; set; }
        public virtual district district1 { get; set; }
    }
}
