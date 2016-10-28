using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.GAPRecruitment.Model.Models
{
    [Table("tblArticle")]
    public class Article
    {
        public Article() {


        }
        [Key]
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

        
        public int StoreId { get; set; }
        public Store Store { get; set; }



    }
}
