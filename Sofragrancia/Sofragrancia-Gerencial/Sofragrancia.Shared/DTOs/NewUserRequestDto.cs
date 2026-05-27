using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Shared.DTOs
{
    public record NewUserRequestDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; } 
        public string NomeCompleto { get; set; }
    }
}
