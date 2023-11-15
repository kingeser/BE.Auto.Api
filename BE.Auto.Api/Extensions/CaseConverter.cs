using System.Globalization;
using System.Text;

namespace BE.Auto.Api.Extensions;

public static class CaseConverter
{
    public static string ToUppercase(this string input)
    {
      return  CultureInfo.CurrentCulture.TextInfo.ToUpper(input);
      
    }

    public static string ToLowercase(this string input)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToLower(input);
    }

    public static string ToTitleCase(this string input)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
    }

    public static string ToCamelCase(this string input)
    {
        var words = input.Split(' ');
        for (var i = 1; i < words.Length; i++)
        {
            words[i] = char.ToLower(words[i][0]) + words[i][1..];
        }
        return string.Concat(words);
    }

    public static string ToPascalCase(this string input)
    {
        var words = input.Split(' ');
        for (var i = 0; i < words.Length; i++)
        {
            words[i] = char.ToUpper(words[i][0]) + words[i][1..];
        }
        return string.Concat(words);
    }

    public static string ToSnakeCase(this string input)
    {
      
        var snakeCase = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            var currentChar = input[i];
            if (char.IsUpper(currentChar))
            {
                if (i > 0)
                {
                    snakeCase.Append("_");
                }

                snakeCase.Append(char.ToLower(currentChar));
            }
            else
            {
                snakeCase.Append(currentChar);
            }
        }

        return snakeCase.ToString();
    }

    public static string ToKebabCase(this string input)
    {
     
        var kebabCase = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            var currentChar = input[i];
            if (char.IsUpper(currentChar))
            {
                if (i > 0)
                {
                    kebabCase.Append("-");
                }

                kebabCase.Append(char.ToLower(currentChar));
            }
            else
            {
                kebabCase.Append(currentChar);
            }
        }

        return kebabCase.ToString();
    
}

    public static string ToSentenceCase(this string input)
    {
        return char.ToUpper(input[0]) + input[1..];
    }

    public static string ToInverseCase(this string input)
    {
        var characters = input.ToCharArray();
        for (var i = 0; i < characters.Length; i++)
        {
            if (char.IsUpper(characters[i]))
                characters[i] = char.ToLower(characters[i]);
            else if (char.IsLower(characters[i]))
                characters[i] = char.ToUpper(characters[i]);
        }
        return new string(characters);
    }
}