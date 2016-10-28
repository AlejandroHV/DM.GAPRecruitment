using DM.GAPRecruitment.UI.Controllers.BLL;
using DM.GAPRecruitment.UI.Controllers.BLL.Util;
using DM.GAPRecruitment.UI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.GAPRecruitment.UI.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            SystemFail error = new SystemFail();
            List<Store> stores = StoreBLL.GetAllStores(error);
            if (stores == null && error.Error)
            {
                ViewBag.StartUpScript = string.Concat("An error has ocurred. Error", error.Message);
            }

            return View(stores);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {

            SystemFail error = new SystemFail();
            Store store = StoreBLL.GetStoreById(id, error);

            return View(store);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        [HttpPost]
        public ActionResult Create(Store model)
        {
            SystemFail error = new SystemFail();
            if (ModelState.IsValid)
            {
                StoreBLL.CreateStore(model, error);
                if (!error.Error)
                {
                    TempData["StartUpScript"] = "$.notify('"+error.Message+"','success');";
                }
                else
                {

                    TempData["StartUpScript"] = "$.notify('" + error.Message + "','error');";
                }
            }
            else
            {
                ViewBag.StartupScript = "$.notify('Some errores were found in the formulary information. Please check it','warn')";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            SystemFail error = new SystemFail();
            Store article = StoreBLL.GetStoreById(id, error);
            if (article == null && error.Error)
            {
                TempData["StartUp"] = "$.notify('The Store with the specific ID wasn´t found','success')";
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // POST: Store/Edit/5
        [HttpPost]
        public ActionResult Edit( Store model)
        {
            SystemFail error = new SystemFail();
            if (ModelState.IsValid)
            {
                StoreBLL.UpdateStore(model, error);
                if (!error.Error)
                {
                    TempData["StartUpScript"] = "$.notify('"+error.Message+"','success');";
                }
                else
                {

                    ViewBag.StartupScript = "$.notify('"+ error.Message + "','error');";
                    return View(model);
                }
            }
            else
            {

                return View(model);
            }
            return RedirectToAction("Index");
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {

            SystemFail error = new SystemFail();
            StoreBLL.DeleteStore(id, error);
            if (!error.Error)
            {
                TempData["StartUpScript"] = "$.notify('"+error.Message+"','success');";
            }
            else
            {

                TempData["StartUpScript"] = "$.notify('" + error.Message + "','error');";
            }
            return RedirectToAction("Index");
        }

       
    }
}
