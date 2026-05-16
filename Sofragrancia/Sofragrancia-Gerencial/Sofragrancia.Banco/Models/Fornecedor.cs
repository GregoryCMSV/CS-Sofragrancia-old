using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Sofragrancia.Banco.Models
{
    [Table("fornecedor")]
    public class Fornecedor : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Name { get; set; }

        [Column("cnpjcpf")]
        public string Cnpj { get; set; }

        [Column("telefone")]
        public string Tel { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("endereco")]
        public string Endereco { get; set; }

        [Column("representante")]
        public string Representante { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }
    }
}
