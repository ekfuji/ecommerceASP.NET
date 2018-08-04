using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class Context : DbContext
    {
        public Context() : base("DbEcommerce") { }

        //Mapear as classes que vão virar tabela no banco
        public DbSet<Produto> Produtos { get; set; }
    }
}