using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    
        public class ExpressionEvaluator
        {
            public double Evaluate(string expression)
            {
                try
                {
                    var result = new System.Data.DataTable().Compute(expression, null);
                    return Convert.ToDouble(result);
                }
                catch
                {
                    return double.NaN;
                }
            }
        }
    

}
