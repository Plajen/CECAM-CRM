using CECAM.Domain.Models;
using CECAM.Domain.Requests;
using CECAM.Domain.Responses;

namespace CECAM.App.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : Entity
    {
        Task<InternalResponse<TEntity>> Get(SearchRequest? searchRequest, IdRequest? idRequest);
        Task<InternalResponse<TEntity>> Post(EntityRequest<TEntity> request);
        Task<InternalResponse<TEntity>> Put(EntityRequest<TEntity> request);
        Task<InternalResponse<TEntity>> Delete(IdRequest request);
    }
}
