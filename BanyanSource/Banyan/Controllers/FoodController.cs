using Banyan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Banyan.Controllers
{
    public class FoodController : BaseController
    {
        //
        // GET: /Food/
        banyandbEntities db = new banyandbEntities();
        FoodLiteDAL foodDAL = new FoodLiteDAL();

        public ActionResult AllFood()
        {
            List<Banyan.Models.ListFood> listItems = foodDAL.AllAvailableInCategory();
            return View(listItems);
        }
    }
}
