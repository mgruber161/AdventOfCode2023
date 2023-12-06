public static class Extensions
{
    public static long ToInt64(this string input)
    {
        var num = 0L;
        for (var i = 0; i < input.Length; i++)
        {
            num = num * 10 + (input[i] - '0');
        }
        return num;
    }

    public static long[] ToInt64(this string[] values) => [.. values.Select(value => value.ToInt64())];
}