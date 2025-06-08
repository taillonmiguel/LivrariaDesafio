namespace Livraria.Domain.Entities
{
    public class Livro : DefaultEntity
    {
        public string Titulo { get; set; }
        public Guid GeneroId { get; set; }
        public Genero Genero { get; set; }
        public Guid AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
