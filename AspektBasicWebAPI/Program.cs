using AspektBasicWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StructureContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AspektBasicWebAPI demo",
        Description = "Basic CRUD WebAPI using concepts for developing an API using ASP.NET Core and Entity Framework",
        TermsOfService = new Uri("https://www.example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Demo Contact",
            Url = new Uri("https://www.example.com/contact"),
        },
        License = new OpenApiLicense
        {
            Name = "Demo Contact",
            Url = new Uri("https://www.example.com/contact"),
        }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspektWebAPI EF Core Demo"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
