using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class PedidoFinanceiroDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tx_numeropedido")]
        public string NumeroPedido { get; set; }

        [JsonPropertyName("dt_datapedido")]
        public DateTime DataPedido { get; set; }

        [JsonPropertyName("dt_horapedido")]
        public DateTime? HoraPedido { get; set; }

        [JsonPropertyName("id_cliente")]
        public int IdCliente { get; set; }

        [JsonPropertyName("id_vendedor")]
        public int IdVendedor { get; set; }

        [JsonPropertyName("tx_status")]
        public string Status { get; set; }

        [JsonPropertyName("tx_observacao")]
        public string Observacao { get; set; }

        [JsonPropertyName("nr_valorbruto")]
        public double ValorBruto { get; set; }

        [JsonPropertyName("nr_valordesconto")]
        public double ValorDesconto { get; set; }

        [JsonPropertyName("nr_valorliquido")]
        public double ValorLiquido { get; set; }

        [JsonPropertyName("dt_createdate")]
        public DateTime CriadoEm { get; set; }

        [JsonPropertyName("dt_updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [JsonPropertyName("fl_isenable")]
        public bool IsEnable { get; set; }
    }
}