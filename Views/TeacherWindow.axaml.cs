using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Diplom.Models;
using Diplom.ModelsDTo;
using Diplom.ViewModels;

namespace Diplom;

public partial class TeacherWindow : Window
{

    public TeacherWindow(Teacher teacher)
    {
        InitializeComponent();
        DataContext = new TeacherWindowViewModel(teacher);
    }
    private void ChangeStatus(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dataContext = (Student)button.DataContext;
        var requestWindow = new StudentWindow(dataContext, (TeacherWindowViewModel)DataContext);
        requestWindow.ShowDialog(this);
    }
}