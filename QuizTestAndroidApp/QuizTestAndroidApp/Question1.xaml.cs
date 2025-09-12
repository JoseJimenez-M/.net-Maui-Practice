namespace QuizTestAndroidApp;

public partial class Question1 : ContentPage
{
	public Question1()
	{
		InitializeComponent();
	}
    private async void RigthAnswer(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            var originalTextColor = btn.TextColor;
            var originalColor = btn.BackgroundColor;
            btn.BackgroundColor = Colors.DarkGreen;
            btn.BorderColor = Colors.White;
            btn.TextColor = Colors.White;
            btn.FontAttributes = FontAttributes.Bold;
            await Task.Delay(200);
            await Navigation.PushAsync(new Question2());
            btn.BackgroundColor = originalColor;
            btn.TextColor = originalTextColor;
            btn.FontAttributes = FontAttributes.None;
            btn.BorderColor = Colors.Purple;


        }
    }

    private async void WrongAnswer(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            var originalTextColor = btn.TextColor;
            var originalColor = btn.BackgroundColor;
            btn.BackgroundColor = Colors.DarkRed;
            btn.BorderColor = Colors.White;
            btn.TextColor = Colors.White;
            btn.FontAttributes = FontAttributes.Bold;
            await Task.Delay(500);
            btn.BackgroundColor = originalColor;
            btn.TextColor = originalTextColor;
            btn.FontAttributes = FontAttributes.None;
            btn.BorderColor = Colors.Purple;
        }
    }

}