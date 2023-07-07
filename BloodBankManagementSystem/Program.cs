using BusinessAccessLayer.Services;
using DataAccessLayer.Contracts;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        }
        );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add the service
builder.Services.AddScoped<IDAL_RegisterUser_repository, DAL_RegisterUser_repository>();
builder.Services.AddScoped<IServiceRegisterUser, ServiceRegisterUser>();

builder.Services.AddScoped<IDAL_BloodAvailability_repository, DAL_BloodAvailability_repository>();
builder.Services.AddScoped<IServiceBloodAvailability, ServiceBloodAvailability>();

builder.Services.AddScoped<IDAL_RequestBloodAndCheck_repository, DAL_RequestBloodAndCheck_repository>();
builder.Services.AddScoped<IServiceRequestBlood, ServiceRequestBlood>();

builder.Services.AddScoped<IDAL_ApproveReject_repository, DAL_ApproveReject_repository>();
builder.Services.AddScoped<IServiceApproveReject, ServiceApproveReject>();


builder.Services.AddScoped<IDAL_BusinessDonateBloodAndCheck_repository, DAL_DonateBloodAndCheck_repository>();
builder.Services.AddScoped<IServiceBusinessDonateBloodAndCheck, ServiceBusinessDonateBloodAndCheck>();




var app = builder.Build();

// Configure the HTTP request pipeline.
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
