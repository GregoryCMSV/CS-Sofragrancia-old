using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class ClienteDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tx_razaosocial")]
        public string RazaoSocial { get; set; } = string.Empty;

        [JsonPropertyName("tx_cnpj")]
        public string Cnpj { get; set; } = string.Empty;

        [JsonPropertyName("tx_telefone")]
        public string? Telefone { get; set; }

        [JsonPropertyName("tx_email")]
        public string? Email { get; set; }

        [JsonPropertyName("tx_endereco")]
        public string? Endereco { get; set; }

        [JsonPropertyName("tx_cidade")]
        public string? Cidade { get; set; }

        [JsonPropertyName("tx_estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("nr_limitecredito")]
        public decimal? LimiteCredito { get; set; }

        [JsonPropertyName("dt_createdate")]
        public DateTime DataCriacao { get; set; }

        [JsonPropertyName("dt_updatedate")]
        public DateTime DataAtualizacao { get; set; }

        [JsonPropertyName("fl_isenable")]
        public bool Ativo { get; set; }

        [JsonPropertyName("nr_codcliente")]
        public int CodCliente { get; set; }
    }
}