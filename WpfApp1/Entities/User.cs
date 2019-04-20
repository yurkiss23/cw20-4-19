using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Entities
{
    [Table("testUsers")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(maximumLength: 50)]
        public string Name { get; set; }
    }
}
