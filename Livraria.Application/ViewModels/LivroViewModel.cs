namespace Livraria.Application.ViewModels
{
    public class LivroViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public Guid AutorId { get; set; }
        public Guid GeneroId { get; set; }
    }
}