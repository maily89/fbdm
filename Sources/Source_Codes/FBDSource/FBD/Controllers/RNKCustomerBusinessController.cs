using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;
using FBD.CommonUtilities;
namespace FBD.Controllers
{
    public class RNKCustomerBusinessController : Controller
    {
        //
        // GET: /RNKCustomerBusiness/

        public ActionResult Index()
        {
            List<CustomersBusinesses> model = null;
            try
            {
                model = CustomersBusinesses.SelectBusinesses();


                if (model==null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_BUSINESS);
            }
            return View(model);
        }

        //
        // GET: /RNKCustomerBusiness/Create

        public ActionResult Add()
        {
            var model = new RNKCustomerBusinessViewModel();
            model.SystemBranches = SystemBranches.SelectBranches();
            return View(model);
        } 

        //
        // POST: /RNKCustomerBusiness/Create

        [HttpPost]
        public ActionResult Add(RNKCustomerBusinessViewModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new FBDEntities();
                    var business = data.CustomerBusiness;
                    business.SystemBranches = SystemBranches.SelectBranchByID(data.BranchID,entity);
                    CustomersBusinesses.AddBusiness(business, entity);
                }
                else throw new Exception();
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.CUSTOMER_BUSINESS);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_BUSINESS);
                data.SystemBranches = SystemBranches.SelectBranches();
                return View(data);
            }
        }

        //
        // GET: /RNLCustomerBusiness/Edit/5

        public ActionResult Edit(int id)
        {
            var model = new RNKCustomerBusinessViewModel();
            try
            {
                model.SystemBranches = SystemBranches.SelectBranches();
                model.CustomerBusiness = CustomersBusinesses.SelectBusinessByID(id);
                model.CustomerBusiness.SystemBranchesReference.Load();
                model.BranchID = model.CustomerBusiness.SystemBranches.BranchID;
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.CUSTOMER_BUSINESS);
            }
            return View(model);

        }

        //
        // POST: /RNLCustomerBusiness/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, RNKCustomerBusinessViewModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new FBDEntities();
                    var business = CustomersBusinesses.SelectBusinessByID(id, entity);
                    business.SystemBranches = SystemBranches.SelectBranchByID(data.BranchID, entity);
                    business.CustomerName = data.CustomerBusiness.CustomerName;
                    business.CIF = data.CustomerBusiness.CIF;
                    
                    //line.BusinessIndustriesReference.EntityKey = new System.Data.EntityKey("FBDEntities.BusinessIndustries", "IndustryID", data.IndustryID);
                    CustomersBusinesses.EditBusiness(business);
                }
                else throw new Exception();
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST,
                                                                        Constants.CUSTOMER_BUSINESS,
                                                                        id.ToString());
                return RedirectToAction("Index");
            }
            catch 
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_BUSINESS);
                data.SystemBranches = SystemBranches.SelectBranches();
                return View(data);
            }
        }

        //
        // GET: /RNLCustomerBusiness/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                if (CustomersBusinesses.DeleteBusiness(id) != 1) throw new Exception();
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.CUSTOMER_BUSINESS);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.CUSTOMER_BUSINESS);
                return RedirectToAction("Index");
            }
        }
    }
}
