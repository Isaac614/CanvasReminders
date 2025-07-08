using System.Collections.ObjectModel;
using CanvasReminders.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CanvasReminders.ViewModels;

public partial class ListDisplayViewModel : ViewModelBase
{
    private UserCalendar _calendar;
    public ObservableCollection<CourseViewModel> Courses { get; set; }

    [ObservableProperty] private bool _isPaneOpen = true;
    
    [ObservableProperty]
    private ViewModelBase _currentPage;
    
    [ObservableProperty]
    private ViewModelBase _selectedItem;

    public ListDisplayViewModel()
    {
        _calendar = new UserCalendar();
        Courses = new ObservableCollection<CourseViewModel>();
        // GetCalendar();
        foreach (Course course in _calendar.Courses)
        {
            Courses.Add(new CourseViewModel(course, _calendar));
        }
    }
    
    
    [RelayCommand] 
    private void ButtonClick()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    partial void OnSelectedItemChanged(ViewModelBase value)
    {
        CurrentPage = value;
    }
}