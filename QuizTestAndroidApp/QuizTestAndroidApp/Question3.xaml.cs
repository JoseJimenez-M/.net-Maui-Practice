namespace QuizTestAndroidApp;

public partial class Question3 : ContentPage
{
	public Question3()
	{
		InitializeComponent();
	}
    private async void RigthAnswer(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            var originalTextColor = btn.TextColor;
            var originalColor = btn.BackgroundColor;
            var originalFontAt = btn.FontAttributes;
            btn.BackgroundColor = Colors.DarkGreen;
            btn.BorderColor = Colors.White;
            btn.TextColor = Colors.White;
            btn.FontAttributes = FontAttributes.Bold;
            await Task.Delay(200);
            await Navigation.PushAsync(new SecondPage());
            btn.BackgroundColor = originalColor;
            btn.TextColor = originalTextColor;
            btn.FontAttributes = originalFontAt;
            btn.BorderColor = Colors.White;


        }
    }

    private async void WrongAnswer(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            var originalTextColor = btn.TextColor;
            var originalColor = btn.BackgroundColor;
            var originalFontAt = btn.FontAttributes;

            btn.BackgroundColor = Colors.DarkRed;
            btn.BorderColor = Colors.White;
            btn.TextColor = Colors.White;
            btn.FontAttributes = FontAttributes.Bold;
            await Task.Delay(500);
            btn.BackgroundColor = originalColor;
            btn.TextColor = originalTextColor;
            btn.FontAttributes = originalFontAt;
            btn.BorderColor = Colors.White;
        }
    }
}