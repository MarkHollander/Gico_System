using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gico.FrontEnd.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index(string code)
        {
            string tmpCode = "001";

            return View();
        }
    }
}