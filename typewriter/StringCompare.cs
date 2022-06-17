using System;

namespace typewriter
{
    public static class StringCompare
    {
        private static int Compare(string firstText, string secondText, int firstSize, int secondSize)
        {
            if (firstSize == 0)
                return secondSize;

            if (secondSize == 0)
                return firstSize;

            int tmp = Math.Min(
                Compare(firstText, secondText, firstSize, secondSize - 1),
                Compare(firstText, secondText, firstSize - 1, secondSize)) + 1;

            return Math.Min(tmp, Compare(firstText, secondText, firstSize - 1, secondSize - 1) +
                                 GetFine(firstText[firstSize - 1], secondText[secondSize - 1]));
        }

        private static int GetFine(char a, char b)
        {
            return a != b ? 1 : 0;
        }

        public static int CheckTexts(string text, string textResult)
        {
            int count = 0;
            char[] separators =
            {
                ' ',
                ';',
                '.',
                ','
            };
            string[] firstText = text.Split(separators, SpringSplitOprions.RemoveEmptyEntries);
            string[] secondText = textResult.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            for (int index = 0; index < firstText.Length; index++)
            {
                string first = firstText[index];
                string second = secondText[index];

                if (!first.Equals(second))
                {
                    int temp = Compare(firstText[index], secondText[index], firstText[index].Length,
                        secondText[index].Length);
                    count += temp;

                    if (temp > 0)
                    {
                        Console.WriteLine(
                            $"Вы ошиблись в написании слова '{secondText[index]}'. Правильное написание: {firstText[index]}");
                    }
                }
            }

            return count;
        }
    }
}