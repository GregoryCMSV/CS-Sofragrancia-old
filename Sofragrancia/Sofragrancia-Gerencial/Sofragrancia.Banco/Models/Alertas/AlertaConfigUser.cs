using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Models.Alertas
{
    [Table("alertaconfigurado")]
    public class AlertaConfigUser : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("id_header")]
        public int IdHeader { get; set; }

        [Column("id_alertabase")]
        public int IdAlertaBase { get; set; }

        [Reference(typeof(AlertaBase))]
        public AlertaBase AlertaBaseDados { get; set; }

        [Column("gatilho")]
        public int Trigger { get; set; }

        [Column("valor")]
        public double Value { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }
    }
}
