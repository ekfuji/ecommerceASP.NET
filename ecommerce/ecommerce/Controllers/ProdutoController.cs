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
        //identacao CTRL+K+D

        #region View Index
        // GET: Produto
        public ActionResult Index()
        {
            ViewBag.Data = DateTime.Now;
            ViewBag.Produtos = ProdutoDAO.ReturnProdutos();
            return View();
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
        public ActionResult CadastrarProduto(string txtNome, string txtDescricao,
            string txtPreco, string txtCategoria)
        {
            Produto produto = new Produto
            {
                Nome = txtNome,
                Descricao = txtDescricao,
                Preco = Convert.ToDouble(txtPreco),
                Categoria = txtCategoria
            };

            ProdutoDAO.CadastrarProduto(produto);

            return RedirectToAction("Index", "Produto");
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
            ViewBag.Produto = ProdutoDAO.BuscarProduto(id);
            return View();
        }
        #endregion

        #region Alterando Produto
        [HttpPost]
        public ActionResult AlterarProduto(int txtid, string txtNome, string txtDescricao, string txtPreco, string txtCategoria)
        {
            Produto produto = ProdutoDAO.BuscarProduto(txtid);
            produto.Nome = txtNome;
            produto.Descricao = txtDescricao;
            produto.Preco = Convert.ToDouble(txtPreco);
            produto.Categoria = txtCategoria;

            ProdutoDAO.AlterarProduto(produto);

            // ctx.Entry(produto).State = System.Data.Entity.EntityState.Modified;


            return RedirectToAction("Index", "Produto");
        }
        #endregion

    }
}