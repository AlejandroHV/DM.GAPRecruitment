
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
    public class ArticleController : Controller
    {
        #region Attributes

        #endregion

        #region Constructor

        public ArticleController()
        {
            
            
        }


        #endregion



        // GET: Article
        public ActionResult Index()
        {
            SystemFail error = new SystemFail();
            if (TempData["StartUpScript"] != null)
            {
                ViewBag.StartupScript=TempData["StartUpScript"].ToString();
            }
            
            
            List<Article> articles = ArticleBLL.GetAllArticles(error);

            return View(articles);
        }

        // GET: Article/Details/5
        public ActionResult Details(int id)
        {
            SystemFail error = new SystemFail();
            Article article = ArticleBLL.GetArticleById(id,error);
            

            return View(article);
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            SystemFail error = new SystemFail();
            List<Store> stores = StoreBLL.GetAllStores(error);
            if (stores != null && !error.Error)
            {
                ViewBag.Stores = stores.Select(x => new { Id = x.Id, Name = x.Name });
            }
           
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        public ActionResult Create(Article article)
        {
            SystemFail error = new SystemFail();
            if (ModelState.IsValid)
            {
                ArticleBLL.CreateArticle(article, error);
                if (!error.Error)
                {
                    TempData["StartUpScript"] = "$.notify('Se creo correctamente el articulo','success');";
                }
                else
                {

                    TempData["StartUpScript"] = "$.notify('Ocurrio un error al crear el articulo  Error" + error.Message + "','error');";
                }
            }
            else {
                ViewBag.StartupScript ="$.notify('Se han encontrador errores en la información ingresada. Por favor revisela','warn')";
                return View(article);
            }

            return RedirectToAction("Index");
        }

        // GET: Article/Edit/5
        public ActionResult Edit(int id)
        {
            SystemFail error = new SystemFail();
            Article article = ArticleBLL.GetArticleById(id, error);


            return View(article);
        }

        // POST: Article/Edit/5
        [HttpPost]
        public ActionResult Edit(Article model)
        {
            SystemFail error = new SystemFail();

            if (ModelState.IsValid)
            {
                ArticleBLL.UpdateArticle(model, error);
                if (!error.Error)
                {
                    TempData["StartUpScript"] = "$.notify('Se creo correctamente el articulo','success');";
                }
                else
                {

                    ViewBag.StartupScript = "$.notify('Ocurrio un error al crear el articulo.Error" + error.Message + "','error');";
                    return View(model);
                }
            }
            else {

                return View(model);
            }
            return RedirectToAction("Index");
            
        }

        // GET: Article/Delete/5
        public ActionResult Delete(int id)
        {
            SystemFail error = new SystemFail();
            ArticleBLL.DeleteArticle(id, error);
            if (!error.Error)
            {
                TempData["StartUpScript"] = "$.notify('Se elimino correctamente el articulo','success');";
            }
            else {

                TempData["StartUpScript"] = "$.notify('"+error.Message+"','error');";
            }
            return RedirectToAction("Index") ;
        }

        
    }
}
