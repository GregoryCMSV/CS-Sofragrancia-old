using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


namespace Sofragrancia.Banco.Models
{
    [Table("financeiro")]
    public class Financeiro : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("id_fatura")]
        public int IdFatura { get; set; }

        [Column("datavencimento")]
        public DateTime Vencimento { get; set; }

        [Column("datapagamento")]
        public DateTime DocDueDate { get; set; }

        [Column("valororiginal")]
        public double TotalLiquidoPedido { get; set; }

        [Column("juros")]
        public double TotalJuros { get; set; }

        [Column("multa")]
        public double Multa { get; set; }

        [Column("desconto")]
        public double TotalDesconto { get; set; }

        [Column("valorpago")]
        public double? ValorPago { get; set; }

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
