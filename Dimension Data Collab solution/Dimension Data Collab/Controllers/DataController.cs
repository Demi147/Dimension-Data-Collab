﻿using System;
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
        private readonly ILogger<HomeController> logger;
        private readonly DataLogic dataLogic;

        public DataController(ILogger<HomeController> _logger, DataLogic _dataLogic)
        {
            logger = _logger;
            dataLogic = _dataLogic;
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
        public async Task<IActionResult> Index(int page,string searchString, string cat)
        {

            int pageTemp = (page > 0 ? page : 1);
            var items = new List<DataItem>();
            if (string.IsNullOrEmpty(searchString))
            {
                //searchString = ViewData["query"].ToString();
            }
            if (string.IsNullOrEmpty(searchString))
            {
                items = await dataLogic.GetAllRecords(pageTemp, 15);
            }
            else
            {
                items = await dataLogic.GetAllRecords(pageTemp, 15,searchString,cat);
            }
            var pages = await dataLogic.GetCount();
            ViewData["query"] = searchString;
            ViewData["pages"] = (int)(pages / 15);
            ViewData["page"] = pageTemp;
            return View(items);
        }

        [Authorize(Roles = "manager,admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "manager,admin")]
        public async Task<IActionResult> Create(DataItem model)
        {
            if (ModelState.IsValid)
            {
                //add here
                await dataLogic.InsertRecord(model);
            }
            return View();
        }

        [Route("data/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var data = await dataLogic.GetRecordById(new ObjectId(id));
            return View(data);
        }

        [Route("data/Delete/{id}")]
        [Authorize(Roles = "manager,admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await dataLogic.GetRecordById(new ObjectId(id));
            return View(data);
        }

        [Route("data/Delete/{id}")]
        [HttpPost]
        public IActionResult DeletePost(string id)
        {
            if (ModelState.IsValid)
            {
                dataLogic.DeleteRecord(new ObjectId(id));
                return RedirectToAction("index");
            }
            ViewData["error"] = "Invalid model";
            return View();
        }

        [Route("data/Update/{id}")]
        [Authorize(Roles = "manager,admin")]
        public async Task<IActionResult> Update(string id)
        {
            var data = await dataLogic.GetRecordById(new ObjectId(id));
            return View(data);
        }

        [Route("data/Update/{id}")]
        [HttpPost]
        [Authorize(Roles = "manager,admin")]
        public IActionResult Update(string id,DataItem model)
        {
            if (ModelState.IsValid)
            {
                dataLogic.UpsertRecord(new ObjectId(id),model);
                return RedirectToAction("index");
            }
            ViewData["error"] = "Invalid model";
            return View();
        }
    }
}
