using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Authentication.Application.Interfaces;
using User.Authentication.Persistance.Data;
using User.Authentication.Infrastructure;
using User.Authentication.Application.Command.Handler;
using User.Authentication.Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"))
);
builder.Services.AddScoped<IIssueTokenInfra, IssueTokenInfra>();
builder.Services.AddScoped<IGetUserRepository,GetUserRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(LoginQueryHandler).Assembly);
});

builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
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
