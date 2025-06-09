namespace Livraria.Shared.DomainValidation
{
    public interface IDomainValidation
    {
        bool Validation { get; }

        bool Any();

        string Message { get; }

        /// <summary>
        /// Utilizar para mostrar erros de validação em formulários
        /// </summary>
        /// <param name="key">nome do campo</param>
        /// <param name="error">mensagem de validação</param>
        void Add(string key, string error);

        IEnumerable<string> AllKeys { get; }

        /// <summary>
        /// Utilizar para mostrar erros de dominio. Erros que não são vínculados a campos.
        /// </summary>
        /// <param name="error">mensagem de erro</param>
        void Add(string error);

        void EnsureValid();
    }
}
