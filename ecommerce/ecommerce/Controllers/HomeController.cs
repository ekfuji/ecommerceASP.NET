using ecommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.Models;
using ecommerce.Util;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {

        private static string guid = Sessao.RetornarCarrinhoId();

        // GET: Home
        #region index
        public ActionResult Index(int? id)
        {
            try
            {
                ViewBag.Validar = false;
                ViewBag.categoriaC = CategoriaDAL.ReturnCategorias();
                if (id == null)
                {
                    return View(ProdutoDAO.ReturnProdutos());

                }
                var categ = ProdutoDAO.BuscarPorCateg(id);
                if (categ.Count.Equals(0))
                {
                    ViewBag.Validar = true;
                    return View(categ);
                }
                return View(categ);
            }
            catch (Exception ex)
            {

                return View(ex);
            }

        }
        #endregion

        #region Detalhes do produto
        public ActionResult DetalheProduto(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ProdutoDAO.BuscarProduto(id) == null)
            {
                return HttpNotFound();
            }
            return View(ProdutoDAO.BuscarProdutoPorId(id));
        }
        #endregion

        #region Adicionar ao carrinho
        public ActionResult AdicionarAoCarrinho(int id)
        {


            Produto produto = ProdutoDAO.BuscarProdutoPorId(id);
            ItemVenda item = ItemVendaDAO.BuscarByProd(id);
            if (item == null)
            {

                ItemVenda itemVenda = new ItemVenda
                {
                    Produto = produto,
                    Quantidade = 1,
                    Valor = produto.Preco,
                    Data = DateTime.Now,
                    CarrinhoId = Sessao.RetornarCarrinhoId()
                };
                ItemVendaDAO.CadastrarItem(itemVenda);
            }
            else
            {
                item.Quantidade++;
            }


            return RedirectToAction("CarrinhoCompras", "Home");
        }
        #endregion

        #region Listar vendas
        public ActionResult CarrinhoCompras()
        {

            return View(ItemVendaDAO.ListarVendaByGuid(Sessao.RetornarCarrinhoId()));
        }
        #endregion

        #region Remover do carrinho

        public ActionResult RemoverQtde(int id)
        {
            ItemVenda item = ItemVendaDAO.BuscarByProd(id);

            if (item.Quantidade != 1)
            {
                item.Quantidade--;
            }

            else
            {
                item.Quantidade--;
                ItemVendaDAO.RemoverItemVenda(id);
            }

            return RedirectToAction("CarrinhoCompras", "Home");
        }

        public ActionResult AddQtde(int id)
        {
            ItemVenda item = ItemVendaDAO.BuscarByProd(id);

            item.Quantidade++;

            return RedirectToAction("CarrinhoCompras", "Home");
        }



        #endregion

        #region Alterar quantidade

        public ActionResult AlterarQuantidadeDeProdutosCarrinho(ItemVenda itemAlterado)
        {
            ItemVenda itemOriginal = ItemVendaDAO.BuscarById(itemAlterado.ItemVendaId);
            itemOriginal.Quantidade = itemAlterado.Quantidade;
            itemOriginal.Valor = itemOriginal.Valor * itemAlterado.Quantidade;
            ItemVendaDAO.AlterarItemVenda(itemOriginal);
            return RedirectToAction("CarrinhoCompras", "Home");
        }

        #endregion
    }
}