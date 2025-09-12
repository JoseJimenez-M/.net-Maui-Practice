namespace QuizTestAndroidApp;

public partial class Sujal02 : ContentPage
{
    public Sujal02()
    {
        InitializeComponent();
        scoreLabel.Text = $"Score: {QuizScoreService.Score}";
    }

    private async void Right(object sender, EventArgs e)
    {
        CorrectAnswer(sender, e);
        await Navigation.PushAsync(new Sujal03());
    }

    private async void Wrong(object? sender, EventArgs e)
    {
        if (QuizScoreService.Score > 0)
        {
            WrongAnswer(sender, e);
        }

        if (sender is Button btn)
        {
            var originalColor = btn.BackgroundColor;
            btn.BackgroundColor = Colors.Red;
            await Task.Delay(1000);
            btn.BackgroundColor = originalColor;
        }
    }

    //SCORE
    private void CorrectAnswer(object sender, EventArgs e)
    {
        QuizScoreService.AddPoint(1);
        scoreLabel.Text = $"Score: {QuizScoreService.Score}";
    }

    private void WrongAnswer(object sender, EventArgs e)
    {
        QuizScoreService.AddPoint(-1);
        scoreLabel.Text = $"Score: {QuizScoreService.Score}";
    }
}