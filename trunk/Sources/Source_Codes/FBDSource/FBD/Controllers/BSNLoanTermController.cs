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
    //TODO: check Rights
    //TODO: check loanTerm name and id unique
    public class BSNLoanTermController : Controller
    {
        //
        // GET: /BSNLoanTerm/
        /// <summary>
        /// Display list of loanTerm
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<CustomersLoanTerm> loanTerms = null;
            try
            {
                loanTerms = CustomersLoanTerm.SelectLoanTerms();

                if (loanTerms == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.SCC_MESSAGE] = string.Format(Constants.ERR_INDEX, Constants.CUSTOMER_LOANTERM);
            }

            return View(loanTerms);
        }

        //
        // GET: /BSNLoanTerm/Add
        /// <summary>
        /// Display Add view
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /BSNLoanTerm/Add
        /// <summary>
        /// Add loanTerm and display result
        /// </summary>
        /// <param name="loanTerm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(CustomersLoanTerm loanTerm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (CustomersLoanTerm.IsIDExist(loanTerm.LoanTermID))
                    {
                        TempData[Constants.ERR_MESSAGE] = Constants.ERR_KEY_EXIST;
                        return View(loanTerm);
                    }
                    int result = CustomersLoanTerm.AddLoanTerm(loanTerm);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_ADD, Constants.CUSTOMER_LOANTERM);
                        return RedirectToAction("Index");
                    }
                }
                throw new Exception();

            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_ADD_POST, Constants.CUSTOMER_LOANTERM);
                return View(loanTerm);
            }
        }

        //
        // GET: /BSNLoanTerm/Edit/5
        /// <summary>
        /// Display edit form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            CustomersLoanTerm model = null;
            try
            {
                model = CustomersLoanTerm.SelectLoanTermByID(id);
                if (model == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT, Constants.CUSTOMER_LOANTERM);
            }
            return View(model);
        }

        //
        // POST: /BSNLoanTerm/Edit/5
        /// <summary>
        /// Update edit form and display result.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loanTerm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string id, CustomersLoanTerm loanTerm)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    int result = CustomersLoanTerm.EditLoanTerm(loanTerm);

                    if (result == 1)
                    {
                        TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_EDIT_POST, Constants.CUSTOMER_LOANTERM, loanTerm.LoanTermID);
                        return RedirectToAction("Index");
                    }
                }

                throw new Exception();
            }
            catch (Exception)
            {
                //TODO: Temporary error handle.

                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_EDIT_POST, Constants.CUSTOMER_LOANTERM);
                return View(loanTerm);
            }
        }

        //
        // GET: /BSNLoanTerm/Delete/5
        /// <summary>
        /// Delete LoanTerm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            try
            {
                int result = CustomersLoanTerm.DeleteLoanTerm(id);

                if (result == 1)
                {
                    TempData[Constants.SCC_MESSAGE] = string.Format(Constants.SCC_DELETE, Constants.CUSTOMER_LOANTERM);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = string.Format(Constants.ERR_DELETE, Constants.CUSTOMER_LOANTERM);
                return RedirectToAction("Index");
            }

        }


    }
}
