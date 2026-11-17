using System.Globalization;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class CsvImporter
{
    private const int FieldCount = 5;

    public ImportResult ImportPatients(Clinic clinic, string filePath)
    {
        ImportResult result = new ImportResult();

        if (!File.Exists(filePath))
        {
            result.AddError(0, $"Файл не знайдено: {filePath}");
            return result;
        }

        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        // i = 0 — заголовок, тому починаємо з 1
        for (int i = 1; i < lines.Length; i++)
        {
            int lineNumber = i + 1;          // номер рядка для людини
            string line = lines[i];

            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');

            if (parts.Length != FieldCount)
            {
                result.AddError(lineNumber,
                    $"Рядок має {parts.Length} {FieldWord(parts.Length)} замість {FieldCount}.");
                continue;
            }

            for (int j = 0; j < parts.Length; j++)
            {
                parts[j] = parts[j].Trim();
            }

            // try/catch — навколо ОДНОГО рядка: помилка тут не зупиняє цикл
            bool bloodTypeParsed = false;
            try
            {
                DateTime dateOfBirth = DateTime.ParseExact(
                    parts[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);

                // порожня група крові → Unknown
                BloodType bloodType = BloodType.Unknown;
                if (parts[3] != "")
                {
                    bloodType = (BloodType)Enum.Parse(typeof(BloodType), parts[3], true);

                    // Enum.Parse приймає і числа ("42"), тому перевіряємо, що таке значення є
                    if (!Enum.IsDefined(typeof(BloodType), bloodType))
                        throw new ArgumentException("not defined");
                }
                bloodTypeParsed = true;

                // валідація з Лаби 05 спрацює в конструкторі Patient
                Patient patient = new Patient(
                    parts[0], parts[1], dateOfBirth, bloodType, parts[4]);

                clinic.Patients.Add(patient);
                result.AddSuccess();
            }
            catch (FormatException)
            {
                result.AddError(lineNumber,
                    $"Некоректна дата «{parts[2]}», очікується дд.мм.рррр.");
            }
            catch (ArgumentException e)
            {
                if (!bloodTypeParsed)
                    result.AddError(lineNumber, $"Невідома група крові «{parts[3]}».");
                else
                    result.AddError(lineNumber, e.Message);
            }
        }

        return result;
    }

    // 1 поле, 2–4 поля, 5+ полів
    private static string FieldWord(int n)
    {
        int lastTwo = n % 100;
        int last = n % 10;

        if (lastTwo >= 11 && lastTwo <= 14)
            return "полів";
        if (last == 1)
            return "поле";
        if (last >= 2 && last <= 4)
            return "поля";
        return "полів";
    }
}