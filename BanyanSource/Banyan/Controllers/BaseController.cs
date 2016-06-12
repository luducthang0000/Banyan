using Banyan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banyan.Controllers
{
    public class BaseController : Controller
    {
        CategoryDAL cateDAL = new CategoryDAL();
        //
        // GET: /Base/
        public BaseController()
        {
            ViewBag.Category = cateDAL.CategoryNameMenu();
        }
    }
}
