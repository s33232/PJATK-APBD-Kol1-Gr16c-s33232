namespace PJATK_APBD_Kol1_Gr16c_s33232.Services;

using PJATK_APBD_Kol1_Gr16c_s33232.DTOs;
using PJATK_APBD_Kol1_Gr16c_s33232.Exceptions;
using Microsoft.Data.SqlClient;


public class MakerService(IConfiguration config) : IMakerService
{
    private readonly string _cs = config.GetConnectionString("Default")!;

    public async Task<GetMakerDto> GetMakerAsync(int id)
    {
        await using var conn = new SqlConnection(_cs);
        await conn.OpenAsync();
        
        await using var cmd = new SqlCommand("""
            SELECT m.Id, m.Name, 
                   p.Id, p.Name, p.Description, p.StickerPrice, 
                   pt.Id, pt.Name, 
                   vp.VendorCode, v.Name, vp.Amount, vp.PricePerUnit
            FROM Makers m
            LEFT JOIN Products p ON m.Id = p.MakerId
            LEFT JOIN ProductTypes pt ON p.ProductTypeId = pt.Id
            LEFT JOIN VendorProducts vp ON p.Id = vp.ProductId
            LEFT JOIN Vendors v ON vp.VendorCode = v.Code
            WHERE m.Id = @Id
            """, conn);
        
        cmd.Parameters.AddWithValue("@Id", id);
        await using var reader = await cmd.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            throw new NotFoundException();

        var maker = new GetMakerDto
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1)
        };

        var productsDict = new Dictionary<int, GetProductDto>();

        do
        {
            if (reader.IsDBNull(2)) continue;

            int pId = reader.GetInt32(2);
            if (!productsDict.TryGetValue(pId, out var product))
            {
                product = new GetProductDto
                {
                    Id = pId,
                    Name = reader.GetString(3),
                    Description = reader.IsDBNull(4) ? null : reader.GetString(4),
                    StrickerPrice = reader.GetDecimal(5),
                    ProductType = new GetProductTypeDto
                    {
                        Id = reader.GetInt32(6),
                        Name = reader.GetString(7)
                    }
                };
                productsDict[pId] = product;
                maker.Products.Add(product);
            }

            if (!reader.IsDBNull(8))
            {
                product.Vendors.Add(new GetVendorDto
                {
                    Code = reader.GetString(8),
                    Name = reader.GetString(9),
                    Amount = reader.GetInt32(10),
                    PricePerUnit = reader.GetDecimal(11)
                });
            }
        } while (await reader.ReadAsync());

        return maker;
    }
    
    public Task AddMakerAsync(PostMakerDto dto) => throw new NotImplementedException();
}