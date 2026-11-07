// Exceeding requirements:
// - The journal is saved and loaded as a .csv file that opens in Excel.
// - Commas and quotation marks inside entries are handled correctly
//   (fields are wrapped in quotes and inner quotes are doubled).
// - The file includes a header row (Date,Prompt,Response).


using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        int choice = 0;

        while (choice != 5)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            string input = Console.ReadLine();
            int.TryParse(input, out choice);

            if (choice == 1)
            {
                Entry entry = new Entry();
                entry._date = DateTime.Now.ToShortDateString();
                entry._promptText = promptGenerator.GetRandomPrompt();

                Console.WriteLine(entry._promptText);
                Console.Write("> ");
                entry._entryText = Console.ReadLine();

                journal.AddEntry(entry);
            }
            else if (choice == 2)
            {
                journal.DisplayAll();
            }
            else if (choice == 3)
            {
                Console.Write("What is the filename? ");
                journal.LoadFromFile(Console.ReadLine());
            }
            else if (choice == 4)
            {
                Console.Write("What is the filename? ");
                journal.SaveToFile(Console.ReadLine());
            }
            else if (choice != 5)
            {
                Console.WriteLine("Please choose a number from 1 to 5.");
            }

            Console.WriteLine();
        }
    }
}