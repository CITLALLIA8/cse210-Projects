using System;


class Program
{
    static void Main(string[] args)
    {
        ScriptureLibrary library = new ScriptureLibrary("scriptures.txt");
        bool playAgain = true;

        while (playAgain)
        {
            int wordsPerRound = AskDifficulty();
            Scripture scripture = library.GetRandomScripture();

            bool quit = RunSession(scripture, wordsPerRound);

            if (quit)
            {
                break;
            }

            Console.WriteLine();
            Console.Write("Great job! Press enter to practice another scripture, or type quit: ");
            string answer = Console.ReadLine() ?? "";
            playAgain = answer.Trim().ToLower() != "quit";
        }

        Console.WriteLine("Goodbye!");
    }


    static bool RunSession(Scripture scripture, int wordsPerRound)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                return false;
            }

            Console.Write("Press enter to continue or type 'quit' to finish: ");
            string input = Console.ReadLine() ?? "";

            if (input.Trim().ToLower() == "quit")
            {
                return true;
            }

            scripture.HideRandomWords(wordsPerRound);
        }
    }

    static int AskDifficulty()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Scripture Memorizer");
            Console.WriteLine("Choose a difficulty:");
            Console.WriteLine("  1. Easy   (hides 2 words per round)");
            Console.WriteLine("  2. Medium (hides 4 words per round)");
            Console.WriteLine("  3. Hard   (hides 6 words per round)");
            Console.Write("> ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": return 2;
                case "2": return 4;
                case "3": return 6;
            }
        }
    }
}