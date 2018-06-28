using FluentValidation.Attributes;
using ListaCompra.Modelo.Enumeradores;
using ListaCompra.Modelo.Validator;
using System.Collections.Generic;

namespace ListaCompra.Modelo
{
	/// <summary>
	/// Representante de Grupo
	/// </summary>
	[Validator(typeof(GrupoValidator))]
	public class Grupo : Base
	{
		/// <summary>
		/// Id do Grupo
		/// </summary>
		public int IdGrupo { get; set; }

		/// <summary>
		/// Nome do Grupo
		/// </summary>
		public string Nome { get; set; }

		/// <summary>
		/// Descrição do Grupo
		/// </summary>
		public string Descricao { get; set; }

        /// <summary>
        /// Icone de identificação da lista
        /// </summary>
        public string Imagem { get; set; }

        /// <summary>
        /// Lista de Listas
        /// </summary>
        public List<Lista> Listas { get; set; }

		/// <summary>
		/// Lista de Usuários
		/// </summary>
		public List<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Lista de perfis
        /// </summary>
        public Dictionary<int, Perfil> UsuariosPerfis { get; set; }
    }
}