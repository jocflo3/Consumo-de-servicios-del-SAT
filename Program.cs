using Descargar_CFDIS.Interfaces;
using Descargar_CFDIS.Interfaces.Repository;
using Descargar_CFDIS.Interfaces.Service;
using Descargar_CFDIS.Repositories;
using Descargar_CFDIS.SAT.Builders;
using Descargar_CFDIS.SAT.Clients;
using Descargar_CFDIS.SAT.Parsers;
using Descargar_CFDIS.SAT.Security;
using Descargar_CFDIS.Services;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IUsuarioRepositoy, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ISatActionsRepository, SatActionsRepository>();
builder.Services.AddScoped<ISatActionsService, SatActionsService>();
builder.Services.AddHttpClient<SatSoapClient>();
builder.Services.AddScoped<CertificateService>();
builder.Services.AddScoped<SatAuthXmlBuilder>();
builder.Services.AddScoped<AuthResponseParser>();
builder.Services.AddHttpClient<SatSoapClient>();
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath,"Keys")));
builder.Services.AddScoped<PasswordProtector>();

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
