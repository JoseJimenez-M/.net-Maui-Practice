using Calculator.ViewModels;

namespace Calculator;

public partial class MainPage : ContentPage
{
    private CalculatorViewModel VM => BindingContext as CalculatorViewModel;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnNumberClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        VM?.PressNumber(btn.Text);
    }

    private void OnOperatorClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        VM?.PressOperator(btn.Text);
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        VM?.Clear();
    }

    private void OnEqualClicked(object sender, EventArgs e)
    {
        VM?.Calculate();
    }

    private void OnDecimalClicked(object sender, EventArgs e)
    {
        (BindingContext as CalculatorViewModel)?.PressDecimal();
    }
    private void OnReciprocalClicked(object sender, EventArgs e) =>
    (BindingContext as CalculatorViewModel)?.Reciprocal();

    private void OnPercentClicked(object sender, EventArgs e) =>
        (BindingContext as CalculatorViewModel)?.Percent();

    private void OnNegateClicked(object sender, EventArgs e) =>
        (BindingContext as CalculatorViewModel)?.Negate();

}
