using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Security.Accounts;

namespace BasicCompany.Feature.Accounts.Pipelines
{
    public interface IPipelineService
    {
        bool RunLoggedIn(User user);
        bool RunLoggedOut(User user);
        bool RunRegistered(User user);
    }
}
