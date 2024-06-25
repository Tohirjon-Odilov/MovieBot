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
            // if (message.Text is not { } messageText)
            //     return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a 'messageText' message in chat {chatId}.");

            // Echo received message text
            var messageHandler = update.Message.Type switch
            {
                MessageType.Text => HandleMessageAsync(botClient, update, cancellationToken),
                // MessageType.Location => HandleLocationAsync(botClient, update, cancellationToken),
                // MessageType.Contact => HandleContactAsync(botClient, update, cancellationToken),
                // MessageType.Audio => HandleAudioAsync(botClient, update, cancellationToken),
                // MessageType.Sticker => HandlerStrikerAsync(botClient, update, cancellationToken),
                // MessageType.Photo => HandlePhotoAsync(botClient, update, cancellationToken),
                // MessageType.Dice => HandleDiceAsync(botClient, update, cancellationToken),
                // MessageType.Document => HandleDocumentAsync(botClient, update, cancellationToken),
                // MessageType.Game => HandleGameAsync(botClient, update, cancellationToken),
                // MessageType.Invoice => HandleInvoiceAsync(botClient, update, cancellationToken),
                // MessageType.Poll => HandlePollAsync(botClient, update, cancellationToken),
                // MessageType.Voice => HandleVoiceAsync(botClient, update, cancellationToken),
                // MessageType.VideoNote => HandleVideoNote(botClient, update, cancellationToken),
                // MessageType.WebAppData => HandleWebAppDataAsync(botClient, update, cancellationToken),
                MessageType.Video => HandleVideoAsync(botClient, update, cancellationToken),
                _ => HandleUnknownMessageAsync(botClient, update, cancellationToken)
            };
            
            await messageHandler;
            // await HandleMessageAsync(botClient, message, cancellationToken);
        }

        private async Task HandleMessageAsync(
            ITelegramBotClient botClient, 
            Update update,
            CancellationToken cancellationToken)
        {
            try
            {
                await botClient.CopyMessageAsync(
                    chatId: update.Message.Chat.Id,
                    fromChatId: -1002204118530,
                    messageId: 42,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private async Task HandleVideoAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await botClient.CopyMessageAsync(
                chatId: new ChatId(
                    -1002204118530
                ),
                fromChatId: update.Message.Chat.Id,
                messageId: update.Message.MessageId,
                caption: update.Message.Caption + "Maxsus kod => "+update.Message.MessageId + "\nMurojaat uchun => @t_odilov \nFilm ushbu bot orqali yuklab olindi => @tarjima_film_2024_bot",
                cancellationToken: cancellationToken);
        }
        
        private async Task HandleUnknownMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Sorry, I didn't understand that command.",
                cancellationToken: cancellationToken);
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
