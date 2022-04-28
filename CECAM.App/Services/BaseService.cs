using CECAM.App.Interfaces;
using CECAM.Domain.Enums;
using CECAM.Domain.Models;
using CECAM.Domain.Requests;
using CECAM.Domain.Responses;
using CECAM.Infra.Interfaces;

namespace CECAM.App.Services
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : Entity
    {
        protected readonly IBaseRepository<TEntity> _repository;

        protected const string COMANDO_NAO_IDENTIFICADO = "Comando não identificado";
        protected const string ERRO_SOLICITACAO_SERVICO = "O serviço encontrou um erro ao processar a solicitação";

        public BaseService(IBaseRepository<TEntity> repository) => _repository = repository;

        #region HTTP Methods

        public async Task<InternalResponse<TEntity>> Get(SearchRequest? searchRequest, IdRequest? idRequest)
        {
            if (searchRequest != null)
            {
                if (searchRequest.Command == CommandEnum.List)
                    return await List(searchRequest);

                if (searchRequest.Command == CommandEnum.Count)
                    return await Count(searchRequest);

                return new InternalResponse<TEntity>(COMANDO_NAO_IDENTIFICADO);
            }

            if (idRequest != null)
            {
                if (idRequest.Command == CommandEnum.Obtain && idRequest.Id > 0)
                    return await Obtain(idRequest.Id);

                return new InternalResponse<TEntity>(COMANDO_NAO_IDENTIFICADO);
            }

            return new InternalResponse<TEntity>(ERRO_SOLICITACAO_SERVICO);
        }

        public async Task<InternalResponse<TEntity>> Post(EntityRequest<TEntity> request)
        {
            if (request.Entity != null)
            {
                if (request.Command == CommandEnum.Create)
                    return await Create(request.Entity);

                return new InternalResponse<TEntity>(COMANDO_NAO_IDENTIFICADO);
            }

            return new InternalResponse<TEntity>(ERRO_SOLICITACAO_SERVICO);
        }

        public async Task<InternalResponse<TEntity>> Put(EntityRequest<TEntity> request)
        {
            if (request.Entity != null)
            {
                if (request.Command == CommandEnum.Update)
                    return await Update(request.Entity);

                if (request.Command == CommandEnum.Delete)
                    return await Delete(request.Entity);

                if (request.Command == CommandEnum.Undelete)
                    return await Undelete(request.Entity);

                return new InternalResponse<TEntity>(COMANDO_NAO_IDENTIFICADO);
            }

            return new InternalResponse<TEntity>(ERRO_SOLICITACAO_SERVICO);
        }

        public async Task<InternalResponse<TEntity>> Delete(IdRequest request)
        {
            if (request.Command == CommandEnum.Delete && request.Id > 0)
                return await HardDelete(request.Id);

            return new InternalResponse<TEntity>(COMANDO_NAO_IDENTIFICADO);
        }

        #endregion

        #region Specific Methods

        protected virtual async Task<InternalResponse<TEntity>> Obtain(int id)
            => await _repository.Read(id: id);

        protected virtual async Task<InternalResponse<TEntity>> List(SearchRequest request)
            => await _repository.Read(request);

        protected virtual async Task<InternalResponse<TEntity>> Count(SearchRequest request)
            => await _repository.Read(request, count: true);

        protected virtual async Task<InternalResponse<TEntity>> Create(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            return await _repository.Create(entity);
        }

        protected virtual async Task<InternalResponse<TEntity>> Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            return await _repository.Update(entity);
        }

        protected virtual async Task<InternalResponse<TEntity>> Delete(TEntity entity)
        {
            entity.Deleted = true;
            entity.DeletedAt = DateTime.Now;
            return await _repository.Update(entity);
        }

        protected virtual async Task<InternalResponse<TEntity>> Undelete(TEntity entity)
        {
            entity.Deleted = false;
            entity.UpdatedAt = DateTime.Now;
            return await _repository.Update(entity);
        }

        protected virtual async Task<InternalResponse<TEntity>> HardDelete(int id)
            => await _repository.Delete(id);

        #endregion
    }
}
