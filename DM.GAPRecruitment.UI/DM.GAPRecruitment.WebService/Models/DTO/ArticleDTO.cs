using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DM.GAPRecruitment.WebService.Models.DTO
{
    public class ArticleDTO
    {

        public ArticleDTO()
        {


        }
        
        public int Id { get; set; }


        [Required]
        [MaxLength(100), MinLength(10)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500), MinLength(5)]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        public int TotalInShelf { get; set; }

        public int TotalInVault { get; set; }

        [Required]
        public int StoreId { get; set; }



    }
}