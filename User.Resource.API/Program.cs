using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User.Resource.Application.CQRS.Command;
using User.Resource.Domain.Interfaces;
using User.Resource.Persistance.Data;
using User.Resource.Persistance.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(SetUserCommand).Assembly);
});

builder.Services.AddScoped<IDataGetter,DataGetter>(); 
builder.Services.AddScoped<IDataSetter,DataSetter>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"))
);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Must match the issuer in token generation
            ValidAudience = builder.Configuration["Jwt:Audience"], // Must match the audience in token generation
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));


    //admin policy given that the role is admin or expiry time is greater than 5 minutes






});
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
