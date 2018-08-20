using ecommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.Models;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        Context ctx = new Context();
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
            ItemVenda itemVenda = new ItemVenda
            {
                Produto = produto,
                Quantidade = 1,
                Valor = produto.Preco,
            };
            ItemVendaDAO.CadastrarItem(itemVenda);
            return RedirectToAction("CarrinhoCompras", "Home");
        }
        #endregion

        #region Listar vendas
        public ActionResult CarrinhoCompras()
        {
            return View(ItemVendaDAO.ListarVenda());
        }
        #endregion

        #region Remover do carrinho

        public ActionResult RemoverDoCarrinho(int id)
        {
            ItemVendaDAO.RemoverItemVenda(id);
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