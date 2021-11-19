namespace System;

public static class StringExtensions
{
    /// <summary>
    /// First char to upper
    /// </summary>
    public static string ToUpperFirstLetter(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return string.Empty;
        }

        char[] letters = str.ToCharArray();
        letters[0] = char.ToUpper(letters[0]);

        return new string(letters);
    }
}