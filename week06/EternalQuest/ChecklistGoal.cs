using System;

namespace EternalQuest
{
    // A goal that must be completed a certain number of times. Each time gives base points,
    // and when target is reached, award a bonus.
    public class ChecklistGoal : Goal
    {
        private int _timesCompleted;
        private int _targetCount;
        private int _bonusPoints;

        public ChecklistGoal(string title, string description, int pointsPerCompletion, int targetCount, int bonusPoints, int timesCompleted = 0)
            : base(title, description, pointsPerCompletion)
        {
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _timesCompleted = timesCompleted;
        }

        public override string Display()
        {
            string completedMark = (_timesCompleted >= _targetCount) ? "[X]" : "[ ]";
            return $"{completedMark} {Title} ({Description}) - {Points} pts each. Progress: {_timesCompleted}/{_targetCount}. Bonus on completion: {_bonusPoints} pts";
        }

        public override int RecordEvent()
        {
            if (_timesCompleted >= _targetCount)
            {
                Console.WriteLine("Checklist goal already finished.");
                return 0;
            }

            _timesCompleted++;
            int earned = Points;
            if (_timesCompleted == _targetCount)
            {
                // award bonus as well
                earned += _bonusPoints;
                Console.WriteLine($"🎉 Goal completed! You earned a bonus of {_bonusPoints} points!");
            }
            return earned;
        }

        public override bool IsComplete => _timesCompleted >= _targetCount;

        public override string GetStringRepresentation()
        {
            return $"ChecklistGoal|{Escape(Title)}|{Escape(Description)}|{Points}|{_targetCount}|{_bonusPoints}|{_timesCompleted}";
        }

        private string Escape(string s) => s.Replace("|", "<pipe>");
        public static string Unescape(string s) => s.Replace("<pipe>", "|");

        public static ChecklistGoal FromString(string[] parts)
        {
            string title = Unescape(parts[0]);
            string desc = Unescape(parts[1]);
            int pts = int.Parse(parts[2]);
            int target = int.Parse(parts[3]);
            int bonus = int.Parse(parts[4]);
            int timesCompleted = int.Parse(parts[5]);
            return new ChecklistGoal(title, desc, pts, target, bonus, timesCompleted);
        }
    }
}
