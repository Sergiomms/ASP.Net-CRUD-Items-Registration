using Microsoft.EntityFrameworkCore;
using pecasAPI.Data;

var MyAllowSpecificOrigins = "PecasOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });

});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddCors(options => options.AddPolicy(name: "PecasOrigins", policy => 
//{
//    policy.WithOrigins("http://localhost:5173/").AllowAnyMethod().AllowAnyHeader().AllowCredentials();

//}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(policy => policy.AllowAnyHeader()
//.AllowAnyMethod()
//.AllowCredentials()
//.WithOrigins("http://localhost:5173/"));

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
