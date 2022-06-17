using System;
using System.Collections.Generic;

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
                Console.WriteLine("Выберите язык для тестирования скорости печати:\n1 - Russian, 2 - English");
                var line = Console.ReadLine();
                Languages languages;

                try
                {
                    languages = int.Parse(line ?? string.Empty) == 1 ? Languages.Russian : Languages.English;
                }
                catch (Exception)
                {
                    Console.WriteLine("Кажется, вы ввели не число");
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
                    Console.WriteLine("Кажется, вы ничего не ввели.");
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
                    "Хотите пройти тест еще раз? Введите 'exit' для выхода или нажмите enter для продолжения");
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