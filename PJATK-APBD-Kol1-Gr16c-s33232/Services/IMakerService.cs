using PJATK_APBD_Kol1_Gr16c_s33232.DTOs;
namespace PJATK_APBD_Kol1_Gr16c_s33232.Services;

public interface IMakerService
{
    Task<GetMakerDto> GetMakerAsync(int id);
    Task AddMakerAsync(PostMakerDto dto);
}