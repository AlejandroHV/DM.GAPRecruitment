using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.GAPRecruitment.Model.Models
{
    [Table("tblStore")]
    public class Store
    {

        public Store() {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100),MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100), MinLength(10)]
        public string Address { get; set; }

    }
}
