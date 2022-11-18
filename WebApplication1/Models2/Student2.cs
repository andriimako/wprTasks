using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models2;

public class Student2
{
    [Key]
    public int IdStudent { get; set; }
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    // public string Email { get; set; }
    // public string IndexNumber { get; set; }
    // public string Address { get; set; }
}