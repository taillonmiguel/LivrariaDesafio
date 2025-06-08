namespace Livraria.Domain.Entities
{
    public class Genero : DefaultEntity
    {
        public string Nome { get; private set; }
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();

        public Genero(string nome)
        {
            Nome = nome;
        }

        public void Atualizar(string nome)
        {
            Nome = nome;
        }
    }
}