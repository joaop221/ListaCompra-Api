using System.ComponentModel.DataAnnotations;

namespace ListaCompra.Modelo.API.Account
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}