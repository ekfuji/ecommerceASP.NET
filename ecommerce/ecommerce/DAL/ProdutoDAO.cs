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
        private static Context ctx = Singleton.GetInstance();

        #region Listar Produtos
        public static List<Produto> ReturnProdutos()
        {
            return ctx.Produtos.Include("Categoria").ToList();
        }
        #endregion

        #region Cadastrar Produto
        public static bool CadastrarProduto(Produto produto)
        {
            if (BuscarProdutoPorNome(produto) == null)
            {
                ctx.Produtos.Add(produto);
                ctx.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion

        #region Buscando produto por nome
        public static Produto BuscarProdutoPorNome(Produto produto)
        {
            return ctx.Produtos.FirstOrDefault(x => x.Nome.Equals(produto.Nome));
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

        #region Buscar produto por Id
        public static Produto BuscarProdutoPorId(int id)
        {
            return ctx.Produtos.Find(id);
        }
        #endregion

        #region Buscar Produto
        public static Produto BuscarProduto(int id)
        {
            return ctx.Produtos.Where(x => x.ProdutoId == id).FirstOrDefault();
        }
        #endregion

        #region Alterar Produto
        public static bool AlterarProduto(Produto produto)
        {
            if (ctx.Produtos.FirstOrDefault(x => x.Nome.Equals(produto.Nome) && x.ProdutoId != produto.ProdutoId) == null)
            {
                ctx.Entry(produto).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        public static List<Produto> BuscarPorCateg(int? id)
        {
            return ctx.Produtos.Include("Categoria").Where(x => x.CategoriaId == id).ToList();
        }
    }
}
