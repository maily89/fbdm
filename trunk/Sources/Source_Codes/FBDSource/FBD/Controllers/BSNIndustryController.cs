using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;

namespace FBD.Controllers
{
    //TODO: check Rights
    //TODO: check industry name and id unique
    public class BSNIndustryController : Controller
    {
        //
        // GET: /BSNIndustry/
        
        public ActionResult Index()
        {
            var industries = BusinessIndustries.SelectIndustries();
            
            return View(industries);
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
                    BusinessIndustries.AddIndustry(industry);
                }
                else throw new Exception();
                TempData["Message"] = "Industry ID "+industry.IndustryID+ " have been added sucessfully";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
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
                
                if (ModelState.IsValid)
                {
                    BusinessIndustries.EditIndustry(industry);
                    
                }
                else throw new ArgumentException();
                TempData["Message"] = "IndustryID "+ industry.IndustryID + " have been updated sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Temporary error handle.
                
                TempData["Message"] = ex.Message;
                return View(industry);
            }
        }

        //
        // GET: /BSNIndustry/Delete/5
 
        public ActionResult Delete(string id)
        {
            BusinessIndustries.DeleteIndustry(id);
            TempData["Message"] = "Industry ID "+ id+" have been deleted sucessfully";
            return RedirectToAction("Index");
        }

        
    }
}
