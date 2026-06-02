using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Shared.Dtos;

public record NewUserRequestDto
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public UserMetaData MetaDados { get; set; }
}

public record UserMetaData
{
    public string Role { get; set; }
    public string NomeCompleto { get; set; }

}

