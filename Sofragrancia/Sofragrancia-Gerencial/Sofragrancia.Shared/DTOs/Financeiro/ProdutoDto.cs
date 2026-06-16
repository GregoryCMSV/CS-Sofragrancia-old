using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class ProdutoDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tx_descricao")]
        public string Descricao { get; set; } = string.Empty;

        [JsonPropertyName("id_fornecedor")]
        public int IdFornecedor { get; set; }

        [JsonPropertyName("tx_categoria")]
        public string? Categoria { get; set; }

        [JsonPropertyName("tx_marca")]
        public string? Marca { get; set; }

        [JsonPropertyName("tx_unidade")]
        public string Unidade { get; set; } = string.Empty;

        [JsonPropertyName("nr_precocusto")]
        public double PrecoCusto { get; set; }

        [JsonPropertyName("nr_precovenda")]
        public double PrecoVenda { get; set; }

        [JsonPropertyName("nr_estoqueatual")]
        public int EstoqueAtual { get; set; }

        [JsonPropertyName("nr_estoqueminimo")]
        public int EstoqueMinimo { get; set; }

        [JsonPropertyName("dt_createdate")]
        public DateTime CriadoEm { get; set; }


        [JsonPropertyName("dt_updatedate")]
        public DateTime AtualizadoEm { get; set; }


        [JsonPropertyName("fl_isenable")]
        public bool IsEnable { get; set; }
        

        [JsonPropertyName("nr_codigo")]
        public int Codigo { get; set; }

        [JsonPropertyName("tx_imagemurl")]
        public string ImagemUrl { get; set; } = string.Empty;
    }
}