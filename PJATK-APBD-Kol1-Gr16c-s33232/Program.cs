using PJATK_APBD_Kol1_Gr16c_s33232.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IMakerService, MakerService>();

var app = builder.Build();

app.MapControllers();

app.Run();