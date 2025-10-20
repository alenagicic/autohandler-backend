using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore; // Needed for AddOpenApi and MapOpenApi

var builder = WebApplication.CreateBuilder(args);

const string AllowAllPolicy = "AllowAll";

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllPolicy,
        policy =>
        {
            policy.AllowAnyOrigin() 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(AllowAllPolicy); 

app.UseAuthorization();

app.MapControllers();

app.Run();
