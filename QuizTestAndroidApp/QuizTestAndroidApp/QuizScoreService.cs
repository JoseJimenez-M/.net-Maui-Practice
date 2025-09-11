using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTestAndroidApp
{
    public static class QuizScoreService
    {
        private static int score = 0;

        public static int Score => score;

        public static void AddPoint(int points)
        {
            score += points;
        }

        public static void Reset()
        {
            score = 0;
        }
    }
}
