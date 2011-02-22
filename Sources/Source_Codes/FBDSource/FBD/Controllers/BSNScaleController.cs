using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
namespace FBD.Controllers
{
    public class BSNScaleController : Controller
    {
        //
        // GET: /BSNScale/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<BusinessScales> scales = null;
            try
            {
                scales = BusinessScales.SelectScales();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return View(scales);
        }


        //
        // GET: /BSNScale/Add
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /BSNScale/Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleScore"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(BusinessScales scale)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessScales.AddScale(scale);
                }
                else throw new Exception();
                TempData["Message"] = "Scale ID " + scale.ScaleID + " have been added sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(scale);
            }
        }

        //
        // GET: /BSNScale/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            var model = BusinessScales.SelectScaleByID(id);
            return View(model);
        }

        //
        // POST: /BSNScale/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scaleScore"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, BusinessScales scale)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    BusinessScales.EditScale(scale);

                }
                else throw new ArgumentException();
                TempData["Message"] = "ScaleID " + scale.ScaleID + " have been updated sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = ex.Message;
                return View(scale);
            }
        }

        //
        // GET: /BSNScale/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            BusinessScales.DeleteScale(id);
            TempData["Message"] = "Scale ID " + id + " have been deleted sucessfully";
            return RedirectToAction("Index");
        }
    }
}
