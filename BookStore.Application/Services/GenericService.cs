using BookStore.Domain.Common;
using BookStore.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IValidator<TEntity> _validator;
        public GenericService(IGenericRepository<TEntity> genericRepository, IValidator<TEntity> validator)
        {
            _repository = genericRepository;
            _validator = validator;
        }

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _repository = genericRepository;
        }

        public async Task<TEntity> FindAsync(params object[] keyValues) => await _repository.FindAsync(keyValues);
        public async Task<List<TEntity>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.GetAllFilteredAsync(filter);
        public async Task<List<TEntity>> GetAllFilteredIncludeAsync(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeExpressions)
            => await _repository.GetAllFilteredIncludeAsync(filter, includeExpressions);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.CountAsync(filter);
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.IsExistAsync(filter);

        protected Result<TEntity> Validate(TEntity entity) => new Result<TEntity>(_validator.Validate(entity));

        public void AddRange(IList<TEntity> entities) => _repository.AddRange(entities);
        public void RemoveRange(IList<TEntity> entities) => _repository.RemoveRange(entities);

    }
}
