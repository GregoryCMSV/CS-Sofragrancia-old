using Sofragrancia.Banco.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofragrancia.Banco.Models
{
    [Table("item_pedido")]
    public class ItemPedido : BaseModel, IEntidadeBase
    {   
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("id_pedido")]
        public int? IdPedido { get; set; }

        [Column("id_produto")]
        public int? IdProduto { get; set; }

        [Column("quantidade")]
        public int Quantidade { get; set; }

        [Column("precounitario")]
        public decimal PrecoUnitario { get; set; }

        [Column("desconto")]
        public String Desconto { get; set; }

        [Column("subtotal")]
        public string Subtotal { get; set; }

        [Column("createdate")]
        public DateTime CriadoEm { get; set; }

        [Column("updatedate")]
        public DateTime AtualizadoEm { get; set; }

        [Column("isenable")]
        public bool IsEnable { get; set; }
    }
}
