using api_BookStore.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace api_BookStore.Services
{
    public class BooksServices
    {
        private readonly IMongoCollection<Book> _booksCollection;
        public BooksServices(
            IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName );
            _booksCollection = mongoDatabase.GetCollection<Book>(
                bookStoreDatabaseSettings.Value.BooksCollectionName);    
        }
    }
}
