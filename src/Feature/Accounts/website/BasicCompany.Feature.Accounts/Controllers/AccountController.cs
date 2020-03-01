using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BasicCompany.Feature.Accounts.Models;
using BasicCompany.Feature.Accounts.Repositories;

namespace BasicCompany.Feature.Accounts.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository AccountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            AccountRepository = accountRepository;
        }

        
        public ActionResult Login()
        {
            return View("~/Views/Accounts/Login.cshtml", new LoginInfo());
            //return this.Login(loginInfo, redirectUrl => new RedirectResult(redirectUrl));
        }

        [HttpPost]
        public ActionResult Login(LoginInfo loginInfo)
        {
            return this.Login(loginInfo, redirectUrl => new RedirectResult(redirectUrl));
        }

        private ActionResult Login(LoginInfo loginInfo, Func<string, ActionResult> redirectAction)
        {
            var user = this.AccountRepository.Login(loginInfo.UserName, loginInfo.Password);
            if (user == null)
            {
                loginInfo.InvalidPasword = true;
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }

            return Redirect("https://helix-basic-unicorn.dev.local/projects/");
        }
    }
}