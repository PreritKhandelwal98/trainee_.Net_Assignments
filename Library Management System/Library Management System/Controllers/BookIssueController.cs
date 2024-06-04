using Library_Management_System.Entities;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookIssueController : Controller
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";
        public string ContainerName = "Issues";

        private readonly Container container;
        private readonly Container bookContainer;
        private readonly Container memberContainer;
        public BookIssueController()
        {
            container = GetContainer(ContainerName);
            bookContainer = GetContainer("Books");
            memberContainer = GetContainer("Members");
        }

        private Container GetContainer(string containerName)
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            return database.GetContainer(containerName);
        }

        [HttpPost]
        public async Task<IActionResult> IssueBook(IssueModel issue)
        {
            // Validate book availability
            var book = bookContainer.GetItemLinqQueryable<BookEntity>(true)
                                    .Where(b => b.UId == issue.BookId && b.IsIssued == false && b.Active && !b.Archived)
                                    .AsEnumerable()
                                    .FirstOrDefault();

            if (book == null)
            {
                return BadRequest("Book not available or already issued.");
            }

            
            var member = memberContainer.GetItemLinqQueryable<MemberEntity>(true)
                                        .Where(m => m.UId == issue.MemberId && m.Active && !m.Archived)
                                        .AsEnumerable()
                                        .FirstOrDefault();

            if (member == null)
            {
                return BadRequest("Member not found.");
            }
            var temp = Guid.NewGuid().ToString();

            IssueEntity issueEntity = new IssueEntity
            {
                
                Id = temp,
                UId = temp,
                BookId = issue.BookId,
                MemberId = issue.MemberId,
                IssueDate = issue.IssueDate,
                ReturnDate = issue.ReturnDate,
                IsReturned = issue.IsReturned,
                DocumentType = "issue",
                CreatedBy = "Prerit",
                CreatedOn = DateTime.Now,
                UpdatedBy = "",
                UpdatedOn = DateTime.Now,
                Version = 1,
                Active = true,
                Archived = false
            };

            IssueEntity response = await container.CreateItemAsync(issueEntity);

            
            book.IsIssued = true;
            book.UpdatedBy = "Prerit";
            book.UpdatedOn = DateTime.Now;
            await bookContainer.ReplaceItemAsync(book, book.UId);

            IssueModel resModel = new IssueModel
            {
                UId = response.UId,
                BookId = response.BookId,
                MemberId = response.MemberId,
                IssueDate = response.IssueDate,
                ReturnDate = response.ReturnDate,
                IsReturned = response.IsReturned
            };

            return Ok(resModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetIssueById(string UId)
        {
            var issue = container.GetItemLinqQueryable<IssueEntity>(true)
                                  .Where(q => q.UId == UId).FirstOrDefault();

            if (issue == null)
            {
                return NotFound();
            }

            var book = bookContainer.GetItemLinqQueryable<BookEntity>(true)
                                    .Where(b => b.UId == issue.BookId && b.Active && !b.Archived)
                                    .AsEnumerable()
                                    .FirstOrDefault();

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            IssueModel issueModel = new IssueModel
            {
                UId = issue.UId,
                BookId = issue.BookId,
                MemberId = issue.MemberId,
                IssueDate = issue.IssueDate,
                ReturnDate = issue.ReturnDate,
                IsReturned = issue.IsReturned,
                BookDetails = new BookModel
                {
                    UId = book.UId,
                    Title = book.Title,
                    Author = book.Author,
                    ISBN = book.ISBN,
                    PublishedDate = book.PublishedDate,
                    IsIssued = book.IsIssued
                }
            };

            return Ok(issueModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIssue(IssueModel issue)
        {
            var availableIssue = container.GetItemLinqQueryable<IssueEntity>(true)
                                           .Where(q => q.UId == issue.UId && q.Active && !q.Archived)
                                           .FirstOrDefault();

            if (availableIssue == null)
            {
                return NotFound("Issue not found.");
            }

            availableIssue.Archived = true;
            availableIssue.Active = false;
            await container.ReplaceItemAsync(availableIssue, availableIssue.UId);

            availableIssue.Id = Guid.NewGuid().ToString();
            availableIssue.UpdatedBy = "Prerit";
            availableIssue.UpdatedOn = DateTime.Now;
            availableIssue.Version = availableIssue.Version + 1;
            availableIssue.Active = true;
            availableIssue.Archived = false;
            availableIssue.BookId = issue.BookId;
            availableIssue.MemberId = issue.MemberId;
            availableIssue.IssueDate = issue.IssueDate;
            availableIssue.ReturnDate = issue.ReturnDate;
            availableIssue.IsReturned = issue.IsReturned;

            availableIssue = await container.CreateItemAsync(availableIssue);

            IssueModel issueModel = new IssueModel
            {
                UId = availableIssue.UId,
                BookId = availableIssue.BookId,
                MemberId = availableIssue.MemberId,
                IssueDate = availableIssue.IssueDate,
                ReturnDate = availableIssue.ReturnDate,
                IsReturned = availableIssue.IsReturned
            };

            // Update book status if returned
            if (issue.IsReturned)
            {
                var book = bookContainer.GetItemLinqQueryable<BookEntity>(true)
                                        .Where(b => b.UId == issue.BookId && b.Active && !b.Archived)
                                        .AsEnumerable()
                                        .FirstOrDefault();

                if (book != null)
                {
                    book.IsIssued = false;
                    book.UpdatedBy = "Prerit";
                    book.UpdatedOn = DateTime.Now;
                    await bookContainer.ReplaceItemAsync(book, book.UId);
                }
            }

            return Ok(issueModel);
        }
    }
}
