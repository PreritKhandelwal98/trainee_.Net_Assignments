using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface IVisitorService
    {
        Task<VisitorDTO> AddVisitor(VisitorDTO visitorModel);
        Task<IEnumerable<VisitorDTO>> GetAllVisitors();
        Task<VisitorDTO> GetVisitorById(string id);
        Task<VisitorDTO> UpdateVisitor(string id, VisitorDTO visitorModel);
        Task<VisitorDTO> UpdateVisitorStatus(string visitorId, bool newStatus);
        Task<List<VisitorDTO>> GetVisitorsByStatus(bool status);

        Task DeleteVisitor(string id);
        Task<VisitorDTO> AddVisitorByMakePostRequest(VisitorDTO visitor);
        Task<IEnumerable<VisitorDTO>> GetAllEmployees();
    }
}
