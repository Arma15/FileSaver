﻿namespace FilesSaver
{
    partial class MainUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.Source = new System.Windows.Forms.Button();
            this.Destination = new System.Windows.Forms.Button();
            this.ActivatedBox = new System.Windows.Forms.CheckBox();
            this.Source_label = new System.Windows.Forms.Label();
            this.Destination_label = new System.Windows.Forms.Label();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SourceText = new System.Windows.Forms.TextBox();
            this.DestinationText = new System.Windows.Forms.TextBox();
            this.TimerSet_label = new System.Windows.Forms.Label();
            this.SetTime_button = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.CopyCancel = new System.Windows.Forms.Button();
            this.Feedback = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Source
            // 
            this.Source.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Source.Location = new System.Drawing.Point(80, 118);
            this.Source.Name = "Source";
            this.Source.Size = new System.Drawing.Size(128, 45);
            this.Source.TabIndex = 0;
            this.Source.Text = "Browse for Source Folder";
            this.Source.UseVisualStyleBackColor = true;
            this.Source.Click += new System.EventHandler(this.BtnBrowseSource_Click);
            // 
            // Destination
            // 
            this.Destination.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Destination.Location = new System.Drawing.Point(582, 118);
            this.Destination.Name = "Destination";
            this.Destination.Size = new System.Drawing.Size(133, 45);
            this.Destination.TabIndex = 2;
            this.Destination.Text = "Browse for Destination Folder";
            this.Destination.UseVisualStyleBackColor = true;
            this.Destination.Click += new System.EventHandler(this.BtnBrowseDestination_Click);
            // 
            // ActivatedBox
            // 
            this.ActivatedBox.AutoSize = true;
            this.ActivatedBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivatedBox.ForeColor = System.Drawing.Color.Red;
            this.ActivatedBox.Location = new System.Drawing.Point(335, 200);
            this.ActivatedBox.Name = "ActivatedBox";
            this.ActivatedBox.Size = new System.Drawing.Size(102, 19);
            this.ActivatedBox.TabIndex = 3;
            this.ActivatedBox.Text = "Not Activated";
            this.ActivatedBox.UseVisualStyleBackColor = true;
            this.ActivatedBox.CheckedChanged += new System.EventHandler(this.ActivatedBox_CheckedChanged);
            // 
            // Source_label
            // 
            this.Source_label.AutoSize = true;
            this.Source_label.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Source_label.ForeColor = System.Drawing.Color.Red;
            this.Source_label.Location = new System.Drawing.Point(95, 100);
            this.Source_label.Name = "Source_label";
            this.Source_label.Size = new System.Drawing.Size(90, 15);
            this.Source_label.TabIndex = 4;
            this.Source_label.Text = "Current Source";
            // 
            // Destination_label
            // 
            this.Destination_label.AutoSize = true;
            this.Destination_label.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Destination_label.ForeColor = System.Drawing.Color.Red;
            this.Destination_label.Location = new System.Drawing.Point(590, 100);
            this.Destination_label.Name = "Destination_label";
            this.Destination_label.Size = new System.Drawing.Size(116, 15);
            this.Destination_label.TabIndex = 6;
            this.Destination_label.Text = "Current Destination";
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.CalendarFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimePicker.CustomFormat = "hh:mm tt";
            this.DateTimePicker.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePicker.Location = new System.Drawing.Point(335, 111);
            this.DateTimePicker.MaximumSize = new System.Drawing.Size(120, 40);
            this.DateTimePicker.MinimumSize = new System.Drawing.Size(120, 30);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.ShowUpDown = true;
            this.DateTimePicker.Size = new System.Drawing.Size(120, 30);
            this.DateTimePicker.TabIndex = 7;
            this.DateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::FilesSaver.Properties.Resources.aboutLogo;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(166, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(402, 81);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // SourceText
            // 
            this.SourceText.AllowDrop = true;
            this.SourceText.Location = new System.Drawing.Point(12, 169);
            this.SourceText.Multiline = true;
            this.SourceText.Name = "SourceText";
            this.SourceText.ReadOnly = true;
            this.SourceText.Size = new System.Drawing.Size(285, 50);
            this.SourceText.TabIndex = 9;
            this.SourceText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DestinationText
            // 
            this.DestinationText.AllowDrop = true;
            this.DestinationText.Location = new System.Drawing.Point(523, 169);
            this.DestinationText.Multiline = true;
            this.DestinationText.Name = "DestinationText";
            this.DestinationText.ReadOnly = true;
            this.DestinationText.Size = new System.Drawing.Size(285, 50);
            this.DestinationText.TabIndex = 10;
            this.DestinationText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TimerSet_label
            // 
            this.TimerSet_label.AutoSize = true;
            this.TimerSet_label.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerSet_label.ForeColor = System.Drawing.Color.Red;
            this.TimerSet_label.Location = new System.Drawing.Point(344, 93);
            this.TimerSet_label.Name = "TimerSet_label";
            this.TimerSet_label.Size = new System.Drawing.Size(83, 15);
            this.TimerSet_label.TabIndex = 11;
            this.TimerSet_label.Text = "Timer Not set";
            // 
            // SetTime_button
            // 
            this.SetTime_button.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetTime_button.Location = new System.Drawing.Point(355, 147);
            this.SetTime_button.Name = "SetTime_button";
            this.SetTime_button.Size = new System.Drawing.Size(75, 23);
            this.SetTime_button.TabIndex = 12;
            this.SetTime_button.Text = "Set Time";
            this.SetTime_button.UseVisualStyleBackColor = true;
            this.SetTime_button.Click += new System.EventHandler(this.BtnSetTime_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ProgressBar.Location = new System.Drawing.Point(594, 41);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(214, 23);
            this.ProgressBar.Step = 1;
            this.ProgressBar.TabIndex = 13;
            // 
            // CopyCancel
            // 
            this.CopyCancel.Enabled = false;
            this.CopyCancel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyCancel.Location = new System.Drawing.Point(663, 12);
            this.CopyCancel.Name = "CopyCancel";
            this.CopyCancel.Size = new System.Drawing.Size(75, 23);
            this.CopyCancel.TabIndex = 14;
            this.CopyCancel.Text = "Copy Now";
            this.CopyCancel.UseVisualStyleBackColor = true;
            this.CopyCancel.Visible = false;
            this.CopyCancel.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // Feedback
            // 
            this.Feedback.AutoSize = true;
            this.Feedback.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Feedback.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Feedback.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Feedback.Location = new System.Drawing.Point(591, 67);
            this.Feedback.Name = "Feedback";
            this.Feedback.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Feedback.Size = new System.Drawing.Size(119, 15);
            this.Feedback.TabIndex = 15;
            this.Feedback.Text = "Last transfered: N/A";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DoWork_Event);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ProgressChanged_Event);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.RunWorkerCompleted_Event);
            // 
            // MainUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(818, 232);
            this.Controls.Add(this.Feedback);
            this.Controls.Add(this.CopyCancel);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.SetTime_button);
            this.Controls.Add(this.TimerSet_label);
            this.Controls.Add(this.DestinationText);
            this.Controls.Add(this.SourceText);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DateTimePicker);
            this.Controls.Add(this.Destination_label);
            this.Controls.Add(this.Source_label);
            this.Controls.Add(this.ActivatedBox);
            this.Controls.Add(this.Destination);
            this.Controls.Add(this.Source);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.Name = "MainUI";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Saver";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUI_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Source;
        private System.Windows.Forms.Button Destination;
        private System.Windows.Forms.CheckBox ActivatedBox;
        private System.Windows.Forms.Label Source_label;
        private System.Windows.Forms.Label Destination_label;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox SourceText;
        private System.Windows.Forms.TextBox DestinationText;
        private System.Windows.Forms.Label TimerSet_label;
        private System.Windows.Forms.Button SetTime_button;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Button CopyCancel;
        private System.Windows.Forms.Label Feedback;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

