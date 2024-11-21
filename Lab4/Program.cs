using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Lab4;


class Program
{
    static void Main(string[] args)
    {
        // Задание 1
        List<int> task1 = new List<int>()
        {
            11,12,12,1,2,3,4,5,6,7,8,9,10,11
        };

        Console.WriteLine("Исходный список для задания 1");
        foreach (int i in task1)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        Console.WriteLine("Введите число");
        try
        {
            int e = Convert.ToInt32(Console.ReadLine());

            Spiski.TaskFirst(task1, e);

            Console.WriteLine($"Список с удаленным элементом после {e}");

            foreach (int i in task1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine() ;   
        }
        catch (Exception)
        {
           Console.WriteLine("Ошибочка вышла");
            throw;
        }

        //Задание 2
        LinkedList<int> task2 = new LinkedList<int>();
        task2.AddLast(11);
        task2.AddLast(12);
        task2.AddLast(12);
        task2.AddLast(1);
        task2.AddLast(2);
        task2.AddLast(3);
        task2.AddLast(4);
        task2.AddLast(5);
        task2.AddLast(6);
        task2.AddLast(7);
        task2.AddLast(8);
        task2.AddLast(9);
        task2.AddLast(10);
        task2.AddLast(11);

        Console.WriteLine("Исходный список для задания 2");
        foreach (int item in task2)
        {
            Console.Write(item + " ");
        }

        try
        {
            Console.WriteLine("Результат решения 2 задания");
            Spiski.TaskSecond(task2);
        }
        catch (Exception)
        {
            Console.WriteLine("Ошибочка вышла");
            throw;
        }

        // Задание 3
        HashSet<string> countries = new HashSet<string>()
        { "Абхазия", "Венгрия", "Польша", "Гондурас", "Литва", "Монако", "Таиланд", "Япония" };

        HashSet<string> turist1 = new HashSet<string>()
        { "Абхазия", "Венгрия", "Монако", "Таиланд" };

        HashSet<string> turist2 = new HashSet<string>()
        {
            "Абхазия","Венгрия","Гондурас", "Литва", "Таиланд" };

        HashSet<string> turist3 = new HashSet<string>()
        { "Абхазия", "Венгрия", "Польша", "Таиланд" };

        Console.WriteLine("РЕзультаты выполнения 3 задания");
        try
        {
            Spiski.TaskThird(countries, turist1, turist2, turist3);
        }
        catch (Exception)
        {
            Console.WriteLine("Ошибочка вышла");
            throw;
        }

        //Задание 4
        Spiski.TaskFourth();

        //Задание 5
        Spiski.TaskFifth();

    }
}
