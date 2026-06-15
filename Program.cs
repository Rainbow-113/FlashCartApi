
using FlashcardAPI.Service;
using MongoDB.Driver;

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
            //add mongo
            builder.Services.AddSingleton<IMongoClient>(s =>
            new MongoClient(builder.Configuration["MongoDB:ConnectionString"])
            );
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<FolderService>();
            builder.Services.AddScoped<WordService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
        }
    }
}
