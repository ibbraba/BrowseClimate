using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BrowseClimate.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(config);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();


builder.Services.AddControllers();
/*
builder.Services.AddCors(p => p.AddPolicy("localhost", builder =>
{
    builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
}));
*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





string keyVaultUrl = builder.Configuration.GetValue<string>("KeyVaultConfig:KVUrl")!;
string tenantId = builder.Configuration.GetValue<string>("KeyVaultConfig:TenantID")!;
string clientID= builder.Configuration.GetValue<string>("KeyVaultConfig:ClientID")!;
string clientSecret = builder.Configuration.GetValue<string>("KeyVaultConfig:ClientSecretID")!;

var credential = new ClientSecretCredential(tenantId, clientID, clientSecret);
var client = new SecretClient(new Uri(keyVaultUrl), credential);



var secret = await client.GetSecretAsync("bclocaldb");



DBHelper._cnn = secret.Value.Value; 







var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("localhost");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
