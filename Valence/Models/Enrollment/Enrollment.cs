using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UWD2L.Valence;

namespace UWD2L.Valence.Models.Enrollment
{
    public class Enrollment
    {
    }

    public class ClasslistUser
    {
        ///Structure for the enrolled user’s information that the service exposes through the classlist API.

        public string Identifier { get; set; }
        public string ProfileIdentifier { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string OrgDefinedId { get; set; }
        public string Email { get; set; }
    }


    public class CreateEnrollmentData
    {
        public int OrgUnitId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public CreateEnrollmentData() { }
        public CreateEnrollmentData(int orgUnitID, int userID, int roleID)
          {
              OrgUnitId = orgUnitID;
              UserId = userID;
              RoleId = roleID;
        
        }
    }

    public class EnrollmentData
    {
        int OrgUnitId { get; set; }
        int UserId { get; set; }
        int RoleId { get; set; }
        bool IsCascading { get; set; }
    }

    public class OrgUnitInfo
    {
        public int Id { get; set; }
        public OrgUnit.OrgUnitTypeInfo Type { get; set; }
        public string Code { get; set; }
    }

    public class Access
    {
        public bool IsActive { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool CanAccess { get; set; }
    }

    public class MyOrgUnitInfo
    {
        public OrgUnitInfo OrgUnit { get; set; }
        public Access Access { get; set; }

    }


    public class RoleInfo
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class OrgUnitUser
    {
        public UWD2L.Valence.Models.User.User User { get; set; }
        public RoleInfo Role { get; set; }
    }

  

    public class UserOrgUnit
    { 
        public OrgUnitInfo OrgUnit {get;set;}
       
        public RoleInfo Role {get;set;}


    
    }
    
}
