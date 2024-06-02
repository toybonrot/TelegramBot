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
using Microsoft.Extensions.Logging;
using TelegramBot.Clients;
using TelegramBot.Models;

namespace TelegramBot
{
    public class TZKBot
    {
        TelegramBotClient botClient = new TelegramBotClient("6500128752:AAESnmHzG59jUPeZxbsNJvB05Sjzip6WQ1E");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        int k , page;
        double priceMax;
        string currency;
        string filters = "none", arrival, departure, city;
        SearchHotel hotels = new SearchHotel();
        SearchAttraction attraction = new SearchAttraction();
        List<BaseAttraction> baseAttractions = new List<BaseAttraction>();
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
                                            InlineKeyboardButton.WithCallbackData("Більше інформації", "button7")
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
                                            InlineKeyboardButton.WithCallbackData("Більше інформації", "button8"),
                                        },
    });
        InlineKeyboardMarkup inlineKeyboard3 = new InlineKeyboardMarkup(
   new List<InlineKeyboardButton[]>()
   {
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Попередній", "button9"),
                                            InlineKeyboardButton.WithCallbackData("Наступний", "button10"),
                                        },
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Додати до списку", "button11"),
                                            InlineKeyboardButton.WithCallbackData("Більше інформації", "button12")
                                        },
   });
        InlineKeyboardMarkup inlineKeyboard4 = new InlineKeyboardMarkup(
new List<InlineKeyboardButton[]>()
{
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Попередній", "button13"),
                                            InlineKeyboardButton.WithCallbackData("Наступний", "button114"),
                                        },
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Видалити зі списку", "button15"),
                                            InlineKeyboardButton.WithCallbackData("Більше інформації", "button16")
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
                { new KeyboardButton[] {"Пошук готелю", "Отримати список", "Знайти розваги в місті" }
                })
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Чим я можу допомогти?\n/GetHotel - введіть для детальнішої інформації\n" +
                    "/GetHotelList - для виведення списку збережених отелів\n/GetFilters - для отримання можливих фільтрів\n" +
                    "/SetFilters - задати фільтр для пошуку\n/ResetFilters - скидання фільтрів\n/GetAttraction - для пошуку розваг в місті\n" +
                    "/SetDate - для задання дати, щоб отримати більше інформіції про отель в цей час (для готелів зі списку)"
                    , replyMarkup: replaKeyboardMarkup);
                return;
            }
            else if (message.Text == "Пошук готелю")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть місто прибуття /GetHotel " +
                    "\n{Місто}, {час прибуття}, {час відбуття}, {опціонально валюта(зазвичай UAH)}, {опціонально максимальна ціна}" +
                    "\nМіста треба писати без пробілів та англійскою\n " +
                    "Приклад: /GetHotel Kyiv, 2024-12-01, 2024-12-31, UAH, 100000");
            }
            else if (message.Text == "Отримати список")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "/GetList - виведення списку збережених отелів ");
            }
            else if (message.Text == "Знайти розваги в місті")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть /GetAttraction {Місто}, {опціонально валюта(зазвичай UAH)}, " +
                    "{опціонально дата прибуття}, {опціоналтно дата відбуття}\n" +
                    "Міста треба писати без пробілів та англійскою\nПриклад: /GetAttraction Kyiv, UAH, 2024-12-01, 2024-12-31");
            }
            else if (arr.Length >= 1)
            {
                if (arr[0] == "/GetHotel")
                {
                    if (arr.Length >= 4 && arr.Length <= 6)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Шукаю...");
                        HotelClient hotelClient = new HotelClient();
                        currency = "UAH";
                        priceMax = 10000000;
                        k = 0;
                        page = 1;
                        city = arr[1];
                        arrival = arr[2];
                        departure = arr[3];
                        if (arr.Length >= 5)
                        {
                            currency = arr[4];
                        }
                        if (arr.Length == 6)
                        {
                            priceMax = double.Parse(arr[5]);
                        }

                        hotels = hotelClient.GetHotel(city, arrival, departure, message.Chat.Id, filters, priceMax, page, currency).Result;
                        if (hotels.data.hotels.Length != 0)
                        {
                            await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                                caption: $"{hotels.data.hotels[k].accessibilityLabel}", replyMarkup: inlineKeyboard);
                        }
                        else { await botClient.SendTextMessageAsync(message.Chat.Id, "Нічого не знайшов"); }
                    }
                    else { await botClient.SendTextMessageAsync(message.Chat.Id, "Приклад: /GetHotel Kyiv, 2024-12-01, 2024-12-31, " +
                        "UAH, 100000\n" +
                        "Напишіть \"Пошук готелю\" для більш детальної інструкції"); }

                }
                else if (arr[0] == "/GetHotelList")
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
                        mess += $"{e} ; ";
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
                else if (arr[0] == "/GetAttraction")
                {
                    if (arr.Length >= 2 && arr.Length < 6)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Шукаю...");
                        AttractionClient attractionClient = new AttractionClient();
                        currency = arr.Length == 3 ? arr[2] : "UAH";
                        arrival = arr.Length == 5 ? arr[3] : "none";
                        departure = arr.Length == 5 ? arr[4] : "none";
                        k = 0;
                        attraction = attractionClient.GetAttractions(arr[1], arrival, departure, currency, message.Chat.Id).Result;
                        if (attraction.data.products.Length != 0)
                        {
                            await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri(attraction.data.products[k].primaryPhoto.small),
                                caption: $"{attraction.data.products[k].name}\n{attraction.data.products[k].shortDescription}\n" +
                                $"\nЦіна: {attraction.data.products[k].representativePrice.publicAmount} " +
                                $"{attraction.data.products[k].representativePrice.currency}"
                                , replyMarkup: inlineKeyboard3);
                        }
                        else { await botClient.SendTextMessageAsync(message.Chat.Id, "Нічого не знайшов"); }
                    }
                    else { await botClient.SendTextMessageAsync(message.Chat.Id, "Приклад: /GetAttraction NewYork, USD, 2024-12-01, 2024-12-31"); }
                }
                else if (arr[0] == "/GetAttractionList")
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Зачекайте...");
                    AttractionClient attractionClient = new AttractionClient();
                    k = 0;
                    baseAttractions = attractionClient.ReadAttractionList(message.Chat.Id).Result;
                    if (baseAttractions.Count != 0 && k < baseAttractions.Count)
                    {
                        await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri($"{baseAttractions[k].photoURL}"),
                            caption: $"{baseAttractions[k].name}\n" +
                            $"Місто: {baseAttractions[k].city}\nБал: {baseAttractions[k].score:f}\n" +
                            $"Ціна: {baseAttractions[k].price:f} " +
                            $"{baseAttractions[k].currency}\nId розваги: {baseAttractions[k].id}",
                            replyMarkup: inlineKeyboard4);
                    }
                    else { await botClient.SendTextMessageAsync(message.Chat.Id, "Список пустий"); }
                }
                else if (message.Text == "Так" && hotels.data != null)
                {
                    k = 0;
                    page++;
                    HotelClient hotelClient = new HotelClient();
                    hotels = hotelClient.GetHotel(city, arrival, departure, message.Chat.Id, filters, priceMax, page, currency).Result;
                    await botClient.SendPhotoAsync(message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                                caption: $"{hotels.data.hotels[k].accessibilityLabel}", replyMarkup: inlineKeyboard);
                }
                else if (message.Text == "Ні" && hotels.data != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Добре");
                }
                else if (arr[0] == "/SetDate")
                {
                    if (arr.Length == 3)
                    {
                        arrival = arr[1];
                        departure = arr[2];
                    }
                }
            }
        }
        private async Task HanlderCallBackQuertAsync(ITelegramBotClient botClient, CallbackQuery callback)
        {

            if (callback.Data == "button1" && hotels.data != null)
            {
                if (k != 0)
                {
                    k--;
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
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                                caption: $"{hotels.data.hotels[k].accessibilityLabel}", replyMarkup: inlineKeyboard);
                }
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
            else if (callback.Data == "button3" && hotels.data != null)
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
                k++;
                if (k < baseHotels.Count)
                {
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{baseHotels[k].PhotoURL}"),
                        caption: $"{baseHotels[k].Name}\n" +
                        $"Кількість зірок: {baseHotels[k].Stars}\nБал: {baseHotels[k].Score:f}\n" +
                        $"Кількість оцінок: {baseHotels[k].Reviews}\n" +
                        $"Ціна: {baseHotels[k].Price:f} " +
                        $"{baseHotels[k].Currency}\nId отеля: {baseHotels[k].Hotel_id}",
                        replyMarkup: inlineKeyboard2);
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Це кінець списку"); k--; }

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
            else if (callback.Data == "button7" && hotels.data != null)
            {
                HotelClient hotelClient = new HotelClient();
                HotelDetails det = hotelClient.GetDetails(hotels.data.hotels[k].property.id, arrival, departure, currency).Result;
                string include_breakfast = det.data.hotel_include_breakfast == 1 ? "Так" : "Ні",
                    breakfast_rating = det.data.breakfast_review_score != null ? $"{det.data.breakfast_review_score.rating}" : "Оцінка відсутння",
                    wifi_rating = det.data.wifi_review_score != null ? $"{det.data.wifi_review_score.rating}" : "Оцінка відсутння",
                    tags = det.data.property_highlight_strip[0].name,
                    impMess = "Відсутння",
                    allInclude = det.data.composite_price_breakdown.all_inclusive_amount != null ?
                    $"{det.data.composite_price_breakdown.all_inclusive_amount.value}" : "Відсутняя";
                if (det.data.property_highlight_strip.Length != 0)
                {
                    for (int i = 1; i < det.data.property_highlight_strip.Length; i++)
                    {
                        tags += $", {det.data.property_highlight_strip[i].name}";
                    }
                    impMess = det.data.hotel_important_information_with_codes[0].phrase;
                    for (int i = 1; i < det.data.hotel_important_information_with_codes.Length; i++)
                    {
                        impMess += $"\n\n{det.data.hotel_important_information_with_codes[i].phrase}";
                    }
                }
                await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{hotels.data.hotels[k].property.photoUrls[0]}"),
                                caption: $"{det.data.hotel_name}\nПосилання: {det.data.url}\n" +
                                $"Тип помешкання: {det.data.accommodation_type_name}\nМісто: {det.data.city}\n" +
                                $"Район: {det.data.district}\nАдреса: {det.data.address}\nВідстань від центру: {det.data.distance_to_cc} км\n" +
                                $"Валюта: {det.data.currency_code}\nДоступні кімнати: {det.data.available_rooms}\n" +
                                $"Середній розмір кімнат: {det.data.average_room_size_for_ufi_m2} м2\n" +
                                $"Включає сніданок: {include_breakfast}\n");
                await botClient.SendTextMessageAsync(callback.Message.Chat.Id, $"Ціна заніч: {det.data.product_price_breakdown.excluded_amount.value} " +
                                $"{det.data.product_price_breakdown.excluded_amount.currency}\n" +
                                $"Все увімкнено: {allInclude} " +
                                $"{det.data.composite_price_breakdown.all_inclusive_amount.currency}\n" +
                                $"\nТеги: {tags}\n\nОцінка сніданку: {breakfast_rating}\n" +
                                $"Оцінка Wi-Fi: {wifi_rating}\n\n" +
                                $"Додаткова інформація від готелю: {impMess}", replyMarkup: inlineKeyboard);
            }
            else if (callback.Data == "button8" && baseHotels != null)
            {
                if (arrival != null && departure != null)
                {
                    HotelClient hotelClient = new HotelClient();
                    HotelDetails det = hotelClient.GetDetails(baseHotels[k].Hotel_id, arrival, departure, currency).Result;
                    string include_breakfast = det.data.hotel_include_breakfast == 1 ? "Так" : "Ні",
                        breakfast_rating = det.data.breakfast_review_score != null ? $"{det.data.breakfast_review_score.rating}" : "Оцінка відсутння",
                        wifi_rating = det.data.wifi_review_score != null ? $"{det.data.wifi_review_score.rating}" : "Оцінка відсутння",
                        tags = det.data.property_highlight_strip[0].name,
                        impMess = "Відсутння",
                        allInclude = det.data.composite_price_breakdown.all_inclusive_amount.value != null ?
                        $"{det.data.composite_price_breakdown.all_inclusive_amount.value}" : "Відсутняя";
                    if (det.data.property_highlight_strip.Length != 0)
                    {
                        for (int i = 1; i < det.data.property_highlight_strip.Length; i++)
                        {
                            tags += $", {det.data.property_highlight_strip[i].name}";
                        }
                        impMess = det.data.hotel_important_information_with_codes[0].phrase;
                        for (int i = 1; i < det.data.hotel_important_information_with_codes.Length; i++)
                        {
                            impMess += $"\n\n{det.data.hotel_important_information_with_codes[i].phrase}";
                        }
                    }
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{baseHotels[k].PhotoURL}"),
                                    caption: $"{det.data.hotel_name}\nПосилання: {det.data.url}\n" +
                                    $"Тип помешкання: {det.data.accommodation_type_name}\nМісто: {det.data.city}\n" +
                                    $"Район: {det.data.district}\nАдреса: {det.data.address}\nВідстань від центру: {det.data.distance_to_cc} км\n" +
                                    $"Валюта: {det.data.currency_code}\nДоступні кімнати: {det.data.available_rooms}\n" +
                                    $"Середній розмір кімнат: {det.data.average_room_size_for_ufi_m2} м2\n" +
                                    $"Включає сніданок: {include_breakfast}\n");
                    await botClient.SendTextMessageAsync(callback.Message.Chat.Id, $"Ціна заніч: {det.data.product_price_breakdown.excluded_amount.value} " +
                                    $"{det.data.product_price_breakdown.excluded_amount.currency}\n" +
                                    $"Все увімкнено: {allInclude} " +
                                    $"{det.data.composite_price_breakdown.all_inclusive_amount.currency}\n" +
                                    $"\nТеги: {tags}\n\nОцінка сніданку: {breakfast_rating}\n" +
                                    $"Оцінка Wi-Fi: {wifi_rating}\n\n" +
                                    $"Додаткова інформація від готелю: {impMess}", replyMarkup: inlineKeyboard2);
                }
                else
                {
                    await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Відсутння інформація дати прибуття та відбуття." +
                        "\nПропишіть /SetDate {дата прибуття}, {дата відбуття}\nПриклад: /SetDate 2024-12-01, 2024-12-31");
                }
            }
            if (callback.Data == "button9" && attraction.data != null)
            {
                if (k != 0)
                {
                    k--;
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{attraction.data.products[k].primaryPhoto.small}"),
                                caption: $"{attraction.data.products[k].name}\n{attraction.data.products[k].shortDescription}\n" +
                                $"\nЦіна: {attraction.data.products[k].representativePrice.publicAmount} " +
                                $"{attraction.data.products[k].representativePrice.currency}"
                                , replyMarkup: inlineKeyboard3);
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Це початок списку"); }
            }
            else if (callback.Data == "button10" && attraction.data != null)
            {
                k++;
                if (k < attraction.data.products.Length)
                {
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{attraction.data.products[k].primaryPhoto.small}"),
                               caption: $"{attraction.data.products[k].name}\n{attraction.data.products[k].shortDescription}\n" +
                               $"\nЦіна: {attraction.data.products[k].representativePrice.publicAmount} " +
                               $"{attraction.data.products[k].representativePrice.currency}"
                               , replyMarkup: inlineKeyboard3);
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Це кінець списку"); }
            }
            else if (callback.Data == "button11" && attraction.data != null)
            {
                if (k < attraction.data.products.Length)
                {
                    AttractionClient AttractionClient = new AttractionClient();
                    AttractionClient.InsertAttraction(attraction.data.products[k].id, callback.Message.Chat.Id);
                    await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Додав");
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Неможливо виконати"); }
            }
            else if (callback.Data == "button12" && attraction.data != null)
            {
                AttractionClient attractionClient = new AttractionClient();
                AttractionDetails det = attractionClient.GetAttractionDetails(attraction.data.products[k].slug, currency).Result;
                string supportedLeng = det.data.guideSupportedLanguages != null ? det.data.guideSupportedLanguages[0] : "Інформцація відсутння";
                string notInclude = det.data.notIncluded != null ? det.data.notIncluded[0] : "Інформцація відсутння";
                string Included = det.data.whatsIncluded != null ? det.data.whatsIncluded[0] : "Інформцація відсутння";
                string address = det.data.addresses.arrival != null ? det.data.addresses.arrival[0].address : "Інформцація відсутння";
                if (det.data.guideSupportedLanguages != null && det.data.guideSupportedLanguages.Length > 1)
                {
                    for (int i = 1; i < det.data.guideSupportedLanguages.Length; i++)
                    {
                        supportedLeng += $", {det.data.guideSupportedLanguages[i]}";
                    }
                }
                if (det.data.notIncluded != null && det.data.notIncluded.Length > 1)
                {
                    for (int i = 1; i < det.data.notIncluded.Length; i++)
                    {
                        notInclude += $", {det.data.notIncluded[i]}";
                    }
                }
                if (det.data.whatsIncluded != null && det.data.whatsIncluded.Length > 1)
                {
                    for (int i = 0; i < det.data.whatsIncluded.Length; i++)
                    {
                        Included += $", {det.data.whatsIncluded[i]}";
                    }
                }
                await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri(det.data.primaryPhoto.small),
                    caption: det.data.description);
                await botClient.SendTextMessageAsync(callback.Message.Chat.Id, $"{det.data.name}\n\n{det.data.description}\n\n" +
                    $"Адреса: {address}\nМови які використовуються: {supportedLeng}\n" +
                    $"Включає: {Included}\nНе включає: {notInclude}\nЦіна: {det.data.representativePrice.publicAmount} {det.data.representativePrice.currency}\n" +
                    $"Середній бал: {det.data.reviewsStats.combinedNumericStats.average}\n" +
                    $"Кількість відгуків: {det.data.reviewsStats.combinedNumericStats.total}", replyMarkup: inlineKeyboard3);
            }
            if (callback.Data == "button13" && baseAttractions.Count != 0)
            {
                if (k != 0)
                {
                    k--;
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{baseAttractions[k].photoURL}"),
                                caption: $"{baseAttractions[k].name}\nМісто: {baseAttractions[k].city}\nId: {baseAttractions[k].id}" +
                                $"\n\nОпис: {baseAttractions[k].description}\n\nЦіна: {baseAttractions[k].price} " +
                                $"{baseAttractions[k].currency}\nБал: {baseAttractions[k].score}"
                                , replyMarkup: inlineKeyboard4);
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Це початок списку"); }
            }
            else if (callback.Data == "button14" && baseAttractions.Count != 0)
            {
                k++;
                if (k < attraction.data.products.Length)
                {
                    await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri($"{baseAttractions[k].photoURL}"),
                                caption: $"{baseAttractions[k].name}\nМісто: {baseAttractions[k].city}\nId: {baseAttractions[k].id}" +
                                $"\n\nОпис: {baseAttractions[k].description}\n\nЦіна: {baseAttractions[k].price} " +
                                $"{baseAttractions[k].currency}\nБал: {baseAttractions[k].score}"
                                , replyMarkup: inlineKeyboard4);
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Це кінець списку"); }
            }
            else if (callback.Data == "button15" && baseAttractions.Count != 0)
            {
                if (k < baseAttractions.Count)
                {
                    AttractionClient AttractionClient = new AttractionClient();
                    AttractionClient.DeleteAttraction(baseAttractions[k].id, callback.Message.Chat.Id);
                    await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Видалив");
                }
                else { await botClient.SendTextMessageAsync(callback.Message.Chat.Id, "Неможливо виконати"); }
            }
            else if (callback.Data == "button16" && baseAttractions.Count != 0)
            {
                AttractionClient attractionClient = new AttractionClient();
                AttractionDetails det = attractionClient.GetAttractionDetails(baseAttractions[k].slug, currency).Result;
                string supportedLeng = det.data.guideSupportedLanguages != null ? det.data.guideSupportedLanguages[0] : "Інформцація відсутння";
                string notInclude = det.data.notIncluded != null ? det.data.notIncluded[0] : "Інформцація відсутння";
                string Included = det.data.whatsIncluded != null ? det.data.whatsIncluded[0] : "Інформцація відсутння";
                string address = det.data.addresses.arrival != null ? det.data.addresses.arrival[0].address : "Інформцація відсутння";
                if (det.data.guideSupportedLanguages != null && det.data.guideSupportedLanguages.Length > 1)
                {
                    for (int i = 1; i < det.data.guideSupportedLanguages.Length; i++)
                    {
                        supportedLeng += $", {det.data.guideSupportedLanguages[i]}";
                    }
                }
                if (det.data.notIncluded != null && det.data.notIncluded.Length > 1)
                {
                    for (int i = 1; i < det.data.notIncluded.Length; i++)
                    {
                        notInclude += $", {det.data.notIncluded[i]}";
                    }
                }
                if (det.data.whatsIncluded != null && det.data.whatsIncluded.Length > 1)
                {
                    for (int i = 0; i < det.data.whatsIncluded.Length; i++)
                    {
                        Included += $", {det.data.whatsIncluded[i]}";
                    }
                }
                await botClient.SendPhotoAsync(callback.Message.Chat.Id, InputFile.FromUri(det.data.primaryPhoto.small),
                    caption: det.data.description);
                await botClient.SendTextMessageAsync(callback.Message.Chat.Id, $"{det.data.name}\n\n{det.data.description}\n\n" +
                    $"Адреса: {address}\nМови які використовуються: {supportedLeng}\n" +
                    $"Включає: {Included}\nНе включає: {notInclude}\nЦіна: {det.data.representativePrice.publicAmount} {det.data.representativePrice.currency}\n" +
                    $"Середній бал: {det.data.reviewsStats.combinedNumericStats.average}\n" +
                    $"Кількість відгуків: {det.data.reviewsStats.combinedNumericStats.total}", replyMarkup: inlineKeyboard4);
            }
        }
    }
}
