namespace LazyBones
{
    partial class LazyBones
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LazyBones));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.vpnBox = new System.Windows.Forms.TextBox();
            this.userBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rdpPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.watchDogTimer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.oncheckBox = new System.Windows.Forms.CheckBox();
            this.offcheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.connectBox = new System.Windows.Forms.CheckBox();
            this.passBox = new System.Windows.Forms.TextBox();
            this.onTimePicker = new System.Windows.Forms.DateTimePicker();
            this.offTimePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.vpnBox);
            this.groupBox1.Controls.Add(this.userBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметри VPN з\'єднання";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(167, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 21);
            this.button2.TabIndex = 2;
            this.button2.Text = "Токен";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // vpnBox
            // 
            this.vpnBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LazyBones.Properties.Settings.Default, "vpnname", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.vpnBox.Location = new System.Drawing.Point(99, 39);
            this.vpnBox.Name = "vpnBox";
            this.vpnBox.Size = new System.Drawing.Size(131, 20);
            this.vpnBox.TabIndex = 3;
            this.vpnBox.Text = global::LazyBones.Properties.Settings.Default.vpnname;
            this.vpnBox.TextChanged += new System.EventHandler(this.vpnBox_TextChanged);
            // 
            // userBox
            // 
            this.userBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LazyBones.Properties.Settings.Default, "user", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.userBox.Location = new System.Drawing.Point(71, 16);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(90, 20);
            this.userBox.TabIndex = 1;
            this.userBox.Text = global::LazyBones.Properties.Settings.Default.user;
            this.userBox.TextChanged += new System.EventHandler(this.userBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Назва з\'єднання";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Користувач";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ipBox);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.rdpPath);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(246, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 68);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметри RDP з\'єднання";
            // 
            // ipBox
            // 
            this.ipBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LazyBones.Properties.Settings.Default, "remoteip", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ipBox.Location = new System.Drawing.Point(70, 17);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(158, 20);
            this.ipBox.TabIndex = 5;
            this.ipBox.Text = global::LazyBones.Properties.Settings.Default.remoteip;
            this.ipBox.TextChanged += new System.EventHandler(this.ipBox_TextChanged);
            this.ipBox.Leave += new System.EventHandler(this.ipBox_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 20);
            this.button1.TabIndex = 8;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rdpPath
            // 
            this.rdpPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LazyBones.Properties.Settings.Default, "rdpPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rdpPath.Location = new System.Drawing.Point(70, 40);
            this.rdpPath.Name = "rdpPath";
            this.rdpPath.Size = new System.Drawing.Size(134, 20);
            this.rdpPath.TabIndex = 7;
            this.rdpPath.Text = global::LazyBones.Properties.Settings.Default.rdpPath;
            this.rdpPath.TextChanged += new System.EventHandler(this.rdpPath_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "RDP-файл";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "IP адреса";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // timer
            // 
            this.timer.Interval = 3600000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // watchDogTimer
            // 
            this.watchDogTimer.Interval = 10000;
            this.watchDogTimer.Tick += new System.EventHandler(this.watchDogTimer_Tick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "RDP файли|*.rdp";
            // 
            // oncheckBox
            // 
            this.oncheckBox.AutoSize = true;
            this.oncheckBox.Checked = global::LazyBones.Properties.Settings.Default.onFlag;
            this.oncheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::LazyBones.Properties.Settings.Default, "onFlag", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.oncheckBox.Location = new System.Drawing.Point(7, 77);
            this.oncheckBox.Name = "oncheckBox";
            this.oncheckBox.Size = new System.Drawing.Size(135, 17);
            this.oncheckBox.TabIndex = 9;
            this.oncheckBox.Text = "Вмикати комп\'ютер в";
            this.toolTip1.SetToolTip(this.oncheckBox, "Тільки з режиму гібернації!");
            this.oncheckBox.UseVisualStyleBackColor = true;
            this.oncheckBox.CheckedChanged += new System.EventHandler(this.oncheckBox_CheckedChanged);
            // 
            // offcheckBox
            // 
            this.offcheckBox.AutoSize = true;
            this.offcheckBox.Checked = global::LazyBones.Properties.Settings.Default.offFlag;
            this.offcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.offcheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::LazyBones.Properties.Settings.Default, "offFlag", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.offcheckBox.Location = new System.Drawing.Point(246, 77);
            this.offcheckBox.Name = "offcheckBox";
            this.offcheckBox.Size = new System.Drawing.Size(160, 17);
            this.offcheckBox.TabIndex = 11;
            this.offcheckBox.Text = "Вимкнути комп\'ютер після";
            this.toolTip1.SetToolTip(this.offcheckBox, "Тільки при наявності режиму гібернації!");
            this.offcheckBox.UseVisualStyleBackColor = true;
            this.offcheckBox.CheckedChanged += new System.EventHandler(this.offcheckBox_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Пароль входу в Windows";
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(4, 123);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbLog.Size = new System.Drawing.Size(479, 109);
            this.rtbLog.TabIndex = 15;
            this.rtbLog.Text = "";
            // 
            // connectBox
            // 
            this.connectBox.AutoSize = true;
            this.connectBox.Checked = global::LazyBones.Properties.Settings.Default.autoconnect;
            this.connectBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::LazyBones.Properties.Settings.Default, "autoconnect", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.connectBox.Location = new System.Drawing.Point(246, 100);
            this.connectBox.Name = "connectBox";
            this.connectBox.Size = new System.Drawing.Size(158, 17);
            this.connectBox.TabIndex = 16;
            this.connectBox.Text = "Автоматичне підключення";
            this.connectBox.UseVisualStyleBackColor = true;
            this.connectBox.CheckedChanged += new System.EventHandler(this.connectBox_CheckedChanged);
            // 
            // passBox
            // 
            this.passBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::LazyBones.Properties.Settings.Default, "password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.passBox.Location = new System.Drawing.Point(137, 98);
            this.passBox.Name = "passBox";
            this.passBox.Size = new System.Drawing.Size(100, 20);
            this.passBox.TabIndex = 14;
            this.passBox.Text = global::LazyBones.Properties.Settings.Default.password;
            this.passBox.UseSystemPasswordChar = true;
            // 
            // onTimePicker
            // 
            this.onTimePicker.CustomFormat = "";
            this.onTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::LazyBones.Properties.Settings.Default, "onTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.onTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.onTimePicker.Location = new System.Drawing.Point(166, 75);
            this.onTimePicker.Name = "onTimePicker";
            this.onTimePicker.ShowUpDown = true;
            this.onTimePicker.Size = new System.Drawing.Size(71, 20);
            this.onTimePicker.TabIndex = 10;
            this.onTimePicker.Value = global::LazyBones.Properties.Settings.Default.onTime;
            // 
            // offTimePicker
            // 
            this.offTimePicker.CustomFormat = "";
            this.offTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::LazyBones.Properties.Settings.Default, "offTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.offTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.offTimePicker.Location = new System.Drawing.Point(408, 76);
            this.offTimePicker.Name = "offTimePicker";
            this.offTimePicker.ShowUpDown = true;
            this.offTimePicker.Size = new System.Drawing.Size(71, 20);
            this.offTimePicker.TabIndex = 12;
            this.offTimePicker.Value = global::LazyBones.Properties.Settings.Default.offTime;
            // 
            // LazyBones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 235);
            this.Controls.Add(this.connectBox);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.passBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.onTimePicker);
            this.Controls.Add(this.oncheckBox);
            this.Controls.Add(this.offTimePicker);
            this.Controls.Add(this.offcheckBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LazyBones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LazyBones";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.LazyBones_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox vpnBox;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox rdpPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox offcheckBox;
        private System.Windows.Forms.DateTimePicker offTimePicker;
        private System.Windows.Forms.Timer watchDogTimer;
        private System.Windows.Forms.CheckBox oncheckBox;
        private System.Windows.Forms.DateTimePicker onTimePicker;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.CheckBox connectBox;
    }
}

