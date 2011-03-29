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

        public ActionResult Index()
        {
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
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_BUSINESS);
            }
            return View(model);
        }

        //
        // GET: /RNKCustomerIndividuals/Create

        public ActionResult Add()
        {
            var model = new RNKCustomerIndividualsViewModel();
            model.SystemBranches = SystemBranches.SelectBranches();
            return View(model);
        } 

        //
        // POST: /RNKCustomerIndividuals/Create

        [HttpPost]
        public ActionResult Add(RNKCustomerIndividualsViewModel data)
        {
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
        // GET: /RNLCustomerIndividuals/Edit/5

        public ActionResult Edit(int id)
        {
            var model = new RNKCustomerIndividualsViewModel();
            try
            {
                model.SystemBranches = SystemBranches.SelectBranches();
                model.CustomerIndividual = CustomersIndividuals.SelectIndividualByID(id);
                model.CustomerIndividual.SystemBranchesReference.Load();
                model.BranchID = model.CustomerIndividual.SystemBranches.BranchID;
            }
            catch
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.CUSTOMER_BUSINESS);
            }
            return View(model);

        }

        //
        // POST: /RNLCustomerIndividuals/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, RNKCustomerIndividualsViewModel data)
        {
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
        // GET: /RNLCustomerIndividuals/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                if (CustomersIndividuals.DeleteIndividual(id) != 1) throw new Exception();
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
