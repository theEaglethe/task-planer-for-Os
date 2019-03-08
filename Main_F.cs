using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Task_1
{    
    public partial class Main_F : Form
    {
        public Main_F()
        {
            InitializeComponent();
        }       
        //TProcess currentProcess = null; // Текущий процесс (первый из ГОТОВНОСТЬ)
        List<TProcess> queueOfReady = new List<TProcess>();  // Главный список процессов  - ГОТОВНОСТЬ 
        List<TProcess> queueOfWait = new List<TProcess>(); // Главный список процессов  - ОЖИДАНИЕ        
        TProcessor processor = new TProcessor();
        TIO inOut = new TIO();
        
        #region Модуль загрузки файлов

        /*Меню загрузки файла*/
        private void Button_TS_Click(object sender, EventArgs e)
        {
            Load_GB.Hide();
            ReadyLoad_LB.ClearSelected();
            Open_D.Multiselect = true;
            Open_D.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            Open_D.FileName = ""; // Имя файла "по умолчанию" не задано 
            Open_D.Title = "Загрузка файла, моделирующего процесс";
            /* *
            * Вызов диалогового окна выбора файла для загрузки и проверка результата завершения его работы. 
            * Если окно закрыто с подтверждением загрузки (нажата кнопка OK),
            * то выполнение действий по чтению файла построчно
            * */
            if (Open_D.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in Open_D.FileNames)
                {
                    using (StreamReader sr = new StreamReader(file, Encoding.Default))
                    {
                        TBootFile bootFile = new TBootFile();
                        bootFile.nameOfFile = Path.GetFileNameWithoutExtension(file);
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine().Trim();
                            bootFile.comandsOfFile.Add(line);
                        }
                        ReadyLoad_LB.Items.Add(bootFile);
                    }
                }
                Log.Clear();
                ToLog("Выберите любой файл и добавте его в список ГОТОВНОСТЬ", Color.DeepSkyBlue);
            }
        }
        /*Установка свойст при загрузке главной формы*/
        private void Main_F_Load(object sender, EventArgs e)
        {
            Load_GB.Hide();
            ToLog("Планировщик процессов готов к работе. Загрузите файлы, моделирующие процесс:\r\n", Color.DeepSkyBlue);
            ToLog("       -- допускается выбор нескольких фалйлов.", Color.DeepSkyBlue);
        }
        /*Кнопка добавление файла в ГОТОВНОСТЬ*/
        private void AddToQueueOfReady_B_Click(object sender, EventArgs e)
        {
            if(TimerOfProcessor.Enabled == false)
            {
                Log.Clear();
                Go_B.Enabled = true;
                TBootFile selectFile = ReadyLoad_LB.SelectedItem as TBootFile;
                //добавление процесса, выбранного в списке доступных, в очередь ГОТОВНОСТЬ
                QueueOfReady_LB.Items.Add(selectFile);
            }else
            {

            }
        }
        /*Добавить в список ГОТОВНОСТЬ все из списка Доступные файлы*/
        private void AddAll_B_Click(object sender, EventArgs e)
        {
            Load_GB.Hide();
            ReadyLoad_LB.ClearSelected();
            if (TimerOfProcessor.Enabled == false)
            {
                Log.Clear();
                ToLog("Что бы удалить файл из списка ГОТОВНОСТЬ выберите его.", Color.DeepSkyBlue);
                if (ReadyLoad_LB.Items.Count != 0)
                {
                    QueueOfReady_LB.Items.AddRange(ReadyLoad_LB.Items);
                    Go_B.Enabled = true;
                }
                else
                {
                    Log.Clear();
                    ToLog("Список доступных файлов пуст. Загрузите хоть один файл, моделирующий процесс.", Color.Red);

                }
            }
        }
        /*Кнопка удаления файла из списка доступных файлов*/
        private void DelReadyLoad_B_Click(object sender, EventArgs e)
        {
            int index = ReadyLoad_LB.SelectedIndex;
            ReadyLoad_LB.Items.RemoveAt(index);
            Load_GB.Hide();
        }
        /*ПКМ - скрыть описание загруженного файла*/
        private void FileShow_LB_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        ReadyLoad_LB.ClearSelected();
                        Load_GB.Hide();
                        break;
                    }
            }
        }
        /*События, при выборе файла в списке доступных файлов*/
        private void ReadyLoad_LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            FileShow_LB.Items.Clear();
            QueueOfReady_LB.ClearSelected();
            if (ReadyLoad_LB.SelectedIndex != -1)
            {
                Load_GB.Show();
                TBootFile bootFile = ReadyLoad_LB.SelectedItem as TBootFile;
                foreach (string comandfile in bootFile.comandsOfFile)
                {
                    FileShow_LB.Items.Add(comandfile);
                }
                if(TimerOfProcessor.Enabled == false)
                {
                    ToLog("Нажмите Добавить, чтобы отправить выбранный файл в очередь ГОТОВНОСТЬ.\r\n", Color.DeepSkyBlue);
                    ToLog("       -- клик ПКМ по области описание файла, скрывает его.", Color.DeepSkyBlue);
                    Log.Clear();
                }
            }
        }
        /*События, при выборе файла в очерди ГОТОВНОСТЬ*/
        private void QueueOfReady_LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.Clear();
            Load_GB.Hide();
            ReadyLoad_LB.ClearSelected();
            ToLog("Что бы удалить выбранный файл, выюерите его и кликните ПКМ по области ГОТОВНОСТЬ", Color.DeepSkyBlue);
        }
        /*ПКМ - удалить файл из списка ГОТОВНОСТЬ*/
        private void QueueOfReady_LB_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        if (QueueOfReady_LB.SelectedIndex != -1)
                        {
                            int index = QueueOfReady_LB.SelectedIndex;
                            QueueOfReady_LB.Items.RemoveAt(index);
                        }
                        break;
                    }
            }
        }
        #endregion

        #region Запуск и остановка работы модели
                /*Запуск модели*/
        private void Go_B_Click(object sender, EventArgs e)
        {
            Load_GB.Hide();
            ReadyLoad_LB.ClearSelected();
            Log.Clear();
            //TBootFile bf = new TBootFile();
            //bf.nameOfFile = "процесс2";
            //bf.comandsOfFile.Add("ПАМЯТЬ-1000");
            //bf.comandsOfFile.Add("ПРОЦЕССОР-3");
            //bf.comandsOfFile.Add("ПРОЦЕССОР-2");
            //bf.comandsOfFile.Add("КОНЕЦ");
            //TProcess prss = CreateProcess(bf);
            //queueOfReady.Add(prss);
            //TimerOfProcessor.Start();            
            if (QueueOfReady_LB.Items.Count > 0)
            {
                ///Processor_TC.TabPages.Clear();//Очистка вкладок                
                /*Заполнение главного списка процессов из списка ГОТОВНОСТЬ*/
                for (int i = 0; i < QueueOfReady_LB.Items.Count; i++)
                {
                    TBootFile bootFile = QueueOfReady_LB.Items[i] as TBootFile;                    
                    TProcess process = CreateProcess(bootFile);
                    queueOfReady.Add(process);
                    /* Для рендера ЛОГА  при создании процесса*/
                    ToLog("Создан процесс "); ToLog(process.ToString(), Color.Lime);
                    ToLog(" и добавлен в очерель ГОТОВНОСТЬ.", Color.White);                    
                    ToLog("Занято ");
                    ToLog(process.descriptor.memory.ToString() + " байт", Color.Lime);
                    ToLog(" процессом ", Color.White);
                    ToLog(process.descriptor.name, Color.Lime);
                }                
                /*Отрисовка вкладок ПРОЦЕССОР*/
                /*int j = 0;
                foreach (TProcess str in queueOfReady)
                {
                    DrawPageProc(queueOfReady[j++]);
                }*/
                ToLog("Очередь ГОТОВНОСТЬ сформирована.");                
                ToLog("Планировщик процессов - запущен.");                
                TimerOfProcessor.Start();
                Go_B.Enabled = false;
            }
            else
            {
                ToLog("Добавте сперва процесс в очередь ГОТОВНОСТЬ", Color.Red);
            }
        }
        /*Остановка работы модели*/
        private void button1_Click(object sender, EventArgs e)
        {
            if (TimerOfProcessor.Enabled)
            {
                button1.Text = "Пауза";
            }
            else
                button1.Text = "Остановить";
            TimerOfProcessor.Enabled = !TimerOfProcessor.Enabled;
        }
        #endregion
        
        #region Рендеринг
        /* Метод для отрисовки новой вкладки ПРОЦЕССОР*/
        void DrawPageProc(TProcess process)
        {
            TabPage newTabPage = new TabPage();
            newTabPage.Name = "TabPage" + process.descriptor.PID;
            newTabPage.Text = process.descriptor.name;
            newTabPage.BackColor = SystemColors.Control;
            newTabPage.BorderStyle = BorderStyle.Fixed3D;
            newTabPage.ImeMode = ImeMode.NoControl;
            newTabPage.Location = new Point(4, 22);
            newTabPage.Size = new Size(150, 180);
            Processor_TC.TabPages.Add(newTabPage);
            Label PID = new Label();
            PID.Name = "PID_L" + process.descriptor.PID;
            PID.Text = "PID: " + process.descriptor.PID;
            Label state = new Label();
            state.Name = "State_L" + process.descriptor.PID;
            state.Text = "Состояние: " + process.descriptor.state.GetDescription();
            Label kvant = new Label();
            kvant.Name = "Kvant_L" + process.descriptor.PID;
            kvant.Text = "Квант: " + process.descriptor.kvant;
            Label memory = new Label();
            memory.Name = "Memory_L" + process.descriptor.PID;
            memory.Text = "Память: " + process.descriptor.memory;
            ListBox ProcessLB = new ListBox();
            ProcessLB.FormattingEnabled = true;
            ProcessLB.Name = "Process_LB" + process.descriptor.PID;
            ProcessLB.SelectionMode = SelectionMode.One;
            ProcessLB.Size = new Size(140, 108);
            ProcessLB.DataSource = process.strFileCommands;
            Control[] MassComponnents = new Control[] { PID, state, kvant, memory, ProcessLB };
            for (var i = 0; i < MassComponnents.Length; i++)
            {
                MassComponnents[i].Location = new Point(3, 5 + (15 * i));
                MassComponnents[i].AutoSize = true;
                newTabPage.Controls.Add(MassComponnents[i]);
            }            
        }
        void DrawPageIO(TProcess process)
        {
            TabPage newTabPage = new TabPage();
            newTabPage.Name = "TabPageIO" + process.descriptor.PID;
            newTabPage.Text = process.descriptor.name;
            newTabPage.BackColor = SystemColors.Control;
            newTabPage.BorderStyle = BorderStyle.Fixed3D;
            newTabPage.ImeMode = ImeMode.NoControl;            
            newTabPage.Size = new Size(150, 180);            
            IO_TC.TabPages.Add(newTabPage);
            Label PID = new Label();
            PID.Name = "PIDio_L" + process.descriptor.PID;
            PID.Text = "PIDio: " + process.descriptor.PID;
            Label state = new Label();
            state.Name = "StateIO_L" + process.descriptor.PID;
            state.Text = "Состояние: " + process.descriptor.state.GetDescription();
            Label kvant = new Label();
            kvant.Name = "KvantIO_L" + process.descriptor.PID;
            kvant.Text = "Квант: " + process.descriptor.kvant;
            Label memory = new Label();
            memory.Name = "MemoryIO_L" + process.descriptor.PID;
            memory.Text = "Память: " + process.descriptor.memory;
            ListBox IO_LB = new ListBox();
            IO_LB.FormattingEnabled = true;
            IO_LB.Name = "IO_LB" + process.descriptor.PID;
            IO_LB.SelectionMode = SelectionMode.One;
            IO_LB.Size = new Size(140, 108);
            IO_LB.DataSource = process.strFileCommands;
            Control[] MassComponnents = new Control[] { PID, state, kvant, memory, IO_LB };
            for (var i = 0; i < MassComponnents.Length; i++)
            {
                MassComponnents[i].Location = new Point(3, 5 + (15 * i));
                MassComponnents[i].AutoSize = true;
                newTabPage.Controls.Add(MassComponnents[i]);
            }
        }
        /* Рендер 1 тика процессора */
        void ProcessorTick()
        {
            /*Для рендера ПРОЦЕССОР */
            GoingP_L.Text = "Выполнено: " + processor.run.context.currentRun +
                                "  из " + processor.run.context.countRun.ToString();
            /* Для рендера ЛОГА */
            ToLog(""); ToLog(processor.run.ToString(), Color.Lime);
            ToLog(",  после выполнения команды  ", Color.White); ToLog("ПРОЦЕСОР-" + processor.run.context.countRun.ToString(), Color.Lime);
            ToLog(",  перешел в ГОТОВНОСТЬ.", Color.White);
            /* Для рендора ПРОЦЕСС */
            TabPage tabPage = Processor_TC.Controls["TabPage" + processor.run.descriptor.PID] as TabPage;
            ListBox listBox = tabPage.Controls["Process_LB" + processor.run.descriptor.PID] as ListBox;
            //listBox.SetSelected(processor.run.context.currentLine + 1, true);
            listBox.ClearSelected();
            Label label = tabPage.Controls["State_L" + processor.run.descriptor.PID] as Label;
            label.Text = "Состояние: удаляется."; //+ processor.run.descriptor.state.GetDescription();            
        }        
        void EndTick()
        {
            /* для рендера ПРОЦЕССОР */
            StateP_L.Text = "Состояние: " + processor.stateProcessor.GetDescription();
            GoingP_L.Text = "Выполнено: - " + " из -";
            /* для рендера ЛОГИ */
            ToLog(""); ToLog(processor.run.descriptor.name, Color.Orange);
            ToLog(",  после выполнения команды  ", Color.White); ToLog("КОНЕЦ", Color.Lime);            
            ToLog(", был уничтожен.", Color.White);
            /*для рендера ПРОЦЕСС*/
            //listBox.ClearSelected();
            TabPage tabPage = Processor_TC.Controls["TabPage" + processor.run.descriptor.PID] as TabPage;
            ListBox listBox = tabPage.Controls["Process_LB" + processor.run.descriptor.PID] as ListBox;
            listBox.SetSelected(processor.run.context.currentLine, true);
            Processor_TC.TabPages.Clear();
        }
        void MoveToRun()
        {
            Processor_TC.TabPages.Clear();
            DrawPageProc(processor.run);        // отрисовка вкладки
            Processor_TC.Refresh();
            TabPage tabPage = Processor_TC.Controls["TabPage" + processor.run.descriptor.PID] as TabPage;            
            /* Рендер ЛОГА */
            ToLog(""); ToLog(processor.run.ToString(), Color.Lime);
            ToLog(" добавлен в ВЫПОЛНЕНИЕ (квант=", Color.White);
            ToLog(processor.run.descriptor.kvant.ToString(), Color.Lime);
            ToLog(")", Color.White);            
            StateP_L.Text = "Состояние: " + processor.stateProcessor.GetDescription();
            /* Рендер ПРОЦЕССА*/
            Label label = tabPage.Controls["State_L" + processor.run.descriptor.PID] as Label;
            ListBox listBox = tabPage.Controls["Process_LB" + processor.run.descriptor.PID] as ListBox;
            label.Text = "Состояние: " + processor.run.descriptor.state.GetDescription();            
            listBox.SetSelected(processor.run.context.currentLine, true);
            
            //Processor_TC.Refresh();
            //groupBox4.Refresh();
        }
        void MoveToIO()
        {
            IO_TC.TabPages.Clear();
            Processor_TC.TabPages.Clear();
            DrawPageIO(inOut.ioRun);        // отрисовка вкладки
            IO_TC.Refresh();//<---
            Processor_TC.Refresh();//<----
            TabPage tabPage = IO_TC.Controls["TabPageIO" + inOut.ioRun.descriptor.PID] as TabPage;            
            StateIO_L.Text = "Состояние: " + inOut.stateIO.GetDescription();
            /* Рендер ПРОЦЕССА*/
            Label label = tabPage.Controls["StateIO_L" + inOut.ioRun.descriptor.PID] as Label;
            ListBox listBox = tabPage.Controls["IO_LB" + inOut.ioRun.descriptor.PID] as ListBox;
            label.Text = "Состояние: " + inOut.ioRun.descriptor.state.GetDescription();
            //listBox.SetSelected(inOut.ioRun.context.currentLine, true);
        }
        #endregion

        #region Процессор
        /* Создание процесса */
        TProcess CreateProcess(TBootFile bootFile)
        {
            TProcess process = new TProcess();
            /*Список команд загрузочного файла - кодовый сегмент*/
            process.strFileCommands = bootFile.comandsOfFile;
            /*Заполнение дескриптора процесса*/
            process.descriptor.name = bootFile.nameOfFile;
            process.descriptor.PIDcount();
            process.descriptor.kvant = 1;
            process.descriptor.state = TStateProcess.spREADY;
            process.descriptor.memory = process.GetCountRun(0);
            /*Настроить содержимое контекста нового процесса (инициализация начальными параметрами)*/
            process.context.currentLine = 0;
            process.context.command = TCommand.cMEMORY;
            process.context.currentRun = 0;
            process.context.countRun = process.GetCountRun(0);
            return process;
        }
        void RefreshContext()
        {
            int line = ++processor.run.context.currentLine;
            if (processor.run.strFileCommands.Count > 1)
            {
                processor.run.context.command = processor.run.GetCommand(line);
                processor.run.context.countRun = processor.run.GetCountRun(line);
            }
            else
            {
                TimerOfProcessor.Stop();
                ToLog(""); ToLog("Минимум 2 команды необходимо в загрузочном файле: ПАМЯТЬ, КОНЕЦ", Color.Red);
            }            
        }
        void RefreshReady()
        {
            QueueOfReady_LB.Items.Clear();
            foreach (TProcess process in queueOfReady)
            {
                QueueOfReady_LB.Items.Add(process);
            }
        }        
    /* ПРОЦЕССОР - обработчик текущего процесса за каждый тик*/
    private void TimerOfProcessor_Tick(object sender, EventArgs e)
        {
            ++processor.countTick;
            if (processor.stateProcessor == TStateProcessor.sprBUSY) // процессор работает
            {
                if(processor.run.descriptor.state == TStateProcess.spRUN)// обрабатываем команды работающего процесса
                {
                    again:
                    /*Обработчик команд текущего процесса*/
                    switch (processor.run.context.command)
                    {
                        case TCommand.cMEMORY:
                            RefreshContext();// переход на следующую команду
                            TabPage tabPage = Processor_TC.Controls["TabPage" + processor.run.descriptor.PID] as TabPage;
                            ListBox listBox = tabPage.Controls["Process_LB" + processor.run.descriptor.PID] as ListBox;
                            listBox.SetSelected(processor.run.context.currentLine, true);
                            goto again;
                        case TCommand.cPROCESSOR:                            
                            if ((int)processor.run.context.currentRun < (int)processor.run.context.countRun)     // выполнять, пока не истечет время команды ПРОЦЕССОР
                            {// рендер каждого тика
                                GoingP_L.Text = "Выполнено: " + (processor.run.context.currentRun++) +
                                                " из " + processor.run.context.countRun.ToString();                               
                            }
                            else // если команда ПРОЦЕССОР отработала
                            {
                                processor.run.descriptor.state = TStateProcess.spREADY; // пометка, для отправки процесса менеджером в ГОТОВНОСТЬ 
                                ProcessorTick();  // отрисовка результат работы команды ПРОЦЕССОР после выхода из таймера                                                  
                                processor.run.context.currentRun = 0; // обнуление счетчика                                
                                RefreshContext(); // переход на новую команду                                
                            }
                            break;
                        case TCommand.cIO:
                            processor.run.descriptor.state = TStateProcess.spWAIT;
                            queueOfWait.Add(processor.run);
                            RefreshQueueOfWait();
                            QueueOfWait_LB.Refresh();
                            /* Рендер ЛОГА */
                            ToLog(""); ToLog(processor.run.ToString(), Color.Lime);
                            ToLog(" добавлен в ОЖИДАНИЕ (квант=", Color.White);
                            ToLog(processor.run.descriptor.kvant.ToString(), Color.Lime);
                            ToLog(")", Color.White);
                            Log.Refresh();
                            processor.run = null;
                            processor.stateProcessor = TStateProcessor.sprEMPTY;
                            if(queueOfWait.Count > 0)
                            {
                                TimerOfIO.Enabled = true;
                            }
                            break;
                        case TCommand.cEND:
                            processor.stateProcessor = TStateProcessor.sprEMPTY;// процессор пуст, так как текущий процесс был удален
                            EndTick();
                            processor.run = null;// удаление процесса
                            break;
                        default:
                            break;
                    }
                }else// ожидание события прерывания процесса
                {
                    ManagerOfProc();// отправка текущего процесса в ВЫПОЛНЕНИЕ
                }                 
            }
            else // если процессор свободен
            {
                ManagerOfProc();         // инициализация текущего процесса и процесс добавлен на ВЫПОЛНЕНИЕ              
            }            
        }

        #endregion

        #region Операция ВВОД/ВЫВОД

        void RefreshContextIO()
        {
            int line = ++inOut.ioRun.context.currentLine;
            if (inOut.ioRun.strFileCommands.Count > 1)
            {
                inOut.ioRun.context.command = inOut.ioRun.GetCommand(line);
                inOut.ioRun.context.countRun = inOut.ioRun.GetCountRun(line);
            }
            else
            {
                TimerOfIO.Stop();
                ToLog(""); ToLog("Минимум 2 команды необходимо в загрузочном файле: ПАМЯТЬ, КОНЕЦ", Color.Red);
            }
        }
        void RefreshQueueOfWait()
        {
            QueueOfWait_LB.Items.Clear();
            foreach (TProcess process in queueOfWait)
            {
                QueueOfWait_LB.Items.Add(process);
            }
        }
        /*Рендеринг IO*/
        private void TimerOfIO_Tick(object sender, EventArgs e)
        {
            ++inOut.countTick;
            if(inOut.stateIO == TStateIO.sprBUSY)
            {
                if(inOut.ioRun != null)
                {
                    if ((int)inOut.ioRun.context.currentRun < (int)inOut.ioRun.context.countRun)     // выполнять, пока не истечет время команды ПРОЦЕССОР
                    {// рендер каждого тика
                        IO_L.Text = "Выполнено: " + (inOut.ioRun.context.currentRun++) +
                                        " из " + inOut.ioRun.context.countRun.ToString();
                    }
                    else // если команда ВВОД/ВЫВОД отработала
                    {
                        TabPage tabPage = IO_TC.Controls["TabPageIO" + inOut.ioRun.descriptor.PID] as TabPage;
                        ListBox listBox = tabPage.Controls["IO_LB" + inOut.ioRun.descriptor.PID] as ListBox;
                        listBox.ClearSelected();
                        Label label = tabPage.Controls["StateIO_L" + inOut.ioRun.descriptor.PID] as Label;
                        label.Text = "Состояние: УДАЛЯЕТСЯ.";
                        IO_L.Text = "Выполнено: " + (inOut.ioRun.context.currentRun++) +
                                        " из " + inOut.ioRun.context.countRun.ToString();
                        inOut.ioRun.descriptor.state = TStateProcess.spREADY; // пометка, для отправки процесса менеджером в ГОТОВНОСТЬ
                        inOut.stateIO = TStateIO.sprEMPTY;
                        inOut.ioRun.context.currentRun = 0;
                        /* Рендер ЛОГА */
                        ToLog(""); ToLog(inOut.ioRun.ToString(), Color.Lime);
                        ToLog(",  после выполнения команды  ", Color.White); ToLog("ВВОД\\ВЫВОД-" + inOut.ioRun.context.countRun.ToString(), Color.Lime);
                        ToLog(",  перешел в ГОТОВНОСТЬ.", Color.White);
                        StateIO_L.Text = "Состояние: " + inOut.stateIO.GetDescription();
                        RefreshContextIO();                        
                        List<TProcess> temp = new List<TProcess>();
                        temp.Add(inOut.ioRun);
                        temp.AddRange(queueOfReady);
                        queueOfReady.Clear();
                        queueOfReady = temp;
                        RefreshReady(); // текущее состояние ГОТОВНОСТЬ
                        inOut.ioRun = null;
                        IO_TC.TabPages.Clear();
                    }
                }                
            }
            else
            {
                /* для рендера ПРОЦЕССОР */
                StateIO_L.Text = "Состояние: " + inOut.stateIO.GetDescription();
                IO_L.Text = "Выполнено: - " + " из -";
                ManagerOfIO();
            }            
        }
        #endregion

        #region Планирование процессов
        /*Диспетчер процессов FIFO*/
        void ManagerOfProc()
        {            
            if(processor.stateProcessor == TStateProcessor.sprBUSY )
            {
                /*Если пришел с ВЫПОЛНЕНИЕ, то отправляется в ГОТОВНОСТЬ по условию FIFO*/
                if (processor.run.descriptor.state == TStateProcess.spREADY)// ожидаемое событие - окончание работы команды процесса
                {
                    List<TProcess> temp = new List<TProcess>();
                    temp.Add(processor.run);                    
                    temp.AddRange(queueOfReady);
                    queueOfReady.Clear();
                    queueOfReady = temp;
                    RefreshReady(); // текущее состояние ГОТОВНОСТЬ
                    Processor_TC.TabPages.Clear(); //визуализация удаления процесса  - ПРОЦЕССОР                    
                    Processor_TC.Refresh();
                    processor.stateProcessor = TStateProcessor.sprEMPTY; // процессор пуст, так в нем нет процесса
                    GoingP_L.Text = "Выполнено: - " + " из -";                    
                    processor.run = null;
                }
            }
            else/* процесс С ГОТОВНОСТЬ на ВЫПОЛНЕНИЕ - когда процессор свободен */
            {
                if (queueOfReady.Count > 0)
                {
                    processor.run = queueOfReady[0];    // инициализируем текущий
                    queueOfReady.RemoveAt(0);           // процесс
                    RefreshReady();                     // перерисовка ГОТОВНОСТЬ
                    processor.stateProcessor = TStateProcessor.sprBUSY; // переключене процессора в рабочий режим
                    processor.run.descriptor.state = TStateProcess.spRUN; // запуск процесса
                    MoveToRun(); //рендер отправки процесса на ВЫПОЛНЕНИЕ
                }
                else
                {
                    if (!TimerOfIO.Enabled)
                    {
                        TimerOfProcessor.Stop();
                        ToLog(""); ToLog("Все процессы отработали", Color.Red);
                        Processor_TC.TabPages.Clear();
                    }
                }                
            }
        }
        void ManagerOfIO()
        {
            if(inOut.stateIO == TStateIO.sprEMPTY)
            {
                if (queueOfWait.Count > 0)
                {
                    inOut.ioRun = queueOfWait[0];
                    queueOfWait.RemoveAt(0);
                    RefreshQueueOfWait();
                    QueueOfWait_LB.Refresh();
                    inOut.stateIO = TStateIO.sprBUSY;
                    inOut.ioRun.descriptor.state = TStateProcess.spRUN;
                    MoveToIO();
                }                
            }else
            {
                TimerOfIO.Stop();
                ToLog(""); ToLog("Очередь ожидания пуста. ", Color.Orange);
                Processor_TC.TabPages.Clear();
            }
        }
        #endregion
       
        #region Прочее
        /*Добавление в лог новой строки*/
        void ToLog(string output) //Функция добавления новых строк в лог
        {
            if (!string.IsNullOrWhiteSpace(Log.Text)) //Если лог непустой то новая строка
            {
                Log.AppendText("\r\n" + output);
            }
            else
            {
                Log.AppendText(output); //Если лог пуст то пишем в первой строке
            }
            Log.ScrollToCaret(); //Прокручиваем лог
        }
        /*Добавление в лог цветной строки*/
        void ToLog(string output, Color color)
        {
            Log.AppendText(output, color);            
        }
        #endregion
    }
    static class Extantion
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {

            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;

        }
        public static string GetDescription(this Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }        
    }
}
