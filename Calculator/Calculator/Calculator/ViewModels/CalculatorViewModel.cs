using Calculator.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _previewText = "";
        private string _resultText = "0";
        private readonly ExpressionEvaluator _evaluator = new();

        public string PreviewText
        {
            get => _previewText;
            set { _previewText = value; OnPropertyChanged(); }
        }

        public string ResultText
        {
            get => _resultText;
            set { _resultText = value; OnPropertyChanged(); }
        }

        public void PressNumber(string number)
        {
            if (PreviewText.Length == 1 && PreviewText == "0")
                PreviewText = number;
            else
                PreviewText += number;
        }

        public void PressOperator(string op)
        {
            if (PreviewText.Length == 0 && op != "-")
                return;
            if (PreviewText.EndsWith("+") || PreviewText.EndsWith("-") ||
                PreviewText.EndsWith("*") || PreviewText.EndsWith("/"))
            {
                PreviewText = PreviewText[..^1] + op;
            }
            else
            {
                PreviewText += op;
            }
        }

        public void PressDecimal()
        {
            // Prevent multiple decimals in a single number segment
            if (string.IsNullOrEmpty(PreviewText) ||
                PreviewText.EndsWith("+") ||
                PreviewText.EndsWith("-") ||
                PreviewText.EndsWith("*") ||
                PreviewText.EndsWith("/"))
            {
                PreviewText += "0.";
            }
            else
            {
                int lastOp = Math.Max(Math.Max(PreviewText.LastIndexOf("+"), PreviewText.LastIndexOf("-")),
                               Math.Max(PreviewText.LastIndexOf("*"), PreviewText.LastIndexOf("/")));
                string lastNumber = lastOp >= 0 ? PreviewText[(lastOp + 1)..] : PreviewText;

                if (!lastNumber.Contains("."))
                    PreviewText += ".";
            }
        }
        public void Reciprocal()
        {
            if (double.TryParse(ResultText, out double value) && value != 0)
            {
                double result = 1 / value;
                ResultText = result.ToString();
                PreviewText = $"1/({value})";
            }
        }

        public void Percent()
        {
            if (double.TryParse(ResultText, out double value))
            {
                double result = value / 100;
                ResultText = result.ToString();
                PreviewText = $"{value}%";
            }
        }

        public void Negate()
        {
            if (double.TryParse(ResultText, out double value))
            {
                value = -value;
                ResultText = value.ToString();
                PreviewText = value.ToString();
            }
        }

        public void Clear()
        {
            PreviewText = "";
            ResultText = "0";
        }


        public void Delete()
        {
            if (!string.IsNullOrEmpty(PreviewText) && PreviewText != "0")
            {
                PreviewText = PreviewText.Length > 1 ? PreviewText[..^1] : "0";
            }
        }

        public void Calculate()
        {
            var result = _evaluator.Evaluate(PreviewText);
            ResultText = double.IsNaN(result) ? "Error" : result.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
