using System.ComponentModel.DataAnnotations;
namespace PJATK_APBD_Kol1_Gr16c_s33232.DTOs;

public class GetMakerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<GetProductDto> Products { get; set; } = [];
}
public class GetProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal StriskerPrice { get; set; }
}
public class CreateProductTypeDto
{
    public int Id { get; set; }
    public string? Name { get; set; } = null!;
}
public class GetVendorDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Amount { get; set; }
    public decimal PricePerUnit { get; set; }
}
public class PostProductDto
{
    [Required, MaxLength(150)] public string Name { get; set; } = null!;
    [MaxLength(500)] public string? Description { get; set; }
    public decimal StriskerPrice { get; set; }
    [Required, MaxLength(50)] public string Type { get; set; } = null!;
}
public class PostMakerDto
{
    [Required, MaxLength(150)] public string Name { get; set; } = null!;
    public List<PostProductDto> Products { get; set; } = [];
}
