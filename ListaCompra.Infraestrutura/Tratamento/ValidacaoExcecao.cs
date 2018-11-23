/*
© Copyright 2018 Rede S.A.
Autor   : frank.galindo
Empresa : Iteris Consultoria e Software
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using ListaCompra.Modelo;

namespace ListaCompra.Infraestrutura.Tratamento
{
    /// <summary>
    /// Classe de tratamento de erro do sistema.
    /// </summary>
    public class ValidacaoExcecao : Exception
    {
        /// <summary>
        /// Código do erro
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Nome do serviço e método onde o erro ocorreu
        /// </summary>
        public string Fonte { get; set; }

        /// <summary>
        /// Lista de erros na validação
        /// </summary>
        public List<RetornoErro> ValidacaoErros { get; set; }

        /// <summary>
        /// Construtor
        /// Obs: recupera o Fonte por Reflection de quem instanciou a <see cref="ValidacaoExcecao">TratamentoExcecao.</see>
        /// </summary>
        /// <param name="codigo">Código de erro</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public ValidacaoExcecao(int codigo)
        {
            this.Codigo = codigo;
            this.Fonte = GetCurrentMethod(2);
        }

        /// <summary>
        /// Construtor
        /// Obs: recupera o Fonte por Reflection de quem instanciou a <see cref="ValidacaoExcecao">TratamentoExcecao.</see>
        /// </summary>
        /// <param name="erros">Lista de erros</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public ValidacaoExcecao(List<RetornoErro> erros)
        {
            this.ValidacaoErros = erros;
            this.Codigo = 400;
            this.Fonte = GetCurrentMethod(2);
        }

        /// <summary>
        /// Construtor
        /// Obs: recupera o Fonte por Reflection de quem instanciou a <see cref="ValidacaoExcecao">TratamentoExcecao.</see>
        /// </summary>
        /// <param name="codigo">Código de erro</param>
        /// <param name="ex">    Exceção</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public ValidacaoExcecao(int codigo, Exception ex)
            : base(ex.Message, ex)
        {
            this.Codigo = codigo;
            this.Fonte = GetCurrentMethod(2);
        }

        /// <summary>
        /// Construtor
        /// Obs: recupera o Fonte por Reflection de quem instanciou a <see cref="ValidacaoExcecao">TratamentoExcecao.</see>
        /// </summary>
        /// <param name="codigo">  Código de erro</param>
        /// <param name="mensagem">Mensagem de erro</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public ValidacaoExcecao(string mensagem, int codigo)
            : base(mensagem)
        {
            this.Codigo = codigo;
            this.Fonte = GetCurrentMethod(2);
        }

        /// <summary>
        /// Construtor
        /// Obs: recupera o Fonte por Reflection de quem instanciou a <see cref="ValidacaoExcecao">TratamentoExcecao.</see>
        /// </summary>
        /// <param name="codigo">  Código de erro</param>
        /// <param name="mensagem">Mensagem de erro</param>
        /// <param name="ex">      Exceção</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public ValidacaoExcecao(string mensagem, int codigo, Exception ex)
            : base(mensagem, ex)
        {
            this.Codigo = codigo;
            this.Fonte = GetCurrentMethod(2);
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="codigo">Código de erro</param>
        /// <param name="fonte"> Nome de onde o erro ocorreu (Servico.Metodo) ou (Classe.Metodo)</param>
        public ValidacaoExcecao(int codigo, string fonte)
        {
            this.Codigo = codigo;
            this.Fonte = fonte;
        }

        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="codigo">  Código de erro</param>
        /// <param name="fonte">   Nome de onde o erro ocorreu (Servico.Metodo) ou (Classe.Metodo)</param>
        /// <param name="mensagem">Mensagem de erro</param>
        public ValidacaoExcecao(int codigo, string fonte, string mensagem)
            : base(mensagem)
        {
            this.Codigo = codigo;
            this.Fonte = fonte;
        }

        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="codigo">Código de erro</param>
        /// <param name="fonte"> Nome de onde o erro ocorreu (Servico.Metodo) ou (Classe.Metodo)</param>
        /// <param name="ex">    Exceção original</param>
        public ValidacaoExcecao(int codigo, string fonte, Exception ex)
            : base(ex.Message, ex)
        {
            this.Codigo = codigo;
            this.Fonte = fonte;
        }

        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="codigo">  Código de erro</param>
        /// <param name="fonte">   Nome de onde o erro ocorreu (Servico.Metodo) ou (Classe.Metodo)</param>
        /// <param name="mensagem">Mensagem de erro</param>
        /// <param name="ex">      Exceção original</param>
        public ValidacaoExcecao(int codigo, string fonte, string mensagem, Exception ex)
            : base(mensagem, ex)
        {
            this.Codigo = codigo;
            this.Fonte = fonte;
        }

        /// <summary>
        /// Retorna o nome do Método completo (namespace + classe + método)
        /// </summary>
        /// <param name="frame">Frame a ser considerado</param>
        /// <returns>Nome do método completo</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod(int frame)
        {
            var stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(frame).GetMethod();
            Type classType = methodBase.ReflectedType;
            return string.Concat(classType.FullName, ".", methodBase.Name);
        }
    }
}