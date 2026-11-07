using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string file)
    {
        // UTF8 con BOM para que Excel muestre bien los acentos y la ñ
        using (StreamWriter outputFile = new StreamWriter(file, false, new UTF8Encoding(true)))
        {
            outputFile.WriteLine("Date,Prompt,Response");

            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(
                    $"{EscapeCsv(entry._date)},{EscapeCsv(entry._promptText)},{EscapeCsv(entry._entryText)}");
            }
        }
    }

    public void LoadFromFile(string file)
    {
        if (!File.Exists(file))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _entries.Clear();

        string[] lines = File.ReadAllLines(file);

        
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            List<string> fields = ParseCsvLine(lines[i]);

            if (fields.Count < 3)
            {
                continue;
            }

            Entry entry = new Entry();
            entry._date = fields[0];
            entry._promptText = fields[1];
            entry._entryText = fields[2];

            _entries.Add(entry);
        }
    }

    
    private string EscapeCsv(string value)
    {
        string safeValue = value.Replace("\"", "\"\"");
        return $"\"{safeValue}\"";
    }

   
    private List<string> ParseCsvLine(string line)
    {
        List<string> fields = new List<string>();
        StringBuilder current = new StringBuilder();
        bool insideQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (insideQuotes)
            {
                if (c == '"')
                {
                    if (i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        insideQuotes = false;
                    }
                }
                else
                {
                    current.Append(c);
                }
            }
            else
            {
                if (c == '"')
                {
                    insideQuotes = true;
                }
                else if (c == ',')
                {
                    fields.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
        }

        fields.Add(current.ToString());
        return fields;
    }
}