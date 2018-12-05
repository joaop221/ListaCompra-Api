using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
        Task<TEntidade> InserirAsync(TEntidade item, int? setTimeoutTo = null, int reattempts = 0);

        /// <summary>
        /// Efetua a inclusão da lista enviada
        /// </summary>
        /// <param name="itens">       </param>
        /// <param name="setTimeoutTo"></param>
        /// <param name="reattempts">  </param>
        /// <returns>Objeto</returns>
        Task InserirAsync(List<TEntidade> itens, int? setTimeoutTo = null, int reattempts = 0);

        #endregion [ Métodos de Inclusão ]

        #region [ Atualizar ]

        /// <summary>
        /// Efetua a atualização do objeto informado
        /// </summary>
        /// <param name="item">          </param>
        /// <param name="exclusaoLogica"></param>
        /// <param name="reattempts">    </param>
        /// <returns>Objeto</returns>
        Task AtualizarAsync(TEntidade item, bool exclusaoLogica = false, int reattempts = 0);

        /// <summary>
        /// Atualiza a lista de objetos informados
        /// </summary>
        /// <param name="itens">         Lista de entidades</param>
        /// <param name="exclusaoLogica"></param>
        /// <param name="reattempts">    </param>
        /// <returns></returns>
        Task AtualizarAsync(List<TEntidade> itens, bool exclusaoLogica = false, int reattempts = 0);

        #endregion [ Atualizar ]

        #region [ Métodos para Excluir o item ]

        /// <summary>
        /// Exclui o objeto solicitado
        /// </summary>
        /// <param name="item">          </param>
        /// <param name="exclusaoFisica"></param>
        /// <param name="reattempts">    </param>
        /// <returns>Objeto</returns>
        Task ExcluirAsync(TEntidade item, bool exclusaoFisica = false, int reattempts = 0);

        /// <summary>
        /// Exclui a lista de objetos informados
        /// </summary>
        /// <param name="itens">         Lista de entidades</param>
        /// <param name="exclusaoFisica"></param>
        /// <returns>Objeto</returns>
        Task ExcluirAsync(List<TEntidade> itens, bool exclusaoFisica = false);

        /// <summary>
        /// Exclui os objetos com as chaves informadas
        /// </summary>
        /// <param name="chaves">        Lista de chaves para exclusão</param>
        /// <param name="exclusaoFisica">Tipo de exclusão</param>
        Task ExcluirAsync(bool exclusaoFisica = false, params object[] chaves);

        /// <summary>
        /// Retorna a lista de objetos aplicando um filtro
        /// </summary>
        /// <param name="filtro">        Expressão a ser usada como filtro</param>
        /// <param name="exclusaoFisica"></param>
        /// <returns>Lista de objetos encontrados</returns>
        Task ExcluirAsync(Expression<Func<TEntidade, bool>> filtro, bool exclusaoFisica = false);

        #endregion [ Métodos para Excluir o item ]

        #region [ Métodos Obter ]

        /// <summary>
        /// Retorna o objeto solicitado
        /// </summary>
        /// <param name="chave"></param>
        /// <returns>Objeto</returns>
        Task<TEntidade> ObterAsync(object chave);

        /// <summary>
        /// Retorna o objeto solicitado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Objeto</returns>
        Task<TEntidade> ObterAsync(Expression<Func<TEntidade, bool>> filtro);

        #endregion [ Métodos Obter ]

        #region [ Métodos Listar ]

        /// <summary>
        /// Retorna a lista de objetos aplicando um filtro
        /// </summary>
        /// <param name="filtro">    Expressão a ser usada como filtro</param>
        /// <param name="ordenacao"> </param>
        /// <param name="ascendente"></param>
        /// <returns>Lista de objetos encontrados</returns>
        Task<List<TEntidade>> ConsultarOrdenadoAsync<TKey>(Expression<Func<TEntidade, bool>> filtro, Expression<Func<TEntidade, TKey>> ordenacao, bool ascendente = true);

        /// <summary>
        /// Retorna a lista de objetos
        /// </summary>
        /// <returns>Lista de objetos na base</returns>
        Task<List<TEntidade>> ConsultarAsync(params object[] chaves);

        /// <summary>
        /// Retorna a lista de objetos
        /// </summary>
        /// <param name="filtro">     </param>
        /// <param name="paginaAtual"></param>
        /// <param name="itensPagina"></param>
        /// <param name="ascendente"> </param>
        /// <returns></returns>
        Task<List<TEntidade>> ConsultarAsync(Expression<Func<TEntidade, bool>> filtro, int paginaAtual = -1, int itensPagina = -1, bool ascendente = true);

        /// <summary>
        /// Retorna a quantidade de objetos encontrados com o filtro informado
        /// </summary>
        /// <param name="filtro">Expressão a ser usada como filtro</param>
        /// <returns>Quantidade de objetos encontrados</returns>
        Task<int> ConsultarQtdAsync(Expression<Func<TEntidade, bool>> filtro);

        #endregion [ Métodos Listar ]
    }
}