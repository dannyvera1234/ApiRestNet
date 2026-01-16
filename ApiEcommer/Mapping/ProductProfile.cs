using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommer.Models.Dtos;
using ApiEcommer.Models.Dtos.ProductDtos;
using AutoMapper;

namespace ApiEcommer.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // mapeo de prodct y prodct dto
        CreateMap<Product, ProductDto>().ReverseMap();

        // mapeo de create prodt dto y produt
        CreateMap<CreateProductDto, Product>()
        .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        // mapeo de update prodt dto y prodt
        CreateMap<UpdateProductDto, Product>()
          .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationDate, opt => opt.Ignore());
    }
}
