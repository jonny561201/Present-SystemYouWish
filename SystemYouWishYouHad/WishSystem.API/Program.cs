using System.API.Config;
using System.API.Endpoints;
using System.Shared.Config;
using System.Data.Config;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddOpenApi();
builder.Services.AddCorsConfig(settings);
builder.Services.AddUserDbContext(settings);
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.RegisterEndpoints();

app.Run();
