using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BussinessLogic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Operations;

namespace Dimension_Data_Collab.Controllers
{
    public class DataAnalyticsController : Controller
    {
        private DataLogic dataLogic;
        public DataAnalyticsController(DataLogic _dataLogic)
        {
            dataLogic = _dataLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var data = await dataLogic.GetGenderCount();
            ViewBag.Data = data;
            return View();
        }
    }
}
