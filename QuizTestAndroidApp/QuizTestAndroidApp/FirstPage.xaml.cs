namespace QuizTestAndroidApp;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		
	}

    private async void Rigth(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new SecondPage());
    }

    private async void Wrong(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            var originalColor = btn.BackgroundColor;
            btn.BackgroundColor = Colors.Red;
            await Task.Delay(1000);
            btn.BackgroundColor = originalColor;
        }
    }

}