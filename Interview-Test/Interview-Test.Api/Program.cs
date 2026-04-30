using Interview_Test.Infrastructure;
using Interview_Test.Middlewares;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "x-api-key",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKApiKeySchemeey"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                Scheme = "ApiKeyScheme",
                Name = "ApiKey",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<AuthenMiddleware>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<InterviewTestDbContext>(options =>
    {
        options.UseSqlServer(connection,
            sqlOptions =>
            {
                sqlOptions.UseCompatibilityLevel(110);
                sqlOptions.CommandTimeout(30);
                sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
            });
    }
);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InterviewTestDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<AuthenMiddleware>();
app.UseMvc();
app.Run();