using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Sofragrancia.Banco.Models
{
    [Table("produto")]
    public class Produto : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("id_fornecedor")]
        public int IdFornecedor { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("marca")]
        public string Marca { get; set; }

        [Column("unidade")]
        public string unidade { get; set; }

        [Column("precocusto")]
        public string PrecoCusto { get; set; }

        [Column("precovenda")]
        public string PrecoVenda { get; set; }

        [Column("estoqueatual")]
        public string EstoqueAtual { get; set; }

        [Column("estoqueminimo")]
        public string EstoqueMinimo { get; set; }

        [Column("categoria")]
        public string Categoria { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }
    }
}
