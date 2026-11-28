// ============================================================
// W05 Project: Mindfulness Program
//
// EXCEEDING REQUIREMENTS (creativity):
// 1. Added a 4th activity
// 2. Random prompts/questions are NOT repeated until all have been used
//    at least once in the session.
// 3. Activity log saved to "mindfulness_log.txt" (date, activity, seconds).
//    The menu shows how many activities have been completed in total.
// 4. Breathing animation: the "breathe in" countdown is shorter than the
//    "breathe out" to feel more natural.
// ============================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

// ---------- Base class ----------
abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    protected int GetDuration() => _duration;

    public void Run()
    {
        DisplayStartingMessage();
        Execute();
        DisplayEndingMessage();
        ActivityLog.Save(_name, _duration);
    }

    protected abstract void Execute();

    private void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name} Activity.\n");
        Console.WriteLine(_description + "\n");

        Console.Write("How long, in seconds, would you like for your session? ");
        while (!int.TryParse(Console.ReadLine(), out _duration) || _duration <= 0)
        {
            Console.Write("Please enter a positive number of seconds: ");
        }

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(5);
        Console.WriteLine();
    }

    private void DisplayEndingMessage()
    {
        Console.WriteLine("\n\nWell done!!");
        ShowSpinner(4);
        Console.WriteLine($"\nYou have completed another {_duration} seconds of the {_name} Activity.");
        ShowSpinner(5);
    }

    protected void ShowSpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        DateTime end = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < end)
        {
            Console.Write(frames[i % frames.Length]);
            Thread.Sleep(250);
            Console.Write("\b \b");
            i++;
        }
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    
    protected string GetUniqueRandom(List<string> all, List<string> used)
    {
        if (used.Count >= all.Count) used.Clear();
        var available = all.FindAll(x => !used.Contains(x));
        string pick = available[new Random().Next(available.Count)];
        used.Add(pick);
        return pick;
    }
}

// ---------- Breathing ----------
class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.") { }

    protected override void Execute()
    {
        DateTime end = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now < end)
        {
            Console.Write("Breathe in...  ");
            ShowCountdown(4);
            Console.WriteLine();
            if (DateTime.Now >= end) break;

            Console.Write("Breathe out... ");
            ShowCountdown(6);
            Console.WriteLine("\n");
        }
    }
}

// ---------- Reflection ----------
class ReflectionActivity : Activity
{
    private List<string> _prompts = new()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };
    private List<string> _questions = new()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };
    private List<string> _usedPrompts = new();
    private List<string> _usedQuestions = new();

    public ReflectionActivity() : base("Reflection",
        "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.") { }

    protected override void Execute()
    {
        Console.WriteLine("Consider the following prompt:\n");
        Console.WriteLine($"--- {GetUniqueRandom(_prompts, _usedPrompts)} ---\n");
        Console.Write("When you have something in mind, press Enter to continue.");
        Console.ReadLine();

        Console.WriteLine("\nNow ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountdown(5);
        Console.Clear();

        DateTime end = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now < end)
        {
            Console.Write($"> {GetUniqueRandom(_questions, _usedQuestions)} ");
            ShowSpinner(10);
            Console.WriteLine();
        }
    }
}

// ---------- Listing ----------
class ListingActivity : Activity
{
    private List<string> _prompts = new()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };
    private List<string> _usedPrompts = new();

    public ListingActivity() : base("Listing",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") { }

    protected override void Execute()
    {
        Console.WriteLine("List as many responses as you can to the following prompt:\n");
        Console.WriteLine($"--- {GetUniqueRandom(_prompts, _usedPrompts)} ---\n");
        Console.Write("You may begin in: ");
        ShowCountdown(5);
        Console.WriteLine("\n");

        int count = 0;
        DateTime end = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now < end)
        {
            Console.Write("> ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item)) count++;
        }

        Console.WriteLine($"\nYou listed {count} items!");
    }
}

// ---------- Prayer (extra) ----------
class PrayerActivity : Activity
{
    public PrayerActivity() : base("Prayer",
        "This activity gives you a quiet moment to pray and speak with your Heavenly Father in your own words.") { }

    protected override void Execute()
    {
        Console.WriteLine("Heavenly Father,");
        Console.WriteLine("We thank Thee for this day and for all Thy blessings...\n");
        Console.WriteLine("Continue your prayer in your heart:\n");

        DateTime end = DateTime.Now.AddSeconds(GetDuration());
        while (DateTime.Now < end)
        {
            ShowSpinner(1);
        }
        Console.WriteLine("\nIn the name of Jesus Christ, amen.");
    }
}

// ---------- Log (extra) ----------
static class ActivityLog
{
    private const string FileName = "mindfulness_log.txt";

    public static void Save(string activity, int seconds)
    {
        File.AppendAllText(FileName, $"{DateTime.Now:yyyy-MM-dd HH:mm}|{activity}|{seconds}\n");
    }

    public static int CountEntries()
    {
        return File.Exists(FileName) ? File.ReadAllLines(FileName).Length : 0;
    }
}

// ---------- Program ----------
class Program
{
    static void Main(string[] args)
    {
        string choice = "";
        while (choice != "5")
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Start prayer activity");
            Console.WriteLine("  5. Quit");
            Console.WriteLine($"\n(Activities completed so far: {ActivityLog.CountEntries()})");
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine();

            Activity activity = null;
            switch (choice)
            {
                case "1": activity = new BreathingActivity(); break;
                case "2": activity = new ReflectionActivity(); break;
                case "3": activity = new ListingActivity(); break;
                case "4": activity = new PrayerActivity(); break;
                case "5": Console.WriteLine("\nGoodbye!"); break;
                default:
                    Console.WriteLine("\nInvalid option. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
            activity?.Run();
        }
    }
}