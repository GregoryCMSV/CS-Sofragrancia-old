using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


namespace Sofragrancia.Banco.Models
{
    [Table("cliente")]
    public class Cliente : BaseModel , IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("razaosocial")]
        public string Name { get; set; }

        [Column("cnpj")]
        public string Cnpj { get; set; }

        [Column("telefone")]
        public string telefone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("endereco")]
        public string Endereco { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [Column("limitecredito")]
        public double Limitecredito { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("tipo")]
        public int TipoCliente { get; set; }
    }
}
