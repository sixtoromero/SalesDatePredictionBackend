using AutoMapper;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.Transversal.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {                        
            CreateMap<Orders, OrdersDTO>().ReverseMap();
            CreateMap<Employees, EmployeesDTO>().ReverseMap();
            CreateMap<Shippers, ShippersDTO>().ReverseMap();
        }
    }
}
