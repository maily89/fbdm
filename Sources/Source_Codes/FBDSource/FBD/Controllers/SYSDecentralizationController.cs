﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FBD.Controllers
{
    public class SYSDecentralizationController : Controller
    {
        //
        // GET: /SYSDecentralization/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /SYSDecentralization/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SYSDecentralization/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SYSDecentralization/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /SYSDecentralization/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SYSDecentralization/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SYSDecentralization/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SYSDecentralization/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
