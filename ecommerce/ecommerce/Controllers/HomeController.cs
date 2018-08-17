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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
            if (ProdutoDAO.BuscarProduto(id) == null)
            {
                return HttpNotFound();
            }
                return View(ProdutoDAO.BuscarProdutoPorId(id));
        }



    }
}