using LazyBones.Properties;
using Microsoft.Win32.TaskScheduler;
using NLog;
using NLog.Windows.Forms;
using OtpNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyBones
{
    public partial class LazyBones : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        bool minimizedToTray;
        private Random _random = new Random();
        private int _rndMinute;
        private bool _timeToShutdown = false;

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == SingleInstance.WM_SHOWFIRSTINSTANCE)
            {
                ShowWindow();
            }
            base.WndProc(ref message);
        }

        public LazyBones()
        {
            InitializeComponent();
        }

        private string CheckToken()
        {
            string value = InputForm("Введіть токен ключа для VPN з'єднання", "Токен VPN з'єднання", false);

            if (!String.IsNullOrEmpty(value) && value.Length >= 26)
            {
                Settings.Default.token = value;
            }
            else Settings.Default.token = String.Empty;
            return Settings.Default.token;
        }

        private bool AllFieldsIsFull()
        {
            IPAddress ip;
            if ((Settings.Default.user == String.Empty)
                || (Settings.Default.vpnname == String.Empty)
                || ((Settings.Default.remoteip == String.Empty) && IPAddress.TryParse(Settings.Default.remoteip, out ip))
                || (Settings.Default.rdpPath == String.Empty))

            {
                Logger.Warn("Не заповнені поля для корректного з'єднання.");
                connectBox.Enabled = false;
                return false;
            }
            connectBox.Enabled = true;
            return true;    
        }

        private string VersionLabel
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                }
                else
                {
                    return Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _rndMinute = _random.Next(1, 10);
            onTimePicker.Enabled = !oncheckBox.Checked;

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"LazyBones.log"),
                Layout = "${date:format=dd-MM-yyyy HH\\:mm\\:ss} - ${level} - ${message}",
                AutoFlush = true,
                Encoding = Encoding.GetEncoding("windows-1251"),
                DeleteOldFileOnStartup = true
            };

            RichTextBoxTarget target = new RichTextBoxTarget()
            {
                FormName = "LazyBones", // your winform class name
                ControlName = "rtbLog", // your RichTextBox control/variable name
                AutoScroll = true,
                Layout = "${date:format=dd-MM-yyyy HH\\:mm\\:ss} - ${message}",
                UseDefaultRowColoringRules = false,
            };
            
            target.RowColoringRules.Add
                (new RichTextBoxRowColoringRule
                    (
                        "level >= LogLevel.Warning", // condition
                        "Red", // font color
                        "InactiveBorder" // back color
                    )
                );

            config.AddTarget("richTextBox", target);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, target);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;

           // Logger.Info($"ver.{Application.ProductVersion} © 2022 by tigroff");
            Logger.Info($"{Assembly.GetEntryAssembly().GetName().Name} v.{VersionLabel} © 2022 by tigroff");
            OffLogging();

            if (Settings.Default.token == String.Empty && CheckToken() == String.Empty)
            {
                MessageBox.Show("Робота без токена двуфакторної аутентифікації неможлива! Зверніться до системного адміністратора.","Помилка",MessageBoxButtons.OK);
                Logger.Error("Робота без токена двуфакторної аутентифікації неможлива! Зверніться до системного адміністратора.");
                Application.Exit();
            }

            EnablingControls();

            if (AllFieldsIsFull())
            {
                Logger.Info("Чекаємо на SoftEther.");

                while (!Process.GetProcessesByName(Environment.Is64BitOperatingSystem ? "vpnclient_x64" : "vpnclient").Any())
                {
                    Thread.Sleep(500);
                }
                Logger.Info("SoftEther готовий до роботи.");
                watchDogTimer.Start();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Utils.CheckForConnection())
            {
                if (Rdp.Connected)
                {
                    Rdp.Disconnect();
                }
                
                if (Vpn.Connected)
                {
                    Vpn.Disconnect();
                }
            }
            Settings.Default.Save();
            LogManager.Shutdown();
            if (_timeToShutdown) 
            {
                Process.Start("shutdown", "/h /f");
            }
        }

        void MinimizeToTray()
        {
            notifyIcon.Text = ProgramInfo.AssemblyTitle;
            notifyIcon.Visible = true;
            this.Hide();
            minimizedToTray = true;
        }

        public void ShowWindow()
        {
            if (minimizedToTray)
            {
                notifyIcon.Visible = false;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                minimizedToTray = false;
            }
            else
            {
                WinApi.ShowToFront(this.Handle);
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            ShowWindow();
        }

        private void LazyBones_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                MinimizeToTray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.ShowDialog();
            rdpPath.Text = openFileDialog.FileName;
        }
        
        private void watchDogTimer_Tick(object sender, EventArgs e)
        {
            if (Utils.CheckForConnection() && connectBox.Checked)
            {
                if (!Vpn.Connected)
                {
                    Vpn.Connect();
                }
                else if (!Rdp.Connected)
                { 
                    Rdp.Connect();
                    if (this.WindowState == FormWindowState.Normal)
                    {
                        MinimizeToTray();
                    }
                }
            }
            else
            {
                if (Vpn.Connected)
                {
                    Rdp.Disconnect();
                    Vpn.Disconnect();
                }
            }

            if (((DateTime.Now.Hour >= offTimePicker.Value.Hour && DateTime.Now.Minute >= (offTimePicker.Value.Minute + _rndMinute)) || 
                (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= (45 + _rndMinute) && DateTime.Now.DayOfWeek == DayOfWeek.Friday)) && 
                (offcheckBox.Checked))
            {
                watchDogTimer.Stop();
                DialogResult dialog = new ShutdownForm().ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    Logger.Info("Автоматичне вимкнення комп'ютера.");
                    _timeToShutdown = true;
                    Application.Exit();
                }
                else
                {
                    Logger.Info("Автоматичне вимкнення скасовано.");
                    offcheckBox.Checked = false;
                    watchDogTimer.Start();
                }
            }
        }

        private void oncheckBox_CheckedChanged(object sender, EventArgs e)
        {
            onTimePicker.Enabled = !oncheckBox.Checked;
            const string taskName = "WakeUp and Work";
            
            using (TaskService ts = new TaskService())
            {
                if (oncheckBox.Checked)
                {
                    string password = InputForm("Введіть пароль входу в Windows:", "Введіть пароль", true);

                    if (!String.IsNullOrEmpty(password))
                    {
                        // Create a new task definition and assign properties
                        TaskDefinition td = TaskService.Instance.NewTask();

                        td.RegistrationInfo.Description = "LazyBones task";
                        td.Principal.LogonType = TaskLogonType.Password;
                        td.Principal.UserId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                        td.Settings.WakeToRun = true;

                        td.Triggers.Add(new WeeklyTrigger
                        {
                            StartBoundary = DateTime.Today
                          + TimeSpan.FromHours(onTimePicker.Value.Hour) + TimeSpan.FromMinutes(onTimePicker.Value.Minute),
                            DaysOfWeek = DaysOfTheWeek.Monday | DaysOfTheWeek.Tuesday | DaysOfTheWeek.Wednesday | DaysOfTheWeek.Thursday | DaysOfTheWeek.Friday
                        });

                        // Add an action that will launch Notepad whenever the trigger fires
                        td.Actions.Add(new ExecAction(Application.ExecutablePath));

                        // Register the task in the root folder
                        try
                        {
                            TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, td.Principal.UserId, password, TaskLogonType.Password);

                        }
                        catch (Exception ex)
                        {
                            oncheckBox.Checked = false;
                            Logger.Error(ex.Message);
                        } 
                    }
                }
                else
                {
                    try
                    {
                        ts.RootFolder.DeleteTask(taskName);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private string InputForm(string label1, string text, bool passchar)
        {
            string value = String.Empty;
            InputForm form = new InputForm();
            form.label1.Text = label1;
            form.Text = text;
            form.textBox1.UseSystemPasswordChar = passchar;

            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                value = form.textBox1.Text;
            }
            return value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(CheckToken()))
            {
                Logger.Error("Робота без токена двуфакторної аутентифікації неможлива! Зверніться до системного адміністратора.");
            }
        }

        private void OffLogging() 
        {
            if (offcheckBox.Checked)
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                    Logger.Info($"Заплановано вимкнення {DateTime.Now:dd.MM} після 15:{45+_rndMinute}.");
                else
                    Logger.Info($"Заплановано вимкнення {DateTime.Now:dd.MM} після {offTimePicker.Value.AddMinutes(_rndMinute):HH:mm}.");
        }

        private void connectBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AllFieldsIsFull())
            {
                EnablingControls();
            }
        }

        private void EnablingControls()
        {
            userBox.Enabled = !connectBox.Checked;
            vpnBox.Enabled = !connectBox.Checked;
            ipBox.Enabled = !connectBox.Checked;
            rdpPath.Enabled = !connectBox.Checked;
            button1.Enabled = !connectBox.Checked;
            button2.Enabled = !connectBox.Checked;
        }

        private void offcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OffLogging();
        }

        private void userBox_TextChanged(object sender, EventArgs e)
        {
            connectBox.Enabled = (userBox.Text != String.Empty && vpnBox.Text != String.Empty && ipBox.Text != String.Empty && rdpPath.Text != String.Empty);
        }

        private void ipBox_Leave(object sender, EventArgs e)
        {
            IPAddress ip;
            if (!IPAddress.TryParse(ipBox.Text, out ip)) 
            {
                Logger.Error("Введена невірна IP адреса!");
            }
        }

    }
}
