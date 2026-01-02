using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommer.Models.Dtos;
using AutoMapper; // Librería para mapeo automático entre objetos

namespace ApiEcommer.Mapping
{
    /// <summary>
    /// Perfil de AutoMapper para configurar el mapeo entre entidades Category y sus DTOs
    /// Los perfiles definen cómo convertir un tipo de objeto a otro automáticamente
    /// </summary>
    public class CategoryProfile : Profile
    {
        /// <summary>
        /// Constructor que configura los mapeos para Category
        /// </summary>
        public CategoryProfile()
        {
            // Mapeo bidireccional entre Category (entidad) y CategoryDto (para lectura)
            CreateMap<Category, CategoryDto>().ReverseMap();
            
            // Mapeo bidireccional entre Category (entidad) y CreateCategoryDto (para creación)
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
        }
    }
}