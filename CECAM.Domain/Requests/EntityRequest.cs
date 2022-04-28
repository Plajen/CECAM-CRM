using CECAM.Domain.Models;

namespace CECAM.Domain.Requests
{
    public class EntityRequest<TEntity> : BaseRequest where TEntity : Entity
    {
        public TEntity Entity { get; set; }
    }
}
