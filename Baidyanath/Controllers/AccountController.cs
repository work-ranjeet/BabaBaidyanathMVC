using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Baidyanath.Filters;
using Baidyanath.Models.Account;
using Baidyanath.Models.DataAccess;


namespace Baidyanath.Controllers
{
    [LanguageSwitcher]
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Login()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginModel User)
        {
            if (ModelState.IsValid)
            {
                if (await User.IsValid(User.UserName, User.Password))
                {
                    FormsAuthentication.SetAuthCookie(User.UserName, User.RememberMe);
                    ModelState.Clear();
                    User.UserName = string.Empty;
                    User.Password = string.Empty;
                    return RedirectToAction("UserDetails");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Incorrect Credentials.");
                }

            }
            return View(User);
        }

        public async Task<ActionResult> Logout()
        {
            FormsAuthentication.SignOut();
            return await Task.Run(() =>
            {
                return RedirectToAction("Index", "Home");
            });

        }

        public async Task<JsonResult> IsUserAvailable(UserRegistrationModel User)
        {
            DBQuery dbQuery = new DBQuery();
            try
            {
                return await dbQuery.IsAvailable(User.UserID) ? Json(false, JsonRequestBehavior.AllowGet) : Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                dbQuery.Dispose();
            }
        }

        [HttpGet]
        public async Task<ActionResult> UserRegisteration()
        {
            return await Task.Run(() =>
            {
                return View();
            });
        }

        [HttpPost]
        public async Task<ActionResult> UserRegisteration(UserRegistrationModel User)
        {
            if (ModelState.IsValid)
            {
                DBQuery dbQuery = new DBQuery();
                try
                {
                    await dbQuery.RegisterUser(User);
                    FormsAuthentication.SetAuthCookie(User.FirstName + "_" + User.UserID, false);
                    return RedirectToAction("UserDetails");
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    dbQuery.Dispose();
                }
            }
            return PartialView(User);
        }

        [HttpGet]
        public async Task<ActionResult> UserDetails()
        {
            return await Task.Run(() =>
            {
                return View(new UserProfile().GetUserProfile(User.Identity.Name.Split('_')[1]));
            });
        }

        [HttpGet]
        public ActionResult UserBasicProfileEditPartial()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UserBasicProfileEditPartial(UserProfileBasicModel BasicModel)
        {

            if (ModelState.IsValid)
            {
                DBQuery dbQuery = new DBQuery();
                try
                {
                    await dbQuery.UpdatetUserProfileBasicData(BasicModel);
                   // return RedirectToAction("UserDetails");
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    dbQuery.Dispose();
                }
            }
            return PartialView(BasicModel);
        }


        [HttpGet]
        public ActionResult UserLocationEditPartial()
        {
            return View();
        }
        [HttpPost]

        public async Task<ActionResult> UserLocationEditPartial(UserProfileLocationModel locationModel)
        {

            if (ModelState.IsValid)
            {
                DBQuery dbQuery = new DBQuery();
                try
                {
                    await dbQuery.UpdatetUserProfileLocationData(locationModel);
                    // return RedirectToAction("UserDetails");
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    dbQuery.Dispose();
                }
            }
            return PartialView(locationModel);
        }

    }
}
