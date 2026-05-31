using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Models.Alertas
{
    [Table("alertaheader")]
    public class AlertaHeader : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public string IdUsuario { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("horario")]
        public TimeOnly Horario { get; set; }

        [Column("dias")]
        public int[] Dias { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }

        [Reference(typeof(AlertaConfigUser))]
        public List<AlertaConfigUser> Alertas { get; set; }
    }
}
