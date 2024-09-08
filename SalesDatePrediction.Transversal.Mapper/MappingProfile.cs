using AutoMapper;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.Transversal.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Users, UsersDTO>().ReverseMap();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<UserRoles, UserRolesDTO>().ReverseMap();

            CreateMap<Databases, DatabasesDTO>().ReverseMap();
            CreateMap<Tables, TablesDTO>().ReverseMap();
            CreateMap<Columns, ColumnsDTO>().ReverseMap();
            CreateMap<Indexes, IndexesDTO>().ReverseMap();
            CreateMap<Constraints, ConstraintsDTO>().ReverseMap();
            
            CreateMap<Orders, OrdersDTO>().ReverseMap();
            CreateMap<Employees, EmployeesDTO>().ReverseMap();

        }
    }
}
