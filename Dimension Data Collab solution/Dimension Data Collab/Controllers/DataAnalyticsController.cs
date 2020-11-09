using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.BussinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Operations;
using Newtonsoft.Json;

namespace Dimension_Data_Collab.Controllers
{
    [Authorize]
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
            ViewBag.MaleData = JsonConvert.SerializeObject(data.Item1);
            ViewBag.FemaleData = JsonConvert.SerializeObject(data.Item2);

            //complex data
            var complexData = await dataLogic.GetComplexData();
            //xvalues,jobinvolement,JobSatisfaction,education,EnvironmentSatisfaction,JobLevel
            ViewBag.ComplexLabels = JsonConvert.SerializeObject(complexData.Item1);
            ViewBag.jobinvolement = JsonConvert.SerializeObject(complexData.Item2);
            ViewBag.JobSatisfaction = JsonConvert.SerializeObject(complexData.Item3);
            ViewBag.education = JsonConvert.SerializeObject(complexData.Item4);
            ViewBag.EnvironmentSatisfaction = JsonConvert.SerializeObject(complexData.Item5);
            ViewBag.JobLevel = JsonConvert.SerializeObject(complexData.Item6);
            return View();
        }
    }
}
