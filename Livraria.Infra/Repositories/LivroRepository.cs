using Livraria.Domain.Dto;
using Livraria.Domain.Entities;
using Livraria.Domain.Repositories;
using Livraria.Infra.Context;
using Livraria.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Repositories
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(AppDbContext context) : base(context) { }

        public async Task<PagedResult<Livro>> SearchAsync(LivroFilter f)
        {
            IQueryable<Livro> query = _context.Set<Livro>()
                .Include(l => l.Autor)
                .Include(l => l.Genero);   // tipo agora é IQueryable<Livro>

            if (f.LivroId.HasValue)
                query = query.Where(l => l.Id == f.LivroId.Value);

            if (f.AutorId.HasValue)
                query = query.Where(l => l.AutorId == f.AutorId.Value);

            if (f.Active.HasValue)
                query = query.Where(l => l.Active == f.Active.Value);

            if (f.Search is not null)
                query = query.Where(l =>
                    l.Titulo.Contains(f.Search) ||
                    l.Descricao.Contains(f.Search));

            var total = await query.CountAsync();

            var itens = await query
                .Skip((f.Page - 1) * f.PageSize)
                .Take(f.PageSize)
                .ToListAsync();

            return new PagedResult<Livro>(itens, total, f.Page, f.PageSize);
        }
    }
}