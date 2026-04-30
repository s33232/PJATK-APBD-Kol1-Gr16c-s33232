using PJATK_APBD_Kol1_Gr16c_s33232.DTOs;
using PJATK_APBD_Kol1_Gr16c_s33232.Exceptions;
using PJATK_APBD_Kol1_Gr16c_s33232.Services;
using Microsoft.AspNetCore.Mvc;
namespace PJATK_APBD_Kol1_Gr16c_s33232.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MakersController(IMakerService service) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return Ok(await service.GetMakerAsync(id));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(PostMakerDto dto)
    {
        await service.AddMakerAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }
}