
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    
    public bool InStock { get; set; }=true;
}
