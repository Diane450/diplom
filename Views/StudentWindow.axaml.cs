using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Diplom.Models;
using Diplom.ModelsDTo;
using Diplom.ViewModels;
using System.Threading.Tasks;

namespace Diplom;

public partial class StudentWindow : Window
{
    public StudentWindow(StudentsDTO student)
    {
        InitializeComponent();
        DataContext = new StudentWindowViewModel(student);
    }
    public static async Task GetContent()
    {
        await StudentWindowViewModel.GetContent();
    }
}