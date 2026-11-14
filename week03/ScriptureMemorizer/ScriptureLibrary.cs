using System;
using System.Collections.Generic;
using System.IO;


public class ScriptureLibrary
{
    private List<(string Reference, string Text)> _entries = new List<(string, string)>();
    private Random _random = new Random();

    public ScriptureLibrary(string fileName)
    {
        LoadFromFile(fileName);

        if (_entries.Count == 0)
        {
            LoadDefaults();
        }
    }

    private void LoadFromFile(string fileName)
    {
        string[] candidates =
        {
            Path.Combine(AppContext.BaseDirectory, fileName),
            fileName
        };

        foreach (string path in candidates)
        {
            if (!File.Exists(path))
            {
                continue;
            }

            foreach (string line in File.ReadAllLines(path))
            {
                string[] parts = line.Split('|', 2);
                if (parts.Length == 2 && parts[0].Trim() != "" && parts[1].Trim() != "")
                {
                    _entries.Add((parts[0].Trim(), parts[1].Trim()));
                }
            }

            return;
        }
    }

    private void LoadDefaults()
    {
        _entries.Add(("John 3:16",
            "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."));
        _entries.Add(("Proverbs 3:5-6",
            "Trust in the LORD with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."));
        _entries.Add(("Philippians 4:13",
            "I can do all things through Christ which strengtheneth me."));
    }

    public Scripture GetRandomScripture()
    {
        var entry = _entries[_random.Next(_entries.Count)];
        return new Scripture(Reference.Parse(entry.Reference), entry.Text);
    }
}