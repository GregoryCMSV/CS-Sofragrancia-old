using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Sofragrancia.Banco.Models
{
    [Table("meta_vendas")]
    public class MetaVendas : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("id_vendedor")]
        public int IdVendedor { get; set; }

        [Column("id_produto")]
        public int IdProduto { get; set; }

        [Column("mes")]
        public int Mes { get; set; }

        [Column("ano")]
        public int Ano { get; set; }

        [Column("quantidademeta")]
        public int QtdDemanda { get; set; }

        [Column("valormeta")]
        public double ValorMeta { get; set; }

        [Column("quantidaderealizada")]
        public int QtdRealizada { get; set; }

        [Column("valorrealizado")]
        public double ValorRealizado { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }
    }
}
