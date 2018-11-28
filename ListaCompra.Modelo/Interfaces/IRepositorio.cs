using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ListaCompra.Modelo.Entidades;

namespace ListaCompra.Modelo.Interfaces
{
    /// <summary>
    /// Interface base para repositorios
    /// </summary>
    public interface IRepositorio<TEntidade> where TEntidade : Entidade
    {
        #region [ Métodos de Inclusão ]

        /// <summary>
        /// Inclui o item no banco
        /// </summary>
        /// <returns>Objeto</returns>
        void Inserir(TEntidade item, int? setTimeoutTo = null, int reattempts = 0);

        /// <summary>
        /// Efetua a inclusão da lista enviada
        /// </summary>
        /// <param name="itens">       </param>
        /// <param name="setTimeoutTo"></param>
        /// <param name="reattempts">  </param>
        /// <returns>Objeto</returns>
        void Inserir(List<TEntidade> itens, int? setTimeoutTo = null, int reattempts = 0);

        #endregion [ Métodos de Inclusão ]

        #region [ Atualizar ]

        /// <summary>
        /// Efetua a atualização do objeto informado
        /// </summary>
        /// <param name="item">          </param>
        /// <param name="exclusaoLogica"></param>
        /// <param name="reattempts">    </param>
        /// <returns>Objeto</returns>
        void Atualizar(TEntidade item, bool exclusaoLogica = false, int reattempts = 0);

        /// <summary>
        /// Atualiza a lista de objetos informados
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para ser atualizado</typeparam>
        /// <param name="itens">         Lista de entidades</param>
        /// <param name="exclusaoLogica"></param>
        /// <param name="reattempts">    </param>
        /// <returns></returns>
        void Atualizar(List<TEntidade> itens, bool exclusaoLogica = false, int reattempts = 0);

        #endregion [ Atualizar ]

        #region [ Métodos para Excluir o item ]

        /// <summary>
        /// Exclui o objeto solicitado
        /// </summary>
        /// <param name="item">          </param>
        /// <param name="exclusaoFisica"></param>
        /// <param name="reattempts">    </param>
        /// <returns>Objeto</returns>
        void Excluir(TEntidade item, bool exclusaoFisica = false, int reattempts = 0);

        /// <summary>
        /// Exclui a lista de objetos informados
        /// </summary>
        /// <param name="itens">         Lista de entidades</param>
        /// <param name="exclusaoFisica"></param>
        /// <returns>Objeto</returns>
        void Excluir(List<TEntidade> itens, bool exclusaoFisica = false);

        /// <summary>
        /// Exclui os objetos com as chaves informadas
        /// </summary>
        /// <typeparam name="T">Tipo de entidade de banco</typeparam>
        /// <param name="chaves">        Lista de chaves para exclusão</param>
        /// <param name="exclusaoFisica">Tipo de exclusão</param>
        void Excluir(List<int> chaves, bool exclusaoFisica = false);

        /// <summary>
        /// Retorna a lista de objetos aplicando um filtro
        /// </summary>
        /// <param name="filtro">        Expressão a ser usada como filtro</param>
        /// <param name="exclusaoFisica"></param>
        /// <returns>Lista de objetos encontrados</returns>
        void Excluir(Expression<Func<TEntidade, bool>> filtro, bool exclusaoFisica = false);

        #endregion [ Métodos para Excluir o item ]

        #region [ Métodos Obter ]

        /// <summary>
        /// Retorna o objeto solicitado
        /// </summary>
        /// <param name="chave"></param>
        /// <returns>Objeto</returns>
        TEntidade Obter(int chave);

        /// <summary>
        /// Retorna o objeto solicitado
        /// </summary>
        /// <param name="chave"></param>
        /// <returns>Objeto</returns>
        TEntidade Obter(object[] chave);

        #endregion [ Métodos Obter ]

        #region [ Métodos Listar ]

        /// <summary>
        /// Retorna a lista de objetos aplicando um filtro
        /// </summary>
        /// <param name="filtro">    Expressão a ser usada como filtro</param>
        /// <param name="ordenacao"> </param>
        /// <param name="ascendente"></param>
        /// <returns>Lista de objetos encontrados</returns>
        List<TEntidade> ConsultarOrdenado<TKey>(Expression<Func<TEntidade, bool>> filtro, Expression<Func<TEntidade, TKey>> ordenacao, bool ascendente = true);

        /// <summary>
        /// Retorna a lista de objetos
        /// </summary>
        /// <returns>Lista de objetos na base</returns>
        List<TEntidade> Consultar(List<int> chaves);

        /// <summary>
        /// Retorna a lista de objetos
        /// </summary>
        /// <param name="filtro">     </param>
        /// <param name="paginaAtual"></param>
        /// <param name="itensPagina"></param>
        /// <param name="ascendente"> </param>
        /// <returns></returns>
        List<TEntidade> Consultar(Expression<Func<TEntidade, bool>> filtro, int paginaAtual = -1, int itensPagina = -1, bool ascendente = true);

        /// <summary>
        /// Retorna a quantidade de objetos encontrados com o filtro informado
        /// </summary>
        /// <param name="filtro">Expressão a ser usada como filtro</param>
        /// <returns>Quantidade de objetos encontrados</returns>
        int ConsultarQtd(Expression<Func<TEntidade, bool>> filtro);

        #endregion [ Métodos Listar ]
    }
}