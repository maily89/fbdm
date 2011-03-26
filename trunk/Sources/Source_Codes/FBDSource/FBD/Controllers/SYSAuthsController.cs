using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBD.CommonUtilities;
using FBD.Models;

namespace FBD.Controllers
{
    public class SYSAuthsController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SYSLoginModel loginModel)
        {
            try
            {
                string userID = loginModel.UserID;
                string password = loginModel.Password;

                if (!SYSLoginModel.VerifyLogin(userID, password))
                {
                    TempData[Constants.ERR_MESSAGE] = Constants.ERR_LOGIN_MATCH;
                    return View();
                }

                Session[Constants.SESSION_USER_ID] = userID;
                return RedirectToAction("LoginSuccess");
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_LOGIN_INPUT;
                return View();
            }
        }

        public ActionResult Logout()
        {
            if (Session[Constants.SESSION_USER_ID] != null)
            {
                Session[Constants.SESSION_USER_ID] = null;
                Session[Constants.SESSION_USER_GROUP_ID] = null;
            }

            return RedirectToAction("Login");
        }

        public ActionResult ChangePassword()
        {
            if (Session[Constants.SESSION_USER_ID] != null)
            {
                SYSChangePassModel model = new SYSChangePassModel();
                model.UserID = Session[Constants.SESSION_USER_ID].ToString();

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ChangePassword(SYSChangePassModel model)
        {
            try
            {
                if (!SYSChangePassModel.VerifyChangePass(model.UserID, model.OldPassword, model.NewPassword, model.ConfirmNewPassword))
                {
                    TempData[Constants.ERR_MESSAGE] = Constants.ERR_CHANGE_PASS_MATCH;
                    return View(model);
                }

                int result = SYSChangePassModel.ChangePass(model.UserID, model.NewPassword);
                if (result == 1)
                {
                    Session[Constants.SESSION_USER_ID] = null;
                    Session[Constants.SESSION_USER_GROUP_ID] = null;
                    TempData[Constants.SCC_MESSAGE] = Constants.SCC_CHANGE_PASS;
                    return RedirectToAction("Login");
                }

                throw new Exception();
            }
            catch (Exception)
            {
                TempData[Constants.ERR_MESSAGE] = Constants.ERR_CHANGE_PASS_INPUT;
                return View(model);
            }
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult LoginSuccess()
        {
            try
            {
                if (Session[Constants.SESSION_USER_ID] != null)
                {
                    var model = SystemUsers.SelectUserByID(Session[Constants.SESSION_USER_ID].ToString());

                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
