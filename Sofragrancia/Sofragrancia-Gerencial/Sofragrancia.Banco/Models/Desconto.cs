using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Models
{
    [Table("desconto")]
    public class Desconto : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("valormin")]
        public double ValorMin { get; set; }

        [Column("valormax")]
        public double ValorMax { get; set; }

        [Column("percentual")]
        public double Percentual { get; set; }

        [Column("id_cliente")]
        public int? IdCliente { get; set; }

        [Column("id_produto")]
        public int? IdProduto { get; set; }

        [Column("id_vendedor")]
        public int? IdVendedor { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }
    }
}
