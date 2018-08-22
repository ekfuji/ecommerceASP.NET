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
            if (item.Produto != null)
            {
                ctx.ItemVendas.Add(item);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region Listar Itens Venda
        public static List<ItemVenda> ListarVendaByGuid(string carrinhoGuid)
        {
            return ctx.ItemVendas.Include("Produto").Where(e => e.CarrinhoId.Equals(carrinhoGuid)).ToList();
        }
        #endregion

        #region Buscar ItemVenda pelo Guid do Carrinho
        public static ItemVenda BuscarPeloGuidCar(string CarrinhoId)
        {
            return ctx.ItemVendas.Where(e => e.CarrinhoId == CarrinhoId).FirstOrDefault();

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

        #region Alterar Quantidade
        public static void AlteraQuantidade(ItemVenda item, bool qtde)
        {
            if (qtde)
            {
                item.Quantidade++;
            }
            else
            {
                item.Quantidade--;
            }
            ctx.Entry(item).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        #endregion

        #region Buscar Item pelo Id do Produto
        public static ItemVenda BuscarByProduto(int id , string carrinhoId)
        {
            return ctx.ItemVendas.Include("Produto").Where(e => e.Produto.ProdutoId == id && e.CarrinhoId == carrinhoId).FirstOrDefault();
        }
        #endregion

    }
}