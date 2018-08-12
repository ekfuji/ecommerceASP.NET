using ecommerce.DAL;
using ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            ViewBag.CategoriaId = new SelectList(ctx.Categorias, "CategoriaId", "Nome");
            return View();
        }
        #endregion

        #region Cadastrando Produto
        [HttpPost]
        public ActionResult CadastrarProduto([Bind(Include = "ProdutoId,Nome,Descricao,Preco,CategoriaId")] HttpPostedFileBase fupImagem, Produto produto)
        {
            ViewBag.CategoriaId = new SelectList(ctx.Categorias, "CategoriaId", "Nome");
            if (ModelState.IsValid)
            {
                if (fupImagem != null)
                {
                    string nomeImagem = Path.GetFileName(fupImagem.FileName);
                    string caminho = Path.Combine(Server.MapPath("~/Images/"), fupImagem.FileName + "jpg");

                    fupImagem.SaveAs(caminho);

                    produto.Imagem = nomeImagem;
                }
                else
                {
                    produto.Imagem = "semimagem.jpg";
                }

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
            ViewBag.CategoriaId = new SelectList(ctx.Categorias, "CategoriaId", "Nome");
            return View(ProdutoDAO.BuscarProduto(id));
        }
        #endregion

        #region Alterando Produto
        [HttpPost]
        public ActionResult AlterarProduto(Produto produtoAlterado, HttpPostedFileBase fupImagem)
        {
            Produto produtoOriginal = ProdutoDAO.BuscarProduto(produtoAlterado.ProdutoId);
            produtoOriginal.Nome = produtoAlterado.Nome;
            produtoOriginal.Descricao = produtoAlterado.Descricao;
            produtoOriginal.Preco = produtoAlterado.Preco;
            produtoOriginal.CategoriaId = produtoAlterado.CategoriaId;

            // ctx.Entry(produto).State = System.Data.Entity.EntityState.Modified;
            if (ModelState.IsValid)
            {
                if (fupImagem != null)
                {
                    string nomeImagem = Path.GetFileName(fupImagem.FileName);
                    string caminho = Path.Combine(Server.MapPath("~/Images/"), fupImagem.FileName + "jpg");

                    fupImagem.SaveAs(caminho);

                    produtoOriginal.Imagem = nomeImagem;
                }
                else
                {
                    produtoOriginal.Imagem = "semimagem.jpg";
                }
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