namespace Lighting
{
    partial class Form_Lighting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Lighting));
            this.pictureBoxLightBulb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLightBulb)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLightBulb
            // 
            this.pictureBoxLightBulb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLightBulb.Image = global::Lighting.Properties.Resources.unpowered_light_bulb_wide;
            this.pictureBoxLightBulb.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLightBulb.Name = "pictureBoxLightBulb";
            this.pictureBoxLightBulb.Size = new System.Drawing.Size(495, 569);
            this.pictureBoxLightBulb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLightBulb.TabIndex = 0;
            this.pictureBoxLightBulb.TabStop = false;
            this.pictureBoxLightBulb.Click += new System.EventHandler(this.pictureBoxLightBulb_Click);
            // 
            // Form_Lighting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(495, 569);
            this.Controls.Add(this.pictureBoxLightBulb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Lighting";
            this.Text = "Lighting";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLightBulb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLightBulb;
    }
}

