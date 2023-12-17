using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace UniversityWebAPI.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        
        public DateTime EnrollmentDate { get; set;}

        public Course Course { get; set; }

        public Student Student { get; set; }
    }
}
