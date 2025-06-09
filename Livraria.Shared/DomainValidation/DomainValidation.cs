using Livraria.Shared.CustomException;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Shared.DomainValidation
{
    public class DomainValidation : IDomainValidation
    {
        public DomainValidation()
        {
            FieldErrors = new Dictionary<string, string>();
            DomainErrors = [];
            Validation = true;
            Message = "Validação";
        }

        public bool Validation { get; private set; }

        public string Message { get; private set; }

        public bool Any() => FieldErrors.Any() || DomainErrors.Any();

        public IEnumerable<string> AllKeys
        {
            get
            {
                foreach (var item in FieldErrors)
                    yield return item.Key;
            }
        }

        public IEnumerable<string> Errors
        {
            get
            {
                foreach (var item in FieldErrors)
                    yield return item.Value;

                foreach (var item in DomainErrors)
                    yield return item;
            }
        }

        public IDictionary<string, string> FieldErrors { get; }
        public IList<string> DomainErrors { get; }

        public void Add(string key, string error)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(error))
                throw new ValidationException("Chave ou Erro está nulo ou vazio.");

            FieldErrors.Add(key, error);
        }

        public void Add(string error)
        {
            if (string.IsNullOrEmpty(error))
                throw new ValidationException("Erro está nulo ou vazio.");

            DomainErrors.Add(error);
        }

        public void EnsureValid()
        {
            if (Any())
            {
                throw new DomainValidationException();
            }
        }
    }
}