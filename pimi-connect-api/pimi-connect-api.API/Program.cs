using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Middleware;
using pimi_connect_api.Services;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_app.Data.AppDbContext;

var builder = WebApplication.CreateBuilder(args);

#region Inject Services

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();


#region Add Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Add AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
        options.UseNpgsql(builder.Configuration.GetConnectionString("pimi-connect-postgresql"),
        b => b.MigrationsAssembly("pimi-connect-api.API"));
});

#endregion

#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
