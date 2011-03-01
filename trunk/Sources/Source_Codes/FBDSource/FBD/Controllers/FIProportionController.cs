using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.Models;
using FBD.ViewModels;

namespace FBD.Controllers
{
    public class FIProportionController : Controller
    {
        //
        // GET: /FIProportion/

        public ActionResult Index()
        {
            FIProportionViewModel viewModel = new FIProportionViewModel();
            try
            {
                List<BusinessIndustries> lstIndustries = new List<BusinessIndustries>();

                lstIndustries = BusinessIndustries.SelectIndustries();

                if (lstIndustries.Count() < 1)
                    return View(viewModel);

                viewModel = BusinessFinancialIndexProportion.CreateViewModelByIndustry(lstIndustries[0].IndustryID);
            }
            catch (Exception)
            {
                TempData["Message"] = CommonUtilities.Constants.ERR_DISPLAY_FIPROPORTION;
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            try
            {
                if (formCollection["Industry"] != null)
                {
                    FIProportionViewModel viewModelForSelectingIndustries = BusinessFinancialIndexProportion
                                                            .CreateViewModelByIndustry(formCollection["Industry"].ToString());

                    return View(viewModelForSelectingIndustries);
                }
            }
            catch (Exception)
            {
                TempData["Message"] = CommonUtilities.Constants.ERR_DISPLAY_FIPROPORTION;
                return RedirectToAction("Index");
            }

            try
            {
                if (formCollection["Save"] != null)
                {
                    FIProportionViewModel viewModelForSavingProportion = new FIProportionViewModel();

                    for (int i = 0; i < int.Parse(formCollection["NumberOfProportionRows"].ToString()); i++)
                    {
                        FIProportionRowViewModel rowForSavingProportion = new FIProportionRowViewModel();

                        if (formCollection["ProportionRows[" + i + "].Checked"] != null)
                        {
                            if (formCollection["ProportionRows[" + i + "].Checked"].ToString().Equals("true,false")
                                || formCollection["ProportionRows[" + i + "].Checked"].ToString().Equals("True,False")
                                    || formCollection["ProportionRows[" + i + "].Checked"].ToString().Equals("TRUE,FALSE"))
                            {
                                rowForSavingProportion.Checked = true;
                            }
                        }

                        rowForSavingProportion.IndexID = formCollection["ProportionRows[" + i + "].IndexID"].ToString();

                        rowForSavingProportion.IndexName = formCollection["ProportionRows[" + i + "].IndexName"].ToString();

                        if (formCollection["ProportionRows[" + i + "].Proportion"] != null)
                        {
                            try
                            {
                                rowForSavingProportion.Proportion = decimal.
                                                        Parse(formCollection["ProportionRows[" + i + "].Proportion"].ToString());

                            }
                            catch (Exception)
                            {
                                rowForSavingProportion.Proportion = 0;
                            }
                        }

                        rowForSavingProportion.ProportionID = int.Parse(
                                                        formCollection["ProportionRows[" + i + "].ProportionID"].ToString());

                        viewModelForSavingProportion.ProportionRows.Add(rowForSavingProportion);
                    }

                    viewModelForSavingProportion.IndustryID = formCollection["IndustryID"].ToString();

                    string errorIndex = BusinessFinancialIndexProportion
                                                    .EditFinancialIndexProportion(viewModelForSavingProportion);

                    if (errorIndex != null)
                    {
                        TempData["Message"] = string.Format(CommonUtilities.Constants.ERR_UPDATE_FIPROPORTION, errorIndex);
                        FIProportionViewModel viewModelAfterError = BusinessFinancialIndexProportion
                                                            .CreateViewModelByIndustry(formCollection["IndustryID"].ToString());
                        return View(viewModelAfterError);
                    }
                    else
                    {
                        TempData["Message"] = CommonUtilities.Constants.SCC_UPDATE_FIPROPORTION;
                        FIProportionViewModel viewModelAfterSuccess = BusinessFinancialIndexProportion
                                                            .CreateViewModelByIndustry(formCollection["IndustryID"].ToString());
                        return View(viewModelAfterSuccess);
                    }
                }
            }
            catch (Exception)
            {
                TempData["Message"] = CommonUtilities.Constants.ERR_POST_FIPROPORTION;
                return RedirectToAction("Index");
            }

            return View(new FIProportionViewModel());
        }
    }
}
