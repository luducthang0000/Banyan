using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banyan.Models
{
    public class FoodSpecialDateLite { }
    public class FoodSpecialDateList
    {
        public FoodSpecialDateList() { }
        public FoodSpecialDateList(List<FoodLite> food)
        {
            _Foods = food;
        }
        private List<FoodLite> _Foods;
        public List<FoodLite> Foods
        {
            get { return _Foods; }
            set { _Foods = value; }
        }
    }
}