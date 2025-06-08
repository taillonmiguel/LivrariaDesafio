namespace Livraria.Domain.Entities
{
    public class Livro : DefaultEntity
    {
        public string Titulo { get; private set; }
        public Guid GeneroId { get; private set; }
        public Genero Genero { get; private set; }
        public Guid AutorId { get; private set; }
        public Autor Autor { get; private set; }
        public string Descricao { get; private set; }

        public Livro(string titulo, string descricao, Guid autorId, Guid generoId)
        {
            Titulo = titulo;
            Descricao = descricao;
            AutorId = autorId;
            GeneroId = generoId;
        }

        public void Atualizar(string titulo, string descricao, Guid autorId, Guid generoId)
        {
            Titulo = titulo;
            Descricao = descricao;
            AutorId = autorId;
            GeneroId = generoId;
        }
    }
}