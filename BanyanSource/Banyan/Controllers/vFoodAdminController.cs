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
    public class vFoodAdminController : BaseAdminController
    {
        private banyandbEntities db = new banyandbEntities();
        private FoodLiteDAL foodDAL = new FoodLiteDAL();
        private CategoryDAL cateDAL = new CategoryDAL();
        private string Message = "Đây là trang quản lý các món được bán";
        private string Title = "Quản lý món";
        //
        // GET: /vFoodAdmin/

        public ActionResult Index(int n = 0, Guid? categoryid = null, string searchString = "", int isAllday = -1, int isEnable = -1)
        {
            IEnumerable<FoodLiteAdmin> foods = foodDAL.Search(categoryid, searchString, isAllday, isEnable);

            ViewBag.Message = Message;
            ViewBag.Title = Title;
            ViewBag.categoryid = new SelectList(db.category.OrderBy(x=>x.position).Select(x => new {x.id,x.name }), "id", "name");
            return View(foods);
        }

        private string RemoveSign(string text)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = text.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        //
        // GET: /vFoodAdmin/Details/5

        public ActionResult Details(Guid? id = null)
        {
            food food = db.food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            ViewBag.Message = Message;
            ViewBag.Title = Title;
            ViewBag.ActionTitle = "Thông tin chi tiết món";
            return View(food);
        }

        //
        // GET: /vFoodAdmin/Create

        public ActionResult Create()
        {
            ViewBag.Message = Message;
            ViewBag.Title = Title;
            ViewBag.ActionTitle = "Tạo món mới";
            ViewBag.categoryid = new SelectList(db.category.OrderBy(x => x.position).Select(x => new { x.id, x.name }), "id", "name");
            return View();
        }

        //
        // POST: /vFoodAdmin/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(food food)
        {
            if (ModelState.IsValid)
            {
                food.id = Guid.NewGuid();
                food.sold = 1;
                food.inventory = 1000;
                food.createdate = DateTime.Now;
                food.position = food.position != null ? food.position : foodDAL.MaxPosition;
                db.food.Add(food);
                db.SaveChanges();
                foodDAL.UpdatePosition(food);
                return RedirectToAction("Index");
            }

            ViewBag.Message = Message;
            ViewBag.Title = Title;
            ViewBag.categoryid = new SelectList(db.category.OrderBy(x => x.position).Select(x => new { x.id, x.name }), "id", "name", food.categoryid);
            return View(food);
        }

        //
        // GET: /vFoodAdmin/Edit/5

        public ActionResult Edit(Guid? id = null)
        {
            food food = db.food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            ViewBag.Message = Message;
            ViewBag.Title = Title;
            ViewBag.ActionTitle = "Chỉnh sửa món";
            ViewBag.categoryid = new SelectList(db.category.OrderBy(x => x.position).Select(x => new { x.id, x.name }), "id", "name", food.categoryid);
            return View(food);
        }

        //
        // POST: /vFoodAdmin/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(food food)
        {
            if (ModelState.IsValid)
            {
                food.position = food.position != null ? food.position : foodDAL.MaxPosition;
                    
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                foodDAL.UpdatePosition(food);
                return RedirectToAction("Index");
            }

            ViewBag.Message = Message;
            ViewBag.Title = Title;
            ViewBag.categoryid = new SelectList(db.category.OrderBy(x => x.position).Select(x => new { x.id, x.name }), "id", "name", food.categoryid);
            return View(food);
        }

        //
        // GET: /vFoodAdmin/Delete/5

        public ActionResult Delete(Guid? id = null)
        {
            food food = db.food.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            ViewBag.Message = Message;
            ViewBag.Title = Title;
            ViewBag.ActionTitle = "Xóa món";
            return View(food);
        }

        //
        // POST: /vFoodAdmin/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            food food = db.food.Find(id);
            db.food.Remove(food);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}