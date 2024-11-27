using api.Interfaces;
using api.Repositories;
using api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAppUsersRepo, AppUserRepo>(provider =>
{
    return new AppUserRepo(builder.Configuration.GetConnectionString("MySQL") ?? "");
});

builder.Services.AddScoped<ITokenService, TokenService>(provider =>
{
    return new TokenService(builder.Configuration["TokenKey"] ?? "");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", builder =>
    {
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:4200");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Angular");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
