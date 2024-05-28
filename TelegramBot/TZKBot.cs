using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot;
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
        int k = 0;
        SearchHotel hotels = new SearchHotel();
        InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(
    new List<InlineKeyboardButton[]>()
    {
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Попередній", "button1"),
                                            InlineKeyboardButton.WithCallbackData("Наступний", "button2"),
                                        },
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Додати до списку", "button3"),
                                        },
    });
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
            if (update.Type == UpdateType.CallbackQuery)
            {
                await HanlderCallBackQuertAsync(botClient, update.CallbackQuery);
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
                { new KeyboardButton[] {"Пошук отелю", "Отримати список", "Знайти розваги в місті" }
                })
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Чим я можу допомогти?", replyMarkup: replaKeyboardMarkup);
                return;
            }
            else if (message.Text == "Пошук отелю")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть місто прибуття /GetHotel {Місто}, {час прибуття}, {час відбуття}");
            }
            else if (message.Text == "Отримати список")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть /GetList");
            }
            else if (message.Text == "Знайти розваги в місті")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть /GetAttraction");
            }
            else if (arr.Length > 1)
            {
                if (arr[0] == "/GetHotel" && arr[1] != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Шукаю...");
                    HotelClient hotelClient = new HotelClient();
                    k = 0;
                    hotels = hotelClient.GetHotel(arr[1], arr[2], arr[3], message.Chat.Id).Result;
                    await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"), caption: $"{hotels.data.hotels[k].property.name}\n" +
                        $"Загальний опис: ({hotels.data.hotels[k].accessibilityLabel})\n\n" +
                    $"Кількість зірок: {hotels.data.hotels[k].property.propertyClass}\nБал: {hotels.data.hotels[k].property.reviewScore}\n" +
                    $"Кількість оцінок: {hotels.data.hotels[k].property.reviewCount}\n" +
                    $"Ціна: {hotels.data.hotels[k].property.priceBreakdown.grossPrice.value} " +
                    $"{hotels.data.hotels[k].property.priceBreakdown.grossPrice.currency}\nId отеля: {hotels.data.hotels[k].property.id}",
                    replyMarkup: inlineKeyboard);
                }
                else if (arr[0] == "/GetList")
                {

                }
            }
        }
        private async Task HanlderCallBackQuertAsync(ITelegramBotClient botClient, CallbackQuery callback)
        {

            if (callback.Data == "button1" && k != 0)
            {
                k--;
                await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"), 
                    caption: $"{hotels.data.hotels[k].property.name}\nЗагальний опис: ({hotels.data.hotels[k].accessibilityLabel})\n\n" +
                    $"Кількість зірок: {hotels.data.hotels[k].property.propertyClass}\nБал: {hotels.data.hotels[k].property.reviewScore}\n" +
                    $"Кількість оцінок: {hotels.data.hotels[k].property.reviewCount}\n" +
                    $"Ціна: {hotels.data.hotels[k].property.priceBreakdown.grossPrice.value} " +
                    $"{hotels.data.hotels[k].property.priceBreakdown.grossPrice.currency}\nId отеля: {hotels.data.hotels[k].property.id}", 
                    replyMarkup: inlineKeyboard);
            }
            else if (callback.Data == "button2" && k <= hotels.data.hotels.Length && hotels != null)
            {
                k++;
                await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"), 
                    caption: $"{hotels.data.hotels[k].property.name}\nЗагальний опис: ({hotels.data.hotels[k].accessibilityLabel})\n\n" +
                    $"Кількість зірок: {hotels.data.hotels[k].property.propertyClass}\nБал: {hotels.data.hotels[k].property.reviewScore}\n" +
                    $"Кількість оцінок: {hotels.data.hotels[k].property.reviewCount}\n" +
                    $"Ціна: {hotels.data.hotels[k].property.priceBreakdown.grossPrice.value} " +
                    $"{hotels.data.hotels[k].property.priceBreakdown.grossPrice.currency}\nId отеля: {hotels.data.hotels[k].property.id}",
                    replyMarkup: inlineKeyboard);
            }
            else if (callback.Data == "button3" && k <= hotels.data.hotels.Length && hotels != null)
            {
                HotelClient hotelClient = new HotelClient();
                hotelClient.InsertHotel(hotels, callback.Message.Chat.Id, k);
                await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Додав");
            }
        }
    }
}
