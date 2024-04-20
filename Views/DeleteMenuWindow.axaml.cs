using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Diplom.ModelsDTo;
using Diplom.ViewModels;

namespace Diplom;

public partial class DeleteMenuWindow : Window
{
    public DeleteMenuWindow(MenuDTO menuDTO, AdminWindowViewModel adminWindowViewModel)
    {
        InitializeComponent();
        DataContext = new DeleteMenuWindowViewModel(menuDTO, adminWindowViewModel);
    }
    private void Close(object sender, RoutedEventArgs e)
    {
        Close();
    }
}