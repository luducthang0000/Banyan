using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banyan.Models
{
    public class CategoryDAL
    {
        banyandbEntities db = new banyandbEntities();

        public IEnumerable<string> CategoryNameList()
        {
            return db.category.OrderBy(x => x.position).ThenBy(x => x.name).Select(x => x.name);
        }

        public IEnumerable<string> CategoryNameMenu()
        {
            return db.category.OrderBy(x => x.position).ThenBy(x => x.name).Select(x => x.name).Take(7);
        }

        public System.Web.Mvc.SelectList CategoryDropdownList()
        {
            return new System.Web.Mvc.SelectList(db.category.OrderBy(x => x.position).ThenBy(x => x.name).Select(x => new { x.id, x.name }), "id", "name");
        }
    }
}