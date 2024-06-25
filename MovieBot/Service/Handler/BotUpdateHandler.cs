using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
// using TelegramBackgroundService.Bot.Models;
// using TelegramBackgroundService.Bot.Service.UserServices;

namespace TelegramBackgroundService.Bot.Service.Handler
{
    public class BotUpdateHandler : IUpdateHandler
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<BotUpdateHandler> logger;

        public BotUpdateHandler(IServiceScopeFactory serviceScopeFactory, ILogger<BotUpdateHandler> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        public BotUpdateHandler(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            // Echo received message text
            await HandleMessageAsync(botClient, message, cancellationToken);
        }

        private async Task HandleMessageAsync(ITelegramBotClient botClient, Message message,
            CancellationToken cancellationToken)
        {
            try
            {
                // await botClient.SendTextMessageAsync(
                //     chatId: message.Chat.Id,
                //     text: $"You said:\n<i>{message.Text}</i>",
                //     parseMode: ParseMode.Html,
                //     cancellationToken: cancellationToken);
                
                await botClient.CopyMessageAsync(
                    chatId: 1633746526,
                    fromChatId: -1002204118530,
                    messageId: 10,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        
        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }
    }
}
