using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration ;

using D2L.Extensibility;
using D2L.Extensibility.AuthSdk;
using D2L.Extensibility.AuthSdk.Restsharp;
using RestSharp;

using UWD2L.Valence;

namespace UWD2L.Valence.Biz
{
     
    public class Valence
    {
        private readonly string m_appId = WebConfigurationManager.AppSettings["valence_appId"];
        private readonly string m_appKey = WebConfigurationManager.AppSettings["valence_appKey"];

        private readonly string m_userId = WebConfigurationManager.AppSettings["valence_userId"];
        private readonly string m_userKey = WebConfigurationManager.AppSettings["valence_userKey"];

        private readonly string LMS_URL = WebConfigurationManager.AppSettings["lms_host"];

        private const string LP_VERSION = "1.4";

        private const string USER_CREATE_ENROLLMENT = "/d2l/api/lp/" + LP_VERSION + "/enrollments/";
     
     // Must suppply Course nts/OU
        private const string COURSE_LOOKUP = "/d2l/api/lp/" + LP_VERSION + "/courses/{0}";
        
        private const string COURSE_UPDATE = "/d2l/api/lp/" + LP_VERSION + "/courses/{0}";
        // EXPECTS JSON PARM WITH CourseOfferingInfo (Course.CourseOfferingInfo)

        private const string USERLOOKUP_USERNAME_ROUTE = "/d2l/api/lp/" + LP_VERSION + "/users/?userName={0}";
        private const string USER_ENROLLMENTS = "/d2l/api/lp/1.4/enrollments/users/{0}/orgUnits/";

        private const string WHOAMI_ROUTE = "/d2l/api/lp/" + LP_VERSION + "/users/whoami";

        // {0} orgUnitID , //{1} UserID
        private const string USER_ENROLL_REMOVE = "/d2l/api/lp/1.4/enrollments/orgUnits/{0}/users/{1}";

        // {0} OrgUnitID {1} UserID
        private const string USER_GRADE_FINAL = "/d2l/api/le/1.4/{0}/grades/final/values/{1}";

        private readonly ID2LAppContext m_valenceAppContext;
        private readonly HostSpec m_valenceHost;
        private ID2LUserContext m_valenceUserContext;

        public Valence()
        {
            var appFactory = new D2LAppContextFactory();
            m_valenceAppContext = appFactory.Create(m_appId, m_appKey);
            m_valenceHost = new HostSpec("https", LMS_URL, 443);

            // Note Using existing authenicated Tokens
            m_valenceUserContext = m_valenceAppContext.CreateUserContext(m_userId, m_userKey, m_valenceHost);
        

        }

        public Models.Course.CourseOffering GetCourseOffering(string orgUnitID)
        {
            var client = new RestClient("https://" + LMS_URL);
            var authenticator = new ValenceAuthenticator(m_valenceUserContext);

            var request = new RestRequest(string.Format(COURSE_LOOKUP, orgUnitID));
            authenticator.Authenticate(client, request);
            var response = client.Execute<Models.Course.CourseOffering>(request);

            return response.Data;
        
        }

        public bool UpdateCourseActivation(string orgUnitId, bool Activation)
        {
            // Get Course Object
            Models.Course.CourseOffering course = GetCourseOffering(orgUnitId);

            Models.Course.CourseOfferingInfo model = new Models.Course.CourseOfferingInfo(course);

            model.IsActive = Activation.ToString();

            m_valenceUserContext = m_valenceAppContext.CreateUserContext(m_userId, m_userKey, m_valenceHost);
           
           

            var client = new RestClient("https://" + LMS_URL);
            var authenticator = new ValenceAuthenticator(m_valenceUserContext);


            var request = new RestRequest(string.Format(COURSE_UPDATE,orgUnitId), Method.PUT );

            RestSharp.Parameter p = new Parameter();
            p.Type = ParameterType.RequestBody;
            p.Name = "CourseOfferingInfo";
            p.Value = SimpleJson.SerializeObject(model);

            request.AddParameter(p);


            authenticator.Authenticate(client, request);
            var response = client.Execute(request);

            return true; 



        
        }

