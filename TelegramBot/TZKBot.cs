using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bots.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Polling;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;

namespace TelegramBot
{
    public class TZKBot
    {
        TelegramBotClient botClient = new TelegramBotClient("6500128752:AAESnmHzG59jUPeZxbsNJvB05Sjzip6WQ1E");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, receiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"Бот {botMe.Username} працює");
        }

        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Помилка: \n {apiRequestException.ErrorCode}" +
                $"\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null) 
            {
                await HandlerMessageAsync(botClient, update.Message);
            }
        }
        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            var arr = message.Text.Split(' ');
            if (arr.Length > 2)
            {
                for (int i = 1; i <= arr.Length - 2; i++)
                {
                    arr[i] = arr[i].Substring(0, arr[i].Length - 1);
                }
            }
            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вибір команд /command");
                return;
            }
            else if (message.Text == "/command")
            {
                ReplyKeyboardMarkup replaKeyboardMarkup = new(new[]
                { new KeyboardButton[] {"Пошук отелю", "Додати до списку", "Видалити з списку" }
                })
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Чим я можу допомогти?", replyMarkup: replaKeyboardMarkup);
                return;
            }
            else if (message.Text == "Пошук отелю")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть місто прибуття /getHotel (Місто)");
            }
            else if (message.Text == "Додати до списку")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть id отелю, який бажаєте додати");
            }
            else if (message.Text == "Видалити з списку")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Правильно блять");
            }
            else if (arr.Length > 1) 
            {
                if (arr[0] == "/getHotel" && arr[1] != null)
                {
                    foreach (var i in arr)
                    {
                        Console.WriteLine(i);
                    }
                } 
            }
        }
    }
}
