using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightConsoleApp
{
    public class Group
    {
        
        #region Fields

        private string _id;
        private string _name;
        private List<Light> lights = new List<Light>();

        #endregion


        #region Properties

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public List<Light> Lights { get => lights; set => lights = value; }

        #endregion

    }
}
