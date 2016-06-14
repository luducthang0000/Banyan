using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banyan.Models;
using System.Text.RegularExpressions;
using System.Text;

namespace Banyan.Controllers
{
    public class vSpecialAdminController : BaseAdminController
    {
        private banyandbEntities db = new banyandbEntities();
        private FoodLiteDAL foodDAL = new FoodLiteDAL();
        private FoodSpecialDateDAL foodSpecialDateDAL = new FoodSpecialDateDAL();
        private CategoryDAL cateDAL = new CategoryDAL();
        private string Message = "Đây là trang quản lý các món được bán theo ngày chỉ định";
        private string Title = "Quản lý món theo ngày";
        //
        // GET: /vFoodAdmin/

        public ActionResult Index(int n = 0, Guid? categoryid = null, string searchString = "", string date = "")
        {
            List<Banyan.Models.FoodSpecialDateList> list = foodSpecialDateDAL.FoodSpecialDate(categoryid, searchString, date);

            ViewBag.Message = Message;
            ViewBag.Title = Title;
            ViewBag.categoryid = new SelectList(db.category.OrderBy(x => x.position).Select(x => new { x.id, x.name }), "id", "name");
            return View(list);
        }

        private string RemoveSign(string text)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = text.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public ActionResult FoodOption(Guid? categoryid = null, string searchString = "", string date = "")
        {
            IEnumerable<FoodLite> foods = foodSpecialDateDAL.FoodOptionSearchRemoveFoodInDate(categoryid, searchString, date);
            return Json(foods, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFoodInDate(string datestr = "", Guid? categoryid = null, string searchString = "")
        {

            if (!string.IsNullOrEmpty(datestr))
            {
                DateTime date;
                try
                {
                    date = Convert.ToDateTime(datestr);
                }
                catch
                {
                    return Json(new { Success = false, Message = "Ngày bán không đúng định dạng" }, JsonRequestBehavior.AllowGet);
                }
                IEnumerable<FoodLite> foods = foodSpecialDateDAL.FoodInDate(datestr);
                return Json(new { foodInDate = foods, foodOption = "" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddFoodInDate(Guid? id, string dateStr)
        {
            if (!string.IsNullOrEmpty(dateStr))
            {
                DateTime date;
                try
                {
                    date = Convert.ToDateTime(dateStr);
                }
                catch
                {
                    return Json(new { Success = false, Message = "Ngày bán không đúng định dạng" });
                }

                specialdate sd = foodSpecialDateDAL.GetSpecialDate(dateStr);
                if (sd != null)
                {
                    if (date.Date >= DateTime.Now.Date)
                    {
                        foodSpecialDateDAL.CreateFoodSpecialDate(id, sd);
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "Không thể quản lý món trong ngày của quá khứ" });
                    }
                }
                else
                {
                    if (date.Date >= DateTime.Now.Date)
                    {
                        specialdate newsd = foodSpecialDateDAL.CreateSpecialDate(dateStr);
                        foodSpecialDateDAL.CreateFoodSpecialDate(id, newsd);
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "Không thể quản lý món trong ngày của quá khứ" });
                    }
                }
            }
            return Json(new { Success = true, Message = "" });
        }

        [HttpPost]
        public ActionResult RemoveFoodInDate(Guid? id, string dateStr)
        {
            if (!string.IsNullOrEmpty(dateStr))
            {
                DateTime date;
                try
                {
                    date = Convert.ToDateTime(dateStr);
                }
                catch
                {
                    return Json(new { Success = false, Message = "Ngày bán không đúng định dạng" });
                }

                specialdate sd = foodSpecialDateDAL.GetSpecialDate(dateStr);
                if (sd != null)
                {
                    if (date.Date >= DateTime.Now.Date)
                    {
                        foodSpecialDateDAL.RemoveFoodSpecialDate(id, sd);
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "Không thể quản lý món trong ngày của quá khứ" });
                    }
                }
            }
            return Json(new { Success = true, Message = "" });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}