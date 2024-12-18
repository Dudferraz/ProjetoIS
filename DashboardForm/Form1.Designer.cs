namespace DashboardForm
{
    partial class Form1
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
            this.btnGet = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnPut = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.rtbShow = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.rtbContainer = new System.Windows.Forms.RichTextBox();
            this.rtbRecord = new System.Windows.Forms.RichTextBox();
            this.rtbNotification = new System.Windows.Forms.RichTextBox();
            this.comboBoxHeaders = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rtbContent = new System.Windows.Forms.RichTextBox();
            this.comboBoxNotf = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnGetAllApps = new System.Windows.Forms.Button();
            this.comboBoxEvent = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.rtbParentID = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(33, 25);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(109, 41);
            this.btnGet.TabIndex = 0;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(33, 84);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(109, 41);
            this.btnPost.TabIndex = 1;
            this.btnPost.Text = "Post";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(33, 148);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(109, 41);
            this.btnPut.TabIndex = 2;
            this.btnPut.Text = "Put";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(33, 210);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(109, 41);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // rtbShow
            // 
            this.rtbShow.Location = new System.Drawing.Point(33, 298);
            this.rtbShow.Name = "rtbShow";
            this.rtbShow.Size = new System.Drawing.Size(634, 257);
            this.rtbShow.TabIndex = 4;
            this.rtbShow.Text = "";
            this.rtbShow.TextChanged += new System.EventHandler(this.rtbShow_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(307, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Application Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(362, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Container:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(379, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Record:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(352, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Notification:";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(451, 25);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(217, 41);
            this.richTextBox2.TabIndex = 9;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // rtbContainer
            // 
            this.rtbContainer.Location = new System.Drawing.Point(450, 84);
            this.rtbContainer.Name = "rtbContainer";
            this.rtbContainer.Size = new System.Drawing.Size(217, 41);
            this.rtbContainer.TabIndex = 10;
            this.rtbContainer.Text = "";
            this.rtbContainer.TextChanged += new System.EventHandler(this.rtbContainer_TextChanged);
            // 
            // rtbRecord
            // 
            this.rtbRecord.Location = new System.Drawing.Point(450, 148);
            this.rtbRecord.Name = "rtbRecord";
            this.rtbRecord.Size = new System.Drawing.Size(217, 41);
            this.rtbRecord.TabIndex = 11;
            this.rtbRecord.Text = "";
            this.rtbRecord.TextChanged += new System.EventHandler(this.rtbRecord_TextChanged);
            // 
            // rtbNotification
            // 
            this.rtbNotification.Location = new System.Drawing.Point(450, 212);
            this.rtbNotification.Name = "rtbNotification";
            this.rtbNotification.Size = new System.Drawing.Size(217, 39);
            this.rtbNotification.TabIndex = 12;
            this.rtbNotification.Text = "";
            this.rtbNotification.TextChanged += new System.EventHandler(this.rtbNotification_TextChanged);
            // 
            // comboBoxHeaders
            // 
            this.comboBoxHeaders.FormattingEnabled = true;
            this.comboBoxHeaders.Items.AddRange(new object[] {
            "Application",
            "Container",
            "Record",
            "Notification"});
            this.comboBoxHeaders.Location = new System.Drawing.Point(780, 48);
            this.comboBoxHeaders.Name = "comboBoxHeaders";
            this.comboBoxHeaders.Size = new System.Drawing.Size(180, 21);
            this.comboBoxHeaders.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(776, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "SOMIOD header options";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(798, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Content to send";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // rtbContent
            // 
            this.rtbContent.Location = new System.Drawing.Point(737, 325);
            this.rtbContent.Name = "rtbContent";
            this.rtbContent.Size = new System.Drawing.Size(253, 73);
            this.rtbContent.TabIndex = 16;
            this.rtbContent.Text = "";
            this.rtbContent.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // comboBoxNotf
            // 
            this.comboBoxNotf.FormattingEnabled = true;
            this.comboBoxNotf.Items.AddRange(new object[] {
            "mqtt",
            "http"});
            this.comboBoxNotf.Location = new System.Drawing.Point(800, 457);
            this.comboBoxNotf.Name = "comboBoxNotf";
            this.comboBoxNotf.Size = new System.Drawing.Size(121, 21);
            this.comboBoxNotf.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(803, 441);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "Choose mqtt or http";
            // 
            // btnGetAllApps
            // 
            this.btnGetAllApps.Location = new System.Drawing.Point(49, -4);
            this.btnGetAllApps.Name = "btnGetAllApps";
            this.btnGetAllApps.Size = new System.Drawing.Size(75, 23);
            this.btnGetAllApps.TabIndex = 19;
            this.btnGetAllApps.Text = "GetAllApps";
            this.btnGetAllApps.UseVisualStyleBackColor = true;
            this.btnGetAllApps.Click += new System.EventHandler(this.btnGetAllApps_Click);
            // 
            // comboBoxEvent
            // 
            this.comboBoxEvent.FormattingEnabled = true;
            this.comboBoxEvent.Items.AddRange(new object[] {
            "creation",
            "deletion"});
            this.comboBoxEvent.Location = new System.Drawing.Point(800, 513);
            this.comboBoxEvent.Name = "comboBoxEvent";
            this.comboBoxEvent.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEvent.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(766, 495);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(215, 15);
            this.label8.TabIndex = 21;
            this.label8.Text = "Choose Creation or Deletion for event*";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Perpetua Titling MT", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(723, 413);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(277, 28);
            this.label9.TabIndex = 22;
            this.label9.Text = "POST SECTION BELOW";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Perpetua Titling MT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(31, 273);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(413, 22);
            this.label10.TabIndex = 23;
            this.label10.Text = "Results and erros will be shown here";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(748, 542);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(233, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "* be notified when a record is created or deleted";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Perpetua Titling MT", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(797, 267);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 28);
            this.label11.TabIndex = 26;
            this.label11.Text = "POST/PUT ";
            // 
            // rtbParentID
            // 
            this.rtbParentID.Location = new System.Drawing.Point(684, 357);
            this.rtbParentID.Name = "rtbParentID";
            this.rtbParentID.Size = new System.Drawing.Size(47, 22);
            this.rtbParentID.TabIndex = 27;
            this.rtbParentID.Text = "";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(681, 341);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Parent ID";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 601);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.rtbParentID);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxEvent);
            this.Controls.Add(this.btnGetAllApps);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBoxNotf);
            this.Controls.Add(this.rtbContent);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxHeaders);
            this.Controls.Add(this.rtbNotification);
            this.Controls.Add(this.rtbRecord);
            this.Controls.Add(this.rtbContainer);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbShow);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPut);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.btnGet);
            this.Name = "Form1";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnPut;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.RichTextBox rtbShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox rtbContainer;
        private System.Windows.Forms.RichTextBox rtbRecord;
        private System.Windows.Forms.RichTextBox rtbNotification;
        private System.Windows.Forms.ComboBox comboBoxHeaders;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rtbContent;
        private System.Windows.Forms.ComboBox comboBoxNotf;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGetAllApps;
        private System.Windows.Forms.ComboBox comboBoxEvent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox rtbParentID;
        private System.Windows.Forms.Label label13;
    }
}

