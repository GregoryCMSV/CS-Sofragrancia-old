namespace Sofragrancia.Shared.Dtos;

public class RespostaSucessoApiDto
{
    public string Mensagem { get; set; } = string.Empty;
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