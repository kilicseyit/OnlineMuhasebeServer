using Microsoft.OpenApi.Models;
using System.Reflection.Metadata;
using static System.Net.WebRequestMethods;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);//burada aktarma iþlemini yaptýk
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
{
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
};
    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id,jwtSecuritySheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecuritySheme, Array.Empty<string>() }
    });
   
});

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
