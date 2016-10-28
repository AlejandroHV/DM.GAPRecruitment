using DM.GAPRecruitment.Model.Models;
using DM.GAPRecruitment.BLL.Services;
using DM.GAPRecruitment.WebService.Models.Responses;
using DM.GAPRecruitment.WebService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DM.GAPRecruitment.Model.Util;

namespace DM.GAPRecruitment.WebService.Controllers
{
    public class StoreController : ApiController
    {

        #region Attributes 

        private readonly IStoreService storeService;

        #endregion


        #region Constructor

        public StoreController(IStoreService _storeService)
        {

            this.storeService = _storeService;
        }

        #endregion

        public GetAllStoresResponse Get()
        {


            SystemFail error = new SystemFail();
            List<Store> stores = storeService.GetAllStores(error).ToList();
            GetAllStoresResponse response = new GetAllStoresResponse();

            response.success = !error.Error;
            response.total_elements = stores.Count;
            response.stores = stores.Select(x => new StoreDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToList();

            return response;
        }


        public GetStoreByIdResponse GetStoreById(int id)
        {

            GetStoreByIdResponse response = new GetStoreByIdResponse();
            SystemFail error = new SystemFail();
            Store store = storeService.GetStoreById(id, error);
            if (store != null && !error.Error)
            {
                response.store = new StoreDTO();
                response.store.Address = store.Address;
                response.store.Id = store.Id;
                response.store.Name = store.Name;
                response.success = true;
            }
            else
            {

                response.success = false;
            }

            return response;
        }

        [HttpPost]
        public ApiResponse CreateStore([FromBody] StoreDTO store, string dummy)
        {

            SystemFail error = new SystemFail();
            ApiResponse response = new ApiResponse();
            if (ModelState.IsValid)
            {
                Store newStore = new Store();
                newStore.Address = store.Address;
                newStore.Id = store.Id;
                newStore.Name = store.Name;
                storeService.CreateStore(newStore, error);
                if (!error.Error)
                {
                    response.success = true;
                    response.Message = "Se creo correctamente la tienda";

                }
                else
                {
                    response.success = false;
                    response.Message = string.Concat("Ha ocurrido un error. Error:", error.Message);
                }
            }
            else
            {

                response.success = false;
                response.Message = string.Concat("Ha ocurrido un error al validar la entidad. Error:", string.Join(";", ModelState.Values.Select(x => x.Errors.Select(e => e.ErrorMessage))));
            }

            return response;

        }

        [HttpDelete]
        public ApiResponse DeleteStore([FromBody] StoreDTO store)
        {

            SystemFail error = new SystemFail();
            ApiResponse response = new ApiResponse();
            
            if (store != null && !error.Error)
            {
                Store deletStore = new Store();
                deletStore.Id = store.Id;
                deletStore.Name = store.Name;
                deletStore.Address = store.Address;
                storeService.DeleteStore(deletStore, error);
                if (!error.Error)
                {
                    response.Message = "La tienda fue eliminada exitosamente.";
                    response.success = true;
                }
                else {

                    response.Message = string.Concat( "No fue posible eliminar la tienda. Error: ", error.Message);
                    response.success = false;
                }
            }
            else
            {
                response.Message = "Ha ocurrido un error. La tienda con el Id especificado , no fue encontrada";
                response.success = false;

            }
            return response;
        }

        [HttpPut]
        public ApiResponse UpdateStore([FromBody] StoreDTO store) {

            SystemFail error = new SystemFail();
            ApiResponse response = new ApiResponse();
            if (ModelState.IsValid)
            {
                Store updatedStore = new Store();
                updatedStore.Address = store.Address;
                updatedStore.Id = store.Id;
                updatedStore.Name = store.Name;
                storeService.UpdateStore(updatedStore, error);
                if (!error.Error)
                {
                    response.Message = "La tienda fue actualizada exitosamente.";
                    response.success = true;
                }
                else
                {

                    response.Message = string.Concat("No fue posible actualizar la tienda. Error: ", error.Message);
                    response.success = false;
                }


            } else {
                response.Message = string.Concat("Ha ocurrido un error al validar la entidad. Error:", string.Join(";", ModelState.Values.Select(x => x.Errors.Select(e => e.ErrorMessage))));
                response.success = false;
            }

            return response;
        }

    }
}
