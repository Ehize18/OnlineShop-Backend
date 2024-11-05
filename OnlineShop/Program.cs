using OnlineShop.Core.Interfaces;
using OnlineShop.DataBase.Redis;
using OnlineShop.DataBase.PostgreSQL;
using OnlineShop.Infrastructure.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OnlineShop.Infrastructure.Email;
using OnlineShop.Application.Services;
using StackExchange.Redis;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.DataBase.PostgreSQL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NUnit.Options;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.AddJsonFile("appsettings.json");
configuration.AddJsonFile("appsettings.Development.json");

builder.Services.AddControllers();

builder.Services.AddStackExchangeRedisCache(options =>
{
	options.ConfigurationOptions = new ConfigurationOptions()
	{
		EndPoints = new EndPointCollection { "172.17.0.3" },
		ConnectRetry = 3,
		ReconnectRetryPolicy = new LinearRetry(250),
		AbortOnConnectFail = false,
		ConnectTimeout = 500,
		SyncTimeout = 500
	};
	options.InstanceName = "OnlineStore";
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddDbContext<OnlineStoreDbContext>(
	options =>
	{
		options.UseNpgsql(configuration.GetConnectionString(nameof(OnlineStoreDbContext)));
	});

builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtOptions:SecretKey")))
		};
	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("OnlyForAdmin", policy =>
	{
		policy.RequireRole(new[] { "ADMIN" }).
		RequireAuthenticatedUser();
	});
});

builder.Services.Configure<EmailOptions>(configuration.GetSection(nameof(EmailOptions)));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IRedisCache, RedisCache>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IGoodCategoriesService, GoodCategoriesService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IGoodCategoriesRepository, GoodCategoriesRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
	o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header
	});
	o.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				 Reference = new OpenApiReference
				 {
					 Type = ReferenceType.SecurityScheme,
					 Id = "Bearer"
				 }
			},
			new string[] {}
		}
 });
});
builder.Services.AddStackExchangeRedisCache(option =>
{
	option.Configuration = builder.Configuration["redisconnection"];
});

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
	using (var DbContext = scope.ServiceProvider.GetRequiredService<OnlineStoreDbContext>())
	{
		if (DbContext.Database.GetPendingMigrations().Any())
		{
			DbContext.Database.Migrate();
		}
	}
}

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
