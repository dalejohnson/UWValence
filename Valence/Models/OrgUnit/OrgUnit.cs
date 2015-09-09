using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UWD2L.Valence.Models.OrgUnit
{
    public class BasicOrgUnit
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }

    public class OrgUnitTypeInfo
    {
        ///Encapsulates the core information associated with an org unit type for use by other services (for example, the Enrollment and Course related actions).


    public int Id {get;set;}
    public string Code {get;set;}
    public string Name {get;set;}
}

}