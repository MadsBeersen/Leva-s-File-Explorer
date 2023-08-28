using System;
using System.Collections.Generic;
using System.IO;

namespace ПроводникНахуй
{
    class Program
    {
        static void Main()
        {
            Commands.Interact();
        }
    }

    class Commands
    {
        private static void Help()
        {
            Console.WriteLine("\nopenFolder - открытие папки внутри данной директории либо по заданному пути");
            Console.WriteLine("createFolder - создание папки");
            Console.WriteLine("openFile - открытие файла");
            Console.WriteLine("createFile - создание файла");
            Console.WriteLine("openFile - открытие файла");
            Console.WriteLine("out - выход");
        }

        private static void HelpAtTheBeggining()
        {
            Console.WriteLine("openFolder - открытие папки внутри данной директории либо по заданному пути");
            Console.WriteLine("out - выход");
        }

        public static void Interact(string path)
        {
            Console.WriteLine("\nПроводникНахуй\n2023 by Lev. Free distribution software\n");

        //обработка комманд        
        CommandType:
            Console.WriteLine("\n-help для просмотра комманд\n");
            string typed = Console.ReadLine();

            switch (typed)
            {
                case "-help":
                    Commands.Help();
                    goto CommandType;
                case "openFolder":
                    Folder.OpenFolder();
                    break;
                case "createFolder":
                    Folder.CreateFolder(path);
                    break;
                case "openFile":
                    Folder.OpenFile(path);
                    break;
                case "createFile":
                    Folder.CreateFile(path);
                    break;
                case "out":
                    Environment.Exit(0);
                    break;
                case "Ника":
                    Console.WriteLine("Устретимся завтра возле островов.");
                    goto CommandType;
                case "Лёва":
                    Console.WriteLine("шарик в ванной.");
                    goto CommandType;
                default:
                    Console.WriteLine("Неправильно указан аргумент!");
                    goto CommandType;
            }
            Console.Clear();

        }

        public static void Interact()
        {
            Console.WriteLine("ПроводникНахуй\n2023 by Lev. Free distribution software\n");

        //обработка комманд        
        CommandType:
            Console.WriteLine("-help для просмотра комманд\n");
            string typed = Console.ReadLine();
            switch (typed)
            {
                case "-help":
                    Commands.HelpAtTheBeggining();
                    goto CommandType;
                case "openFolder":
                    Folder.OpenFolder();
                    break;
                case "out":
                    Environment.Exit(0);
                    break;
                case "Ника":
                    Console.WriteLine("синий гуманоид.");
                    goto CommandType;
                case "Лёва":
                    Console.WriteLine("красный гуманоид.");
                    goto CommandType;
                default:
                    Console.WriteLine("Неправильно указан аргумент!");
                    goto CommandType;
            }
            Console.Clear();
        }
    }

    class Folder
    {
        public static void CreateFile(string directory)
        {
            string fileName;
            while (true)
            {
                if (directory.Equals("C:\\"))
                {
                    Console.WriteLine($"создание файла невозможно в директории C:\\");
                    break;
                }

                Console.Write("Название файла вместе с расширением: ");
                fileName = Console.ReadLine();
                Console.Clear();
                try
                {
                    File.Create(Path.Combine(directory, fileName));
                    Console.WriteLine($"Файл {fileName} скравчен");
                }
                catch
                {
                    Console.WriteLine(Path.Combine(directory, fileName));
                    Console.WriteLine("ошибка в создании файла!");
                    continue;
                }
                break;
            }

            Folder.OpenFolder(directory);
        }

        public static void OpenFile(string directory)
        {
            string content;
            while (true)
            {
                Console.Write("Название файла или полный путь: ");
                string fileName = Console.ReadLine();
                //Console.Clear();
                //попытка нахождения файла с помощью имени
                try
                {
                    content = File.ReadAllText(Path.Combine(directory, fileName));
                }
                catch
                {
                    //попытка нахождения файла с помощью пути
                    try
                    {
                        content = File.ReadAllText(Path.Combine(fileName));
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("Указаный файл не был найден!");
                        continue;
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка доступа!");
                        continue;
                    }
                }
                Console.WriteLine($"\n{fileName}\n{content}");
                break;
            }

            Console.Read();
            OpenFolder(directory);
        }

        public static void CreateFolder(string path)
        {
            Console.Write("Название папки: ");
            string folderName = Console.ReadLine();
            Console.Clear();
            Directory.CreateDirectory(Path.Combine(path, folderName));
            Console.WriteLine($"Папка {folderName} скравчена");
            OpenFolder(path);
        }

        public static void OpenFolder()
        {
            Console.Write("Введите путь: ");
            string path = Console.ReadLine();
            if (!path.EndsWith("\\")) path += "\\";
            Console.Clear();
            try
            {
                OpenFolder(path);
            }
            catch
            {
                Console.WriteLine("Неправильно введен путь!");
                OpenFolder();
            }
        }

        private static void OpenFolder(string path)
        {
            Console.WriteLine(path + "\n");

            IEnumerable<string> pathFiles = Directory.EnumerateFiles(path);
            IEnumerable<string> pathDirectories = Directory.EnumerateDirectories(path);

            //вывод директорий и файло в
            Console.WriteLine("Папки:");
            foreach (var directoryName in pathDirectories)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(path, directoryName));
                Console.Write(directoryInfo.CreationTime);
                Console.Write("\t");
                Console.WriteLine(directoryInfo.FullName);
            }
            Console.WriteLine("Файлы:");
            foreach (var FileName in pathFiles)
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(path, FileName));
                Console.Write(fileInfo.CreationTime);
                Console.Write("\t");
                Console.WriteLine(fileInfo.Name);
            }
            Console.Write("");
            Commands.Interact(path);
        }
    }
}
