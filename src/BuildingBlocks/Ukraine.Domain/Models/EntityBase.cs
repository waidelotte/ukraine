namespace Ukraine.Domain.Models
{
    public abstract class EntityBase<TIdentity>
    {
        public TIdentity Id { get; protected set; } = default!;
        
        protected EntityBase() { }

        protected EntityBase(TIdentity id)
        {
            Id = id;
        }
    }
}
