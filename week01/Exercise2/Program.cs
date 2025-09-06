using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your percentage? ");
        string userinput = Console.ReadLine();
        int gradepercentage = int.Parse(userinput);

        string letter = "";

        if (gradepercentage >= 90)
        {
            letter = "A";
        }
        else if (gradepercentage >= 80)
        {
            letter = "B";
        }
        else if (gradepercentage >= 70)
        {
            letter = "C";
        }
        else if (gradepercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        Console.WriteLine($"Your grade is: {letter}");

        if (gradepercentage >= 70)
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("Better luck next time. Keep trying!");
        }
    }
}