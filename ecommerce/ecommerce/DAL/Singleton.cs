using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ecommerce.Models;

namespace ecommerce.DAL
{
    public class Singleton
    {
        private static Context contexto;

        private Singleton()
        {

        }
        public static Context GetInstance()
        {
            if (contexto == null)
                contexto = new Context();

            return contexto;
        }
    }
}
