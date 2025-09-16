using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace ColorMaker
{
    public partial class MainPage : ContentPage
    {
        private bool isRandom = false;
        private string hexValue = "#FFFFFF";

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!isRandom)
            {
                var color = Color.FromRgb((int)sldRed.Value, (int)sldGreen.Value, (int)sldBlue.Value);
                this.BackgroundColor = color;

                hexValue = $"#{(int)sldRed.Value:X2}{(int)sldGreen.Value:X2}{(int)sldBlue.Value:X2}";
                lblHex.Text = hexValue;
            }
        }

        // Generate random color
        private void OnRandomClicked(object sender, EventArgs e)
        {
            isRandom = true;
            Random rand = new Random();

            int r = rand.Next(0, 256);
            int g = rand.Next(0, 256);
            int b = rand.Next(0, 256);

            var color = Color.FromRgb(r, g, b);
            this.BackgroundColor = color;

            hexValue = $"#{r:X2}{g:X2}{b:X2}";
            lblHex.Text = hexValue;

            sldRed.Value = r;
            sldGreen.Value = g;
            sldBlue.Value = b;

            isRandom = false;
        }

        // Copy color code
        private async void OnCopyClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hexValue))
            {
                await Clipboard.SetTextAsync(hexValue);
                var toast = Toast.Make("Code copied", ToastDuration.Short, 14);
                await toast.Show();
            }
        }
    }
}
