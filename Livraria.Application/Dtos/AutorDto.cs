namespace Livraria.Application.Dtos
{
    public class AutorDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
    }
}