using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [MaxLength(50, ErrorMessage = "O campo deve ter no máximo de 50 caracteres!")]
        [Display(Name = "Nome da Categoria")]
        public string Nome { get; set; }

        public virtual IEnumerable<Produto> Produtos { get; set; }
    }
}