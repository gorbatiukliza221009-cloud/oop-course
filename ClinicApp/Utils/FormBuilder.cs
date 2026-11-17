using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class FormBuilder
{
    public static T Build<T>() where T : new()
    {
        T obj = new T();
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            // лише властивості з ПУБЛІЧНИМ сеттером (Id і Status пропускаються)
            if (prop.GetSetMethod() == null)
                continue;

            string hint = BuildHint(prop);

            while (true)
            {
                Console.Write($"  {prop.Name}{hint}: ");
                string input = Console.ReadLine() ?? "";

                object converted;
                try
                {
                    converted = Convert.ChangeType(input, prop.PropertyType);
                }
                catch (Exception)
                {
                    Console.WriteLine("  Некоректне значення, спробуйте ще раз.");
                    continue;
                }

                string? error = CheckAttributes(prop, converted);
                if (error != null)
                {
                    Console.WriteLine($"  {error}");
                    continue;
                }

                prop.SetValue(obj, converted);
                break;
            }
        }

        return obj;
    }

    // " (обов'язкове, макс. 200)" / " (мін. 1)" / ""
    private static string BuildHint(PropertyInfo prop)
    {
        List<string> parts = new List<string>();

        if (prop.GetCustomAttribute<RequiredAttribute>() != null)
            parts.Add("обов'язкове");

        MaxLengthAttribute? maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
        if (maxLength != null)
            parts.Add($"макс. {maxLength.Length}");

        MinValueAttribute? minValue = prop.GetCustomAttribute<MinValueAttribute>();
        if (minValue != null)
            parts.Add($"мін. {minValue.Min}");

        if (parts.Count == 0)
            return "";

        return " (" + string.Join(", ", parts) + ")";
    }

    // перевірка одного поля тими самими правилами, що й у ModelValidator
    private static string? CheckAttributes(PropertyInfo prop, object value)
    {
        RequiredAttribute? required = prop.GetCustomAttribute<RequiredAttribute>();
        if (required != null && value is string text && string.IsNullOrWhiteSpace(text))
            return required.ErrorMessage;

        MaxLengthAttribute? maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
        if (maxLength != null && value is string str && str.Length > maxLength.Length)
            return maxLength.ErrorMessage != "" ? maxLength.ErrorMessage
                                                : $"Must not exceed {maxLength.Length} characters.";

        MinValueAttribute? minValue = prop.GetCustomAttribute<MinValueAttribute>();
        if (minValue != null && Convert.ToDouble(value) < minValue.Min)
            return minValue.ErrorMessage != "" ? minValue.ErrorMessage
                                               : $"Must be at least {minValue.Min}.";

        return null;
    }
}