using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ListaCompra.Modelo.Interfaces;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Entidade base
    /// </summary>
    public class Entidade : IEntidade
    {
        ///<summary>
        ///Login do usuário que efetuou a inclusão
        ///</summary>
        [Required(ErrorMessage = "Campo obrigatório.")]
        [MaxLength(200)]
        [Column("LoginInclusao", Order = 9990)]
        public string LoginInclusao { get; set; }

        ///<summary>
        ///Data de inclusão do registro
        ///</summary>
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Column("DataInclusao", Order = 9991)]
        public DateTime DataInclusao { get; set; }

        ///<summary>
        ///Login do usuário que efetuou a alteração
        ///</summary>
        [MaxLength(200)]
        [Column("LoginAlteracao", Order = 9992)]
        public string LoginAlteracao { get; set; }

        ///<summary>
        ///Data de alteração do registro
        ///</summary>

        [Column("DataAlteracao", Order = 9993)]
        public DateTime? DataAlteracao { get; set; }

        ///<summary>
        ///Flag que identifica se o registro está excluído lógicamente
        ///</summary>
        [Required(ErrorMessage = "Campo obrigatório.")]
        [Column("Excluido", Order = 9994)]
        public bool Excluido { get; set; }
    }
}