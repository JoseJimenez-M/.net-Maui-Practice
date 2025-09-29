namespace MovieHub;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.MovieDetailsPage), typeof(Views.MovieDetailsPage));
    }
}
