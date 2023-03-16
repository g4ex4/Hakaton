using Hakaton.Application;
using Hakaton.Application.Common.Mappings;
using Hakaton.Application.Interfaces;
using Hakaton.Application.Users.Handlers;
using Hakaton.Domain;
using Hakaton.Persistance;
using Hakaton.WebApi.Middleware;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();




// Add services to the container.
builder.Services.AddApplication();
var scope = builder.Services.BuildServiceProvider().CreateScope();
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});
builder.Services.AddControllers();
builder.Services.AddTransient<RegisterUserHandler>();
builder.Services.AddTransient<LoginUserHandler>();
builder.Services.AddTransient<AuthManager>();
var mediatr = new ServiceCollection();
builder.Services.AddMediatR(typeof(Program).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseCustomExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
