using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DM.GAPRecruitment.WebService.Models.DTO
{
    public class StoreDTO
    {

        public StoreDTO()
        {

        }
        
        public int Id { get; set; }

        [Required]
        [MaxLength(100), MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100), MinLength(10)]
        public string Address { get; set; }



    }
}