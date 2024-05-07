namespace T4ResDemo.Wpf;
using System.Windows;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Strings.SetCulture("en-US");
    }
}
