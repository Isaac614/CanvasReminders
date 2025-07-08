using System;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.Input;

namespace CanvasReminders.Models;

public class Event
{
    public string Classname { get; set; }
    public string Summary { get; set; }
    public string DueDate { get; set; }
    public bool Completed { get; set; }
    public string Description { get; set; }

    public Event(string classname, string summary, string dueDate, string description)
    {
        Classname = classname;
        Summary = summary;
        DueDate = dueDate;
        Completed = false;
        Description = description;
    }
    
    [JsonConstructor]
    public Event(string classname, string summary, string dueDate, bool completed, string description)
    {
        Classname = classname;
        Summary = summary;
        DueDate = dueDate;
        Completed = completed;
        Description = description;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"{Classname} - {Summary}\n{DueDate}\n");
    }
}