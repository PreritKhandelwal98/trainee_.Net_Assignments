using VisitorSecurityClearanceSystem.Entites;

namespace VisitorSecurityClearanceSystem.CosmoDB
{
    public interface ICosmoDBService
    {
        Task<VisitorEntity> GetVisitorByEmail(string email);
        Task<List<VisitorEntity>> GetVisitorByStatus(bool status);
        Task<VisitorEntity> GetVisitorById(string id);
        Task<SecurityEntity> GetSecurityById(string id);
        Task<ManagerEntity> GetManagerById(string id);
        Task<OfficeEntity> GetOfficeById(string id);
        Task<T> GetById<T>(int id);
        Task<IEnumerable<T>> GetAll<T>();
        Task<T> Add<T>(T entity);
        Task<T> Update<T>(T entity);

        Task<OfficeEntity> GetOfficeUserByEmail(string email);
        Task<SecurityEntity> GetSecurityUserByEmail(string email);
        Task DeleteVisitor(string id);
        Task DeleteManager(string id);
        Task DeleteSecurity(string id);
        Task DeleteOffice(string id);

    }
}
