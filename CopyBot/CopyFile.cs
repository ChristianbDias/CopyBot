using System;
using System.Configuration;
using System.IO;

namespace CopyBot
{
    internal class CopyFile
    {
        static string date = DateTime.Now.ToString("yyyyMMdd");
        static string sourceDirectoryApp = ConfigurationManager.AppSettings["source"];
        static string targetDirectoryApp = ConfigurationManager.AppSettings["target"];
        public static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {

            string sourceDirectory = sourceDirectoryApp + date;
            string targetDirectory = targetDirectoryApp + date;
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);

            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            FileInfo fileOrigin;
            string fileWrite2;

            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fileOrigin = new FileInfo(Path.Combine(source.ToString(), fi.Name));
                string fileWrite1 = fileOrigin.LastWriteTime.ToString("dd/MM/yyyy HH:mm");

                string path1 = Path.Combine(target.ToString(), fi.Name);
                FileInfo fi2 = new FileInfo(path1);
                fileWrite2 = fi2.LastWriteTime.ToString("dd/MM/yyyy HH:mm");


                if (File.Exists(Path.Combine(target.ToString(), fi.Name)) == false)
                {
                    Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), false);
                }
                else if (File.Exists(Path.Combine(target.ToString(), fi.Name)) == true && fileWrite1 != fileWrite2)
                {
                    Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                }

            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
