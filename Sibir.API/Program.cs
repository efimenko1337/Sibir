using Microsoft.OpenApi.Models;
using Sibir.BL.Services;
using Sibir.DAL;
using Sibir.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<SqlServerContext>();
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<CRUDProjectService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sibers", Version = "v1" });
    c.CustomSchemaIds(t=>t.FullName);
});

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.Run();
