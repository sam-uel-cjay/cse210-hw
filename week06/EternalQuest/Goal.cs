using System;

namespace EternalQuest
{
    // The base class for all the goals
    public abstract class Goal
    {
        // The encapsulated member variables
        private string _title;
        private string _description;
        private int _points; // points thata is awarded for the action (or base points)

        protected Goal(string title, string description, int points)
        {
            _title = title;
            _description = description;
            _points = points;
        }

        public string Title => _title;
        public string Description => _description;
        public int Points => _points;

        // displays friendly form (for listing)
        public abstract string Display();

        // records an accomplishment of the goal, returning points earned this event
        public abstract int RecordEvent();

        // whether the goal is considered complete (eternal goals return false)
        public abstract bool IsComplete { get; }

        // the string representation for saving to file
        public abstract string GetStringRepresentation();
    }
}
