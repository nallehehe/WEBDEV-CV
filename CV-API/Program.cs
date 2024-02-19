
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBDEV_CV.Data;
using WEBDEV_CV.Models;

namespace CV_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/skills/{id}", async ([FromRoute] int id, [FromServices] ApplicationDbContext dbContext) =>
            {
                var book = await dbContext.Skills.FindAsync(id);

                if (book == null)
                {
                    return Results.NotFound("Unable to find that skill.");
                }

                return Results.Ok(book);
            });

            app.MapGet("/skills", async ([FromServices] ApplicationDbContext dbContext) =>
            {
                var books = await dbContext.Skills.ToListAsync();
                return Results.Ok(books);
            });

            app.MapPost("/skill", async ([FromBody] Skills book, [FromServices] ApplicationDbContext dbContext) =>
            {
                dbContext.Skills.Add(book);
                await dbContext.SaveChangesAsync();
                return Results.Ok(book);
            });

            app.MapPut("/skills/{id}", async ([FromRoute] int id, [FromBody] Skills skills, [FromServices] ApplicationDbContext dbContext) =>
            {
                Skills? updatedSkills = await dbContext.Skills.FindAsync(id);

                if (updatedSkills == null)
                {
                    return Results.NotFound("Unable to find that skill.");
                }

                updatedSkills.Name = skills.Name;
                updatedSkills.Experience = skills.Experience;
                updatedSkills.Level = skills.Level;

                await dbContext.SaveChangesAsync();
                return Results.Ok(skills);
            });

            app.MapDelete("/skills/{id}", async ([FromRoute] int id, [FromServices] ApplicationDbContext dbContext) =>
            {
                var book = await dbContext.Skills.FindAsync(id);

                if (book == null)
                {
                    return Results.Ok();
                }

                dbContext.Skills.Remove(book);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });

            app.Run();
        }
    }
}
