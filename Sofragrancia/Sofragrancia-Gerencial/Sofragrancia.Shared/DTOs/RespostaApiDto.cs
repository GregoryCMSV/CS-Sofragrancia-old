using System.Text.Json.Serialization;

namespace Sofragrancia.Shared.Dtos;

public class RespostaSucessoApiDto
{
    [JsonPropertyName("mensagem")]
    public string Mensagem { get; set; } = string.Empty;

    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;
}

public class RespostaErroApiDto
{
    public string Erro { get; set; } = string.Empty;
    public string Detalhe { get; set; } = string.Empty;
}

public class RespostaValidacaoTokenDto
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
}