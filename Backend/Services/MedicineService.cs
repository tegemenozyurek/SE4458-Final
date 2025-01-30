using MongoDB.Driver;
using Prescription_and_Doctor_Visit_Management_System.Models;

namespace Prescription_and_Doctor_Visit_Management_System.Services
{
    public class MedicineService
    {
        private readonly IMongoCollection<Medicine> _medicinesCollection;

        public MedicineService(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration["MongoDB:ConnectionString"]);
            var mongoDatabase = mongoClient.GetDatabase(configuration["MongoDB:DatabaseName"]);
            _medicinesCollection = mongoDatabase.GetCollection<Medicine>(configuration["MongoDB:MedicinesCollectionName"]);
        }

        public async Task<List<Medicine>> GetAllAsync() =>
            await _medicinesCollection.Find(_ => true).ToListAsync();

        public async Task<Medicine?> GetByNameAsync(string name) =>
            await _medicinesCollection.Find(m => m.Name == name).FirstOrDefaultAsync();

        public async Task<List<Medicine>> GetByPriceRangeAsync(int minPrice, int maxPrice) =>
            await _medicinesCollection.Find(m => m.Price >= minPrice && m.Price <= maxPrice).ToListAsync();

        public async Task CreateAsync(Medicine newMedicine) =>
            await _medicinesCollection.InsertOneAsync(newMedicine);

        public async Task<bool> DeleteByNameAsync(string name)
        {
            var result = await _medicinesCollection.DeleteOneAsync(m => m.Name == name);
            return result.DeletedCount > 0;
        }

        // Search medicines by partial name (autocomplete-like)
        public async Task<List<Medicine>> GetByPartialNameAsync(string partialName)
        {
            var filter = Builders<Medicine>.Filter.Regex(m => m.Name, new MongoDB.Bson.BsonRegularExpression(partialName, "i")); // "i" => case-insensitive
            var medicines = await _medicinesCollection.Find(filter).ToListAsync();
            return medicines;
        }
    }
}
