using FlashcardAPI.Repositories;
using FlashcardAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;  
using Microsoft.IdentityModel.Tokens;                  
using MongoDB.Driver;
using System.Text;                                     

namespace FlashcardAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "mongodb+srv://nguyenvanddan113_db_user:Dan2005113@cluster0.5eezoni.mongodb.net/?appName=Cluster0";
            string databaseName = "rememvoca";

            var builder = WebApplication.CreateBuilder(args);

           
            var mongoClient = new MongoClient(connectionString);
            builder.Services.AddSingleton<IMongoClient>(mongoClient);
            builder.Services.AddScoped<IMongoDatabase>(sp => mongoClient.GetDatabase(databaseName));

            builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
            builder.Services.AddScoped<Folder_Repo>();
            builder.Services.AddScoped<Word_Repo>();
            builder.Services.AddScoped<FolderService>();
            builder.Services.AddScoped<WordService>();
            builder.Services.AddScoped<User_Repo>();
            builder.Services.AddScoped<AuthService>();

            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                    };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            var app = builder.Build(); 

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
        }
    }
}