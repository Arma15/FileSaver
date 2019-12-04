using System;
using System.Timers;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using log4net;
using System.Reflection;

namespace FilesSaver
{
    public partial class MainUI : Form
    {
        /******************************************************************************************
		 * Private Data Members and Fields
		******************************************************************************************/
        #region Private Data Members and Fields
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const int _duration = 5000;
        private static System.Timers.Timer _timer;
        private static bool _timerSet;
        private static bool _timerActive;
        private double _directorySize = 0;
        private double _currSize = 0;
        private bool _timerRan = false;

        /// <summary> The Source Path to transfer files from, stored from previous session </summary>
        private string SourcePath
        {
            get
            {
                return Properties.Settings.Default.SourcePath;
            }
            set
            {
                Properties.Settings.Default.SourcePath = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary> The Destination Path to transfer files to, stored from previous session </summary>
        private string DestinationPath
        {
            get
            {
                return Properties.Settings.Default.DestinationPath;
            }
            set
            {
                Properties.Settings.Default.DestinationPath = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary> The last time files were copied to the destination, stored from previous session </summary>
        private DateTime LastCopied
        {
            get
            {
                return Properties.Settings.Default.LastCopy;
            }
            set
            {
                Properties.Settings.Default.LastCopy = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary> The last time set by the user, stored from previous session </summary>
        private DateTime LastSetTime
        {
            get
            {
                return Properties.Settings.Default.LastTimeSet;
            }
            set
            {
                Properties.Settings.Default.LastTimeSet = value;
                Properties.Settings.Default.Save();
            }
        }
        #endregion

        /******************************************************************************************
		 * Constructor and Destructor
		******************************************************************************************/
        #region Constructor and Destructor
        public MainUI()
        {
            InitializeComponent();
            _timerSet = true;
            CreateTimer();
            ValidateSrc();
            ValidateDest();
            SourceText.Text = SourcePath;
            DestinationText.Text = DestinationPath;
            DateTimePicker.Value = LastSetTime;
            Feedback.Text = "Last transfered: "  + Properties.Settings.Default.LastTimeSet.ToString("hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            _log.Info("In MainUI constructor.");
        }
        
        ~MainUI()
        {
            DisposeTimer();
        }
        #endregion

        /******************************************************************************************
		 * Buttons and Events
		******************************************************************************************/
        #region Buttons and Events
        /// <summary> Button to instantly copy files, currently hidden from user </summary>
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            StartCopy();
        }

        /// <summary> Button to display file explorer so the user can select a source to copy from </summary>
        private void BtnBrowseSource_Click(object sender, EventArgs e)
        {
            var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            var dialogResult = folderDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                SourceText.Text = SourcePath = folderDialog.SelectedPath;
                Source_label.ForeColor = Color.LightGreen;
            }
        }

        /// <summary> Button to display file explorer so the user can select a destination to copy to </summary>
        private void BtnBrowseDestination_Click(object sender, EventArgs e)
        {
            var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            var dialogResult = folderDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                DestinationText.Text = DestinationPath = folderDialog.SelectedPath;
                Destination_label.ForeColor = Color.LightGreen;
            }
        }

        /// <summary> Button to set the time to transfer files </summary>
        private void BtnSetTime_Click(object sender, EventArgs e)
        {
            LastSetTime = DateTimePicker.Value;
            _log.Info("Timer Set to: " + LastCopied.ToString("hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            ToggleTimerLabel(true);
        }
        
        /// <summary> Displays a time for the user to modify when they would like to transfer files </summary>
        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ToggleTimerLabel(false);
            ToggleActivatedLabel(false);
        }

        /// <summary> Event fired when the current time matches the user selected time to transfer </summary>
        private void TimeChecker_Event(Object source, ElapsedEventArgs e)
        {
            if (_timerRan)
            {
                return;
            }

            if (_timerSet && _timerActive)
            {
                if (LastSetTime.Hour == DateTime.Now.Hour && LastSetTime.Minute == DateTime.Now.Minute && ActivatedBox.Checked)
                {
                    _timerRan = true;
                    StartCopy();
                    Thread.Sleep(60000);
                    _timerRan = false;
                }
            }
        }
        
        /// <summary> Runs a separate async thread to transfer files </summary>
        private void DoWork_Event(object sender, DoWorkEventArgs e)
        {
            _directorySize = 0;
            _currSize = 0;
            var diSource = new DirectoryInfo(SourcePath);
            var diTarget = new DirectoryInfo(SourcePath);

            DirectorySize(diSource);
            _log.Info($"DoWork activated, Directory size is: {_directorySize}");

            if (_directorySize <= 1)
            {
                MessageBox.Show("No Data to be tranferred..");
                _log.Warn("No Data in source file location.");
            }
            else
            {
                CopyAll(diSource, diTarget);
            }
        }

        /// <summary> Runs on main thread to update UI </summary>
        private void ProgressChanged_Event(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            this.Invoke(new Action(() =>
            {
                Feedback.Text = "Transferring....";
                ProgressBar.Value = e.ProgressPercentage;
            }));
        }

        /// <summary> Runs on main thread when the work has been completed </summary>
        private void RunWorkerCompleted_Event(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                LastCopied = DateTime.Now;
                Feedback.Text = "Last transfered: " + LastCopied.ToString("hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                Feedback.ForeColor = Color.LightGreen;
                CopyCancel.Text = "Copy Now";
                ProgressBar.Value = 0;
            }));
            _log.Info("File transfer is completed.");
        }

        /// <summary> Called when the user selects the checkbox to activate the timer </summary>
        private void ActivatedBox_CheckedChanged(object sender, EventArgs e)
        {
            RunValidation();
        }
        #endregion

        /******************************************************************************************
		 * Private Methods
		******************************************************************************************/
        #region Private Methods
        /// <summary> Initiates the copying sequence </summary>
        private void StartCopy()
        {
            _timer.Enabled = false;

            if (!RunValidation())
            {
                _log.Info("Validation failed in StartCopy().");
                return;
            }
            if (backgroundWorker1 != null && backgroundWorker1.IsBusy)
            {
                _log.Info("Tried to copy files when Background Worker is running/null.");
                return;
            }
            else if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        /// <summary> Copies all files from the source directory to target directory </summary>
        /// <param name="source"> Path of the source directory </param>
        /// <param name="target"> Path of the target directory </param>
        public void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            // Copy each file into the new directory.
            foreach (FileInfo file in source.GetFiles())
            {
                _log.Info($"Copying: {target.FullName}\\{file.Name}");
                try
                {
                    _currSize += file.Length;
                    file.CopyTo(Path.Combine(target.FullName, file.Name), false);
                    if (file.Length < 1)
                    {
                        continue;
                    }
                    double result = ((_currSize / _directorySize) * 100.0);
                    int percent = (int)result == 0 ? 1 : (int)result;
                    backgroundWorker1.ReportProgress(percent);
                }
                catch (Exception ex)
                {
                    _log.Error($"Exception: {ex.Message.ToString()}");
                }
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        /// <summary> Calculates the size of the directory </summary>
        /// <param name="source"> Path to the directory </param>
        private void DirectorySize(DirectoryInfo source)
        {
            _directorySize = 0;
            List<FileInfo> files = new List<FileInfo>();
            FileInfo[] folder = source.GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo file in folder)
            {
                if ((file.Attributes & FileAttributes.Directory) != 0) continue;
                files.Add(file);
                _directorySize += file.Length;
            }
        }

        /// <summary> Changes state of the labels indicating whether the timer is set </summary>
        /// <param name="tog"> True: Timer is set, False: Timer not set </param>
        private void ToggleTimerLabel(bool tog)
        {
            if (tog)
            {
                TimerSet_label.Text = "Timer set";
                TimerSet_label.ForeColor = Color.LightGreen;
                _timerSet = true;
            }
            else
            {
                TimerSet_label.Text = "Timer Not set";
                TimerSet_label.ForeColor = Color.Red;
                _timerSet = false;
            }
        }

        /// <summary> Changes state of the labels, indicating whether the application is activated </summary>
        /// <param name="tog"> True: Application is active, False: Application is not active </param>
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

        /// <summary> Validates whether the application has its required settings set to run </summary>
        /// <returns> True if application can run, False if not </returns>
        private bool RunValidation()
        {
            bool valid = false;
            if (!ActivatedBox.Checked)
            {
                _timer.Enabled = false;
                CopyCancel.Enabled = false;
                ToggleActivatedLabel(false);
                return false;
            }

            if (ValidateSrc() && ValidateDest() && _timerSet)
            {
                _timerActive = ActivatedBox.Checked;
                ToggleActivatedLabel(true);
                _timer.Enabled = true;
                valid = true;
            }
            else
            {
                if (!ValidateSrc())
                {
                    MessageBox.Show("Error: Source path not valid.");
                }
                if (!ValidateDest())
                {
                    MessageBox.Show("Error: Destination path not valid.");
                }
                else
                {
                    MessageBox.Show("Error: Timer is NOT set.");
                }
                ToggleActivatedLabel(false);
            }
            return valid;
        }

        /// <summary> Validates the path passed to the method </summary>
        /// <param name="path"> Path to be validated </param>
        private bool ValidateDirectory(string path)
        {
            if (path != "")
            {
                return Directory.Exists(path);
            }
            return false;
        }
        
        /// <summary> Validates the stored source path </summary>
        private bool ValidateSrc()
        {
            if (ValidateDirectory(SourcePath))
            {
                Source_label.ForeColor = Color.LightGreen;
                return true;
            }
            Source_label.ForeColor = Color.Red;
            return false;
        }

        /// <summary> Validates the stored destination path </summary>
        private bool ValidateDest()
        {
            if (ValidateDirectory(DestinationPath))
            {
                Destination_label.ForeColor = Color.LightGreen;
                return true;
            }
            Destination_label.ForeColor = Color.Red;
            return false;
        }

        /// <summary> Checks if a timer is created already, if so then it calls DisposeTimer() </summary>
        private void CreateTimer()
        {
            if (_timer != null)
            {
                DisposeTimer();
            }

            _timer = new System.Timers.Timer(_duration);
            _timer.Elapsed += TimeChecker_Event;

            // Have the timer go off only once (false), or repeat (true)
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        /// <summary> Properly disposes of the current timer </summary>
        private void DisposeTimer()
        {
            if (_timer != null)
            {
                _timer.Elapsed -= new ElapsedEventHandler(TimeChecker_Event);
                _timer.Dispose();
            }
        }
        #endregion

        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            _log.Info("User has closed the form.");
            this.Dispose();
        }
    }
}
