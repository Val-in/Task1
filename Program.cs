using System;
using System.IO;

namespace Task1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Введите путь к нужной папке: ");
            string folderPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(folderPath) == false)
            {
                if (Directory.Exists(folderPath))
                {
                    CleanDirectory(folderPath);
                }
                else
                {
                    Console.WriteLine("Папки по такому пути не существует");
                }
            }
            else
            {
                Console.WriteLine("Вы ввели некорректный путь к нужной папке");
            }
        }

        public static void CleanDirectory(string folderPath)
        {
            DateTime unusedFolder = DateTime.Now - TimeSpan.FromMinutes(30);
            string[] filePaths = Directory.GetFiles(folderPath);
            
            foreach (string filePath in filePaths)
            {
                DateTime lastAccessTime = File.GetLastAccessTime(filePath);
                Console.WriteLine($"File: {filePath}, Last Access Time: {lastAccessTime}");
                if (lastAccessTime < unusedFolder)
                {
                    Console.WriteLine($"Deleting file: {filePath}");
                    File.Delete(filePath);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"{filePath} was used within 30 mins.");
                    Console.WriteLine();
                }
            }

            string[] directoryPaths = Directory.GetDirectories(folderPath);

            foreach (string directoryPath in directoryPaths)
            {
                Console.WriteLine(directoryPath);
                Console.WriteLine();
            }

            foreach (string directoryPath in directoryPaths)
            {
                DateTime lastAccessTime = Directory.GetLastAccessTime(directoryPath);

                if (lastAccessTime < unusedFolder)
                {
                    Directory.Delete(directoryPath, true);
                }
                else
                {
                    CleanDirectory(directoryPath);
                }
            }
        }
    }
}