        public Models.User.UserData GetUserData(string userName)
        {
            var client = new RestClient("https://" + LMS_URL);
            var authenticator = new ValenceAuthenticator(m_valenceUserContext);

            var request = new RestRequest(string.Format(USERLOOKUP_USERNAME_ROUTE, userName));
            authenticator.Authenticate(client, request);
            var response = client.Execute<Models.User.UserData>(request);

            return response.Data;
        
        }

        public object GetUserEnrollments(string userName)
        {
            int userOrgID = GetUserData(userName).UserId;
            var client = new RestClient("https://" + LMS_URL);
            var authenticator = new ValenceAuthenticator(m_valenceUserContext);

            string bookmark = "";
            List<Models.Enrollment.UserOrgUnit > enrollments = new List<Models.Enrollment.UserOrgUnit> ();

            do
            {
                string reqStr = string.Format(USER_ENROLLMENTS, userOrgID.ToString());
                if (bookmark != "")
                    reqStr += "?bookmark=" + bookmark;

                var request = new RestRequest(reqStr);
                authenticator.Authenticate(client, request);


                var response = client.Execute<Dictionary<string, string>>(request);

        

                Models.API.PagingInfo pageInfo = SimpleJson.DeserializeObject<Models.API.PagingInfo>(response.Data["PagingInfo"]);
                List<Models.Enrollment.UserOrgUnit> items = SimpleJson.DeserializeObject<List<Models.Enrollment.UserOrgUnit>>(response.Data["Items"]);
                enrollments.AddRange(items);
                if (pageInfo.HasMoreItems)
                    bookmark = pageInfo.Bookmark;
                else bookmark = "";

            } while (bookmark != "");
            

            
            return enrollments ; 
        }

        public string GetFinalGrade(int OrgUnitID, int UserId)
        { 

            var client = new RestClient("https://" + LMS_URL);
            var authenticator = new ValenceAuthenticator(m_valenceUserContext);

            var request = new RestRequest(string.Format(USER_GRADE_FINAL , OrgUnitID .ToString (),UserId.ToString ()));
            authenticator.Authenticate(client, request);
            var response = client.Execute<Models.Grade .GradeValue>(request);

            return response.Data.DisplayedGrade;

        }



        public Models.Enrollment.EnrollmentData EnrollUser(int OrgUnitID, int UserID, int RoleId)
        {
            m_valenceUserContext = m_valenceAppContext.CreateUserContext(m_userId, m_userKey, m_valenceHost);
            Models.Enrollment.EnrollmentData rtnValue = new Models.Enrollment.EnrollmentData();

            Models.Enrollment.CreateEnrollmentData model =
                new Models.Enrollment.CreateEnrollmentData(OrgUnitID, UserID, RoleId );

            var client = new RestClient("https://" + LMS_URL);
            var authenticator = new ValenceAuthenticator(m_valenceUserContext);

            
            var request = new RestRequest( USER_CREATE_ENROLLMENT, Method.POST);
            
            RestSharp.Parameter p = new Parameter ();
            p.Type = ParameterType.RequestBody;
            p.Name = "CreateEnrollmentData";
            p.Value = SimpleJson.SerializeObject(model);

            request.AddParameter(p);
           

            authenticator.Authenticate(client, request);
            var response = client.Execute (request);

            return null;
        
        
        }

        public bool DeleteUser(int OrgUnitID, int UserID)
        {
            m_valenceUserContext = m_valenceAppContext.CreateUserContext(m_userId, m_userKey, m_valenceHost);
            Models.Enrollment.EnrollmentData rtnValue = new Models.Enrollment.EnrollmentData();

            var client = new RestClient("https://" + LMS_URL);
            var authenticator = new ValenceAuthenticator(m_valenceUserContext);

            string url = string.Format(USER_ENROLL_REMOVE, OrgUnitID.ToString(), UserID.ToString());
            var request = new RestRequest(url, Method.DELETE);

            authenticator.Authenticate(client, request);
            var response = client.Execute(request);

            return true;
        
        
        }
        

    }
}