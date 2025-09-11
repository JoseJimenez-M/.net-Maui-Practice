using QuizTestAndroidApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTestAndroidApp.Data
{
    internal class QuestionData
    {

        public static List<Question> Questions = new()
        {
            new Question
            {
                Text = "What is the capital of France?",
                Options = new List<string> { "Berlin", "Madrid", "Paris" },
                CorrectOptionIndex = 2
            },
            new Question
            {
                Text = "Which planet is known as the Red Planet?",
                Options = new List<string> { "Venus", "Mars", "Jupiter" },
                CorrectOptionIndex = 1
            }
        };
    }

}

