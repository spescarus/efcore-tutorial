using System.ComponentModel.DataAnnotations;

namespace EfCoreMvcTutorial.Models;

public class OfficeAssignment
{
    public int Id           { get; set; }
    public int InstructorId { get; set; }

    [StringLength(50)]
    [Display(Name = "Office Location")]
    public string Location { get; set; }

    public Instructor Instructor { get; set; }
}