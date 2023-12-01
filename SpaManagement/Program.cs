using SpaManagement.Domain.Entities;
using SpaManagement.Infrastructure.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Database
builder.Services.RegisterContextDb(builder.Configuration);

//Register Dependency Injection
builder.Services.RegisterDI(builder.Configuration);

//Register Authentication Token
builder.Services.RegisterTokenBear(builder.Configuration);

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "MyPolicy", builder =>
        {
            builder.AllowAnyOrigin().
                    AllowAnyMethod().
                    AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//allow react localhost:3000 acceess to endpoint
app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
