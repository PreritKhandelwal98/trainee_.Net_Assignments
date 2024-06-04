using Library_Management_System.Entities;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;


namespace Library_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : Controller
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";
        public string ContainerName = "Books";

        private readonly Container container;

        public BookController()
        {
            container = GetContiner();
        }

        private Container GetContiner()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database databse = cosmosClient.GetDatabase(DatabaseName);

            Container container = databse.GetContainer(ContainerName);

            return container;
        }

        [HttpPost]

        public async Task<BookModel> AddBooks(BookModel book)
        {
            BookEntity bookEntity = new BookEntity();

            bookEntity.Title = book.Title;
            bookEntity.Author = book.Author;
            bookEntity.ISBN = book.ISBN;
            bookEntity.PublishedDate = book.PublishedDate;
            bookEntity.IsIssued = false;

            bookEntity.Id=Guid.NewGuid().ToString();
            bookEntity.UId = bookEntity.Id;
            bookEntity.DocumentType = "book";
            bookEntity.CreatedBy = "Prerit";
            bookEntity.CreatedOn = DateTime.Now;
            bookEntity.UpdatedBy = "";
            bookEntity.UpdatedOn = DateTime.Now;
            bookEntity.Version = 1;
            bookEntity.Active = true;
            bookEntity.Archived = false;

           
            BookEntity response = await container.CreateItemAsync(bookEntity);

            
            BookModel resModel = new BookModel();
            resModel.Title = book.Title;
            resModel.Author = book.Author;
            resModel.ISBN = book.ISBN;
            resModel.PublishedDate = book.PublishedDate;
            resModel.IsIssued = book.IsIssued;

            return resModel;
        }

        [HttpGet]

        public async Task<List<BookModel>> GetAllBooks()
        {
            var booksQuery = container.GetItemLinqQueryable<BookEntity>(true)
                                         .Where(q => q.Active == true && q.Archived == false && q.DocumentType == "book");

            var books = booksQuery.ToList();

            List<BookModel> BookModel = new List<BookModel>();

            foreach (var bk in books)
            {
                BookModel model = new BookModel
                {
                    UId = bk.UId,
                    Id = bk.Id,
                    Title = bk.Title,
                    Author = bk.Author,
                    ISBN = bk.ISBN,
                    PublishedDate = bk.PublishedDate,
                    IsIssued = bk.IsIssued

                };

                BookModel.Add(model);
            }

           
            return BookModel;
        }

        [HttpGet]
        public async Task<BookModel> GetBookById(string UId)
        {
            
            var student = container.GetItemLinqQueryable<BookEntity>(true)
                                   .Where(q => q.UId == UId && q.Active == true && q.Archived == false)
                                   .AsEnumerable() // Ensures query execution
                                   .FirstOrDefault();

            BookModel bookModel = new BookModel
            {
                UId = student.UId,
                Id = student.Id,
                Title = student.Title,
                Author = student.Author,
                ISBN = student.ISBN,
                PublishedDate = student.PublishedDate,
                IsIssued = student.IsIssued
            };

            // Step 3: Return Model
            return bookModel;
        }

        [HttpGet]
        public async Task<BookModel> GetBookByName(string Title)
        {

            var student = container.GetItemLinqQueryable<BookEntity>(true)
                                   .Where(q => q.Title == Title && q.Active == true && q.Archived == false)
                                   .AsEnumerable() // Ensures query execution
                                   .FirstOrDefault();

            BookModel bookModel = new BookModel
            {
                UId = student.UId,
                Id = student.Id,
                Title = student.Title,
                Author = student.Author,
                ISBN = student.ISBN,
                PublishedDate = student.PublishedDate,
                IsIssued = student.IsIssued
            };

            // Step 3: Return Model
            return bookModel;
        }

        [HttpGet]
        public async Task<BookModel> GetNotIssuedBooks()
        {

            var student = container.GetItemLinqQueryable<BookEntity>(true)
                                   .Where(q => q.IsIssued == false && q.Active == true && q.Archived == false)
                                   .AsEnumerable() // Ensures query execution
                                   .FirstOrDefault();

            BookModel bookModel = new BookModel
            {
                UId = student.UId,
                Id = student.Id,
                Title = student.Title,
                Author = student.Author,
                ISBN = student.ISBN,
                PublishedDate = student.PublishedDate,
                IsIssued = student.IsIssued
            };

            // Step 3: Return Model
            return bookModel;
        }

        [HttpGet]
        public async Task<BookModel> GetIssuedBooks()
        {

            var student = container.GetItemLinqQueryable<BookEntity>(true)
                                   .Where(q => q.IsIssued == true && q.Active == true && q.Archived == false)
                                   .FirstOrDefault();

            BookModel bookModel = new BookModel
            {
                UId = student.UId,
                Id = student.Id,
                Title = student.Title,
                Author = student.Author,
                ISBN = student.ISBN,
                PublishedDate = student.PublishedDate,
                IsIssued = student.IsIssued
            };

            // Step 3: Return Model
            return bookModel;
        }

        [HttpPost]
        public async Task<BookModel> UpdateBook(BookModel book)
        {
            var availableBook = container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == book.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            availableBook.Archived = true;
            availableBook.Active = false;
            await container.ReplaceItemAsync(availableBook, availableBook.UId);

            availableBook.Id = Guid.NewGuid().ToString();
            availableBook.UpdatedBy = "Prerit";
            availableBook.UpdatedOn = DateTime.Now;
            availableBook.Version = availableBook.Version + 1;
            availableBook.Active = true;
            availableBook.Archived = false;

            availableBook.Title  = book.Title;
            availableBook.Author = book.Author;
            availableBook.ISBN = book.ISBN;
            availableBook.PublishedDate = book.PublishedDate;
            availableBook.IsIssued = book.IsIssued;

           
            availableBook = await container.CreateItemAsync(availableBook);

            
            BookModel bookModel = new BookModel();
            bookModel.UId = availableBook.UId;
            bookModel.Title = availableBook.Title;
            bookModel.Author = availableBook.Author;
            bookModel.ISBN = availableBook.ISBN;
            bookModel.PublishedDate = availableBook.PublishedDate;
            bookModel.IsIssued = availableBook.IsIssued;

            return bookModel;


        }
    }
}
