using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UWD2L.Valence.Models.User
{
    public class User
    {
            public string Identifier {get;set;}
            public string DisplayName {get;set;}
            public string EmailAddress {get;set;}
            public string OrgDefinedId {get;set;}
            public string ProfileBadgeUrl {get;set;}
            public string ProfileIdentifier { get; set; }

    }

    public class CreateUserData
    {///When you use an action to create a user, you pass in a block of new-user data like this:


        public string OrgDefinedId {get;set;}
        public string FirstName {get;set;}
        public string MiddleName {get;set;}
        public string LastName {get;set;}
        public string ExternalEmail {get;set;}
        public string UserName {get;set;}
        public int  RoleId {get;set;}
        public bool IsActive {get;set;}
        public bool SendCreationEmail {get;set;}

    }

    public class UserData
    {
        ///When you use an action with the User Management service to retrieve a user’s data, the service passes you back a data block like this (notice that it’s different to the User.WhoAmIUser information block provided by the WhoAmI service actions):

        public int OrgId {get;set;}
        public int UserId {get;set;}
        public string FirstName {get;set;}
        public string MiddleName{get;set;} 
        public string LastName {get;set;}
        public string UserName {get;set;}
        public string ExternalEmail {get;set;}
        public string OrgDefinedId {get;set;}
        public string UniqueIdentifier {get;set;}
        public UserActivationData Activation { get; set; }
}

    public class UserActivationData
{
        public bool IsActive { get; set; }
}
}

