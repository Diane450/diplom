using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Diplom.Models;
using Diplom.ModelsDTo;
using Diplom.ViewModels;

namespace Diplom;

public partial class StudentWindow : Window
{
    public StudentWindow(Student student, TeacherWindowViewModel teacherWindowViewModel)
    {
        InitializeComponent();
        DataContext = new StudentWindowViewModel(student, teacherWindowViewModel);
    }
}