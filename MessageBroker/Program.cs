using MessageBroker;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
            builder =>
            {
                builder.WithOrigins("https://localhost:7246") // URL do seu frontend
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
});
builder.Services.AddSignalR();

var app = builder.Build();
app.UseCors("AllowAll");
app.MapHub<MessageHub>("message-hub");
app.Run();
