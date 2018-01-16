using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightConsoleApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var home = new Home("cf96509c257dc3144c43be8e1aaae2728220f386c3c94450b859ea10b06f1b1e");
            var initilizeTask = home.InitializeAsync();

            var mainMenu = new Menu(home);

        }
    }
}
