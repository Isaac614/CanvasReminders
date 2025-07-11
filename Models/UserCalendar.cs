using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.Proxies;

namespace CanvasReminders.Models;

public class UserCalendar
{
    private string? _link;
    private string? _rawData;
    private List<Course> _courses = new();
    public Course AllEvents { get; set; }

    public List<Course> Courses
    {
        get { return _courses; }
    }
    
    public UserCalendar()
    {
        _link = "https://byui.instructure.com/feeds/calendars/user_MW9zKHiVd9h9cuWWsZjt5i1zHLRYUrt3wzEo4xjC.ics";
        Initialize();
    }

    private void Initialize()
    {
        if (File.Exists("Models/CalData.json"))
        {
            LoadFromFile();
        }
        else
        {
            using (File.Create("Models/CalData.ics")) { }
            using (File.Create("Models/CalData.json")) { }
            RequestFile(_link, "Models/CalData.ics");
            ParseData(_rawData);
            SortByDate();
            // CreateCombinedCourse();
            SaveToFile();
        }
    }
    
    

    public void GetLink()
    {
        Console.WriteLine("Copy and paste the ical link in canvas: ");
        _link = Console.ReadLine();
    }

    private void RequestFile(string link, string filepath)
    {
        HttpClient client = new();

        try
        {
            string data = client.GetStringAsync(_link).Result;
            _rawData = data;
            SaveToFile(filepath, data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void SaveToFile(string filepath, string text)
    {
        File.WriteAllText(filepath, text);
    }
    //
    // public void SaveToFile(string filepath)
    // {
    //     File.WriteAllText(filepath, _rawData);
    // }
    
    public void SaveToFile()
    {
        string jsonString = JsonSerializer.Serialize(_courses, new JsonSerializerOptions{ WriteIndented = true });
        File.WriteAllText("Models/CalData.json", jsonString);
    }

    public void LoadFromFile()
    {
        string jsonString = File.ReadAllText("Models/CalData.json");
        List<Course> courses = JsonSerializer.Deserialize<List<Course>>(jsonString);
        _courses = courses;
    }

    private void ParseData(string data)
    {
        Calendar calendar = Calendar.Load(data);
        var events = calendar.Events;
        Dictionary<string, List<Dictionary<string, string>>> parsedEvents = new();

        List<string> classes = new();
        foreach (CalendarEvent ev in events)
        {
            if (!String.IsNullOrEmpty(ev.Summary))
            {
                // Dictionary<string, string> eventInfo = new();
                int startInd = ev.Summary.IndexOf("[", StringComparison.Ordinal);
                int endInd = ev.Summary.IndexOf("]", StringComparison.Ordinal);
                if (startInd != -1 && endInd != -1)
                {
                    string classname = ev.Summary.Substring(startInd + 1, endInd - startInd - 1);
                    string summary = ev.Summary.Substring(0, startInd);
                    string dueDate = "";

                    if (ev.DtEnd != null)
                    {
                        dueDate = ev.DtEnd.Value.ToShortDateString();
                    }
                    else if (ev.DtStart != null)
                    {
                        dueDate = ev.DtStart.Value.ToShortDateString();
                    }

                    string? description = ev.Description;
                    if (description == null)
                    {
                        description = "";
                    }
                    
                    bool courseFound = false;
                    foreach (Course course in _courses)
                    {
                        if (course.CourseName == classname)
                        {
                            course.AddEvent(new Event(classname, summary, dueDate, description));
                            courseFound = true;
                        }
                    }

                    if (!courseFound)
                    {
                        Course newCourse = new Course(classname);
                        newCourse.AddEvent(new Event(classname, summary, dueDate, description));
                        _courses.Add(newCourse);
                    }
                }
            }
        }
    }

    private void SortByDate()
    {
        foreach (Course course in _courses)
        {
            course.SortEventsByDate();
        }
    }


    private void CreateCombinedCourse()
    {
        AllEvents = new Course("All Reminders");
        AllEvents.Events = new List<Event>(
            Courses.SelectMany(c => c.Events)
        );
        AllEvents.SortEventsByDate();
    }
}