namespace ListaCompra.Modelo.API.Conta
{
    /// <summary>
    /// Login response
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Contrutor que preenche o token
        /// </summary>
        /// <param name="token"></param>
        public LoginResponse(string token)
        {
            this.JwtToken = token;
        }

        /// <summary>
        /// Token
        /// </summary>
        public string JwtToken { get; set; }
    }
}