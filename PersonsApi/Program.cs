using Microsoft.EntityFrameworkCore;
using PersonsApi.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PersonContext>(opt => opt.UseInMemoryDatabase("Persons"));
builder.Services.AddScoped<IPersonsRepository, PersonsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
