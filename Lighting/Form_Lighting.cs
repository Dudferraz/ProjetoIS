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
using System.Xml;


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
                name = "Lighting",
                creation_datetime = DateTime.Now,
                res_type = "application"
            };

            RestRequest request = new RestRequest("api/somiod", Method.Post);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");

            request.AddObject(app);
            
            var response = client.Execute(request);

            string xmlContent = response.Content;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("default", "http://schemas.datacontract.org/2004/07/Somiod.Models");

            XmlNode nameNode = xmlDoc.SelectSingleNode("/default:Application/default:name", namespaceManager);

            string app_name = nameNode?.InnerText;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show($"Lighting app created with name: {app_name}");
            }
            else
            {
                MessageBox.Show($"Unable to create lighting app: {response.StatusDescription}");
            }

            Models.Container cont = new Models.Container
            {
                id = 0,
                name = "light_bulb",
                creation_datetime = DateTime.Now,
                parent = 0,
                res_type = "container"
            };

            request = new RestRequest($"api/somiod/{app_name}", Method.Post);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");

            request.AddObject(cont);
            response = client.Execute(request);

            xmlContent = response.Content;
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("default", "http://schemas.datacontract.org/2004/07/Somiod.Models");

            nameNode = xmlDoc.SelectSingleNode("/default:Container/default:name", namespaceManager);

            string cont_name = nameNode?.InnerText;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show($"Light Bulb container created with name: {cont_name}");
            }
            else
            {
                MessageBox.Show($"Unable to create light bulb container: {response.StatusDescription}");
            }

            Models.Notification notif = new Models.Notification
            {
                id = 0,
                name = "light_bulb",
                creation_datetime = DateTime.Now,
                parent = 0,
                @event = 1,
                endpoint = "lolipop", //TODO
                enabled = 1,
                res_type = "notificaton"
            };

            request = new RestRequest($"api/somiod/{app_name}/{cont_name}/notif", Method.Post);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");

            request.AddObject(notif);
            response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show($"Light bulb notification created!");
            }
            else
            {
                MessageBox.Show($"Unable to create light bulb notification: {response.StatusDescription}");
            }

            string variableApp = "App: "+app_name;
            string variableCont = "Container: "+cont_name;
            label_app.Text = variableApp;
            label_cont.Text = variableCont;


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
