namespace QuizTestAndroidApp;

public partial class SecondPage : ContentPage
{
	public SecondPage()
	{
		InitializeComponent();
        scoreLabel.Text = $"Score: {QuizScoreService.Score}";
    }

    private async void GoMain(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
        // await Navigation.PopAsync(); // Navigate back 

        //Restart score
        QuizScoreService.Reset();
        
    }

}