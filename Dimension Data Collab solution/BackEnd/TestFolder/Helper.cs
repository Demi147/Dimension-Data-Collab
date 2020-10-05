using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace BackEnd.TestFolder
{
    public static class Helper
    {
        //mongo crud test
        private static IMongoDatabase db;

        public static object TestMetod<T>()
        {
            var client = new MongoClient("mongodb+srv://mainUser:BtPCEIb8e3Dbb06n@323projectdatacluster.k94r8.azure.mongodb.net/MainData?retryWrites=true&w=majority");
            var db = client.GetDatabase("MainData");
            var col = db.GetCollection<T>("Test");
            return col;
        }

        public class TestModel{
            [BsonId]
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Random { get; set; }
        }

        public static void InsertTest()
        {
            var client = new MongoClient("mongodb+srv://mainUser:BtPCEIb8e3Dbb06n@323projectdatacluster.k94r8.azure.mongodb.net/MainData?retryWrites=true&w=majority");
            var db = client.GetDatabase("MainData");
            var col = db.GetCollection<TestModel>("ConnectionTest");
            col.InsertOne(new TestModel { Name="Test",Random="Random value" });
        }

        public static List<T> GetData<T>()
        {
            var client = new MongoClient("mongodb+srv://mainUser:BtPCEIb8e3Dbb06n@323projectdatacluster.k94r8.azure.mongodb.net/MainData?retryWrites=true&w=majority");
            var db = client.GetDatabase("MainData");
            var col = db.GetCollection<T>("Test");
            return col.Find(new BsonDocument()).ToList();
        }

    }
}
