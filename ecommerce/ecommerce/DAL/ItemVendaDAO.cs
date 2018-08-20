using ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecommerce.DAL
{
    public class ItemVendaDAO
    {
        private static Context ctx = Singleton.GetInstance();

        #region Add no Carrinho
        public static bool CadastrarItem(ItemVenda item)
        {
            if(item.Produto != null)
            {
                ctx.ItemVendas.Add(item);
                ctx.SaveChanges();
                return true;
            }
                return false;
        }
        #endregion

        #region Listar Itens Venda
        public static List<ItemVenda> ListarVenda()
        {
            return ctx.ItemVendas.ToList();
        }
        #endregion

        #region Remover ItemVenda
        public static void RemoverItemVenda(int id)
        {
            ItemVenda itemVenda = new ItemVenda();          
               itemVenda = BuscarById(id);
                ctx.ItemVendas.Remove(itemVenda);
                ctx.SaveChanges();

        }
        #endregion

        #region Buscar ItemVenda
        public static ItemVenda BuscarById(int id)
        {
            return ctx.ItemVendas.FirstOrDefault(x => x.ItemVendaId == id);
        }
        #endregion

        #region Alterar ItemVenda
        public static void AlterarItemVenda(ItemVenda itemVenda)
        {
                ctx.Entry(itemVenda).State = EntityState.Modified;
                ctx.SaveChanges();
        }
        #endregion

    }
}