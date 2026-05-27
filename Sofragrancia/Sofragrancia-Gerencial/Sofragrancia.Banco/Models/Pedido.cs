using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;    
using Supabase.Postgrest.Models;

namespace Sofragrancia.Banco.Models
{
    [Table("pedido")]
    public class Pedido : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("numeropedido")]
        public string CodPedido { get; set; }

        [Column("datapedido")]
        public DateTime DocDate { get; set; }

        [Column("horapedido")]
        public DateTime DocHour { get; set; }

        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("id_vendedor")]
        public int IdVendedor { get; set; }

        [Column("observacao")]
        public string Observacao { get; set; }

        [Column("valorbruto")]
        public double ValorBruto { get; set; }

        [Column("valordesconto")]
        public double ValorDesconto { get; set; }

        [Column("valorliquido")]
        public double ValorLiquido { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }

        [Reference(typeof(ItemPedido))]
        public List<ItemPedido> Itens { get; set; }
    }
}
