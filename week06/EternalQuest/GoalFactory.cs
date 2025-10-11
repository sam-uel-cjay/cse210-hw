using System;

namespace EternalQuest
{
    // Responsible for creating Goal objects from saved strings.
    public static class GoalFactory
    {
        public static Goal CreateFromSavedLine(string line)
        {
            // Saved format: Type|...
            // We'll split by '|' but Title/Description may contain '|' replaced by <pipe>
            string[] parts = line.Split('|');

            string type = parts[0];
            if (type == "SimpleGoal")
            {
                // parts: SimpleGoal|Title|Description|Points|IsComplete
                string[] payload = new string[] { parts[1], parts[2], parts[3], parts[4] };
                return SimpleGoal.FromString(payload);
            }
            else if (type == "EternalGoal")
            {
                // EternalGoal|Title|Description|Points
                string[] payload = new string[] { parts[1], parts[2], parts[3] };
                return EternalGoal.FromString(payload);
            }
            else if (type == "ChecklistGoal")
            {
                // ChecklistGoal|Title|Description|Points|Target|Bonus|TimesCompleted
                string[] payload = new string[] { parts[1], parts[2], parts[3], parts[4], parts[5], parts[6] };
                return ChecklistGoal.FromString(payload);
            }
            else
            {
                throw new Exception("Unknown goal type in saved file.");
            }
        }
    }
}
