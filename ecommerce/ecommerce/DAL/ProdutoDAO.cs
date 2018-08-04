using ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ecommerce.DAL
{
    public class ProdutoDAO
    {
        private static Context ctx = new Context();

        #region Listar Produtos
        public static List<Produto> ReturnProdutos()
        {
            return ctx.Produtos.ToList();
        }
        #endregion

        #region Cadastrar Produto
        public static void CadastrarProduto(Produto produto)
        {
            ctx.Produtos.Add(produto);
            ctx.SaveChanges();

        }
        #endregion

        #region Remover Produto
        public static void RemoverProduto(int id)
        {
            Produto produto = new Produto();
            produto = BuscarProduto(id);
            ctx.Produtos.Remove(produto);
            ctx.SaveChanges();
        }
        #endregion

        #region Buscar Produto
        public static Produto BuscarProduto(int id)
        {
            return ctx.Produtos.Where(x => x.ProdutoId == id).FirstOrDefault();
        }
        #endregion

        #region Alterar Produto
        public static void AlterarProduto(Produto produto)
        {
            ctx.Entry(produto).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        #endregion

    }
}
