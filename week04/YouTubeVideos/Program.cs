using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video v1 = new Video("Intro to CSE210", "Samuel Chijike", 600);
        v1.AddComment(new Comment("Alice", "This was very helpful!"));
        v1.AddComment(new Comment("Ebuka", "I like how clear the explanation is."));
        v1.AddComment(new Comment("Chisom", "Can you make one on classes next?"));
        videos.Add(v1);

        // Video 2
        Video v2 = new Video("WDD Rafting Adventure", "Outdoor Channel", 300);
        v2.AddComment(new Comment("James", "Wow, looks fun!"));
        v2.AddComment(new Comment("Lily", "I want to go rafting now."));
        v2.AddComment(new Comment("Mike", "Great footage, thanks for sharing."));
        videos.Add(v2);

        // Video 3
        Video v3 = new Video("Cooking Indomie", "Chef Lobo", 420);
        v3.AddComment(new Comment("Sarah", "I tried this and it came out great!"));
        v3.AddComment(new Comment("Tom", "Where can I find the recipe?"));
        v3.AddComment(new Comment("Emma", "Delicious! My family loved it."));
        videos.Add(v3);

        // Display all videos
        foreach (Video video in videos)
        {
            video.Display();
        }
    }
}
