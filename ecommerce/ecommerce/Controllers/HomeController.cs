using ecommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int? id)
        {
            try
            {
            ViewBag.categoriaC = CategoriaDAL.ReturnCategorias();
           if(id == null)
            {
            return View(ProdutoDAO.ReturnProdutos());

            }
            return View(ProdutoDAO.BuscarPorCateg(id));  
            }
            catch (Exception ex)
            {
                
                return View(ex);
            }
    
        }

        [HttpPost]
        public ActionResult BuscarCateg(int? categoriaC)
        {
            ViewBag.categoriaC = new SelectList(CategoriaDAL.ReturnCategorias(), "CategoriaId", "Nome");
            return  View("Index","Home",ProdutoDAO.BuscarPorCateg(categoriaC));
        }
    }
}