using System;
using System.Collections.ObjectModel;
using System.IO;
using CanvasReminders.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CanvasReminders.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _needLink = false;

    private string _link;
    
    [ObservableProperty] 
    private ViewModelBase _currentWindow;

    public MainWindowViewModel()
    {
        // GetLink();
        CurrentWindow = new ListDisplayViewModel();
    }

    private void GetLink()
    {
        NeedLink = true;
        CurrentWindow = new InputViewModel();
    }

    partial void OnNeedLinkChanged(bool value)
    {
        if (value)
        {
            CurrentWindow = new InputViewModel();
        }
        else
        {
            CurrentWindow = new ListDisplayViewModel();
            NeedLink = false;
        }
    }


    // private UserCalendar _calendar;
    // public ObservableCollection<CourseViewModel> Courses { get; set; }
    //
    // [ObservableProperty] private bool _isPaneOpen = true;
    //
    // [ObservableProperty]
    // private ViewModelBase _currentPage;
    //
    // [ObservableProperty]
    
    // private ViewModelBase _selectedItem;
    //
    // public MainWindowViewModel()
    // {
    //     _calendar = new UserCalendar();
    //     Courses = new ObservableCollection<CourseViewModel>();
    //     // GetCalendar();
    //     foreach (Course course in _calendar.Courses)
    //     {
    //         Courses.Add(new CourseViewModel(course, _calendar));
    //     }
    // }
    //
    // [RelayCommand]
    // public void ButtonClick()
    // {
    //     IsPaneOpen = !IsPaneOpen;
    // }
    //
    // partial void OnSelectedItemChanged(ViewModelBase value)
    // {
    //     CurrentPage = value;
    // }
}