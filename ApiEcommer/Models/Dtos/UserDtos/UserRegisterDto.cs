using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommer.Models.Dtos.UserDtos
{
    public class UserRegisterDto
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? Role { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}