using DM.GAPRecruitment.Model.Models;
using DM.GAPRecruitment.BLL.Services;
using DM.GAPRecruitment.WebService.Models.Responses;
using DM.GAPRecruitment.WebService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DM.GAPRecruitment.Model.Util;

namespace DM.GAPRecruitment.WebService.Controllers
{
    public class ArticleController : ApiController
    {
        #region Attributes 

        private readonly IArticleService articleService;

        #endregion

        #region Constructor 

        public ArticleController(IArticleService _articleService)
        {

            this.articleService = _articleService;
        }

        #endregion 


        // GET: Article
        [HttpGet]
        public GetAllArticleResponse Get()
        {
            SystemFail error = new SystemFail();
            List<Article> articles = articleService.GetAllArticles(error).ToList();
            GetAllArticleResponse response = new GetAllArticleResponse();

            response.success = !error.Error;
            response.total_elements = articles.Count;
            response.articles = articles.Select(x => new ArticleDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                StoreId = x.StoreId,
                TotalInShelf = x.TotalInShelf,
                TotalInVault = x.TotalInVault
            }).ToList();

            return response;
        }

        [HttpGet]
        public GetArticleByIdResponse GetArticleById(int articleId)
        {
            SystemFail error = new SystemFail();
            GetArticleByIdResponse response = new GetArticleByIdResponse();
            Article article = articleService.GetArticleById(articleId,error);
            ArticleDTO dto = new ArticleDTO();
            dto.Description = article.Description;
            dto.Id = article.Id;
            dto.Name = article.Name;
            dto.Price = article.Price;
            dto.StoreId = article.StoreId;
            dto.TotalInShelf = article.TotalInShelf;
            dto.TotalInVault = article.TotalInVault;
           
            response.success = !error.Error;
            response.article = dto;
            return response;

        }


        // GET: Article
        [HttpGet]
        public GetAllArticleResponse GetStoreArticles(int id,string dummy)
        {
            SystemFail error = new SystemFail();
            List<Article> articles = articleService.GettStoreArticles(id,error).ToList();
            GetAllArticleResponse response = new GetAllArticleResponse();

            response.success = !error.Error;
            response.total_elements = articles.Count;
            response.articles = articles.Select(x => new ArticleDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                StoreId = x.StoreId,
                TotalInShelf = x.TotalInShelf,
                TotalInVault = x.TotalInVault
            }).ToList();

            return response;
        }


        
        [HttpPost]
        public ApiResponse Post([FromBody] ArticleDTO article, string dummy)
        {

            SystemFail error = new SystemFail();
            ApiResponse response = new ApiResponse();
            if (ModelState.IsValid)
            {
                Article newArticle = TransformArticleDTOtoArticle(article);
                
                articleService.CreateArticle(newArticle, error);
                if (!error.Error)
                {
                    response.success = true;
                    response.Message = "Se creo correctamente el articulo";

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

        /// <summary>
        /// Deletes an article
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="dummy"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResponse DeleteArticle([FromBody] Article article)
        {

            SystemFail error = new SystemFail();
            ApiResponse response = new ApiResponse();
        
            if (article != null && !error.Error)
            {
                articleService.DeleteArticle(article, error);
                if (!error.Error)
                {
                    response.Message = "El articulo fue eliminada exitosamente.";
                    response.success = true;
                }
                else
                {

                    response.Message = string.Concat("No fue posible eliminar el articulo. Error: ", error.Message);
                    response.success = false;
                }
            }
            else
            {
                response.Message = "Ha ocurrido un error. El articulo con el Id especificado , no fue encontrado";
                response.success = false;

            }
            return response;
        }

        [HttpPut]
        public ApiResponse UpdateArticle([FromBody] ArticleDTO article)
        {

            SystemFail error = new SystemFail();
            ApiResponse response = new ApiResponse();
            if (ModelState.IsValid)
            {
                Article updatedArticle = TransformArticleDTOtoArticle(article);
                articleService.UpdateArticle(updatedArticle, error);
                if (!error.Error)
                {
                    response.Message = "El articulo fue actualizado exitosamente.";
                    response.success = true;
                }
                else
                {

                    response.Message = string.Concat("No fue posible actualizar el articulo. Error: ", error.Message);
                    response.success = false;
                }


            }
            else
            {
                response.Message = string.Concat("Ha ocurrido un error al validar la entidad. Error:", string.Join(";", ModelState.Values.Select(x => x.Errors.Select(e => e.ErrorMessage))));
                response.success = false;
            }

            return response;
        }

        public Article TransformArticleDTOtoArticle(ArticleDTO dtoModel) {

            Article model = new Article();
            model.Description = dtoModel.Description;
            model.Id = dtoModel.Id;
            model.Name = dtoModel.Name;
            model.Price = dtoModel.Price;
            model.StoreId = dtoModel.StoreId;
            model.TotalInShelf = dtoModel.TotalInShelf;
            model.TotalInVault = dtoModel.TotalInVault;

            return model;

        }


    }
}