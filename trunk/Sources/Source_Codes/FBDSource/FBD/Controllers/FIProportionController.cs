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
            List<BusinessIndustries> lstIndustries = new List<BusinessIndustries>();

            lstIndustries = BusinessIndustries.SelectIndustries();

            if (lstIndustries.Count() < 1)
                return View(viewModel);

            viewModel = BusinessFinancialIndexProportion.CreateViewModelByIndustry(lstIndustries[0].IndustryID);

            return View(viewModel);
        }

        //[HttpPost]
        //public ActionResult Index(string IndustryID)
        //{
        //    FIProportionViewModel viewModel = BusinessFinancialIndexProportion.CreateViewModelByIndustry(IndustryID);

        //    return View(viewModel);
        //}

        [HttpPost]
        public ActionResult Index(FIProportionViewModel viewModel)
        {

            return View(viewModel);
        }
    }
}
