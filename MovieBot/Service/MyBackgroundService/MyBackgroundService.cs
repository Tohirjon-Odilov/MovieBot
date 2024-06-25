using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TelegramBackgroundService.Bot.Service.MyBackgroundService
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly TelegramBotClient client;
        private readonly IUpdateHandler updateHandler;

        public MyBackgroundService(TelegramBotClient client, IUpdateHandler updateHandler)
        {
            this.client = client;
            this.updateHandler = updateHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            client.StartReceiving(
                updateHandler: updateHandler.HandleUpdateAsync,
                pollingErrorHandler: updateHandler.HandlePollingErrorAsync,
                receiverOptions: new ReceiverOptions()
                {
                    ThrowPendingUpdates = true
                },
                cancellationToken: stoppingToken);
        }
    }
}