namespace Livraria.Application.Dtos
{
    public sealed class LivroFilterDto
    {
        public Guid? LivroId { get; init; }
        public Guid? AutorId { get; init; }
        public bool? Active { get; init; }
        public string? Search { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
