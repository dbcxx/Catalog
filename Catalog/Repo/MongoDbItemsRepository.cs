//using Catalog.Entities;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Catalog.Repo
//{
//    public class MongoDbItemsRepository : IInMemoryItemsRepository
//    {
//        private const string databaseName = "catalog";
//        private const string collectionName = "items";
//        private readonly IMongoCollection<Item> itemsCollection; 

//        public MongoDbItemsRepository(IMongoClient mongoClient)
//        {
//            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
//            itemsCollection = database.GetCollection<Item>(collectionName);
//        }
//        public async Task CreateItemsAsync(Item item)
//        {
//            await itemsCollection.InsertOneAsync(item);
//        }

//        public async Task DeleteItemAsync(Guid id)
//        {
//            var filter = filterBuilder.Eq(item => item.Id, id);
//            await itemsCollection.DeleteOneAsync(filter);
//        }

//        public async Task<Item> GetItemAsync(Guid id)
//        {
//            var filter = filterBuilder.Eq(item => item.Id, id);
//            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
//        }

//        public async Task<IEnumerable<Item>> GetItemsAsync()
//        {
//            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
//        }

//        public Task<Item> GetItemsAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task UpdateItemAsync(Item item)
//        {
//            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
//            await itemsCollection.ReplaceOneAsync(filter, item);
//        }

//        public Task UpdateItemsAsync(Item item)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
