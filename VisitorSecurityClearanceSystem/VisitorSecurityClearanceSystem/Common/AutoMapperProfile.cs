using AutoMapper;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entites;

namespace VisitorSecurityClearanceSystem.Common
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VisitorEntity,VisitorDTO>().ReverseMap();
            CreateMap<SecurityEntity,SecurityDTO>().ReverseMap();
            CreateMap<OfficeEntity,OfficeDTO>().ReverseMap();
            CreateMap<ManagerEntity,ManagerDTO>().ReverseMap();
        }
    }
}
