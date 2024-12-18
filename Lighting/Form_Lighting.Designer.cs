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
            this.label_cont = new System.Windows.Forms.Label();
            this.label_app = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLightBulb)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLightBulb
            // 
            this.pictureBoxLightBulb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLightBulb.Image = global::Lighting.Properties.Resources.unpowered_light_bulb_wide;
            this.pictureBoxLightBulb.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLightBulb.Name = "pictureBoxLightBulb";
            this.pictureBoxLightBulb.Size = new System.Drawing.Size(384, 439);
            this.pictureBoxLightBulb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLightBulb.TabIndex = 0;
            this.pictureBoxLightBulb.TabStop = false;
            this.pictureBoxLightBulb.Click += new System.EventHandler(this.pictureBoxLightBulb_Click);
            // 
            // label_cont
            // 
            this.label_cont.AutoSize = true;
            this.label_cont.BackColor = System.Drawing.Color.Silver;
            this.label_cont.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_cont.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_cont.Location = new System.Drawing.Point(0, 421);
            this.label_cont.Name = "label_cont";
            this.label_cont.Size = new System.Drawing.Size(141, 18);
            this.label_cont.TabIndex = 1;
            this.label_cont.Text = "Container: light_bulb";
            // 
            // label_app
            // 
            this.label_app.AutoSize = true;
            this.label_app.BackColor = System.Drawing.Color.Silver;
            this.label_app.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_app.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_app.Location = new System.Drawing.Point(0, 403);
            this.label_app.Name = "label_app";
            this.label_app.Size = new System.Drawing.Size(91, 18);
            this.label_app.TabIndex = 2;
            this.label_app.Text = "App: Lighting";
            // 
            // Form_Lighting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(384, 439);
            this.Controls.Add(this.label_app);
            this.Controls.Add(this.label_cont);
            this.Controls.Add(this.pictureBoxLightBulb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Lighting";
            this.Text = "Lighting";
            this.Load += new System.EventHandler(this.Form_Lighting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLightBulb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLightBulb;
        private System.Windows.Forms.Label label_cont;
        private System.Windows.Forms.Label label_app;
    }
}

