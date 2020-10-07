using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd.DataAccess;
using MongoDB.Bson;

namespace Dimension_Data_Collab.Controllers
{
    public class DataController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public DataController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /*Actions to take:
        ListPage
        CreatePage
        EditPage
        DeletePage
        ViewSingle
        */


        //// GET: DataController/List/5S
        //public async Task<IActionResult> Index()
        //{
        //    var db = new DataAccessClass<BackEnd.Models.DataItem>("Test");
        //    var data = await db.GetAllRecords(1, 15);
        //    return View(data);
        //}
        [Route("data")]
        [Route("data/list")]
        [Route("data/index")]
        [Route("data/index/{id}")]
        [Route("data/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var db = new DataAccessClass<BackEnd.Models.DataItem>("Test");
            var data = await db.GetAllRecords((id > 0 ? id : 1), 15);
            return View(data);
        }

        [Route("data/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {

            new ObjectId();
            var db = new DataAccessClass<BackEnd.Models.DataItem>("Test");
            var data = await db.GetRecordById(new ObjectId(id));
            return View(data);
        }
    }
}
