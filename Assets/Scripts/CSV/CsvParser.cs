using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DefaultNamespace;

namespace CSV
{
    public static class CsvParser
    {
        /// <summary>
        /// Load CSV data from specified path.
        /// </summary>
        /// <param name="path">CSV file path.</param>
        /// <param name="delimiter">Delimiter.</param>
        /// <param name="encoding">Type of text encoding. (default UTF-8)</param>
        /// <returns>Nested list that CSV parsed.</returns>
        public static List<List<string>> LoadFromPath(string path, Delimiter delimiter = Delimiter.Auto, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;

            if (delimiter == Delimiter.Auto)
            {
                delimiter = EstimateDelimiter(path);
            }

            string data = File.ReadAllText(path, encoding);
            return Parse(data, delimiter);
        }

        /// <summary>
        /// Load CSV data asynchronously from specified path.
        /// </summary>
        /// <param name="path">CSV file path.</param>
        /// <param name="delimiter">Delimiter.</param>
        /// <param name="encoding">Type of text encoding. (default UTF-8)</param>
        /// <returns>Nested list that CSV parsed.</returns>
        public static async Task<List<List<string>>> LoadFromPathAsync(string path, Delimiter delimiter = Delimiter.Auto, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;

            if (delimiter == Delimiter.Auto)
            {
                delimiter = EstimateDelimiter(path);
            }

            using (StreamReader reader = new StreamReader(path, encoding))
            {
                string data = await reader.ReadToEndAsync();
                return Parse(data, delimiter);
            }
        }

        static Delimiter EstimateDelimiter(string path)
        {
            string extension = Path.GetExtension(path);
            if (extension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                return Delimiter.Comma;
            }

            if (extension.Equals(".tsv", StringComparison.OrdinalIgnoreCase))
            {
                return Delimiter.Tab;
            }

            throw new Exception($"Delimiter estimation failed. Unknown Extension: {extension}");
        }

        /// <summary>
        /// Load CSV data from string.
        /// </summary>
        /// <param name="data">CSV string</param>
        /// <param name="delimiter">Delimiter.</param>
        /// <returns>Nested list that CSV parsed.</returns>
        public static List<List<string>> LoadFromString(string data, Delimiter delimiter = Delimiter.Comma)
        {
            if (delimiter == Delimiter.Auto) throw new InvalidEnumArgumentException("Delimiter estimation from string is not supported.");
            return Parse(data, delimiter);
        }

        static List<List<string>> Parse(string data, Delimiter delimiter)
        {
            ConvertToCrlf(ref data);

            List<List<string>> sheet = new List<List<string>>();
            List<string> row = new List<string>();
            StringBuilder cell = new StringBuilder();
            bool insideQuoteCell = false;
            int start = 0;

            ReadOnlySpan<char> delimiterSpan = ((IConvertible) delimiter).ToChar(null).ToString().AsSpan();
            ReadOnlySpan<char> crlfSpan = "\r\n".AsSpan();
            ReadOnlySpan<char> oneDoubleQuotSpan = "\"".AsSpan();
            ReadOnlySpan<char> twoDoubleQuotSpan = "\"\"".AsSpan();

            while (start < data.Length)
            {
                int length = start <= data.Length - 2 ? 2 : 1;
                ReadOnlySpan<char> span = data.AsSpan(start, length);

                if (span.StartsWith(delimiterSpan))
                {
                    if (insideQuoteCell)
                    {
                        cell.Append(((IConvertible) delimiter).ToChar(null));
                    }
                    else
                    {
                        AddCell(row, cell);
                    }

                    start += 1;
                }
                else if (span.StartsWith(crlfSpan))
                {
                    if (insideQuoteCell)
                    {
                        cell.Append("\r\n");
                    }
                    else
                    {
                        AddCell(row, cell);
                        AddRow(sheet, ref row);
                    }

                    start += 2;
                }
                else if (span.StartsWith(twoDoubleQuotSpan))
                {
                    cell.Append("\"");
                    start += 2;
                }
                else if (span.StartsWith(oneDoubleQuotSpan))
                {
                    insideQuoteCell = !insideQuoteCell;
                    start += 1;
                }
                else
                {
                    cell.Append(span[0]);
                    start += 1;
                }
            }

            if (row.Count > 0 || cell.Length > 0)
            {
                AddCell(row, cell);
                AddRow(sheet, ref row);
            }

            return sheet;
        }

        static void AddCell(List<string> row, StringBuilder cell)
        {
            row.Add(cell.ToString());
            cell.Length = 0; // Old C#.
        }

        static void AddRow(List<List<string>> sheet, ref List<string> row)
        {
            sheet.Add(row);
            row = new List<string>();
        }

        static void ConvertToCrlf(ref string data)
        {
            data = Regex.Replace(data, @"\r\n|\r|\n", "\r\n");
        }
    }
}