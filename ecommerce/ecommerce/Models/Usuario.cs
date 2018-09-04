using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Nome do Usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "E-mail do usuário!")]
        [EmailAddress(ErrorMessage ="E-mail inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Campo obrigatório!")]
        public string Senha { get; set; }

        [Compare("Senha" , ErrorMessage = "Os campos não coicidem!")]
        [Display(Name ="Confirmação senha")]
        [NotMapped]
        public string ConfirmacaoSenha { get; set; }
    }
}