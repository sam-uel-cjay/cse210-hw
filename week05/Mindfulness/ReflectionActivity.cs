using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;
    private Queue<string> _promptQueue;
    private Queue<string> _questionQueue;
    private Random _random;

    public ReflectionActivity()
        : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience and faith in the Lord Jesus Christ. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        _random = new Random();

        _prompts = new List<string>()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>()
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

        _promptQueue = new Queue<string>(ShuffleList(_prompts));
        _questionQueue = new Queue<string>(ShuffleList(_questions));
    }

    public void Run()
    {
        DisplayStartingMessage();
        Console.Clear();

        // get a prompt ( ensure no repeats until all is used)
        if (_promptQueue.Count == 0)
        {
            _promptQueue = new Queue<string>(ShuffleList(_prompts));
        }
        string prompt = _promptQueue.Dequeue();

        Console.WriteLine("Consider the following prompt:\n");
        Console.WriteLine($"--- {prompt} ---\n");
        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountdown(5);
        Console.Clear();

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            if (_questionQueue.Count == 0)
            {
                _questionQueue = new Queue<string>(ShuffleList(_questions));
            }

            string question = _questionQueue.Dequeue();
            Console.Write($"> {question} ");
            ShowSpinner(5);
            Console.WriteLine();
        }

        DisplayEndingMessage();

        // log session
        SessionLogger.LogSession("Reflection", _duration);
    }

    // elper: return a shuffled copy of a list
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
