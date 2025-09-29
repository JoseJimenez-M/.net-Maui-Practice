using MovieHub.Models;
using MovieHub.Services;

namespace MovieHub;

public partial class MainPage : ContentPage
{
    private readonly ApiService _api = new();
    private readonly CancellationTokenSource _cts = new();
    private List<Genre> _genres = new();
    private List<Movie> _current = new();
    private string _lastQuery = string.Empty;
    private bool _isInitDone;

    public MainPage()
    {
        InitializeComponent();
        Loaded += async (_, __) => await InitAsync();
    }

    private async Task InitAsync()
    {
        try
        {
            SetBusy(true);

            // Load genres
            _genres = await _api.GetGenresAsync(_cts.Token);
            GenrePicker.ItemsSource = new[] { new Genre { Id = 0, Name = "All genres" } }
                                      .Concat(_genres).ToList();
            GenrePicker.SelectedIndex = 0;
            GenrePicker.SelectedIndexChanged += async (_, __) => await LoadByGenreOrSearchAsync();

            // Load now playing movies
            _current = await _api.GetNowPlayingAsync(_cts.Token);
            MoviesList.ItemsSource = _current;

            // Poster URL test
            var sample = _current.FirstOrDefault();
            if (sample != null)
            {
                System.Diagnostics.Debug.WriteLine($"Poster Path: {sample.PosterPath}");
                System.Diagnostics.Debug.WriteLine($"Poster URL : {sample.PosterUrl}");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            SetBusy(false);
            _isInitDone = true;
        }
    }


    private async Task LoadByGenreOrSearchAsync()
    {
        if (!_isInitDone) return;

        try
        {
            SetBusy(true);

            var selected = GenrePicker.SelectedItem as Genre;
            var hasQuery = !string.IsNullOrWhiteSpace(_lastQuery);

            if (hasQuery)
            {
                _current = await _api.SearchMoviesAsync(_lastQuery, _cts.Token);
            }
            else if (selected != null && selected.Id > 0)
            {
                _current = await _api.GetMoviesByGenreAsync(selected.Id, _cts.Token);
            }
            else
            {
                _current = await _api.GetNowPlayingAsync(_cts.Token);
            }

            MoviesList.ItemsSource = _current;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            SetBusy(false);
            RefreshHost.IsRefreshing = false;
        }
    }

    private void SetBusy(bool on)
    {
        BusySpinner.IsVisible = BusySpinner.IsRunning = on;
        MoviesList.Opacity = on ? 0.4 : 1.0;
        SearchButton.IsEnabled = !on;
        GenrePicker.IsEnabled = !on;
    }

    private async void OnSearchClicked(object? sender, EventArgs e)
    {
        _lastQuery = SearchBox.Text ?? string.Empty;
        await LoadByGenreOrSearchAsync();
    }

    private async void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        // Optional: live search when user clears text
        if (string.IsNullOrWhiteSpace(e.NewTextValue) && !string.IsNullOrWhiteSpace(e.OldTextValue))
        {
            _lastQuery = string.Empty;
            await LoadByGenreOrSearchAsync();
        }
    }

    private async void OnRefresh(object? sender, EventArgs e)
    {
        await LoadByGenreOrSearchAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _cts.Cancel();
        _cts.Dispose();
    }
}
