using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Hosting;

namespace QuizTestAndroidApp
{
    public static class Fonts
    {
        public static void RegisterFonts(IFontCollection fonts)
        {
            // fonts
            fonts.AddFont("Oswald-VariableFont_wght.ttf", "Oswald"); 
        }
    }
}