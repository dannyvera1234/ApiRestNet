using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommer.Models.Dtos.UserDtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "El campo es requerido")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string? Role { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}