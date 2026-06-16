using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class FornecedorFinanceiroDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tx_nome")]
        public string Nome { get; set; }

        [JsonPropertyName("tx_cnpjcpf")]
        public string CnpjCpf { get; set; }

        [JsonPropertyName("tx_telefone")]
        public string Telefone { get; set; }

        [JsonPropertyName("tx_email")]
        public string Email { get; set; }

        [JsonPropertyName("tx_endereco")]
        public string? Endereco { get; set; }

        [JsonPropertyName("tx_representante")]
        public string Representante { get; set; }

        [JsonPropertyName("dt_createdate")]
        public DateTime DataCriacao { get; set; }

        [JsonPropertyName("dt_updatedate")]
        public DateTime DataAtualizacao { get; set; }

        [JsonPropertyName("fl_isenable")]
        public bool Ativo { get; set; }
    }
}