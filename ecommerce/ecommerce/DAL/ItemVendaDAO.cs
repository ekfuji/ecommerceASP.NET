using ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        #region Listar Venda
        public static List<ItemVenda> ListarVenda()
        {
            return ctx.ItemVendas.ToList();
        }
        #endregion

        #region Remover ItemVenda
        public static bool RemoverItemVenda(int id)
        {
            ItemVenda itemVenda = BuscarById(id);
            if (itemVenda.ItemVendaId != 0)
            {
                ctx.ItemVendas.Remove(itemVenda);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region Buscar ItemVenda
        public static ItemVenda BuscarById(int id)
        {
            return ctx.ItemVendas.FirstOrDefault(x => x.ItemVendaId == id);
        }
        #endregion

    }
}