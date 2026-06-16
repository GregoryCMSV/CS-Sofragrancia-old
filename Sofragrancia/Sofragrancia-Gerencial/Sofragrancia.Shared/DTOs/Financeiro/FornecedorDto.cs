using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class FornecedorDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tx_nome")]
        public string Nome { get; set; } = string.Empty;

        [JsonPropertyName("tx_cnpjcpf")]
        public string CnpjCpf { get; set; } = string.Empty;

        [JsonPropertyName("tx_telefone")]
        public string Telefone { get; set; } = string.Empty;

        [JsonPropertyName("tx_email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("tx_endereco")]
        public string Endereco { get; set; } = string.Empty;

        [JsonPropertyName("tx_representante")]
        public string Representante { get; set; } = string.Empty;

        [JsonPropertyName("dt_createdate")]
        public DateTime DataCriacao { get; set; }

        [JsonPropertyName("dt_updatedate")]
        public DateTime DataAtualizacao { get; set; }

        [JsonPropertyName("fl_isenable")]
        public bool Ativo { get; set; }
    }
}