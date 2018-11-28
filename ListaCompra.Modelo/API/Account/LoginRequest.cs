using System.ComponentModel.DataAnnotations;

namespace ListaCompra.Modelo.API.Account
{
    public class LoginRequest
    {
        public string NomeUsuario { get; set; }

        public string Senha { get; set; }
    }
}