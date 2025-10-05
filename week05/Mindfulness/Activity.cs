using System;
using System.Collections.Generic;
using System.Threading;

public class Activity
{
    private string _name;
    private string _description;
    protected int _duration; // derived classes can access

    // the constructor
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // shared behavior that will display starting info. 
    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name} Activity.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like your session to be? ");

        // validates integer input
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int seconds) && seconds > 0)
            {
                _duration = seconds;
                break;
            }
            Console.Write("Please enter a valid positive number for seconds: ");
        }

        Console.WriteLine("\nGet ready...");
        ShowSpinner(3);
    }

    // shared behavior that displays the ending message
    public void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(2);
        Console.WriteLine($"\nYou have completed the {_name} activity for {_duration} seconds.");
        ShowSpinner(2);
    }

    // spinner animation that use backspace
    public void ShowSpinner(int seconds)
    {
        string[] spinner = new string[] { "|", "/", "-", "\\" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < endTime)
        {
            Console.Write(spinner[i]);
            Thread.Sleep(250);
            Console.Write("\b \b");
            i = (i + 1) % spinner.Length;
        }
    }

    // countdown animation that use backspace (shows numbers)
    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            string s = i.ToString();
            Console.Write(s);
            Thread.Sleep(1000);
            // erase the numbers
            for (int k = 0; k < s.Length; k++)
            {
                Console.Write("\b \b");
            }
        }
    }
}
