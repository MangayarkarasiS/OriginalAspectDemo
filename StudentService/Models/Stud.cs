using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models
{
    public class Stud
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int studId { get; set; }
        [Required]
        public string studName { get; set; }

        public int studTotalMarks { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime studDOB { get; set; }
        public string studGender { get; set; }

      
    }
}
