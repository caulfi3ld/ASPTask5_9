using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPTask5_9.Models
{
    public class StudentModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Age")]
        [Range(16, 100)]
        public int Age { get; set; }

        [Required]
        [Display(Name ="Group")]
        [MaxLength(20)]
        public string Group { get; set; }
        
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
