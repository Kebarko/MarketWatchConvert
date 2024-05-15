using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using TextCopy;

namespace MarketWatchConvert;

/// <summary>
/// This program reads scraped data from MarketWatch and produces the required output.
/// </summary>
internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // Reading
            List<List<string>> records = new List<List<string>>();

            using (var reader = new StreamReader(args[0], true))
            {
                bool isHeader = true;
                while (reader.ReadLine() is string line)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                    }
                    else
                    {
                        records.Add(Split(line, ','));
                    }
                }
            }

            // Processing
            var result = new List<int>();

            foreach (List<string> record in records)
            {
                double value = double.Parse(Regex.Replace(record[1], @"^\D+", string.Empty), CultureInfo.InvariantCulture);
                double change = 0;
                if (record[3] == "Open")
                {
                    change = double.Parse(record[2].Replace("Chg", string.Empty), CultureInfo.InvariantCulture);
                }

                result.Add((int)Math.Round(value - change, MidpointRounding.AwayFromZero));
            }

            // Writing
            string resultLine = string.Join("\t", result);
            using (var writer = new StreamWriter(Path.ChangeExtension(args[0], ".txt"), false, Encoding.UTF8))
            {
                writer.WriteLine(resultLine);
            }

            ClipboardService.SetText(resultLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Splits a string into parts based on a specified delimiter, while considering quoted sections.
    /// </summary>
    /// <param name="input">The string to split.</param>
    /// <param name="delimiter">The character used to separate the parts.</param>
    /// <returns>A list of parts.</returns>
    private static List<string> Split(string input, char delimiter)
    {
        var result = new List<string>();

        bool insideQuotes = false;
        int startIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (c == '"')
            {
                insideQuotes = !insideQuotes;
            }
            else if (c == delimiter && !insideQuotes)
            {
                result.Add(input.Substring(startIndex, i - startIndex).Trim('"'));
                startIndex = i + 1;
            }
        }

        result.Add(input.Substring(startIndex).Trim('"'));

        return result;
    }
}
