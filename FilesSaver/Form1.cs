using System;
using System.Timers;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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

        public Form1()
        {
            InitializeComponent();
            _timerSet = false;
            CreateTimer();
        }

        ~Form1()
        {
            DisposeTimer();
        }

        private void TimeChecker_Event(Object source, ElapsedEventArgs e)
        {
            if (_timerSet && _timerActive)
            {
                if (_scheduledTime.Hour == DateTime.Now.Hour && _scheduledTime.Minute == DateTime.Now.Minute)
                {
                    Copy(_sourcePath, _destinationPath);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize picker to yesterday.
            DateTime result = DateTime.Now;
            DateTimePicker.Format = DateTimePickerFormat.Time;
            DateTimePicker.ShowUpDown = true;
        }

        private void BrowseSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK && ValidateDirectory(fbd.SelectedPath))
            {
                _sourcePath = fbd.SelectedPath;
                SourceText.Text = _sourcePath;
                Source_label.ForeColor = Color.Green;
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
                Destination_label.ForeColor = Color.Green;
            }
            else
            {
                _destinationPath = "";
                Destination.Text = "Path not found";
                Destination_label.ForeColor = Color.Red;
            }

            fbd.Dispose();
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), false);
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
                TimerSet_label.ForeColor = Color.Green;
                _timerSet = true;
            }
        }

        private void ToggleActivateLabel(bool tog)
        {
            if (tog)
            {
                ActivatedBox.Text = "Activated";
                ActivatedBox.ForeColor = Color.Green;
                _timerActive = true;
            }
            else
            {
                ActivatedBox.Text = "Not Activated";
                ActivatedBox.ForeColor = Color.Red;
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
                ToggleActivateLabel(false);
                return;
            }

            if (ValidateDirectory(_sourcePath) && ValidateDirectory(_destinationPath) && _timerSet)
            {
                _timerActive = ActivatedBox.Checked;
                ToggleActivateLabel(true);
                Source_label.ForeColor = Color.Green;
                Destination_label.ForeColor = Color.Green;
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
