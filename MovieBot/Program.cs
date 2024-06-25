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
builder.Services.AddSingleton(p =>
    new TelegramBotClient("7171508471:AAFNSo6lY2NilbhNe-Zlezq7M5QVhVNiKYM")
);

var app = builder.Build();
app.Run();