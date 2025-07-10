using System.Collections.ObjectModel;
using CanvasReminders.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;

namespace CanvasReminders.ViewModels;

public partial class ListDisplayViewModel : ViewModelBase
{
    private UserCalendar _calendar;
    public ObservableCollection<CourseViewModel> Courses { get; set; }
    public ObservableCollection<EventViewModel> AllEvents { get; set; }
    
    [ObservableProperty] private bool _isPaneOpen = true;
    
    [ObservableProperty]
    private ViewModelBase _currentPage;
    
    [ObservableProperty]
    private ViewModelBase _selectedItem;
    
    public ListDisplayViewModel()
    {
        _calendar = new UserCalendar();
        Courses = new ObservableCollection<CourseViewModel>();
        AllEvents = new ObservableCollection<EventViewModel>();
        // GetCalendar();
        foreach (Course course in _calendar.Courses)
        {
            Courses.Add(new CourseViewModel(course, _calendar));
        }
    }
    
    [RelayCommand]
    public void ButtonClick()
    {
        IsPaneOpen = !IsPaneOpen;
    }
    
    partial void OnSelectedItemChanged(ViewModelBase value)
    {
        CurrentPage = value;
    }
}