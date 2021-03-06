﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ecommerce.Models;

namespace ecommerce.DAL
{
    public class PedidoDAO
    {
        private static Context ctx = Singleton.GetInstance();

        #region Salvar Venda 
        public static bool SalvarVenda(Pedido novoPedido)
        {
            if (novoPedido.ItemsVenda != null)
            {
                ctx.Pedidos.Add(novoPedido);
                ctx.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion


    }
}