using System.ComponentModel.DataAnnotations;

namespace GeoPet.Controllers.TypesReq;

public class ReqPet
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int Breed { get; set; }
    [Required]
    [Range(1, 3)]
    public int Size { get; set; }
    [Required]
    public int Age { get; set; }
    
}