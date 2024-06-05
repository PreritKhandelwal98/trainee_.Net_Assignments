﻿using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface IVisitorService
    {
        Task<VisitorDTO> AddVisitor(VisitorDTO visitorModel);
        Task<IEnumerable<VisitorDTO>> GetAllVisitors();
        Task<VisitorDTO> GetVisitorById(string id);
        Task<VisitorDTO> UpdateVisitor(string id, VisitorDTO visitorModel);
        Task DeleteVisitor(string id);
        /*Task<IEnumerable<VisitorDTO>> GetVisitorsByStatus(string status);*/
    }
}