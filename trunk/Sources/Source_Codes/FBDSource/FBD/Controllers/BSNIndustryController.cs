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
        /// <summary>
        /// Display list of industry
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var industries = BusinessIndustries.SelectIndustries();
            
            return View(industries);
        }

        //
        // GET: /BSNIndustry/Add
        /// <summary>
        /// Display Add view
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        } 

        //
        // POST: /BSNIndustry/Add
        /// <summary>
        /// Add industry and display result
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
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
         /// <summary>
         /// Display edit form
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        public ActionResult Edit(string id)
        {
            var model = BusinessIndustries.SelectIndustryByID(id);
            return View(model);
        }

        //
        // POST: /BSNIndustry/Edit/5
        /// <summary>
        /// Update edit form and display result.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="industry"></param>
        /// <returns></returns>
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
         /// <summary>
         /// Delete Industry
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        public ActionResult Delete(string id)
        {
            BusinessIndustries.DeleteIndustry(id);
            TempData["Message"] = "Industry ID "+ id+" have been deleted sucessfully";
            return RedirectToAction("Index");
        }

        
    }
}
