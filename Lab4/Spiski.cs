using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Lab4
{
    public class Spiski
    {
        //Задание 1
        public static void TaskFirst(List<int> task1, int e)
        {
            int size = task1.Count;
            for (int i=size-1; i>=0; i--)
            {
                if (task1[i] == e)
                {
                    task1.RemoveAt(i + 1);
                }

            }

        }

        //Задание 2
        public static void TaskSecond(LinkedList<int> task2)
        {
            bool flag = false;
            int counter = 0;

            if (task2.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            // Переменная для итерации по списку
            LinkedListNode<int> current = task2.First;

            // Проход по всем элементам, кроме последнего
            while (current.Next != null)
            {
                if (current.Value == current.Next.Value)
                {
                    flag = true;
                    Console.WriteLine($"Равные соседние элементы найдены на позициях {counter} и {counter + 1} (значение: {current.Value}).");
                }

                current = current.Next; // Переход к следующему элементу
                counter++;
            }

            // Проверка "последний — первый" для круговой связи
            if (task2.Last.Value == task2.First.Value)
            {
                flag = true;
                Console.WriteLine($"Равные соседние элементы найдены на позициях {counter} и 0 (значение: {task2.Last.Value}).");
            }

            // Если равных соседних элементов нет
            if (!flag)
            {
                Console.WriteLine("Список не содержит соседних равных элементов.");
            }

        }

        //Задание 3
        public static void TaskThird(HashSet<string> countries, HashSet<string> turist1, HashSet<string> turist2, HashSet<string> turist3)
        {
            var all = new HashSet<string>(countries);
            var some = new HashSet<string>();
            var nobody = new HashSet<string>(countries);

            var tourists = new List<HashSet<string>> { turist1, turist2, turist3 };


            foreach (var tuorist in tourists)
            {
                all.IntersectWith(tuorist);
                some.UnionWith(tuorist);
                
                nobody.ExceptWith(tuorist);
            }
            some.ExceptWith(all);

            Console.WriteLine("Страны, посещенные всеми туристами: " + string.Join(", ", all));
            Console.WriteLine("Страны, посещенные хотя бы одним туристом: " + string.Join(", ", some));
            Console.WriteLine("Страны, которые никто не посещал: " + string.Join(", ", nobody));
        }

        //Задание 4
        public static void TaskFourth()
        {
            HashSet<char> voicelessConsonants = new HashSet<char> { 'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч' };

            HashSet<char> usedOnce = new HashSet<char>();
            HashSet<char> usedMoreThanOnce = new HashSet<char>();

            try
            {
                string text = File.ReadAllText("text.txt").ToLower();

                string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?', '-', '—', ';', ':', '«', '»', '(', ')' });

                foreach (string word in words)
                {
                    HashSet<char> lettersInWord = new HashSet<char>(word);

                    foreach (char letter in lettersInWord)
                    {
                        if (voicelessConsonants.Contains(letter))
                        {

                            if (usedOnce.Contains(letter))
                            {
                                usedMoreThanOnce.Add(letter);
                            }
                            else
                            {
                                usedOnce.Add(letter);
                            }
                        }
                    }

                }

                var result = usedOnce.Except(usedMoreThanOnce).OrderBy(c => c);

                Console.WriteLine("Глухие согласные буквы, которые не входят ровно в одно слово:");
                foreach (char letter in result)
                {
                    Console.WriteLine(letter);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        //Задание 5
        public static void TaskFifth()
        {
            string fileName = "applicants.xml";

            Console.WriteLine("Заполнить данные об абитуриентах? (да/нет)");
            if (Console.ReadLine()?.ToLower() == "да")
            {
                FillApplicantsFile(fileName);
            }

            List<Applicant> applicants = LoadApplicantsFromFile(fileName);
            DisplayFailedApplicants(applicants);

        }

        private static void FillApplicantsFile(string fileName)
        {
            List<Applicant> applicants = new List<Applicant>();

            Console.WriteLine("Введите данные абитуриентов (Фамилия Имя Баллы), пустая строка для завершения:");

            try
            {
                while (true)
                {
                    string input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input)) break;

                    string[] parts = input.Split(' ');
                    if (parts.Length != 3)
                    {
                        Console.WriteLine("Ошибка ввода. Введите данные в формате: Фамилия Имя Баллы");
                        continue;
                    }

                    try
                    {
                        applicants.Add(new Applicant
                        {
                            Surname = parts[0],
                            Name = parts[1],
                            Score = int.Parse(parts[2]),
                        });
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: баллы должны быть целыми числами.");
                    }
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<Applicant>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(fs, applicants);
                }

                Console.WriteLine("Данные успешно сохранены в файл.");
            }
            catch (Exception)
            {

                throw;
            }
        }
        


            private static List<Applicant> LoadApplicantsFromFile(string fileName)
            {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с данными не найден.");
                return new List<Applicant>();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Applicant>));
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return (List<Applicant>)serializer.Deserialize(fs);
            }
             }

        private static void DisplayFailedApplicants(List<Applicant> applicants)
        {
            SortedList<string, Applicant> failedApplicants = new SortedList<string, Applicant>();

            foreach (var applicant in applicants)
            {
                if (applicant.Score < 30)
                {
                    string key = $"{applicant.Surname} {applicant.Name}";
                    failedApplicants.Add(key, applicant);
                }
            }

            Console.WriteLine("Абитуриенты, не допущенные к экзаменам:");
            foreach (var key in failedApplicants.Keys)
            {
                Console.WriteLine(key);
            }
        }

        public class Applicant
        {
            public string Surname { get; set; }
            public string Name { get; set; }
            public int Score { get; set; }
        }

    }
}
