using Microsoft.EntityFrameworkCore;
using MediatR;
using CQRS_Demo.Data;
using CQRS_Demo.Commands;
using CQRS_Demo.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("CqrsExampleDb"));
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
