using System;
using System.IO;

namespace Task1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter the path to the Folder: ");
            string folderPath = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                if (Directory.Exists(folderPath))
                {
                    CleanFolder(folderPath);
                    Console.WriteLine("Cleaning completed.");
                }
                else
                {
                    Console.WriteLine("Folder with this path does not exist.");
                }
            }
            else
            {
                Console.WriteLine("You entered incorrect folder path.");
            }
        }

        public static void CleanFolder(string folderPath)
        {
            DateTime unusedTime = DateTime.Now - TimeSpan.FromMinutes(30);
            string[] filePaths = Directory.GetFiles(folderPath);
            
            foreach (string filePath in filePaths)
            {
                try
                {
                    DateTime lastWriteTime = File.GetLastWriteTime(filePath);
                    Console.WriteLine($"File: {filePath}, Last Access Time: {lastWriteTime}");
                    if (lastWriteTime < unusedTime)
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete file: {filePath}. Error: {ex.Message}");
                }
            }

            string[] directoryPaths = Directory.GetDirectories(folderPath);

            foreach (string directoryPath in directoryPaths)
            {
                try
                {
                    CleanFolder(directoryPath);
         
                    if (Directory.GetFiles(directoryPath).Length == 0 && Directory.GetDirectories(directoryPath).Length == 0)
                    {
                        DateTime lastWriteTime = Directory.GetLastWriteTime(directoryPath);
                        if (lastWriteTime < unusedTime)
                        {
                            Console.WriteLine($"Deleting directory: {directoryPath}");
                            Directory.Delete(directoryPath, true);
                            Console.WriteLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete directory: {directoryPath}. Error: {ex.Message}");
                }
            }
        }
    }
}