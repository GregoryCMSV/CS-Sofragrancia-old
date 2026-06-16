using System.Text.Json.Serialization;

public class ProdutoFinanceiroDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }


    [JsonPropertyName("tx_descricao")]
    public string Descricao { get; set; }


    [JsonPropertyName("id_fornecedor")]
    public int IdFornecedor { get; set; }


    [JsonPropertyName("tx_categoria")]
    public string Categoria { get; set; }


    [JsonPropertyName("tx_marca")]
    public string Marca { get; set; }


    [JsonPropertyName("tx_unidade")]
    public string Unidade { get; set; }


    [JsonPropertyName("nr_precocusto")]
    public decimal PrecoCusto { get; set; }


    [JsonPropertyName("nr_precovenda")]
    public decimal PrecoVenda { get; set; }


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
}