using MovieHub.Models;

namespace MovieHub.Views;

[QueryProperty(nameof(Movie), "Movie")]
public partial class MovieDetailsPage : ContentPage
{
    public MovieDetailsPage()
    {
        InitializeComponent();
    }

    public Movie? Movie
    {
        get => BindingContext as Movie;
        set => BindingContext = value;
    }
}
