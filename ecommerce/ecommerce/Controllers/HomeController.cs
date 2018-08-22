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
            string CarrinhoId = Sessao.RetornarCarrinhoId();
            ItemVenda item = ItemVendaDAO.BuscarPeloGuidCar(CarrinhoId);

            if (item != null)
            {
                ItemVenda itemVendaProd = ItemVendaDAO.BuscarByProduto(produto.ProdutoId, CarrinhoId);

                if (itemVendaProd != null)
                {
                    if(itemVendaProd.Produto.ProdutoId == produto.ProdutoId)
                    {
                    bool add = true;
                    ItemVendaDAO.AlteraQuantidade(itemVendaProd, add);
                    }
                }
                else if (item.CarrinhoId == null)
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
            }
            else
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
            string CarrinhoId = Sessao.RetornarCarrinhoId();
            ItemVenda item = ItemVendaDAO.BuscarPeloGuidCar(CarrinhoId);
            ItemVenda itemVendaProd = ItemVendaDAO.BuscarByProduto(item.Produto.ProdutoId, CarrinhoId);
            bool remove = false;
            if (itemVendaProd.Quantidade != 1)
            {
                ItemVendaDAO.AlteraQuantidade(itemVendaProd, remove);
            }
            else
            {
                ItemVendaDAO.AlteraQuantidade(itemVendaProd, remove);
                ItemVendaDAO.RemoverItemVenda(itemVendaProd.ItemVendaId);
            }

            return RedirectToAction("CarrinhoCompras", "Home");
        }
        #endregion

        #region Adicionar Itens no Carrinho
        public ActionResult AddQtde(int id)
        {
            string CarrinhoId = Sessao.RetornarCarrinhoId();
            ItemVenda item = ItemVendaDAO.BuscarPeloGuidCar(CarrinhoId);
            ItemVenda itemVendaProd = ItemVendaDAO.BuscarByProduto(item.Produto.ProdutoId, CarrinhoId);
            bool add = true;
            ItemVendaDAO.AlteraQuantidade(item, add);
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