using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd.DataAccess;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using BackEnd.Models;
using BackEnd.BussinessLogic;

namespace Dimension_Data_Collab.Controllers
{
    [Authorize(Roles ="user")]
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
        [Route("data/index")]
        [Route("data/index/{page}")]
        public async Task<IActionResult> Index(int page)
        {
            var items = await DataLogic.GetItems(page);
            var pages = await DataLogic.GetCount();
            var pageSize = 15;
            ViewData["pages"] = (int)(pages / pageSize);
            ViewData["page"] = (page > 0 ? page : 1);
            return View(items);
        }

        [Route("data/details/{id}")]
        [Authorize(Roles ="manager")]
        public async Task<IActionResult> Details(string id)
        {

            //new ObjectId();
            //var db = new DataAccessClass<BackEnd.Models.DataItem>("Test");
            //var data = await db.GetRecordById(new ObjectId(id));
            return View();
        }
    }
}
