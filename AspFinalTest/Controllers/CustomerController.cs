using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AspFinalTest.Models;

namespace AspFinalTest.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        Models.CustomerService customerService = new Models.CustomerService();
        Models.CodeService codeService = new Models.CodeService();
        // GET: Orders
        public ActionResult Index(Models.Customer selectitem)
        {

            ViewBag.Customerdata = customerService.GetCustomer(selectitem);

            CodeService CodeService = new CodeService();
            List<Code> result1 = CodeService.GetTitle();
            List<SelectListItem> CodeData = new List<SelectListItem>();
            CodeData.Add(new SelectListItem()
            {
                Text = "",
                Value = null
            });
            foreach (var item1 in result1)
            {
                CodeData.Add(new SelectListItem()
                {
                    Text = item1.CodeVal.ToString(),
                    Value = item1.CodeId.ToString()
                });
                ViewData["CodeData"] = CodeData;
            }

            return View(new Models.Customer());
        }
    }
}