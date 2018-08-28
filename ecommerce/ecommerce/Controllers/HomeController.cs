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
        int qtde = 0;
      
        // GET: Home
        #region index
        public ActionResult Index(int? id)
        {
            try
            {
                QtdeTotalCarrinho();
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
            
            ViewBag.Total = 0;
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
            ValorCar();
            QtdeTotalCarrinho();
            return RedirectToAction("CarrinhoCompras", "Home",ViewBag.Total);
        }
        #endregion

        #region Listar vendas
        public ActionResult CarrinhoCompras()
        {
            ValorCar();
            QtdeTotalCarrinho();
            return View(ItemVendaDAO.ListarVendaByGuid(Sessao.RetornarCarrinhoId()));
        }
        #endregion

        #region Remover do carrinho

        public ActionResult RemoverQtde(int id)
        {
            string CarrinhoId = Sessao.RetornarCarrinhoId();
            ItemVenda item = ItemVendaDAO.BuscarPeloGuidCar(CarrinhoId);
            Produto produto = ProdutoDAO.BuscarProdutoPorId(id);
            ItemVenda itemVendaProd = ItemVendaDAO.BuscarByProduto(produto.ProdutoId, CarrinhoId);
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

            QtdeTotalCarrinho();
            ValorCar();
            return RedirectToAction("CarrinhoCompras", "Home");
        }
        #endregion

        #region Adicionar Itens no Carrinho
        public ActionResult AddQtde(int id)
        {
            string CarrinhoId = Sessao.RetornarCarrinhoId();
            ItemVenda item = ItemVendaDAO.BuscarPeloGuidCar(CarrinhoId);
            Produto produto = ProdutoDAO.BuscarProdutoPorId(id);
            ItemVenda itemVendaProd = ItemVendaDAO.BuscarByProduto(produto.ProdutoId, CarrinhoId);
            bool add = true;
            ItemVendaDAO.AlteraQuantidade(itemVendaProd, add);
            ValorCar();
            QtdeTotalCarrinho();
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
            ValorCar();
            QtdeTotalCarrinho();
            return RedirectToAction("CarrinhoCompras", "Home");
        }

        #endregion

        #region Valo Total no Carrinho
        public ActionResult ValorCar()
        {
            string CarrinhoId = Sessao.RetornarCarrinhoId();
            TempData["ValorTotal"] = ItemVendaDAO.TotalCarrinho(CarrinhoId);
            return RedirectToAction("CarrinhoCompras", "Home");
        }
        #endregion

        #region Quantidade de Produtos no Carrinho
        public void QtdeTotalCarrinho()
        {
            string CarrinhoId = Sessao.RetornarCarrinhoId();
            TempData["QtdeTotal"] = ItemVendaDAO.QtdeCarinho(CarrinhoId);
        }
        #endregion

        #region Pag Resumo da Compra
        public ActionResult FinalizarCarrinho()
        {
            QtdeTotalCarrinho();
            ValorCar();
            ViewBag.ListarVendas = ItemVendaDAO.ListarVendaByGuid(Sessao.RetornarCarrinhoId());
            return View();
        }
        #endregion

        #region Finalizar a Compra
        public ActionResult Finalizar(Pedido pedido)
        {
            pedido = new Pedido
            {
                ItemsVenda = ItemVendaDAO.ListarVendaByGuid(Sessao.RetornarCarrinhoId()),
                CarrinhoId = Sessao.RetornarCarrinhoId(),
                ValorTotal = ItemVendaDAO.TotalCarrinho(Sessao.RetornarCarrinhoId()),
                NomeCliente = pedido.NomeCliente,
                EnderecoCliente = pedido.EnderecoCliente,
                TelefoneCliente = pedido.TelefoneCliente
            };

            if(pedido != null)
            {
                PedidoDAO.SalvarVenda(pedido);
                Sessao.CriarSessao();
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("FinalizarCompra", "Home");
        }
        #endregion
    }
}