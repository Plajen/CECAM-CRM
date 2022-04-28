using CECAM.API.IoC;
using CECAM.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.

builder.Services.AddSingleton(builder.Configuration);

IoCConfiguration.Register(builder.Services, builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy",
        b => b.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddDbContext<CECAMDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "CECAM Web Api",
            Version = "v1",
            Description = "Project for CECAM in ASP.NET Core",
            Contact = new OpenApiContact { Name = "Jean Pereira Lourenço" }
        });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

#endregion

var app = builder.Build();

#region Configure Middlewares (HTTP Request Pipeline)

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CECAM.API v1"));
}

app.UseCors("corsPolicy");

app.UseHttpsRedirection();

app.UseRouting();

app.UseDeveloperExceptionPage();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

#endregion

app.Run();
