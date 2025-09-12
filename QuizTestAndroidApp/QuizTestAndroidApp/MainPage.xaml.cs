namespace QuizTestAndroidApp
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoFirst(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Question2Page());
            //result page: SecondPage



        }
    }
}
