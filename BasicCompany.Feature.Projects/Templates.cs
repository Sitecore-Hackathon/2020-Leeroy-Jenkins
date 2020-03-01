using Sitecore.Data;

namespace BasicCompany.Feature.Projects
{
  public static class Templates
  {
        public static class Project
        {
            public static readonly ID Id = new ID("{1E483FB7-94D7-4796-892D-BAFF9AE67DE3}");

            public static class Fields
            {
                public static readonly ID ModuleTitle = new ID("{41DD4B40-C329-4E48-AF10-3175AD8A0DF5}");
                public static readonly ID Price = new ID("{47D11429-1DB7-45A1-B7AB-DE9E082781E5}");
                public static readonly ID Votes = new ID("{05A94C2C-63DD-4147-A341-2E80850567EE}");
                public static readonly ID ModuleDescription = new ID("{91F2E072-41C0-4ABA-9CB5-7DF29460EDD4}");
                public static readonly ID WebServerOS = new ID("{3D00FFA2-19C3-4FDB-AA4A-C12C53C3B068}");
                public static readonly ID SitecoreProduct = new ID("{ADF180BF-82BD-4076-8351-3277B960374A}");
                public static readonly ID Owner = new ID("{045AEA92-527D-4466-AE44-F77CD76919FD}");
                public static readonly ID Participants = new ID("{F9CB090C-3A2C-4487-89AA-4CFAF5D8ADCB}");
            }
        }
  }
}