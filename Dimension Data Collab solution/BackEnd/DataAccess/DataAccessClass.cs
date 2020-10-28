using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.DataAccess
{
    public class  DataAccessClass<T>
    {

        public IMongoCollection<T> collection { get; private set; }

        public DataAccessClass(string conString,string table,string database)
        {
            var client = new MongoClient(conString);
            var db = client.GetDatabase(database);
            var col = db.GetCollection<T>(table);
            collection = col;
        }

        public virtual async Task<List<T>> GetAllRecords(int page,int pageSize)//NOTE: start counting at 1
        {
            return await collection.Find(new BsonDocument()).Skip((page-1) * pageSize).Limit(pageSize).ToListAsync();
        }

        //public async Task<List<T>> GetAllRecordsAndFilter(int page, int pageSize , string query)//NOTE: start counting at 1
        //{
        //    var filter = Builders<T>.Filter.AnyIn();
        //    return await collection.Find(new BsonDocument()).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync();
        //}

        public async Task<T> GetRecordById(ObjectId _id)
        {
            var filter = Builders<T>.Filter.Eq("_id", _id);

            var res = await collection.FindAsync(filter);

            return res.First();
        }

        public void InsertRecord(T record)
        {
            collection.InsertOne(record);
        }

        //public void UpdateRecord(ObjectId _id, T record)
        //{
        //    var filter = Builders<T>.Filter.Eq("_id", _id);
        //    var update = Builders<BsonDocument>.Update.
        //    collection.UpdateOne(filter,record);
        //}

        public void UpsertRecord (ObjectId _id,T record)
        {
            collection.ReplaceOne(new BsonDocument("_id",_id), record,new ReplaceOptions {IsUpsert = true });
        }

        public void DeleteRecord(ObjectId _id)
        {
            var filter = Builders<T>.Filter.Eq("_id", _id);
            collection.DeleteOne(filter);
        }

        public async Task<long> GetCount()
        {
            return await collection.CountDocumentsAsync(new BsonDocument());
        }
    }
}
