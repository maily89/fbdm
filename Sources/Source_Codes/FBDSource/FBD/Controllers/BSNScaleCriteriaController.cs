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
    //TODO: check scaleCriteria name and id unique
    public class BSNScaleCriteriaController : Controller
    {
        //
        // GET: /BSNScaleCriteria/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var scaleCriteria = BusinessScaleCriteria.SelectScaleCriteria();

            return View(scaleCriteria);
        }

        //
        // GET: /BSNScaleCriteria/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {

            return View();
        }

        //
        // GET: /BSNScaleCriteria/Add
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /BSNScaleCriteria/Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleCriteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(BusinessScaleCriteria scaleCriteria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessScaleCriteria.AddScaleCriteria(scaleCriteria);
                }
                else throw new Exception();
                TempData["Message"] = "ScaleCriteria ID " + scaleCriteria.CriteriaID + " have been added sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(scaleCriteria);
            }
        }

        //
        // GET: /BSNScaleCriteria/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            var model = BusinessScaleCriteria.SelectScaleCriteriaByID(id);
            return View(model);
        }

        //
        // POST: /BSNScaleCriteria/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scaleCriteria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, BusinessScaleCriteria scaleCriteria)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    BusinessScaleCriteria.EditScaleCriteria(scaleCriteria);

                }
                else throw new ArgumentException();
                TempData["Message"] = "ScaleCriteriaID " + scaleCriteria.CriteriaID + " have been updated sucessfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Temporary error handle.

                TempData["Message"] = ex.Message;
                return View(scaleCriteria);
            }
        }

        //
        // GET: /BSNScaleCriteria/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            BusinessScaleCriteria.DeleteScaleCriteria(id);
            TempData["Message"] = "ScaleCriteria ID " + id + " have been deleted sucessfully";
            return RedirectToAction("Index");
        }


    }
}
