using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ValidationResult result = new ValidationResult();

        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            object? value = prop.GetValue(obj);

            // Required: null або рядок з пробілів
            RequiredAttribute? required = prop.GetCustomAttribute<RequiredAttribute>();
            if (required != null)
            {
                bool isEmpty = value == null
                               || (value is string text && string.IsNullOrWhiteSpace(text));

                if (isEmpty)
                {
                    result.AddError($"{prop.Name}: {required.ErrorMessage}");
                }
            }

            // MaxLength: рядок довший за Length
            MaxLengthAttribute? maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLength != null && value is string str && str.Length > maxLength.Length)
            {
                string message = maxLength.ErrorMessage;
                if (message == "")
                    message = $"Must not exceed {maxLength.Length} characters.";

                result.AddError($"{prop.Name}: {message}");
            }

            // MinValue: число менше за Min
            MinValueAttribute? minValue = prop.GetCustomAttribute<MinValueAttribute>();
            if (minValue != null && value != null)
            {
                double number = Convert.ToDouble(value);
                if (number < minValue.Min)
                {
                    string message = minValue.ErrorMessage;
                    if (message == "")
                        message = $"Must be at least {minValue.Min}.";

                    result.AddError($"{prop.Name}: {message}");
                }
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        Console.WriteLine($"Тип: {type.Name}");

        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo prop in properties)
        {
            string line = $"  {prop.Name} ({prop.PropertyType.Name})";

            foreach (Attribute attr in prop.GetCustomAttributes())
            {
                line += " " + DescribeAttribute(attr);
            }

            Console.WriteLine(line);
        }
    }

    private static string DescribeAttribute(Attribute attr)
    {
        if (attr is RequiredAttribute)
            return "[Required]";

        if (attr is MaxLengthAttribute maxLength)
            return $"[MaxLength({maxLength.Length})]";

        if (attr is MinValueAttribute minValue)
            return $"[MinValue({minValue.Min})]";

        // будь-який інший атрибут: назва без суфікса "Attribute"
        string name = attr.GetType().Name;
        if (name.EndsWith("Attribute"))
            name = name.Substring(0, name.Length - "Attribute".Length);

        return $"[{name}]";
    }
}