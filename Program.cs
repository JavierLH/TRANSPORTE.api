using API.Services;
using APIV2.Repositorio;
using APIV2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SiilContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddHttpContextAccessor();

Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("SILLAdministrador@@"));

builder.Services.AddScoped<UsuariosSistemaService>();
builder.Services.AddScoped<RolesService>();
builder.Services.AddScoped<ProgramacionViajesService>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<EjecutivosService>();
builder.Services.AddScoped<ConvenioService>();
builder.Services.AddScoped<PizarraService>();
builder.Services.AddScoped<ClientesService>();
builder.Services.AddScoped<OperadoresService>();
builder.Services.AddScoped<OrigenesDestinosService>();
builder.Services.AddScoped<TarifarioService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaAngular", app =>
    {
        app.WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("PoliticaAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
