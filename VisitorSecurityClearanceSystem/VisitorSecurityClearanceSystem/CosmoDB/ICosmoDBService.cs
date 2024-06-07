using VisitorSecurityClearanceSystem.Entites;

namespace VisitorSecurityClearanceSystem.CosmoDB
{
    public interface ICosmoDBService
    {

        //Generic Function for all services
        Task<IEnumerable<T>> GetAll<T>();
        Task<T> Add<T>(T entity);
        Task<T> Update<T>(T entity);


        //Visitor User functions
        Task<VisitorEntity> GetVisitorByEmail(string email);
        Task<List<VisitorEntity>> GetVisitorByStatus(bool status);
        Task<VisitorEntity> GetVisitorById(string id);
        Task<IEnumerable<VisitorEntity>> GetAllVisitor();
        Task DeleteVisitor(string id);


        //Security User functions
        Task<SecurityEntity> GetSecurityById(string id);
        Task DeleteSecurity(string id);
        Task<SecurityEntity> GetSecurityUserByEmail(string email);


        //Manager User functions
        Task<ManagerEntity> GetManagerById(string id);
        Task DeleteManager(string id);


        //Office User functions
        Task<OfficeEntity> GetOfficeById(string id);
        Task<OfficeEntity> GetOfficeUserByEmail(string email);
        Task DeleteOffice(string id);

    }
}
