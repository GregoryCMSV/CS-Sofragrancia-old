using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class FaturaFinanceiroDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("id_pedido")]
        public int IdPedido { get; set; }

        [JsonPropertyName("tx_numeronf")]
        public string NumeroNotaFiscal { get; set; }

        [JsonPropertyName("dt_dataemissao")]
        public DateTime DataEmissao { get; set; }

        [JsonPropertyName("nr_valorprodutos")]
        public decimal ValorProdutos { get; set; }

        [JsonPropertyName("nr_valorimposto")]
        public decimal? ValorImposto { get; set; }

        [JsonPropertyName("nr_valordesconto")]
        public decimal? ValorDesconto { get; set; }

        [JsonPropertyName("nr_valortotal")]
        public decimal ValorTotal { get; set; }

        [JsonPropertyName("tx_status")]
        public string Status { get; set; }

        [JsonPropertyName("dt_createdate")]
        public DateTime DataCriacao { get; set; }

        [JsonPropertyName("dt_updatedate")]
        public DateTime DataAtualizacao { get; set; }

        [JsonPropertyName("fl_isenable")]
        public bool Ativo { get; set; }
    }
}