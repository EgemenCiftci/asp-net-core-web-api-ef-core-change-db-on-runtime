using AspNetCoreWebApiEfCoreChangeDbOnRuntime;
using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Models;
using AspNetCoreWebApiEfCoreChangeDbOnRuntime.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ConnectionService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(f => f.OperationFilter<CustomHeaderSwaggerAttribute>());
builder.Services.AddDbContext<MyContext>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
