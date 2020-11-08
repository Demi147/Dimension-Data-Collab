using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackEnd.Models
{
    public class DataItem
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id;
        public int Age { get; set; }
        public bool Attrition { get; set; }

        [DisplayName("Business Travel")]
        public string BusinessTravel { get; set; }

        [DisplayName("Daily Rate")]
        [Range(0.0,Double.MaxValue)]
        public double DailyRate { get; set; }

        public string Department { get; set; }

        [DisplayName("Distance From Home")]
        [Range(0.0, Double.MaxValue)]
        public double DistanceFromHome { get; set; }
        public int Education { get; set; }

        [DisplayName("Education Field")]
        public string EducationField { get; set; }

        [DisplayName("Employee Count")]
        public int EmployeeCount { get; set; }

        [DisplayName("Employee Number")]
        [Range(0, Int32.MaxValue)]
        public int EmployeeNumber { get; set; }

        [DisplayName("Environment Satisfaction")]
        public int EnvironmentSatisfaction { get; set; }

        public string Gender { get; set; }

        [DisplayName("Hourly Rate")]
        [Range(0.0, Double.MaxValue)]
        public double HourlyRate { get; set; }

        [DisplayName("Job Involvement")]
        public int JobInvolvement { get; set; }

        [DisplayName("Job Level")]
        [Range(0, 5)]
        public int JobLevel { get; set; }

        [DisplayName("Job Role")]
        public string JobRole { get; set; }

        [DisplayName("Job Satisfaction")]
        [Range(0, 5)]
        public int JobSatisfaction { get; set; }

        [DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }

        [DisplayName("Monthly Income")]
        [Range(0.0, Double.MaxValue)]
        public double MonthlyIncome { get; set; }

        [DisplayName("Monthly Rate")]
        [Range(0.0, Double.MaxValue)]
        public double MonthlyRate { get; set; }

        [DisplayName("Number of Companies Worked")]
        public int NumCompaniesWorked { get; set; }

        [DisplayName("Over 18")]
        public bool Over18 { get; set; }

        [DisplayName("Over Time")]
        public bool OverTime { get; set; }

        [DisplayName("Percent Salar yHike")]
        [Range(0.0, 100.00)]
        public double PercentSalaryHike { get; set; }

        [DisplayName("Performance Rating")]
        [Range(0, 5)]
        public int PerformanceRating { get; set; }

        [DisplayName("Relationship Satisfaction")]
        [Range(0, 5)]
        public int RelationshipSatisfaction { get; set; }

        [DisplayName("Standard Hours")]
        [Range(0.0, Double.MaxValue)]
        public double StandardHours { get; set; }

        [DisplayName("Stock Option Level")]
        public int StockOptionLevel { get; set; }

        [DisplayName("Total Working Years")]
        [Range(0.0, Double.MaxValue)]
        public double TotalWorkingYears { get; set; }

        [DisplayName("Training Times LastYear")]
        [Range(0, Int32.MaxValue)]
        public int TrainingTimesLastYear { get; set; }

        [DisplayName("Work Life Balance")]
        [Range(0, 5)]
        public int WorkLifeBalance { get; set; }

        [DisplayName("Years At Company")]
        [Range(0, Int32.MaxValue)]
        public int YearsAtCompany { get; set; }

        [DisplayName("Years In Curren tRole")]
        [Range(0, Int32.MaxValue)]
        public int YearsInCurrentRole { get; set; }

        [DisplayName("Years Since Last Promotion")]
        [Range(0, Int32.MaxValue)]
        public int YearsSinceLastPromotion { get; set; }

        [DisplayName("Years With Curr Manager")]
        [Range(0, Int32.MaxValue)]
        public int YearsWithCurrManager { get; set; }
    }
}
