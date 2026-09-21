using WishSystem.API.Config;
using WishSystem.API.Endpoints;
using WishSystem.Data.Config;
using WishSystem.External.Config;
using WishSystem.Shared.Config;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Configuration.Get<AppSettings>();

builder.Services.AddOpenApi();
builder.Services.AddUserDbContext(settings);
builder.Services.AddServices();
builder.Services.AddExternalClients(settings);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.RegisterEndpoints();

app.Run();
