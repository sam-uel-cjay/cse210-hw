using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts;
    private Queue<string> _promptQueue;
    private Random _random;

    public ListingActivity()
        : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many items as you can in a certain area.")
    {
        _random = new Random();

        _prompts = new List<string>()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        _promptQueue = new Queue<string>(ShuffleList(_prompts));
    }

    public void Run()
    {
        DisplayStartingMessage();
        Console.Clear();

        if (_promptQueue.Count == 0)
        {
            _promptQueue = new Queue<string>(ShuffleList(_prompts));
        }
        string prompt = _promptQueue.Dequeue();

        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine($"\n--- {prompt} ---");
        Console.Write("You may begin in: ");
        ShowCountdown(5);

        List<string> responses = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            // If user types nothing (just presses enter quickly) we still consider the input (could be blank)
            string response = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(response))
            {
                responses.Add(response.Trim());
            }
            // If they press enter and time still left, continue prompting until time expires.
        }

        Console.WriteLine($"\nYou listed {responses.Count} item(s)!");
        ShowSpinner(2);
        DisplayEndingMessage();

        // Log session
        SessionLogger.LogSession("Listing", _duration);
    }

    private List<string> ShuffleList(List<string> list)
    {
        List<string> copy = new List<string>(list);
        int n = copy.Count;
        for (int i = 0; i < n; i++)
        {
            int j = _random.Next(i, n);
            string tmp = copy[i];
            copy[i] = copy[j];
            copy[j] = tmp;
        }
        return copy;
    }
}
