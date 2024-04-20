using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Diplom.ModelsDTo;
using Diplom.ViewModels;

namespace Diplom;

public partial class EditMenuWindow : Window
{
    public EditMenuWindow(MenuDTO menuDTO)
    {
        InitializeComponent();
        DataContext = new EditMenuWindowViewModel(menuDTO);
    }
}