namespace QuizTestAndroidApp;

public partial class Question3Page : ContentPage
{
	public Question3Page()
	{
		InitializeComponent();
	}

    private async void Answer_Clicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;

        if (btn.Text == "Iron Man")
            await DisplayAlert("Correct!", "You chose the right answer.", "OK");
        else
            await DisplayAlert("Wrong!", "Try again.", "OK");

        
    }

}