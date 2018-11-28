using System;
using System.Collections.Generic;
using System.Text;

namespace ListaCompra.Dado.EF.Core
{
    /// <summary>Campos padrões do banco de dados (Usado para colunas de auditoria ou exclusão lógica)</summary>
    public class CampoDB
    {
        public const string UsuarioInclusao = "LoginInclusao";
        public const string DataInclusao = "DataInclusao";
        public const string UsuarioAlteracao = "LoginAlteracao";
        public const string DataAlteracao = "DataAlteracao";
        public const string FlagExclusao = "Excluido";
    }
}
