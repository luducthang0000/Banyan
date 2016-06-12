using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banyan.Models
{
    public class FoodLite
    {
        public FoodLite() { }
        public System.Guid id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string priceVND { get; set; }
        public string smallimage { get; set; }
        public System.Guid categoryid { get; set; }
        public string categoryname { get; set; }
        public int categoryposition { get; set; }
        public int position { get; set; }
    }

    public class FoodLiteAdmin
    {
        public FoodLiteAdmin() { }
        public System.Guid id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string priceVND { get; set; }
        public string smallimage { get; set; }
        public string bigimage { get; set; }
        public System.Guid categoryid { get; set; }
        public string categoryname { get; set; }
        public int categoryposition { get; set; }
        public bool allday { get; set; }
        public bool enable { get; set; }
        public int inventory { get; set; }
        public int sold { get; set; }
        public DateTime createdate { get; set; }
        public int position { get; set; }
    }

    public class ListFood
    {
        public ListFood() { }
        public ListFood(List<FoodLite> food, string category, string heading)
        {
            _Foods = food;
            _Category = category;
            _Heading = heading;
        }
        private List<FoodLite> _Foods;
        private string _Category;
        private string _Heading;
        public List<FoodLite> Foods
        {
            get { return _Foods; }
            set { _Foods = value; }
        }
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
        public string Heading
        {
            get { return _Heading; }
            set { _Heading = value; }
        }
    }
}
