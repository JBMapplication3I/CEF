using GeneratePdfs.CefEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvcCore().AddRazorViewEngine();
//Fetching Connection string from APPSETTINGS.JSON
builder.Host.ConfigureServices((hc, sc) =>
{
    sc.AddDbContext<ClarityEcommerceEntities>(db => 
        db.UseSqlServer(hc.Configuration.GetConnectionString("DbContext"), 
        options => options.EnableRetryOnFailure(5))
    );
});
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
