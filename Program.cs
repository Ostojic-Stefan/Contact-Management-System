using Contacts.Data;
using Contacts.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
