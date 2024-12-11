using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Internal;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;
using System.Windows.Forms;
using RestSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Switch.Models;
using System.Xml;

namespace Switch
{
    public partial class Form_Switch : Form
    {
        string baseURI = @"http://localhost:58225/"; //TODO: needs to be updated!

        RestClient client = null;
        public Form_Switch()
        {
            InitializeComponent();
            client = new RestClient(baseURI);
        }
        private void Form_Switch_Load(object sender, EventArgs e)
        {
            Models.Application app = new Models.Application
            {
                id=0,
                name = "Switch",
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
                MessageBox.Show($"Switch App created with name: {app_name}");
            }
            else
            {
                MessageBox.Show($"Unable to create switch app: {response.StatusDescription}");
            }

        }
        private void button_ON_Click(object sender, EventArgs e)
        {
            Record record = new Record
            {
                id=0,
                name = "On",
                content = "on",
                res_type = "record"
            };

            RestRequest request = new RestRequest("api/somiod/lighting/light_bulb/record", Method.Post);
            request.RequestFormat = DataFormat.Xml;

            request.AddObject(record);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("On record created!");
            }
            else
            {
                MessageBox.Show($"Unable to create on record: {response.StatusDescription}");
            }
        }

        private void button_OFF_Click(object sender, EventArgs e)
        {
            Record record = new Record
            {
                id = 0,
                name = "Off",
                content = "off",
                res_type = "record"
            };

            RestRequest request = new RestRequest("api/somiod/lighting/light_bulb/record", Method.Post);
            request.RequestFormat = DataFormat.Xml;

            request.AddObject(record);
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Off record created!");
            }
            else
            {
                MessageBox.Show($"Unable to create off record: {response.StatusDescription}");
            }
        }


    }
}

