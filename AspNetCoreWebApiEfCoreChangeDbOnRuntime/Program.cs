using AspNetCoreWebApiEfCoreChangeDbOnRuntime;
using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(f => f.OperationFilter<CustomHeaderSwaggerAttribute>());

builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddDbContext<MyContext>(ServiceLifetime.Transient);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
