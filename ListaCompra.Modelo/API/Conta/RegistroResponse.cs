namespace ListaCompra.Modelo.API.Conta
{
    /// <summary>
    /// Response do registro
    /// </summary>
    public class RegistroResponse
    {
        /// <summary>
        /// Contrutor que preenche o token
        /// </summary>
        /// <param name="token"></param>
        public RegistroResponse(string token)
        {
            this.JwtToken = token;
        }

        /// <summary>
        /// Token
        /// </summary>
        public string JwtToken { get; set; }
    }
}