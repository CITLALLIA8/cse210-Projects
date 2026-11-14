using System;


public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
    }

    
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    
    public static Reference Parse(string text)
    {
        text = text.Trim();
        int lastSpace = text.LastIndexOf(' ');
        if (lastSpace < 0)
        {
            throw new FormatException($"Referencia inválida: '{text}'");
        }

        string book = text.Substring(0, lastSpace);
        string[] chapterAndVerses = text.Substring(lastSpace + 1).Split(':');
        if (chapterAndVerses.Length != 2)
        {
            throw new FormatException($"Referencia inválida: '{text}'");
        }

        int chapter = int.Parse(chapterAndVerses[0]);
        string[] verses = chapterAndVerses[1].Split('-');

        if (verses.Length == 1)
        {
            return new Reference(book, chapter, int.Parse(verses[0]));
        }

        return new Reference(book, chapter, int.Parse(verses[0]), int.Parse(verses[1]));
    }

    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }

        return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
    }
}