using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(50, ErrorMessage = "O campo deve ter no máximo de 50 caracteres!")]
        [Display(Name = "Nome do produto")]
        public string Nome { get; set; }

        [Display(Name = "Descrição do produto")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Valor do produto")]
        public double Preco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Categoria do produto")]
        public int CategoriaId { get; set; }

        public string Imagem { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}