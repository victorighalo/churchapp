using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingdomJoy.Helpers
{
    public class Helpers
    {
       
    }

    public class FormOptions
    {
        public IEnumerable<System.Web.Mvc.SelectListItem> YesNo()
        {
            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();
            items.Add(new System.Web.Mvc.SelectListItem() { Text = "Yes", Value = "1", Selected = false });
            items.Add(new System.Web.Mvc.SelectListItem() { Text = "No", Value = "0", Selected = false });
            return items;
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> Gender()
        {
            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();
            items.Add(new System.Web.Mvc.SelectListItem() { Text = "Male", Value = "1", Selected = false });
            items.Add(new System.Web.Mvc.SelectListItem() { Text = "Female", Value = "0", Selected = false });
            return items;
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> MartitalStatus()
        {
            List<System.Web.Mvc.SelectListItem> items = new List<System.Web.Mvc.SelectListItem>();
            items.Add(new System.Web.Mvc.SelectListItem() { Text = "Single", Value = "1", Selected = false });
            items.Add(new System.Web.Mvc.SelectListItem() { Text = "Married", Value = "2", Selected = false });
            return items;
        }
    }
}