
using Assignment_UserEntity.Service;
using Assignment_UserEntity.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_UserEntity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddControllers();

            //userEntity service
            builder.Services.AddScoped<IUserEntityService, UserEntityService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
        }
    }
}
