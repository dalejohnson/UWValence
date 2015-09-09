using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UWD2L.Valence.Models.Grade
{
    public class GradeValue
    {
    public string  DisplayedGrade {get;set;}
    public string  GradeObjectIdentifier {get;set;}
    public string  GradeObjectName {get;set;}
    public double  GradeObjectType {get;set;}
    public string  GradeObjectTypeName {get;set;}
    public double  PointsNumerator {get;set;}
    public double  PointsDenominator {get;set;}
    public double  WeightedDenominator {get;set;}
    public double WeightedNumerator { get; set; }
    }
}
