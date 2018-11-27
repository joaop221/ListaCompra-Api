using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ListaCompra.Modelo.API.Account
{
    public class RegistroRequest
    {
        public string Nome { get; set; }

        public string Email { get; set; }


        public string Senha { get; set; }

        public List<string> Funcoes { get; set; }
    }
}