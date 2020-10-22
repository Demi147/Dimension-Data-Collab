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
    [Authorize]
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
        [Route("data/{page}")]
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
        public async Task<IActionResult> Details(string id)
        {
            var data = await DataLogic.GetItemById(new ObjectId(id));
            return View(data);
        }

        [Route("data/Delete/{id}")]
        [Authorize(Roles = "manager,admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await DataLogic.GetItemById(new ObjectId(id));
            return View(data);
        }

        [Route("data/Delete/{id}")]
        [HttpPost]
        public IActionResult DeletePost(string id)
        {
            if (ModelState.IsValid)
            {
                DataLogic.DeleteItemById(new ObjectId(id));
                return RedirectToAction("index");
            }
            ViewData["error"] = "Invalid model";
            return View();
        }

        [Route("data/Update/{id}")]
        [Authorize(Roles = "manager,admin")]
        public async Task<IActionResult> Update(string id)
        {
            var data = await DataLogic.GetItemById(new ObjectId(id));
            return View(data);
        }

        [Route("data/Update/{id}")]
        [HttpPost]
        public IActionResult Update(DataItem model)
        {
            if (ModelState.IsValid)
            {
                DataLogic.UpdateItem(model);
                return RedirectToAction("index");
            }
            ViewData["error"] = "Invalid model";
            return View();
        }
    }
}
