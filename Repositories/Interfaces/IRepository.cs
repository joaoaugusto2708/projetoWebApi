﻿using System.Linq.Expressions;

namespace projetoWebApi.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? Get(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
