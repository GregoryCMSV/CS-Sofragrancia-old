using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Sofragrancia.Banco.Models
{
    [Table("Vendedor")]
    public class Vendedor : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Name { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("telefone")]
        public string Tel { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("regiao")]
        public string Regiao { get; set; }

        [Column("dataadmissao")]
        public DateTime Admissao { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }



    }
}
