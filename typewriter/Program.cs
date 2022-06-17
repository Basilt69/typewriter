using System;
using System.Collections.Generic;
using System.Resources;

namespace Homework01
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dictionary = GetDictionary();
            var rand = new Random();
            var info = new InfoIteration();

            while (true)
            {
                Console.WriteLine("Выберите язык для тестирования скорости печати:\n1 - Russian, \n2 - English");
                var line = Console.ReadLine();
                Languages languages;

                try
                {
                    languages = int.Parse(line ?? string.Empty) == 1 ? Languages.Russian : Languages.English;
                }
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели не число");
                    continue;
                }

                var texts = dictionary[languages];

                Console.WriteLine("Тест на скорость печати:\n");
                var startedAt = DateTime.Now;
                info.IndexText = rand.Next(texts.Length);
                var text = texts[info.IndexText];

                Console.WriteLine(text);
                var textResult = Console.ReadLine();
                info.Time = DateTime.Now - startedAt;

                Console.WriteLine("\nВаш результат:");

                if (string.IsNullOrEmpty(textResult))
                    Console.WriteLine("Вы ничего не ввели.");
                else if (text.Equals(textResult))
                    Console.WriteLine("В вашем тексте нет ошибок.");
                else
                {
                    info.MisPrintCount = StringCompare.CheckTexts(text, textResult);
                    Console.WriteLine(
                        $"В вашем тексте {info.MisPrintCount} {Utils.Plural(info.MisPrintCount, "опечатка", "опечатки", "опечаток")}");
                }

                Console.WriteLine($"Скорость печати составила: {info.GetTimeFormated()}\n");
                info.AddResult(textResult?.Length ?? 0);

                Console.WriteLine(
                    "Введите 'exit' для выхода или нажмите enter для продолжения");
                var answer = Console.ReadLine();

                if (answer != null && answer.Equals("exit"))
                {
                    Console.WriteLine(
                        $"У вас было {info.GetCountResult()} {Utils.Plural(info.GetCountResult(), "попытка", "попытки", "попыток")}.");

                    Console.WriteLine($"Лучшая скорость: {info.GetBestTime()}");

                    if (info.GetCountResult() > 1)
                    {
                        Console.WriteLine($"Худшая скорость: {info.GetWorseTime()}");
                        Console.WriteLine($"Средняя скорость: {info.GetAverageTime()}");
                    }

                    break;
                }
            }
        }

        private static Dictionary<Languages, string[]> GetDictionary()
        {
            var dictionary = new Dictionary<Languages, string[]>();

            string[] russianTexts =
            {
                "Чиполлино был сыном Чиполлоне. И было у него семь братьев: Чиполлетто," +
                "Чиполлотто, Чиполлочча, Чиполлучча и так далее – самые подходящие имена" +
                "для честной луковой семьи. Люди они были хорошие, надо прямо сказать," +
                "да только не везло им в жизни." +
                "Что ж поделаешь: где лук, там и слезы. Чиполлоне, его жена и сыновья жили" +
                "в деревянной лачуге чуть побольше ящичка для огородной рассады. Если богачам" +
                "случалось попадать в эти места, они недовольно морщили носы, ворчали: «Фу, как" +
                "несёт луком!» – и приказывали кучеру ехать быстрее."
            };

            string[] englishTexts =
            {
                "Scarlett O ’Hara was not beautiful, but men did not realize" +
                "this when caught by her charm as the Tarleton twins were. Her" +
                "eyes were green, and her skin was that soft white skin which" +
                "Southern women valued so highly, and covered so carefully from" +
                "the hot Georgia sun with hats and gloves. On that bright April" +
                "afternoon of 1861, sixteen-year-old Scarlett sat in the cool" +
                "shadows of the house at Tara, her father’s plantation. Stuart" +
                "and Brent Tarleton sat each side of her. They were friendly young" +
                "men with deep red-brown hair, and were clever in the things thatm" +
                "attered in north Georgia at that time — growing good cotton, riding" +
                "well, shooting straight and behaving like a gentleman."
            };

            dictionary.Add(Languages.Russian, russianTexts);
            dictionary.Add(Languages.English, englishTexts);

            return dictionary;
        }
    }
}