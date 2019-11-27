using System;
using System.Timers;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace FilesSaver
{
    public partial class Form1 : Form
    {
        const int _duration = 59000;
        string _sourcePath = "";
        string _destinationPath = "";
        static System.Timers.Timer _timer;
        static bool _timerSet;
        static bool _timerActive;
        static DateTime _scheduledTime;
        long directorySize = 0;

        /// *************************************  Status Bar Start code ***************************************************

        public Form1()
        {
            InitializeComponent();
            _timerSet = false;
            CreateTimer();
            ChangeUI(false);
        }

        ~Form1()
        {
            DisposeTimer();
        }

        private void ChangeUI(bool docopy)
        {
            label1.Visible = docopy;
            ProgressBar.Visible = docopy;
            CopyCancel.Text = docopy ? "Cancel" : "Copy";
            label1.Text = "Starting copy...";
            ProgressBar.Value = 0;
        }

        private void CopyCancelBtn_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            // Start the BackgroundWorker.
            backgroundWorker1.RunWorkerAsync();
            // Initialize picker to yesterday.
            DateTime result = DateTime.Now;
            DateTimePicker.Format = DateTimePickerFormat.Time;
            DateTimePicker.ShowUpDown = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                // Wait 100 milliseconds.
                Thread.Sleep(100);
                // Report progress.
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            ProgressBar.Value = e.ProgressPercentage;
            // Set the text.
            this.Text = e.ProgressPercentage.ToString();
        }

        /// *************************************  Status Bar End code ***************************************************

        private void TimeChecker_Event(Object source, ElapsedEventArgs e)
        {
            if (_timerSet && _timerActive)
            {
                if (_scheduledTime.Hour == DateTime.Now.Hour && _scheduledTime.Minute == DateTime.Now.Minute && ActivatedBox.Checked)
                {
                    CopyCancelBtn_Click(this, EventArgs.Empty);
                }
            }
        }

        private void BrowseSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK && ValidateDirectory(fbd.SelectedPath))
            {
                _sourcePath = fbd.SelectedPath;
                SourceText.Text = _sourcePath;
                Source_label.ForeColor = Color.LightGreen;
            }
            else
            {
                _sourcePath = "";
                SourceText.Text = "Path not found";
                Source_label.ForeColor = Color.Red;
            }

            fbd.Dispose();
        }

        private void BrowseDestination_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK && ValidateDirectory(fbd.SelectedPath))
            {
                _destinationPath = fbd.SelectedPath;
                DestinationText.Text = _destinationPath;
                Destination_label.ForeColor = Color.LightGreen;
            }
            else
            {
                _destinationPath = "";
                Destination.Text = "Path not found";
                Destination_label.ForeColor = Color.Red;
            }

            fbd.Dispose();
        }

        private void Copy()
        {
            var diSource = new DirectoryInfo(_sourcePath);
            var diTarget = new DirectoryInfo(_destinationPath);
            FileSize(diSource);

            CopyAll(diSource, diTarget);
        }

        private void FileSize(DirectoryInfo source)
        {
            directorySize = 1;
            // Check file size and access of files
            foreach (var dir in source.EnumerateDirectories())
            {
                try
                {
                    foreach (var file in dir.EnumerateFiles("*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            directorySize += file.Length;
                            Console.WriteLine("File size is: " + file.Length);
                        }
                        catch (UnauthorizedAccessException unAuthFile)
                        {
                            MessageBox.Show($"unAuthFile: {unAuthFile.Message}");
                        }
                    }
                }
                catch (UnauthorizedAccessException unAuthSubDir)
                {
                    MessageBox.Show($"unAuthSubDir: {unAuthSubDir.Message}");
                }
            }
        }

        public void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            long currBytes = 1;
            
            Directory.CreateDirectory(target.FullName);
            // Copy each file into the new directory.
            foreach (FileInfo file in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, file.Name);
                try
                {
                    file.CopyTo(Path.Combine(target.FullName, file.Name), false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                currBytes += file.Length;
                ProgressBar.Value = (int)((currBytes % directorySize));
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void BtnSetTime_Click(object sender, EventArgs e)
        {
            _scheduledTime = DateTimePicker.Value;
            ToggleTimerLabel(false);
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ToggleTimerLabel(true);
            ToggleActivatedLabel(false);
        }

        /// <summary>
        /// Changes state of the labels indicating whether the timer is set
        /// </summary>
        /// <param name="tog"> If true then turn label red, false green </param>
        private void ToggleTimerLabel(bool tog)
        {
            if (tog)
            {
                TimerSet_label.Text = "Timer Not set";
                TimerSet_label.ForeColor = Color.Red;
                _timerSet = false;
            }
            else
            {
                TimerSet_label.Text = "Timer set";
                TimerSet_label.ForeColor = Color.LightGreen;
                _timerSet = true;
            }
        }

        private void ToggleActivatedLabel(bool tog)
        {
            if (tog)
            {
                ActivatedBox.Text = "Activated";
                ActivatedBox.ForeColor = Color.LightGreen;
                ActivatedBox.Checked = true;
                _timerActive = true;
            }
            else
            {
                ActivatedBox.Text = "Not Activated";
                ActivatedBox.ForeColor = Color.Red;
                ActivatedBox.Checked = false;
                _timerActive = false;
            }
        }

        private void DisposeTimer()
        {
            if (_timer != null)
            {
                _timer.Elapsed -= new ElapsedEventHandler(TimeChecker_Event);
                _timer.Dispose();
            }
        }

        private void CreateTimer()
        {
            if (_timer != null)
            {
                DisposeTimer();
            }

            _timer = new System.Timers.Timer(_duration);
            _scheduledTime = DateTime.Now;
            _timer.Elapsed += TimeChecker_Event;

            // Have the timer go off only once (false), or repeat (true)
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        private bool ValidateDirectory(string path)
        {
            if (path != "")
            {
                return Directory.Exists(path);
            }
            return false;
        }

        private void ActivatedBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!ActivatedBox.Checked)
            {
                _timer.Enabled = false;
                CopyCancel.Enabled = false;
                ToggleActivatedLabel(false);
                return;
            }

            if (ValidateDirectory(_sourcePath) && ValidateDirectory(_destinationPath) && _timerSet)
            {
                _timerActive = ActivatedBox.Checked;
                ToggleActivatedLabel(true);
                Source_label.ForeColor = Color.LightGreen;
                Destination_label.ForeColor = Color.LightGreen;
                CopyCancel.Enabled = true;
                _timer.Enabled = true;
            }
            else if (!ValidateDirectory(_sourcePath))
            {
                Source_label.ForeColor = Color.Red;
                ActivatedBox.Checked = false;
                MessageBox.Show("Error: Source path not valid.");
            }
            else if (!ValidateDirectory(_destinationPath))
            {
                Destination_label.ForeColor = Color.Red;
                ActivatedBox.Checked = false;
                MessageBox.Show("Error: Destination path not valid.");
            }
            else
            {
                MessageBox.Show("Error: Timer is NOT set.");
                ActivatedBox.Checked = false;
            }
        }
    }
}
