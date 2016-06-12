using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Banyan.Models
{
    public class FoodSpecialDateDAL
    {
        banyandbEntities db = new banyandbEntities();
        public IEnumerable<FoodLite> FoodOptionSearch(Guid? categoryid = null, string searchString = "")
        {
            IEnumerable<food> foods = db.food.Where(x => x.enable == true);
            if (categoryid != null)
                foods = foods.Where(x => x.category.id == categoryid);
            if (!string.IsNullOrEmpty(searchString))
                foods = foods.Where(x => RemoveSign(x.name.ToUpper()).Contains(RemoveSign(searchString.ToUpper())));

            IEnumerable<FoodLite> results = foods.OrderBy(x => x.category.name).ThenBy(x => x.position).ThenBy(x => x.name).Select(x => new FoodLite
            {
                id = x.id,
                name = x.name,
                price = x.price,
                smallimage = string.IsNullOrEmpty(x.smallimage) ? "blank-image.jpg" : x.smallimage,
                categoryname = x.category.name
            });

            return results;
        }

        public IEnumerable<FoodLite> FoodOptionSearchRemoveFoodInDate(Guid? categoryid = null, string searchString = "", string date = "")
        {
            IEnumerable<FoodLite> foods = FoodOptionSearch(categoryid, searchString);
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.AddDays(1).ToShortDateString();
            }
            foods = foods.Except(FoodInDate(date));
            return foods;
        }
        public List<Banyan.Models.FoodSpecialDateList> FoodSpecialDate(Guid? categoryid = null, string searchString = "", string date = "")
        {
            List<Banyan.Models.FoodSpecialDateList> foodSpecialDateList = new List<Banyan.Models.FoodSpecialDateList>();
            List<FoodLite> foodInDate = FoodInDate(date).ToList();
            List<FoodLite> foodlitesResult = FoodOptionSearch(categoryid, searchString).Except(foodInDate).ToList();

            foodSpecialDateList.Add(new Banyan.Models.FoodSpecialDateList(foodlitesResult));
            foodSpecialDateList.Add(new Banyan.Models.FoodSpecialDateList(foodInDate));

            return foodSpecialDateList;
        }

        public IEnumerable<FoodLite> FoodInDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.AddDays(1).ToShortDateString();
            }
            IEnumerable<FoodLite> foods = db.specialdate.Where(x => x.date == date).SelectMany(x => x.food).Where(x => x.enable == true).OrderBy(x => x.category.name).ThenBy(x => x.position).ThenBy(x => x.name).Select(x => new FoodLite
            {
                id = x.id,
                name = x.name,
                price = x.price,
                smallimage = string.IsNullOrEmpty(x.smallimage) ? "blank-image.jpg" : x.smallimage,
                categoryname = x.category.name
            });

            return foods;
        }

        private string RemoveSign(string text)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = text.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        internal specialdate CreateSpecialDate(string dateStr)
        {
            specialdate sd = new specialdate()
            {
                id = Guid.NewGuid(),
                date = dateStr
            };
            db.specialdate.Add(sd);
            db.SaveChanges();
            return sd;
        }
        internal specialdate GetSpecialDate(string date) 
        {
            return db.specialdate.FirstOrDefault(x => x.date == date);
        }
        internal void CreateFoodSpecialDate(Guid? foodid, specialdate sd)
        {
            food food = db.food.Find(foodid);
            food.specialdate.Add(sd);
            db.SaveChanges();
        }
    }
}