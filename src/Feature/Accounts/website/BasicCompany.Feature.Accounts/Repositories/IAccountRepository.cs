using System;
using System.Collections.Generic;
using Sitecore.Security.Accounts;

namespace BasicCompany.Feature.Accounts.Repositories
{
    public interface IAccountRepository
    {
        string RestorePassword(string userName);
        void RegisterUser(string email, string password, string profileId);
        bool Exists(string userName);
        void Logout();
        User Login(string userName, string password);
    }
}
