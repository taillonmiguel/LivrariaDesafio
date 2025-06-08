namespace Livraria.Application.ViewModels
{
    public class CreateLivroViewModel
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public Guid AutorId { get; set; }
        public Guid GeneroId { get; set; }
    }
}