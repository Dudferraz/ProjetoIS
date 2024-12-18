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
            this.textBox_app = new System.Windows.Forms.TextBox();
            this.textBox_cont = new System.Windows.Forms.TextBox();
            this.button_submit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_ON
            // 
            this.button_ON.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ON.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ON.Location = new System.Drawing.Point(48, 115);
            this.button_ON.Name = "button_ON";
            this.button_ON.Size = new System.Drawing.Size(259, 137);
            this.button_ON.TabIndex = 0;
            this.button_ON.Text = "ON";
            this.button_ON.UseVisualStyleBackColor = true;
            this.button_ON.Click += new System.EventHandler(this.button_ON_Click);
            // 
            // button_OFF
            // 
            this.button_OFF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OFF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_OFF.Location = new System.Drawing.Point(48, 284);
            this.button_OFF.Name = "button_OFF";
            this.button_OFF.Size = new System.Drawing.Size(261, 131);
            this.button_OFF.TabIndex = 1;
            this.button_OFF.Text = "OFF";
            this.button_OFF.UseVisualStyleBackColor = true;
            this.button_OFF.Click += new System.EventHandler(this.button_OFF_Click);
            // 
            // textBox_app
            // 
            this.textBox_app.Location = new System.Drawing.Point(122, 29);
            this.textBox_app.Name = "textBox_app";
            this.textBox_app.Size = new System.Drawing.Size(138, 22);
            this.textBox_app.TabIndex = 2;
            this.textBox_app.Text = "Lighting";
            // 
            // textBox_cont
            // 
            this.textBox_cont.Location = new System.Drawing.Point(122, 58);
            this.textBox_cont.Name = "textBox_cont";
            this.textBox_cont.Size = new System.Drawing.Size(138, 22);
            this.textBox_cont.TabIndex = 3;
            this.textBox_cont.Text = "light_bulb";
            // 
            // button_submit
            // 
            this.button_submit.Location = new System.Drawing.Point(266, 35);
            this.button_submit.Name = "button_submit";
            this.button_submit.Size = new System.Drawing.Size(71, 34);
            this.button_submit.TabIndex = 4;
            this.button_submit.Text = "submit";
            this.button_submit.UseVisualStyleBackColor = true;
            this.button_submit.Click += new System.EventHandler(this.button_submit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "App name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Container name:";
            // 
            // Form_Switch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 468);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_submit);
            this.Controls.Add(this.textBox_cont);
            this.Controls.Add(this.textBox_app);
            this.Controls.Add(this.button_OFF);
            this.Controls.Add(this.button_ON);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Switch";
            this.Text = "Switch";
            this.Load += new System.EventHandler(this.Form_Switch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ON;
        private System.Windows.Forms.Button button_OFF;
        private System.Windows.Forms.TextBox textBox_app;
        private System.Windows.Forms.TextBox textBox_cont;
        private System.Windows.Forms.Button button_submit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

