using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ListaCompra.Infraestrutura.Tratamento
{
    public class RegistroExcecao : Exception
    {
        public RegistroExcecao(IEnumerable<IdentityError> erros)
        {
            this.Erros = erros.ToList();
        }

        public List<IdentityError> Erros { get; set; }
    }
}