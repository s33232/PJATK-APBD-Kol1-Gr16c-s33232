using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace PJATK_APBD_Kol1_Gr16c_s33232.Exceptions;

public class NotFoundException(string message = "Not found") : Exception(message);
