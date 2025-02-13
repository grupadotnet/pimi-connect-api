using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using pimi_connect_api.Attachment;
using pimi_connect_api.Hubs;
using pimi_connect_api.Message;
using pimi_connect_api.Middleware;
using pimi_connect_api.Services;
using pimi_connect_api.Services.Interfaces;
using pimi_connect_api.User;
using pimi_connect_api.UserChat;
using pimi_connect_app.Data.AppDbContext;
using pimi_connect_app.Data.Entities;
using pimi_connect_app.Data.Models;
using pimi_connect_app.Data.Models.Validators;
using pimi_connect_app.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

#region Inject Services

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Middleware
builder.Services.AddScoped<ErrorHandlingMiddleware>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUserChatService, UserChatService>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IUserChatRepository, UserChatRepository>();

// Validators
builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();

builder.Services.AddSignalR();

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

app.MapHub<UserHub>("/hubs/user");

app.Run();

#endregion
