using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Banyan.Models
{
    public class FoodLiteDAL
    {
        banyandbEntities db = new banyandbEntities();
        CategoryDAL cateDAL = new CategoryDAL();
        public List<FoodLite> AllAvailable()
        {
            string shortDate = DateTime.Now.ToShortDateString();
            return db.food.Where(x => x.allday == true || x.specialdate.Any(y => y.date == shortDate)).Select(x => new FoodLite
            {
                id = x.id,
                name = x.name,
                price = x.price,
                smallimage = string.IsNullOrEmpty(x.smallimage) ? "blank-image.jpg" : x.smallimage,
                categoryname = x.category.name,
                categoryposition = x.category.position.Value,
                position = x.position.Value
            }).ToList().Select(x =>
            {
                x.priceVND = ToMoney(x.price); return x;
            })
            .OrderBy(x => x.categoryposition).ThenBy(x => x.position).ThenBy(x => x.name).ToList();
        }

        public IEnumerable<FoodLiteAdmin> AllAvailableAdmin()
        {
            return db.food.Select(x => new FoodLiteAdmin
            {
                id = x.id,
                name = x.name,
                price = x.price,
                smallimage = string.IsNullOrEmpty(x.smallimage) ? "blank-image.jpg" : x.smallimage,
                bigimage = string.IsNullOrEmpty(x.bigimage) ? "blank-image.jpg" : x.bigimage,
                categoryname = x.category.name,
                categoryposition = x.category.position.Value,
                position = x.position.Value,
                allday = x.allday,
                enable = x.enable,
                inventory = x.inventory.Value,
                sold = x.sold.Value,
                createdate = x.createdate.Value
            }).OrderBy(x => x.categoryposition).ThenBy(x => x.position).ThenBy(x => x.name);
        }

        public List<ListFood> AllAvailableInCategory()
        {
            List<FoodLite> items = AllAvailable();
            List<ListFood> listFoods = new List<ListFood>();
            List<string> cates = cateDAL.CategoryNameList().ToList();
            foreach (string category in cates)
            {
                var cateFood = items.Where(x => x.categoryname == category).ToList();
                if (cateFood.Count > 0)
                {
                    ListFood list = new ListFood();
                    list.Foods = cateFood;
                    list.Category = category;
                    list.Heading = category == "Cơm chay" ? category.ToUpper() + " " + DateTime.Now.ToString("dd-MM") : category.ToUpper();
                    listFoods.Add(list);
                }
            }
            return listFoods;
        }

        private string ToMoney(decimal price)
        {
            return (price / 1000m).ToString("0.000");
        }

        public int MaxPosition
        {
            get { return 999; }
        }

        public void UpdatePosition(food food)
        {
            int nextPosition = food.position.Value;
            food curFood = food;
            while (db.food.Any(x => x.position == nextPosition && x.categoryid == curFood.categoryid && x.id != curFood.id))
            {
                IEnumerable<food> foods = db.food.Where(x => x.position == nextPosition && x.categoryid == curFood.categoryid && x.id != curFood.id);
                foreach (var item in foods)
                {
                    item.position++;
                }
                db.SaveChanges();
                nextPosition++;
                curFood = db.food.FirstOrDefault(x => x.position == nextPosition && x.categoryid == curFood.categoryid);
            };
        }

        public IEnumerable<FoodLiteAdmin> Search(Guid? categoryid = null, string searchString = "", int isAllday = -1, int isEnable = -1)
        {
            IEnumerable<food> foods = db.food;

            if (categoryid != null)
                foods = foods.Where(x => x.category.id == categoryid);
            if (!string.IsNullOrEmpty(searchString))
                foods = foods.Where(x => RemoveSign(x.name.ToUpper()).Contains(RemoveSign(searchString.ToUpper())));
            if (isAllday != -1)
                foods = foods.Where(x => x.allday == Convert.ToBoolean(isAllday));
            if (isEnable != -1)
                foods = foods.Where(x => x.enable == Convert.ToBoolean(isEnable));

            IEnumerable<FoodLiteAdmin> results = foods.OrderBy(x => x.category.name).ThenBy(x => x.position).ThenBy(x => x.name).Select(x => new FoodLiteAdmin
            {
                id = x.id,
                name = x.name,
                price = x.price,
                smallimage = string.IsNullOrEmpty(x.smallimage) ? "blank-image.jpg" : x.smallimage,
                bigimage = string.IsNullOrEmpty(x.bigimage) ? "blank-image.jpg" : x.bigimage,
                categoryname = x.category.name,
                position = x.position.Value,
                allday = x.allday,
                enable = x.enable,
                createdate = x.createdate.Value
            });
            return results;
        }
        private string RemoveSign(string text)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = text.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}