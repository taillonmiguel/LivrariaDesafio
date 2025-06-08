namespace Livraria.Domain.Entities
{
    public class Autor : DefaultEntity
    {
        public string Nome { get; set; }
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}
