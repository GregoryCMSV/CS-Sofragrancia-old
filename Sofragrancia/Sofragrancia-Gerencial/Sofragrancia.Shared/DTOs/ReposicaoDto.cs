using System.Text.Json.Serialization;

namespace Sofragrancia.API.DTOs
{
    public class ReposicaoDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("produtoId")]
        public int ProdutoId { get; set; }

        [JsonPropertyName("fornecedorId")]
        public int FornecedorId { get; set; }

        [JsonPropertyName("nrQuantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("nrPrecounitario")]
        public decimal? PrecoUnitario { get; set; }

        [JsonPropertyName("nrDescontounitario")]
        public decimal? DescontoUnitario { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal? Subtotal { get; set; }

        [JsonPropertyName("tipo")]
        public string? Tipo { get; set; }

        [JsonPropertyName("flIsenable")]
        public bool Ativo { get; set; }

        [JsonPropertyName("dtCreatedate")]
        public DateTime DataCriacao { get; set; }
    }
}