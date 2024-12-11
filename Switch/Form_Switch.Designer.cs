namespace Switch
{
    partial class Form_Switch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Switch));
            this.button_ON = new System.Windows.Forms.Button();
            this.button_OFF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_ON
            // 
            this.button_ON.Location = new System.Drawing.Point(48, 74);
            this.button_ON.Name = "button_ON";
            this.button_ON.Size = new System.Drawing.Size(240, 137);
            this.button_ON.TabIndex = 0;
            this.button_ON.Text = "ON";
            this.button_ON.UseVisualStyleBackColor = true;
            this.button_ON.Click += new System.EventHandler(this.button_ON_Click);
            // 
            // button_OFF
            // 
            this.button_OFF.Location = new System.Drawing.Point(48, 245);
            this.button_OFF.Name = "button_OFF";
            this.button_OFF.Size = new System.Drawing.Size(240, 137);
            this.button_OFF.TabIndex = 1;
            this.button_OFF.Text = "OFF";
            this.button_OFF.UseVisualStyleBackColor = true;
            this.button_OFF.Click += new System.EventHandler(this.button_OFF_Click);
            // 
            // Form_Switch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 450);
            this.Controls.Add(this.button_OFF);
            this.Controls.Add(this.button_ON);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Switch";
            this.Text = "Switch";
            this.Load += new System.EventHandler(this.Form_Switch_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_ON;
        private System.Windows.Forms.Button button_OFF;
    }
}

