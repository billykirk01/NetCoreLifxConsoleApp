using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightConsoleApp
{
    public class Light
    {
        
        #region Fields

        private string _id;
        private string _label;
        private Group _group;
        private string _connected;

        #endregion


        #region Properties

        public string Id { get => _id; set => _id = value; }
        public string Label { get => _label; set => _label = value; }
        public Group Group { get => _group; set => _group = value; }
        public string Connected { get => _connected; set => _connected = value; }

        #endregion

    }
}
