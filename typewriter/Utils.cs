namespace typewriter
{
    public static class Utils
    {
        public static string Plural(int num, string one, string few, string many)
        {
            return num % 10 == 1 && num % 100 != 11 ? one :
                num % 10 >= 2 && num % 10 <= 4 && (num % 100 < 10 || num % 100 >= 20) ? few : many;
        }
    }
}
