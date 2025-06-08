namespace Livraria.Domain.Entities
{
    public class Autor : DefaultEntity
    {
        public string Nome { get; private set; }
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();

        public Autor(string nome)
        {
            Nome = nome;
        }

        public void Atualizar(string nome)
        {
            Nome = nome;
        }
    }
}