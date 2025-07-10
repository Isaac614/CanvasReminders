using System;
using CanvasReminders.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CanvasReminders.ViewModels;

public partial class EventViewModel : ViewModelBase
{
    private UserCalendar _calendar;
    
    private Event _event;
    
    [ObservableProperty]
    private string _courseName;

    [ObservableProperty] 
    private string _summary;
    
    [ObservableProperty]
    private string _dueDate;
    
    [ObservableProperty]
    private bool _completed;
    
    [ObservableProperty]
    private string _description;
    public EventViewModel(Event e)
    {
        _event = e;
        CourseName = e.Classname;
        Summary = e.Summary;
        DueDate = e.DueDate;
        Completed = e.Completed;
        Description = e.Description;
    }
    
    public EventViewModel(Event e, UserCalendar calendar)
    {
        _event = e;
        CourseName = e.Classname;
        Summary = e.Summary;
        DueDate = e.DueDate;
        Completed = e.Completed;
        Description = e.Description;
        _calendar = calendar;
    }
    
    partial void OnCompletedChanged(bool value)
    {
        _event.Completed = value;
        // _calendar.SaveToFile();
    }

    // [RelayCommand]
    // private void CheckboxClick()
    // {
    //     _calendar.SaveToFile();
    // }
}