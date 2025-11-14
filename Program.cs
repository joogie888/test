using VMS.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ⭐ IMPORTANT: Register DatabaseService HERE
builder.Services.AddScoped<DatabaseService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMAUI",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowMAUI");

app.UseAuthorization();

app.MapControllers();

app.Run();