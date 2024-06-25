using Microsoft.EntityFrameworkCore;
using MovieBot.Persistence;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramBackgroundService.Bot.Service.Handler;
using TelegramBackgroundService.Bot.Service.MyBackgroundService;

var builder = WebApplication.CreateBuilder(args);

//service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//BackgroundService
builder.Services.AddHostedService<MyBackgroundService>();
// builder.Services.AddHostedService<Test>();

//Singleton
builder.Services.AddSingleton<IUpdateHandler, BotUpdateHandler>();

// //DbContext
// builder.Services.AddDbContext<MovieDbContext>(options => 
//     options.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

//Telegram
builder.Services.AddSingleton(p =>
    new TelegramBotClient(builder.Configuration.GetValue("BotToken", string.Empty) ?? string.Empty));

var app = builder.Build();
app.Run();