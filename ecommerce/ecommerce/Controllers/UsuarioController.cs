using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ecommerce.Models;
using ecommerce.DAL;
using System.Web.Security;

namespace ecommerce.Controllers
{

    public class UsuarioController : Controller
    {


        [Authorize]
        public ActionResult Index()
        {
            return View(UsuarioDAO.RetornarUsuarios());
        }

       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "UsuarioId,Nome,Email,Senha,ConfirmacaoSenha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (UsuarioDAO.CadastrarUsuario(usuario))
                {
                    return RedirectToAction("Index", "Usuario");
                }
                ModelState.AddModelError("", "Esse usuário já existe!");
            }

            return View(usuario);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login ([Bind(Include ="Email,Senha")] Usuario usuario)
        {
            usuario = UsuarioDAO.Logar(usuario);

            if(usuario != null)
            {
                //Autenticar
                FormsAuthentication.SetAuthCookie(usuario.Email,true);
                return RedirectToAction("Index","Home");
            }

            ModelState.AddModelError("", "Email ou senha não coincidem!");
            return View(usuario);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
