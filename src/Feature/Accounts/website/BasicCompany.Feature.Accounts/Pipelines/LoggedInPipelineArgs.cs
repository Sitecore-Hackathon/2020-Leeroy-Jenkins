using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicCompany.Feature.Accounts.Pipelines
{
    public class LoggedInPipelineArgs : AccountsPipelineArgs
    {
        public string Source { get; set; }

        public Guid? PreviousContactId {
            get {
                return (Guid)this.CustomData["PreviousContactId"];
            }
            set {
                this.CustomData["PreviousContactId"] = value;
            }
        }
    }
}