using AutoMapper;
using ApiEcommer.Models;
using ApiEcommer.Models.Dtos;

namespace ApiEcommer.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        // Mapeo de Category a CategoryDto
        CreateMap<Category, CategoryDto>();
        
        // Mapeo de CreateCategoryDto a Category
        CreateMap<CreateCategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        
        // Mapeo de UpdateCategoryDto a Category (ignora Id y CreationDate)
        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreationDate, opt => opt.Ignore());
    }
}