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
        /// <summary>
        /// Forward to action Login
        /// </summary>
        /// <returns>Login View</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Handle login post from client
        /// </summary>
        /// <param name="loginModel">the model containing userid and password</param>
        /// <returns>Login View</returns>
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

        /// <summary>
        /// User logout from system, then working session will be ended
        /// </summary>
        /// <returns>Login View</returns>
        public ActionResult Logout()
        {
            if (Session[Constants.SESSION_USER_ID] != null)
            {
                Session[Constants.SESSION_USER_ID] = null;
            }

            return RedirectToAction("Login");
        }

        /// <summary>
        /// User sends request to change their password
        /// </summary>
        /// <returns>Change Password View</returns>
        public ActionResult ChangePassword()
        {
            if (Session[Constants.SESSION_USER_ID] != null)
            {
                SYSChangePassModel model = new SYSChangePassModel();
                model.UserID = Session[Constants.SESSION_USER_ID].ToString();

                return View(model);
            }

            return RedirectToAction("Login");
        }

        /// <summary>
        /// User post password change information to Server
        /// </summary>
        /// <param name="model">the model containing change information</param>
        /// <returns>Change Password View</returns>
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

        /// <summary>
        /// User does not have enough power to user specific feature
        /// </summary>
        /// <returns>Unauthorized View</returns>
        public ActionResult Unauthorized()
        {
            if (Session[Constants.SESSION_USER_ID] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        /// <summary>
        /// User login successfully
        /// </summary>
        /// <returns>Login Success View</returns>
        public ActionResult LoginSuccess()
        {
            try
            {
                if (Session[Constants.SESSION_USER_ID] != null)
                {
                    var model = SystemUsers.SelectUserByID(Session[Constants.SESSION_USER_ID].ToString());

                    return View(model);
                }

                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
        }
    }
}
