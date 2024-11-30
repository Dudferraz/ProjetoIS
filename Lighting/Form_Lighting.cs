using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lighting
{
    public partial class Form_Lighting : Form
    {
        private bool isLightOn = false; 

        public Form_Lighting()
        {
            InitializeComponent();
            pictureBoxLightBulb.Image = Properties.Resources.unpowered_light_bulb_wide;
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
