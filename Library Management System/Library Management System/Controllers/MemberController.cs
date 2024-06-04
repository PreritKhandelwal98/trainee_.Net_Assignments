using Library_Management_System.Entities;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : Controller
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibraryManagement";
        public string ContainerName = "Members";

        private readonly Container container;

        public MemberController()
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

        public async Task<MemberModel> AddMembers(MemberModel member)
        {
            MemberEntity memberEntity = new MemberEntity();

            memberEntity.Name = member.Name;
            memberEntity.ContactNumber = member.ContactNumber;
            memberEntity.Email = member.Email;
            memberEntity.DateOfBirth = member.DateOfBirth;


            memberEntity.Id = Guid.NewGuid().ToString();
            memberEntity.UId = memberEntity.Id;
            memberEntity.DocumentType = "member";
            memberEntity.CreatedBy = "Prerit";
            memberEntity.CreatedOn = DateTime.Now;
            memberEntity.UpdatedBy = "";
            memberEntity.UpdatedOn = DateTime.Now;
            memberEntity.Version = 1;
            memberEntity.Active = true;
            memberEntity.Archived = false;


            MemberEntity response = await container.CreateItemAsync(memberEntity);


            MemberModel resModel = new MemberModel();
            resModel.Name = member.Name;
            resModel.ContactNumber = member.ContactNumber;
            resModel.Email = member.Email;
            resModel.DateOfBirth = member.DateOfBirth;

            return resModel;
        }

        [HttpGet]

        public async Task<List<MemberModel>> GetAllMembers()
        {
            var membersQuery = container.GetItemLinqQueryable<MemberEntity>(true)
                                         .Where(q => q.Active == true && q.Archived == false && q.DocumentType == "member");

            var members = membersQuery.ToList();

            List<MemberModel> memberModels = new List<MemberModel>();

            foreach (var mb in members)
            {
                MemberModel model = new MemberModel
                {
                    UId = mb.UId,
                    Id = mb.Id,
                    Name = mb.Name,
                    ContactNumber = mb.ContactNumber,
                    Email = mb.Email,
                    DateOfBirth = mb.DateOfBirth,
                };

                memberModels.Add(model);
            }


            return memberModels;
        }

        [HttpGet]
        public async Task<MemberModel> GetMemberById(string UId)
        {

            var member = container.GetItemLinqQueryable<MemberEntity>(true)
                                   .Where(q => q.UId == UId && q.Active == true && q.Archived == false)
                                   .AsEnumerable() // Ensures query execution
                                   .FirstOrDefault();

            MemberModel memberModel = new MemberModel
            {
                UId = member.UId,
                Id = member.Id,
                Name = member.Name,
                ContactNumber = member.ContactNumber,
                Email = member.Email,
                DateOfBirth = member.DateOfBirth
            };

            // Step 3: Return Model
            return memberModel;
        }

        [HttpPost]
        public async Task<MemberModel> UpdateMember(MemberModel book)
        {
            var availableMember = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == book.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            availableMember.Archived = true;
            availableMember.Active = false;
            await container.ReplaceItemAsync(availableMember, availableMember.UId);

            availableMember.Id = Guid.NewGuid().ToString();
            availableMember.UpdatedBy = "Prerit";
            availableMember.UpdatedOn = DateTime.Now;
            availableMember.Version = availableMember.Version + 1;
            availableMember.Active = true;
            availableMember.Archived = false;

            availableMember.Name = book.Name;
            availableMember.ContactNumber = book.ContactNumber;
            availableMember.Email = book.Email;
            availableMember.DateOfBirth = book.DateOfBirth;



            availableMember = await container.CreateItemAsync(availableMember);


            MemberModel memberModel = new MemberModel();
            memberModel.UId = availableMember.UId;
            memberModel.Name = availableMember.Name;
            memberModel.ContactNumber = availableMember.ContactNumber;
            memberModel.Email = availableMember.Email;
            memberModel.DateOfBirth = availableMember.DateOfBirth;


            return memberModel;

        }
    }
}
