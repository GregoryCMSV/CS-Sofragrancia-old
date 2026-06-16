using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class MetaVendaFinanceiroDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("id_produto")]
        public int IdProduto { get; set; }

        [JsonPropertyName("id_vendedor")]
        public int IdVendedor { get; set; }

        [JsonPropertyName("ano")]
        public int Ano { get; set; }

        [JsonPropertyName("mes")]
        public string Mes { get; set; }

        [JsonPropertyName("mes_ano")]
        public string MesAno { get; set; }

        [JsonPropertyName("valor_meta")]
        public decimal ValorMeta { get; set; }

        [JsonPropertyName("quantidade_meta")]
        public int QuantidadeMeta { get; set; }

        [JsonPropertyName("quantidade_realizada")]
        public int QuantidadeRealizada { get; set; }

        [JsonPropertyName("valor_realizado")]
        public decimal ValorRealizado { get; set; }

        [JsonPropertyName("createdate")]
        public DateTime DataCriacao { get; set; }

        [JsonPropertyName("updatedate")]
        public DateTime DataAtualizacao { get; set; }

        [JsonPropertyName("isenable")]
        public bool Ativo { get; set; }
    }
}