using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightConsoleApp
{
    public class State
    {

        #region Fields

        private Color _color;
        private bool _power;

        #endregion


        #region Properties

        internal Color Color { get => _color; set => _color = value; }
        public bool Power { get => _power; set => _power = value; }

        #endregion


        #region Constructors

        public State()
        {
        }

        public State(Color color, bool power = true)
        {
            Color = color;
            Power = power;
        }

        #endregion

    }
}
