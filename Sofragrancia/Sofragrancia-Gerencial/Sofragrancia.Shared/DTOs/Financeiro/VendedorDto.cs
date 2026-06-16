using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class VendedorDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tx_nome")]
        public string Nome { get; set; } = string.Empty;

        [JsonPropertyName("tx_cpf")]
        public string Cpf { get; set; } = string.Empty;

        [JsonPropertyName("tx_telefone")]
        public string? Telefone { get; set; }

        [JsonPropertyName("tx_email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("tx_regiao")]
        public string? Regiao { get; set; }

        [JsonPropertyName("dt_dataadmissao")]
        public DateTime? DataAdmissao { get; set; }

        [JsonPropertyName("dt_createdate")]
        public DateTime DataCriacao { get; set; }

        [JsonPropertyName("dt_updatedate")]
        public DateTime DataAtualizacao { get; set; }

        [JsonPropertyName("fl_isenable")]
        public bool Ativo { get; set; }

        [JsonPropertyName("nr_codvendedor")]
        public int CodVendedor { get; set; }
    }
}