using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        public Pedido()
        {
            this.ItemsVenda = new HashSet<ItemVenda>().ToList();
        }
        [Key]
        public int PedidoId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Data { get; set; } = DateTime.Now;

        public decimal ValorTotal { get; set; }

        public List<ItemVenda> ItemsVenda { get; set; }
    }
}