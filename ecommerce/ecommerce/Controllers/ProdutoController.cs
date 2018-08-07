using ecommerce.DAL;
using ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecommerce.Controllers
{
    public class ProdutoController : Controller
    {
        private static Context ctx = new Context();
        //identacao CTRL+K+D

        #region View Index
        // GET: Produto
        public ActionResult Index()
        {
            ViewBag.Data = DateTime.Now;
            return View(ProdutoDAO.ReturnProdutos());
        }
        #endregion

        #region Pag Cadastrar Produto
        public ActionResult CadastrarProduto()
        {
            return View();
        }
        #endregion

        #region Cadastrando Produto
        [HttpPost]
        public ActionResult CadastrarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (ProdutoDAO.CadastrarProduto(produto))
                {
                    return RedirectToAction("Index", "Produto");
                }
                else
                {
                    ModelState.AddModelError("", "Produto já cadastrado");
                    return View(produto);
                }
            }
            else
            {
                return View(produto);
            }

        }
        #endregion

        #region Remover Produto
        public ActionResult RemoverProduto(int id)
        {

            ProdutoDAO.RemoverProduto(id);
            return RedirectToAction("Index", "Produto");
            //não esquecer de utilizar o RedirectToAction("Nome da página", "Controller") pois ao não utiliza-lo gerará uma excessão de arquivo não encontrado

        }
        #endregion

        #region Buscar Produto (AlterarProduto)
        public ActionResult AlterarProduto(int id)
        {
            return View(ProdutoDAO.BuscarProduto(id));
        }
        #endregion

        #region Alterando Produto
        [HttpPost]
        public ActionResult AlterarProduto(Produto produtoAlterado)
        {
            Produto produtoOriginal = ProdutoDAO.BuscarProduto(produtoAlterado.ProdutoId);
            produtoOriginal.Nome = produtoAlterado.Nome;
            produtoOriginal.Descricao = produtoAlterado.Descricao;
            produtoOriginal.Preco = produtoAlterado.Preco;
            produtoOriginal.Categoria = produtoAlterado.Categoria;

            // ctx.Entry(produto).State = System.Data.Entity.EntityState.Modified;
            if (ModelState.IsValid)
            {
                if (ProdutoDAO.AlterarProduto(produtoOriginal))
                {
                    return RedirectToAction("Index", "Produto");
                }
                else
                {
                    ModelState.AddModelError("", "Produto já cadastrado");
                    return View(produtoOriginal);
                }
            }
            else
            {
                return View(produtoOriginal);
            }
        }
        #endregion

    }
}