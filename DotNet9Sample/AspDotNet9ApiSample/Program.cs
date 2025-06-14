using AspDotNet9ApiSample.Configuration;
using AspDotNet9ApiSample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterInfrastructureServices(builder.Configuration);
builder.Services.AddResponseCompression();
builder.Services.AddScoped<SearchCountService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseResponseCompression();

// Configure Swagger UI localhost:port/swagger/
app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "Swagger UI"));

app.UseAuthorization();

app.MapControllers();

app.Run();
