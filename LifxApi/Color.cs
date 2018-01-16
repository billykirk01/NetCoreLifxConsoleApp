using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightConsoleApp
{
    public class Color
    {

        #region Fields

        private int _hue;
        private int _saturation;
        private int _brightness;
        private int _kelvin;

        #endregion


        #region Properties

        public string Name { get; set; }

        public int Hue
        {
            get => _hue;
            set
            {
                if (value>=1 && value<=360)
                {
                    _hue = value;
                }
            }
        }

        public int Saturation
        {
            get => _saturation;
            set
            {
                if (value >= 1 && value <= 100)
                {
                    _saturation = value;
                }
            }
        }

        public int Brightness
        {
            get => _brightness;
            set
            {
                if (value >= 1 && value <= 100)
                {
                    _brightness = value;
                }
            }
        }

        public int Kelvin
        {
            get => _kelvin;
            set
            {
                if (value >= 1500 && value <= 9000)
                {
                    _kelvin = value;
                }
            }
        }

        #endregion


        #region Constructors

        public Color()
        {
        }

        public Color(string name)
        {
            Name = name;
        }

        public Color(int hue, int saturation, int brightness)
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
        }

        public Color(int kelvin, int brightness)
        {
            Kelvin = kelvin;
            Brightness = brightness;
        }


        #endregion


        #region Built In Colors

        public Color WhiteColor()
        {
            return new Color("white");
        }

        public Color RedColor()
        {
            return new Color("red");
        }

        public Color OrangeColor()
        {
            return new Color("orange");
        }

        public Color YellowColor()
        {
            return new Color("yellow");
        }

        public Color CyanColor()
        {
            return new Color("cyan");
        }

        public Color GreenColor()
        {
            return new Color("green");
        }

        public Color BlueColor()
        {
            return new Color("blue");
        }

        public Color PurpleColor()
        {
            return new Color("purple");
        }

        public Color PinkColor()
        {
            return new Color("pink");
        }

        #endregion

    }
}
