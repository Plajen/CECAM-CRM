using CECAM.Domain.Models;
using CECAM.Domain.Requests;
using CECAM.Domain.Responses;

namespace CECAM.Infra.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        Task<InternalResponse<TEntity>> Create(TEntity entity);
        Task<InternalResponse<TEntity>> Read(SearchRequest? request = null, int? id = null, bool count = false);
        Task<InternalResponse<TEntity>> Update(TEntity entity);
        Task<InternalResponse<TEntity>> Delete(int id);
    }
}
