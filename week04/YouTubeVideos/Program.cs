using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("Unboxing the Ultimate Gaming Mouse", "TechTom", 540);
        video1.AddComment(new Comment("Maria", "Great review, very detailed!"));
        video1.AddComment(new Comment("Carlos", "Where can I buy this mouse?"));
        video1.AddComment(new Comment("Ana", "I have the same one and I love it."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Easy Pasta Recipe in 10 Minutes", "ChefLaura", 615);
        video2.AddComment(new Comment("Pedro", "Made this last night, delicious!"));
        video2.AddComment(new Comment("Sofia", "Can I use whole wheat pasta?"));
        video2.AddComment(new Comment("Luis", "Thanks for the quick tips."));
        video2.AddComment(new Comment("Elena", "Subscribed!"));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Top 5 Hiking Trails in Utah", "OutdoorAdventures", 1230);
        video3.AddComment(new Comment("Jake", "Number 3 is my favorite."));
        video3.AddComment(new Comment("Lily", "The drone footage is stunning."));
        video3.AddComment(new Comment("Mateo", "Adding these to my bucket list."));
        videos.Add(video3);

        // Video 4
        Video video4 = new Video("Beginner Guitar Lesson: First Chords", "MusicWithMia", 900);
        video4.AddComment(new Comment("Daniel", "Finally a lesson that makes sense."));
        video4.AddComment(new Comment("Valeria", "My fingers hurt but I'm learning!"));
        video4.AddComment(new Comment("Tom", "Could you do a lesson on barre chords?"));
        videos.Add(video4);

        // Display all videos
        foreach (Video video in videos)
        {
            video.Display();
        }
    }
}