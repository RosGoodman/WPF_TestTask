using System.Drawing;
using System.Windows;

namespace WPF_TestTask;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.WindowState = WindowState.Maximized;

        //Bitmap bitmap = new(1000, 1000);
        //Graphics gfx = Graphics.FromImage(bitmap);

        //var pen = new Pen(Color.Black);
        //gfx.DrawRectangle(pen, 100, 120, 200, 200);

        //imageControl
    }
}