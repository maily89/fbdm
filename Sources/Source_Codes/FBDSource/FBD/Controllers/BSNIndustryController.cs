using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;
using FBD.CommonUtilities;
using Telerik.Web.Mvc;

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
            List<BusinessIndustries> industries=null;
            try
            {
                industries= BusinessIndustries.SelectIndustries();

                if (industries == null)
                {
                    throw new Exception();
                }
            }
            catch(Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.BUSINESS_INDUSTRY);
            }
            
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
                    if (BusinessIndustries.IsIDExist(industry.IndustryID))
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_KEY_EXIST;
                        return View(industry);
                    }
                    int result=BusinessIndustries.AddIndustry(industry);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.BUSINESS_INDUSTRY);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();
                
            }
            catch(Exception )
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.BUSINESS_INDUSTRY);
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
            BusinessIndustries model = null;
            try
            {
                model = BusinessIndustries.SelectIndustryByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.BUSINESS_INDUSTRY);
            }
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
                    int result=BusinessIndustries.EditIndustry(industry);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.BUSINESS_INDUSTRY, industry.IndustryID);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.BUSINESS_INDUSTRY);
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
            try
            {
                int result=BusinessIndustries.DeleteIndustry(id);

                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.BUSINESS_INDUSTRY);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.BUSINESS_INDUSTRY);
                return RedirectToAction("Index");
            }
            
        }

        
    }
}
