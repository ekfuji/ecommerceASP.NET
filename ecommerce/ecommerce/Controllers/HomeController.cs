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
        // GET: Home
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

        public ActionResult AdicionarAoCarrinho(int id)
        {
            Produto produto = ProdutoDAO.BuscarProdutoPorId(id);
            ItemVenda itemVenda = new ItemVenda
            {
                Produto = produto,
                Quantidade = 1,
                Valor = produto.Preco,
                Data = DateTime.Now
            };
            ItemVendaDAO.CadastrarItem(itemVenda);
            return RedirectToAction("CarrinhoCompras","Home");
        }

        public ActionResult CarrinhoCompras()
        {
            return View(ItemVendaDAO.ListarVenda());
        }

    }
}