using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    [Table("ItemVenda")]
    public class ItemVenda
    {
        [Key]
        public int ItemVendaId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Nome do produto")]
        public Produto Produto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Valor do produto")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }
    }
}