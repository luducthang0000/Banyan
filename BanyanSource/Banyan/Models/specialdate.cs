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
    
    public partial class specialdate
    {
        public specialdate()
        {
            this.food = new HashSet<food>();
        }
    
        public System.Guid id { get; set; }
        public string date { get; set; }
    
        public virtual ICollection<food> food { get; set; }
    }
}
