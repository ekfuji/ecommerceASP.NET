using ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ecommerce.DAL
{
    public class CategoriaDAL
    {
        private static Context ctx = Singleton.GetInstance();

        #region Listar Categorias
        public static List<Categoria> ReturnCategorias()
        {
            return ctx.Categorias.ToList();
        }
        #endregion

        #region Cadastrar Categoria
        public static bool CadastrarCategoria(Categoria categoria)
        {
            if (BuscarCategoriaPorNome(categoria) == null)
            {
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion

        #region Buscando categoria por nome
        public static Categoria BuscarCategoriaPorNome(Categoria categoria)
        {
            return ctx.Categorias.FirstOrDefault(x => x.Nome.Equals(categoria.Nome));
        }
        #endregion

        #region Remover Categoria
        public static void RemoverCategoria(int id)
        {
            Categoria categoria = new Categoria();
            categoria = BuscarCategoria(id);
            ctx.Categorias.Remove(categoria);
            ctx.SaveChanges();
        }
        #endregion

        #region Buscar Categoria
        public static Categoria BuscarCategoria(int id)
        {
            return ctx.Categorias.Where(x => x.CategoriaId == id).FirstOrDefault();
        }
        #endregion

        #region Alterar Categoria
        public static bool AlterarCategoria(Categoria categoria)
        {
            if (ctx.Categorias.FirstOrDefault(x => x.Nome.Equals(categoria.Nome) && x.CategoriaId != categoria.CategoriaId) == null)
            {
                ctx.Entry(categoria).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion
    }
}