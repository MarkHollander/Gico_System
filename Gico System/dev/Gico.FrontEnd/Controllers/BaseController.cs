using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gico.FrontEndModels.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gico.FrontEnd.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        public ViewResult View(PageModel model)
        {
            if (model.HttpResponseCode == StatusCodes.Status404NotFound)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return base.View("Components/_404", model);
            }
            return base.View(model);
        }

        public ViewResult View(string viewName, PageModel model)
        {
            if (model.HttpResponseCode == StatusCodes.Status404NotFound)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return base.View("Components/_404", model);
            }
            return base.View(viewName, model);
        }

    }
}