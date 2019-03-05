namespace Task_1
{
    partial class Main_F
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_F));
            this.ReadyLoad_LB = new System.Windows.Forms.ListBox();
            this.Open_D = new System.Windows.Forms.OpenFileDialog();
            this.Main_TS = new System.Windows.Forms.ToolStrip();
            this.Button_TS = new System.Windows.Forms.ToolStripButton();
            this.Load_GB = new System.Windows.Forms.GroupBox();
            this.FileShow_LB = new System.Windows.Forms.ListBox();
            this.DelReadyLoad_B = new System.Windows.Forms.Button();
            this.AddToQueueOfReady_B = new System.Windows.Forms.Button();
            this.QueueOfReady_LB = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Go_B = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.QueueOfWait_LB = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.IO_L = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.IO_LB = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GoingP_L = new System.Windows.Forms.Label();
            this.StateP_L = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TimerOfProcessor = new System.Windows.Forms.Timer(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.TimerOfIO = new System.Windows.Forms.Timer(this.components);
            this.AddAll_B = new System.Windows.Forms.Button();
            this.Log = new System.Windows.Forms.RichTextBox();
            this.Processor_TC = new System.Windows.Forms.TabControl();
            this.Main_TS.SuspendLayout();
            this.Load_GB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReadyLoad_LB
            // 
            this.ReadyLoad_LB.FormattingEnabled = true;
            this.ReadyLoad_LB.Location = new System.Drawing.Point(6, 19);
            this.ReadyLoad_LB.MultiColumn = true;
            this.ReadyLoad_LB.Name = "ReadyLoad_LB";
            this.ReadyLoad_LB.Size = new System.Drawing.Size(263, 173);
            this.ReadyLoad_LB.TabIndex = 0;
            this.ReadyLoad_LB.SelectedIndexChanged += new System.EventHandler(this.ReadyLoad_LB_SelectedIndexChanged);
            // 
            // Open_D
            // 
            this.Open_D.FileName = "Open_D";
            this.Open_D.Multiselect = true;
            // 
            // Main_TS
            // 
            this.Main_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_TS});
            this.Main_TS.Location = new System.Drawing.Point(0, 0);
            this.Main_TS.Name = "Main_TS";
            this.Main_TS.Size = new System.Drawing.Size(838, 25);
            this.Main_TS.TabIndex = 3;
            this.Main_TS.Text = "toolStrip1";
            // 
            // Button_TS
            // 
            this.Button_TS.Image = global::Task_1.Properties.Resources.add;
            this.Button_TS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_TS.Name = "Button_TS";
            this.Button_TS.Size = new System.Drawing.Size(81, 22);
            this.Button_TS.Text = "Загрузить";
            this.Button_TS.Click += new System.EventHandler(this.Button_TS_Click);
            // 
            // Load_GB
            // 
            this.Load_GB.Controls.Add(this.FileShow_LB);
            this.Load_GB.Controls.Add(this.DelReadyLoad_B);
            this.Load_GB.Controls.Add(this.AddToQueueOfReady_B);
            this.Load_GB.Location = new System.Drawing.Point(100, 12);
            this.Load_GB.Name = "Load_GB";
            this.Load_GB.Size = new System.Drawing.Size(165, 176);
            this.Load_GB.TabIndex = 5;
            this.Load_GB.TabStop = false;
            this.Load_GB.Text = "Описание файла";
            // 
            // FileShow_LB
            // 
            this.FileShow_LB.FormattingEnabled = true;
            this.FileShow_LB.Location = new System.Drawing.Point(6, 19);
            this.FileShow_LB.Name = "FileShow_LB";
            this.FileShow_LB.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.FileShow_LB.Size = new System.Drawing.Size(151, 108);
            this.FileShow_LB.TabIndex = 6;
            this.FileShow_LB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FileShow_LB_MouseUp);
            // 
            // DelReadyLoad_B
            // 
            this.DelReadyLoad_B.Location = new System.Drawing.Point(92, 139);
            this.DelReadyLoad_B.Name = "DelReadyLoad_B";
            this.DelReadyLoad_B.Size = new System.Drawing.Size(65, 31);
            this.DelReadyLoad_B.TabIndex = 5;
            this.DelReadyLoad_B.Text = "Удалить";
            this.DelReadyLoad_B.UseVisualStyleBackColor = true;
            this.DelReadyLoad_B.Click += new System.EventHandler(this.DelReadyLoad_B_Click);
            this.DelReadyLoad_B.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FileShow_LB_MouseUp);
            // 
            // AddToQueueOfReady_B
            // 
            this.AddToQueueOfReady_B.Location = new System.Drawing.Point(6, 139);
            this.AddToQueueOfReady_B.Name = "AddToQueueOfReady_B";
            this.AddToQueueOfReady_B.Size = new System.Drawing.Size(65, 31);
            this.AddToQueueOfReady_B.TabIndex = 5;
            this.AddToQueueOfReady_B.Text = "Добавить";
            this.AddToQueueOfReady_B.UseVisualStyleBackColor = true;
            this.AddToQueueOfReady_B.Click += new System.EventHandler(this.AddToQueueOfReady_B_Click);
            this.AddToQueueOfReady_B.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FileShow_LB_MouseUp);
            // 
            // QueueOfReady_LB
            // 
            this.QueueOfReady_LB.FormattingEnabled = true;
            this.QueueOfReady_LB.Location = new System.Drawing.Point(6, 31);
            this.QueueOfReady_LB.Name = "QueueOfReady_LB";
            this.QueueOfReady_LB.Size = new System.Drawing.Size(150, 160);
            this.QueueOfReady_LB.TabIndex = 6;
            this.QueueOfReady_LB.SelectedIndexChanged += new System.EventHandler(this.QueueOfReady_LB_SelectedIndexChanged);
            this.QueueOfReady_LB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.QueueOfReady_LB_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.Load_GB);
            this.groupBox1.Controls.Add(this.ReadyLoad_LB);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 200);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Список доступных файлов";
            // 
            // Go_B
            // 
            this.Go_B.Location = new System.Drawing.Point(311, 171);
            this.Go_B.Name = "Go_B";
            this.Go_B.Size = new System.Drawing.Size(75, 30);
            this.Go_B.TabIndex = 10;
            this.Go_B.Text = "Запустить";
            this.Go_B.UseVisualStyleBackColor = true;
            this.Go_B.Click += new System.EventHandler(this.Go_B_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.QueueOfWait_LB);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.QueueOfReady_LB);
            this.groupBox2.Location = new System.Drawing.Point(12, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 200);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Очереди";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "ОЖИДАНИЕ";
            // 
            // QueueOfWait_LB
            // 
            this.QueueOfWait_LB.FormattingEnabled = true;
            this.QueueOfWait_LB.Location = new System.Drawing.Point(183, 31);
            this.QueueOfWait_LB.Name = "QueueOfWait_LB";
            this.QueueOfWait_LB.Size = new System.Drawing.Size(150, 160);
            this.QueueOfWait_LB.TabIndex = 6;
            this.QueueOfWait_LB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.QueueOfReady_LB_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "ГОТОВНОСТЬ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(12, 215);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(349, 288);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ВЫПОЛНЕНИЕ";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.IO_L);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.tabControl1);
            this.groupBox5.Location = new System.Drawing.Point(175, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(163, 263);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ВВОД/ВЫВОД";
            // 
            // IO_L
            // 
            this.IO_L.AutoSize = true;
            this.IO_L.Location = new System.Drawing.Point(0, 35);
            this.IO_L.Name = "IO_L";
            this.IO_L.Size = new System.Drawing.Size(67, 13);
            this.IO_L.TabIndex = 12;
            this.IO_L.Text = "Выполнено:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Состояние:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(158, 209);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.IO_LB);
            this.tabPage2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(150, 183);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Процесс";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "PID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Память:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Квант:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Состояние:";
            // 
            // IO_LB
            // 
            this.IO_LB.FormattingEnabled = true;
            this.IO_LB.Location = new System.Drawing.Point(0, 69);
            this.IO_LB.Name = "IO_LB";
            this.IO_LB.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.IO_LB.Size = new System.Drawing.Size(140, 108);
            this.IO_LB.TabIndex = 10;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.GoingP_L);
            this.groupBox4.Controls.Add(this.StateP_L);
            this.groupBox4.Controls.Add(this.Processor_TC);
            this.groupBox4.Location = new System.Drawing.Point(6, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(163, 263);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ПРОЦЕССОР";
            // 
            // GoingP_L
            // 
            this.GoingP_L.AutoSize = true;
            this.GoingP_L.Location = new System.Drawing.Point(0, 35);
            this.GoingP_L.Name = "GoingP_L";
            this.GoingP_L.Size = new System.Drawing.Size(67, 13);
            this.GoingP_L.TabIndex = 12;
            this.GoingP_L.Text = "Выполнено:";
            // 
            // StateP_L
            // 
            this.StateP_L.AutoSize = true;
            this.StateP_L.Location = new System.Drawing.Point(1, 16);
            this.StateP_L.Name = "StateP_L";
            this.StateP_L.Size = new System.Drawing.Size(64, 13);
            this.StateP_L.TabIndex = 12;
            this.StateP_L.Text = "Состояние:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(311, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 10;
            this.button1.Text = "Остановить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TimerOfProcessor
            // 
            this.TimerOfProcessor.Interval = 1000;
            this.TimerOfProcessor.Tick += new System.EventHandler(this.TimerOfProcessor_Tick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox3);
            this.groupBox6.Controls.Add(this.groupBox2);
            this.groupBox6.Location = new System.Drawing.Point(431, 37);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(371, 516);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            // 
            // TimerOfIO
            // 
            this.TimerOfIO.Interval = 1000;
            this.TimerOfIO.Tick += new System.EventHandler(this.TimerOfIO_Tick);
            // 
            // AddAll_B
            // 
            this.AddAll_B.CausesValidation = false;
            this.AddAll_B.Location = new System.Drawing.Point(310, 56);
            this.AddAll_B.Name = "AddAll_B";
            this.AddAll_B.Size = new System.Drawing.Size(95, 30);
            this.AddAll_B.TabIndex = 10;
            this.AddAll_B.Text = "Добавить все";
            this.AddAll_B.UseVisualStyleBackColor = true;
            this.AddAll_B.Click += new System.EventHandler(this.AddAll_B_Click);
            // 
            // Log
            // 
            this.Log.BackColor = System.Drawing.SystemColors.InfoText;
            this.Log.ForeColor = System.Drawing.SystemColors.Window;
            this.Log.Location = new System.Drawing.Point(12, 243);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(374, 310);
            this.Log.TabIndex = 13;
            this.Log.Text = "";
            // 
            // Processor_TC
            // 
            this.Processor_TC.Location = new System.Drawing.Point(0, 54);
            this.Processor_TC.Name = "Processor_TC";
            this.Processor_TC.SelectedIndex = 0;
            this.Processor_TC.Size = new System.Drawing.Size(158, 209);
            this.Processor_TC.TabIndex = 10;
            // 
            // Main_F
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(838, 573);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.Go_B);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.AddAll_B);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Main_TS);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main_F";
            this.Text = "Планировщик процессов (модель)";
            this.Load += new System.EventHandler(this.Main_F_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.QueueOfReady_LB_MouseUp);
            this.Main_TS.ResumeLayout(false);
            this.Main_TS.PerformLayout();
            this.Load_GB.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ReadyLoad_LB;
        private System.Windows.Forms.OpenFileDialog Open_D;
        private System.Windows.Forms.ToolStrip Main_TS;
        private System.Windows.Forms.ToolStripButton Button_TS;
        private System.Windows.Forms.GroupBox Load_GB;
        private System.Windows.Forms.Button AddToQueueOfReady_B;
        private System.Windows.Forms.ListBox FileShow_LB;
        private System.Windows.Forms.ListBox QueueOfReady_LB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DelReadyLoad_B;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox QueueOfWait_LB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label GoingP_L;
        private System.Windows.Forms.Label StateP_L;
        private System.Windows.Forms.Button Go_B;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label IO_L;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox IO_LB;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Timer TimerOfIO;
        private System.Windows.Forms.Button AddAll_B;
        private System.Windows.Forms.Timer TimerOfProcessor;
        private System.Windows.Forms.RichTextBox Log;
        internal System.Windows.Forms.TabControl Processor_TC;
    }
}

