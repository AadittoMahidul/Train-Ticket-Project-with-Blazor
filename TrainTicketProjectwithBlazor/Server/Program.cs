using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TrainTicketProjectwithBlazor.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TrainDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("db"), b=> b.MigrationsAssembly("TrainTicketProjectwithBlazor.Server")));


builder.Services.AddControllers()
     .AddNewtonsoftJson(option => {
         option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
         option.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
     });
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
