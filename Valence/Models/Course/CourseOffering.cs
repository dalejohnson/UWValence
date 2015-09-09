using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UWD2L.Valence.Models.Course
{
    public class CourseOffering
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string IsActive { get; set; }
        public string Path { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public BasicOrgUnit CourseTemplate { get; set; }
        public BasicOrgUnit Semester { get; set; }
        public BasicOrgUnit Department { get; set; }
    }

    public class CourseOfferingInfo
    { 
        
        public string Name {get;set;}
        public string Code {get;set;}
        public string StartDate {get;set;}
        public string EndDate {get;set;}
        public string  IsActive {get;set;}

        public CourseOfferingInfo(){}
        public CourseOfferingInfo(CourseOffering offering)
        {
            Name = offering.Name;
            Code = offering.Code;
            StartDate = offering.StartDate;
            EndDate = offering.EndDate;
            IsActive = offering.IsActive;

        }

    
    
    }
}