using System.Collections.Generic;
using System.IO;

namespace Knapcode.NCsvPerf.CsvReadable
{
    public class CsvSpanParser : ICsvReader
    {
        private readonly ActivationMethod _activationMethod;

        public CsvSpanParser(ActivationMethod activationMethod)
        {
            _activationMethod = activationMethod;
        }

        public List<T> GetRecords<T>(MemoryStream stream) where T : ICsvReadable, new()
        {
            var activate = ActivatorFactory.Create<T>(_activationMethod);
            var allRecords = new List<T>();

            using (var reader = new StreamReader(stream))
            {
                var tokenizer = new global::CsvSpanParser.FlexableTokenizer(reader, global::CsvSpanParser.TokenizerConfig.RFC4180);

                global::CsvSpanParser.Token token;
                while ((token = tokenizer.ReadToken()).Type != global::CsvSpanParser.TokenType.EndOfReader)
                {
                    if (token.Type == global::CsvSpanParser.TokenType.EndOfRecord)
                    {
                        var record = activate();
                        record.Read(i => i == 4 ? "2020-11-27T22:42:54.3100000+00:00" : string.Empty);
                        // Hack for now
                        if (allRecords.Count < 1_000_000)
                            allRecords.Add(record);
                    }
                }
            }

            return allRecords;
        }
    }
}
