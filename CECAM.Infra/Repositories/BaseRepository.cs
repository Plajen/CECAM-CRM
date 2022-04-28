using Microsoft.EntityFrameworkCore;
using CECAM.Domain.Models;
using CECAM.Domain.Requests;
using CECAM.Domain.Responses;
using CECAM.Infra.Interfaces;
using CECAM.Infra.Extensions;
using CECAM.Infra.Context;

namespace CECAM.Infra.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly CECAMDbContext _context;

        protected const string ERRO_SOLICITACAO_REPOSITORIO = "O repositório encontrou um erro ao processar a solicitação";

        public BaseRepository(CECAMDbContext context) => _context = context;

        public virtual async Task<InternalResponse<TEntity>> Create(TEntity entity)
        {
            var response = new InternalResponse<TEntity>();

            try
            {
                entity.Id = 0;
                response.EntityResult = new List<TEntity>() { _context.Add(entity).Entity };
                await _context.SaveChangesAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public virtual async Task<InternalResponse<TEntity>> Read(SearchRequest? request = null, int? id = null, bool count = false)
        {
            if (request == null && id != null && id > 0)
            {
                return await FindByIdAsync(id.Value);
            }

            if (request != null && id == null)
            {
                if (count == true)
                {
                    return await CountAsync(request);
                }

                return await ListAsync(request);
            }

            return new InternalResponse<TEntity>(ERRO_SOLICITACAO_REPOSITORIO);
        }

        public virtual async Task<InternalResponse<TEntity>> Update(TEntity entity)
        {
            var response = new InternalResponse<TEntity>();

            try
            {
                response.EntityResult = new List<TEntity>() { _context.Update(entity).Entity };
                await _context.SaveChangesAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        public virtual async Task<InternalResponse<TEntity>> Delete(int id)
        {
            var response = new InternalResponse<TEntity>();

            try
            {
                var entity = await FindByIdAsync(id);

                if (entity.Success == false)
                {
                    response = entity;
                    return response;
                }

                _context.Remove(entity);
                await _context.SaveChangesAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        protected virtual async Task<InternalResponse<TEntity>> FindByIdAsync(int id)
        {
            var response = new InternalResponse<TEntity>();

            try
            {
                var entity = await _context.FindAsync<TEntity>(id);

                if (entity != null)
                    response.EntityResult.Add(entity);

                response.Success = response.EntityResult.Count > 0;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        protected virtual async Task<InternalResponse<TEntity>> ListAsync(SearchRequest request)
        {
            var response = new InternalResponse<TEntity>();

            try
            {
                response.EntityResult = await GetQueryable(request).Where(x => x.Deleted == false).ToListAsync();
                response.Success = response.EntityResult.Count > 0;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        protected virtual async Task<InternalResponse<TEntity>> CountAsync(SearchRequest request)
        {
            var response = new InternalResponse<TEntity>();

            try
            {
                response.NumericResult = (await GetQueryable(request).ToListAsync()).Count;
                response.Success = response.NumericResult != null;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        protected virtual IQueryable<TEntity> GetQueryable(SearchRequest request)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (request.SearchProperty != null && request.SearchValue != null)
            {
                if (typeof(TEntity).GetProperty(request.SearchProperty) == null)
                    return Enumerable.Empty<TEntity>().AsQueryable();

                query = query.Where(x => BaseRepository<TEntity>.CheckProperty(x, request.SearchProperty, request.SearchValue));
            }

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
                query = query.OrderBy(request.OrderBy);

            if (request.Skip.HasValue)
                query = query.Skip(request.Skip.Value);

            if (request.Take.HasValue && request.Take.Value > 0)
                query = query.Take(request.Take.Value);

            return query.AsNoTracking();
        }

        private static bool CheckProperty(TEntity entity, string searchProperty, string searchValue)
        {
            var propertyValue = entity.GetType().GetProperty(searchProperty)!.GetValue(entity);

            return propertyValue != null
                && propertyValue!.ToString()!.ToLower().Contains(searchValue.ToLower());
        }
    }
}
