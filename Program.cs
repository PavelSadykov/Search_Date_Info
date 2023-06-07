using System;
using System.IO;
namespace Date_Info;
class Program
{
    static void Main()
    {
        // Получаем данные от пользователя
        Console.Write("Введите дату последнего изменения (дд.мм.гггг): ");
        string lastModifiedDate = Console.ReadLine();
        Console.Write("Введите имя файла с идентификаторами (без расширения): ");
        string fileName = Console.ReadLine();

        // Получаем пути к папкам "Мои документы" и "Рабочий стол"
        string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Ищем файлы в указанных папках
        string[] documentFiles = Directory.GetFiles(myDocumentsPath, $"{fileName}.txt", SearchOption.AllDirectories);
        string[] desktopFiles = Directory.GetFiles(desktopPath, $"{fileName}.txt", SearchOption.AllDirectories);

        // Проверяем файлы на соответствие дате последнего изменения
        string filePath = FindFileByModifiedDate(documentFiles, lastModifiedDate);
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = FindFileByModifiedDate(desktopFiles, lastModifiedDate);
        }

        // Выводим результат
        if (!string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine($"Найден файл: {filePath}");
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }

        Console.ReadLine();
    }

    // Метод для поиска файла по указанной дате последнего изменения
    static string FindFileByModifiedDate(string[] files, string lastModifiedDate)
    {
        foreach (string file in files)
        {
            DateTime modifiedDate = File.GetLastWriteTime(file);
            string formattedModifiedDate = modifiedDate.ToString("dd.MM.yyyy");

            if (formattedModifiedDate == lastModifiedDate)
            {
                return file;
            }
        }

        return null;
    }
}
