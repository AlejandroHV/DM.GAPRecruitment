using DM.GAPRecruitment.DAL.Config;
using DM.GAPRecruitment.Model.Models;
using DM.GAPRecruitment.Model.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.GAPRecruitment.DAL.Repositories
{

    public class StoreRepository : IRepository<Store>
    {
        private readonly IContext context;
        private readonly IDbSet<Store> _dbSet;

        public StoreRepository(IContext _context)
        {

            context = _context;
            _dbSet = context.Set<Store>();
        }


        public void Add(Store entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _dbSet.Add(entity);
        }

        public void Delete(Store entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
           
        }

        public IEnumerable<Store> GetAll()
        {
            return _dbSet.ToList();
        }

        public Store GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(Store entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            
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
