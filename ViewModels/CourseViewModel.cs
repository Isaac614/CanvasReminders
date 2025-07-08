using System.Collections.ObjectModel;
using CanvasReminders.Models;
using CommunityToolkit.Mvvm.Input;

namespace CanvasReminders.ViewModels;

public partial class CourseViewModel : ViewModelBase
{
    // private UserCalendar _calendar;
    private Course _course;
    public string CourseName { get; }
    public ObservableCollection<EventViewModel> Events { get; }

    public CourseViewModel(Course course)
    {
        _course = course;
        CourseName = course.CourseName;
        Events = new ObservableCollection<EventViewModel>();
        foreach (Event ev in course.Events)
        {
            Events.Add(new EventViewModel(ev));
        }
    }
    
    public CourseViewModel(Course course, UserCalendar calendar)
    {
        // _calendar = calendar;
        _course = course;
        CourseName = course.CourseName;
        Events = new ObservableCollection<EventViewModel>();
        foreach (Event ev in course.Events)
        {
            Events.Add(new EventViewModel(ev, calendar));
        }
    }
}