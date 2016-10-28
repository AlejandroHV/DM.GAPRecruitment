using System;
using System.Collections.Generic;
using DM.GAPRecruitment.DAL.Config;
using DM.GAPRecruitment.Model.Models;
using System.Data.Entity;
using DM.GAPRecruitment.DAL.Context;
using System.Linq;
using System.Linq.Expressions;
using DM.GAPRecruitment.Model.Util;
using System.Data.Entity.Validation;

namespace DM.GAPRecruitment.DAL.Repositories
{
    public class ArticleRepository : IRepository<Article>
    {

        private readonly IContext context;
        private readonly IDbSet<Article> _dbSet;

        public ArticleRepository(IContext _context) {

            context = _context;
            _dbSet = context.Set<Article>();
        }


        public void Add(Article entity)
        {
            if(entity == null) throw new ArgumentNullException("entity");
            _dbSet.Add(entity);
        }

        public void Delete(Article entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public IEnumerable<Article> GetAll()
        {
            return _dbSet.ToList();
        }

        public Article GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(Article entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _dbSet.Attach(entity);
            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void SaveRepository(ISystemFail error)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                error.Error = true;
                error.Exception = dbValidationEx;
                error.Message = dbValidationEx.Message;

            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }
           
        }
    }

}
