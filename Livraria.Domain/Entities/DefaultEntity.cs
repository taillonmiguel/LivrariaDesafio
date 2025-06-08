using Livraria.Shared.Data;

namespace Livraria.Domain.Entities
{
    public abstract class DefaultEntity : IHaveId
    {

        public virtual Guid Id { get; set; }

        public virtual bool Active { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public DefaultEntity()
        {
            Id = Guid.NewGuid();
            Active = true;
            DataCriacao = DateTime.Now;
        }
    }
}
