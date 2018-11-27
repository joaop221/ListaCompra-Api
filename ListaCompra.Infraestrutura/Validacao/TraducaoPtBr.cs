using FluentValidation.Resources;

namespace ListaCompra.Infraestrutura.Validacao
{
    public class TraducaoPtBr : LanguageManager
    {
        public TraducaoPtBr()
        {
            AddTranslation("br", "EmailValidator", "'{PropertyName}' é um endereço de email inválido.");
            AddTranslation("br", "GreaterThanOrEqualValidator", "'{PropertyName}' deve ser superior ou igual a '{ComparisonValue}'.");
            AddTranslation("br", "GreaterThanValidator", "'{PropertyName}' deve ser superior a '{ComparisonValue}'.");
            AddTranslation("br", "LengthValidator", "'{PropertyName}' deve ter entre {MinLength} e {MaxLength} caracteres. Você digitou {TotalLength} caracteres.");
            AddTranslation("br", "MinimumLengthValidator", "'{PropertyName}' deve ser maior ou igual a {MinLength} caracteres. Você digitou {TotalLength} caracteres.");
            AddTranslation("br", "MaximumLengthValidator", "'{PropertyName}' deve ser menor ou igual a {MaxLength} caracteres. Você digitou {TotalLength} caracteres.");
            AddTranslation("br", "LessThanOrEqualValidator", "'{PropertyName}' deve ser inferior ou igual a '{ComparisonValue}'.");
            AddTranslation("br", "LessThanValidator", "'{PropertyName}' deve ser inferior a '{ComparisonValue}'.");
            AddTranslation("br", "NotEmptyValidator", "'{PropertyName}' deve ser informado.");
            AddTranslation("br", "NotEqualValidator", "'{PropertyName}' deve ser diferente de '{ComparisonValue}'.");
            AddTranslation("br", "NotNullValidator", "'{PropertyName}' não pode ser nulo.");
            AddTranslation("br", "PredicateValidator", "'{PropertyName}' não atende a condição definida.");
            AddTranslation("br", "AsyncPredicateValidator", "'{PropertyName}' não atende a condição definida.");
            AddTranslation("br", "RegularExpressionValidator", "'{PropertyName}' não está no formato correto.");
            AddTranslation("br", "EqualValidator", "'{PropertyName}' deve ser igual a '{ComparisonValue}'.");
            AddTranslation("br", "ExactLengthValidator", "'{PropertyName}' deve ter no máximo {MaxLength} caracteres. Você digitou {TotalLength} caracteres.");
            AddTranslation("br", "ExclusiveBetweenValidator", "'{PropertyName}' deve, exclusivamente, estar entre {From} e {To}. Você digitou {Value}.");
            AddTranslation("br", "InclusiveBetweenValidator", "'{PropertyName}' deve estar entre {From} e {To}. Você digitou {Value}.");
            AddTranslation("br", "CreditCardValidator", "'{PropertyName}' não é um número válido de cartão de crédito.");
            AddTranslation("br", "ScalePrecisionValidator", "'{PropertyName}' não pode ter mais do que {expectedPrecision} dígitos no total, com {expectedScale} dígitos decimais. {digits} dígitos e {actualScale} decimais foram informados.");
            AddTranslation("br", "EmptyValidator", "'{PropertyName}' deve estar vazio.");
            AddTranslation("br", "NullValidator", "'{PropertyName}' deve estar null.");
            AddTranslation("br", "EnumValidator", "'{PropertyName}' possui um intervalo de valores que não inclui '{PropertyValue}'.");
        }
    }
}