using System.Collections.Generic;
using System.IO;

namespace Knapcode.NCsvPerf.CsvReadable;

/// <summary>
/// Package: https://www.nuget.org/packages/FlexableCsvParser
/// Source: https://github.com/snargledorf/FlexableCsvParser
/// </summary>
public class FlexableCsvParser : ICsvReader
{
    public List<T> GetRecords<T>(MemoryStream stream) where T : ICsvReadable, new()
    {
        var allRecords = new List<T>();
            
        using var reader = new StreamReader(stream);
        var parser = new global::FlexableCsvParser.CsvParser(reader, 25);

        while (parser.Read())
        {
            var item = new T();
            item.Read(parser.GetString);
            allRecords.Add(item);
        }

        return allRecords;
    }
}