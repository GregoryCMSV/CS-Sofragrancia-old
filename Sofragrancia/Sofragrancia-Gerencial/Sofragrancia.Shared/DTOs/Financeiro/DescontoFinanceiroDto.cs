using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class DescontoFinanceiroDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tx_descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("nr_valormin")]
        public decimal ValorMinimo { get; set; }

        [JsonPropertyName("nr_valormax")]
        public decimal ValorMaximo { get; set; }

        [JsonPropertyName("nr_percentual")]
        public decimal Percentual { get; set; }

        [JsonPropertyName("id_cliente")]
        public int? IdCliente { get; set; }

        [JsonPropertyName("id_produto")]
        public int? IdProduto { get; set; }

        [JsonPropertyName("id_vendedor")]
        public int? IdVendedor { get; set; }

        [JsonPropertyName("dt_createdate")]
        public DateTime DataCriacao { get; set; }

        [JsonPropertyName("dt_updatedate")]
        public DateTime DataAtualizacao { get; set; }

        [JsonPropertyName("fl_isenable")]
        public bool Ativo { get; set; }
    }
}