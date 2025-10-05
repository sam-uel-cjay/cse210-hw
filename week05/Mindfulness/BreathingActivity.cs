using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("\nBreathe in... ");
            ShowCountdown(4); // 4 seconds inhale (it can be adjustable)
            Console.Write("\nNow breathe out... ");
            ShowCountdown(6); // 6 seconds exhale (it can be adjustable)
            Console.WriteLine();
        }

        DisplayEndingMessage();

        // log session (part of my creativity feature)
        SessionLogger.LogSession("Breathing", _duration);
    }
}
