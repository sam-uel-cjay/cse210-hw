using System;

namespace EternalQuest
{
    // A goal that never completes; each recording grants points.
    public class EternalGoal : Goal
    {
        public EternalGoal(string title, string description, int points)
            : base(title, description, points)
        {
        }

        public override string Display()
        {
            return $"[~] {Title} ({Description}) - {Points} pts each time (Eternal)";
        }

        public override int RecordEvent()
        {
            // Always grants points, never completes.
            return Points;
        }

        public override bool IsComplete => false;

        public override string GetStringRepresentation()
        {
            // Format: EternalGoal|Title|Description|Points
            return $"EternalGoal|{Escape(Title)}|{Escape(Description)}|{Points}";
        }

        private string Escape(string s) => s.Replace("|", "<pipe>");
        public static string Unescape(string s) => s.Replace("<pipe>", "|");

        public static EternalGoal FromString(string[] parts)
        {
            string title = Unescape(parts[0]);
            string desc = Unescape(parts[1]);
            int pts = int.Parse(parts[2]);
            return new EternalGoal(title, desc, pts);
        }
    }
}
