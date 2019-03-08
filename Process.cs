using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Task_1
{
    #region Термины
    /**
    Процесс (или задача) ­- это не сама программа, а некая абстракция, описывающая выполняющуюся в вычислительной 
           системе программу. Для ОС представляет собой единицу работы, заявку на потребление системных ресурсов.
    Загрузочный файл - загружаемый в модель файл расширения .txt и моделирующий процесс. 
           К примеру:
                ПАМЯТЬ-1000   ...команда, сколько необходимо выделить памяти для процесса (1000 байт)
                ПРОЦЕССОР-10  ... команда, определяющая количество тиков процесса (10)
                ВВОД\ВЫВОД-20 ... команда, определяющая количество тиков для операции I/O (20)
                ПРОЦЕССОР-12
                ПРОЦЕССОР-5
                ВВОД\ВЫВОД-25
                КОНЕЦ         ... команда, завершение процесса
    Дескриптор - часть информационной структуры процесса. Хранит информацию, описывающую конкретный процесс. 
           Статическая информация о процессе. Описывает чем один процесс отличается от другого.
           Служит для выбора планировщим конкретного процесса из всех доступных, его идентификацию.
    Контекст - часть информационной структуры процесса. Описывает динамическая информация о текущем (работающем) процессе.
           Служит для возобновления выполнения процесса не с начала, а в продолжении.
    Квант - процессорное время, отводимое данному процессу. Т.е. вермя, по истечении которого происходит прерывание 
           процесса. Прцесс, исчерпавший свой квант, переходит в состояние ГОТОВНОСТЬ и ожидает, когда ему 
           будет предоставлен новый квант процессорного времени.
           В рамках модели, прерывание по кванту производится не будет. Вместо этого в поле процессора Выполненно: будут 
           отбражаться периоды, заданные величиной кванта. Периоды и будут моделировать, что каждый квант происходило 
           прерывание. Таким образом, квант - это период.
           К примеру команда ПРОЦЕССОР-10 с Квант - 2:
                команда будет выполняться: 0/10, 2/10, 4/10 .. 10/10. Прерывание. Отправка процесса в ГОТОВНОСТЬ.
                Что означает по факту: ВЫПОЛНЕНИЕ 1/10, 2/10 -  прерывание -->ГОТОВНОСТЬ
                                       --> ВЫПОЛНЕНИЕ 3/10, 4/10 - прерывание --> ГОТОВНОСТЬ...
                                       --> ВЫПОЛНЕНИЕ 9/10, 10/10 - прерывание --> ГОТОВНОСТЬ
                                       --> запись контекста процесса --> отправка в ГОТОВНОСТЬ
                                       --> запуск планировщика процессов (что означает выбор очередного
                                       процесса из очереди ГОТОВНОСТЬ на основании приоритетов или FIFO/LIFO)
           
                                                
         **/

    #endregion
    class TIO
    {
        public TProcess ioRun = null;
        public TStateIO stateIO = TStateIO.sprEMPTY;
        public int countTick = 0;
    }
    class TProcessor
    {
        public TProcess run = null;
        public TStateProcessor stateProcessor = TStateProcessor.sprEMPTY;
        public int countTick = 0;
    }
    ///<summary>
    /// Представление процесса, как отдельного класса (выделение общих признаков всех процессов)
    ///</summary>
    class TProcess
    {
        public List<string> strFileCommands = new List<string>();//строковый список команд файла
        public TDescriptor descriptor; 
        public TContext context;


        //public List<TContext> fileCommands = new List<TContext>();// список команд процесса (контекст)
        
        /*метод для извлечения типа команд команды из строки*/
        public TCommand GetCommand(int commandLine)
        {
            if (strFileCommands != null)
            {
                string strLine = strFileCommands[commandLine].Trim();
                string[] mLine = strLine.Split('-').ToArray();
                if (mLine[0] == "ПАМЯТЬ")
                {
                    return TCommand.cMEMORY;
                }
                if (mLine[0] == "ПРОЦЕССОР")
                {
                    return TCommand.cPROCESSOR;
                }
                if (mLine[0] == "ВВОД\\ВЫВОД")
                {
                    return TCommand.cIO;
                }
                if (mLine[0] == "КОНЕЦ")
                {
                    return TCommand.cEND;
                }
            }
            return TCommand.cNONE;
        }
        /*метод для извлечения из строки времени работы команды*/
        public int GetCountRun(int commandLine)
        {
            if (strFileCommands != null)
            {
                string strLine = strFileCommands[commandLine].Trim();
                string[] mLine = strLine.Split('-').ToArray();
                if (mLine[0] != "КОНЕЦ")
                {
                    return Convert.ToInt32(mLine[1]);
                }
            }
            return 0;
        }
        public override string ToString() //Переопределяем метод ToString(), чтобы автоматическое преобразование
        {                                 //в строковый тип выводило в ListBox только название файла
            return descriptor.name;
        }
    }
    /* Дескриптор */
    struct TDescriptor
    {
        public string name;            // имя процесса (название файла, моделирующего процесс)
        public int PID;                // идентификатор процесса. Число, однозначно идентифи­цирующее процесс в ОС
        public byte kvant;             // квант процесса        
        public int memory;             // требуемая память для процесса
        public TStateProcess state;    // состояние процесса (ГОТОВНОСТЬ, ВЫПОЛНЕНИЕ, ОЖИДАНИЕ)
        public void PIDcount()         // функция, генерирующая идентификатор при создании нового процесса
        {
            PID = ++PID_i;
        }
        private static int PID_i = 0;  // единое начало отсчета для PID всех процессов
    }
    /* Контекст процесса */
    struct TContext
    {
        public byte currentLine; // текущая строка процесса. Команда, которую необходимо выполнить
        public TCommand command; // определяет тип команды (ПРОЦЕССОР, ВВОД/ВЫВОД и т.д.)
        public byte currentRun;  // текущее время выполнение команды
        public int countRun;     // полное время выполнения команды
    }
    /* Тип для загрузочного файла, хранящего название и список команд */
    class TBootFile
    {
        public string nameOfFile;  //Название загруженного файла
        public List<string> comandsOfFile = new List<string>(); //Список команд, загруженного файла                
        public override string ToString() //Переопределяем метод ToString(), чтобы автоматическое преобразование
        {                                 //в строковый тип выводило в ListBox только название файла
            return nameOfFile;
        }
    }
    /*
     Тип комманды
         */
    enum TCommand : byte
    {
        cNONE,          // нет ни одной команды в файле
        cMEMORY,        // ПАМЯТЬ
        cPROCESSOR,     // ПРОЦЕССОР
        cIO,            // ВВОД/ВЫВОД
        cEND            // КОНЕЦ
    }
    /* Состояние процессора */
    enum TStateProcessor : byte
    {
        [Description("ЗАНЯТ")]
        sprBUSY,        //Занят
        [Description("СВОБОДЕН")]
        sprEMPTY,       //Свободен        
    }
    /* Состояние процессора */
    enum TStateIO : byte
    {
        [Description("ЗАНЯТ")]
        sprBUSY,        //Занят
        [Description("СВОБОДЕН")]
        sprEMPTY,       //Свободен        
    }
    /*
     Состояние процесса
         */
    enum TStateProcess : byte
    {
        [Description("ГОТОВНОСТЬ")]
        spREADY,        //Готовность
        [Description("ВЫПОЛНЕНИЕ")]
        spRUN,          //Выполнение
        [Description("ОЖИДАНИЕ")]
        spWAIT          //Ожидание
    }    
}

