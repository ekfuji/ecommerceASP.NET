using ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ecommerce.DAL;

namespace ecommerce.Controllers
{
    public class CategoriaController : Controller
    {
        
        //identacao CTRL+K+D
        #region View Index
        public ActionResult Index()
        {
            ViewBag.Data = DateTime.Now;
            return View(CategoriaDAL.ReturnCategorias());
        }
        #endregion

        #region Pag Cadastrar Categoria
        public ActionResult CadastrarCategoria()
        {

            return View();
        }
        #endregion

        #region Cadastrando Categoria
        [HttpPost]
        public ActionResult CadastrarCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (CategoriaDAL.CadastrarCategoria(categoria))
                {
                    return RedirectToAction("Index", "Categoria");
                }
                else
                {
                    ModelState.AddModelError("", "Categoria já cadastrada");
                    return View(categoria);
                }
            }
            else
            {
                return View(categoria);
            }

        }
        #endregion

        #region Remover Categoria
        public ActionResult RemoverCategoria(int id)
        {

            CategoriaDAL.RemoverCategoria(id);
            return RedirectToAction("Index", "Categoria");
            //não esquecer de utilizar o RedirectToAction("Nome da página", "Controller") pois ao não utiliza-lo gerará uma excessão de arquivo não encontrado

        }
        #endregion

        #region Buscar Categoria (AlterarCategoria)
        public ActionResult AlterarCategoria(int id)
        {
            return View(CategoriaDAL.BuscarCategoria(id));
        }
        #endregion

        #region Alterando Categoria
        [HttpPost]
        public ActionResult AlterarCategoria(Categoria categoriaAlterada)
        {
            Categoria categoriaOriginal = CategoriaDAL.BuscarCategoria(categoriaAlterada.CategoriaId);
            categoriaOriginal.Nome = categoriaAlterada.Nome;


            // ctx.Entry(categoria).State = System.Data.Entity.EntityState.Modified;
            if (ModelState.IsValid)
            {
                if (CategoriaDAL.AlterarCategoria(categoriaOriginal))
                {
                    return RedirectToAction("Index", "Categoria");
                }
                else
                {
                    ModelState.AddModelError("", "Categoria já cadastrada");
                    return View(categoriaOriginal);
                }
            }
            else
            {
                return View(categoriaOriginal);
            }
        }
        #endregion

    }
}