using System;
using System.Collections.Generic;

namespace CanvasReminders.Models;

public class Course
{
    public string CourseName { get; set; }
    public List<Event> Events { get; set; } = new();

    public Course(string courseName)
    {
        CourseName = courseName;
    }

    public void AddEvent(Event e)
    {
        Events.Add(e);
    }

    public void DisplayAssignments()
    {
        foreach (Event e in Events)
        {
            e.DisplayInfo();
        }
    }

    public void SortEventsByDate()
    {
        int end = Events.Count - 1;
        while (end > 1)
        {
            int largestIndex = 0;
            for (int i = 0; i < end; i++)
            {
                if (DateTime.Parse(Events[i].DueDate) > DateTime.Parse(Events[largestIndex].DueDate))
                {
                    largestIndex = i;
                }
            }
    
            var endVal = Events[end - 1];
            Events[end - 1] = Events[largestIndex];
            Events[largestIndex] = endVal;
            end -= 1;
        }
    }
}