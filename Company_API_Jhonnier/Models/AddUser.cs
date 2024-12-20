using System.ComponentModel.DataAnnotations;

namespace Company_API_Jhonnier.Models;
public class AddUser
{
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}
