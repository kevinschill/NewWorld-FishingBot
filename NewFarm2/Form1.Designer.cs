namespace NewFarm2
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
            this.startBotButton = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.castPowerLabel = new System.Windows.Forms.Label();
            this.castPowerCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stopBotButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.totalCastCounter_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.repair_CheckBox = new System.Windows.Forms.CheckBox();
            this.repairSetupButton = new System.Windows.Forms.Button();
            this.repairAmount_label = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.outofbait_label = new System.Windows.Forms.Label();
            this.baitSetupButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.antiAfkCheckBox = new System.Windows.Forms.CheckBox();
            this.keyButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // startBotButton
            // 
            this.startBotButton.Location = new System.Drawing.Point(182, 31);
            this.startBotButton.Name = "startBotButton";
            this.startBotButton.Size = new System.Drawing.Size(75, 23);
            this.startBotButton.TabIndex = 2;
            this.startBotButton.Text = "Start";
            this.startBotButton.UseVisualStyleBackColor = true;
            this.startBotButton.Click += new System.EventHandler(this.startBotButton_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 19);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(314, 45);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // castPowerLabel
            // 
            this.castPowerLabel.AutoSize = true;
            this.castPowerLabel.Location = new System.Drawing.Point(18, 67);
            this.castPowerLabel.Name = "castPowerLabel";
            this.castPowerLabel.Size = new System.Drawing.Size(57, 13);
            this.castPowerLabel.TabIndex = 4;
            this.castPowerLabel.Text = "Power: 0%";
            // 
            // castPowerCheckBox
            // 
            this.castPowerCheckBox.AutoSize = true;
            this.castPowerCheckBox.Location = new System.Drawing.Point(185, 66);
            this.castPowerCheckBox.Name = "castPowerCheckBox";
            this.castPowerCheckBox.Size = new System.Drawing.Size(135, 17);
            this.castPowerCheckBox.TabIndex = 5;
            this.castPowerCheckBox.Text = "Random Cast Distance";
            this.castPowerCheckBox.UseVisualStyleBackColor = true;
            this.castPowerCheckBox.CheckedChanged += new System.EventHandler(this.castPowerCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.castPowerCheckBox);
            this.groupBox1.Controls.Add(this.castPowerLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 88);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Casting Settings";
            // 
            // stopBotButton
            // 
            this.stopBotButton.Location = new System.Drawing.Point(263, 31);
            this.stopBotButton.Name = "stopBotButton";
            this.stopBotButton.Size = new System.Drawing.Size(75, 23);
            this.stopBotButton.TabIndex = 7;
            this.stopBotButton.Text = "Stop";
            this.stopBotButton.UseVisualStyleBackColor = true;
            this.stopBotButton.Click += new System.EventHandler(this.stopBotButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.totalCastCounter_label);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 42);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stats";
            // 
            // totalCastCounter_label
            // 
            this.totalCastCounter_label.AutoSize = true;
            this.totalCastCounter_label.Location = new System.Drawing.Point(6, 16);
            this.totalCastCounter_label.Name = "totalCastCounter_label";
            this.totalCastCounter_label.Size = new System.Drawing.Size(72, 13);
            this.totalCastCounter_label.TabIndex = 0;
            this.totalCastCounter_label.Text = "Total Casts: 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(186, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Hotkey: F8";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Hotkey: F9";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.repair_CheckBox);
            this.groupBox3.Controls.Add(this.repairSetupButton);
            this.groupBox3.Controls.Add(this.repairAmount_label);
            this.groupBox3.Controls.Add(this.trackBar2);
            this.groupBox3.Location = new System.Drawing.Point(12, 65);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(164, 96);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Repair Settings";
            // 
            // repair_CheckBox
            // 
            this.repair_CheckBox.AutoSize = true;
            this.repair_CheckBox.Location = new System.Drawing.Point(9, 56);
            this.repair_CheckBox.Name = "repair_CheckBox";
            this.repair_CheckBox.Size = new System.Drawing.Size(57, 17);
            this.repair_CheckBox.TabIndex = 3;
            this.repair_CheckBox.Text = "Repair";
            this.repair_CheckBox.UseVisualStyleBackColor = true;
            this.repair_CheckBox.CheckedChanged += new System.EventHandler(this.repair_CheckBox_CheckedChanged);
            // 
            // repairSetupButton
            // 
            this.repairSetupButton.Location = new System.Drawing.Point(98, 69);
            this.repairSetupButton.Name = "repairSetupButton";
            this.repairSetupButton.Size = new System.Drawing.Size(60, 21);
            this.repairSetupButton.TabIndex = 2;
            this.repairSetupButton.Text = "Setup";
            this.repairSetupButton.UseVisualStyleBackColor = true;
            this.repairSetupButton.Click += new System.EventHandler(this.repairSetupButton_Click);
            // 
            // repairAmount_label
            // 
            this.repairAmount_label.AutoSize = true;
            this.repairAmount_label.Location = new System.Drawing.Point(6, 73);
            this.repairAmount_label.Name = "repairAmount_label";
            this.repairAmount_label.Size = new System.Drawing.Size(55, 13);
            this.repairAmount_label.TabIndex = 1;
            this.repairAmount_label.Text = "Amount: 0";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(9, 19);
            this.trackBar2.Maximum = 40;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(149, 45);
            this.trackBar2.TabIndex = 0;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.outofbait_label);
            this.groupBox4.Controls.Add(this.baitSetupButton);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Location = new System.Drawing.Point(182, 107);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(156, 54);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bait Settings";
            // 
            // outofbait_label
            // 
            this.outofbait_label.AutoSize = true;
            this.outofbait_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outofbait_label.ForeColor = System.Drawing.Color.Red;
            this.outofbait_label.Location = new System.Drawing.Point(4, 14);
            this.outofbait_label.Name = "outofbait_label";
            this.outofbait_label.Size = new System.Drawing.Size(90, 15);
            this.outofbait_label.TabIndex = 2;
            this.outofbait_label.Text = "Out of Bait....";
            this.outofbait_label.Visible = false;
            // 
            // baitSetupButton
            // 
            this.baitSetupButton.Location = new System.Drawing.Point(88, 27);
            this.baitSetupButton.Name = "baitSetupButton";
            this.baitSetupButton.Size = new System.Drawing.Size(60, 21);
            this.baitSetupButton.TabIndex = 1;
            this.baitSetupButton.Text = "Setup";
            this.baitSetupButton.UseVisualStyleBackColor = true;
            this.baitSetupButton.Click += new System.EventHandler(this.baitSetupButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 31);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Use Bait";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // antiAfkCheckBox
            // 
            this.antiAfkCheckBox.AutoSize = true;
            this.antiAfkCheckBox.Location = new System.Drawing.Point(182, 64);
            this.antiAfkCheckBox.Name = "antiAfkCheckBox";
            this.antiAfkCheckBox.Size = new System.Drawing.Size(63, 17);
            this.antiAfkCheckBox.TabIndex = 13;
            this.antiAfkCheckBox.Text = "Anti Afk";
            this.antiAfkCheckBox.UseVisualStyleBackColor = true;
            this.antiAfkCheckBox.CheckedChanged += new System.EventHandler(this.antiAfkCheckBox_CheckedChanged);
            // 
            // keyButton
            // 
            this.keyButton.Location = new System.Drawing.Point(263, 60);
            this.keyButton.Name = "keyButton";
            this.keyButton.Size = new System.Drawing.Size(75, 23);
            this.keyButton.TabIndex = 14;
            this.keyButton.Text = "Key";
            this.keyButton.UseVisualStyleBackColor = true;
            this.keyButton.Click += new System.EventHandler(this.keyButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Win+Full 1920x1080",
            "Windowed 19201080 4K",
            "Windowed 1280x720 FullHD"});
            this.comboBox1.Location = new System.Drawing.Point(182, 84);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(156, 21);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 262);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.keyButton);
            this.Controls.Add(this.antiAfkCheckBox);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.stopBotButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.startBotButton);
            this.Name = "Form1";
            this.Text = "Hi.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button startBotButton;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label castPowerLabel;
        private System.Windows.Forms.CheckBox castPowerCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button stopBotButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label totalCastCounter_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label repairAmount_label;
        private System.Windows.Forms.Button repairSetupButton;
        private System.Windows.Forms.CheckBox repair_CheckBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button baitSetupButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox antiAfkCheckBox;
        private System.Windows.Forms.Label outofbait_label;
        private System.Windows.Forms.Button keyButton;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

