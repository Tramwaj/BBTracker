using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace NBAcsvPlayByPlayReader
{
    public static class PlaysCSVReader
    {
        public static IEnumerable<PlayFromCSV> GetPlays(int year)
        {
            using var reader = new StreamReader($"NBA-PBP_{year}-{year + 1}.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<PlayFromCSV>();
        }
    }
}
