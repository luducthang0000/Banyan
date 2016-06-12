using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banyan.Models
{
    public class CategoryLite
    {
        public CategoryLite() { }
        public string name { get; set; }
        public Nullable<int> position { get; set; }
    }
}