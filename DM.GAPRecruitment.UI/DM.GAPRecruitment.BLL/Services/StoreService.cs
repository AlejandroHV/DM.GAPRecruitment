using DM.GAPRecruitment.DAL.Config;
using DM.GAPRecruitment.Model.Models;
using DM.GAPRecruitment.Model.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.GAPRecruitment.BLL.Services
{
    public interface IStoreService
    {
        void CreateStore(Store model, ISystemFail error);
        void DeleteStore(Store model, ISystemFail error);
        void UpdateStore(Store model, ISystemFail error);
        Store GetStoreById(int id, ISystemFail error);
        List<Store> GetAllStores( ISystemFail error);


    }


    public class StoreService : IStoreService
    {
        private readonly IRepository<Store> storeRepository;

        public StoreService(IRepository<Store> _storeRepository)
        {

            this.storeRepository = _storeRepository;


        }

        public void CreateStore(Store model, ISystemFail error)
        {
            try
            {
                storeRepository.Add(model);
                storeRepository.SaveRepository(error);
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }
        }

        public void DeleteStore(Store model, ISystemFail error)
        {
            try
            {
                storeRepository.Delete(model);
                storeRepository.SaveRepository(error);
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }
        }

        public List<Store> GetAllStores(ISystemFail error)
        {
            List<Store> stores = null;
            try
            {
                stores = storeRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }
            return stores;
        }

        public Store GetStoreById(int id, ISystemFail error)
        {
            Store store = null;
            try
            {
                store = storeRepository.GetById(id);
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }

            return store;
        }

        public void UpdateStore(Store model, ISystemFail error)
        {
            try
            {
                storeRepository.Update(model);
                storeRepository.SaveRepository(error);
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
