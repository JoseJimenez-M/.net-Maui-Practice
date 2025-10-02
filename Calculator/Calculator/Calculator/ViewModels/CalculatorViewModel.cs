using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Calculator.Models;

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
            PreviewText += number;
        }

        public void PressOperator(string op)
        {
            PreviewText += op;
        }

        public void Clear()
        {
            PreviewText = "";
            ResultText = "0";
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
