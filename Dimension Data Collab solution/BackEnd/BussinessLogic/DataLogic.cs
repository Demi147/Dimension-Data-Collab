using BackEnd.DataAccess;
using BackEnd.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using System.Linq;

namespace BackEnd.BussinessLogic
{
    public class DataLogic : DataAccessClass<DataItem>
    {
        public DataLogic(string conString, string table, string database) :base(conString, table, database)
        {

        }

        //Custom data logic

        public async Task<(List<int>, List<int>)> GetAgeCount()
        {
            var x = new List<int>();
            var y = new List<int>();
            var temp = (x,y);
            //get distinct ages
            var xdata = await collection.DistinctAsync(Q=>Q.Age, new BsonDocument());
            x = xdata.ToList();
            foreach (var item in x)
            {

            }
            return temp;
        }

        public async Task<(long, long)> GetGenderCount()
        {
            var x = await collection.CountDocumentsAsync(new BsonDocument("Gender","Male"));
            var y = await collection.CountDocumentsAsync(new BsonDocument("Gender", "Female"));

            var temp = (x, y);
            return temp;
        }

        public async Task<(int[], double[], double[], double[], double[], double[])> GetComplexData() 
        {
            var rawData = await collection.Find(new BsonDocument()).ToListAsync();

            int[] bins = { 18, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70,75 };

            rawData = rawData.OrderBy(x => x.Age).ToList();
            var labels = bins;



            //jobinvolement
            double[] jobinvolement = new double[bins.Length];
            for (int i = 0; i < bins.Length-1; i++)
            {
                try
                {
                    jobinvolement[i] = rawData.Where(x => x.Age < bins[i]+2.5 && x.Age >= bins[i]-2.5).Select(x => x.JobInvolvement).Average();
                }
                catch (Exception)
                {

                }
                
            }

            //JobSatisfaction
            double[] JobSatisfaction = new double[bins.Length];
            for (int i = 0; i < bins.Length - 1; i++)
            {
                try
                {
                    JobSatisfaction[i] = rawData.Where(x => x.Age < bins[i]+2.5 && x.Age >= bins[i]-2.5).Select(x => x.JobSatisfaction).Average();
                }
                catch (Exception)
                {

                }

            }

            //education
            double[] education = new double[bins.Length];
            for (int i = 0; i < bins.Length - 1; i++)
            {
                try
                {
                    education[i] = rawData.Where(x => x.Age < bins[i]+2.5 && x.Age >= bins[i]-2.5).Select(x => x.Education).Average();
                }
                catch (Exception)
                {

                }

            }

            //EnvironmentSatisfaction
            double[] EnvironmentSatisfaction = new double[bins.Length];
            for (int i = 0; i < bins.Length - 1; i++)
            {
                try
                {
                    EnvironmentSatisfaction[i] = rawData.Where(x => x.Age < bins[i]+2.5 && x.Age >= bins[i]-2.5).Select(x => x.EnvironmentSatisfaction).Average();
                }
                catch (Exception)
                {

                }

            }

            //JobLevel
            double[] JobLevel = new double[bins.Length];
            for (int i = 0; i < bins.Length - 1; i++)
            {
                try
                {
                    JobLevel[i] = rawData.Where(x => x.Age < bins[i]+2.5 && x.Age >= bins[i]-2.5).Select(x => x.JobLevel).Average();
                }
                catch (Exception)
                {

                }

            }

            //xvalues,jobinvolement,JobSatisfaction,education,EnvironmentSatisfaction,JobLevel
            return (labels,jobinvolement,JobSatisfaction,education,EnvironmentSatisfaction,JobLevel);
        }
    }
}
