using DM.GAPRecruitment.Model.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DM.GAPRecruitment.DAL.Config
{
    public interface IRepository<T> where T : class
    {
        

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
         T GetById(int id);
        IEnumerable<T> GetAll() ;
        void SaveRepository(ISystemFail error);

        
    }
}
