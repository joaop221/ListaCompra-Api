using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ListaCompra.Modelo.Base
{
    /// <summary>
    /// Lista de entidades a serem incluidas
    /// </summary>
    /// <typeparam name="TEntidade"></typeparam>
    public class ListaEntidade<TEntidade> where TEntidade : class, IEntidade
    {
        /// <summary>
        /// Entidades a serem carregadas
        /// </summary>
        public List<Expression<Func<TEntidade, IEntidade>>> Itens = null;

        /// <summary>
        /// Lista de entidades a serem carregadas
        /// </summary>
        public List<Expression<Func<TEntidade, IEnumerable<IEntidade>>>> Itens_collection = null;


        /// <summary>
        /// Contrutor padrão
        /// </summary>
        public ListaEntidade()
        {
            this.Itens_collection = new List<Expression<Func<TEntidade, IEnumerable<IEntidade>>>>();
            this.Itens = new List<Expression<Func<TEntidade, IEntidade>>>();
        }

        /// <summary>
        /// Contrutor que recebe a lista das entidades
        /// </summary>
        /// <param name="entidades"></param>
        public ListaEntidade(params Expression<Func<TEntidade, IEnumerable<IEntidade>>>[] entidades) : this()
        {
            this.Itens_collection.AddRange(entidades);
        }

        /// <summary>
        /// Contrutor que recebe a lista das entidades
        /// </summary>
        /// <param name="entidades"></param>
        public ListaEntidade(params Expression<Func<TEntidade, IEntidade>>[] entidades) : this()
        {
            this.Itens.AddRange(entidades);
        }

        /// <summary>
        /// Adiciona nova entidade
        /// </summary>
        /// <param name="expressao"></param>
        public void Adicionar(Expression<Func<TEntidade, IEntidade>> expressao)
            => this.Itens.Add(expressao);

        /// <summary>
        /// Adiciona nova lista de entidade
        /// </summary>
        /// <param name="expressao"></param>
        public void Adicionar(Expression<Func<TEntidade, IEnumerable<IEntidade>>> expressao)
            => this.Itens_collection.Add(expressao);

    }
}
