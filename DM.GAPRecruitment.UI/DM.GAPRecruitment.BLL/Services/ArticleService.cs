using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.GAPRecruitment.Model.Models;
using DM.GAPRecruitment.DAL.Config;
using DM.GAPRecruitment.Model.Util;

namespace DM.GAPRecruitment.BLL.Services
{
    public interface IArticleService
    {

        void CreateArticle(Article model, ISystemFail error);
        void DeleteArticle(Article model, ISystemFail error);
        void UpdateArticle(Article model, ISystemFail error);
        Article GetArticleById(int id, ISystemFail error);
        List<Article> GettStoreArticles(int storeId, ISystemFail error);
        List<Article> GetAllArticles( ISystemFail error);

    }


    public class ArticleService : IArticleService
    {

        private readonly IRepository<Article> articleRepository;

        public ArticleService(IRepository<Article> _articleRepository)
        {

            this.articleRepository = _articleRepository;


        }

        public void CreateArticle(Article model, ISystemFail error)
        {
            try
            {
                articleRepository.Add(model);
                articleRepository.SaveRepository(error);
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }

        }

        public void DeleteArticle(Article model, ISystemFail error)
        {
            try
            {
                articleRepository.Delete(model);
                articleRepository.SaveRepository(error);
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }
        }

        public List<Article> GetAllArticles(ISystemFail error)
        {
            List<Article> articles = null;
            try
            {
                articles = articleRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }
            return articles;
        }

        public List<Article> GettStoreArticles(int storeId, ISystemFail error) {

            List<Article> articles = null;
            try
            {
                articles = articleRepository.GetAll().Where(x=> x.StoreId == storeId).ToList();
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }
            return articles;
        }

        public Article GetArticleById(int id, ISystemFail error)
        {
            Article article = null;
            try
            {
                article= articleRepository.GetById(id);
            }
            catch (Exception ex)
            {
                error.Error = true;
                error.Exception = ex;
                error.Message = ex.Message;
            }
            return article;
        }

        public void UpdateArticle(Article model, ISystemFail error)
        {
            try
            {
                articleRepository.Update(model);
                articleRepository.SaveRepository(error);
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
