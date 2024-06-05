using System.Reflection.Metadata;
using System.Xml.Linq;
using VisitorSecurityClearanceSystem.CosmoDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entites;
using VisitorSecurityClearanceSystem.Interface;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using AutoMapper;

namespace VisitorSecurityClearanceSystem.Services
{
    public class VisitorService: IVisitorService
    {
        private readonly ICosmoDBService _cosmoDBService;
        private readonly IMapper _autoMapper;
        public VisitorService(ICosmoDBService cosmoDBService, IMapper mapper)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;

        }

        public async Task<VisitorDTO> AddVisitor(VisitorDTO visitorModel)
        {
            var existingVisitor = await _cosmoDBService.GetVisitorByEmail(visitorModel.Email);
            if (existingVisitor != null)
            {
                throw new InvalidOperationException("A visitor with the provided email already exists.");
            }

            // Map the DTO to an Entity
            var visitorEntity = _autoMapper.Map<VisitorEntity>(visitorModel);

            // Initialize the Entity
            visitorEntity.Intialize(true, "visitor", "Prerit", "Prerit");

            // Add the entity to the database
            var response = await _cosmoDBService.Add(visitorEntity);

            // Prepare email details
            string subject = "Visitor Registration Approval Request";
            string toEmail = "prerit.web@gmail.com";  // Change to manager's email
            string userName = "Manager";

            // Construct the email message with visitor's details
            string message = $"Dear {userName},\n\n" +
                             $"A new visitor has registered and is awaiting your approval.\n\n" +
                             $"Visitor Details:\n" +
                             $"Name: {visitorModel.Name}\n" +
                             $"Contact Number: {visitorModel.Phone}\n" +
                             $"Email: {visitorModel.Email}\n" +
                             $"Purpose of Visit: {visitorModel.Purpose}\n\n" +
                             "Please review the details and approve or reject the request.\n\n" +
                             "Thank you,\nVisitor Management System";

            // Send the email
            EmailSender emailSender = new EmailSender();
            await emailSender.SendEmail(subject, toEmail, userName, message);

            // Map the response back to a DTO
            return _autoMapper.Map<VisitorDTO>(response);
        }

        public async Task<IEnumerable<VisitorDTO>> GetAllVisitors()
        {
            var visitors = await _cosmoDBService.GetAll<VisitorEntity>();
            return visitors.Select(MapEntityToDTO).ToList();
        }

        public async Task<VisitorDTO> GetVisitorById(string id)
        {
            var visitor = await _cosmoDBService.GetVisitorById(id); // Call non-generic method
            return _autoMapper.Map<VisitorDTO>(visitor);
        }

        public async Task<List<VisitorDTO>> GetVisitorsByStatus(bool status)
        {
            var visitors = await _cosmoDBService.GetVisitorByStatus(status);
            var visitorDTOs = visitors.Select(MapEntityToDTO).ToList();
            return visitorDTOs;
        }


        public async Task<VisitorDTO> UpdateVisitor(string id, VisitorDTO visitorModel)
        {
            var visitorEntity = await _cosmoDBService.GetVisitorById(id);
            if (visitorEntity == null)
            {
                throw new Exception("Manager not found");
            }
            visitorEntity = _autoMapper.Map<VisitorEntity>(visitorModel); ;
            visitorEntity.Id = id;
            var response = await _cosmoDBService.Update(visitorEntity);
            return _autoMapper.Map<VisitorDTO>(response);
        }

        public async Task<VisitorDTO> UpdateVisitorStatus(string visitorId, bool newStatus)
        {
            var visitor = await _cosmoDBService.GetVisitorById(visitorId);
            if (visitor == null)
                throw new Exception("Visitor not found");

            visitor.PassStatus = newStatus;
            await _cosmoDBService.Update(visitor);

            // Prepare email details
            string subject = "Your Visitor Status Has Been Updated";
            string toEmail = visitor.Email;  // Send to visitor's email
            string userName = visitor.Name;

            // Construct the email message with the new status details
            string message = $"Dear {userName},\n\n" +
                             $"We wanted to inform you that your visitor status has been updated.\n\n" +
                             $"New Status: {newStatus}\n\n" +
                             "If you have any questions or need further assistance, please contact us.\n\n" +
                             "Thank you,\nVisitor Management System";

            // If the status is true, generate the PDF and attach it to the email
            byte[] pdfBytes = null;
            if (newStatus)
            {
                pdfBytes = GenerateVisitorPassPdf(visitor);
            }

            // Send the email with or without the PDF attachment
            EmailSender emailSender = new EmailSender();
            await emailSender.SendEmail(subject, toEmail, userName, message, pdfBytes);

            return new VisitorDTO
            {
                Id = visitor.Id,
                Name = visitor.Name,
                Email = visitor.Email,
                PassStatus = visitor.PassStatus,
                // Map other properties as needed
            };
        }

        private byte[] GenerateVisitorPassPdf(VisitorEntity visitor)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Define fonts
                XFont titleFont = new XFont("Arial", 20, XFontStyle.Bold);
                XFont normalFont = new XFont("Arial", 12);

                // Draw title
                gfx.DrawString("Visitor Pass", titleFont, XBrushes.Black, new XRect(0, 20, page.Width.Point, page.Height.Point), XStringFormats.Center);

                // Draw visitor details
                int yOffset = 60;
                gfx.DrawString($"Name: {visitor.Name}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width.Point - 100, page.Height.Point), XStringFormats.TopLeft);
                yOffset += 20;
                gfx.DrawString($"Email: {visitor.Email}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width.Point - 100, page.Height.Point), XStringFormats.TopLeft);
                yOffset += 20;
                gfx.DrawString($"Phone: {visitor.Phone}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width.Point - 100, page.Height.Point), XStringFormats.TopLeft);
                yOffset += 20;
                gfx.DrawString($"Purpose of Visit: {visitor.Purpose}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width.Point - 100, page.Height.Point), XStringFormats.TopLeft);

                // Save the PDF to memory stream
                document.Save(ms);
                ms.Position = 0;

                return ms.ToArray();
            }
        }

        /*public async Task DeleteVisitor(string id)
        {
            await _cosmoDBService.Delete<VisitorEntity>(id);
        }*/

        private VisitorEntity MapDTOToEntity(VisitorDTO visitorModel)
        {
            return new VisitorEntity
            {
                Id = visitorModel.Id,
                Name = visitorModel.Name,
                Email = visitorModel.Email,
                Phone = visitorModel.Phone,
                Address = visitorModel.Address,
                CompanyName = visitorModel.CompanyName,
                Purpose = visitorModel.Purpose,
                EntryTime = visitorModel.EntryTime,
                ExitTime = visitorModel.ExitTime,
                Role= "visitor",
                PassStatus = false,
            };
        }

        private VisitorDTO MapEntityToDTO(VisitorEntity visitorEntity)
        {
            if (visitorEntity == null) return null;
            return new VisitorDTO
            {
                Id = visitorEntity.Id,
                Name = visitorEntity.Name,
                Email = visitorEntity.Email,
                Phone = visitorEntity.Phone,
                Address = visitorEntity.Address,
                CompanyName = visitorEntity.CompanyName,
                Purpose = visitorEntity.Purpose,
                EntryTime = visitorEntity.EntryTime,
                ExitTime = visitorEntity.ExitTime,
                Role = "visitor",
                PassStatus = false
            };
        }
    }
}
