using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightConsoleApp
{
    class Menu
    {

        #region Constructor

        public Menu(Home home)
        {
            MainMenu(home);
        }

        #endregion


        #region Main Menu

        public void MainMenu(Home home)
        {
            Console.Clear();
            Console.WriteLine("---------------Lifx Menu--------------");
            Console.WriteLine();
            Console.WriteLine("1. Print out home structure.");
            Console.WriteLine("2. Toggle power of home/group/light.");
            Console.WriteLine("3. Set a state of home/group/light");
            Console.WriteLine();
            Console.Write("Option: ");
            int.TryParse(Console.ReadLine(), out var selection);

            switch (selection)
            {
                case 1:
                    Console.Clear();
                    HomeStructure(home);
                    break;
                case 2:
                    Console.Clear();
                    TogglePowerMenu(home);
                    break;
                case 3:
                    Console.Clear();
                    SetStateMenu(home);
                    break;
                default:
                    break;
            }

        }


        #endregion


        #region Menu Option 1: Home Structure

        public void HomeStructure(Home home)
        {
            Console.WriteLine("Home Structure:");
            Console.WriteLine();
            var task = home.HomeDetails();
            Console.ReadLine();
            Console.Clear();
            this.MainMenu(home);
        }

        #endregion


        #region Menu Option 2: Toggle Power

        public void TogglePowerMenu(Home home)
        {
            Console.Clear();

            Console.WriteLine("Toggle Power Menu:");

            var groupingSelection = GroupingMenu(home, new Action<Home>(MainMenu));

            var selector = SelectorMenu(home, groupingSelection, new Action<Home>(TogglePowerMenu));

            if (selector == "") { return; }

            var task = home.TogglePower(selector);

            MainMenu(home);

        }

        #endregion


        #region Menu Option 3: Set State

        private void SetStateMenu(Home home)
        {
            Console.Clear();

            Console.WriteLine("Set State Menu:");

            var groupingSelection = GroupingMenu(home, new Action<Home>(MainMenu));

            var selector = SelectorMenu(home, groupingSelection, new Action<Home>(SetStateMenu));

            if (selector == "") { return; }

            var state = StateTypeSelectionMenu();

            var task = home.SetState(selector, state);

            MainMenu(home);

        }

        private State StateTypeSelectionMenu()
        {
            Console.Clear();
            Console.WriteLine("State Type Menu:");
            Console.WriteLine();
            Console.WriteLine("1. Hue, Saturation, Brightness");
            Console.WriteLine("2. Kelvin brightness");
            Console.WriteLine();
            Console.Write("Option: ");
            int.TryParse(Console.ReadLine(), out var stateTypeSelection);
            Console.Clear();

            var state = new State();

            switch (stateTypeSelection)
            {
                case 1:
                    state = HSBStateBuilder();
                    break;
                case 2:
                    state = KBStateBuilder();
                    break;
                default:
                    break;
            }

            return state;

        }

        private State HSBStateBuilder()
        {
            int Hue;
            int Saturation;
            int Brightness;

            Console.WriteLine("State Builder:");
            Console.WriteLine();

            Console.Write("Hue: ");
            Hue = int.Parse(Console.ReadLine());

            Console.Write("Saturation: ");
            Saturation = int.Parse(Console.ReadLine());

            Console.Write("Brightness: ");
            Brightness = int.Parse(Console.ReadLine());

            var color = new Color(Hue, Saturation, Brightness);

            return new State(color);
        }

        private State KBStateBuilder()
        {
            int Kelvin;
            int Brightness;

            Console.WriteLine("State Builder:");
            Console.WriteLine();

            Console.Write("Kelvin: ");
            Kelvin = int.Parse(Console.ReadLine());

            Console.Write("Brightness: ");
            Brightness = int.Parse(Console.ReadLine());

            var color = new Color(Kelvin, Brightness);

            return new State(color);
        }

        #endregion


        #region Grouping and Selector Menus

        public int GroupingMenu(Home home, Action<Home> escapingMenu)
        {
            Console.WriteLine();
            Console.WriteLine("1. All lights");
            Console.WriteLine("2. A Group of lights");
            Console.WriteLine("3. A single light");
            Console.WriteLine();
            Console.Write("Option: ");
            int.TryParse(Console.ReadLine(), out var groupingSelection);
            Console.Clear();

            if (groupingSelection == 0)
            {
                escapingMenu(home);
            }

            return groupingSelection;
        }

        public string SelectorMenu(Home home, int groupingSelection, Action<Home> escapingMenu)
        {
            string selectorString = "";

            switch (groupingSelection)
            {
                case 1:
                    selectorString = "all";
                    break;
                case 2:
                    selectorString = GroupSelectorMenu(home, escapingMenu);
                    break;
                case 3:
                    selectorString = LightSelectorMenu(home, escapingMenu);
                    break;
                default:
;                    break;
            }

            return selectorString;

        }

        public string GroupSelectorMenu(Home home, Action<Home> escapingMenu)
        {
            var groupBaseIndex = 1;
            foreach (var group in home.Groups)
            {
                Console.WriteLine("{0}. {1}", groupBaseIndex, group.Name);
                groupBaseIndex++;
            }
            Console.WriteLine();
            Console.Write("Option: ");
            int.TryParse(Console.ReadLine(), out var groupIndex);

            if (groupIndex == 0)
            {
                escapingMenu(home);
            }

            if (home.Groups.Count >= groupIndex && groupIndex > 0)
            {
                var groupName = home.Groups[groupIndex - 1].Name;
                return string.Format("group:{0}", groupName);
            }
            else
            {
                Console.Clear();
                this.MainMenu(home);
                return "";
            }
        }

        public string LightSelectorMenu(Home home, Action<Home> escapingMenu)
        {
            var lightBaseIndex = 1;
            foreach (var light in home.Lights)
            {
                Console.WriteLine("{0}. {1}", lightBaseIndex, light.Label);
                lightBaseIndex++;
            }
            Console.WriteLine();
            Console.Write("Option: ");
            int.TryParse(Console.ReadLine(), out var lightIndex);

            if (lightIndex == 0)
            {
                escapingMenu(home);
            }

            if (home.Lights.Count >= lightIndex && lightIndex > 0)
            {
                var lightName = home.Lights[lightIndex - 1].Label;
                return string.Format("label:{0}", lightName);
            }
            else
            {
                Console.Clear();
                this.MainMenu(home);
                return "";
            }
        }

        #endregion

    }
}
