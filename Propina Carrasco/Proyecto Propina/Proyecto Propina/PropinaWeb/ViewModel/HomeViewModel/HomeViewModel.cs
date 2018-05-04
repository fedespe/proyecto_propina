using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropinaWeb.ViewModel.HomeViewModel
{
    public class HomeViewModel
    {
        private MensajePrincipalBL mensajeBL = new MensajePrincipalBL();

        public List<MensajePrincipal> Mensajes { get; set; }

        public HomeViewModel() {
            Mensajes = new List<MensajePrincipal>();
            Mensajes = mensajeBL.obtenerTodos();
        }

        
    }
}