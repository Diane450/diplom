using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Diplom.Models;
using Diplom.ViewModels;

namespace Diplom;

public partial class DishDetailsWindow : Window
{
    public DishDetailsWindow(MenuDish menuDish)
    {
        InitializeComponent();
        DataContext = new DishDetailsWindowViewModel(menuDish);
    }
}