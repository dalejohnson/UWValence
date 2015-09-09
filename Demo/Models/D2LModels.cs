using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Demo.Models
{
    public class D2LModels
    {
        
    }
    public class D2LEnroll
    { 
        public int OrgUnitID {get;set;}
        public string UserName {get;set;}
        public string Role {get;set;}

        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string ErrorMessge { get; set; }
    }

    public class D2LFinalGrade
    { 
        public int OrgUnitID {get;set;}
        public string UserName { get; set; }
    }
}