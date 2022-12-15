using System.ComponentModel.DataAnnotations;

namespace GeoPet.Controllers.TypesReq;

public class ReqUser
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public int Cep { get; set; }
    [Required]
    public string Password { get; set; }
}