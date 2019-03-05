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
            * то выполнение действий по чтению файла
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
            Log.Clear();
            TBootFile selectFile = ReadyLoad_LB.SelectedItem as TBootFile;
            //добавление процесса, выбранного в списке доступных, в очередь ГОТОВНОСТЬ
            QueueOfReady_LB.Items.Add(selectFile);            
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
            Log.Clear();
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
                ToLog("Нажмите Добавить, чтобы отправить выбранный файл в очередь ГОТОВНОСТЬ.\r\n", Color.DeepSkyBlue);
                ToLog("       -- клик ПКМ по области описание файла, скрывает его.", Color.DeepSkyBlue);
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
        /*Добавить в список ГОТОВНОСТЬ все из списка Доступные файлы*/
        private void AddAll_B_Click(object sender, EventArgs e)
        {
            Load_GB.Hide();
            ReadyLoad_LB.ClearSelected();
            Log.Clear();
            ToLog("Что бы удалить файл из списка ГОТОВНОСТЬ выберите его.", Color.DeepSkyBlue);
            if (ReadyLoad_LB.Items.Count != 0)
            {
                QueueOfReady_LB.Items.AddRange(ReadyLoad_LB.Items);
            }
            else
            {
                Log.Clear();
                ToLog("Список доступных файлов пуст. Загрузите хоть один файл, моделирующий процесс.", Color.Red);
                
            }
        }       

        /*Запуск модели*/
        private void Go_B_Click(object sender, EventArgs e)
        {
            Load_GB.Hide();
            ReadyLoad_LB.ClearSelected();
            Log.Clear();
            TBootFile bf = new TBootFile();
            bf.nameOfFile = "процесс2";
            bf.comandsOfFile.Add("ПАМЯТЬ-1000");
            bf.comandsOfFile.Add("ПРОЦЕССОР-3");
            bf.comandsOfFile.Add("ПРОЦЕССОР-2");
            bf.comandsOfFile.Add("КОНЕЦ");
            TProcess prss = CreateProcess(bf);
            queueOfReady.Add(prss);
            ProcessorRun();
            if (QueueOfReady_LB.Items.Count > 0)
            {
                ///Processor_TC.TabPages.Clear();//Очистка вкладок                
                /*Заполнение главного списка процессов из списка ГОТОВНОСТЬ*/
                for (int i = 0; i < QueueOfReady_LB.Items.Count; i++)
                {
                    TBootFile bootFile = QueueOfReady_LB.Items[i] as TBootFile;
                    
                    TProcess process = CreateProcess(bootFile);
                    queueOfReady.Add(process);
                    ToLog(""); ToLog(process.ToString(), Color.Lime);
                    ToLog(" добавлен в очерель ГОТОВНОСТЬ.", Color.White);
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
            }
            //else
            //{
            //    ToLog("Добавте сперва процесс в очередь ГОТОВНОСТЬ", Color.Red);
            //}
        }
        /*Остановка работы модели*/
        private void button1_Click(object sender, EventArgs e)
        {
                
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
            ProcessLB.SelectionMode = SelectionMode.None;
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
        /*Заполнение данными вкладки Процесс*/
        void AddData()
        {
            TProcess process = processor.run;
            TabPage currentTabPage = Processor_TC.Controls["TabPage" + process.descriptor.PID] as TabPage;
            Label currentPID = currentTabPage.Controls["PID_L" + process.descriptor.PID] as Label;
             
        }
        /* Рендер 1 тика процессора */
        void Tick(TProcess process)
        {            
            int currentRUN = process.context.currentRun;    // обращение к начальному  
            int countRUN = process.context.countRun;        // состоянию команды процесса 
            if (currentRUN < countRUN)     // выполнять, пока не истечет время команды ПРОЦЕССОР
            {
                GoingP_L.Text = "Выполнено: " + currentRUN +
                                " из " + countRUN.ToString();
                ++process.context.currentRun; // имитация работы процесса - изменение состояния команды каждый тик
            }
            else // если команда ПРОЦЕССОР отработала, выполняется прерывание
            {
                GoingP_L.Text = "Выполнено: " + currentRUN +
                                " из " + countRUN.ToString();
                ToLog(""); ToLog(processor.run.ToString(), Color.Lime);
                ToLog("  после команды  ", Color.White); ToLog("ПРОЦЕСОР-" + processor.run.context.countRun.ToString(), Color.Lime);
                ToLog("  перешел в ГОТОВНОСТЬ.", Color.White);
                processor.run.descriptor.state = TStateProcess.spREADY; // преключение процесса в готовность
                process.context.currentRun = 0; // обнуление текущего состояния 
                process.context.countRun = 0;   // команды процесса
                RefreshCommand();
            }            
        }
        #endregion
        #region Процессор
        void ProcessorRun()// диспетчер команд
        {
            /*Выбор текущего процесса - processor.run */
            if (processor.stateProcessor == TStateProcessor.sprEMPTY)
            {
                ManagerOfProc();         // инициализация текущего процесса
            }
            else // если процессор занят работой
            {
                if (processor.run.descriptor.state == TStateProcess.spRUN)// обрабатываем процесс
                {
                    
                }
                else //отправляет отработанный процесс в ГОТОВНОСТЬ
                {
                    ManagerOfProc();
                }
            }
        }
        void RefreshCommand()
        {
            int line = ++processor.run.context.currentLine;
            processor.run.context.command = processor.run.GetCommand(line);
            processor.run.context.countRun = processor.run.GetCountRun(line);
        }
        /* ПРОЦЕССОР - обработчик текущего процесса за каждый тик*/
        private void TimerOfProcessor_Tick(object sender, EventArgs e)
        {
            ++processor.countTick;
            /*Обработчик команд*/
            switch (processor.run.context.command)
            {
                case TCommand.cMEMORY:
                    ToLog(""); ToLog(processor.run.ToString(), Color.Lime);
                    ToLog("  занял  ", Color.White); ToLog(processor.run.context.countRun.ToString(), Color.Lime);
                    ToLog("  памяти.", Color.White);
                    RefreshCommand();
                    break;
                case TCommand.cPROCESSOR:
                    processor.run.descriptor.state = TStateProcess.spRUN; // процесс отправляется на ВЫПОЛНЕНИЕ
                    Tick(processor.run);
                    break;
                case TCommand.cIO:

                    break;
                case TCommand.cEND:
                    TimerOfProcessor.Stop(); // системная команда - прерывание, что останавливает процессор
                                             // что бы запустить его заново 
                    ToLog(""); ToLog(processor.run.ToString(), Color.Lime);
                    ToLog("  завершился.", Color.White);
                    break;
                default:
                    break;
            }
        }

        #endregion
        /*Диспетчер процессов FIFO*/
        void ManagerOfProc()
        {
            /* процесс С ГОТОВНОСТЬ на ВЫПОЛНЕНИЕ - когда процессор свободен */
            if(processor.stateProcessor == TStateProcessor.sprEMPTY
                && queueOfReady.Count > 0 )
            {
                processor.run = queueOfReady[0];   // инициализируем текущий
                queueOfReady.RemoveAt(0);           // процесс                
                processor.stateProcessor = TStateProcessor.sprBUSY; // переключене процессора в рабочий режим
                DrawPageProc(processor.run);                
                StateP_L.Text = "Состояние: " + TStateProcessor.sprBUSY.GetDescription();
                TimerOfProcessor.Start();
            }
            else
            {
                /*Если пришел с ВЫПОЛНЕНИЕ, то отправляется в ГОТОВНОСТЬ по условию FIFO*/
                if(processor.run.descriptor.state == TStateProcess.spREADY)
                {
                    List<TProcess> temp = new List<TProcess>();
                    temp.Add(processor.run);
                    temp.AddRange(queueOfReady);
                    queueOfReady.Clear();
                    queueOfReady = temp;
                    QueueOfReady_LB.Items.Clear();
                    foreach (TProcess process in queueOfReady)
                    {
                        QueueOfReady_LB.Items.Add(process);
                    }
                }
            }
        }
        
        /*Рендеринг IO*/
        private void TimerOfIO_Tick(object sender, EventArgs e)
        {
            /*currentProcess.descriptor.state = TStateProcess.spWAIT;
            int startRun = currentProcess.startRun;
            int lineTime = currentProcess.currentLine;
            int allTime = currentProcess.fileCommands[lineTime].countRun;
            if (startRun <= allTime)
            {
                IO_L.Text = "Выполнено: " + startRun++ + " из " + allTime.ToString();
                currentProcess.startRun++;
            }
            else
            {
                TimerOfIO.Stop();
                currentProcess.currentLine++;
                currentProcess.startRun = 0;
                ModuleOfCommandManage(currentProcess);
            }*/
        }

        /* Создание процесса */
        TProcess CreateProcess(TBootFile bootFile)
        {
            TProcess process = new TProcess();
            /*Список команд загрузочного файла*/
            process.strFileCommands = bootFile.comandsOfFile;
            /*Заполнение дескриптора процесса*/
            process.descriptor.name = bootFile.nameOfFile;            
            process.descriptor.PIDcount();
            process.descriptor.kvant = 2;
            process.descriptor.state = TStateProcess.spREADY;            
            process.descriptor.memory = process.GetCountRun(0);
            /*Настроить содержимое контекста нового процесса (инициализация начальными параметрами)*/            
            process.context.currentLine = 0;
            process.context.command = TCommand.cMEMORY;
            process.context.currentRun = 0;
            process.context.countRun = process.GetCountRun(0);
            return process;            
        }

        
        
        
        



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
