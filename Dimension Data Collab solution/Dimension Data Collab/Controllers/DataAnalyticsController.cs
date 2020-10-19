using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Dimension_Data_Collab.Controllers
{
    public class DataAnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
