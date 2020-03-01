using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BasicCompany.Feature.Accounts.Pipelines;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;

namespace BasicCompany.Feature.Accounts.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IPipelineService pipelineService;

        public AccountRepository(IPipelineService pipelineService)
        {
            this.pipelineService = pipelineService;
        }

        public bool Exists(string userName)
        {
            var fullName = Context.Domain.GetFullName(userName);

            return User.Exists(fullName);
        }

        public User Login(string userName, string password)
        {
            var accountName = string.Empty;
            var domain = Context.Domain;
            if (domain != null)
            {
                accountName = domain.GetFullName(userName);
            }

            var result = AuthenticationManager.Login(accountName, password);
            if (!result)
            {
                return null;
            }

            var user = AuthenticationManager.GetActiveUser();
            return user;
        }

        public void Logout()
        {
            AuthenticationManager.Logout();
        }

        public string RestorePassword(string userName)
        {
            var domainName = Context.Domain.GetFullName(userName);
            var user = Membership.GetUser(domainName);
            if (user == null)
                throw new ArgumentException($"Could not reset password for user '{userName}'", nameof(userName));
            return user.ResetPassword();
        }

        public void RegisterUser(string email, string password, string profileId)
        {
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));
            Assert.ArgumentNotNullOrEmpty(password, nameof(password));

            var fullName = Context.Domain.GetFullName(email);
            try
            {

                Assert.IsNotNullOrEmpty(fullName, "Can't retrieve full userName");

                var user = User.Create(fullName, password);
                user.Profile.Email = email;
                if (!string.IsNullOrEmpty(profileId))
                {
                    user.Profile.ProfileItemId = profileId;
                }

                user.Profile.Save();
                this.pipelineService.RunRegistered(user);
            }
            catch
            {
                throw;
            }

            this.Login(email, password);
        }
    }
}