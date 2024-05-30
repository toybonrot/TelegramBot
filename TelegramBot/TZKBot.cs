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
using Microsoft.Extensions.Logging;

namespace TelegramBot
{
    public class TZKBot
    {
        TelegramBotClient botClient = new TelegramBotClient("6500128752:AAESnmHzG59jUPeZxbsNJvB05Sjzip6WQ1E");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        int k = 0, page = 1;
        double priceMax = 1000000;
        string filters = "none";
        SearchHotel hotels = new SearchHotel();
        List<BaseHotel> baseHotels = new List<BaseHotel>();
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
        InlineKeyboardMarkup inlineKeyboard2 = new InlineKeyboardMarkup(
    new List<InlineKeyboardButton[]>()
    {
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Попередній", "button4"),
                                            InlineKeyboardButton.WithCallbackData("Наступний", "button5"),
                                        },
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Видалити зі списку", "button6"),
                                        },
    });
        InlineKeyboardMarkup YesNo = new InlineKeyboardMarkup(
   new List<InlineKeyboardButton[]>()
   {
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Так", "button7"),
                                            InlineKeyboardButton.WithCallbackData("Ні", "button8"),
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
            else if (update.Type == UpdateType.CallbackQuery)
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
                await botClient.SendTextMessageAsync(message.Chat.Id, "Чим я можу допомогти?\n/GetHotel - введіть для детальнішої інформації\n" +
                    "/GetList - для виведення списку збережених отелів\n/GetFilters - для отримання можливих фільтрів\n" +
                    "/SetFilters - задати фільтр для пошуку", replyMarkup: replaKeyboardMarkup);
                return;
            }
            else if (message.Text == "Пошук отелю")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть місто прибуття /GetHotel " +
                    "\n{Місто}, {час прибуття}, {час відбуття} {опціонально максимальна ціна}\nМіста треба писати без пробілів та англійскою\n " +
                    "Приклад: /GetHotel Kyiv, 2024-12-01, 2024-12-31");
            }
            else if (message.Text == "Отримати список")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "/GetList - для пошуку отеля\n/GetFilters - для отримання можливих фільтрів" +
                    "\n/SetFilters - задати фільтр для пошуку");
            }
            else if (message.Text == "Знайти розваги в місті")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть /GetAttraction");
            }
            else if (arr.Length >= 1)
            {
                if (arr[0] == "/GetHotel")
                {
                    if (arr.Length >= 4 && arr.Length <= 5)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Шукаю...");
                        HotelClient hotelClient = new HotelClient();
                        k = 0;
                        page = 1;
                        if (arr.Length == 5)
                        {
                            priceMax = double.Parse(arr[4]);
                        }
                        else
                        {
                            priceMax = 10000000;
                        }
                        hotels = hotelClient.GetHotel(arr[1], arr[2], arr[3], message.Chat.Id, filters, priceMax, page).Result;
                        if (hotels.data.hotels.Length != 0)
                        {
                            //await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                            //    caption: $"{hotels.data.hotels[k].property.name}\n" +
                            //     $"Кількість зірок: {hotels.data.hotels[k].property.propertyClass}\nБал: {hotels.data.hotels[k].property.reviewScore:f}\n" +
                            //     $"Кількість оцінок: {hotels.data.hotels[k].property.reviewCount}\n" +
                            //     $"Ціна: {hotels.data.hotels[k].property.priceBreakdown.grossPrice.value:f} " +
                            //     $"{hotels.data.hotels[k].property.priceBreakdown.grossPrice.currency}\nId отеля: {hotels.data.hotels[k].property.id}",
                            //     replyMarkup: inlineKeyboard);
                            await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                                caption: $"{hotels.data.hotels[k].accessibilityLabel}", replyMarkup: inlineKeyboard);
                        }
                        else { await botClient.SendTextMessageAsync(message.Chat.Id, "Нічого не знайшов"); }
                    }
                    else { await botClient.SendTextMessageAsync(message.Chat.Id, "Приклад: /GetHotel Kyiv, 2024-12-01, 2024-12-31"); }

                }
                else if (arr[0] == "/GetList")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Зачекайте...");
                    HotelClient hotelClient = new HotelClient();
                    k = 0;
                    baseHotels = hotelClient.GetHotelList(message.Chat.Id).Result;
                    if (baseHotels.Count != 0 && k < baseHotels.Count)
                    {
                        await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri($"{baseHotels[k].PhotoURL}"),
                            caption: $"{baseHotels[k].Name}\n" +
                            $"Кількість зірок: {baseHotels[k].Stars}\nБал: {baseHotels[k].Score:f}\n" +
                            $"Кількість оцінок: {baseHotels[k].Reviews}\n" +
                            $"Ціна: {baseHotels[k].Price:f} " +
                            $"{baseHotels[k].Currency}\nId отеля: {baseHotels[k].Hotel_id}",
                            replyMarkup: inlineKeyboard2);
                    }
                    else { await botClient.SendTextMessageAsync(message.Chat.Id, "Список пустий"); }
                }
                else if (arr[0] == "/GetFilters")
                {
                    HotelClient hotelClient = new HotelClient();
                    List<string> availableFilters = hotelClient.GetFilters().Result;
                    string mess = "";
                    foreach (var e in availableFilters)
                    {
                        mess += $"{e}\n";
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, mess);

                }
                else if (arr[0] == "/SetFilters")
                {
                    if (arr.Length != 1)
                    {
                        var filtersArr = arr[1].Split(',');
                        HotelClient hotelClient = new HotelClient();
                        string temp = hotelClient.GetFilterId(filtersArr[0]).Result;
                        if (temp != "Фільтр не існує")
                        {
                            filters = temp;
                        }
                        for (int i = 1; i < filtersArr.Length; i++)
                        {
                            temp = hotelClient.GetFilterId(filtersArr[i]).Result;
                            if (temp == "Фільтр не існує")
                            {
                                break;
                            }
                            else
                            {
                                filters += $",{temp}";
                            }                           
                        }
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Додав філтр");
                    }
                    else { await botClient.SendTextMessageAsync(message.Chat.Id, "Приклад: /SetFilters Hotel,5Star"); }
                }
                else if (arr[0] == "/ResetFilters")
                {
                    filters = "none";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Фільтр оновлений");
                }
                else if (message.Text == "Так" && hotels.data != null)
                {
                    k = 0;
                    HotelClient hotelClient = new HotelClient();
                    hotels = hotelClient.GetHotel(arr[1], arr[2], arr[3], message.Chat.Id, filters, priceMax, ++page).Result;
                    await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                                caption: $"{hotels.data.hotels[k].accessibilityLabel}", replyMarkup: inlineKeyboard);
                }
                else if (message.Text == "Ні" && hotels.data != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "А нахуй я нужен");
                }
            }
        }
        private async Task HanlderCallBackQuertAsync(ITelegramBotClient botClient, CallbackQuery callback)
        {

            if (callback.Data == "button1" && hotels != null)
            {
                if (k != 0)
                {
                    k--;
                    //await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                    //        caption: $"{hotels.data.hotels[k].property.name}\n" +
                    //        $"Кількість зірок: {hotels.data.hotels[k].property.propertyClass}\nБал: {hotels.data.hotels[k].property.reviewScore:f}\n" +
                    //        $"Кількість оцінок: {hotels.data.hotels[k].property.reviewCount}\n" +
                    //        $"Ціна: {hotels.data.hotels[k].property.priceBreakdown.grossPrice.value:f} " +
                    //        $"{hotels.data.hotels[k].property.priceBreakdown.grossPrice.currency}\nId отеля: {hotels.data.hotels[k].property.id}",
                    //        replyMarkup: inlineKeyboard);
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                                caption: $"{hotels.data.hotels[k].accessibilityLabel}", replyMarkup: inlineKeyboard);
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Це початок списку"); } 
            }
            else if (callback.Data == "button2" && hotels.data != null)
            {
                k++;
                if (k < hotels.data.hotels.Length)
                {
                    //await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                    //        caption: $"{hotels.data.hotels[k].property.name}\n" +
                    //        $"Кількість зірок: {hotels.data.hotels[k].property.propertyClass}\nБал: {hotels.data.hotels[k].property.reviewScore:f}\n" +
                    //        $"Кількість оцінок: {hotels.data.hotels[k].property.reviewCount}\n" +
                    //        $"Ціна: {hotels.data.hotels[k].property.priceBreakdown.grossPrice.value:f} " +
                    //        $"{hotels.data.hotels[k].property.priceBreakdown.grossPrice.currency}\nId отеля: {hotels.data.hotels[k].property.id}",
                    //        replyMarkup: inlineKeyboard);
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                                caption: $"{hotels.data.hotels[k].accessibilityLabel}", replyMarkup: inlineKeyboard);
                }
                //else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Неможливо виконати"); } // Додати про наступний список
                else 
                {
                    ReplyKeyboardMarkup replaKeyboardMarkup = new(new[]
               { new KeyboardButton[] {"Так", "Ні" }
                })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Це кінець сторінки. Відкрити наступну?", replyMarkup: replaKeyboardMarkup);
                }
            }
            else if (callback.Data == "button3" && hotels != null)
            {
                if (k < hotels.data.hotels.Length)
                {
                    HotelClient hotelClient = new HotelClient();
                    hotelClient.InsertHotel(hotels, callback.Message.Chat.Id, k);
                    await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Додав");
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Неможливо виконати"); }
            }
            else if (callback.Data == "button4" && baseHotels != null)
            {
                if (k != 0)
                {
                    k--;
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{baseHotels[k].PhotoURL}"),
                        caption: $"{baseHotels[k].Name}\n" +
                        $"Кількість зірок: {baseHotels[k].Stars}\nБал: {baseHotels[k].Score:f}\n" +
                        $"Кількість оцінок: {baseHotels[k].Reviews}\n" +
                        $"Ціна: {baseHotels[k].Price:f} " +
                        $"{baseHotels[k].Currency}\nId отеля: {baseHotels[k].Hotel_id}",
                        replyMarkup: inlineKeyboard2);
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Це початок списку"); }

            }
            else if (callback.Data == "button5" && baseHotels != null)
            {
                if (k < baseHotels.Count)
                {
                    k++;
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{baseHotels[k].PhotoURL}"),
                        caption: $"{baseHotels[k].Name}\n" +
                        $"Кількість зірок: {baseHotels[k].Stars}\nБал: {baseHotels[k].Score:f}\n" +
                        $"Кількість оцінок: {baseHotels[k].Reviews}\n" +
                        $"Ціна: {baseHotels[k].Price:f} " +
                        $"{baseHotels[k].Currency}\nId отеля: {baseHotels[k].Hotel_id}",
                        replyMarkup: inlineKeyboard2);
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Це кінець списку"); }

            }
            else if (callback.Data == "button6" && baseHotels != null)
            {
                if (k < baseHotels.Count)
                {
                    HotelClient hotelClient = new HotelClient();
                    hotelClient.DeleteHotel(baseHotels, callback.Message.Chat.Id, k);
                    await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Видалив");
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Неможливо виконати"); }
            }
        }
    }
}
