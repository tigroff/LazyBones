﻿using LazyBones.Properties;
using Microsoft.Win32.TaskScheduler;
using NLog;
using OtpNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyBones
{
    public partial class LazyBones : Form
    {
        //https://1gai.ru/publ/525372-kak-nastroit-avtomaticheskoe-vkljuchenie-kompjutera-na-windows-i-macos.html
        private bool _connected = false;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        bool minimizedToTray;
        private Random _rand = new Random();
        bool _firstPingLog = true;


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

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Відміна";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 15, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private string CheckToken()
        {
            string value = "Введіть ключ";
            if ((InputBox("Введіть токен ключа для VPN з'єднання", "Токен:", ref value) == DialogResult.OK) && (value.Length >= 26))
            {
                Settings.Default.token = value;
            }
            else Settings.Default.token = String.Empty;
            return Settings.Default.token;
        }

        private void RdpConnect()
        {
            try
            {
                Thread.Sleep(Decimal.ToInt32(sleepTime.Value) * 1000);
                System.Diagnostics.Process.Start("mstsc.exe", Settings.Default.rdpPath);
                Logger.Info("RDP під'єднано.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void RdpDisconnect()
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("mstsc"))
                {
                    proc.Kill();
                    Logger.Info("RDP від'єднано.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            onTimePicker.Enabled = !oncheckBox.Checked;
            passBox.Enabled = !oncheckBox.Checked;

            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = "d:\\LazyBones.log",
                Layout = "${date:format=MM-dd-yyyy HH\\:mm\\:ss} - ${level} - ${message}",
                AutoFlush = true,
                Encoding = Encoding.GetEncoding("windows-1251")
            };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;

            if (Settings.Default.token == String.Empty && CheckToken() == String.Empty)
            {
                MessageBox.Show("Робота без токена двуфакторної аутентифікації неможлива!\r\n" +
                "Зверніться до системного адміністратора.", "Помилка");
                Logger.Error("Не заповнено токен двуфакторної аутентифікації.");
                Application.Exit();
            }

            if ((Settings.Default.user == String.Empty) 
                || (Settings.Default.vpnname == String.Empty)
                || (Settings.Default.remoteip == String.Empty)
                || (Settings.Default.rdpPath == String.Empty))

            {
                Logger.Error("Не заповнені поля для корректного з'єднання.");
                StatusLabel.Text = "Заповніть всі поля та перезапустіть програму!";
            }
            else
            {
                RandomizeTimer();
                watchDogTimer.Start();
                StatusLabel.Text = "Чекаємо старту SoftEther.";
            }
        }

        private void RandomizeTimer()
        {
            timer.Interval = _rand.Next(240, 360) * 10000;
            Logger.Info($"Наступне перепідключення через {timer.Interval / 60000} хвилин.");
            timer.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (PingNet())
            {
                RdpDisconnect();
                if (_connected)
                {
                    Vpn.Disconnect();
                }
            }
            Settings.Default.Save();
            LogManager.Shutdown();
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
            openFileDialog.ShowDialog();
            rdpPath.Text = openFileDialog.FileName;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Logger.Info("Автоматичне перепідключення");
            timer.Stop();
            if (PingNet())
            {
                if (!_connected)
                {
                    _connected = Vpn.Connect();
                }
                RdpDisconnect();
                RdpConnect(); 
            }
            RandomizeTimer();
        }

        private bool PingNet()
        {
            Ping pingSender = new Ping();
            try
            {
                bool _pinggoogle = false, _pingvpn = false;
                PingReply reply = pingSender.Send("8.8.8.8", 250);
                if (reply.Status == IPStatus.Success && reply.RoundtripTime > 0)
                    _pinggoogle = true;
                else
                {
                    _pinggoogle = false;
                    StatusLabel.Text = "Немає інтернету.";
                    LogPing();
                }

                PingReply reply1 = pingSender.Send("193.109.248.251", 250); //193.109.248.251
                if (reply.Status == IPStatus.Success && _pinggoogle && reply1.RoundtripTime > 0)
                    _pingvpn = true;
                else
                {
                    _pingvpn = false;
                    StatusLabel.Text = "Немає зв'язку з VPN сервером.";
                    LogPing();
                }

                if (_pinggoogle && _pingvpn)
                {
                    _firstPingLog = true;
                    StatusLabel.Text = "";
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        private void LogPing()
        {
            if (_firstPingLog)
            {
                Logger.Warn(StatusLabel.Text);
                _firstPingLog = false;
            }
        }

        private void watchDogTimer_Tick(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("vpnclient_x64").Any() || Process.GetProcessesByName("vpnclient").Any()) 
            {
                if (PingNet())
                {
                    if (!_connected)
                    {
                        _connected = Vpn.Connect();
                        RdpConnect();
                        if (_connected && this.WindowState == FormWindowState.Normal)
                        {
                            MinimizeToTray();
                        }
                    }
                }
                else
                {
                    if (_connected)
                    {
                        _connected = false;
                        RdpDisconnect();
                        Vpn.Disconnect();
                    }

                }
            }

            if (((DateTime.Now.Hour >= offTimePicker.Value.Hour && DateTime.Now.Minute >= (offTimePicker.Value.Minute + _rand.Next(1,10))) || 
                (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= (45 + _rand.Next(1,10)) && DateTime.Now.DayOfWeek == DayOfWeek.Friday)) && 
                (offcheckBox.Checked))
            {
                timer.Stop();
                watchDogTimer.Stop();
                Logger.Info("Автоматичне вимкнення комп'ютера.");
                System.Diagnostics.Process.Start("shutdown", "/h /f");
                Application.Exit();
            }
        }

        private void oncheckBox_CheckedChanged(object sender, EventArgs e)
        {
            onTimePicker.Enabled = !oncheckBox.Checked;
            passBox.Enabled = !oncheckBox.Checked;
            const string taskName = "WakeUp and Work";
            
            using (TaskService ts = new TaskService())
            {
                if (oncheckBox.Checked)
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
                        TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, td.Principal.UserId, passBox.Text, TaskLogonType.Password);

                    }
                    catch (Exception ex)
                    {
                        oncheckBox.Checked = false;
                        Logger.Error(ex.Message);
                        MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button2_Click(object sender, EventArgs e)
        {
            CheckToken();
        }
    }
}