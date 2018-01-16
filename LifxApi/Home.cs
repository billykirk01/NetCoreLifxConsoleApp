using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace LightConsoleApp
{
    public partial class Home
    {
        #region Fields

        private String _token;
        private RestClient _client;
        private List<Light> _lights;
        private List<Group> _groups;

        #endregion


        #region Properties

        public List<Light> Lights { get => _lights; set => _lights = value; }
        public List<Group> Groups { get => _groups; set => _groups = value; }

        #endregion


        #region Constructor

        public Home(string token)
        {
            this._token = token;
            this._client = new RestClient("https://api.lifx.com/v1/");
        }

        #endregion


        #region Initializer

        public async Task InitializeAsync()
        {

            var request = new RestRequest("lights/all", Method.GET);

            request.AddHeader("Authorization", string.Format("Bearer {0}", this._token));

            var restResponse = await _client.ExecuteTaskAsync(request);

            var response = JsonConvert.DeserializeObject<List<Light>>(restResponse.Content);

            this.Lights = response;

            this.Groups = CreateGroups(response);
        }

        //Helper function that creates the groups structure

        private List<Group> CreateGroups(List<Light> lights)
        {
            var GroupList = new List<Group>();
            var index = new int();

            foreach (var light in lights)
            {
                if (!GroupList.Any(x => x.Id == light.Group.Id))
                {
                    light.Group.Lights.Add(light);
                    GroupList.Add(light.Group);
                }
                else
                {
                    index = GroupList.FindIndex(x => x.Id == light.Group.Id);
                    GroupList[index].Lights.Add(light);
                }
            }

            return GroupList;
        }

        #endregion


        #region Home Functions

        public async Task HomeDetails()
        {
            if (this.Groups == null)
            {
                var task = this.InitializeAsync();
                await task;
            }

            foreach (var group in this.Groups)
            {
                Console.WriteLine(String.Format("{0}:", group.Name));

                foreach (var light in group.Lights)
                {
                    Console.WriteLine(String.Format(" - {0}", light.Label));
                }

                Console.WriteLine();
            }
        }

        public void ListGroups()
        {
            foreach (var group in this.Groups)
            {
                Console.WriteLine(String.Format("{0}:", group.Name));

            }
        }


        public void ListLights()
        {
            foreach (var light in this.Lights)
            {
                Console.WriteLine(String.Format("{0}", light.Label));
            }
        }


        public async Task SetState(string selector, State state)
        {
            var request = new RestRequest(string.Format("lights/{0}/state", selector), Method.PUT);

            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            switch (state.Power)
            {
                case true:
                    request.AddParameter("power", "on");
                    break;
                case false:
                    request.AddParameter("power", "off");
                    break;
            }

            if (state.Color.Name != null)
            {
                request.AddParameter("color", state.Color.Name);
            }
            else if (state.Color.Kelvin != 0)
            {
                request.AddParameter("color", String.Format("kelvin:{0} brightness:{1}", state.Color.Kelvin, (state.Color.Brightness) / 100.0));
            }
            else
            {
                request.AddParameter("color", String.Format("hue:{0} saturation:{1} brightness:{2}", state.Color.Hue, (state.Color.Saturation) / 100.0, (state.Color.Brightness) / 100.0));
            }

            var response = await _client.ExecuteTaskAsync(request);

        }


        public async Task TogglePower(string selector)
        {
            var request = new RestRequest(string.Format("lights/{0}/toggle", selector), Method.POST);

            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            var response = await _client.ExecuteTaskAsync(request);

        }


        public async Task SetBrightness(string selector, double brightness)
        {
            var request = new RestRequest(string.Format("lights/{0}/state", selector), Method.PUT);

            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            if (brightness <= 100 && brightness >= 0)
            {
                request.AddParameter("brightness", string.Format("{0}", brightness / 100));
            }

            var response = await _client.ExecuteTaskAsync(request);

        }


        public async Task SetPower(string selector, bool power)
        {
            var request = new RestRequest(string.Format("lights/{0}/state", selector), Method.PUT);

            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            switch (power)
            {
                case true:
                    request.AddParameter("power", "on");
                    break;
                case false:
                    request.AddParameter("power", "off");
                    break;
            }

            var response = await _client.ExecuteTaskAsync(request);

        }


        public async Task AllNormal()
        {
            var request = new RestRequest("lights/all/state", Method.PUT);

            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));
            request.AddParameter("power", "on");
            request.AddParameter("color", "kelvin:2500 brightness:1.0");

            var response = await _client.ExecuteTaskAsync(request);

        }

        #endregion

    }
}
