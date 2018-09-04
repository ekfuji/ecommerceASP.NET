using ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerce.DAL
{
    public class UsuarioDAO
    {
        private static Context ctx = Singleton.GetInstance();

        public static List<Usuario> RetornarUsuarios()
        {
            return ctx.Usuarios.ToList();
        }

        public static bool CadastrarUsuario(Usuario usuario)
        {
            Usuario usuarioNodb = BuscarPorEmail(usuario);
            if (usuario.Email != usuarioNodb.Email)
            {
                ctx.Usuarios.Add(usuario);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Usuario BuscarPorEmail(Usuario usuario)
        {
            return ctx.Usuarios.FirstOrDefault(x => x.Email.Equals(usuario.Email));
        }

        public static Usuario Logar(Usuario usuario)
        {
            return ctx.Usuarios.FirstOrDefault(x => x.Senha == usuario.Senha && x.Email.Equals(usuario.Email));
 
        }


    }
}