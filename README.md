# task-planer-for-Os
Планировщик зада для операционной системы
# Исполняемый файл
Исполняемый файл лежит в папке compiled. ВЫполненно задание "Моделирование алгоритмов управления процессами в операционных системах"
Задание на лабораторную работу
Задание

Разработать программу, моделирующую один из алгоритмов управления процессами в соответствии с вариантом задания. При моделировании считать что:

    однопроцессорная вычислительная система разделения времени;

    общий объём памяти вычислительной системы составляет 64К;

    ввод-вывод является разделяемым ресурсом, не допускающий одновременного использования несколькими процессами и требующий решения проблемы синхронизации процессов;

    поступаемые на выполнение задачи содержатся в файлах, моделирующих процессы (формат приведён ниже);

    вытеснение процесса из процессора приводит к сохранению информации о его состоянии в контексте процесса, который восстанавливается при последующем поступлении процесса в процессор;

    вытеснение процесса из процессора также приводит к освобождению занимаемой им памяти;

    пользователь может «загружать» в моделируемую систему новые задачи, порождающие процессы;

    Предусмотреть возможность интерактивного слежения за состоянием вычислительной системы в процессе выполнения задач (состояние очередей «ОЖИДАНИЕ» и «ГОТОВНОСТЬ», приоритеты процессов, требования к ресурсам, состояние ПРОЦЕССОРА, состояние ВВОДА\ВЫВОДА,  состояние ПАМЯТИ и т.д.).

    Формат файла, моделирующего задачу:

    ПАМЯТЬ-1000

    ПРОЦЕССОР-10

    ВВОД\ВЫВОД-20

    ПРОЦЕССОР-12

    ...

    ПРОЦЕССОР-5

    ВВОД\ВЫВОД-25

    КОНЕЦ

    Где первая строка «ПАМЯТЬ» – определяет требования процесса на объём (в байтах) доступной памяти (если такового в системе на данный момент нет, то процесс поступает в очередь готовых). Остальные строки представляют собой поэтапное требование процесса в процессорном времени «ПРОЦЕССОР» (в секундах или квантах процессорного времени, в зависимости от реализуемого варианта задания) и во времени ввода-вывода «ВВОД-ВЫВОД» (в секундах). Командой окончания выполнения программы является команда «КОНЕЦ».
# ВАРИАНТ
Управление процессами на основе бесприоритетного квантования. Очередь готовых процессов – FIFO. Синхронизация процессов с использованием аппарата событий.
