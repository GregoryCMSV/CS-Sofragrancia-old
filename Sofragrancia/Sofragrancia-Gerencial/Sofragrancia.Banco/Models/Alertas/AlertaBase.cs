using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Models.Alertas
{
    [Table("alertabase")]
    public class AlertaBase : BaseModel, IEntidadeBase
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("nome_alerta")]
        public string Name { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("gatilho")]
        public int Trigger { get; set; }

        [Column("gatilhos_permitidos")]
        public int[] ValidTrigger { get; set; }

        [Column("unidade_medida")]
        public int UnidadeMedida { get; set; }

        [Column("unidade_medida_permitidas")]
        public int[] UnidadeMedidaValidas { get; set; }

        [Column("valor")]
        public double Value { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }
    }
}
