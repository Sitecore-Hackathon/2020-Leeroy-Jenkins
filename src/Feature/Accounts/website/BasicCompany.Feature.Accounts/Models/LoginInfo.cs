using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCompany.Feature.Accounts.Models
{
    public class LoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool InvalidPasword { get; set; }
    }
}