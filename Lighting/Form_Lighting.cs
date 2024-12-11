using Lighting.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace Lighting
{
    public partial class Form_Lighting : Form
    {
        private bool isLightOn = false;
        string baseURI = Properties.Settings.Default.RestApi; 
      

        RestClient client = null;
        public Form_Lighting()
        {
            InitializeComponent();
            client = new RestClient(baseURI);
            pictureBoxLightBulb.Image = Properties.Resources.unpowered_light_bulb_wide;
        }
        private void Form_Lighting_Load(object sender, EventArgs e)
        {
            Models.Application app = new Models.Application
            {
                id = 0,
                name = "Lighting" + Guid.NewGuid().ToString(),
                creation_datetime = DateTime.Now,
                res_type = "application"
            };

            RestRequest request = new RestRequest("api/somiod", Method.Post);
            request.RequestFormat = DataFormat.Xml;

            request.AddObject(app);
            
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("App created with name !" + response.Content);
            }
            else
            {
                MessageBox.Show($"Unable to create app: {response.StatusDescription}");
            }

            Models.Container cont = new Models.Container
            {
                id = 0,
                name = "light_bulb",
                creation_datetime = DateTime.Now,
                parent = 0,
                res_type = "container"
            };

            request = new RestRequest("api/somiod/lighting", Method.Post);
            request.RequestFormat = DataFormat.Xml;

            request.AddObject(cont);
            response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Cont created!");
            }
            else
            {
                MessageBox.Show($"Unable to create cont: {response.StatusDescription}");
            }

            Models.Notification notif = new Models.Notification
            {
                id = 0,
                name = "light_bulb",
                creation_datetime = DateTime.Now,
                parent = 0,
                @event = 0,
                endpoint = null, //TODO
                enabled = 1,
                res_type = "notificaton"
            };

            request = new RestRequest("api/somiod/lighting/light_bulb", Method.Post);
            request.RequestFormat = DataFormat.Xml;

            request.AddObject(notif);
            response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Notif created!");
            }
            else
            {
                MessageBox.Show($"Unable to create notif: {response.StatusDescription}");
            }

            while (true)
            {
                //TODO espera por broker
            }
        }

        private void pictureBoxLightBulb_Click(object sender, EventArgs e)
        {
            if (isLightOn)
            {
                pictureBoxLightBulb.Image = Properties.Resources.unpowered_light_bulb_wide;
                isLightOn = false;
            }
            else
            {
                pictureBoxLightBulb.Image = Properties.Resources.powered_light_bulb_wide;
                isLightOn = true;
            }
        }

    }
}
