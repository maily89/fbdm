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
        /// <summary>
        /// Display List of business customers
        /// </summary>
        /// <returns>View of Index</returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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

        // GET: /RNKCustomerBusiness/
        /// <summary>
        /// Display List of business customers
        /// </summary>
        /// <returns>View of Index</returns>
        [HttpPost]
        public ActionResult Index(string BranchID)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            List<CustomersBusinesses> model = null;
            try
            {
                if (!string.IsNullOrEmpty(BranchID))
                {
                    ViewData["BranchID"] = BranchID;
                    model = CustomersBusinesses.SelectBusinessByBranchID(BranchID);
                }
                else model = CustomersBusinesses.SelectBusinesses();

                if (model == null)
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
        /// <summary>
        /// Display Add new customer view
        /// </summary>
        /// <returns>Add view</returns>
        public ActionResult Add()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            var model = new RNKCustomerBusinessViewModel();
            model.SystemBranches = SystemBranches.SelectBranches();
            return View(model);
        } 

        //
        // POST: /RNKCustomerBusiness/Add
        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="data">Info of new customer</param>
        /// <returns>Index if Add success/Add with error otherwise</returns>
        [HttpPost]
        public ActionResult Add(RNKCustomerBusinessViewModel data)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
        /// <summary>
        /// Display Edit View
        /// </summary>
        /// <param name="id">id of item to be edited</param>
        /// <returns>Index if edit sucess, Edit view with error other wise</returns>
        public ActionResult Edit(int id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
        // POST: /RNKCustomerBusiness/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, RNKCustomerBusinessViewModel data)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
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
        // GET: /RNKCustomerBusiness/Delete/5
        /// <summary>
        /// delete item
        /// </summary>
        /// <param name="id">item to be delete</param>
        /// <returns>redirect ot index</returns>
        public ActionResult Delete(int id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            try
            {
                //if unable to delete, throw exception
                if (CustomersBusinesses.DeleteBusiness(id) != 1) throw new Exception();

                //otherwise, return index with success message
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.CUSTOMER_BUSINESS);
                return RedirectToAction("Index");
            }
            catch
            {
                //return index with unsuccess message
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.CUSTOMER_BUSINESS);
                return RedirectToAction("Index");
            }
        }
    }
}
