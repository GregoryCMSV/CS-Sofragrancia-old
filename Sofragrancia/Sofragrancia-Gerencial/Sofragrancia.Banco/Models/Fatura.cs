using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using Sofragrancia.Banco.Interfaces;

namespace Sofragrancia.Banco.Models
{
    [Table("fatura")]
    public class Fatura : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [Column("numeronf")]
        public string Serial { get; set; }

        [Column("dataemissao")]
        public DateTime DocDate { get; set; }

        [Column("valorprodutos")]
        public double ValorBruto { get; set; }

        [Column("valorimposto")]
        public double TotalImposto { get; set; }

        [Column("valordesconto")]
        public double TotalDesconto { get; set; }

        [Column("valortotal")]
        public double ValorLiquido { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }

    }
}
