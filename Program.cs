using System;
using System.IO;

namespace Date_Info
{
    class Program
    {
        static void Main()
        {
            // Получаем данные от пользователя
            Console.Write("Введите дату последнего изменения (дд.мм.гггг): ");
            string lastModifiedDate = Console.ReadLine();
            Console.Write("Введите имя файла  (без расширения): ");
            string fileName = Console.ReadLine();
            Console.Write("Введите  идентификатор: ");
            string fileNIdetity = Console.ReadLine();

            // Получаем путь к папке, где находится файл 
            string programDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Формируем полный путь к файлу
            string filePath = Path.Combine(programDirectory, $"{fileName}.txt");

            // Проверяем существование файла и соответствие дате последнего изменения
            if (File.Exists(filePath))
            {
                string[] files = new string[] { filePath };


                // Обращаемся  к Методу для поиска файла по указанной дате последнего изменения
                // и проверки  идентификатора
                string foundFile = FindFileByModifiedDate(files, lastModifiedDate, fileNIdetity);

                if (!string.IsNullOrEmpty(foundFile))
                {
                    Console.WriteLine($"Найден файл: {foundFile}");
                }
                else
                {
                    Console.WriteLine("Идентификатор или дата последнего изменения не совпадает.");
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }


            Console.ReadLine();
        }

        // Метод для поиска файла по указанной дате последнего измененияи проверки  идентификатора
        static string FindFileByModifiedDate(string[] files, string lastModifiedDate, string fileNIdetity)
        {
            foreach (string file in files)
            {
                try
                {
                   //Определяем  дату последнего изменения файла и приводим ее в нужный строковый формат
                    DateTime modifiedDate = File.GetLastWriteTime(file);
                    string formattedModifiedDate = modifiedDate.ToString("dd.MM.yyyy");

                    //Если дата создания файла  совпадает с датой предоставленным пользователем,
                    //это указывает на то, что дата последнего изменения файла соответствует указанной дате
                    if (formattedModifiedDate == lastModifiedDate)
                    {
                        // Считываем содержимое файла
                        string[] lines = File.ReadAllLines(file);

                        // Проверяем соответствие считанного текста из файла введенному идентификатору
                        foreach (string line in lines)
                        {
                            if (line == fileNIdetity)
                            {
                     
                                return file; // Файл с идентификатором найден
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Несанкционированный доступ");
                }
            }

            return null; // Файл не найден
        }
    }
}

