using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    /*
     * The added creativty requirement
     * I was able to implement the required goals and persistence, and added:
     *  A simple Leveling system: where the player gains levels every 1000 points.
     *  Also Badges fr when reaching certain point thresholds or completing specific checklist goals, badges will be awarded.
     *  All the enhancements are documented here and the user can see their level and badges in the main menu.
     * This comment serves as the "explain what you did to exceed requirements" so i explained it, hehehe.
     */

    class Program
    {
        static List<Goal> _goals = new List<Goal>();
        static int _score = 0;
        static List<string> _badges = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Eternal Quest!");
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== Main Menu ===");
                Console.WriteLine($"Score: {_score}   Level: {GetLevel()}   Badges: {string.Join(", ", _badges)}");
                Console.WriteLine("1. Create a new goal");
                Console.WriteLine("2. List goals");
                Console.WriteLine("3. Record an event (mark goal accomplished)");
                Console.WriteLine("4. Save goals");
                Console.WriteLine("5. Load goals");
                Console.WriteLine("6. Show score and details");
                Console.WriteLine("7. Quit");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": CreateGoal(); break;
                    case "2": ListGoals(); break;
                    case "3": RecordEvent(); break;
                    case "4": SaveToFile(); break;
                    case "5": LoadFromFile(); break;
                    case "6": ShowScoreDetails(); break;
                    case "7": running = false; break;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        static void CreateGoal()
        {
            Console.WriteLine("\nChoose goal type:");
            Console.WriteLine("1. Simple Goal (one-time)");
            Console.WriteLine("2. Eternal Goal (repeatable)");
            Console.WriteLine("3. Checklist Goal (N times + bonus)");
            Console.Write("Choice: ");
            string c = Console.ReadLine();
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Description: ");
            string desc = Console.ReadLine();
            Console.Write("Points (integer): ");
            int pts = int.Parse(Console.ReadLine() ?? "0");

            if (c == "1")
            {
                _goals.Add(new SimpleGoal(title, desc, pts));
                Console.WriteLine("Simple goal created.");
            }
            else if (c == "2")
            {
                _goals.Add(new EternalGoal(title, desc, pts));
                Console.WriteLine("Eternal goal created.");
            }
            else if (c == "3")
            {
                Console.Write("Target count (how many times to complete): ");
                int target = int.Parse(Console.ReadLine() ?? "1");
                Console.Write("Bonus points on completion: ");
                int bonus = int.Parse(Console.ReadLine() ?? "0");
                _goals.Add(new ChecklistGoal(title, desc, pts, target, bonus));
                Console.WriteLine("Checklist goal created.");
            }
            else
            {
                Console.WriteLine("Invalid type.");
            }
        }

        static void ListGoals()
        {
            Console.WriteLine("\nYour goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i+1}. {_goals[i].Display()}");
            }
            if (_goals.Count == 0) Console.WriteLine("No goals yet.");
        }

        static void RecordEvent()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals to record. Create some first.");
                return;
            }
            ListGoals();
            Console.Write("Enter the number of the goal you accomplished: ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > _goals.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }
            var goal = _goals[idx - 1];
            int gained = goal.RecordEvent();
            if (gained <= 0)
            {
                Console.WriteLine("No points awarded for this action.");
                return;
            }
            _score += gained;
            Console.WriteLine($"You earned {gained} points! New score: {_score}");

            // creative: award badge if reaching thresholds
            AwardBadges();
        }

        static void SaveToFile()
        {
            Console.Write("Enter filename to save to (e.g. save.txt): ");
            string filename = Console.ReadLine();
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    // first line: score
                    sw.WriteLine(_score);
                    // badges on second line (comma separated)
                    sw.WriteLine(string.Join(",", _badges));
                    // then goals, one per line using GetStringRepresentation
                    foreach (var g in _goals)
                    {
                        sw.WriteLine(g.GetStringRepresentation());
                    }
                }
                Console.WriteLine($"Saved to {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }

        static void LoadFromFile()
        {
            Console.Write("Enter filename to load from: ");
            string filename = Console.ReadLine();
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filename);
                if (lines.Length >= 1)
                {
                    _score = int.Parse(lines[0]);
                }
                _badges.Clear();
                if (lines.Length >= 2)
                {
                    string badgesLine = lines[1];
                    if (!string.IsNullOrWhiteSpace(badgesLine))
                    {
                        _badges.AddRange(badgesLine.Split(','));
                    }
                }
                _goals.Clear();
                for (int i = 2; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    Goal g = GoalFactory.CreateFromSavedLine(line);
                    _goals.Add(g);
                }
                Console.WriteLine("Loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
            }
        }

        static void ShowScoreDetails()
        {
            Console.WriteLine($"\nScore: {_score}");
            Console.WriteLine($"Level: {GetLevel()} (next level at {((GetLevel()+1)*1000)} pts)");
            Console.WriteLine($"Badges: { ( _badges.Count == 0 ? "None" : string.Join(", ", _badges) ) }");
        }

        // Simple leveling: every 1000 points
        static int GetLevel()
        {
            return (_score / 1000) + 1;
        }

        // Award basic badges
        static void AwardBadges()
        {
            if (_score >= 500 && !_badges.Contains("500 Club"))
            {
                _badges.Add("500 Club");
                Console.WriteLine("🏅 Badge earned: 500 Club!");
            }
            if (_score >= 1000 && !_badges.Contains("1000 Club"))
            {
                _badges.Add("1000 Club");
                Console.WriteLine("🏅 Badge earned: 1000 Club!");
            }

            // Award badge if any checklist goal completed
            foreach (var g in _goals)
            {
                if (g is ChecklistGoal cg && cg.IsComplete)
                {
                    if (!_badges.Contains("Checklist Finisher"))
                    {
                        _badges.Add("Checklist Finisher");
                        Console.WriteLine("🏅 Badge earned: Checklist Finisher!");
                    }
                }
            }
        }
    }
}
