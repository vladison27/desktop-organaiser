using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Logist
{
    class Program
    {
        static List<string> docExt = new List<string>() { "doc", "docx", "docm", "dotx", "dotm",
                                                          "xlsx", "xlsm", "xltx", "xltm", "xlsb", 
                                                          "xlam", "txt",
                                                          "pptx", "pptm", "ppsx", "ppsm", "potx",
                                                          "potm", "ppam" , "pdf" };                 // extentions of document files

        static List<string> pictureExt = new List<string>() { "png", "jpg", "jpeg", "psd", "tiff",  // extentions of picture files
                                                              "bmp", "gif", "ico" };

        static List<string> musicExt = new List<string>() { "mp3" };        // extentions of music files

        static List<string> videoExt = new List<string>() { "3g2", "3gp", "3gp2" , "3gpp", "3gpp2",     // extentions of video files
                                                            "asf", "asx", "avi", "bin", "dat", "drv",
                                                            "mkv", "mp4" };

       static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);       // path of desktop

        static List<string> FilesInDir()        // it return files in our directory
        {
            List<string> files = new List<string>();        //create list to filling and output
            DirectoryInfo dir = new DirectoryInfo(path);    // create info about of desktop's dir.

            foreach(var item in dir.GetFiles()) files.Add(Convert.ToString(item));      // add files in list

            return files;
        }

        static void Moving(List<string> exts, string fileName)      // it can move our files
        {
            string docPath    = @"C:\Users\domni\Desktop\Documents";        // path's to dirs
            string picPath    =    @"C:\Users\domni\Desktop\Photos";
            string musicPath  =    @"C:\Users\domni\Desktop\Audios";
            string videosPath =    @"C:\Users\domni\Desktop\Videos";

            switch (exts[0])
            {
                case "doc":         // checking extentions
                    try
                    {
                        File.Move(path + @"\" + fileName, docPath + @"\" + fileName);           // moving file 
                        Console.WriteLine("Файл " + fileName + " успешно перенесён в подготовленное ему место!");   //print about of moving file
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("В его месте уже лежит такой файл и он был обнволён.");       // it's need if file is already in directory
                        File.Delete(docPath + @"\" + fileName);
                        File.Move(path + @"\" + fileName, docPath + @"\" + fileName);
                        break;
                    }

                case "png":
                    try
                    {
                        File.Move(path + @"\" + fileName, picPath + @"\" + fileName);
                        Console.WriteLine("Файл " + fileName + " успешно перенесён в подготовленное ему место!");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("В его месте уже лежит такой файл и он был обнволён.");
                        File.Delete(picPath + @"\" + fileName);
                        File.Move(path + @"\" + fileName, picPath + @"\" + fileName);
                        break;
                    }

                case "mp3":
                    try
                    {
                        File.Move(path + @"\" + fileName, musicPath + @"\" + fileName);
                        Console.WriteLine("Файл " + fileName + " успешно перенесён в подготовленное ему место!");
                        break;
                    }
                    catch
                    {
                        File.Delete(musicPath + @"\" + fileName);
                        File.Move(path + @"\" + fileName, musicPath + @"\" + fileName);
                        Console.WriteLine("В его месте уже лежит такой файл и он был обнволён.");
                        break;
                    }

                case "3g2":
                    File.Move(path + @"\" + fileName, videosPath + @"\" + fileName);
                    break;
            }
        }

        static bool ExtentionCheck(List<string> data, string ext)       // it check extentions of your file
        {
            char dot = '.';
            string[] extent = ext.Split(dot);       // split file to ["name", "extention"]

            foreach(string i in data) if (extent[1] == i) return true; // if extention lie in "data", then return "true"

            return false;
        }

        static void Watch()
        {
            List<string> files = DeleteAnyDirs();

            foreach(string i in files)      // checking files in desktop
            {
                if (ExtentionCheck(docExt, i))     Moving(docExt, i);
                if (ExtentionCheck(pictureExt, i)) Moving(pictureExt, i);
                if (ExtentionCheck(musicExt, i))   Moving(musicExt, i);
                if (ExtentionCheck(videoExt, i))   Moving(videoExt, i);
            }

        }
        static List<string> DeleteAnyDirs()     // if file is not a dir, then add file is writable
        {
            char dot = '.';
            string[] a;
            List<string> endFiles = new List<string>();

            foreach(string i in FilesInDir())
            {
                a = i.Split(dot);
                if(a.Length > 1) endFiles.Add(i);
            }
            return endFiles;
        }

        static void Main(string[] args)     // running program
        {
            Console.WriteLine("Нажмите ENTER, чтобы запустить программу.");
            Console.ReadLine();
            while(true)
            {
                Watch();
                Thread.Sleep(10000);
            }
        }
    }
}
