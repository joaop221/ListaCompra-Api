using System;

namespace ListaCompra.Modelo.Base
{
    /// <summary>
    /// interface para entidades
    /// </summary>
    public interface IEntidade
    {
        ///<summary>
        ///Login do usuário que efetuou a inclusão
        ///</summary>
        string LoginInclusao { get; set; }

        ///<summary>
        ///Data de inclusão do registro
        ///</summary>
        DateTime DataInclusao { get; set; }

        ///<summary>
        ///Login do usuário que efetuou a alteração
        ///</summary>
        string LoginAlteracao { get; set; }

        ///<summary>
        ///Data de alteração do registro
        ///</summary>
        DateTime? DataAlteracao { get; set; }

        ///<summary>
        ///Flag que identifica se o registro está excluído lógicamente
        ///</summary>
        bool Excluido { get; set; }
    }
}