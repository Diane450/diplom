using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Diplom.ViewModels;

namespace Diplom;

public partial class NewMenuWindow : Window
{
    public NewMenuWindow(AdminWindowViewModel adminWindowViewModel)
    {
        InitializeComponent();
        DataContext = new NewMenuWindowViewModel(adminWindowViewModel);
    }
}