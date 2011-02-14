using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;
namespace FBD.Controllers
{
    public class BSNLineController : Controller
    {
        //
        // GET: /BSNLine/

        public ActionResult Index()
        {
            var industry = BusinessIndustries.SelectIndustries();
            var model = new BSNLineIndexViewModel();
            model.Industries = industry;
            var temp = BusinessLines.SelectLines();

            //int temp = 0;
            //if (industryID!= null && industryID > 0 && industryID < industry.Count) 
            //{
            //    temp = industryID.Value;
                
            //}
            //if (industry.Count > 0)
            //{
            //    model.IndustryID = temp;
            //    model.IndustryName = industry[temp].IndustryName;
            //    model.Lines = industry[temp].BusinessLines.ToList();
            //}
            model.Lines = temp;
            return View(model);
        }

        // Post: /BSNLine/
        [HttpPost]
        public ActionResult Index(string IndustryID)
        {
            var industries = BusinessIndustries.SelectIndustries();
            var model = new BSNLineIndexViewModel();
            model.Industries = industries;
            int temp = 0;

            if (IndustryID != null)
            {
                var industry = BusinessIndustries.SelectIndustryByID(IndustryID);
                model.IndustryName = industry.IndustryName;
                model.IndustryID = IndustryID;
                industry.BusinessLines.Load();
                model.Lines = industry.BusinessLines.ToList();

            }
            else model.Lines = BusinessLines.SelectLines();
            
            return View(model);
        }

        //
        // GET: /BSNLine/Create

        public ActionResult Add()
        {
            var model = new BSNLineViewModel();
            model.BusinessIndustries = BusinessIndustries.SelectIndustries();
            return View(model);
        } 

        //
        // POST: /BSNLine/Create

        [HttpPost]
        public ActionResult Add(BSNLineViewModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new FBDEntities();
                    var line = data.BusinessLines;
                    line.BusinessIndustries = BusinessIndustries.SelectIndustryByID(data.IndustryID,entity);
                    //line.BusinessIndustriesReference.EntityKey = new System.Data.EntityKey("FBDEntities.BusinessIndustries", "IndustryID", data.IndustryID);
                    BusinessLines.AddLine(line,entity);
                }
                else throw new Exception();
                TempData["Message"] = "New Line has been added successfully";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                data.BusinessIndustries = BusinessIndustries.SelectIndustries();
                return View(data);
            }
        }
        
        //
        // GET: /BSNLine/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = new BSNLineViewModel();
            model.BusinessIndustries = BusinessIndustries.SelectIndustries();
            model.BusinessLines = BusinessLines.SelectLineByID(id);
            model.BusinessLines.BusinessIndustriesReference.Load();
            model.IndustryID = model.BusinessLines.BusinessIndustries.IndustryID;
            return View(model);
            
        }

        //
        // POST: /BSNLine/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, BSNLineViewModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new FBDEntities();
                    var line = BusinessLines.SelectLineByID(id, entity);
                    line.BusinessIndustries = BusinessIndustries.SelectIndustryByID(data.IndustryID, entity);
                    //line.BusinessIndustriesReference.EntityKey = new System.Data.EntityKey("FBDEntities.BusinessIndustries", "IndustryID", data.IndustryID);
                    BusinessLines.EditLine(line);
                }
                else throw new Exception();
                TempData["Message"] = "Line has been edited successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                data.BusinessIndustries = BusinessIndustries.SelectIndustries();
                return View(data);
            }
        }

        //
        // GET: /BSNLine/Delete/5
 
        public ActionResult Delete(int id)
        {
            BusinessLines.DeleteLine(id);
            TempData["Message"] = "Line ID " + id + " have been deleted sucessfully";
            return RedirectToAction("Index");
        }

        //
        // POST: /BSNLine/Delete/5

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
