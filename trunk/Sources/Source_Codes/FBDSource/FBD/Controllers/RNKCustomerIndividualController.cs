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
    public class RNKCustomerIndividualsController : Controller
    {
        //
        // GET: /RNKCustomerIndividuals/

        /// <summary>
        /// Display List of individual customers
        /// </summary>
        /// <returns>View of Index</returns>
        public ActionResult Index()
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            List<CustomersIndividuals> model = null;
            try
            {
                model = CustomersIndividuals.SelectIndividuals();


                if (model==null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_INDIVIDUAL);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string BranchID)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_VIEW, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            List<CustomersIndividuals> model = null;
            try
            {
                if (!string.IsNullOrEmpty(BranchID))
                {
                    ViewData["BranchID"] = BranchID;
                    model = CustomersIndividuals.SelectIndividualByBranchID(BranchID);
                }
                else model = CustomersIndividuals.SelectIndividuals();

                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_INDIVIDUAL);
            }
            return View(model);
        }
        //
        // GET: /RNKCustomerIndividuals/Add
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
            var model = new RNKCustomerIndividualsViewModel();
            model.SystemBranches = SystemBranches.SelectBranches();
            return View(model);
        } 

        //
        // POST: /RNKCustomerIndividuals/Add
        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="data">Info of new customer</param>
        /// <returns>Index if Add success/Add with error otherwise</returns>
        [HttpPost]
        public ActionResult Add(RNKCustomerIndividualsViewModel data)
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
                    var individual = data.CustomerIndividual;
                    individual.SystemBranches = SystemBranches.SelectBranchByID(data.BranchID,entity);
                    CustomersIndividuals.AddIndividual(individual, entity);
                }
                else throw new Exception();
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.CUSTOMER_INDIVIDUAL);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_INDIVIDUAL);
                data.SystemBranches = SystemBranches.SelectBranches();
                return View(data);
            }
        }

        //
        // GET: /RNLCustomerIndividuals/Edit/5

        public ActionResult Edit(int id)
        {
            if (!AccessManager.AllowAccess(Constants.RIGHT_CUSTOMERS_UPDATE, Session[Constants.SESSION_USER_ID]))
            {
                return RedirectToAction("Unauthorized", "SYSAuths");
            }
            var model = new RNKCustomerIndividualsViewModel();
            try
            {
                model.SystemBranches = SystemBranches.SelectBranches();
                model.CustomerIndividual = CustomersIndividuals.SelectIndividualByID(id);
                if (model.CustomerIndividual != null)
                {
                    model.CustomerIndividual.SystemBranchesReference.Load();
                    if(model.CustomerIndividual.SystemBranches!=null)
                    model.BranchID = model.CustomerIndividual.SystemBranches.BranchID;
                }
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.CUSTOMER_INDIVIDUAL);
            }
            return View(model);

        }

        //
        // POST: /RNLCustomerIndividuals/Edit/5
        /// <summary>
        /// Display Edit View
        /// </summary>
        /// <param name="id">id of item to be edited</param>
        /// <returns>Index if edit sucess, Edit view with error other wise</returns>
        [HttpPost]
        public ActionResult Edit(int id, RNKCustomerIndividualsViewModel data)
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
                    var individual = CustomersIndividuals.SelectIndividualByID(id, entity);
                    individual.SystemBranches = SystemBranches.SelectBranchByID(data.BranchID, entity);
                    individual.CustomerName = data.CustomerIndividual.CustomerName;
                    individual.CIF = data.CustomerIndividual.CIF;
                    
                    //line.IndividualsIndustriesReference.EntityKey = new System.Data.EntityKey("FBDEntities.IndividualsIndustries", "IndustryID", data.IndustryID);
                    CustomersIndividuals.EditIndividual(individual);
                }
                else throw new Exception();
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST,
                                                                        Constants.CUSTOMER_INDIVIDUAL,
                                                                        id.ToString());
                return RedirectToAction("Index");
            }
            catch 
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_INDIVIDUAL);
                data.SystemBranches = SystemBranches.SelectBranches();
                return View(data);
            }
        }

        //
        // GET: /RNKCustomerIndividuals/Delete/5
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
                if (CustomersIndividuals.DeleteIndividual(id) != 1) throw new Exception();
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.CUSTOMER_INDIVIDUAL);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.CUSTOMER_INDIVIDUAL);
                return RedirectToAction("Index");
            }
        }
    }
}
