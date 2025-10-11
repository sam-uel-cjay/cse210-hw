using System;

namespace EternalQuest
{
    // a goal that is completed once.
    public class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string title, string description, int points, bool isComplete = false)
            : base(title, description, points)
        {
            _isComplete = isComplete;
        }

        public override string Display()
        {
            string box = _isComplete ? "[X]" : "[ ]";
            return $"{box} {Title} ({Description}) - {Points} pts";
        }

        public override int RecordEvent()
        {
            if (_isComplete)
            {
                Console.WriteLine("This goal is already completed.");
                return 0;
            }
            _isComplete = true;
            return Points;
        }

        public override bool IsComplete => _isComplete;

        public override string GetStringRepresentation()
        {
            return $"SimpleGoal|{Escape(Title)}|{Escape(Description)}|{Points}|{_isComplete}";
        }

        private string Escape(string s) => s.Replace("|", "<pipe>");
        public static string Unescape(string s) => s.Replace("<pipe>", "|");

        // the factory helper used by GoalFactory
        public static SimpleGoal FromString(string[] parts)
        {
            // parts expected: Title, Description, Points, IsComplete
            string title = Unescape(parts[0]);
            string desc = Unescape(parts[1]);
            int pts = int.Parse(parts[2]);
            bool isComp = bool.Parse(parts[3]);
            return new SimpleGoal(title, desc, pts, isComp);
        }
    }
}
