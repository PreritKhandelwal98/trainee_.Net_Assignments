using AutoMapper;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.Common
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() {
            CreateMap<EmployeeBasicDetailsDTO,EmployeeBasicDetailsEntity>().ReverseMap();
            CreateMap<EmployeeAdditionalDetailsDTO,EmployeeAdditionalDetailsEntity>().ReverseMap();
        }
    }
}
