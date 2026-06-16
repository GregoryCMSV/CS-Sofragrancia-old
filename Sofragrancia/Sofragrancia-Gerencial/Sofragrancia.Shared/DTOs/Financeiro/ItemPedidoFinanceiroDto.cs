using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class ItemPedidoFinanceiroDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("id_pedido")]
        public int IdPedido { get; set; }

        [JsonPropertyName("id_produto")]
        public int IdProduto { get; set; }

        [JsonPropertyName("nr_quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("nr_precounitario")]
        public decimal PrecoUnitario { get; set; }

        [JsonPropertyName("nr_descontounitario")]
        public double DescontoUnitario { get; set; }

        [JsonPropertyName("nr_subtotal")]
        public double Subtotal { get; set; }

        [JsonPropertyName("dt_createdate")]
        public DateTime DataCriacao { get; set; }

        [JsonPropertyName("dt_updatedate")]
        public DateTime DataAtualizacao { get; set; }

        [JsonPropertyName("fl_isenable")]
        public bool Ativo { get; set; }
    }
}