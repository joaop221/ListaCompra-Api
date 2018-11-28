using FluentValidation;

namespace ListaCompra.Modelo.API.Account
{
    public class LoginRequestValidacao : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidacao()
        {
            RuleFor(x => x.NomeUsuario)
                .NotEmpty();

            RuleFor(x => x.Senha)
                .NotEmpty();
        }
    }

    public class RegistroRequestValidacao : AbstractValidator<RegistroRequest>
    {
        public RegistroRequestValidacao()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Nome)
                .NotEmpty();

            RuleFor(x => x.Senha)
                .NotEmpty();
        }
    }
}