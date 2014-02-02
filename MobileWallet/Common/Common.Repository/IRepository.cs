﻿using System.Linq;

namespace Common.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);

        TEntity Find(params object[] keyValues);
        IRepositoryQuery<TEntity> Query();
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
        T SqlQueryScalar<T>(string query, params object[] parameters);
        void ExecuteCommand(string commandText, params object[] parameters);

        //void SaveChanges();
    }
}