using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;

namespace FBD.Controllers
{
    //TODO: check Rights
    //TODO: check industry name and id unique
    public class BSNIndustryController : Controller
    {
        //
        // GET: /BSNIndustry/
        FBDEntities entities = DatabaseAccess.Entities;

        public ActionResult Index()
        {
            var model = BusinessIndustries.SelectIndustries();
            return View(model);
        }

        //
        // GET: /BSNIndustry/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BSNIndustry/Add

        public ActionResult Add()
        {
            return View();
        } 

        //
        // POST: /BSNIndustry/Add

        [HttpPost]
        public ActionResult Add(BusinessIndustries industry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entities.AddToBusinessIndustries(industry);
                    entities.SaveChanges();
                }
                else throw new Exception();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                industry.Error = ex.Message;
                return View(industry);
            }
        }
        
        //
        // GET: /BSNIndustry/Edit/5
 
        public ActionResult Edit(string id)
        {
            var model = BusinessIndustries.SelectIndustryByID(id);
            return View(model);
        }

        //
        // POST: /BSNIndustry/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, BusinessIndustries industry)
        {
            try
            {
                var temp=BusinessIndustries.SelectIndustryByID(id);
                if (ModelState.IsValid)
                {

                    temp.IndustryName = industry.IndustryName;
                    entities.SaveChanges();
                }
                else throw new ArgumentException();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                industry.Error = ex.Message;
                return View(industry);
            }
        }

        //
        // GET: /BSNIndustry/Delete/5
 
        public ActionResult Delete(string id)
        {
            var industry = entities.BusinessIndustries.First(i => i.IndustryID == id);
            entities.DeleteObject(industry);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /BSNIndustry/Delete/5

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
