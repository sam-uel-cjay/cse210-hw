using System;
using System.Threading;

/*
    EXCEEDS REQUIREMENTS (explained here as required):
    - Added SessionLogger which appends each completed activity to session_log.txt.
      This provides a simple persistent history of usage and is described here for grading.
    - Implemented no-repeat selection of prompts/questions until the pool is exhausted in a session.
    - All code is organized into separate files with appropriate encapsulation and naming conventions.
*/

class Program
{
    static void Main(string[] args)
    {
        string choice = "";
        while (choice != "4")
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.Write("\nSelect a choice from the menu: ");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                BreathingActivity breathing = new BreathingActivity();
                breathing.Run();
            }
            else if (choice == "2")
            {
                ReflectionActivity reflection = new ReflectionActivity();
                reflection.Run();
            }
            else if (choice == "3")
            {
                ListingActivity listing = new ListingActivity();
                listing.Run();
            }
            else if (choice == "4")
            {
                Console.WriteLine("\nGoodbye! Stay mindful!");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("Invalid choice. Try again.");
                Thread.Sleep(1000);
            }
        }
    }
}
