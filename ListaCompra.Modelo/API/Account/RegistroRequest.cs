using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ListaCompra.Modelo.API.Account
{
    public class RegistroRequest
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Senha { get; set; }

        public List<string> Funcoes { get; set; }
    }
}