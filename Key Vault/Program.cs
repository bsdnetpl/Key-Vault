using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Key_Vault.DB;
using Key_Vault.Service;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<DbConnection>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS")));
}

if (builder.Environment.IsProduction())
{
    var KeyVaultUrl = builder.Configuration.GetSection("KeyVault:VaultUri");
    var KeyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
    var KeyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
    var KeyVaultIsDirectoryId = builder.Configuration.GetSection("KeyVault:DirectoryID");

    var credential = new ClientSecretCredential(KeyVaultIsDirectoryId.Value!.ToString(), KeyVaultClientId.Value!.ToString(), KeyVaultClientSecret.Value!.ToString());
    builder.Configuration.AddAzureKeyVault(KeyVaultUrl.Value!.ToString(), KeyVaultClientId.Value!.ToString(), KeyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());
    var client = new SecretClient(new Uri(KeyVaultUrl.Value!.ToString()), credential);

    builder.Services.AddDbContext<DbConnection>(opt => opt.UseSqlServer(client.GetSecret("CS").Value.Value.ToString()));
}

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
